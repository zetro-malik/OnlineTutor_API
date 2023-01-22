using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTutorSystem.Models;
using System.Net.Http;

namespace OnlineTutorSystem.Controllers
{
    public class LSController : Controller
    {
       
   
        public ActionResult SignUpStudent()
        { 
           
            return View();
        }
        [HttpPost]
        public ActionResult SignUpStudent(Student obj, HttpPostedFileBase image)
        {

            if (image != null)
            {
                obj.img = new byte[image.ContentLength];
                image.InputStream.Read(obj.img, 0, image.ContentLength);
            }
            else
            {
                ViewBag.Message = "please insert Image";
                return View();
            }




            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/Student");
                    var posttask = client.PostAsJsonAsync<Student>("Student", obj);
                    posttask.Wait();
                    var result = posttask.Result;
                   
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("loginStudent");

                    }
                    else
                    {
                        ViewBag.Message = "Account already been created on this email ";
                        return View();
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator.");
                return View(obj);
            }


            #region "old code"
            // con.Open();
            // string check = "select * from student where email='" + obj.email+"'";
            // SqlCommand chk=new SqlCommand(check,con);
            // SqlDataReader dataCheck=chk.ExecuteReader();
            // if(!dataCheck.HasRows)
            // {
            //     dataCheck.Close();
            //     string q = "insert into student values('" + obj.fname + "','" + obj.lname + "','" + obj.gender + "','" + obj.phone_no + "','" + obj.email + "','" + obj.password + "',@img)";
            //     SqlCommand cmd = new SqlCommand(q, con);
            //     cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = obj.img;
            //     cmd.ExecuteNonQuery();
            //     con.Close();
            //     return RedirectToAction("loginStudent");
            // }
            //dataCheck.Close();
            // ViewBag.Message = "Account already been created on this email ";
            //return View();
            #endregion
        }

        public ActionResult loginStudent()
        {
            HttpCookie ck = Request.Cookies["tempStudent"];
            if(ck!=null)
            {
                ViewBag.studentEmail=ck["Email"];
                ViewBag.studentPassword=ck["Password"];
            }
                return View();
        }

        [HttpPost]
        public ActionResult loginStudent(Student obj)
        {
            List<Student> lst;

            String query = "select * from student where email='" + obj.email + "' and password='" + obj.password + "'";

          
                using (var client = new HttpClient())
                {
 
                    client.BaseAddress = new Uri("https://localhost:44336/api/Student");
                    
                    var responseTask = client.GetAsync("Student?q=" + query);
   
                    responseTask.Wait();
            
                    var result = responseTask.Result;
         
                    if (result.IsSuccessStatusCode)
                    {
                        
                        var readTask =result.Content.ReadAsAsync<List<Student>>();
                        
                         readTask.Wait();
                       
                        lst = readTask.Result;
                        if(lst.Any())
                        {
                       
                            Session["studentID"] = lst[0].id;
                            HttpCookie cookie = new HttpCookie("tempStudent");
                            cookie["Email"] = obj.email;
                            cookie["Password"] = obj.password;

                            Response.Cookies.Add(cookie);
                            cookie.Expires = DateTime.Now.AddDays(2);

                            return RedirectToAction("Home", "Student", new { area = "" });
                        }
                        else
                        {
                            ViewBag.Message = "User Not exists";
                            return View();
                        }
                    }
                    else 
                    {
                  
                        ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                    }
                   
                    return View(obj);
                }






            #region "old code"
            //con.Open();

            //SqlCommand cmd = new SqlCommand(q, con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            //{
            //    dr.Read();
            //    Session["studentID"] = dr[0].ToString();
            //    HttpCookie cookie = new HttpCookie("tempStudent");
            //    cookie["Email"] = obj.email;
            //    cookie["Password"] = obj.password;

            //    Response.Cookies.Add(cookie);
            //    cookie.Expires=DateTime.Now.AddDays(2);

            //    return RedirectToAction("Home", "Student", new { area = "" });
            //}
            //con.Close();
            //ViewBag.Message = "invalid email or password";
            //return View();
            #endregion

        }

        public ActionResult hello()
        {
            return View();
        }
        
        public ActionResult loginTeacher()
        {
            HttpCookie cook = Request.Cookies["TempTutor"];
            if(cook!=null)
            {
                ViewBag.tutorEmail = cook["Email"];
                ViewBag.tutorPassword = cook["Password"];
            }
            return View();
        }


        [HttpPost]
        public ActionResult loginTeacher(tutor obj)
        {

            List<tutor> lst;

            String q = "select * from tutor where email='" + obj.email + "' and password='" + obj.password + "'";


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44336/api/Tutor");

                var responseTask = client.GetAsync("Tutor?q=" + q);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<tutor>>();

                    readTask.Wait();

                    lst = readTask.Result;
                    if (lst.Any())
                    {

                        Session["tutorID"] = lst[0].id;
                        HttpCookie cookies = new HttpCookie("TempTutor");
                        cookies["Email"] = obj.email;
                        cookies["Password"] = obj.password;
                        Response.Cookies.Add(cookies);
                        cookies.Expires = DateTime.Now.AddDays(2);
                       
                         return RedirectToAction("Home", "Tutor", new { area = "" });
                    }
                    else
                    {
                        ViewBag.Message = "User Not exists";
                        return View();
                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }

                return View(obj);
            }
            #region "old code"
            //con.Open();
            //String q = "select * from tutor where email='" + obj.email + "' and password='" + obj.password + "'";
            //SqlCommand cmd = new SqlCommand(q, con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            //{


            //    dr.Read();

            //    Session["tutorID"] = int.Parse(dr[0].ToString());
            //    con.Close();
            //    HttpCookie cookies = new HttpCookie("TempTutor");
            //    cookies["Email"]=obj.email;
            //    cookies["Password"]=obj.password;
            //    Response.Cookies.Add(cookies);
            //    cookies.Expires = DateTime.Now.AddDays(2);
            //    if (FirstLogIn())
            //    {
            //        return RedirectToAction("AddSubjects", "Tutor", new { area = "" });

            //    }
            //    else
            //    return RedirectToAction("Home", "Tutor", new { area = "" });
            //}
            //con.Close();
            //ViewBag.Message = "invalid email or password";
            //return View();
            #endregion
        }


        public ActionResult SignUpTeacher()
        {

            return View();
        }

        [HttpPost]
        public ActionResult  SignUpTeacher(tutor obj, HttpPostedFileBase image)
        {

            if (image != null)
            {
                obj.img = new byte[image.ContentLength];
                image.InputStream.Read(obj.img, 0, image.ContentLength);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/Tutor");
                    var posttask = client.PostAsJsonAsync<tutor>("Tutor", obj);
                    posttask.Wait();
                    var result = posttask.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("loginTeacher");

                    }
                    else
                    {
                        ViewBag.Message = "Account already been created on this email ";
                        return View();
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator.");
                return View(obj);
            }

            #region "old code"

            //con.Open();
            //string check = "select * from tutor where email='" + obj.email + "'";
            //SqlCommand chk = new SqlCommand(check, con);
            //SqlDataReader dataCheck = chk.ExecuteReader();
            //if (!dataCheck.HasRows)
            //{
            //    dataCheck.Close();
            //    string q = "insert into tutor values('" + obj.fname + "','" + obj.lname + "','" + obj.gender + "','" + obj.phone_no + "','"+obj.city +"','"+obj.Class+"','" +obj.email + "','" + obj.password+ "','" + obj.Bio+"',@img)";
            //    SqlCommand cmd = new SqlCommand(q, con);
            //    cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = obj.img;
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    return RedirectToAction("loginTeacher");
            //}

            //ViewBag.Message = "Account already been created on this email ";
            //return View();
            #endregion
        }




        public string AlreadyIn()
        {
            return "user already have account";
        }





        #region "TODO firstLogin"
        //public bool FirstLogIn()
        //{
        //    con.Close();
        //    con.Open();
        //    String q = "select * from tutor where tid in(select tid from subjects where tid="+Session["tutorID"]+")";
        //    SqlCommand cmd=new SqlCommand(q, con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if(dr.HasRows)
        //    {
        //        con.Close();
        //        return false;
        //    }
        //    con.Close();
        //    return true;
        //}

        #endregion


        #region "pata ni konsa code ha..." 

        //public ActionResult display()
        //{
        //    List<Student> students = new List<Student>();
        //    con.Open();
        //    String q = "select * from student";
        //    SqlCommand c = new SqlCommand(q, con);
        //    SqlDataReader dr = c.ExecuteReader();
        //    dr.Read();
        //    Student student = new Student();
        //    student.lname = dr["fname"].ToString();
        //    student.img = (byte[])dr["img"];
        //    students.Add(student);
        //    return View(students);
        //}

        //public ActionResult display2()
        //{
        //    List<Student> students = new List<Student>();
        //    con.Open();
        //    String q = "select * from student";
        //    SqlCommand c = new SqlCommand(q, con);
        //    SqlDataReader dr = c.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Student student = new Student();
        //        student.lname = dr["fname"].ToString();
        //        student.img = (byte[])dr["img"];
        //        students.Add(student);
        //    }
        //    return View(students);
        //}
        #endregion
    }
}