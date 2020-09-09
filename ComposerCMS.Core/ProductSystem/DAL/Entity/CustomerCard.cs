using System;

namespace ComposerCMS.Core.ProductSystem.DAL.Entity
{
    public class CustomerCard
    {
        public Guid ID { get; set; }

        public Guid CustomerID { get; set; }

        /// <summary>
        /// Stripe card token.
        /// </summary>
        public string CardToken { get; set; }
    }
}
