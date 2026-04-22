using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Repositories;
using NEFAB.Domains;

namespace NEFAB.Services
{
    public class ContainerService 
    {
        private readonly ContainerRepository _containerRepository;

        public ContainerService()
        {
            _containerRepository = new ContainerRepository(); 
        }

        public void Add(string ContainerNo) { }


    }
}
