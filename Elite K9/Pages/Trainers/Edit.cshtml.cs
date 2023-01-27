using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Elite_K9.Pages.Trainers
{
    public class EditModel : PageModel
    {
        public TrainerInfo trainerInfo = new TrainerInfo();
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
                    String sql = "SELECT * FROM trainer WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                trainerInfo.id = "" + reader.GetInt32(0);
                                trainerInfo.firstName = reader.GetString(1);
                                trainerInfo.lastName = reader.GetString(2);
                                trainerInfo.startDate = reader.GetString(3);
                                trainerInfo.terminationDate = reader.GetString(4);
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
            trainerInfo.id = Request.Form["id"];
            trainerInfo.firstName = Request.Form["firstName"];
            trainerInfo.lastName = Request.Form["lastName"];
            trainerInfo.startDate = Request.Form["startDate"];
            trainerInfo.terminationDate = Request.Form["terminationDate"];

            if (trainerInfo.id.Length == 0 || trainerInfo.firstName.Length == 0 ||
                trainerInfo.lastName.Length == 0 || trainerInfo.startDate.Length == 0)
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
                    String sql = "UPDATE trainer " +
                        "SET firstName=@firstName, lastName=@lastName, " +
                        "startDate=@startDate, terminationDate=@terminationDate " +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", trainerInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", trainerInfo.lastName);
                        command.Parameters.AddWithValue("@startDate", trainerInfo.startDate);
                        command.Parameters.AddWithValue("@terminationDate", trainerInfo.terminationDate);
                        command.Parameters.AddWithValue("@id", trainerInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Trainers/Index");
        }
    }
}
