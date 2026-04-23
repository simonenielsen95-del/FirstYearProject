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
        private readonly SupplierRepository _supplierRepository;


        public SupplierService() 
        {
            _supplierRepository = new SupplierRepository();
        }

        public void Add(Supplier supplier) 
        {
            if (supplier.SupplierName == null)
            {
                throw new Exception("indtast et navn på leverandør");
            }
            else
            {
                try
                {
                    _supplierRepository.Add(supplier);
                }
                catch(Exception) 
                {
                    throw new Exception("Leverandøren kunne ikke oprettes, navnet findes allerede i systemet");
                }
            }
        }
    }
}
