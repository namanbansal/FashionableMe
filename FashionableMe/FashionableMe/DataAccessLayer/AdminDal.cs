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
        public bool addProduct(AddApparel product)
        {
            bool status = false;
            HttpContext.Current.Session["status"] = "DefaultMessage";
                string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Apparel values (@name,@bname,@desc,@imagepath,@category, GETDATE(),0)",conn);
                
                cmd.Parameters.AddWithValue("name",product.apparel.ApparelName);
                cmd.Parameters.AddWithValue("bname",product.apparel.BrandName);
                cmd.Parameters.AddWithValue("desc",product.apparel.Description);
                cmd.Parameters.AddWithValue("imagepath", product.apparel.ApparelImage);
                cmd.Parameters.AddWithValue("category",product.apparel.ApparelCategory.Trim());
                int i = cmd.ExecuteNonQuery();
                
                SqlCommand cmd2 = new SqlCommand("INSERT into Quantity values ( IDENT_CURRENT('Apparel'), @size, @quantity, @discount, @cost ) ", conn);
                if (product.small > 0)
                {
                    cmd2.Parameters.AddWithValue("size", "S");
                    cmd2.Parameters.AddWithValue("quantity", product.small);
                    cmd2.Parameters.AddWithValue("discount", product.apparel.ApparelDiscount);
                    cmd2.Parameters.AddWithValue("cost", product.apparel.ApparelCost);
                
                int res = cmd2.ExecuteNonQuery();
                status = true;               
            }
                SqlCommand cmd3 = new SqlCommand("INSERT into Quantity values ( IDENT_CURRENT('Apparel'), @size, @quantity, @discount, @cost ) ", conn);

                if (product.medium > 0)
                {
                    cmd3.Parameters.AddWithValue("size", "M");
                    cmd3.Parameters.AddWithValue("quantity", product.medium);
                    cmd3.Parameters.AddWithValue("discount", product.apparel.ApparelDiscount);
                    cmd3.Parameters.AddWithValue("cost", product.apparel.ApparelCost);

                    int res = cmd3.ExecuteNonQuery();
                    status = true;
                }
                SqlCommand cmd4 = new SqlCommand("INSERT into Quantity values ( IDENT_CURRENT('Apparel'), @size, @quantity, @discount, @cost ) ", conn);

                if (product.large > 0)
                {
                    cmd4.Parameters.AddWithValue("size", "L");
                    cmd4.Parameters.AddWithValue("quantity", product.large);
                    cmd4.Parameters.AddWithValue("discount", product.apparel.ApparelDiscount);
                    cmd4.Parameters.AddWithValue("cost", product.apparel.ApparelCost);
                    int res = cmd4.ExecuteNonQuery();
                    status = true;
                }
            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
            }
            conn.Close();
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
                SqlCommand cmd = new SqlCommand("Select * from Apparel ap INNER JOIN Quantity qt ON ap.ApparelID=qt.ApparelID where ApparelCategory=@category order by ap.ApparelAddTime desc", conn);
                cmd.Parameters.AddWithValue("category", category.Trim());
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
                        prodObj.ApparelDiscount = reader.GetDecimal(reader.GetOrdinal("ApparelDiscount"));
                        dataRows.Add(prodObj);
                    }
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return dataRows;

        }

        public bool AddOffer(Offer OfferObj)
        {
            bool status = false;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            if (hasOfferWithSameDate( OfferObj.OfferDate ))
            {
                HttpContext.Current.Session["ErrorMessage"] = "Offer with Same Date exists";
                return status;
            }
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Offer VALUES (@name, @desc, @date, @appid, @discount) ", conn);
                cmd.Parameters.AddWithValue("name", OfferObj.OfferName.Trim());
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
            conn.Close();
            return status;
        }

        public Offer getOfferDetails(string date)
        {
            Offer obj = new Offer();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Offer where OfferDate=@offdate", conn);
                cmd.Parameters.AddWithValue("offdate",date);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Offer obj = new Offer();
                        obj.OfferName = reader.GetString(reader.GetOrdinal("OfferName"));
                        obj.ApparelID = reader.GetInt32(reader.GetOrdinal("OfferApparelID"));
                        obj.OfferDescription = reader.GetString(reader.GetOrdinal("OfferDescription"));
                        obj.Discount = reader.GetDecimal(reader.GetOrdinal("OfferDiscount"));
                        obj.OfferDate = reader.GetDateTime(reader.GetOrdinal("OfferDate")).Date;
                        //objlist.Add(obj);
                    }
                }
                conn.Close();
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return obj;
        }

        public bool hasOfferWithSameDate(DateTime date)
        {
            bool status = false;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) from Offer where OfferDate=@offdate", conn);
                cmd.Parameters.AddWithValue("offdate", date);
                var reader = Convert.ToInt32(cmd.ExecuteScalar());
                if (reader > 0)
                    status = true;
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = "Offer with same name exists";
            }
            conn.Close();
            return status;
        }
        
        public bool updateOfferByDate(string offerDate, string offerName, string offerDescription, string offerDiscount)
        {
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            bool status = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Offer SET OfferName=@offerName, OfferDescription=@offerDescription, OfferDiscount=@offerDiscount where OfferDate=@offerDate", conn);
                cmd.Parameters.AddWithValue("offerName", offerName.Trim());
                cmd.Parameters.AddWithValue("offerDescription", offerDescription);
                cmd.Parameters.AddWithValue("offerDiscount", offerDiscount);
                cmd.Parameters.AddWithValue("offerDate", offerDate);

                var reader = cmd.ExecuteReader();
                status = true;
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return status;
        }

        public bool deleteOfferByDate(string offerDate)
        {
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            bool status = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Offer WHERE OfferDate=@offerDate", conn);
                cmd.Parameters.AddWithValue("offerDate", offerDate);

                var reader = cmd.ExecuteReader();
                conn.Close();

            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
                conn.Close();
                
                return false;
            }
            return true;
        }

        public List<string> getBrandNames()
        {
            List<string> dataRows = new List<string>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select distinct(BrandName) from Apparel", conn);
                
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string brandName = reader.GetString(reader.GetOrdinal("BrandName"));
                        dataRows.Add(brandName);
                    }
                }

            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return dataRows;

        }

        public List<string> getApparelNameByBrand(string brand)
        {
            List<string> dataRows = new List<string>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select DISTINCT(ApparelName) from Apparel where BrandName=@brandName", conn);
                cmd.Parameters.AddWithValue("brandName", brand);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string apparelName = reader.GetString(reader.GetOrdinal("ApparelName"));
                        dataRows.Add(apparelName);
                    }
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return dataRows;

        }

        public List<AddApparel> getApparelsByBrandAndName(string name, string brand)
        {
            List<AddApparel> dataRows = new List<AddApparel>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select ap.ApparelID, ap.ApparelName, ap.BrandName, ap.Description, ap.ApparelCategory, qt.QuantityPerSize, qt.ApparelSize, qt.ApparelDiscount, qt.ApparelCost from Apparel ap INNER JOIN Quantity qt ON ap.ApparelID=qt.ApparelID where ap.BrandName=@brand AND ap.ApparelName=@name", conn);
                cmd.Parameters.AddWithValue("brand", brand.Trim());
                cmd.Parameters.AddWithValue("name", name.Trim());
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AddApparel prodObj = new AddApparel();
                        prodObj.apparel.ApparelName = reader.GetString(reader.GetOrdinal("ApparelName"));
                        prodObj.apparel.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
                        prodObj.apparel.BrandName = reader.GetString(reader.GetOrdinal("BrandName"));
                        prodObj.apparel.ApparelCost = reader.GetDecimal(reader.GetOrdinal("ApparelCost"));
                        prodObj.apparel.Description = reader.GetString(reader.GetOrdinal("Description"));
                        //prodObj.apparel.ApparelImage = reader.GetString(reader.GetOrdinal("ApparelImage"));
                        prodObj.apparel.ApparelCategory = reader.GetString(reader.GetOrdinal("ApparelCategory"));
                        //prodObj.apparel.ApparelRating = reader.GetInt32(reader.GetOrdinal("ApparelRating"));
                        prodObj.apparel.ApparelSize = reader.GetString(reader.GetOrdinal("ApparelSize"));
                        prodObj.apparel.ApparelQuantity = reader.GetInt32(reader.GetOrdinal("QuantityPerSize"));
                        prodObj.apparel.ApparelDiscount = reader.GetDecimal(reader.GetOrdinal("ApparelDiscount"));
                        //if (prodObj.apparel.ApparelSize == "S")
                        //{
                        //    prodObj.small = quant;
                        //    prodObj.costForSmall = prodObj.apparel.ApparelCost;
                        //}
                        //else if (prodObj.apparel.ApparelSize == "M")
                        //{
                        //    prodObj.medium = quant;
                        //    prodObj.costForMedium = prodObj.apparel.ApparelCost;

                        //}
                        //else if (prodObj.apparel.ApparelSize == "L")
                        //{
                        //    prodObj.large = quant;
                        //    prodObj.costForLarge = prodObj.apparel.ApparelCost;

                        //}
                        dataRows.Add(prodObj);
                    }
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return dataRows;

        }

        public bool UpdateApparel(int apparelID, decimal cost, decimal discount, int quantity, string category, string size)
        {
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            bool status = false;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Quantity SET QuantityPerSize=@quantity, ApparelDiscount=@discount, ApparelCost=@cost WHERE ApparelID=@apparelID AND ApparelSize=@size", conn);
                cmd.Parameters.AddWithValue("quantity", quantity);
                cmd.Parameters.AddWithValue("discount", discount);
                cmd.Parameters.AddWithValue("cost", cost);
                cmd.Parameters.AddWithValue("apparelID", apparelID);
                cmd.Parameters.AddWithValue("size", size);

                var reader = cmd.ExecuteReader();
                status = true;
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return status;
        }

    }
}