using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUploadAmazonS3.Model
{
    public class UploadFileMPUHighLevelAPI
    {
        private const string bucketName = "exportcsv-heatc";
        public static void UploadFile(string path)
        {
            try
            {
                var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    FilePath = path,
                    CannedACL = S3CannedACL.PublicRead
                };
                PutObjectResponse response = client.PutObject(putRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
