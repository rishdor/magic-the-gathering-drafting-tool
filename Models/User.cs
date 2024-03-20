using System;
using System.Collections.Generic;

namespace magick.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<UserDeck> UserDecks { get; set; } = new List<UserDeck>();
}
