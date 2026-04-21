using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Repositories.Interfaces
{
    public interface IRepoGetAddUpdateRemove<T, IDType> : IRepoGetAddUpdate<T, IDType>
    {
        /// <summary>
        /// Remove an object from the database
        /// </summary>
        void Remove(T item);
    }
}
