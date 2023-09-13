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


        public CharacterViewModel(Character character)
        {
            this.Character = character;
            this.Titles = this.Character.Titles;
            this.Name = this.Character.GetNameAndRank();
        }

        public string Name { get; }
        public IEnumerable<string> Titles { get; }
    }
}
