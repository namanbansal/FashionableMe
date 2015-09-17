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
            HttpContext.Current.Session["status"] = "DefaultMessage";
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Apparel values (@name,@bname,@cost,@desc,@imagepath,@category, GETDATE(),0)",conn);
                
                cmd.Parameters.AddWithValue("name",product.ApparelName);
                cmd.Parameters.AddWithValue("bname",product.BrandName);
                cmd.Parameters.AddWithValue("cost",product.ApparelCost);
                cmd.Parameters.AddWithValue("desc",product.Description);
                cmd.Parameters.AddWithValue("imagepath",product.ApparelImage);
                cmd.Parameters.AddWithValue("category",product.ApparelCategory);
                int i = cmd.ExecuteNonQuery();
                
                SqlCommand cmd2 = new SqlCommand("insert into Quantity values ( IDENT_CURRENT('Apparel'), @size, @quantity, @discount ) ", conn);
                
                cmd2.Parameters.AddWithValue("size", product.ApparelSize);
                cmd2.Parameters.AddWithValue("quantity", product.QuantityPerSize);
                cmd2.Parameters.AddWithValue("discount", product.ApparelDiscount);
                int res = cmd2.ExecuteNonQuery();
                status = true;               
            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
            }
            return status;
        }

        public List<Apparel> getProductByCategory(string category)
        {
            List<Apparel> dataRows = new List<Apparel>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Apparel ap INNER JOIN Quantity qt ON ap.ApparelID=qt.ApparelID where ApparelCategory=@category ", conn);
                cmd.Parameters.AddWithValue("category", category);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Apparel prodObj = new Apparel();
                        prodObj.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
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
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            return dataRows;

        }

        public bool AddOffer(Offer OfferObj)
        {
            bool status = false;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Offer VALUES (@name, @desc, @date, @appid, @discount) ", conn);
                cmd.Parameters.AddWithValue("name", OfferObj.OfferName);
                cmd.Parameters.AddWithValue("desc", OfferObj.OfferDescription);
                cmd.Parameters.AddWithValue("date", OfferObj.OfferDate);
                cmd.Parameters.AddWithValue("appid", OfferObj.ApparelID);
                cmd.Parameters.AddWithValue("discount", OfferObj.Discount);
                int i = (Int32)cmd.ExecuteNonQuery();
                if (i > 0)
                    status = true;

            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            return status;
        }

        public List<Offer> getOfferDetails(DateTime date)
        {
            List<Offer> objList = new List<Offer>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Offer where OfferDate=@offerdate ", conn);
                cmd.Parameters.AddWithValue("offerdate",date.Date.ToString("yyyyMMdd"));
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Offer obj = new Offer();
                        obj.OfferName = reader.GetString(reader.GetOrdinal("OfferName"));
                        obj.ApparelID = reader.GetInt32(reader.GetOrdinal("OfferApparelID"));
                        obj.OfferDescription = reader.GetString(reader.GetOrdinal("OfferDescription"));
                        obj.Discount = reader.GetDecimal(reader.GetOrdinal("OfferDiscount"));
                        obj.OfferDate = reader.GetDateTime(reader.GetOrdinal("OfferDate")).Date;
                        objList.Add(obj);
                    }
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            return objList;
        }

        public bool UpdateOfferDetails(Offer offerObj)
        {
            bool status = true;

            return status;
        }

    }
}