using AdwardSoft.Provider.Models;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdwardSoft.Provider.Amazon
{
    public class Amazon
    {
        public async Task<AmazonS3Client> Client(AmazonConfig configInfo)
        {
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = configInfo.ServiceURL;

            AmazonS3Client s3Client = new AmazonS3Client(
                                            configInfo.AccessKey,
                                            configInfo.SecretKey,
                                            config
                                            );
            return s3Client;
        }


        public async Task<bool> DowloandAsync(AmazonS3Client s3Client, string bucketName, string nameFile, IFormFile file, string path)
        {
            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = bucketName;
            request.Key = nameFile;
            var response = await s3Client.GetObjectAsync(request);
            var token = new CancellationToken();
            await response.WriteResponseStreamToFileAsync(path, true, token);


            return true;
        }

        public async Task<bool> CreateOrUpdateAsync(AmazonS3Client s3Client, string bucketName, string nameFile, IFormFile file)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = nameFile,
            };
            request.InputStream = file.OpenReadStream();

            var response = await s3Client.PutObjectAsync(request);
            
            return true;
        }

        public async Task<bool> DeleteAsync(AmazonS3Client s3Client, string bucketName, string nameFile)
        {
            DeleteObjectRequest request = new DeleteObjectRequest();
            request.BucketName = bucketName;
            request.Key = nameFile;
            var response = await s3Client.DeleteObjectAsync(request);
            return true;
        }
    }
}
