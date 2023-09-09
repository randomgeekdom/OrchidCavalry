using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class CharacterViewModel: ViewModelBase
    {
        public Character Character { get; }


        public CharacterViewModel(Character character, Game game)
        {
            this.Character = character;
            this.Game = game;
        }

        public Game Game { get; }
        public string Name => this.Character.GetName();
        public string Age => this.Character.GetAge().ToString();
        public IEnumerable<string> Titles => this.Character.GetAllTitles();
    }
}
