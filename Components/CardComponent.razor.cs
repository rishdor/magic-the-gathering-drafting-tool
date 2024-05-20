using System;
using magick.Models;
using magick.Components;
using Microsoft.AspNetCore.Components;

namespace magick.Components
{
    public partial class CardComponent : IDisposable
    {
        [Parameter]
        public Card? card { get; set; }
    
        private CardDetailsPopup? popup;
    
        private bool _isDisposed = false;
    
        public void Dispose()
        {
            _isDisposed = true;
        }
    
        private void PrepareCardPopup()
        {
            if (!_isDisposed && card != null)
            {
                popup!.PrepareToShow(card.Id);
            }
        }
    
        private async Task ShowCardPopup()
        {
            if (!_isDisposed)
            {
                await popup!.Show();
            }
        }
    }
}