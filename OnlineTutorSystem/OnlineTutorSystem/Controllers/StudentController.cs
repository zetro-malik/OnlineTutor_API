using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTutorSystem.Models;
using System.Data.SqlClient;

namespace OnlineTutorSystem.Controllers
{
    public class StudentController : Controller
    {

        /// <summary>
        /// first thing to reset ression 
        /// </summary>
        /// 
        
       


        static string constr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=onlinetutor;Data Source=ZETRO\\MSSQLSERVER01";
        SqlConnection con = new SqlConnection(constr);
        public ActionResult Home()
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }
           
            return View();
        }

        public ActionResult contact()
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }


            return View();
        }


        
        public ActionResult SearchTutor()
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            return View();
        }


        [HttpPost]
        public ActionResult SearchTutor(String ncity,string nclass, string nsubject)
        {
            List<SearchedTutor> lst = new List<SearchedTutor>();
           nclass = getClass(nclass);
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            con.Open();
            String q= "select * from tutor left join subjects on tutor.tid=subjects.tid left join timing on tutor.tid=timing.tid and subjects.subid=timing.subid where not time is NULL and city='"+ncity+"' and title='"+nsubject+"'and class='"+ nclass + "' and not timeid in(select timeid from apply)";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr=cmd.ExecuteReader();
            if(sdr.HasRows)
            {
                String oldtid = "";
                while (sdr.Read())
                {
                    if (oldtid != sdr["tid"].ToString())
                    {
                        oldtid = sdr["tid"].ToString();
                        SearchedTutor temp = new SearchedTutor();
                        temp.id = sdr["tid"].ToString();
                        temp.fname = sdr["fname"].ToString();
                        temp.lname = sdr["lname"].ToString();
                        temp.gender = sdr["gender"].ToString();
                        temp.city = sdr["city"].ToString();
                        temp.Class = sdr["class"].ToString();
                      
                        temp.price = sdr["price"].ToString();
                        temp.time = sdr["time"].ToString();
                        temp.Bio = sdr["bio"].ToString();
                        temp.img = (byte[])sdr["img"];
                        temp.subject = sdr["title"].ToString();
                        lst.Add(temp);


                    }
                }


            }
            else
            {
                ViewBag.islstnull = "No Results Found....";
            }

           


            con.Close();

            return View(lst);
        }
       

        [HttpPost]
        public ActionResult getTimeFromSearchTutor(String id)
        {
         Session["tm"] = id;
            return Json(1, JsonRequestBehavior.AllowGet);
        }

      public ActionResult applyStudent(String ddl, String id)
        {
            
            
            con.Open();
            string q = "select timeid from timing where tid='" + id + "' and time='" + ddl + "'";
            SqlCommand cmd=new SqlCommand(q,con);
            SqlDataReader sdr = cmd.ExecuteReader();
      
            sdr.Read();
            
            q = "insert into apply values('" + Session["studentID"].ToString() + "','" + sdr[0].ToString() + "')";
            sdr.Close();
            SqlCommand cmd1 =new SqlCommand(q,con);
            cmd1.ExecuteNonQuery();
            con.Close();
                                        

            return View("SearchTutor");
        }



        /// <summary>
        /// using jsonReasult method
        /// that is being called from searchTutor page
        /// through jQuery and passing Class id 
        /// to search Subjects
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult GetSubList(String id)
        {
            var Subject = new List<SelectListItem>();
            if(id=="")
                return Json(Subject, JsonRequestBehavior.AllowGet);


            string constr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=onlinetutor;Data Source=ZETRO\\MSSQLSERVER01";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            String q = "select subject from classSub where cid="+id;
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Subject.Add(new SelectListItem { Value = dr["subject"].ToString(), Text = dr["subject"].ToString() });
            }
            dr.Close();
            con.Close();
            return Json(Subject, JsonRequestBehavior.AllowGet);

        }
       
        [HttpPost]
        public String getddl(String id)
        {
            ViewBag.a=id;
            return id;
        }
        //return list type function 
        public ActionResult Mytutors()
        {
            List<SearchedTutor> lst = new List<SearchedTutor>();
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            con.Open();

            String q = "select * from tutor left join subjects on tutor.tid=subjects.tid left join timing on tutor.tid=timing.tid and subjects.subid=timing.subid where not time is NULL and timeid in(select timeid from apply where sid='"+ Session["studentID"].ToString()+"')";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            { 
                    SearchedTutor temp = new SearchedTutor();
                    temp.id = sdr["tid"].ToString();
                    temp.fname = sdr["fname"].ToString();
                    temp.lname = sdr["lname"].ToString();
                    temp.gender = sdr["gender"].ToString();
                    temp.city = sdr["city"].ToString();
                    temp.Class = sdr["class"].ToString();
                    temp.subject = sdr["title"].ToString();
                    temp.price = sdr["price"].ToString();
                    temp.time = sdr["time"].ToString();
                    temp.Bio = sdr["bio"].ToString();
                    temp.img = (byte[])sdr["img"];

                    lst.Add(temp);

            }

            con.Close();
           
                return View(lst);
        }





        // last NAV-LINK is profile




        // not worked on it yet.....
        public ActionResult ProfileEdit()
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult ProfileEdit(Student obj, String ps,HttpPostedFileBase image)
        {
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            
            con.Open();
            String q = "select password from student where sid='" +Session["studentID"].ToString()+"'";
            SqlCommand c = new SqlCommand(q, con);
            SqlDataReader dr = c.ExecuteReader();
            dr.Read();
            
            if(dr[0].Equals(ps))
            {
               // dr.Close();
               dr.Close();
                string check = "select * from student where email='" + obj.email + "' and not sid='" +Session["studentID"].ToString()+"'";
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
                        String query = "update student set fname='" + obj.fname + "', lname='" + obj.lname + "',phone_no='" + obj.phone_no + "', email='" + obj.email + "', password='" + obj.password + "', img=@img where sid = '" + Session["studentID"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = obj.img;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        String query = "update student set fname='" + obj.fname + "', lname='" + obj.lname + "',phone_no='" + obj.phone_no + "', email='" + obj.email + "', password='" + obj.password + "' where sid = '" + Session["studentID"].ToString() + "'";
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
            return View() ;
        }




        public ActionResult logout()
        {
            Session.Abandon();
            Session["studentID"] = null;
           

            return RedirectToAction("loginStudent", "LS", new { area = "" });
            

        }


        public String getClass(string a)
        {
            if (a.Equals("1"))
            {
                return "9th";
            }
            if (a.Equals("2"))
            {
                return "10th";
            }
            if (a.Equals("3"))
            {
                return "11th";
            }
            if (a.Equals("4"))
            {
                return "12th";
            }
            return "-1";
        }
        
        /// action for actionlink of my tutors
        
        public ActionResult unhireTutor(string id,string time)
        {
            con.Open();
            string q = "delete from apply where timeid in(select timeid from timing where tid='"+id+"' and time='"+time+"')";
            SqlCommand cmd = new SqlCommand(q,con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Mytutors");
        }


        public ActionResult detailsTutor(string id)
        {
            List<SearchedTutor> lst = new List<SearchedTutor>();
            if (Session["studentID"] == null)
            {
                return RedirectToAction("loginStudent", "LS", new { area = "" });
            }

            con.Open();

            String q = "select * from tutor where tid='" + id + "'"; 
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();

           sdr.Read();
            SearchedTutor temp = new SearchedTutor();
                temp.fname = sdr["fname"].ToString();
                temp.lname = sdr["lname"].ToString();
                temp.gender = sdr["gender"].ToString();
                temp.city = sdr["city"].ToString();
                temp.Class = sdr["class"].ToString();
                temp.phone = sdr["phone_no"].ToString();
                temp.email = sdr["email"].ToString();
                temp.Bio = sdr["bio"].ToString();
                temp.img = (byte[])sdr["img"];

                lst.Add(temp);

            

            con.Close();

            return View(lst);
        }
    }
}