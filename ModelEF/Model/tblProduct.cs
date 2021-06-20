namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProduct")]
    public partial class tblProduct
    {
        [Key]
        [StringLength(7)]
        [DisplayName("Mã sản phẩm")]
        public string Id { get; set; }

        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:N0} VND", ApplyFormatInEditMode = true)]
        [DisplayName("Đơn giá")]
        public decimal? UnitCost { get; set; }
        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }

        [StringLength(100)]
        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [StringLength(50)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [StringLength(7)]
        [DisplayName("Mã danh mục")]
        public string CategoryId { get; set; }

        public virtual tblCategory tblCategory { get; set; }
    }
}
