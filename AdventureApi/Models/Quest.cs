namespace AdventureApi.Models
{
	public class Quest
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public int Difficulty { get; set; }
		public int Reward { get; set; }
		public string Location { get; set; } = "";
		public bool IsDangerous { get; set; }
	}
}
