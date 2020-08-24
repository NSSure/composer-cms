using ComposerCMS.Core.Entity;
using System.Collections.Generic;

namespace ComposerCMS.Core.Model
{
    public class PackageBundle : ExternalPackage
    {
        public List<ExternalResource> PackageResources { get; set; }
    }
}
