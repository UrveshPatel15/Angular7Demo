using Angular_Demo_API.Models;
using Angular_Demo_API.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Web.Http.Results;

namespace Angular_Demo_API.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        public Angular_demoEntities DB;
        private _IAllRepositry<Employee> EmployeeObj;
        private _IAllRepositry<Country> countryObj;
        private _IAllRepositry<State> stateObj;
        private _IAllRepositry<City> cityObj;
        private _IAllRepositry<Skill> skillObj;


        public EmployeeController()
        {
            this.EmployeeObj = new AllRepositry<Employee>();
            this.countryObj = new AllRepositry<Country>();
            this.stateObj = new AllRepositry<State>();
            this.cityObj = new AllRepositry<City>();
            this.skillObj = new AllRepositry<Skill>();
        }

        [HttpGet]
        [Route("GetAllCountry")]
        public IEnumerable<CCountry> GetCountry()
        {
            var Countrylist = countryObj.GetModel().Select(x => new CCountry
            {
                Id = x.Id,
                CountryName = x.CountryName
            });
            return Countrylist;
        }



        [HttpGet]
        [Route("GetAllState")]
        public IEnumerable<CState> GetState()
        {
            var Statelist = stateObj.GetModel().Select(x => new CState
            {
                Id = x.Id,
                StateName = x.StateName,
                CountryId = x.CountryId
            });
            return Statelist;
        }

        [HttpGet]
        [Route("GetAllCity")]
        public IEnumerable<CCity> GetCity()
        {
            var Citylist = cityObj.GetModel().Select(x => new CCity
            {
                Id = x.Id,
                CityName = x.CityName,
                CountryId = x.CountryId,
                StateId = x.StateId
            });

            return Citylist;
        }

        [HttpGet]
        [Route("GetAllSkills")]
        public IEnumerable<CSkill> GetSkills()
        {
            var SkillList = skillObj.GetModel().Select(x => new CSkill
            {
                SkillName = x.SkillName
            });
            return SkillList; 
        }

        [HttpPost]
        [Route("AddSkills")]
        public IHttpActionResult AddSkills(CSkill Skill)
        {
            try
            {
                Skill Sk = new Skill();
                Sk.SkillName = Skill.SkillName;
                skillObj.InsertModel(Sk);
                skillObj.Save();
                if (true)
                {
                    return Ok("Skill Insert Successfully");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        [HttpGet]
        [Route("GetStateByCountryId")]
        public IEnumerable<CState> GetStateByCountryId(int countryId)
        {
            var statelist = stateObj.GetStateByID(countryId).Select(x => new CState
            {
                Id = x.Id,
                StateName = x.StateName,
                CountryId = x.CountryId
            });
            return statelist;
        }

        [HttpGet]
        [Route("GetCityByStateId")]
        public IEnumerable<CCity> GetCityByStateId(int stateId)
        {
            var Citylist = cityObj.GetCityByID(stateId).Select(x => new CCity
            {
                Id = x.Id,
                CityName = x.CityName,
                StateId = x.StateId,
                CountryId = x.CountryId
            });
            return Citylist;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public IHttpActionResult AddEmployee(CEmployee employee)
        {
            try
            {
                string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/ProfileImage");

                if (!System.IO.Directory.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }

                string ImageName = employee.Image;
                string ImagePath = System.IO.Path.Combine(FolderPath, ImageName);

                byte[] ImageByte = Convert.FromBase64String(employee.ImageBase64.Split(',')[1]);
                System.IO.File.WriteAllBytes(ImagePath, ImageByte);

                string IPath = "~/ProfileImage";
                string relativepath = IPath + "/" + ImageName;


                Employee Emp = new Employee();
                Emp.UserId = employee.UserId;
                Emp.FirstName = employee.FirstName;
                Emp.MiddelName = employee.MiddelName;
                Emp.LastName = employee.LastName;
                Emp.BirthDate = employee.BirthDate.Value.ToLocalTime();
                Emp.Gender = employee.Gender;
                Emp.Designation = employee.Designation;
                Emp.Image = relativepath;
                Emp.Role = employee.Role;
                Emp.Department = employee.Department;
                Emp.JoinDate = employee.JoinDate.Value.ToLocalTime();
                Emp.Email = employee.Email;
                Emp.Password = employee.Password;
                Emp.Address = employee.Address;
                Emp.Skill = employee.Skill;
                Emp.Mobile = employee.Mobile;
                Emp.Description = employee.Description;
                Emp.Country = employee.Country;
                Emp.State = employee.State;
                Emp.City = employee.City;
                Emp.ZipCode = employee.ZipCode;
                Emp.Relationship = employee.Relationship;

                EmployeeObj.InsertModel(Emp);
                EmployeeObj.Save();

                if (true)
                {
                    return Ok("Employee Insert Successfully");
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }

        }

        [HttpPost]
        [Route("EditEmployee")]
        public IHttpActionResult EditEmployee(CEmployee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee.ImageBase64 != "")
                    {
                        string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/ProfileImage");
                        string ImageName = employee.Image;
                        string ImagePath = System.IO.Path.Combine(FolderPath, ImageName);

                        byte[] ImageByte = Convert.FromBase64String(employee.ImageBase64.Split(',')[1]);
                        System.IO.File.WriteAllBytes(ImagePath, ImageByte);

                        string IPath = "~/ProfileImage";
                        string relativepath = IPath + "/" + ImageName;
                        employee.Image = relativepath;
                    }

                    Employee Emp = new Employee();
                    Emp.Id = employee.Id;
                    Emp.UserId = employee.UserId;
                    Emp.FirstName = employee.FirstName;
                    Emp.MiddelName = employee.MiddelName;
                    Emp.LastName = employee.LastName;
                    Emp.BirthDate = employee.BirthDate.Value.ToLocalTime();
                    Emp.Gender = employee.Gender;
                    Emp.Designation = employee.Designation;
                    Emp.Image = employee.Image;
                    Emp.Role = employee.Role;
                    Emp.Department = employee.Department;
                    Emp.JoinDate = employee.JoinDate.Value.ToLocalTime();
                    Emp.Email = employee.Email;
                    Emp.Password = employee.Password;
                    Emp.Address = employee.Address;
                    Emp.Skill = employee.Skill;
                    Emp.Mobile = employee.Mobile;
                    Emp.Description = employee.Description;
                    Emp.Country = employee.Country;
                    Emp.State = employee.State;
                    Emp.City = employee.City;
                    Emp.ZipCode = employee.ZipCode;
                    Emp.Relationship = employee.Relationship;
                    EmployeeObj.UpdateModel(Emp);
                    EmployeeObj.Save();

                    if (true)
                    {
                        return Ok("Employee Detail Update sucessfully");
                    }
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.NoContent, "Invalid Data");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetAllEmployee")]
        public IEnumerable<CEmployee> GetAllEmployee()
        {
            try
            {
                var employeelist = EmployeeObj.GetModel().Select(x => new CEmployee
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    MiddelName = x.MiddelName,
                    LastName = x.LastName,
                    BirthDate =x.BirthDate,
                    Gender = x.Gender,
                    Designation = x.Designation,
                    Image = x.Image.Split('~')[1],
                    //ImageBase64 = Base64(x.Image),
                    Role = x.Role,
                    Department = x.Department,
                    JoinDate = x.JoinDate,
                    Email = x.Email,
                    Password = x.Password,
                    Address = x.Address,
                    Skill = x.Skill,
                    Mobile = x.Mobile,
                    Description = x.Description,
                    Country = x.Country,
                    CountryName=GetCountryName(x.Country),
                    State = x.State,
                    StateName=GetStateName(x.State),
                    City = x.City,
                    CityName=GetCityName(x.City),
                    ZipCode = x.ZipCode,
                    Relationship = x.Relationship
                });

                return employeelist;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetAllEmployeeByUserId")]
        public IEnumerable<CEmployee> GetAllEmployeeByUserId(int id)
        {
            try
            {
                var employeelistByUserId = EmployeeObj.GetAllEmployeeById(id).Select(x => new CEmployee
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    MiddelName = x.MiddelName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    Gender = x.Gender,
                    Designation = x.Designation,
                    Image = x.Image.Split('~')[1],
                    //ImageBase64 = Base64(x.Image),
                    Role = x.Role,
                    Department = x.Department,
                    JoinDate = x.JoinDate,
                    Email = x.Email,
                    Password = x.Password,
                    Address = x.Address,
                    Skill = x.Skill,
                    Mobile = x.Mobile,
                    Description = x.Description,
                    Country = x.Country,
                    CountryName = GetCountryName(x.Country),
                    State = x.State,
                    StateName = GetStateName(x.State),
                    City = x.City,
                    CityName = GetCityName(x.City),
                    ZipCode = x.ZipCode,
                    Relationship = x.Relationship
                }).ToList();

                return employeelistByUserId;

            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [HttpGet]
        [Route("GetEmployeeById")]
        public IEnumerable<CEmployee> GetEmployeeById(int Eid)
        {
            
            try
            {
                var employeelist = EmployeeObj.GetEmployeeByid(Eid).Select(x => new CEmployee
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    MiddelName = x.MiddelName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    Gender = x.Gender,
                    Designation = x.Designation,
                    Image = x.Image,
                    ImageBase64=Base64(x.Image),
                    Role = x.Role,
                    Department = x.Department,
                    JoinDate = x.JoinDate,
                    Email = x.Email,
                    Password = x.Password,
                    Address = x.Address,
                    Skill = x.Skill,
                    Mobile = x.Mobile,
                    Description = x.Description,
                    Country = x.Country,
                    State = x.State,
                    City = x.City,
                    ZipCode = x.ZipCode,
                    Relationship = x.Relationship
                });

                return employeelist;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IHttpActionResult DeleteEmployee(int Did)
        {
            var Employee = EmployeeObj.DeleteModel(Did);
            EmployeeObj.Save();

            if (Employee == true)
            {
                return Ok("Employee Delete");
            }
            return null;
        }

        protected static string Base64(string Image)
        {
            string base64String = null;
            string path = System.Web.HttpContext.Current.Server.MapPath(Image);
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                   return base64String = Convert.ToBase64String(imageBytes);

                }
            }
        }

        public string GetCityName(int? id)
        {
            var City = cityObj.GetModelById(id);
            return City.CityName.ToString();
        }

        public string GetStateName(int? id)
        {
            var State = stateObj.GetModelById(id);
            return State.StateName.ToString();
        }

        public string GetCountryName(int? id)
        {
            var Country = countryObj.GetModelById(id);
            return Country.CountryName.ToString();
        }

    }
}
