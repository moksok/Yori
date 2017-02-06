using Microsoft.Practices.Unity;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UserProfile;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UsersApiController : ApiController
    {
        [Dependency]
        public IUserProfileService _UserProfileService { get; set; }

        [Route("current"), HttpGet]
        [Authorize]
        public HttpResponseMessage getUserForProfile()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }

            string UserId = UserService.GetCurrentUserId();

            UserProfileDomain profile = _UserProfileService.GetUserInfoById(UserId);

            ItemResponse<UserProfileDomain> response = new ItemResponse<UserProfileDomain>();

            response.Item = profile;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("update"), HttpPut]
        [Authorize]
        public HttpResponseMessage updateToForm(UpdateUserInfoRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                model.id = UserService.GetCurrentUserId();

                _UserProfileService.Put(model);

                ItemResponse<bool> response = new ItemResponse<bool>();

                response.IsSuccessful = true;

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
    }
}
