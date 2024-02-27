using System;
using System.Collections.Generic;

namespace magick.Models;

public partial class Artist
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
