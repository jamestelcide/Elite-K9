using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {

            customerInfo.firstName = Request.Form["firstName"];
            customerInfo.lastName = Request.Form["lastName"];
            customerInfo.dogBreed = Request.Form["dogBreed"];
            customerInfo.dogName = Request.Form["dogName"];

            if (customerInfo.firstName.Length == 0 ||
                customerInfo.lastName.Length == 0 || customerInfo.dogBreed.Length == 0 ||
                customerInfo.dogName.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = 
                    "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO customer " +
                        "(firstName, lastName, dogBreed, dogName) VALUES " +
                        "(@firstName, @lastName, @dogBreed, @dogName);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", customerInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", customerInfo.lastName);
                        command.Parameters.AddWithValue("@dogBreed", customerInfo.dogBreed);
                        command.Parameters.AddWithValue("@dogName", customerInfo.dogName);
                        command.ExecuteNonQuery();
                    }
                }

                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            customerInfo.firstName = ""; customerInfo.lastName = "";
            customerInfo.dogBreed = ""; customerInfo.dogName = "";
            successMessage = "New Customer Added Successfully!";

            Response.Redirect("/Customers/Index");
        }
    }
}
