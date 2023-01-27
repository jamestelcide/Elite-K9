using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> listCustomers = 
            new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString =
                    "Data Source=.\\sqlexpress;Initial Catalog=K9ELITE;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customer";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.firstName = reader.GetString(1);
                                customerInfo.lastName = reader.GetString(2);
                                customerInfo.dogBreed = reader.GetString(3);
                                customerInfo.dogName = reader.GetString(4);

                                listCustomers.Add(customerInfo);
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

    public class CustomerInfo
    {
        public String id;
        public String firstName;
        public String lastName;
        public String dogBreed;
        public String dogName;
    }
}
