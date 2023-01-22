using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using OnlineTutorAPI.Models;
namespace OnlineTutorAPI.Models
{
    public class TutorAccess
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);


        public bool Insert(tutor obj)
        {
            con.Open();
            string check = "select * from tutor where email='" + obj.email + "'";
            SqlCommand chk = new SqlCommand(check, con);
            SqlDataReader dataCheck = chk.ExecuteReader();
            if (!dataCheck.HasRows)
            {
                dataCheck.Close();
                string q = "insert into tutor values('" + obj.fname + "','" + obj.lname + "','" + obj.gender + "','" + obj.phone_no + "','" + obj.city + "','" + obj.Class + "','" + obj.email + "','" + obj.password + "','" + obj.Bio + "',@img)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = obj.img;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public List<tutor> select(string q)
        {
            List<tutor> lst = new List<tutor>();
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr=cmd.ExecuteReader();
            while(sdr.Read())
            {
                tutor obj = new tutor();
                obj.id = int.Parse(sdr["tid"].ToString());
                obj.fname = sdr["fname"].ToString();
                obj.lname = sdr["lname"].ToString();
                obj.phone_no = sdr["phone_no"].ToString();
                obj.email = sdr["email"].ToString();

                obj.city = sdr["city"].ToString();
                obj.Bio = sdr["bio"].ToString();
                lst.Add(obj);
            }
            con.Close();

            return lst;
        }

    }
}