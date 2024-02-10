using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGDD_Ver5.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
        [NotMapped]
        public string ConfirmPass { get; set; }
    }
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] files { get; set; }
    }
    [MetadataType(typeof(ProductDetailMetadata))]
    public partial class ProDetail
    {
        public void Caculator_Price()
        {
            double op = Old_Price;
            double dc = Discount ?? 0;
            int np = (int)(op - (op * (dc / 100)));
            Price = np;
            int point = np / 1000;
            Point = point;
        }
        [NotMapped]
        public double Status { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] files { get; set; }
    }
    [MetadataType(typeof(BonuMetadata))]
    public partial class Bonu
    {
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
    }
    [MetadataType(typeof(TradeMarkMetadata))]
    public partial class TradeMark
    {
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
    }
    [MetadataType(typeof(BannerMetadata))]
    public partial class Banner
    {
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
    }

    [MetadataType(typeof(OrderMetadata))]
    public partial class OrderPro
    {
        [NotMapped]
        public int Revenue { get; set; }
    }
    [MetadataType(typeof(CommentsMetadata))]
    public partial class Comments
    {
        [NotMapped]
        public HttpPostedFileBase[] files { get; set; }
    }
}