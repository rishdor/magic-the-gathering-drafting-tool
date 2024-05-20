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

        private void CardPopup()
        {
            if (!_isDisposed)
            {
                popup!.Show(card!);
            }
        }
    }
}