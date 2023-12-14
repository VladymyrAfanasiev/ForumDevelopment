namespace ForumServiceDevelopment.Data.Models
{
	public enum RequestStatusEnum
	{
		New = 0,
		Approved = 1,
		Declined = 2
	}


	public class GroupRequest
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public Guid AuthorId { get; set; }

		public RequestStatusEnum Status { get; set; }
	}
}
