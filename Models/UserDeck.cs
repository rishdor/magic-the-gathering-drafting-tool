using System;
using System.Collections.Generic;

namespace magick.Models;

public partial class UserDeck
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string DeckName { get; set; } = null!;

    public virtual ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();

    public virtual User? User { get; set; }
}
