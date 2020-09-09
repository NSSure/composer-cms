using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.ProductSystem.DAL.Entity
{
    public class Customer : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public DateTime DateAdded  { get; set; }
        public DateTime DateLastUpdated  { get; set; }
        public Guid UserIDAdded  { get; set; }
        public Guid UserIDLastUpdated  { get; set; }
    }
}
