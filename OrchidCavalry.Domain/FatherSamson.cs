using OrchidCavalry.Domain.Enumerations;

namespace OrchidCavalry.Domain
{
    /// <summary>
    /// Father Samson joins once per game when the Order of Fire's reputation is high enough
    /// </summary>
    public static class FatherSamson
    {
        public const int RequiredReputation = 26;

        public static readonly Guid CharacterId = new("b2f1c6d8-7e43-4a19-8c0d-91e5a3d7f620");

        public static Character Create()
        {
            var character = new Character("Father", "Samson") { Id = CharacterId };

            character.AddTitle("Flame Father");
            character.UpdateCharacterSkillValue(Skill.Aura, 2);
            character.UpdateCharacterSkillValue(Skill.Brawn, 1);
            character.UpdateCharacterSkillValue(Skill.Influence, 1);
            character.UpdateCharacterSkillValue(Skill.Knowledge, 1);
            character.UpdateCharacterSkillValue(Skill.Target, 1);

            return character;
        }

        public static void JoinIfReady(Game game)
        {
            var orderOfFire = game.Factions.FirstOrDefault(x => x.Id == FixedFactions.OrderOfFireId);
            if (orderOfFire == null || orderOfFire.OrchidCavalryReputation < RequiredReputation || game.HasFatherSamsonJoined)
            {
                return;
            }

            game.HasFatherSamsonJoined = true;
            game.AddCharacter(Create());
            game.AddAlert("New Character Joined", "Father Samson of the Order of Fire has joined upon hearing of your dedication to the cause of Fire.");
        }
    }
}
