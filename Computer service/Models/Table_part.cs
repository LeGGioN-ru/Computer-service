//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Computer_service.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_part
    {
        public int Entry_number { get; set; }
        public int Service_id { get; set; }
        public int Technic_id { get; set; }
        public int Contract_id { get; set; }
    
        public virtual Contract Contract { get; set; }
        public virtual Service Service { get; set; }
        public virtual Technic Technic { get; set; }
    }
}