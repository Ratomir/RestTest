using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using Amazon.S3.Transfer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestTest
{
    [Route("[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        public ILogger _logger { get; set; }
        public S3Controller(ILogger<S3Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            Model model = new Model()
            {
                Number = new Random().Next(1, 10),
                Id = Guid.NewGuid().ToString()
            };

            try
            {
                RegionEndpoint reg = RegionEndpoint.GetBySystemName(RegionEndpoint.USEast2.SystemName);
                AmazonS3Client s3Client = new AmazonS3Client(reg);
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model));
                using (MemoryStream newMemoryStream = new MemoryStream(byteArray))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = "superfile.json",
                        BucketName = "week7-bucket"
                    };

                    var fileTransferUtility = new TransferUtility(s3Client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return new string[] { "Action is done, check the file." };
        }
    }
}
