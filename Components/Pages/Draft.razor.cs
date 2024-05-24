using magick.Models;
using magick.Controllers;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages
{
    public partial class Draft
    {
        [Inject] public DraftController? DraftController { get; set; }
        [Inject] public NavigationManager? Navigation { get; set; }
        [Parameter] public string? SetCode { get; set; }

        private List<Tagged<Card>> table = [];
        private List<Tagged<Card>> deck = [];
        private string deckName = "";

        protected override void OnInitialized()
        {
            DraftController!.StartDraft(SetCode!);
            table = DraftController!.GetTable();
            deck  = DraftController!.GetDeck();
        }

        private void OpenPack()
        {
            DraftController!.OpenPack();
            table = DraftController!.GetTable();
            deck  = DraftController!.GetDeck();
        }

        private void AddCardToDeck(Guid cardUid)
        {
            DraftController!.AddCardToDeck(cardUid);
            table = DraftController!.GetTable();
            deck  = DraftController!.GetDeck();
        }

        private void RemoveCardFromDeck(Guid cardUid)
        {
            DraftController!.RemoveCardFromDeck(cardUid);
            table = DraftController!.GetTable();
            deck  = DraftController!.GetDeck();
        }

        private void FinishDraft()
        {
            DraftController!.FinishDraft(deckName);
            Navigation!.NavigateTo("/decks");
        }
    }
}