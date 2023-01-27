using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Training
{
    public class CreateModel : PageModel
    {
        public TrainingInfo trainingInfo = new TrainingInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {

            trainingInfo.training = Request.Form["training"];
            trainingInfo.price = Request.Form["price"];
            trainingInfo.commission = Request.Form["commission"];

            if (trainingInfo.training.Length == 0 ||
                trainingInfo.price.Length == 0 || trainingInfo.commission.Length == 0)
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
                    String sql = "INSERT INTO trainings " +
                        "(title, price, commission) VALUES " +
                        "(@training, @price, @commission);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@training", trainingInfo.training);
                        command.Parameters.AddWithValue("@price", trainingInfo.price);
                        command.Parameters.AddWithValue("@commission", trainingInfo.commission);

                        command.ExecuteNonQuery();
                    }
                }

                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            trainingInfo.training = ""; trainingInfo.price = "";
            trainingInfo.commission = "";
            successMessage = "New Training Added Successfully!";

            Response.Redirect("/Training/Index");
        }
    }
}
