using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace FashionableMe.DataAccessLayer
{
    public class AddProductData
    {
        public string addProduct()
        {
            try
            {
                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(@"Data Source=(LocalDB)\v11.0;
AttachDbFilename=|DataDirectory|\FashionableMeDB.mdf;Integrated Security=True;Database=FashionableMeDB");
                SqlConnection conn = new SqlConnection(scsb.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"insert into Apparel(ApparelID,ApparelName,BrandName,ApparelCost,Description
ApparelImage,ApparelCategory,ApparelAddTime,ApparelDiscount,Rating,Comments) values ", conn);
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
