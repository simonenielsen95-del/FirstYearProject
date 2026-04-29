using NEFAB.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Repositories;

namespace NEFAB.Services
{
    public class PackageService
    {
        //private readonly _packageRepository;
        private readonly ContainerService _containerService;
        private readonly SupplierService _supplierService;
        private readonly PackageRepository _packageRepository;


        public PackageService() 
        {
            //_packageRepository = new PackageRepository();
            _containerService = new ContainerService();
            _supplierService = new SupplierService();
            _packageRepository = new PackageRepository();
        }

        public void Add(Container container, Supplier supplier, Package package)
        {
            if (container.ContainerNo == null || supplier.SupplierName == null|| package.ProjectNo == null ||
               package.ProjectItemNo == null || package.PackageWeight == null || package.Amount == null || 
               package.InnerQuantity == null || package.PackageLength == null || package.PackageWidth == null ||
               package.PackageHeight == null)
            {
                throw new ArgumentException("Udfyld alle basis informationer om pakken: Container nummer, leverandør, inholdsmængde, antal pakker," +
                    " vægt, længde, brede, højde, projekt-nummer og projet produkt nummer");
            }

            Container containerDB = _containerService.GetByID(container.ContainerNo);
            Supplier supplierDB = _supplierService.GetByID(supplier.SupplierName);

            try
            {
                _packageRepository.Add(container, supplier, package);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("pakken kunne ikke oprettes");
            }
   
        }
        public List<Package> GetByContainerNo(string containerNo) 
        {
            try
            {
                List<Package> packages = _packageRepository.GetPackagesByContainerNo(containerNo);
                return packages;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("pakker kunne ikke findes");
            }
        }
    }
}
