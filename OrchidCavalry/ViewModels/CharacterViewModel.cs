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
            this.Titles = this.Character.GetAllTitles();
            this.Name = this.Character.GetName();
        }

        public Game Game { get; }
        public string Name { get; }
        public IEnumerable<string> Titles { get; }
    }
}
