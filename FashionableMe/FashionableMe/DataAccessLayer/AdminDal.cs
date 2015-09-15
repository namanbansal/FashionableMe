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
        public bool addProduct()
        {
            bool status = false;

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["FashionableMeDB"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Apparel values (@userid,@email,@pass,@name,@address,@city,@state,@pin,@mobile,@dob,@gender)",conn);
                
                int res = cmd.ExecuteNonQuery();
                status = true;               
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        }
    }
}