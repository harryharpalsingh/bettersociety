﻿using bettersociety.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bettersociety.Areas.User.Dtos
{
    public class CreateBlogPostDto
    { 
        public required string Title { get; set; } = string.Empty;

        //public int? ImageID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public int Deleted { get; set; } = 0;
    }
}