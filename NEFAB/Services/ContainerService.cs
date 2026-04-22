using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Repositories;
using NEFAB.Domains;
using NEFAB.Repositories.Interfaces;

namespace NEFAB.Services
{
    public class ContainerService 
    {
        private readonly IRepoGetAddUpdateRemove<Container, string> _containerRepository;

        public ContainerService(IRepoGetAddUpdateRemove<Container>)
        {
            _containerRepository = containerRepository(); 
        }

        public void Add(Container container)



    }
}
