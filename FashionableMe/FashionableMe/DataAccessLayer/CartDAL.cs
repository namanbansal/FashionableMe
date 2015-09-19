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
    }
}