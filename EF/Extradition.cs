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
    
    public partial class Extradition
    {
        public int ID { get; set; }
        public System.DateTime DateExtradition { get; set; }
        public System.DateTime DateReturn { get; set; }
        public int IDBook { get; set; }
        public int IDClient { get; set; }
        public int IDEmployee { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<bool> IsСompleted { get; set; }
        public Nullable<decimal> ClientDebt { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
