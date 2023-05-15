using ADOProductManagement;
using Spectre.Console;
using System.Data;
using System.Data.SqlClient;

namespace ADOProductManagement
{
    class ProductManagement
    {          
        public void AddProduct(SqlConnection con)

        {          
            SqlDataAdapter adp = new SqlDataAdapter("Select * from ProductDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();

            Console.WriteLine("Enter Product name:");
            string productname = Console.ReadLine();
            Console.WriteLine("Enter description:");
            string proddescription = Console.ReadLine();
            Console.WriteLine("Enter Quantity:");
            int quantity=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter price:");
            int price=Convert.ToInt32(Console.ReadLine());
            
            row["ProductName"] = productname;
            row["ProdDescription"] = proddescription;
            row["Quantity"] = quantity;
            row["Price"]=price;
            
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Product Added");

        }
        public void ViewProductById(SqlConnection con) 
        {
            try {
                Console.WriteLine("Enter the id");
                int id = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter adp = new SqlDataAdapter($"Select * from ProductDetails where ProductId={id}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "ProdManage");
                for (int i = 0; i < ds.Tables["ProdManage"].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables["ProdManage"].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables["ProdManage"].Rows[i][j]} || ");
                    }
                    Console.WriteLine();
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Enter numbers only");
            }
        }
        public void ViewAllProducts(SqlConnection con) 
        {

            SqlDataAdapter adp = new SqlDataAdapter("Select * from ProductDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "ProdManage");
            for (int i = 0; i < ds.Tables["ProdManage"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["ProdManage"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["ProdManage"].Rows[i][j]} || ");
                }
                Console.WriteLine();
            }

        }
        public void UpdateProduct(SqlConnection con)
        {
            try
            {
                Console.WriteLine("Enter id");
                int id = Convert.ToInt32(Console.ReadLine());
                SqlDataAdapter adp = new SqlDataAdapter($"Select * from ProductDetails where ProductId={id}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                Console.WriteLine("Enter Product name to update:");
                string productname = Console.ReadLine();
                Console.WriteLine("Enter description to update:");
                string proddescription = Console.ReadLine();
                Console.WriteLine("Enter Quantity:");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter price:");
                int price = Convert.ToInt32(Console.ReadLine());

                var row = ds.Tables[0].Rows[0];
                row["ProductName"] = productname;
                row["ProdDescription"] = proddescription;
                row["Quantity"] = quantity;
                row["Price"] = price;

                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                Console.WriteLine(" ProductManagement Database Updated");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteProduct(SqlConnection con)
        {
            
            SqlDataAdapter adp = new SqlDataAdapter("Select * from ProductDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            Console.WriteLine("Enter product id to delete:");
            int id = Convert.ToInt32(Console.ReadLine());

            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Product Deleted");
        }
    }
}
internal class Program
{
    static void Main(string[] args)
    {   

        ProductManagement manage = new ProductManagement();
        do
        {
            SqlConnection con = new SqlConnection("Server=IN-F0979S3; database=ProductManagement; Integrated Security=true");
            AnsiConsole.Write(new FigletText("Prod Manage App").Centered().Color(Color.Violet));
            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
               .Title("[Green] Select your option [/]")
               .AddChoices(new[] {
                        "Add Product", "View Product By Id", "View All Products",
                        "Update Product","Delete Product"
                        }));
            Console.WriteLine($" You selected {choice}");

            switch (choice)
            {
                case "Add Product":
                    {
                        manage.AddProduct(con);
                        break;
                    }
                case "View Product By Id":
                    {
                        manage.ViewProductById(con);
                        break;
                    }
                case "View All Products":
                    {
                        manage.ViewAllProducts(con);
                        break;
                    }
                case "Update Product":
                    {
                        manage.UpdateProduct(con);
                        break;
                    }
                case "Delete Product":
                    {
                        manage.DeleteProduct(con);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Enter a valid option");
                        break;
                    }
            }
        } while (true);

    }
}