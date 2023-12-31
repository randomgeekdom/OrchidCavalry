﻿using CommunityToolkit.Mvvm.Input;
using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;
using OrchidCavalry.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OrchidCavalry.ViewModels
{
    public class QuestViewModel : ViewModelBase
    {
        private readonly IChoicePopupService choicePopupService;
        private readonly INavigation navigation;

        public QuestViewModel(Quest quest, Game game, IChoicePopupService choicePopupService, INavigation navigation)
        {
            this.choicePopupService = choicePopupService;
            this.navigation = navigation;
            this.Quest = quest;
            this.Game = game;

            this.ChooseCharacterCommand = new AsyncRelayCommand<QuestCharacterSlot>(this.ChooseCharacterAsync);
        }

        public ObservableCollection<QuestCharacterSlot> CharacterSlots => new(this.Quest?.CharacterSlots ?? []);

        public ICommand ChooseCharacterCommand { get; }

        public Game Game { get; }

        public ObservableCollection<ChoiceOptionViewModel> Options { get; }

        public Quest Quest { get; }

        public ChoiceOptionViewModel SelectedOption { get; set; }

        public string Text => this.Quest.Description;

        public async Task ChooseCharacterAsync(QuestCharacterSlot questCharacterSlot)
        {
            var choice = new Choice("Please Select a character", this.Game.Characters.Where(x => !x.IsDeployed).ToDictionary(x => x.Id, y => y.GetNameAndRank()));

            await this.choicePopupService.ShowAsync(choice, x =>
            {
                var character = this.Game.Characters.First(c => c.Id == x);
                questCharacterSlot.AssignCharacter(character);
            }, this.navigation);
        }
    }
}