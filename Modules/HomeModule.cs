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
        model.Add("course", foundCourse);
        model.Add("students", foundStudents);
        return View["course.cshtml", model];
      };
    }
  }
}
