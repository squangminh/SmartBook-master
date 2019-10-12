using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models.File
{
    public class ResponsiveFileManagerOptions
    {
        /// <summary>
        /// Path from base_url to base of upload folder. Use start and final /
        /// </summary>
        public string UploadDirectory { get; set; }

        /// <summary>
        /// Relative path from filemanager folder to upload folder. Use final /
        /// </summary>
        public string CurrentPath { get; set; }

        /// <summary>
        /// Relative path from filemanager folder to thumbs folder. Use final / and DO NOT put inside upload folder.
        /// </summary>
        public string ThumbsBasePath { get; set; }
    }
}
