using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TechnoMobileProject.Models;

namespace TechnoMobileProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getnameController : ControllerBase
    {
        [HttpGet("{cat}")]
        public IEnumerable<myUsers>Get(string cat)
        {
            List <myUsers> users = new List <myUsers> ();
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("TechnoDb");
            SqlConnection conn1=new SqlConnection(conStr);
            conn1.Open();
            string sql;
            sql = "SELECT * FROM usersaccounts where role='" + cat + "'";
            SqlCommand comm = new SqlCommand(sql, conn1);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new myUsers
                {
                    title = (string)reader["title"],
                });
            }
            reader.Close();
            conn1.Close();
            return users;
        }
    }
}
