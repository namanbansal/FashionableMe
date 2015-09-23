using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace FashionableMe.DataAccessLayer
{
    public class CartDAL
    {
        public Apparel getApparelForCart(string apparelID, string size)
        {
            Apparel data = new Apparel();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select ap.ApparelID, ap.ApparelName, qt.ApparelSize, qt.ApparelDiscount, qt.ApparelCost from Apparel ap INNER JOIN Quantity qt ON ap.ApparelID=qt.ApparelID where ap.ApparelID=@apparelID AND qt.ApparelSize=@size", conn);
                cmd.Parameters.AddWithValue("apparelID", apparelID.Trim());
                cmd.Parameters.AddWithValue("size", size.Trim());
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.ApparelName = reader.GetString(reader.GetOrdinal("ApparelName"));
                        data.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
                        data.ApparelCost = reader.GetDecimal(reader.GetOrdinal("ApparelCost"));
                        data.ApparelSize = reader.GetString(reader.GetOrdinal("ApparelSize")).Trim();
                        data.ApparelDiscount = reader.GetDecimal(reader.GetOrdinal("ApparelDiscount"));
                    }
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return data;

        }

        public string InsertOrderDetails(List<MyOrder> orders)
        {
            string status = "false";
            HttpContext.Current.Session["status"] = "DefaultMessage";
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                
                MyOrder order = new MyOrder();
                DateTime dateOfPurchase = DateTime.Now;
                Random generator = new Random();
                String transactionID = generator.Next(0, 1000000).ToString("D6");
                for (int i = 0; i < orders.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO CustomerOrders values (@TransactionID, @CustomerID, @ProductName, @SizeOfApparel, @Quantity, @TotalAmount, @ShippingAddress, @City, @State, @Pincode, @DateOfPurchase )", conn);
                    order = orders[i];
                    cmd.Parameters.AddWithValue("TransactionID", transactionID);
                    cmd.Parameters.AddWithValue("CustomerID", order.UserID);
                    cmd.Parameters.AddWithValue("ProductName", order.ProductName);
                    cmd.Parameters.AddWithValue("SizeOfApparel", order.SizeOfApparel);
                    cmd.Parameters.AddWithValue("Quantity", order.Quantity);
                    cmd.Parameters.AddWithValue("TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("ShippingAddress", order.ShippingAddress);
                    cmd.Parameters.AddWithValue("City", order.City);
                    cmd.Parameters.AddWithValue("State", order.State);
                    cmd.Parameters.AddWithValue("Pincode", order.Pincode);
                    cmd.Parameters.AddWithValue("DateOfPurchase", dateOfPurchase);

                    int rslt = cmd.ExecuteNonQuery();
                    status = transactionID;
                }

            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
            }
            conn.Close();
            return status;
        }

        public List<MyOrder> GetOrderDetails(string UserID)
        {
            List<MyOrder> orders = new List<MyOrder>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CustomerOrders WHERE CustomerID=@UserID", conn);
                cmd.Parameters.AddWithValue("UserID", UserID);
        
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        MyOrder order = new MyOrder();
                        
                        order.TransactionID = reader.GetString(reader.GetOrdinal("TransactionID"));
                        order.UserID = reader.GetString(reader.GetOrdinal("CustomerID")).ToString();
                        order.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                        order.SizeOfApparel = reader.GetString(reader.GetOrdinal("SizeOfApparel"));
                        order.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        order.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
                        order.ShippingAddress = reader.GetString(reader.GetOrdinal("ShippingAddress"));
                        order.City = reader.GetString(reader.GetOrdinal("City"));
                        order.State = reader.GetString(reader.GetOrdinal("State"));
                        order.Pincode = reader.GetInt64(reader.GetOrdinal("Pincode")).ToString() ;
                        order.DateOfPurchase = reader.GetDateTime(reader.GetOrdinal("DateOfPurchase"));

                        orders.Add(order);
                    }
                }
            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
            }
            conn.Close();
            return orders;
                
        }

    }
}