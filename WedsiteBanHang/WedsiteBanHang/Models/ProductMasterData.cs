using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WedsiteBanHang.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Phải nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Avatar { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public Nullable<int> Category { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string ShortDes { get; set; }
        [Display(Name = "Mô tả đầy đủ")]
        public string FullDexciption { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập giá")]
        [Display(Name = "Giá")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public Nullable<double> PiceDiscount { get; set; }
        [Display(Name = "Kiếu")]
        public Nullable<int> Typeld { get; set; }
        public string Slug { get; set; }
        [Display(Name = "Tên thương hiệu")]
        public Nullable<int> BrandId { get; set; }

    }
}