using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Net;

namespace MarketplaceBackend.Services
{
    public class S3FileService: IFileService
    {
        private readonly string _bucketName;
        private readonly AmazonS3Client _awsS3Client;

        public S3FileService(IConfiguration configuration)
        {
			_bucketName = configuration.GetValue<string>("AWS:BucketName");
			_awsS3Client = new AmazonS3Client(
				configuration.GetValue<string>("AWS:AccessID"), 
				configuration.GetValue<string>("AWS:SecretName"), 
				RegionEndpoint.GetBySystemName(configuration.GetValue<string>("AWS:Region")));
		}

		public async Task<bool> UploadFileAsync(IFormFile file, string name)
		{
			try
			{
				using (var newMemoryStream = new MemoryStream())
				{
					file.CopyTo(newMemoryStream);

					var uploadRequest = new TransferUtilityUploadRequest
					{
						InputStream = newMemoryStream,
						Key = name,
						BucketName = _bucketName,
						ContentType = file.ContentType,
						CannedACL = S3CannedACL.PublicRead
					};

					var fileTransferUtility = new TransferUtility(_awsS3Client);

					await fileTransferUtility.UploadAsync(uploadRequest);

					return true;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<byte[]> DownloadFileAsync(string file)
		{
			MemoryStream ms = null;

			try
			{
				GetObjectRequest getObjectRequest = new()
				{
					BucketName = _bucketName,
					Key = file
				};

				using (var response = await _awsS3Client.GetObjectAsync(getObjectRequest))
				{
					if (response.HttpStatusCode == HttpStatusCode.OK)
					{
						using (ms = new MemoryStream())
                        {
							await response.ResponseStream.CopyToAsync(ms);
                        }
					}
				}

				if (ms is null || ms.ToArray().Length < 1)
					throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

				return ms.ToArray();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> DeleteFileAsync(string fileName)
		{
			try
			{
				if (!IsFileExists(fileName))
					throw new FileNotFoundException(string.Format("The document '{0}' is not found", fileName));

				await DeleteFile(fileName);
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task DeleteFile(string fileName)
		{
			DeleteObjectRequest request = new DeleteObjectRequest
			{
				BucketName = _bucketName,
				Key = fileName
			};

			await _awsS3Client.DeleteObjectAsync(request);
		}

		public bool IsFileExists(string fileName)
		{
			try
			{
				GetObjectMetadataRequest request = new GetObjectMetadataRequest()
				{
					BucketName = _bucketName,
					Key = fileName
				};

				var response = _awsS3Client.GetObjectMetadataAsync(request).Result;

				return true;
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
				{
					if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
						return false;

					else if (string.Equals(awsEx.ErrorCode, "NotFound"))
						return false;
				}

				throw;
			}
		}
	}
}
