using magick.Models;
using magick.Services;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Draft
{
    [Inject] public DraftService? DraftService { get; set; }
    [Parameter] public string? SetCode { get; set; }

    private List<Card> table = new();
    private List<Card> deck = new();


    protected override void OnInitialized()
    {
        DraftService!.StartDraft(SetCode!);
        table = DraftService!.GetTable();
        deck  = DraftService!.GetDeck();
    }


    private void OpenPack()
        => DraftService!.OpenPack();

    private void AddCardToDeck(int tableIndex)
        => DraftService!.AddCardToDeck(tableIndex);

    private void RemoveCardFromDeck(int deckIndex)
        => DraftService!.RemoveCardFromDeck(deckIndex);
}