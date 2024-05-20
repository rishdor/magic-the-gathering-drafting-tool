using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components
{
    public partial class CardDetailsPopup
    {
        [Inject]
        public SetService? SetService { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback<bool> IsVisibleChanged { get; set; }

        [Parameter]
        public string? HeaderText { get; set; }

        private Card? _card;
        private Set? _set;
        private Rarity? _rarity;

        public void Show(Card card)
        {
            Close();
            _card = card;
            HeaderText = card.Name;
            _set = SetService!.GetSetByCode(_card.SetCode);
            _rarity = _card.RarityCode != null ? SetService.GetRarityByCode(_card.RarityCode) : null;
            IsVisible = true;
            StateHasChanged();
        }

        public void Close()
        {
            HeaderText = string.Empty;
            IsVisible = false;
            _card = null;
            StateHasChanged();
        }
    }
}