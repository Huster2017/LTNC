using System.ComponentModel.DataAnnotations;

namespace Ga.Models
{
	public class user: BsonData.Document
	{
		[Key]
		public string userId { get; set; }
		[Required]
		public int Money { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		public string Start { get; set; }
		[Required]
		public string End { get; set; }
	}
}
