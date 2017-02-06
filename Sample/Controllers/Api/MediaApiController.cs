using Microsoft.Practices.Unity;
using Sabio.Web.Models.Requests.Media;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/media")]
    public class MediaApiController : BaseApiController
    {

        [Dependency]
        public IMediaService _MediaService { get; set; }

        [Route(), HttpPost]
        [Authorize]
        public HttpResponseMessage Add()
        {
            string DocFile = "";
            int MediaId = 0;

            HttpRequest HttpRequest = HttpContext.Current.Request;
            if (HttpRequest.Files.Count > 0)
            {
                string FileName = "";
                FileService NewFileUpload = new FileService();

                foreach (string file in HttpRequest.Files)
                {
                    HttpPostedFile PostedFile = HttpRequest.Files[file];
                    string FilePath = HttpContext.Current.Server.MapPath("~/" + PostedFile.FileName);
                    PostedFile.SaveAs(FilePath);

                    DocFile = FilePath;

                    //(string filePath, string newFileName,string s3Bucket,  bool deleteLocalFileOnSuccess)
                    FileName = Guid.NewGuid() + PostedFile.FileName;

                    NewFileUpload.UploadFile(DocFile, FileName, "seokbucket/yori", true);
                }

                MediaAddRequest MediaModel = new MediaAddRequest();

                string MimeType = System.Web.MimeMapping.GetMimeMapping(FileName);

                string UserId = UserService.GetCurrentUserId();

                MediaModel.FileName = FileName;
                //MediaModel.UserId = HttpRequest.Form["UserId"];
                MediaModel.UserId = UserId;
                MediaModel.DataType = MimeType;
                MediaModel.Url = NewFileUpload.getS3Url() + "/" + FileName;

                MediaId = _MediaService.InsertMedia(MediaModel);

            };

            ItemResponse<int> Response = new ItemResponse<int>();
            Response.Item = MediaId;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }
    }
}
