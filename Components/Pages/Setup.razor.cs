using magick.Models.Forms;
using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Setup {
    [Inject] public CardService? CardService { get; set; }
    [SupplyParameterFromForm] public SetQuery Query { get; set; } = new();

    private List<Set> sets = [];
    

    protected override void OnInitialized()
        => FilterSets();

    private void FilterSets()
    {
        sets = CardService!.GetSets(Query.Text);
    }
}