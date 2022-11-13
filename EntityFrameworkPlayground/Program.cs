using EntityFrameworkPlayground;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

using var dbContext = new SchoolDbContextFactory().CreateDbContext(new string[1]);
SchoolDbContextFactory.Seed(dbContext);
//var student1 = (from _student in dbContext.Students
//                where _student.Id == 1
//                select new
//                {
//                    FirstName = _student.FirstMidName,
//                    LastName = _student.LastName,
//                    Courses = (from enrollment in _student.Enrollments select enrollment.Course).ToList()
//                }).Single();
//var student2 = dbContext.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).Single(s => s.Id == 1);
var student3 = dbContext.Students.Single(s => s.Id == 1);
dbContext.Entry(student3).Collection(s => s.Enrollments).Load();
return;