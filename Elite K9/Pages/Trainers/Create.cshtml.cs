using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Trainers
{
    public class CreateModel : PageModel
    {
        public TrainerInfo trainerInfo = new TrainerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {

            trainerInfo.firstName = Request.Form["firstName"];
            trainerInfo.lastName = Request.Form["lastName"];
            trainerInfo.startDate = Request.Form["startDate"];
            trainerInfo.terminationDate = Request.Form["terminationDate"];

            if (trainerInfo.firstName.Length == 0 ||
                trainerInfo.lastName.Length == 0 || trainerInfo.startDate.Length == 0)
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
                    String sql = "INSERT INTO trainer " +
                        "(firstName, lastName, startDate, terminationDate) VALUES " +
                        "(@firstName, @lastName, @startDate, @terminationDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", trainerInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", trainerInfo.lastName);
                        command.Parameters.AddWithValue("@startDate", trainerInfo.startDate);
                        command.Parameters.AddWithValue("@terminationDate", trainerInfo.terminationDate);
                        command.ExecuteNonQuery();
                    }
                }

                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            trainerInfo.firstName = ""; trainerInfo.lastName = "";
            trainerInfo.startDate = ""; trainerInfo.terminationDate = "";
            successMessage = "New Trainer Added Successfully!";

            Response.Redirect("/Trainers/Index");
        }
    }
}
