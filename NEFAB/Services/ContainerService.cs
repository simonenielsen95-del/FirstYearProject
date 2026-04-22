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
            
            
            for (int i = 0; i < container.ContainerNo.Length; i++)
            {
                if (i <= 3 )
                {
                    char c = container.ContainerNo[i];
                    if(!(c>= 'A' && c <= 'Z'))
                    {
                        throw new ArgumentException("Container nr er ikke af korrekt type: 'ABCD1234567'");
                    }
                }
                if (i > 3) 
                {
                    if (!char.IsDigit(container.ContainerNo[i]))
                    {
                        throw new ArgumentException("Container nr er ikke af korrekt type: 'ABCD1234567'");
                    }
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

        public void Remove(Container container)
        {
            if (string.IsNullOrEmpty(container.ContainerNo))
            {
                throw new ArgumentException("Udfyld venligst ContainerNo.");
            }

            Container? containerDB = ContainerRepository.GetByID(container.ContainerNo);

            if (containerDB == null)
            {
                throw new Exception($"Container {container.ContainerNo} blev ikke fundet.");
            }
            else
            {
                try
                {
                    ContainerRepository.Remove(container);
                }
                catch (Exception)
                {
                    throw new Exception("Containeren kunne ikke slettes, hvis der er pakker i containeren kan den ikke slettes i systemet");
                }
            }
        }  
        // NÅET HER TIL!!

        public void Update(Container container)
        {
            if (string.IsNullOrEmpty(container.ContainerNo) || container.Week == null || container.Year == null)
            {
                throw new ArgumentException("Udfyld venligst både ContainerNo, uge og år.");
            }

            //string[] parts = weekYear.Split(new char[] { '-', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            //if (parts.Length != 2)
            //{
            //    throw new ArgumentException("Uge-År formatet er forkert. Brug formatet 'Uge-År' (f.eks. '12-2024').");
            //}

            //int week = int.Parse(parts[0]);
            //int year = int.Parse(parts[1]);

            if (container.ContainerNo.Length != 11)
            {
                throw new ArgumentException("Udfyld venligst et korrekt container nr.");
            }


            for (int i = 0; i < container.ContainerNo.Length; i++)
            {
                if (i <= 3)
                {
                    char c = container.ContainerNo[i];
                    if (!(c >= 'A' && c <= 'Z'))
                    {
                        throw new ArgumentException("Container nr er ikke af korrekt type: 'ABCD1234567'");
                    }
                }
                if (i > 3)
                {
                    if (!char.IsDigit(container.ContainerNo[i]))
                    {
                        throw new ArgumentException("Container nr er ikke af korrekt type: 'ABCD1234567'");
                    }
                }
            }

            if (container.Year < 1949)
            {
                throw new ArgumentException("Udfyld venligst et korrekt År.");
            }

            if (container.Week <= 0 || container.Week >= 54)
            {
                throw new ArgumentException("Udfyld venligst et korrekt uge nr.");
            }


            Container? containerDB = ContainerRepository.GetByID(container.ContainerNo);
            if (containerDB == null)
            {
                throw new Exception($"Container {container.ContainerNo} blev ikke fundet.");
            }

            //container.ContainerNo = newContainerNo;
            //container.Week = week;
            //container.Year = year;
            else
            {
                try
                {
                    ContainerRepository.Update(container);
                }
                catch (Exception ex) 
                {
                    throw new Exception("Containeren kunne ikke opdateres");
                }
            }
        }
    }
}
