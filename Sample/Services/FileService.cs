using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class FileService
    {
        private static string s3Bucket = "seokbucket/yori";
        private static string serviceUrl = "Https://s3-us-west-2.amazonaws.com";  //US West Oregon Service url

        Amazon.S3.AmazonS3Client S3Client = null;

        public FileService()
        {
            Amazon.S3.AmazonS3Config s3Config = new Amazon.S3.AmazonS3Config();
            s3Config.ServiceURL = FileService.serviceUrl;

            this.S3Client = new Amazon.S3.AmazonS3Client(ConfigService.amazonAccessKey, ConfigService.amazonSecretAccessKey, s3Config);
        }

        public string getS3Url()
        {
            return FileService.serviceUrl + "/" + FileService.s3Bucket;
        }

        public void UploadFile(string filePath, string newFileName, string s3Bucket = null, bool deleteLocalFileOnSuccess = true)
        {
            //save in s3
            Amazon.S3.Model.PutObjectRequest s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest.FilePath = filePath;
            s3PutRequest.BucketName = (s3Bucket != null) ? s3Bucket : FileService.s3Bucket;
            s3PutRequest.CannedACL = Amazon.S3.S3CannedACL.PublicRead;

            //key - new file name
            if (!string.IsNullOrWhiteSpace(newFileName))
            {
                s3PutRequest.Key = newFileName;
            }

            s3PutRequest.Headers.Expires = new DateTime(2020, 1, 1);

            try
            {
                Amazon.S3.Model.PutObjectResponse s3PutResponse = this.S3Client.PutObject(s3PutRequest);

                if (deleteLocalFileOnSuccess)
                {
                    //Delete local file
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception exp = ex;
                //handle exceptions
            }
        }
    }
}