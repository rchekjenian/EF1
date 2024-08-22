using BNA.EF1.API.Controllers.Base;
using BNA.EF1.Application.Example.Commands.CreateExample;
using BNA.EF1.Application.Example.Queries.GetExample;
using Microsoft.AspNetCore.Mvc;

namespace BNA.EF1.API.Controllers
{
    public class ExampleController : ApiBaseController
    {
        /// <summary>
        /// Get an example class
        /// </summary>
        /// <param name="id">example id</param>
        /// <returns>The example by id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetExampleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GetExampleDto>> GetExample(Guid id)
        {
            var result = await Mediator.Send(new GetExampleQuery(id));

            return result.Match(Ok, Problem);
        }


        /// <summary>
        /// Creates a new Example
        /// </summary>
        /// <param name="request">The example</param>
        /// <returns>The example id</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateExample(CreateExampleCommand request)
        {
            var result = await Mediator.Send(request);

            return result.Match(_ => Ok(), Problem);
        }
    }
}
