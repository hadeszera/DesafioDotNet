using DesafioDotNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioDotNet.DAL
{
    public class ProductDal : IProductDal
    {
        string connectionString = @"Data Source=Localhost;Initial Catalog=DesafioDotNet;Integrated Security=True;";

        public void AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL =
                    "Insert into Product (Name,Brand,Price,CreatedAt) values(@Name,@Brand,@Price,@CreatedAt)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Brand", product.Brand);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteProduct(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from Product where Id = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> listProducts = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listProducts.Add(new Product {
                    Id = int.Parse(reader["ID"].ToString()),
                    Name = reader["Name"].ToString(),
                    Brand = reader["Brand"].ToString(),
                    Price = Decimal.Parse(reader["Price"].ToString()),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"].ToString()),
                    UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"])  : null,});
                }
                con.Close();
            }
            return listProducts;
        }

        public Product GetProduct(int? id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    product = new Product
                    {
                        Id = int.Parse(reader["ID"].ToString()),
                        Name = reader["Name"].ToString(),
                        Brand = reader["Brand"].ToString(),
                        Price = Decimal.Parse(reader["Price"].ToString()),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"].ToString()),
                        UpdatedAt = reader["UpdatedAt"].ToString() != null ? Convert.ToDateTime(reader["UpdatedAt"].ToString()) : null,
                    };
                }
                con.Close();
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = 
                    "Update Product set" +
                    "Name = @Nome, " +
                    "Price = @Price, " +
                    "Brand = @Brand" +
                    "UpdatedAt = @UpdatedAt, " +
                    " where Id = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Brand", product.Brand);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



    }
}
