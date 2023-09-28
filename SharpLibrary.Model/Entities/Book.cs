namespace SharpLibrary.Models.Entities
{
    public class Book
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}

