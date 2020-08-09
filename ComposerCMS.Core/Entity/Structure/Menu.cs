﻿using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class Menu : IEntityTracking
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
