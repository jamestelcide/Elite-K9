using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Elite_K9.Pages.Training
{
    public class EditModel : PageModel
    {
        public TrainingInfo trainingInfo = new TrainingInfo();
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
                    String sql = "SELECT * FROM trainings WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                trainingInfo.id = "" + reader.GetInt32(0);
                                trainingInfo.training = reader.GetString(1);
                                trainingInfo.price = "" + reader.GetDecimal(2);
                                trainingInfo.commission = "" + reader.GetDecimal(3);
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
            trainingInfo.id = Request.Form["id"];
            trainingInfo.training = Request.Form["training"];
            trainingInfo.price = Request.Form["price"];
            trainingInfo.commission = Request.Form["commission"];

            if (trainingInfo.id.Length == 0 || trainingInfo.training.Length == 0 ||
                trainingInfo.price.Length == 0 || trainingInfo.commission.Length == 0)
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
                    String sql = "UPDATE trainings " +
                        "SET title=@training, price=@price, " +
                        "commission=@commission " +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@training", trainingInfo.training);
                        command.Parameters.AddWithValue("@price", trainingInfo.price);
                        command.Parameters.AddWithValue("@commission", trainingInfo.commission);
                        command.Parameters.AddWithValue("@id", trainingInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Training/Index");
        }
    }
}
