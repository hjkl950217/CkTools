namespace 技术点验证
{
    public class DbInitializer
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="context"></param>
        public static void Initalze(SchoolContext context)
        {
            //创建数据库
            //存在时不会创建数据库并返回false
            context.Database.EnsureCreated();

            //防御-防重复插种子数据
            if(context.Students.Any())
            {
                return;//代表数据库已经有种子了
            }

            #region Student

            Student[] students = new Student[]
            {
                new Student()
                {
                    FirstName="张",
                    LastName="三",
                    EnrollmentDate=Convert.ToDateTime("2001-1-1")
                },
                new Student()
                {
                    FirstName="李",
                    LastName="四",
                    EnrollmentDate=Convert.ToDateTime("2001-2-2")
                },
                new Student()
                {
                    FirstName="赵",
                    LastName="六",
                    EnrollmentDate=Convert.ToDateTime("2008-3-3")
                },
                new Student()
                {
                    FirstName="周",
                    LastName="七",
                    EnrollmentDate=Convert.ToDateTime("2006-4-4")
                }
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            #endregion Student

            #region Course

            Course[] courses = new Course[]
            {                new Course()
                {
                    CourseID=128,
                    Title="物理",
                    Credits=3
                },
                new Course()
                {
                    CourseID=256,
                    Title="数学",
                    Credits=1
                },
                new Course()
                {
                    CourseID=512,
                    Title="化学",
                    Credits=2
                }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            #endregion Course

            #region Enrollment

            Enrollment[] enrollments = new Enrollment[]
            {
                new Enrollment()
                {
                    StudentID=1,
                    CourseID=128,
                    Grade=Grade.A
                },
                new Enrollment()
                {
                    StudentID=1,
                    CourseID=256,
                    Grade=Grade.B
                },
                new Enrollment()
                {
                    StudentID=1,
                    CourseID=512,
                    Grade=Grade.C
                },
                new Enrollment()
                {
                    StudentID=2,
                    CourseID=256,
                    Grade=Grade.C
                },
                new Enrollment()
                {
                    StudentID=2,
                    CourseID=512,
                    Grade=Grade.C
                },
            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

            #endregion Enrollment
        }
    }
}