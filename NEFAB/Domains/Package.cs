using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Domains
{
    public class Package
    {
        public int? PackageId { get; set; }
        public int? ProjectNo { get; set; }
        public int? ProjectItemNo { get; set; }
        public int? PackageWeight { get; set; }
        public int? Amount { get; set; }
        public float? PackageLength {  get; set; }
        public float? PackageWidth { get; set; }
        public float? PackageHeight { get; set; }
        public string? Comment { get; set; }



    }
}
