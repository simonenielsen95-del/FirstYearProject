using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Domains
{
    public class PackageStatus
    {
        public int PackageStatusId { get; set; }
        public string? Comment { get; set; }
        public StatusType Status { get; set; }
        public DateTime StatusDate { get; set; } = DateTime.Now;

        //navigation properties:
        public int? PackageId { get; set; }
        public string? EmployeeId { get; set; }
    }
}
