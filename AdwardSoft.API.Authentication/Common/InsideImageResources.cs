using AdwardSoft.API.Authentication.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Common
{
    public class InsideImageResources
    {
        public string GeneratePhysicalPath(IHostingEnvironment hostingEnvironment, FileType type, ModuleResources module, string value)
        {
            string path = Path.Combine(Path.Combine(hostingEnvironment.WebRootPath, "upload"), type.ToString());
            path = Path.Combine(path, module.ToString());
            path = Path.Combine(path, value);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string GenerateVirtualPath(FileType type, ModuleResources module, string value)
        {
            string path = Path.Combine("upload", type.ToString());
            path = Path.Combine(path, module.ToString());
            path = Path.Combine(path, value);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string GeneratePhysicalPath(IHostingEnvironment hostingEnvironment, FileType type, ModuleResources module)
        {
            string path = Path.Combine(Path.Combine(hostingEnvironment.WebRootPath, "upload"), type.ToString());
            path = Path.Combine(path, module.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string GeneratePhysicalPath(string hostingEnvironment, FileType type, ModuleResources module)
        {
            string path = Path.Combine(Path.Combine(hostingEnvironment, "upload"), type.ToString());
            path = Path.Combine(path, module.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string GeneratePhysicalPath(string hostingEnvironment, FileType type, ModuleResources module, string value)
        {
            string path = Path.Combine(Path.Combine(hostingEnvironment, "upload"), type.ToString());
            path = Path.Combine(path, module.ToString());
            path = Path.Combine(path, value);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public string GenerateVirtualPath(FileType type, ModuleResources module)
        {
            string path = Path.Combine("upload", type.ToString());
            path = Path.Combine(path, module.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public FileSize ParseImageSize(string setting)
        {
            var fileSize = new FileSize();
            bool isValid = true;

            if (setting.Length > 0)
            {
                var data = setting.Split("x");
                if (data.Length == 2)
                {
                    fileSize.Width = Convert.ToInt32(data[0]);
                    fileSize.Height = Convert.ToInt32(data[1]);
                }
                else isValid = !isValid;
            }

            if (!isValid)
            {
                fileSize.Width = 360;
                fileSize.Height = 460;
            }

            return fileSize;
        }
    }

    public enum ModuleResources
    {
        City,
        Food,
        Place,
        ShareExperience,
        Specialties,
        TripIdeas,
        Feedback,
        Festivals,
        Avatar
    }

    public enum FileType
    {
        Images,
        Videos,
        Documents
    }

    public enum ImageAddress
    {
        Server,
        Local,
        URL,
        W_Page
    }
}
