using Microsoft.AspNetCore.Mvc;
using StudentScore.Data;
using StudentScore.Models;

namespace StudentScore.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext DB;
        public StudentController(ApplicationDbContext db)
        {
            DB = db;
        }
        public IActionResult Index()
        {
            var GetStudents = DB.Students.ToList();
            return View(GetStudents);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudents(Student Student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Result = Student.Collect + Student.Mid + Student.Final;
                    var Model = new Student();
                    Model.ID = Student.ID;
                    Model.FirstName = Student.FirstName;
                    Model.LastName = Student.LastName;
                    Model.Collect = Student.Collect;
                    Model.Mid = Student.Mid;
                    Model.Final = Student.Final;
                    Model.Total = Result;
                    if (Result >= 90)
                    {
                        Model.Grade = "A";
                    }
                    else if (Result >= 80)
                    {
                        Model.Grade = "B";
                    }
                    else if (Result >= 70)
                    {
                        Model.Grade = "C";
                    }
                    else if (Result >= 60)
                    {
                        Model.Grade = "D";
                    }
                    else {
                        Model.Grade = "F";
                    }
                    DB.Students.Add(Model);
                    DB.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Error)
            {
                return Json(new { valid = false, message = Error.Message });
            }
            return View(Student);
        }

        public IActionResult EditStudent(int? ID)
        {
            try
            {
                var GetStudent = DB.Students.Where(w => w.ID == ID).FirstOrDefault();
                if (GetStudent != null)
                {
                    return View(GetStudent);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception Error)
            {
                return Json(new { valid = false, message = Error.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudents(Student Student)
        {
            if (ModelState.IsValid)
            {
                var Result = Student.Collect + Student.Mid + Student.Final;
                var GetStudent = DB.Students.Where(w=>w.ID == Student.ID).FirstOrDefault();
                GetStudent.ID = Student.ID;
                GetStudent.FirstName = Student.FirstName;
                GetStudent.LastName = Student.LastName;
                GetStudent.Collect = Student.Collect;
                GetStudent.Mid = Student.Mid;
                GetStudent.Final = Student.Final;
                GetStudent.Total = Result;
                if (Result >= 90)
                {
                    GetStudent.Grade = "A";
                }
                else if (Result >= 80)
                {
                    GetStudent.Grade = "B";
                }
                else if (Result >= 70)
                {
                    GetStudent.Grade = "C";
                }
                else if (Result >= 60)
                {
                    GetStudent.Grade = "D";
                }
                else
                {
                    GetStudent.Grade = "F";
                }
                DB.Students.Update(GetStudent);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Student);
        }

        public IActionResult DeleteStudent(int? ID)
        {
            try
            {
                var GetStudent = DB.Students.Where(w => w.ID == ID).FirstOrDefault();
                if (GetStudent != null)
                {
                    DB.Students.Remove(GetStudent);
                    DB.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception Error)
            {
                return Json(new { valid = false, message = Error.Message });
            }
        }

        public IActionResult Viewer(int? ID)
        {
            try
            {
                var GetStudent = DB.Students.Where(w => w.ID == ID).FirstOrDefault();
                if (GetStudent != null)
                {
                    return View(GetStudent);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception Error)
            {
                return Json(new { valid = false, message = Error.Message });
            }
        }
    }
}
