using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProNotes.AppData.Entities.Configurations
{
	public class CategoryConfig: IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(p=> p.CategoryId).ValueGeneratedOnAdd();
		}
	}
}
