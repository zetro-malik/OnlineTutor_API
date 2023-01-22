using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using OnlineTutorSystem.Models;
namespace OnlineTutorSystem.Controllers
{
    public class TutorController : Controller
    {
        static string constr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=onlinetutor;Data Source=ZETRO\\MSSQLSERVER01";
        SqlConnection con = new SqlConnection(constr);
        // GET: Tutor
        public ActionResult Home()
        {
            return View();
        }


        public ActionResult contact()
        {
            return View();
        }



        public ActionResult MyStudents()
        {
            List<searchedStudentcs> lst = new List<searchedStudentcs>();
            if (Session["tutorID"] == null)
            {
                return RedirectToAction("loginTeacher", "LS", new { area = "" });
            }

            con.Open();

            String q = "select student.sid,student.fname,student.lname,student.gender,student.phone_no,student.email,student.img,timing.time,subjects.title,subjects.price,tutor.class,tutor.city from student left join apply on student.sid=apply.sid left join timing on apply.timeid=timing.timeid left join subjects on subjects.subid=timing.subid  left join tutor on tutor.tid=subjects.tid where timing.tid='"+ Session["tutorID"].ToString()+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                searchedStudentcs temp = new searchedStudentcs();
                temp.id = sdr["sid"].ToString();
                temp.fname = sdr["fname"].ToString();
                temp.lname = sdr["lname"].ToString();
                temp.gender = sdr["gender"].ToString();
                temp.city = sdr["city"].ToString();
                temp.Class = sdr["class"].ToString();
                temp.subject = sdr["title"].ToString();
                temp.price = sdr["price"].ToString();
                temp.phone = sdr["phone_no"].ToString();
                temp.email = sdr["email"].ToString();
                temp.time = sdr["title"].ToString();
                temp.time = sdr["time"].ToString();
                temp.img = (byte[])sdr["img"];

                lst.Add(temp);

            }

            con.Close();

            return View(lst);
           
        }



        public ActionResult detailsStudent(String id)
        {
          
            List<searchedStudentcs> lst = new List<searchedStudentcs>();
            if (Session["tutorID"] == null)
            {
               // return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            con.Open();
            String q="select * from student where sid='"+id+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            sdr.Read();
                searchedStudentcs temp = new searchedStudentcs();
                temp.id = sdr["sid"].ToString();
                temp.fname = sdr["fname"].ToString();
                temp.lname = sdr["lname"].ToString();
                temp.gender = sdr["gender"].ToString();  
                temp.phone = sdr["phone_no"].ToString();
                temp.email = sdr["email"].ToString();
                temp.img = (byte[])sdr["img"];

                lst.Add(temp);

           

            con.Close();

            return View(lst);
        }




        // not worked on it yet.....
        public ActionResult ProfileEdit()
        {
            if (Session["tutorID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult ProfileEdit(tutor obj, String ps, HttpPostedFileBase image)
        {
            if (Session["tutorID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }


            con.Open();
            String q = "select password from tutor where tid='" + Session["tutorID"].ToString() + "'";
            SqlCommand c = new SqlCommand(q, con);
            SqlDataReader dr = c.ExecuteReader();
            dr.Read();

            if (dr[0].Equals(ps))
            {
                // dr.Close();
                dr.Close();
                string check = "select * from tutor where email='" + obj.email + "' and not tid='" + Session["tutorID"].ToString() + "'";
                SqlCommand chk = new SqlCommand(check, con);
                SqlDataReader dataCheck = chk.ExecuteReader();
                if (dataCheck.HasRows)
                {
                    ViewBag.Message = "Account already been created on this email ";

                }
                else
                {
                    dataCheck.Close();
                    if (image != null)
                    {
                        obj.img = new byte[image.ContentLength];
                        image.InputStream.Read(obj.img, 0, image.ContentLength);
                        String query = "update tutor set fname='" + obj.fname + "', lname='" + obj.lname +"', city='"+obj.city+"',phone_no='" + obj.phone_no + "', email='" + obj.email + "', password='" + obj.password + "',bio='" + obj.Bio+"', img=@img where tid = '" + Session["tutorID"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = obj.img;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        String query = "update tutor set fname='" + obj.fname + "', lname='" + obj.lname + "', city='" + obj.city + "',phone_no='" + obj.phone_no + "', email='" + obj.email + "', password='" + obj.password + "',bio='" + obj.Bio + "' where tid = '" + Session["tutorID"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }


                }


            }
            else
            {
                ViewBag.Message = "Password is Incorrect";

            }

            con.Close();
            return View();
        }
        public ActionResult AddSubjects()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubject(subject obj)
        {
            con.Open();
            string q = "insert into subjects values(" + int.Parse(Session["tutorID"].ToString()) + ",'" + obj.title + "'," + obj.price + ")";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close(); 

            return View("AddSubjects");
        }
        [HttpPost]
        public ActionResult AddTimming(subject obj)
        {
            con.Open();
            string q = "select subid from subjects where title='" + obj.title + "' and tid=" + int.Parse(Session["tutorID"].ToString());
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();  
            dr.Read();
            q= "insert into timing (tid,subid,time) values(" + int.Parse(Session["tutorID"].ToString()) +","+int.Parse(dr[0].ToString())+",'"+obj.timing + "')";
            con.Close();
            con.Open();
            dr.Close();
            SqlCommand cmd1 = new SqlCommand(q, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            return View("AddSubjects");
        }
    }
}