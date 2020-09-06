namespace ComposerCMS.Core.Model
{
    public class RouteItem
    {
        public string Name { get; set; }
        public string VirutalPath { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsAbstract { get; set; }
    }
}
