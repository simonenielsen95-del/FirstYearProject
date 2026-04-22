using System;
using NEFAB.Domains;
using NEFAB.Repositories.Interfaces;
using NEFAB.Repositories;

namespace NEFAB.Services
{
    public class ContainerService 
    {
        private readonly IRepoGetAddUpdateRemove<Container, string> _containerRepository;

        public ContainerService(IRepoGetAddUpdateRemove<Container, string> containerRepository)
        {
            _containerRepository = containerRepository; 
        }

        public void Add(string containerNo, string weekYear)
        {
            if (string.IsNullOrEmpty(containerNo) || string.IsNullOrEmpty(weekYear))
                throw new ArgumentException("Udfyld venligst både ContainerNo og Uge-År.");

            string[] parts = weekYear.Split(new char[] { '-', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                throw new ArgumentException("Uge-År formatet er forkert. Brug formatet 'Uge-År' (f.eks. '12-2024').");

            int week = int.Parse(parts[0]);
            int year = int.Parse(parts[1]);

            Container newContainer = new Container(containerNo)
            {
                Week = week,
                Year = year
            };

            _containerRepository.Add(newContainer);
        }

        public void Remove(string containerNo)
        {
            if (string.IsNullOrEmpty(containerNo))
            {
                throw new ArgumentException("Udfyld venligst ContainerNo.");
            }
            Container? container = _containerRepository.GetByID(containerNo);
            if (container == null)
            {
                throw new Exception($"Container {containerNo} blev ikke fundet.");
            }

            _containerRepository.Remove(container);
        }  

        public void Update(string containerNo, string newContainerNo, string weekYear)
        {
            if (string.IsNullOrEmpty(containerNo) || string.IsNullOrEmpty(weekYear) || string.IsNullOrEmpty(newContainerNo))
            {
                throw new ArgumentException("Udfyld venligst både ContainerNo, Nyt ContainerNo og Uge-År.");
            }

            string[] parts = weekYear.Split(new char[] { '-', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                throw new ArgumentException("Uge-År formatet er forkert. Brug formatet 'Uge-År' (f.eks. '12-2024').");
            }

            int week = int.Parse(parts[0]);
            int year = int.Parse(parts[1]);

            Container? container = _containerRepository.GetByID(containerNo);
            if (container == null)
            {
                throw new Exception($"Container {containerNo} blev ikke fundet.");
            }

            container.ContainerNo = newContainerNo;
            container.Week = week;
            container.Year = year;

            _containerRepository.Update(container);
        }
    }
}
