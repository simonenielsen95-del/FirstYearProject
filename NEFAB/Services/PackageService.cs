using NEFAB.Domains;
using NEFAB.Repositories;
using System;
using System.Collections.Generic;

using System.Text;

namespace NEFAB.Services
{
    public class PackageService
    {
       
        private readonly ContainerService _containerService;
        private readonly SupplierService _supplierService;
        private readonly PackageRepository _packageRepository;


        public PackageService() 
        {
            
            _containerService = new ContainerService();
            _supplierService = new SupplierService();
            _packageRepository = new PackageRepository();
        }

        public void Add(Package package)
        {
            if (string.IsNullOrWhiteSpace(package.ContainerNo))
                throw new ArgumentException("Udfyld Container nummer.");

            if (string.IsNullOrWhiteSpace(package.SupplierName))
                throw new ArgumentException("Udfyld Leverandør.");

            if (package == null)
                throw new ArgumentException("Pakke data mangler.");

            if (package.ProjectNo == null || package.ProjectItemNo == null)
                throw new ArgumentException("Udfyld projekt-nummer og projekt produkt nummer.");

            if (package.Amount == null || package.InnerQuantity == null)
                throw new ArgumentException("Udfyld inholdsmængde og antal pakker.");

            if (package.PackageWeight == null || package.PackageLength == null || 
                package.PackageWidth == null || package.PackageHeight == null)
                throw new ArgumentException("Udfyld vægt, længde, bredde og højde.");

            Container containerDB = _containerService.GetByID(package.ContainerNo); 
            
            Supplier supplierDB = _supplierService.GetByID(package.SupplierName);
           

            try
            {   
                _packageRepository.Add(package);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Pakken kunne ikke oprettes", ex);
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
        public void Update(Package package) 
        {
            if (string.IsNullOrWhiteSpace(package.ContainerNo))
                throw new ArgumentException("Udfyld Container nummer.");

            if (string.IsNullOrWhiteSpace(package.SupplierName))
                throw new ArgumentException("Udfyld Leverandør.");

            if (package == null)
                throw new ArgumentException("Pakke data mangler.");

            if (package.ProjectNo == null || package.ProjectItemNo == null)
                throw new ArgumentException("Udfyld projekt-nummer og projekt produkt nummer.");

            if (package.Amount == null || package.InnerQuantity == null)
                throw new ArgumentException("Udfyld inholdsmængde og antal pakker.");

            if (package.PackageWeight == null || package.PackageLength == null ||
                package.PackageWidth == null || package.PackageHeight == null)
                throw new ArgumentException("Udfyld vægt, længde, bredde og højde.");

            Container containerDB = _containerService.GetByID(package.ContainerNo);
            Supplier supplierDB = _supplierService.GetByID(package.SupplierName);
            try
            {
                _packageRepository.Update(package);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Pakken kunne ikke oprettes", ex);
            }
        }


        public void Remove(Package package)
        {
            if (package == null || package.PackageId == null)
            {
                throw new ArgumentException("Pakken kunne ikke slettes.");
            }

            try
            {
                _packageRepository.Remove(package);
            }
            catch (Exception ex)
            {
                throw new Exception("Der opstod en fejl under sletning af pakken", ex);
            }
        }

    }
}
