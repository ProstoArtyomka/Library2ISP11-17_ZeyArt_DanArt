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
    
    public partial class AuthorBook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthorBook()
        {
            this.Book1 = new HashSet<Book>();
        }
    
        public int IDBook { get; set; }
        public int IDAuthor { get; set; }
        public int ID { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Book1 { get; set; }
    }
}