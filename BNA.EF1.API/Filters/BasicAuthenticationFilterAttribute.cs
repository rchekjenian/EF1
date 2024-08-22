using BNA.EF1.Application.Common.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace BNA.EF1.API.Filters
{
    public sealed class BasicAuthenticationFilterAttribute : Attribute, IAuthorizationFilter
    {

        private readonly bool _ignoreFilter = false;
        private readonly string _groupKey = string.Empty;
        private string[] _groupsAD = null!;
        private static PrincipalContext? _yourDomain = null;
        private ILogger<BasicAuthenticationFilterAttribute> _logger = null!;
        private IConfiguration _configuration = null!;
        private static ConcurrentCache<string> _userCache = new ConcurrentCache<string>();
        public BasicAuthenticationFilterAttribute(string key, bool ignoreFilter = false)
        {
            _ignoreFilter = ignoreFilter;
            _groupKey = key;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_ignoreFilter)
                return;

            _logger = context.HttpContext.RequestServices.GetService<ILogger<BasicAuthenticationFilterAttribute>>()!;
            _configuration = context.HttpContext.RequestServices.GetService<IConfiguration>()!;

            _groupsAD = GetADGroup();

            try
            {
                var authHeader = context.HttpContext.Request.Headers.Authorization;

                if (authHeader != StringValues.Empty)
                {
                    var authenticationToken = authHeader.ToString().Remove(0, 5);
                    var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                    var usernamePasswordArray = decodedAuthenticationToken.Split(':');
                    var userName = usernamePasswordArray[0];

                    bool isValid = IsMember(userName, _groupsAD);

                    if (isValid)
                    {
                        return;
                    }
                    else
                        _logger.LogInformation($"El usuario {userName} no tiene acceso a {context.HttpContext.GetEndpoint()!.DisplayName}");
                }

                HandleUnathorized(context);
            }
            catch (Exception)
            {
                HandleUnathorized(context);
            }
        }

        private bool IsMember(string userName, string[] groupsAD)
        {
            if (UserIsInCache(userName, groupsAD))
                return true;

            bool isValid = false;

            _yourDomain = new PrincipalContext(ContextType.Domain, Environment.UserDomainName);

            UserPrincipal user = UserPrincipal.FindByIdentity(_yourDomain, userName);

            if (user == null)
                _logger.LogInformation($"No se encontro el usuario {userName}");
            else
            {
                foreach (var groupAD in _groupsAD)
                {
                    GroupPrincipal group = null!;

                    group = GroupPrincipal.FindByIdentity(_yourDomain, groupAD);

                    if (group == null)
                        _logger.LogInformation($"No se encontro el grupo {userName}");
                    else if (user.IsMemberOf(group))
                    {
                        string keyCache = $"{userName}_{groupAD}";

                        isValid = true;
                        int secondsOfLife = int.Parse(_configuration.GetSection("Caching:AuthorizationFiltersCacheExpiration").Value ?? "0");
                        _userCache.Add(keyCache, new Cache<string>(userName, DateTime.Now.AddSeconds(secondsOfLife)));

                        break;
                    }
                }
            }
            return isValid;
        }

        private bool UserIsInCache(string userName, string[] groupsAD)
        {

            bool isInCache = false;

            foreach (string group in groupsAD)
            {
                string keyCache = $"{userName}_{group}";

                _userCache.ValidateCachedValues();

                isInCache = _userCache.GetCachedValue(keyCache) != null;

                if (isInCache)
                    break;
            }

            return isInCache;
        }

        private string[] GetADGroup()
        {
            var keys = _groupKey.Split(';');

            _groupsAD = new string[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                string group = _configuration.GetSection($"AuthorizationGroups:{keys[i]}").Value!;

                if (string.IsNullOrEmpty(group))
                    throw new Exception($"No se configuró una key para el grupo {keys[i]}");

                _groupsAD[i] = group;
            }

            return _groupsAD;
        }

        private static void HandleUnathorized(AuthorizationFilterContext actionContext)
        {
            actionContext.Result = new UnauthorizedResult();
        }
    }
}
