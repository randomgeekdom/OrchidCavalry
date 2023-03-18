using Mudless.NameGenerator;
using OrchidCavalry.Models;
using RandomNameGeneratorLibrary;

namespace OrchidCavalry.Services
{
    public class CharacterNameGenerator : ICharacterNameGenerator
    {
        private readonly IRandomizer randomizer;
        private readonly NameGenerator fantasyNameGenerator;
        private readonly PersonNameGenerator realNameGenerator;

        public CharacterNameGenerator(IRandomizer randomizer)
        {
            this.realNameGenerator = new PersonNameGenerator();
            this.fantasyNameGenerator = new NameGenerator();
            this.randomizer = randomizer;
        }

        public string GenerateFirstName(Gender gender)
        {
            if (this.randomizer.GetBool())
            {
                return this.fantasyNameGenerator.Generate();
            }
            else
            {
                return gender == Gender.Female
                    ? this.realNameGenerator.GenerateRandomFemaleFirstName()
                    : this.realNameGenerator.GenerateRandomMaleFirstName();
            }
        }

        public string GenerateLastName()
        {
            if (this.randomizer.GetBool())
            {
                return this.fantasyNameGenerator.Generate();
            }
            else
            {
                return this.realNameGenerator.GenerateRandomLastName();
            }
        }
    }
}