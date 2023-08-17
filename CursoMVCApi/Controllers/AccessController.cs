using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CursoMVCApi.Models.WS;

namespace CursoMVCApi.Controllers
{
    public class AccessController : ApiController
    {
        [HttpGet]
        public Reply HelloWorld()
        {
            Reply oR = new Reply();
            oR.result = 1;
            oR.message = "Hi World";

            return oR;
        }
    }
}
