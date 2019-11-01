using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular_Demo_API.Models.DAL
{
    public interface _IAllRepositry<T> where T : class
    {
        IEnumerable<T> GetModel();

        IEnumerable<Employee> GetAllEmployeeById(int id);

        IEnumerable<State> GetStateByID(int id);

        IEnumerable<City> GetCityByID(int id);

        IEnumerable<Employee> GetEmployeeByid(int id);

        T GetModelById(int? modelId);

        bool InsertModel(T model);

        bool DeleteModel(int modelId);

        bool UpdateModel(T model);

        void Save();
    }
}
