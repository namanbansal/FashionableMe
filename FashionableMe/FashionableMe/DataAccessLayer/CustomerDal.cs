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
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
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
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public string LoginCheck(LoginModel login)
        {
            string ret = string.Empty;
            HttpContext.Current.Session["userRole"] = "visitor";
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select count(UserID) from Customer where UserID=@userid and password=@pass",conn);
                cmd.Parameters.AddWithValue("userid",login.UserID);
                cmd.Parameters.AddWithValue("pass",login.Password);
                int count = (Int32)cmd.ExecuteScalar();
                if (count==1)
                {   
                    HttpContext.Current.Session["SessionUser"] = login.UserID.Trim();
                    if(login.UserID.ToLower() == "admin")
                        HttpContext.Current.Session["userRole"] = "admin";
                    else
                    HttpContext.Current.Session["userRole"] = "customer";
                    ret = "success";
                }
            }
            catch (Exception)
            {
                ret = "ERROR";
            }
            return ret;
        }

    }
}