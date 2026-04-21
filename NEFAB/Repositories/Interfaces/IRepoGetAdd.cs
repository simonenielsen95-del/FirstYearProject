using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Repositories.Interfaces
{
    public interface IRepoGetAdd<T,IDType>
    {
        /// <summary>
        /// GetByID accepts the ID type specified when the interface is implemented
        /// </summary>
        T? GetByID(IDType id);
        /// <summary>
        /// Retuns a list with all items from the database table.
        /// </summary>
        List<T> GetAll();
        /// <summary>
        /// Add a new item to the database
        /// </summary>
        void Add(T item);

    }
}
