using magick.Models.Forms;
using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Decks {
    [Inject] public DeckService? DeckService { get; set; }
   
    private List<UserDeck> decks = [];
    

    protected override async Task OnInitializedAsync()
        => await GetDecks();

    private async Task GetDecks()
    {
        decks = await DeckService!.GetDecks();
    }
}