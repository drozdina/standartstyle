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
    
    public partial class ATTRIBUTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATTRIBUTES()
        {
            this.GOOD_ATTRIBUTES = new HashSet<GOOD_ATTRIBUTES>();
        }
    
        public int ATTRIBUTECODE { get; set; }
        public Nullable<int> CATEGORYCODE { get; set; }
        public string ATTRIBUTENAME { get; set; }
    
        public virtual GOODS_CATEGORY GOODS_CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GOOD_ATTRIBUTES> GOOD_ATTRIBUTES { get; set; }
    }
}
