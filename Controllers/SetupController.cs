using magick.Models;
using magick.Models.Forms;
using magick.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace magick.Controllers
{
    public class SetupController
    {
        private readonly CardService _cardService;
        private readonly UserService _userService;
        private List<Set> _sets = new List<Set>();

        public SetupController(CardService cardService, UserService userService)
        {
            _cardService = cardService;
            _userService = userService;
        }

        public async Task<User?> GetUser()
        {
            return await _userService.GetUser();
        }

        public async Task FilterSets(SetQuery query)
        {
            _sets = await _cardService.GetSets(query.Text);
        }

        public List<Set> GetSets()
        {
            return _sets;
        }
    }
}