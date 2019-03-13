using System;

namespace RunPath.WebApi.Domain
{
    public class Photo : BaseEntity
    {
        public int albumId { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
    }
}
