using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cards.Errors;
using Cards.Infrastracture.Data;

namespace Cards.Controllers
{
   
    public class BuggyController : BaseApiController
    {
        private readonly CardsContext _CardsContext;

        public BuggyController(CardsContext CardsContext)
        {
            _CardsContext = CardsContext;
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _CardsContext.Cards.Find(42);
            if (thing == null)
            {
                return NotFound(new APIResponce(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public IActionResult GerServerError()
        {
            var thing = _CardsContext.Cards.Find(42);
            var thingtoReturn = thing.ToString();
            return Ok();
        }
        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new APIResponce(400));
        }
        [HttpGet("BadRequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
        [HttpGet(nameof(testauth))]
        [Authorize]
        public ActionResult<string> testauth()
        {
            return "Test Auth";
        }



    }
}
