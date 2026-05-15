using System;
using System.Collections.Generic;
using System.Text;
namespace NEFAB.Repositories.Interfaces
{
    public interface IRepoGetAddUpdate<T,IDType> : IRepoGetAdd<T,IDType>
    {
        void Update(T item);
    }
}
