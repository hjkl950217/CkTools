using Microsoft.EntityFrameworkCore;

namespace 技术点验证
{
    public class SchoolContext : DbContext
    {
#pragma warning disable CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
#pragma warning restore CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。
        {
            /*
             * 反向工程命令
             * Scaffold-DbContext "Server=wcmis218\sqlexpress;Database=Lynn_Test;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir TestModels
             *
             */
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// 模型创建时
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //指定创建时的表名
            // modelBuilder.Entity<Student>().ToTable(nameof(Student));
            modelBuilder.Entity<Enrollment>().ToTable(nameof(Enrollment));
            modelBuilder.Entity<Course>().ToTable(nameof(Course));

            modelBuilder.ApplyConfiguration(new StudentMap());
        }
    }
}