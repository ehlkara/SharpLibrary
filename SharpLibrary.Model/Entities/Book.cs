﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SharpLibrary.Models.Entities
{
    [Table("Books")]
    public class Book : BaseEntity
	{
        public string Name { get; set; }
        public string Writer { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}

