using Sabio.Web.Models;
using Sabio.Web.Models.Requests.Login;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/login")]
    public class LoginApiController : ApiController
    {
        [Route(), HttpPost]
        public HttpResponseMessage Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (UserService.Signin(model.Username, model.Password))
            {

                //Requesting Username and Password Match via GetUser Method, Assigning to Variable 'uid'
                ApplicationUser uid = UserService.GetUser(model.Username);

                //Declaring Type ItemResponse, Assigning to Variable 'Response'
                ItemResponse<ApplicationUser> Response = new ItemResponse<ApplicationUser>();

                Response.Item = uid;

                //Successful Signin Returns a Cookie to User's Browser
                return Request.CreateResponse(HttpStatusCode.OK, Response);
            }

            else
            {
                //Error Response
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Oops! Your Login attempt didn't get through. Please check that your Username and Password are correct, and then try again.");
            }
        }
    }
}
