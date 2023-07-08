using ewms.common.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University.API.Controllers;

namespace ewmsAPI.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //demo
            bool isCredentialValid = (loginDTO.Password == "123456");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginDTO.Username);
                return Ok(token);
            } else
            {
                return Unauthorized();//status code 401
            }       
        }
    }
}