using Amazon.Runtime.Internal;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Sabio.Web.Exceptions;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Registration;
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
    [RoutePrefix("api/public")]
    public class RegistrationApiController : ApiController
    {
        [Dependency]
        public IUserProfileService _UserProfileService { get; set; }

        [Route("registration"), HttpPost]
        public HttpResponseMessage PostRegistration(RegistrationRequest model)
        {
            IdentityUser newUserRegistration;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            // When model is valid, CreateUser will post email and pw to database
            try
            {

                newUserRegistration = UserService.CreateUser(model.Email, model.Password, model.Username);

                CreateUserProfileRequest NewUserProfile = new CreateUserProfileRequest();
                NewUserProfile.UserName = model.Username;
                NewUserProfile.FirstName = model.FirstName;
                NewUserProfile.LastName = model.LastName;

                _UserProfileService.CreateProfile(newUserRegistration.Id, NewUserProfile);


            }
            catch (IdentityResultException) // Display error code and message if user was not created
            {

                var ExceptionError = new ErrorResponse();

                return Request.CreateResponse(HttpStatusCode.BadRequest, ExceptionError);

            }

            // Insert new user's id into token table and generate new token
            //string UserId = newUserRegistration.Id;

            //Guid NewToken = _IUserTokenService.Insert(UserId);

            // Pass new user's email and token into emailservice for activation link
            //try
            //{

            //    string NewUserEmail = newUserRegistration.Email;

            //    UserEmailService.SendProfileEmail(NewToken, NewUserEmail);

            //}
            //catch (NotImplementedException)
            //{

            //    var ExceptionError = new ErrorResponse("Failed to send activation email to new user");

            //    return Request.CreateResponse(HttpStatusCode.BadRequest, ExceptionError);

            //}


            //SystemEventService.AddSystemEvent(new AddSystemEventModel
            //{
            //    ActorUserId = UserId,
            //    ActorType = ActorType.User,
            //    EventType = SystemEventType.UserRegistration
            //});

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
