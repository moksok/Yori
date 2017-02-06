using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Registration;
using Sabio.Web.Models.Requests.UserProfile;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class _UserProfileService : BaseService, IUserProfileService
    {


        public int CreateProfile(string userId, CreateUserProfileRequest Model) // post
        {
            int _id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserProfile_InsertNewUser"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {

                  paramCollection.AddWithValue("@firstName", Model.FirstName);
                  paramCollection.AddWithValue("@lastName", Model.LastName);
                  paramCollection.AddWithValue("@userName", Model.UserName);
                  paramCollection.AddWithValue("@tagline", Model.Tagline);
                  paramCollection.AddWithValue("@userID", userId);
                  SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                  p.Direction = System.Data.ParameterDirection.Output;

                  paramCollection.Add(p);

              },
               returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out _id);
               }
               );
            return _id;
        }


        public UserProfileDomain GetUserInfoById(string UserId)
        {
            UserProfileDomain UserInfo = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.UserProfiles_GetCurrentUser"

                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", UserId);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    UserInfo = new UserProfileDomain();
                    int startingIndex = 0;

                    UserInfo.Id = reader.GetSafeInt32(startingIndex++);
                    UserInfo.CreatedDate = reader.GetDateTime(startingIndex++);
                    UserInfo.FirstName = reader.GetSafeString(startingIndex++);
                    UserInfo.LastName = reader.GetSafeString(startingIndex++);
                    UserInfo.UserName = reader.GetSafeString(startingIndex++);
                    UserInfo.UserId = reader.GetSafeString(startingIndex++);
                    UserInfo.Tagline = reader.GetSafeString(startingIndex++);
                    UserInfo.Email = reader.GetSafeString(startingIndex++);
                }
            );

            return UserInfo;
        }


        public void Put(UpdateUserInfoRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserProfiles_UpdateInfo"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@firstName", model.FirstName);
                   paramCollection.AddWithValue("@lastName", model.LastName);
                   paramCollection.AddWithValue("@tagLine", model.Tagline);
                   paramCollection.AddWithValue("@userId", model.id);
               }

            );
        }
    }
}