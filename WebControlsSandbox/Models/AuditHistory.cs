//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TypeAhead.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditHistory
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public Nullable<int> EntityId { get; set; }
        public string ModificationType { get; set; }
        public string Modifications { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
