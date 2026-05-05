using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Domains
{
    public class PackageStatus
    {
        public int PackageStatusId { get; }
        public string? Comment { get; set; }
        public StatusType Status { get; set; }

        public enum StatusType //Fra forretningens side af
        {
            Modtaget,
            Bestilt,
            Håndteret,
            Forsinket,
            UnderInspektion,
            Leveret
        }

        //navigation properties:
        public int? PackageId { get; set; }
        public string? EmployeeId { get; set; }
    }
}
