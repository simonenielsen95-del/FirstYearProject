using System;
using NEFAB.Domains;
using NEFAB.Repositories.Interfaces;
using NEFAB.Repositories;

namespace NEFAB.Services
{
    public class ContainerService 
    {
        //private readonly IRepoGetAddUpdateRemove<Container, string> _containerRepository;

        //public ContainerService(IRepoGetAddUpdateRemove<Container, string> containerRepository)
        public ContainerRepository ContainerRepository {  get; set; }

        public ContainerService()
        {
            ContainerRepository = new ContainerRepository(); 
        }

        public void Add(Container container)
        {
            if (string.IsNullOrEmpty(container.ContainerNo) || (container.Year == null) || (container.Week== null))
            {
                throw new ArgumentException("Udfyld venligst både ContainerNo, Uge og År.");
            }

            //string[] parts = weekYear.Split(new char[] { '-', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            //if (parts.Length != 2)
            //{
            //    throw new ArgumentException("Uge-År formatet er forkert. Brug formatet 'Uge-År' (f.eks. '12-2024').");
            //}
            if (container.ContainerNo.Length != 11)
            {
                throw new ArgumentException("Udfyld venligst et korrekt container nr.");
            }
            List<char> cha = new List<char>();
            cha.Add('A', 'B');
            for (int i = 0; i <= container.ContainerNo.Length; i++)
            {
                if (i >= 0 && i <=3 )
                {
                    
                }
            }

            if (container.Year <1949)
            {
                throw new ArgumentException("Udfyld venligst et korrekt År.");
            }
            
            if (container.Week <=0 || container.Week >= 54)
            {
                throw new ArgumentException("Udfyld venligst et korrekt uge nr.");
            }

            //Container newContainer = new Container()
            //{
            //    ContainerNo = containerNo,
            //    Week = int.Parse(parts[0]),
            //    Year = int.Parse(parts[1])
            //};

            ContainerRepository.Add(container);
        }

        public void Remove(string containerNo)
        {
            if (string.IsNullOrEmpty(containerNo))
            {
                throw new ArgumentException("Udfyld venligst ContainerNo.");
            }

            Container? container = ContainerRepository.GetByID(containerNo);

            if (container == null)
            {
                throw new Exception($"Container {containerNo} blev ikke fundet.");
            }

            ContainerRepository.Remove(container);
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

            Container? container = ContainerRepository.GetByID(containerNo);
            if (container == null)
            {
                throw new Exception($"Container {containerNo} blev ikke fundet.");
            }

            container.ContainerNo = newContainerNo;
            container.Week = week;
            container.Year = year;

            ContainerRepository.Update(container);
        }
    }
}
