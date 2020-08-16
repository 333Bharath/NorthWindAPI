using NorthWindAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NorthWindAPI.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        string connectionString = @"database=northwind;server=IN-PF26Z3WV\SQLEXPRESS;user id=sa;pwd=P@ssw0rd";
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Authenticate(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(
                cmdText: $"SELECT * FROM NorthwindUsers where Username=@username and Password=@pwd",
                connection: connection
                );
            cmd.Parameters.AddWithValue("@username", model.Username);
            cmd.Parameters.AddWithValue("@pwd", model.Password);

            try
            {
                connection.Open();
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while(reader.Read())
                {
                    model.UserId = Convert.ToInt32("0" + reader["UserId"].ToString());
                    var usernameBytes = Encoding.UTF8.GetBytes(model.Username);
                    var hash = SHA256.Create().ComputeHash(usernameBytes);
 
                    var hashString = "";
                    foreach (var item in hash)
                    {
                        hashString += $"{item:X}";
                    }
                    model.Token = hashString;
                
          
                }
                reader.Close();
                //update the token in the db too
                //skipped for now
                return Ok(model);

            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }




        }




    }
}
