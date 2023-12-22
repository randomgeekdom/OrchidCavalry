using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Domain
{
    public class Quest : Entity
    {
        public Quest(string title, int expiration, int numberOfTurns, int requiredNumberOfCharacters)
        {
            this.Title = title;
            this.Expiration = expiration;
            this.NumberOfTurns = numberOfTurns;
            this.RequiredNumberOfCharacters = requiredNumberOfCharacters;
        }

        public int Expiration { get; set; }
        public int NumberOfTurns { get; set; }
        public int RequiredNumberOfCharacters { get; set; }
        public required string Title { get; set; }

        public List<Character> Characters { get; set; } = new List<Character>();

        public void AddCharacter(Character character)
        {
            if(Characters.Count >= RequiredNumberOfCharacters)
            {
                return;
            }

            Characters.Add(character);
        }
    }
}