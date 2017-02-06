using Sabio.Web.Models.Requests.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.UserProfile
{
    public class UpdateUserInfoRequest : CreateUserProfileRequest
    {
        public string id { get; set; }
    }
}