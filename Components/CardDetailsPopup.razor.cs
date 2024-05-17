using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components;

    public partial class CardDetailsPopup
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback<bool> IsVisibleChanged { get; set; }

        [Parameter]
        public string? HeaderText { get; set; }

        private Card? _card;

        public void Show(Card card)
        {
            Close();
            _card = card;
            HeaderText = card.Name;
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