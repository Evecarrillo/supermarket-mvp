﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarker.mvp.Models;

namespace Supermarker.mvp._Repositories
{
    internal class ProductRepository : BaseRepository, IProductRepository
    {

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Add(ProductModel productModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Prodcuts VALUES (@name, @price, @stock)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productModel.Name;
                command.Parameters.Add("@price", SqlDbType.Int).Value = productModel.Price;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = productModel.Stock;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Prodcuts WHERE Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ProductModel productModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Prodcuts SET Product_Name = @name,
                                        Product_Price = @price,
                                        Product_Stock = @stock,
                                       
                                        WHERE Product_Id = @Id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productModel.Name;
                command.Parameters.Add("@price", SqlDbType.Int).Value = productModel.Price;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = productModel.Stock;

                command.Parameters.Add("@id", SqlDbType.Int).Value = productModel.Id;
                command.ExecuteNonQuery();

            }
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var productList = new List<ProductModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Prodcuts ORDER BY Product_Id DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.Id = (int)reader["Product_Id"];
                        productModel.Name = reader["Product_Name"].ToString();
                        productModel.Price = (string)reader["Product_Price"];
                        productModel.Stock = (string)reader["Product_Stock"];

                        productList.Add(productModel);
                    }
                }
            }
            return productList;
        }






        public IEnumerable<ProductModel> GetByValue(string value)
        {
            var productList = new List<ProductModel>();
            int productId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string productName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Prodcuts WHERE Product_Id=@id or Product_Name LIKE @name+ '%'
                                      ORDER by Product_Id DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.Id = (int)reader["Product_Id"];
                        productModel.Name = reader["Product_Name"].ToString();
                        productModel.Price = (string)reader["Product_Price"];
                        productModel.Stock = (string)reader["Product_Stock"];

                        productList.Add(productModel);
                    }
                }
            }

            return productList;
        }


    }
}
