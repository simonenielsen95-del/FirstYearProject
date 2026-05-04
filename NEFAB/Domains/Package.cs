using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;

namespace NEFAB.Domains
{
    public class Package
    {
        public int? PackageId { get; set; }
        public long? ProjectNo { get; set; }
        public int? ProjectItemNo { get; set; }
        public int? PackageWeight { get; set; }
        public int? Amount { get; set; }
        public int? InnerQuantity { get; set; }
        public float? PackageLength {  get; set; }
        public float? PackageWidth { get; set; }
        public float? PackageHeight { get; set; }
        public string? Comment { get; set; }
        public byte[]? Image {  get; set; }

        public string? IsComment 
        { 
            get 
            {
                return string.IsNullOrWhiteSpace(Comment) ? null : "*"; 
            } 
        }

        //navigation properties:
        public string? ContainerNo { get; set; }
        public string? SupplierName { get; set; }
        //link og udvidelse?

        // Source - https://stackoverflow.com/q/14337071
        // Posted by Hossein Narimani Rad, modified by community. See post 'Timeline' for change history
        // Retrieved 2026-05-04, License - CC BY-SA 3.0

        public BitmapImage ToImage(byte[] array)
        {
            if (array == null || array.Length == 0) return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(array))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }


    }
}
