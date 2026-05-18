using System;
using System.Collections.Generic;
using System.Text;
namespace NEFAB.Repositories.Interfaces
{
    public interface IRepoGetAdd<T,IDType>
    {
        T? GetByID(IDType id);
        List<T> GetAll();
        void Add(T item);
    }
}
