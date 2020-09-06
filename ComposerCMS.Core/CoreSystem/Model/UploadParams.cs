using System;
using System.Collections.Generic;
using System.Text;

namespace ComposerCMS.Core.Model
{
    public enum UploadTarget
    {
        Resource = 0,
        Package = 1
    }

    public class UploadParams
    {
        public UploadTarget UploadTarget { get; set; }
        public Guid? ExternalPackageID { get; set; }
        public string PackageName { get; set; }
    }
}
