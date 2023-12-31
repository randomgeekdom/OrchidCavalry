using OrchidCavalry.Models;

namespace OrchidCavalry.Domain.Quests
{
    public class QuestCharacterSlot(bool isMandatory)
    {
        public Character? Character { get; set; }
        public bool IsMandatory { get; set; } = isMandatory;

        public void AssignCharacter(Character character)
        {
            UnassignCharacter();
            Character = character;
            character.IsDeployed = true;
        }

        public override string ToString()
        {
            return this.Character?.ToString() ?? "None Selected";
        }

        public void UnassignCharacter()
        {
            if (this.Character != null)
            {
                this.Character.IsDeployed = false;
                this.Character = null;
            }
        }
    }
}