using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Repositories.Interfaces
{
    public interface IRepoGetAddUpdate<T,IDType> : IRepoGetAdd<T,IDType>
    {
        /// <summary>
        /// Update a object in datatable.
        /// </summary>
        void Update(T item);
    }

}
