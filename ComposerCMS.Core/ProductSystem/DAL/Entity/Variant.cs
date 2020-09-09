using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.ProductSystem.DAL.Entity
{
    public class Variant : IEntityTracking
    {
        public Guid ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
