
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
    
public partial class Technic
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Technic()
    {

        this.Table_part = new HashSet<Table_part>();

    }


    public int Technic_id { get; set; }

    public string Technic_name { get; set; }

    public int CPU_id { get; set; }

    public int Motherboard_id { get; set; }

    public int PSU_id { get; set; }

    public int RAM_id { get; set; }

    public int GPU_id { get; set; }

    public int Technic_type_id { get; set; }



    public virtual CPU CPU { get; set; }

    public virtual GPU GPU { get; set; }

    public virtual Motherboard Motherboard { get; set; }

    public virtual PSU PSU { get; set; }

    public virtual RAM RAM { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Table_part> Table_part { get; set; }

    public virtual Technic_type Technic_type { get; set; }

}

}
