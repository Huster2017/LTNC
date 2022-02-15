
using Microsoft.EntityFrameworkCore;

namespace Ga.Models
{
	public class Context : DbContext
	{
		public DbSet<user> Users { get; set; }
	}
}
