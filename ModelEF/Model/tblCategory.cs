﻿namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCategory")]
    public partial class tblCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCategory()
        {
            tblProducts = new HashSet<tblProduct>();
        }
        [Key]
        [StringLength(7)]
        [DisplayName("Mã danh mục")]
        public string Id { get; set; }

        [StringLength(50)]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Trạng thái")]
        public bool? status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct> tblProducts { get; set; }
    }
}
