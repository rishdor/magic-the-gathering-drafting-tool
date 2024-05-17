using magick.Models;
using magick.Services;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Draft
{
    [Inject] public DraftService? DraftService { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }
    [Parameter] public string? SetCode { get; set; }

    private List<Tagged<Card>> table = [];
    private List<Tagged<Card>> deck = [];
    private string deckName = "";


    protected override void OnInitialized()
    {
        DraftService!.StartDraft(SetCode!);
        table = DraftService!.GetTable();
        deck  = DraftService!.GetDeck();
    }


    private void OpenPack()
    {
        DraftService!.OpenPack();
        table = DraftService!.GetTable();
        deck  = DraftService!.GetDeck();
    }

    private void AddCardToDeck(Guid cardUid)
    {
        DraftService!.AddCardToDeck(cardUid);
        table = DraftService!.GetTable();
        deck  = DraftService!.GetDeck();
    }

    private void RemoveCardFromDeck(Guid cardUid)
    {
        DraftService!.RemoveCardFromDeck(cardUid);
        table = DraftService!.GetTable();
        deck  = DraftService!.GetDeck();
    }

    private void FinishDraft()
    {
        DraftService!.FinishDraft(deckName);
        Navigation!.NavigateTo("/decks");
    }
}