//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Standartstyle.AppCode.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CBD_COLLECTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CBD_COLLECTION()
        {
            this.CBD_COLOR = new HashSet<CBD_COLOR>();
        }
    
        public int COLLECTIONCODE { get; set; }
        public Nullable<int> MANCODE { get; set; }
        public string NAME { get; set; }
    
        public virtual CBD_MANUFACTURER CBD_MANUFACTURER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CBD_COLOR> CBD_COLOR { get; set; }
    }
}