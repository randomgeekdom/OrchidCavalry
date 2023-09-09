﻿using OrchidCavalry.Models;
using OrchidCavalry.Services;

namespace OrchidCavalry.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IGameSaver gameSaver;
        private Game game;

        public DashboardViewModel(IGameSaver gameSaver)
        {
            this.gameSaver = gameSaver;
        }

        public Game Game
        {
            get => this.game; 
            set
            {
                this.game = value;
                this.NotifyAll();
            }
        }

        public string PlayerCharacterName => $"Orchid Leader: {this.Game?.PlayerCharacter?.GetName()}";

        public void LoadGame(Game game)
        {
            this.Game = game;
        }
    }
}