using OrchidCavalry.Models;
using OrchidCavalry.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class NewGameViewModel : ViewModelBase
    {
        public NewGameViewModel(IGameSaver gameSaver)
        {
            this.gameSaver = gameSaver;
        }

        private string characterName;
        private readonly IGameSaver gameSaver;

        public string CharacterName
        {
            get => characterName;
            set
            {
                characterName = value;
                this.NotifyAll();
            }
        }

        public bool CanStart => !string.IsNullOrWhiteSpace(this.CharacterName);

        public void Start()
        {
            if (CanStart)
            {
                var startingCharacter = this.CharacterName.Trim();
                var game = new Game
                {
                    FirstName = startingCharacter,
                    LastName = "Orchid"
                };
                
                this.gameSaver.SaveGame(game);
            }
        }
    }
}
