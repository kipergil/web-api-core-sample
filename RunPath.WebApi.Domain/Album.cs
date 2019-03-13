using System;
using System.Collections.Generic;

namespace RunPath.WebApi.Domain
{
    public class Album : BaseEntity
    {
        public int userId { get; set; }
        public string title { get; set; }
        public List<Photo> albumPhotos { get; set; } = new List<Photo>();
    }
}
