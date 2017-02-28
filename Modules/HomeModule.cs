using Nancy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Nancy.ViewEngines.Razor;

namespace RegistrarApp
{
  public class HomeModule: NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/courses"] = _ => {
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Post["/courses/new"] = _ => {
        Course newCourse = new Course(Request.Form["course-name"], Request.Form["course-number"]);
        newCourse.Save();
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Get["/courses/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>{};
        var foundCourse = Course.Find(parameters.id);
        var foundStudents = foundCourse.GetStudents();
        var allStudents = Student.GetAll();
        model.Add("allStudents", allStudents);
        model.Add("course", foundCourse);
        model.Add("students", foundStudents);
        return View["course.cshtml", model];
      };
      Post["/courses/{id}/add_student"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>{};
        var foundCourse = Course.Find(parameters.id);
        foundCourse.AddStudent(Student.Find(Request.Form["students"]));
        var foundStudents = foundCourse.GetStudents();
        var allStudents = Student.GetAll();
        model.Add("allStudents", allStudents);
        model.Add("course", foundCourse);
        model.Add("students", foundStudents);
        return View["course.cshtml", model];
      };
      Get["/students"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Post["/students/new"] = _ => {
        Student newStudent = new Student(Request.Form["student-name"], Request.Form["student-enrolldate"]);
        newStudent.Save();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["/students/{id}"] = parameters => {
        Student thisStudent = Student.Find(parameters.id);
        List<Course> studentCourses = thisStudent.GetCourse();
        List<Course> allCourses = Course.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{{"student", thisStudent},{"courses", studentCourses}, {"allcourses", allCourses}};
        return View["student.cshtml", model];
      };
      Post["/students/{id}/add_course"] = parameters => {
        Course newCourse = Course.Find(int.Parse(Request.Form["students"]));
        Student thisStudent = Student.Find(parameters.id);
        thisStudent.AddCourse(newCourse);
        List<Course> studentCourses = thisStudent.GetCourse();
        List<Course> allCourses = Course.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{{"student", thisStudent},{"courses", studentCourses}, {"allcourses", allCourses}};
        return View["student.cshtml", model];
      };
    }
  }
}
