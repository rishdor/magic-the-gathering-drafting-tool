using System;
using System.Collections.Generic;

namespace magick.Models;

public partial class DeckCard
{
    public int Id { get; set; }

    public int? DeckId { get; set; }

    public long? CardId { get; set; }

    public virtual Card? Card { get; set; }

    public virtual UserDeck? Deck { get; set; }
}
