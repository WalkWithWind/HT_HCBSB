//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HT.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ht_manager_role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ht_manager_role()
        {
            this.ht_manager = new HashSet<ht_manager>();
            this.ht_manager_role_value = new HashSet<ht_manager_role_value>();
        }
    
        public int id { get; set; }
        public string rolename { get; set; }
        public Nullable<int> roletype { get; set; }
        public Nullable<int> issys { get; set; }
        public Nullable<System.DateTime> addtime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ht_manager> ht_manager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ht_manager_role_value> ht_manager_role_value { get; set; }
    }
}
