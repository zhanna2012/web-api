using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterDoctorsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FilterDoctorsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JsonResult Get(Doctor dep)
        {
            string query = @"
                          exec filterDoctor @DoctorId, @DoctorName, @DoctorSpeciality
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DoctorId", dep.DoctorId);
                    myCommand.Parameters.AddWithValue("@DoctorName", dep.DoctorName);
                    myCommand.Parameters.AddWithValue("@DoctorSpeciality", dep.DoctorSpeciality);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
