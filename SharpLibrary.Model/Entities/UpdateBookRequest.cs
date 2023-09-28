namespace SharpLibrary.Models.Entities
{
    public class UpdateBookRequest
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool IsActive { get; set; }
    }
}

