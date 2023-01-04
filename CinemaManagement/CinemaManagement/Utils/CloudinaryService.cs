using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System;

using dotenv.net;

namespace CinemaManagement.Utils
{
    public class CloudinaryService
    {
        private static CloudinaryService _ins;
        public static CloudinaryService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CloudinaryService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private Cloudinary cloudinary;
        private CloudinaryService()
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            cloudinary.Api.Secure = true;
        }



        public async Task<string> UploadImage(string filePath)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath.ToString()),
                    Folder = "TCAssets"
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                Console.WriteLine(uploadResult.JsonObj);

                return uploadResult.SecureUrl.AbsoluteUri;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        public async Task DeleteImage(string imageURL)
        {
            try
            {
                string publicId = GetPublicIdFromURL(imageURL);
                var deletionParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image,
                };

                var deletionResult = await cloudinary.DestroyAsync(deletionParams);
            }
            catch (System.Exception)
            {
                return;
            }
        }

        public async Task<BitmapImage> LoadImageFromURL(string imageURL)
        {
            if (string.IsNullOrEmpty(imageURL))
            {
                return null;
            }
            System.Net.WebRequest request =
                        System.Net.WebRequest.Create(imageURL);
            System.Net.HttpWebResponse response;
            try
            {
                response = (await request.GetResponseAsync()) as System.Net.HttpWebResponse;
            }
            catch (System.Net.WebException)
            {
                return null;
            }

            System.IO.Stream responseStream =
                response.GetResponseStream();

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = responseStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            return bitmap;
        }
        private string GetPublicIdFromURL(string url)
        {
            string strStart = "TCImages";
            string strEnd = ".";
            if (url.Contains("TCImages") && url.Contains("."))
            {
                int Start, End;
                Start = url.IndexOf(strStart, 0);
                End = url.IndexOf(strEnd, Start);
                return url.Substring(Start, End - Start);
            }
            return null;
        }
    }
}
