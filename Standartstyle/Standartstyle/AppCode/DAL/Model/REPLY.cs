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
    
    public partial class REPLY
    {
        public int REPLYCODE { get; set; }
        public Nullable<int> GOODCODE { get; set; }
        public Nullable<int> USERCODE { get; set; }
        public string AUTHOR { get; set; }
        public Nullable<System.DateTime> REPLY_DATE { get; set; }
        public string REPLY_TEXT { get; set; }
        public string AUTHOR_LOCATION { get; set; }
        public Nullable<int> STATE { get; set; }
    
        public virtual GOODS GOODS { get; set; }
        public virtual USERS USERS { get; set; }
    }
}