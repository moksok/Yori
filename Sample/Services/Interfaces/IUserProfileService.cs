using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Registration;
using Sabio.Web.Models.Requests.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IUserProfileService
    {
        int CreateProfile(string userId, CreateUserProfileRequest model);

        UserProfileDomain GetUserInfoById(string UserId);

        void Put(UpdateUserInfoRequest model);
    }
}
