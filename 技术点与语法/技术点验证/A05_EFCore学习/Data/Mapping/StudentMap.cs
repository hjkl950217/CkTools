using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace 技术点验证
{
    public class StudentMap : IEntityTypeConfiguration<Student>

    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student));

            //builder.ToTable("Student");

            //builder.HasKey(t => t.ID);

            //builder.Property(i => i.ID)
            //    .HasColumnName("ID")
            //    .ValueGeneratedOnAdd();//设置主键在新生成时自增

            //  builder.OwnsOne()
            // builder.HasMany()//配置从属关系的
        }
    }
}