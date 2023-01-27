using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Training
{
    public class IndexModel : PageModel
    {
        public List<TrainingInfo> listTrainings = 
            new List<TrainingInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString =
                    "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM trainings";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrainingInfo trainingInfo = new TrainingInfo();
                                trainingInfo.id = "" + reader.GetInt32(0);
                                trainingInfo.training = reader.GetString(1);
                                trainingInfo.price = "" + reader.GetDecimal(2);
                                trainingInfo.commission = "" + reader.GetDecimal(3);

                                listTrainings.Add(trainingInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class TrainingInfo
    {
        public String id;
        public String training;
        public String price;
        public String commission;
    }
}
