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
    
    public partial class AdTag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdTag()
        {
            this.AdUnits = new HashSet<AdUnit>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdUnit> AdUnits { get; set; }
        public virtual Client Client { get; set; }
    }
}
