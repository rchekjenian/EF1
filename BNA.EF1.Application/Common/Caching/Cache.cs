using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Application.Common.Caching
{
    public sealed class Cache<T>
    {
        public DateTime ExpirationDate { get; }
        public T CachedObject { get; }
        public bool IsExpired() { return DateTime.Now > ExpirationDate; }

        public Cache(T cachedObject, DateTime expirationDate)
        {
            this.ExpirationDate = expirationDate;
            this.CachedObject = cachedObject;
        }
    }
}
