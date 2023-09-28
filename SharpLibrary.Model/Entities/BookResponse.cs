using System;
namespace SharpLibrary.Models.Entities
{
	public class BookResponse
	{
        public string Name { get; set; }
        public string Writer { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}

