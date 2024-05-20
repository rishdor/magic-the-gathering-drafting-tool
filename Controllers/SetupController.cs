// using magick.Services;
// using magick.Models;
// using Microsoft.AspNetCore.Components;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace magick.Controllers
// {
//     public class SetupController
//     {
//         private readonly CardService _cardService;
//         private readonly UserService _userService;
//         private readonly NavigationManager _navManager;
//         private List<Set> _sets = new List<Set>();

//         public SetupController(CardService cardService, UserService userService, NavigationManager navManager)
//         {
//             _cardService = cardService;
//             _userService = userService;
//             _navManager = navManager;
//         }

//         public async Task OnInitializedAsync()
//         {
//             var user = await _userService.GetUser();
//             if (user != null)
//             {
//                 await FilterSets();
//             }
//             else
//             {
//                 _navManager.NavigateTo("/login");
//             }
//         }

//         public async Task FilterSets(string queryText)
//         {
//             _sets = await _cardService.GetSets(queryText);
//         }

//         public List<Set> GetSetsList()
//         {
//             return _sets;
//         }

//         public Set GetSetByCode(string setCode)
//         {
//             return _sets.Find(set => set.Code == setCode)!;
//         }

//         public void NavigateToDraft(string setCode)
//         {
//             _navManager.NavigateTo($"/draft/{setCode}");
//         }
//     }
// }