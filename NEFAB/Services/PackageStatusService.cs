using NEFAB.Domains;
using NEFAB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Services
{
    public class PackageStatusService
    {
        private readonly PackageStatusRepository _packageStausRepository;
        public PackageStatusService()
        {
            _packageStausRepository = new PackageStatusRepository();
        }

        public void Add(PackageStatus packageStatus)
        {
            if (packageStatus == null)
            {
                throw new ArgumentException("Gyldig status mangler.");
            }

            try
            {
                _packageStausRepository.Add(packageStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Status kunne ikke oprettes.", ex);
            }
        }
    }
}
