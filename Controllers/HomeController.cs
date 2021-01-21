using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using twoModelsInOneView.Models;

public class HomeController : Controller
{
    // GET: Home
    public IActionResult Index()
    {
        dynamic model = new ExpandoObject();
        model.Customer = GetCustomer();
        model.Product = GetProducts();
        return View(model);
    }

   

    [HttpPost]
    private static List<Customer> GetCustomer()
    {
        Customer cust = new Customer();
        List<Customer> customer = new List<Customer>();
        string connection = "Data Source=localhost;Initial Catalog=Survey;Integrated Security=True";
        using (SqlConnection sqlconn = new SqlConnection(connection))
        {
            string query = "insert into Customer(CustName,EmailAdd) values ('" + cust.CustName + "','" + cust.EmailAdd + "')";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                   /* using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customer.Add(new Customer
                            {
                                CustId = Convert.ToInt32(sdr["CustId"]),
                                CustName = sdr["CustName"].ToString(),
                                EmailAdd = sdr["EmailAdd"].ToString(),*//*
                                DteCreated = DateTime.Parse((string)sdr["DteCreated"]),*//*
                            });
                        }
                    }*/
                    con.Close();
                    return customer;
                }
            }
        }
          
    }

    private static List<Product> GetProducts()
    {
        List<Product> product = new List<Product>();
        
        string connection = "Data Source=localhost;Initial Catalog=Survey;Integrated Security=True";
        using (SqlConnection sqlconn = new SqlConnection(connection))
        {
            string query = "SELECT TOP (1000) [ProdId],[ProdTyp],[Price] FROM [Survey].[dbo].[Product]";
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            product.Add(new Product
                            {
                                ProdId = Convert.ToInt32(sdr["ProdId"]),
                                ProdTyp = sdr["ProdTyp"].ToString(),
                                price = sdr["price"].ToString()
                            });
                        }
                        con.Close();
                        return product;
                    }
                }
            }
        }
       
    }

 /*   public IActionResult GetDetails(Product prod)
    {
        string connection = "Data Source=localhost;Initial Catalog=Survey;Integrated Security=True";
        using (SqlConnection sqlconn = new SqlConnection(connection))
        {
            string sqlquery = "insert into Product(ProdTyp) values ('" + prod.ProdTyp + "')";
            using (SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn))
            {
                sqlconn.Open();
                sqlcomm.ExecuteNonQuery();

            }


        }
        return View("Create");
    }*/
}