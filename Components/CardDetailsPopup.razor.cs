using System;
using magick.Controllers;
using Microsoft.AspNetCore.Components;

namespace magick.Components
{
    public partial class CardDetailsPopup : IDisposable
    {
        [Inject]
        public CardDetailsPopupController? Controller { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback<bool> IsVisibleChanged { get; set; }

        [Parameter]
        public string? HeaderText { get; set; }

        private bool _isDisposed = false;
        
        public void Dispose()
        {
            _isDisposed = true;
        }
        
        public void PrepareToShow(long cardId)
        {
            if (IsVisible)
            {
                Close();
            }
            Controller!.PrepareToShow(cardId);
            if (!_isDisposed)
            {
                StateHasChanged();
            }
        }

        public async Task Show()
        {
            await Controller!.Show();
            if (!_isDisposed)
            {
                StateHasChanged();
            }
        }
        public void Close()
        {
            HeaderText = string.Empty;
            IsVisible = false;
            Controller!.Close();
            if (!_isDisposed)
            {
                StateHasChanged();
            }
        }
    }
}