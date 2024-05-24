using magick.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace magick.Components
{
    public partial class GalleryComponent : ComponentBase
    {
        [Parameter]
        public List<Card>? Cards { get; set; }

        [Parameter]
        public EventCallback OnLoadMore { get; set; }

        [Parameter]
        public string NoMatchingCardsError { get; set; } = "";

        [Parameter]
        public string NoMatchingCardsMessage { get; set; } = "";
        [Parameter]
        public bool ShowLoadMoreButton { get; set; }

        public void UpdateNoMatchingCardsMessage(string error, string message)
        {
            NoMatchingCardsError = error;
            NoMatchingCardsMessage = message;
        }
    }
}