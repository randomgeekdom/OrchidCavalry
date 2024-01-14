using OrchidCavalry.Models;
using OrchidCavalry.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        public CharacterViewModel(Character character)
        {
            this.Character = character;
        }

        public Character Character { get; }
        public bool HasSkills => this.Skills.Any();
        public string IsDeployed => this.Character.IsDeployed ? "Yes" : "No";
        public string Name => this.Character.GetNameAndRank();
        public IEnumerable<CharacterSkill> Skills => this.Character.Skills.OrderByDescending(x => x.Value).ThenBy(x => x.Skill.ToString());
        public IEnumerable<string> Titles => this.Character.Titles;
    }
}