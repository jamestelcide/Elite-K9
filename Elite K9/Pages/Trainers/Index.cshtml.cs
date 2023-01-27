using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        public List<TrainerInfo> listTrainers = 
            new List<TrainerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString =
                    "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM trainer";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrainerInfo trainerInfo = new TrainerInfo();
                                trainerInfo.id = "" + reader.GetInt32(0);
                                trainerInfo.firstName = reader.GetString(1);
                                trainerInfo.lastName = reader.GetString(2);
                                trainerInfo.startDate = reader.GetString(3);
                                trainerInfo.terminationDate = reader.GetString(4);

                                listTrainers.Add(trainerInfo);
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

    public class TrainerInfo
    {
        public String id;
        public String firstName;
        public String lastName;
        public String startDate;
        public String terminationDate;
    }
}
