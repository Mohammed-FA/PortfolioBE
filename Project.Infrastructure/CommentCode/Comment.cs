using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Project.Infrastructure.CommentCode
{
    public class Comment
    {
        public static async Task<string> CheckFile(IFormFile imgurl, IConfiguration con, IWebHostEnvironment env, string host)
        {

            List<string> extention = con.GetSection("uploadFile:allowedFileExtension").Get<List<string>>()!;

            string ex = Path.GetExtension(imgurl.FileName);
            if (!extention.Contains(ex.ToLower()))
            {
                throw new Exception($"File extension '{ex}' is not allowed.");
            }
            int MaxSize = con.GetSection("uploadFile:MaxSize").Get<int>()! * 1024 * 1024;

            if (!(MaxSize > imgurl.Length))
            {
                throw new Exception($"File size {imgurl.Length / (1024 * 1024)}MB exceeds the maximum allowed {MaxSize / (1024 * 1024)}MB.");
            }

            string filename = Guid.NewGuid().ToString() + ex;
            string subFile = con.GetSection("uploadFile:subFile").Value!;

            string path = Path.Combine(env.WebRootPath, subFile, filename);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                await imgurl.CopyToAsync(fs);
            }

            return $"https://{host}/{subFile}/{filename}";
        }

        public static void RemoveFile(string imgurl, IConfiguration con, IWebHostEnvironment env)
        {

            string subFile = con.GetSection("uploadFile:subFile").Value!;
            try
            {

                var fileName = Path.GetFileName(new Uri(imgurl).LocalPath);

                var filePath = Path.Combine(env.WebRootPath, subFile, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch { }
        }

    }
}
