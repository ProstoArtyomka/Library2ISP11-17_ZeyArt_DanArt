//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library2ISP11_17_ZeyArt_DanArt.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Extradition = new HashSet<Extradition>();
            this.Author = new HashSet<Author>();
            this.Genre = new HashSet<Genre>();
        }
    
        public int ID { get; set; }
        public string NameBook { get; set; }
        public int IDPublishing { get; set; }
        public int YearOfPublishing { get; set; }
        public int NumberOfPages { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public byte[] Preview { get; set; }
        public int Cost { get; set; }
    
        public virtual Publishing Publishing { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Extradition> Extradition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author> Author { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genre { get; set; }
    }
}
