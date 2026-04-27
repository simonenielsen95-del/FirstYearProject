using NEFAB.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Services
{
    public class PackageService
    {
        //private readonly _packageRepository;
        private readonly ContainerService _containerService;
        private readonly SupplierService _supplierService;

        public PackageService() 
        {
            //_packageRepository = new PackageRepository();
            _containerService = new ContainerService();
            _supplierService = new SupplierService();
        }

        public void Add(Container container, Supplier supplier, Package package)
        {
            if (container.ContainerNo == null || supplier.SupplierName == null|| package.ProjectNo == null ||
               package.ProjectItemNo == null || package.PackageWeight == null || package.Amount == null || 
               package.InnerQuantaty == null || package.PackageLength == null || package.PackageWidth == null ||
               package.PackageHeight == null)
            {
                throw new ArgumentException("Udfyld alle basis informationer om pakken: Container nummer, leverandør, inholdsmængde, antal pakker," +
                    " vægt, længde, brede, højde, projekt-nummer og projet produkt nummer");
            }

            Container containerDB = _containerService.GetByID(container.ContainerNo);
            Supplier supplierDB = _supplierService.GetByID(supplier.SupplierName);

            try
            {
                _packageRepo.Add(package, containerDB.ContainerNo, supplierDB.SupplierName);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("pakken kunne ikke oprettes");
            }
   
        }
    }
}
