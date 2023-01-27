using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Elite_K9.Pages.Sales
{
    public class CreateModel : PageModel
    {
        public SalesInfo salesInfo = new SalesInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            
            salesInfo.training = Request.Form["training"];
            salesInfo.customer = Request.Form["customer"];
            salesInfo.dogName = Request.Form["dogName"];
            salesInfo.trainer = Request.Form["trainer"];
            salesInfo.salePrice = Request.Form["salePrice"];
            salesInfo.saleDate = Request.Form["saleDate"];

            if (salesInfo.training.Length == 0 ||
                salesInfo.customer.Length == 0 || salesInfo.dogName.Length == 0 ||
                salesInfo.trainer.Length == 0 || salesInfo.salePrice.Length == 0 ||
                salesInfo.saleDate.Length == 0)
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
                    String sql = "INSERT INTO sales " +
                        "(training, customer, dogName, trainer, salePrice, saleDate) VALUES " +
                        "(@training, @customer, @dogName, @trainer, @salePrice, @saleDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@training", salesInfo.training);
                        command.Parameters.AddWithValue("@customer", salesInfo.customer);
                        command.Parameters.AddWithValue("@dogName", salesInfo.dogName);
                        command.Parameters.AddWithValue("@trainer", salesInfo.trainer);
                        command.Parameters.AddWithValue("@salePrice", salesInfo.salePrice);
                        command.Parameters.AddWithValue("@saleDate", salesInfo.saleDate);

                        command.ExecuteNonQuery();
                    }
                }

                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            salesInfo.training = ""; salesInfo.customer = "";
            salesInfo.dogName = ""; salesInfo.trainer = "";
            salesInfo.salePrice = ""; salesInfo.saleDate = "";
            successMessage = "New Sale Added Successfully!";

            Response.Redirect("/Sales/Index");
        }
    }
}
