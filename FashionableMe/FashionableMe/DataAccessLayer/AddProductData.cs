using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;

namespace FashionableMe.DataAccessLayer
{
    public class AddProductData
    {
        public string addProduct()
        {
            try
            {   
                string conStr = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                SqlConnection conn = new SqlConnection(conStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ApparelName from Apparel", conn);
                string str = "Result:";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        str += reader.GetString(reader.GetOrdinal("ApparelName"));
                    }
                }
                
                conn.Close();
                return str;
            }
            catch (Exception exc)
            {
                return exc.Message;   
            }

        }
        
    }
}
