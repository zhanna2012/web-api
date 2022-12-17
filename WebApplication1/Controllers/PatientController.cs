using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select PatientId, PatientName, PatientEmail from
                            Patient
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Patient dep)
        {
            string query = @"
                           insert into Patient
                           values (@PatientName, @PatientEmail)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PatientName", dep.PatientName);
                    myCommand.Parameters.AddWithValue("@PatientEmail", dep.PatientEmail);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Patient dep)
        {
            string query = @"
                           update Patient
                           set PatientName= @PatientName, PatientEmail= @PatientEmail
                            where PatientId=@PatientId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PatientId", dep.PatientId);
                    myCommand.Parameters.AddWithValue("@PatientName", dep.PatientName);
                    myCommand.Parameters.AddWithValue("@PatientEmail", dep.PatientEmail);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from Patient
                            where PatientId=@PatientId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PatientId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
