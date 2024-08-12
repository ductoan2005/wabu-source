using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Services
{
    public interface IAttachmentsToDOServices
    {
        Task<string> UploadAttachmentsToDO(IFormFile file);
        Task DeleteAttachmentsToDO(string file);
    }
    public class AttachmentsDOServices : IAttachmentsToDOServices
    {
        private readonly AmazonS3Client s3Client;
        public AttachmentsDOServices()
        {
            string accessKey = ConfigurationManager.AppSettings["AccessKey"];
            string secretKey = ConfigurationManager.AppSettings["SecretKey"];
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = ConfigurationManager.AppSettings["ServiceUrl"];
            s3Client = new AmazonS3Client(accessKey, secretKey, config);
        }

        public async Task DeleteAttachmentsToDO(string file)
        {
            if (string.IsNullOrEmpty(file)) return;
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = ConfigurationManager.AppSettings["BucketName"],
                    Key = file
                };

                await s3Client.DeleteObjectAsync(deleteRequest);
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> UploadAttachmentsToDO(IFormFile file)
        {
            if (file == null) return string.Empty;
            try
            {
                string fileUploadName = Guid.NewGuid().ToString();
                var fileTransferUtility = new TransferUtility(s3Client);
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = ConfigurationManager.AppSettings["BucketName"],
                    InputStream = file.OpenReadStream(),
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    Key = fileUploadName,
                    CannedACL = S3CannedACL.PublicRead
                };
                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                return fileUploadName;
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}