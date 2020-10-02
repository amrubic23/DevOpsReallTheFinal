using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication15.Pages
{
    public class UpdateModel : PageModel
    {
        public string Pass => (string)TempData[nameof(Pass)];

        public string newPass1 => (string)TempData[nameof(newPass1)];

        public string newPass2 => (string)TempData[nameof(newPass2)];

        public void OnGet()
        {
            if (((String)HttpContext.Session.GetString("loggedin") != "t"))
                Response.Redirect("login");

            HttpContext.Session.SetString("loggedin", "t");
            HttpContext.Session.SetString("username", (String)HttpContext.Session.GetString("username"));
        }

        public IActionResult OnPostUpdate([FromForm] string pass, [FromForm] string newpass1, [FromForm] string newpass2)
        {
            if (newpass1 == newpass2)
            {
                if (((String)HttpContext.Session.GetString("username") == "admin"))
                {
                    if (CheckLogin(pass))
                    {
                        if (UpdatePassword(newpass1))
                            return RedirectToPage("Index");
                        else
                            return RedirectToPage("Update");
                    }
                    return RedirectToPage("Update");
                }

                else
                {
                    if (CheckLogin(pass))
                    {
                        if (UpdatePassword(newpass1))
                            return RedirectToPage("Request");
                        else
                            return RedirectToPage("Update");
                    }
                    return RedirectToPage("Update");
                }
            }
            return RedirectToPage("Update");
        }
        public bool CheckLogin(string password)
        {
            String query = "SELECT username, password FROM dbo.users WHERE username = @username AND password = @password;";
            String connectionString = "Data Source=192.168.0.212;Initial Catalog=testDB;User ID=sa;Password=7v!SkU{r";
            using (SqlConnection connection = new SqlConnection(
                connectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection);
                command.Parameters.AddWithValue("@username", (String)HttpContext.Session.GetString("username"));
                command.Parameters.AddWithValue("@password", password);
                connection.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            return true;
                        }
                        else //incorrect login information
                        {
                            connection.Close();
                            return false;
                        }

                    }



                }
                catch (System.Data.SqlClient.SqlException)
                {
                    //incorrect login information
                    connection.Close();
                    return false;
                }
            }

        }
        public bool UpdatePassword(string newPassword)
        {
            String query = "UPDATE dbo.users SET password = @password WHERE username = @username;";
            String connectionString = "Data Source=192.168.0.212;Initial Catalog=testDB;User ID=sa;Password=7v!SkU{r";
            using (SqlConnection connection2 = new SqlConnection(
                connectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection2);
                command.Parameters.AddWithValue("@username", (String)HttpContext.Session.GetString("username"));
                command.Parameters.AddWithValue("@password", newPassword);
                connection2.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                            connection2.Close();
                            return true;
                        
                    }

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    return false;
                }

            }
        }
    }

}
