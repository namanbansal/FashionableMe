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
    public class CustomerDal
    {
        public bool AddCustomer(Customer cust)
        {
            bool status = false;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Customer values (@userid,@email,@pass,@name,@address,@city,@state,@pin,@mobile,@dob,@gender)",conn);
                cmd.Parameters.AddWithValue("userid",cust.UserID);
                cmd.Parameters.AddWithValue("email", cust.Email);
                cmd.Parameters.AddWithValue("pass",cust.Password );
                cmd.Parameters.AddWithValue("name",cust.Name );
                cmd.Parameters.AddWithValue("address", cust.Address);
                cmd.Parameters.AddWithValue("city", cust.City);
                cmd.Parameters.AddWithValue("state", cust.State);
                cmd.Parameters.AddWithValue("pin", cust.Pincode);
                cmd.Parameters.AddWithValue("mobile", cust.MobileNumber);
                cmd.Parameters.AddWithValue("dob", cust.DateOfBirth.ToString());
                cmd.Parameters.AddWithValue("gender", cust.IsMale?"M":"F");
                int res = cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
                status = false; 
            }
            conn.Close();
            return status;
        }

        public bool LoginCheck(LoginModel login)
        {
            bool status = false;
            HttpContext.Current.Session["UserRole"] = "visitor";
            //HttpContext.Current.Session["UserID"] = "000";
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select count(UserID) from Customer where UserID=@userid and PassWord=@pass ", conn);
                cmd.Parameters.AddWithValue("userid",login.UserID);
                cmd.Parameters.AddWithValue("pass",login.Password);
                int count = (Int32)cmd.ExecuteScalar();
                //return count.ToString();
                if (count==1)
                {   
                    HttpContext.Current.Session["UserID"] = login.UserID;
                    if(login.UserID.ToLower() == "adminfme")
                        HttpContext.Current.Session["UserRole"] = "admin";
                    else
                    HttpContext.Current.Session["UserRole"] = "customer";
                    status = true;
                }
                
            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
                status = false;
            }
            conn.Close();
            return status;
        }

        public DetailsViewModel getCustomerDetails(string userID)
        {
            DetailsViewModel custObj = null;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE  UserID=@user ",conn);
                cmd.Parameters.AddWithValue("user",userID);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {   
                    reader.Read();
                    custObj = new DetailsViewModel();
                    custObj.Name = reader.GetString(reader.GetOrdinal("Name"));
                    custObj.Address = reader.GetString(reader.GetOrdinal("Address"));
                    custObj.City = reader.GetString(reader.GetOrdinal("City"));
                    custObj.State = reader.GetString(reader.GetOrdinal("State"));
                    custObj.Pincode = reader.GetString(reader.GetOrdinal("Pincode"));
                    custObj.MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber"));
                    custObj.IsMale = (reader.GetString(reader.GetOrdinal("Gender"))=="M")?true:false;
                }
            }
            catch (Exception exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = exc.Message;
            }

            return custObj;
        }


        public bool CheckAndUpdateCustomer(DetailsViewModel model,out int statusCode)
        {
            bool status = false;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd;
            statusCode = 0;
            string userID = HttpContext.Current.Session["UserID"].ToString();
            try
            {
                conn.Open();
                if (model.OldPassword != "")
                {
                    cmd = new SqlCommand("UPDATE Customer SET Password=@newPass where UserID=@userid and Password=@pass ", conn);
                    cmd.Parameters.AddWithValue("newPass", model.Password);
                    cmd.Parameters.AddWithValue("userid", userID);
                    cmd.Parameters.AddWithValue("pass", model.OldPassword);
                    
                    int count = (Int32)cmd.ExecuteNonQuery() ;
                    if (count == 0)
                    {
                        HttpContext.Current.Session["ErrorMessage"] = "Old Password is NOT Correct ";
                        statusCode = 1;
                        status = false;
                    }
                    else
                    {
                        status = true;
                    }
                }
                else
                {
                    cmd = new SqlCommand("UPDATE Customer SET Name=@name , Address=@address , City=@city , State=@state , Pincode=@pincode , MobileNumber=@mobile , Gender=@gender where  UserID=@userID   ",conn);
                    cmd.Parameters.AddWithValue("name",model.Name);
                    cmd.Parameters.AddWithValue("address",model.Address);
                    cmd.Parameters.AddWithValue("city",model.City);
                    cmd.Parameters.AddWithValue("state",model.State);
                    cmd.Parameters.AddWithValue("pincode", model.Pincode);
                    cmd.Parameters.AddWithValue("mobile",model.MobileNumber);
                    cmd.Parameters.AddWithValue("gender",model.IsMale?"M":"F");
                    cmd.Parameters.AddWithValue("userID",userID);
                    int success = cmd.ExecuteNonQuery();
                    if (success > 0)
                    {
                        status = true;
                        statusCode = 0;
                    }
                }

            }
            catch (Exception Exc)
            {
                HttpContext.Current.Session["ErrorMessage"] = Exc.Message;
                statusCode = 2;
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
                SqlCommand cmd = new SqlCommand("Select * from Apparel where ApparelCategory=@category", conn);
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
                        prodObj.Description = reader.GetString(reader.GetOrdinal("Description"));
                        prodObj.ApparelImage = reader.GetString(reader.GetOrdinal("ApparelImage"));
                        prodObj.ApparelCategory = reader.GetString(reader.GetOrdinal("ApparelCategory"));
                        prodObj.ApparelRating = reader.GetInt32(reader.GetOrdinal("ApparelRating"));
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

        public List<Apparel> getApparelByID(int id)
        {
            List<Apparel> dataRows = new List<Apparel>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Apparel where ApparelID=@id", conn);
                cmd.Parameters.AddWithValue("id", id);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Apparel prodObj = new Apparel();
                        prodObj.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
                        prodObj.ApparelName = reader.GetString(reader.GetOrdinal("ApparelName"));
                        prodObj.BrandName = reader.GetString(reader.GetOrdinal("BrandName"));
                        prodObj.Description = reader.GetString(reader.GetOrdinal("Description"));
                        prodObj.ApparelImage = reader.GetString(reader.GetOrdinal("ApparelImage"));
                        prodObj.ApparelCategory = reader.GetString(reader.GetOrdinal("ApparelCategory"));
                        prodObj.ApparelRating = reader.GetInt32(reader.GetOrdinal("ApparelRating"));
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

        public List<Quantity> getQuantityDetails(int apparelID)
        {
            List<Quantity> dataRows = new List<Quantity>();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Quantity where ApparelID=@apparelID", conn);
                cmd.Parameters.AddWithValue("apparelID", apparelID);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Quantity prodObj = new Quantity();
                        prodObj.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
                        prodObj.ApparelCost = reader.GetDecimal(reader.GetOrdinal("ApparelCost"));
                        prodObj.ApparelSize = reader.GetString(reader.GetOrdinal("ApparelSize")).Trim();
                        prodObj.ApparelDiscount = reader.GetDecimal(reader.GetOrdinal("ApparelDiscount"));
                        prodObj.ApparelQuantity = reader.GetInt32(reader.GetOrdinal("QuantityPerSize"));
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

        public Quantity getQuantityDetailForApparel(int apparelID, string size)
        {
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            Quantity prodObj = new Quantity();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from Quantity where ApparelID=@apparelID AND ApparelSize=@size", conn);
                cmd.Parameters.AddWithValue("apparelID", apparelID);
                cmd.Parameters.AddWithValue("size", size);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prodObj.ApparelID = reader.GetInt32(reader.GetOrdinal("ApparelID"));
                        prodObj.ApparelCost = reader.GetDecimal(reader.GetOrdinal("ApparelCost"));
                        prodObj.ApparelDiscount = reader.GetDecimal(reader.GetOrdinal("ApparelDiscount"));
                        prodObj.ApparelQuantity = reader.GetInt32(reader.GetOrdinal("QuantityPerSize"));
                    }
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return prodObj;
        }

        public Offer getTodaysOffer(string date)
        {
            Offer obj = new Offer();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Offer where OfferDate=@offdate", conn);
                cmd.Parameters.AddWithValue("offdate", date);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Offer obj = new Offer();
                        obj.OfferID = reader.GetInt32(reader.GetOrdinal("OfferID")).ToString();
                        obj.OfferName = reader.GetString(reader.GetOrdinal("OfferName"));
                        obj.OfferDescription = reader.GetString(reader.GetOrdinal("OfferDescription"));
                        obj.ApparelID = reader.GetInt32(reader.GetOrdinal("OfferApparelID"));
                        obj.Discount = reader.GetDecimal(reader.GetOrdinal("OfferDiscount"));
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

        


        public Rating getRatingDetails(string ApparelID)
        {
            Rating rateObj = new Rating();
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            rateObj.ApparelID = ApparelID;
            
            rateObj.AddTime = DateTime.Now;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 10 Comment from Rating where ApparelID=@appId ORDER BY AddTime DESC ", conn);
                cmd.Parameters.AddWithValue("appId",ApparelID);
                var reader = cmd.ExecuteReader();
                rateObj.Comment = string.Empty;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rateObj.Comment += reader.GetString(reader.GetOrdinal("Comment"))+"\r\n*****\r\n";
                    }
                }
                reader.Close();
                cmd = new SqlCommand("SELECT AVG(cast(Rating as Float)) as TotalRating FROM Rating where ApparelID=@appID  ",conn);
                cmd.Parameters.AddWithValue("appID", ApparelID);
                var val = cmd.ExecuteScalar();
                if (val != System.DBNull.Value)
                {
                    rateObj.ApparelRating = Convert.ToInt32((double)val);
                }
                else
                {
                    rateObj.ApparelRating = 0;
                }

                if (HttpContext.Current.Session["UserID"] != null)
                {
		            rateObj.canRate = true;
                    rateObj.UserID = HttpContext.Current.Session["UserID"].ToString();
                }   
                else
                    rateObj.canRate = false;

                rateObj.userRating = 0;
                rateObj.userComment = string.Empty;

                if(rateObj.ApparelRating != 0 && rateObj.canRate)
                {
                    rateObj.UserID = HttpContext.Current.Session["UserID"].ToString();
                    cmd = new SqlCommand("SELECT Rating, Comment from Rating where ApparelID=@appid and UserID=@userID ", conn);
                    cmd.Parameters.AddWithValue("appid", rateObj.ApparelID);
                    cmd.Parameters.AddWithValue("userID", rateObj.UserID);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rateObj.userRating = reader.GetInt32(reader.GetOrdinal("Rating"));
                        rateObj.userComment = reader.GetString(reader.GetOrdinal("Comment"));
                    }
                    reader.Close();
                }
            }
            catch (Exception ExcObj)
            {
                HttpContext.Current.Session["ErrorMessage"] = ExcObj.Message;
            }
            conn.Close();
            return rateObj;
        }


        public bool InsertOrUpdateRating(string apparelID, string userID, int rating, string comment, int currRating)
        {
            bool status = false;
            string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                SqlCommand cmd ;
                if (currRating > 0)
                    cmd = new SqlCommand("UPDATE Rating set Rating=@rating, Comment=@comment, AddTime=GETDATE() WHERE ApparelID=@appid and UserID=@userid ", conn);
                else
                    cmd = new SqlCommand("INSERT INTO Rating VALUES(@appid, @userid, @rating, @comment, GETDATE()) ", conn);
                
                cmd.Parameters.AddWithValue("rating", rating);
                cmd.Parameters.AddWithValue("comment", comment);
                cmd.Parameters.AddWithValue("appid", apparelID);
                cmd.Parameters.AddWithValue("userid", userID);
                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
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