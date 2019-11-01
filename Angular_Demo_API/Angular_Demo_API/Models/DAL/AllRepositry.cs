using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Angular_Demo_API.Models.DAL
{
    public class AllRepositry<T> : _IAllRepositry<T> where T : class
    {
        private Angular_demoEntities DB;
        private IDbSet<T> dbEntity;

        public AllRepositry()
        {
            DB = new Angular_demoEntities();
            dbEntity = DB.Set<T>();


        }

        public bool DeleteModel(int modelId)
        {
            try
            {
                T model = dbEntity.Find(modelId);
                dbEntity.Remove(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<T> GetModel()
        {
            try
            {
                return dbEntity.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IEnumerable<Employee>GetAllEmployeeById(int id)
        {
            try
            {
                var items = DB.Employees.Where(x => x.UserId == id).ToList();
                return items;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<State> GetStateByID(int id)
        {
            try
            {
                var items = DB.States.Where(x => x.CountryId == id).ToList();
                return items;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IEnumerable<City> GetCityByID(int id)
        {
            try
            {
                var items = DB.Cities.Where(x => x.StateId == id).ToList();
                return items;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public IEnumerable<Employee> GetEmployeeByid(int id)
        {
            try
            {
                var item = DB.Employees.Where(x => x.Id == id).ToList();

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public T GetModelById(int? modelId)
        {
            try
            {
                return dbEntity.Find(modelId);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool InsertModel(T model)
        {
            try
            {
                dbEntity.Add(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public void Save()
        {
            try
            {
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                var Ex = ex.Message;
            }
           
        }

        public bool UpdateModel(T model)
        {
            try
            {
                DB.Entry(model).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}