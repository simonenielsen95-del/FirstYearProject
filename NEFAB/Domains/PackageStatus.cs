using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Domains
{
    public class PackageStatus
    {
        public int PackageStatusId { get; }
        public string? Comment { get; set; }

        public enum StatusType
        {
            Modtaget,
            Bestilt,
            Håndteret,
            Forsinket,
            UnderInspektion
        }

        //navigation properties:
        public int? PackageId{ get; }
        public string? EmployeeId { get; }
    }
}
