using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace TGDD_Ver5.Models
{
    public class PaymentMetadata
    {
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public int PaymentMethod { get; set; }
    }
    public class CustomerMetadata
    {
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string NameUser { get; set; }

        [Compare("PasswordUser", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPass { get; set; }
    }

    public class ProductMetadata
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
        public string NamePro { get; set; }
    }
    public class BonuMetadata
    {
        [DefaultValue("~/Content/images/logo.png")]
        public string Img_Bonus { get; set; }
    }
    public class TradeMarkMetadata
    {

    }
    public class BannerMetadata
    {
        
    }
    public class ProductDetailMetadata
    {
        [Required(ErrorMessage = "Vui lòng nhập giá trị")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public int Old_Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá trị")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int ViewQuantity { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá trị")]
        [Range(1, 100, ErrorMessage = "Giá trị phải lớn hơn 0 và nhỏ hơn 100")]
        public int Discount { get; set; }
    }
    public class OrderMetadata
    {

    }
    public class AdminUserMetadata
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
        public int NameUser { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public int PasswordUser { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chức vụ")]
        public int AdminRole { get; set; }
    }
    public class CommentsMetadata
    {
        [Required(ErrorMessage = "Vui lòng chọn")]
        [Range(1, 5, ErrorMessage = "Giá trị phải lớn hơn 0 và nhỏ hơn 5")]
        public int Rating { get; set; }
    }
}