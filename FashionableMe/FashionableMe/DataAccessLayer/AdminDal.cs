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
    public class AdminDal
    {
        public bool addProduct(Apparel product)
        {
            bool status = false;

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Apparel values (@appid,@name,@bname,@cost,@desc,@imagepath,@category, GETDATE(),0)",conn);
                cmd.Parameters.AddWithValue("appid",product.ApparelID);
                cmd.Parameters.AddWithValue("name",product.ApparelName);
                cmd.Parameters.AddWithValue("bname",product.BrandName);
                cmd.Parameters.AddWithValue("cost",product.ApparelCost);
                cmd.Parameters.AddWithValue("desc",product.Description);
                cmd.Parameters.AddWithValue("imagepath",product.ApparelImage);
                cmd.Parameters.AddWithValue("category",product.ApparelCategory);
                int i = cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("insert into Quantity values (@appid,@size,@quantity,@discount",conn);
                cmd2.Parameters.AddWithValue("appid",product.ApparelID);
                cmd2.Parameters.AddWithValue("appid",product.ApparelID);
                cmd2.Parameters.AddWithValue("appid",product.ApparelID);
                cmd2.Parameters.AddWithValue("appid",product.ApparelID);
                int res = cmd2.ExecuteNonQuery();
                status = true;               
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public List<Apparel> getProductByCategory(string category)
        {
            List<Apparel> dataRows = new List<Apparel>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from Apparel ap INNER JOIN Quantity qt ON ap.ApparelID = qt.ApparelID where ApparelCategory='Male' ",conn);
            //cmd.Parameters.AddWithValue("category",category);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Apparel prodObj = new Apparel();
                    //prodObj.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
                    prodObj.ApparelName = reader.GetString(reader.GetOrdinal("ApparelName"));
                    prodObj.BrandName = reader.GetString(reader.GetOrdinal("BrandName"));
                    prodObj.ApparelCost = reader.GetDecimal(reader.GetOrdinal("ApparelCost"));
                    prodObj.Description = reader.GetString(reader.GetOrdinal("Description"));
                    prodObj.ApparelImage = reader.GetString(reader.GetOrdinal("ApparelImage"));
                    prodObj.ApparelCategory = reader.GetString(reader.GetOrdinal("ApparelCategory"));
                    prodObj.ApparelRating = reader.GetInt32(reader.GetOrdinal("ApparelRating"));
                    prodObj.ApparelSize = reader.GetString(reader.GetOrdinal("ApparelSize"));
                    prodObj.QuantityPerSize = reader.GetInt32(reader.GetOrdinal("QuantityPerSize"));
                    prodObj.ApparelDiscount = reader.GetDecimal(reader.GetOrdinal("ApparelDiscount"));
                    dataRows.Add(prodObj); 
                }
            }
            return dataRows;

        }
    }
}