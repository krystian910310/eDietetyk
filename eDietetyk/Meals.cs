//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eDietetyk
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meals()
        {
            this.UserMeals = new HashSet<UserMeals>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> Protein { get; set; }
        public Nullable<int> Sugar { get; set; }
        public Nullable<int> Fat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMeals> UserMeals { get; set; }
    }
}
