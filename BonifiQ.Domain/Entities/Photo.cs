using System;
using System.Collections.Generic;

namespace BonifiQ.Domain.Entities
{
    public partial class Photo
    {
        public int? AlbumId { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? ThumbnailUrl { get; set; }
        public DateTime? DateIncluded { get; set; }
    }
}
