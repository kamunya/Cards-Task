using Microsoft.AspNetCore.Mvc;
using Cards.Controllers;
using System.Collections.Generic;

namespace Cards.Errors
{
    public class APIValidationErrorResponce : APIResponce
    {
        public APIValidationErrorResponce() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }

    }
}
