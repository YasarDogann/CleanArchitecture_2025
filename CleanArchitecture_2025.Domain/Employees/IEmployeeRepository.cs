using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture_2025.Domain.Employees
{
    // Ana işlemleri bu repository'de tutarım : Create, update, delete, get
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
