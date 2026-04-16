using System.Collections.Generic;
using NEFAB.Domains;

namespace NEFAB.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }

    public interface IContainerRepository : IRepository<Container>
    {
        Container GetByContainerNumber(string containerNumber);
        List<Container> GetByWeek(int week, int year);
    }

    public interface ISupplierRepository : IRepository<Supplier>
    {
        Supplier GetBySupplierName(string supplierName);
    } 

    public interface IEmployeeRepository : IRepository<Employee>
    {
        Supplier GetByEmployeeName(string supplierName);
    }

    


}
