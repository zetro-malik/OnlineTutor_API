using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using OnlineTutorAPI.Models;
using System.Configuration;

namespace OnlineTutorAPI.Models
{
    public class StudentAccess
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);


        public bool Insert(student obj)
        {
            con.Open();
            string check = "select * from student where email='" + obj.email + "'";
            SqlCommand chk = new SqlCommand(check, con);
            SqlDataReader dataCheck = chk.ExecuteReader();
            if (!dataCheck.HasRows)
            {
                dataCheck.Close();
                string q = "insert into student values('" + obj.fname + "','" + obj.lname + "','" + obj.gender + "','" + obj.phone_no + "','" + obj.email + "','" + obj.password + "',@img)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = obj.img;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public List<student> select(string q)
        {
            List<student> lst = new List<student>();
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                student obj = new student();
                obj.id = int.Parse(sdr["sid"].ToString());
                obj.fname = sdr["fname"].ToString();
                obj.lname = sdr["lname"].ToString();
                obj.phone_no = sdr["phone_no"].ToString();
                obj.email = sdr["email"].ToString();

              
                lst.Add(obj);
            }
            con.Close();

            return lst;
        }
    }
}