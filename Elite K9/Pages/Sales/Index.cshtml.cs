using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Sales
{
    public class IndexModel : PageModel
    {
        public List<SalesInfo> listSales = 
            new List<SalesInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString =
                    "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM sales";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SalesInfo salesInfo = new SalesInfo();
                                salesInfo.id = "" + reader.GetInt32(0);
                                salesInfo.training = reader.GetString(1);
                                salesInfo.customer = reader.GetString(2);
                                salesInfo.dogName = reader.GetString(3);
                                salesInfo.trainer = reader.GetString(4);
                                salesInfo.salePrice = "" + reader.GetDecimal(5);
                                salesInfo.saleDate = reader.GetString(6);

                                listSales.Add(salesInfo);
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

    public class SalesInfo
    {
        public String id;
        public String training;
        public String customer;
        public String dogName;
        public String trainer;
        public String salePrice;
        public String saleDate;
    }
}
