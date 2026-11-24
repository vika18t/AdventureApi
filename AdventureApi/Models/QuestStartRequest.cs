namespace AdventureApi.Models
{
	public class QuestStartRequest
	{
		public int PlayerId { get; set; }
		public int QuestId { get; set; }
		public List<string> Equipment { get; set; } = new();
	}
}
