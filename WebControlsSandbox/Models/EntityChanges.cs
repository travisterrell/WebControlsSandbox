using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TypeAhead.Models
{
    public class EntityChanges
    {
        public string EntityName { get; set; }
        public EntityState ChangeType { get; set; }
        public Dictionary<string,object> Original { get; set; }
        public Dictionary<string, object> Current { get; set; }
    }
}