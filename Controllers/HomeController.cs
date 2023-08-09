using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {

        private readonly DatabaseContext _context;
        private  string connectionstring= "Server = (localdb)\\MSSQLLocalDB; Database = EmpRegistration; Trusted_Connection = True; MultipleActiveResultSets = true";

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Test()
        {
            
            return View();
        }

        //EmployeeList
        public IActionResult Index()
        {
            var employee = new List<Employee>();
            employee = _context.Employee.FromSqlRaw($"EmployeeList").ToList();
            return View(employee);
        }
        public IActionResult Create()
        {
            var state = new List<State>();
            state = _context.State.FromSqlRaw($"StateList").ToList();

            return View(state);
        }
        
        public IActionResult Edit(string id)
        {

            Employee emp = new Employee();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand("GetEmployeeByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    emp.EmployeeId = sdr["EmployeeId"].ToString();
                    emp.EmployeeName = sdr["EmployeeName"].ToString();
                    emp.EmployeeDOB = Convert.ToDateTime(sdr["EmployeeDOB"].ToString());
                    emp.Gender = Convert.ToInt32(sdr["Gender"].ToString());
                    emp.Address = sdr["Address"].ToString();
                    emp.State = sdr["State"].ToString();
                    emp.Hobbies = sdr["Hobbies"].ToString();
                    
                }
            }
           

            var state = new List<State>();
            state = _context.State.FromSqlRaw($"StateList").ToList();
            ViewBag.State = state;
            return View(emp);
        }
        public IActionResult Delete(string id)
        {

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CreateEmployee(string EmployeeName, string EmployeeDOB, int Gender, string Address, string State, string Hobbies)
        {
            
            Employee emp = new Employee
            {
                EmployeeName = EmployeeName,
                EmployeeDOB = Convert.ToDateTime(EmployeeDOB),
                Gender = Convert.ToInt32(Gender),
                Address = Address,
                State = State,
                Hobbies = Hobbies
            };

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeDOB", emp.EmployeeDOB);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Address", emp.Address);
                cmd.Parameters.AddWithValue("@State", emp.State);
                if (!string.IsNullOrEmpty(emp.Hobbies))
                {
                    cmd.Parameters.AddWithValue("@Hobbies", emp.Hobbies);
                }
                
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }


            return Json("");
        }
        [HttpPost]
        public JsonResult UpdateEmployee(string EmployeeId, string EmployeeName, string EmployeeDOB, int Gender, string Address, string State, string Hobbies)
        {
            
            Employee emp = new Employee
            {
                EmployeeId= EmployeeId,
                EmployeeName = EmployeeName,
                EmployeeDOB = Convert.ToDateTime(EmployeeDOB),
                Gender = Convert.ToInt32(Gender),
                Address = Address,
                State = State,
                Hobbies = Hobbies
            };

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeDOB", emp.EmployeeDOB);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Address", emp.Address);
                cmd.Parameters.AddWithValue("@State", emp.State);
                if (!string.IsNullOrEmpty(emp.Hobbies))
                {
                    cmd.Parameters.AddWithValue("@Hobbies", emp.Hobbies);
                }
                
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }


            return Json("");
        }


       
    }
}
