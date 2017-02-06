using Sabio.Web.Models.Requests.Media;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class _MediaService : BaseService, IMediaService
    {
        public int InsertMedia(MediaAddRequest model)
        {
            int uid = 0;
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Media_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@userid", model.UserId);
                   paramCollection.AddWithValue("@datatype", model.DataType);
                   paramCollection.AddWithValue("@url", model.Url);

                   SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out uid);
               }
               );
            return uid;
        }
    }
}