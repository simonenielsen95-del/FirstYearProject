using NEFAB.Domains;
using NEFAB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
namespace NEFAB.Services
{
    public class PackageStatusService
    {
        private readonly PackageStatusRepository _packageStatusRepository;
        public PackageStatusService()
        {
            _packageStatusRepository = new PackageStatusRepository();
        }
        public void Add(PackageStatus packageStatus)
        {
            if (packageStatus == null)
            {
                throw new ArgumentException("Gyldig status mangler.");
            }
            try
            {
                _packageStatusRepository.Add(packageStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Status kunne ikke oprettes.", ex);
            }
        }
        public List<PackageStatus> GetByPackageId(int? packageId)
        {
            try
            {
                List<PackageStatus> packagestatuses = _packageStatusRepository.GetByPackageId(packageId);
                return packagestatuses;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("pakker kunne ikke findes");
            }
        }
    }
}
