using NEFAB.Domains;
using NEFAB.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace NEFAB.Services
{
    internal class SupplierService
    {
        public SupplierRepository SupplierRepository { get; set; }


        public SupplierService() 
        { 
            SupplierRepository = new SupplierRepository();
        }

        public void Add(Supplier supplier) 
        {
            if (supplier.SupplierName == null)
            {
                throw new Exception("indtast et navn på leverandør");
            }
            else 
            { 
                SupplierRepository.Add(supplier);
            }
        }
    }
}
