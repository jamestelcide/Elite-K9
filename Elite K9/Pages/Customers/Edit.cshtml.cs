using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Elite_K9.Pages.Customers
{
    public class EditModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString =
                    "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customer WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.firstName = reader.GetString(1);
                                customerInfo.lastName = reader.GetString(2);
                                customerInfo.dogBreed = reader.GetString(3);
                                customerInfo.dogName = reader.GetString(4);
                            }
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            customerInfo.id = Request.Form["id"];
            customerInfo.firstName = Request.Form["firstName"];
            customerInfo.lastName = Request.Form["lastName"];
            customerInfo.dogBreed = Request.Form["dogBreed"];
            customerInfo.dogName = Request.Form["dogName"];

            if (customerInfo.id.Length == 0 || customerInfo.firstName.Length == 0 ||
                customerInfo.lastName.Length == 0 || customerInfo.dogBreed.Length == 0 ||
                customerInfo.dogName.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE customer " +
                        "SET firstName=@firstName, lastName=@lastName, " +
                        "dogBreed=@dogBreed, dogName=@dogName " +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", customerInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", customerInfo.lastName);
                        command.Parameters.AddWithValue("@dogBreed", customerInfo.dogBreed);
                        command.Parameters.AddWithValue("@dogName", customerInfo.dogName);
                        command.Parameters.AddWithValue("@id", customerInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Customers/Index");
        }
    }
}
