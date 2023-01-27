using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Elite_K9.Pages.Sales
{
    public class EditModel : PageModel
    {
        public SalesInfo salesInfo = new SalesInfo();
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
                    String sql = "SELECT * FROM sales WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                salesInfo.id = "" + reader.GetInt32(0);
                                salesInfo.training = reader.GetString(1);
                                salesInfo.customer = reader.GetString(2);
                                salesInfo.dogName = reader.GetString(3);
                                salesInfo.trainer = reader.GetString(4);
                                salesInfo.salePrice = "" + reader.GetDecimal(5);
                                salesInfo.saleDate = reader.GetString(6);
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
            salesInfo.id = Request.Form["id"];
            salesInfo.training = Request.Form["training"];
            salesInfo.customer = Request.Form["customer"];
            salesInfo.dogName = Request.Form["dogName"];
            salesInfo.trainer = Request.Form["trainer"];
            salesInfo.salePrice = Request.Form["salePrice"];
            salesInfo.saleDate = Request.Form["saleDate"];

            if (salesInfo.id.Length == 0 || salesInfo.training.Length == 0 ||
                salesInfo.customer.Length == 0 || salesInfo.dogName.Length == 0 ||
                salesInfo.trainer.Length == 0 || salesInfo.salePrice.Length == 0 || 
                salesInfo.saleDate.Length == 0)
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
                    String sql = "UPDATE sales " +
                        "SET training=@training, customer=@customer, " +
                        "dogName=@dogName, trainer=@trainer, salePrice=@salePrice, saleDate=@saleDate " +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@training", salesInfo.training);
                        command.Parameters.AddWithValue("@customer", salesInfo.customer);
                        command.Parameters.AddWithValue("@dogName", salesInfo.dogName);
                        command.Parameters.AddWithValue("@trainer", salesInfo.trainer);
                        command.Parameters.AddWithValue("@salePrice", salesInfo.salePrice);
                        command.Parameters.AddWithValue("@saleDate", salesInfo.saleDate);
                        command.Parameters.AddWithValue("@id", salesInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Sales/Index");
        }
    }
}
