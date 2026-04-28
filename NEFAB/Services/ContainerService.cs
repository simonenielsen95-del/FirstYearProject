using NEFAB.Domains;
using NEFAB.Repositories;
using NEFAB.Repositories.Interfaces;
using System;
//using System.ComponentModel;

namespace NEFAB.Services
{
    public class ContainerService 
    {
        

        //public ContainerService(IRepoGetAddUpdateRemove<Container, string> containerRepository)
        private readonly ContainerRepository _containerRepository;
        public ContainerService()
        {
            _containerRepository = new ContainerRepository(); 
        }

        

        public void Add(Container container)
        {
            if (string.IsNullOrEmpty(container.ContainerNo) || (container.Year == null) || (container.Week== null))
            {
                throw new ArgumentException("Udfyld venligst både ContainerNo, Uge og År.");
            }

            
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

            if (container.Year <1949) 
            {
                throw new ArgumentException("Udfyld venligst et korrekt År.");
            }
            
            if (container.Week <=0 || container.Week >= 54)
            {
                throw new ArgumentException("Udfyld venligst et korrekt uge nr.");
            }

           
            else
            {
                try 
                {
                    _containerRepository.Add(container);
                }
                catch 
                { 
                    throw new ArgumentException("Noget gik galt, prøv igen");
                }

                    
               
            }
            
        }

        public void Remove(Container container)
        {
            if (string.IsNullOrEmpty(container.ContainerNo))
            {
                throw new ArgumentException("Udfyld venligst ContainerNo.");
            }

            Container? containerDB = _containerRepository.GetByID(container.ContainerNo);

            if (containerDB == null)
            {
                throw new Exception($"Container {container.ContainerNo} blev ikke fundet.");
            }
            else
            {
                try
                {
                    _containerRepository.Remove(container);
                }
                catch (Exception)
                {
                    throw new Exception("Containeren kunne ikke slettes, hvis der er pakker i containeren kan den ikke slettes i systemet");
                }
            }
        }  
 

        public void Update(Container container)
        {
            if (string.IsNullOrEmpty(container.ContainerNo) || container.Week == null || container.Year == null)
            {
                throw new ArgumentException("Udfyld venligst både ContainerNo, uge og år.");
            }


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


            Container? containerDB = _containerRepository.GetByID(container.ContainerNo);
            if (containerDB == null)
            {
                throw new Exception($"Container {container.ContainerNo} blev ikke fundet.");
            }

            else
            {
                try
                {
                    _containerRepository.Update(container);
                }
                catch (Exception ex) 
                {
                    throw new Exception("Containeren kunne ikke opdateres");
                }
            }
        }
        public Container? GetByID(string containerNo)
        {
            try
            {
                Container containerDB = _containerRepository.GetByID(containerNo);
                return containerDB;
            }
            catch (Exception ex)
            {
                throw new Exception("Container nummer kunne ikke findes");
            }
        }
    }
}
