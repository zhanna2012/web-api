using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AppointmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select 
                            a.AppointmentId,
                            a.AppointmentDate, 
                            a.AppointmentStartTime,
                            a.AppointmentEndTime,
                            d.DoctorName, 
                            p.PatientName,
                            a.AppointmentRoomNumber
                            from 
                            Appointment a
                            inner join Doctor d on a.AppointmentDoctorId=d.DoctorId
                            inner join Patient p on a.AppointmentPatientId=p.PatientId;
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
        public JsonResult Post(Appointment dep)
        {
            string query = @"
                           insert into Appointment
                           values (@AppointmentDate, 
                            @AppointmentStartTime, @AppointmentEndTime, 
                            @AppointmentDoctorId, @AppointmentPatientId, 
                            @AppointmentRoomNumber)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AppointmentDate", dep.AppointmentDate);
                    myCommand.Parameters.AddWithValue("@AppointmentStartTime", dep.AppointmentStartTime);
                    myCommand.Parameters.AddWithValue("@AppointmentEndTime", dep.AppointmentEndTime);
                    myCommand.Parameters.AddWithValue("@AppointmentDoctorId", dep.AppointmentDoctorId);
                    myCommand.Parameters.AddWithValue("@AppointmentPatientId", dep.AppointmentPatientId);
                    myCommand.Parameters.AddWithValue("@AppointmentRoomNumber", dep.AppointmentRoomNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Appointment dep)
        {
            string query = @"
                           update Appointment
                           set AppointmentDate= @AppointmentDate,
                            AppointmentStartTime= @AppointmentStartTime,
                            AppointmentEndTime= @AppointmentEndTime,
                            AppointmentDoctorId= @AppointmentDoctorId,
                            AppointmentPatientId= @AppointmentPatientId,
                            AppointmentRoomNumber= @AppointmentRoomNumber
                            where AppointmentId=@AppointmentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AppointmentId", dep.AppointmentId);
                    myCommand.Parameters.AddWithValue("@AppointmentDate", dep.AppointmentDate);
                    myCommand.Parameters.AddWithValue("@AppointmentStartTime", dep.AppointmentStartTime);
                    myCommand.Parameters.AddWithValue("@AppointmentEndTime", dep.AppointmentEndTime);
                    myCommand.Parameters.AddWithValue("@AppointmentDoctorId", dep.AppointmentDoctorId);
                    myCommand.Parameters.AddWithValue("@AppointmentPatientId", dep.AppointmentPatientId);
                    myCommand.Parameters.AddWithValue("@AppointmentRoomNumber", dep.AppointmentRoomNumber);
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
                           delete from Appointment
                           where AppointmentId=@AppointmentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HospitalAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AppointmentId", id);

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
