using OrchidCavalry.Domain;
using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;
using System.Collections.ObjectModel;

namespace OrchidCavalry.ViewModels
{
    public class QuestViewModel : ViewModelBase
    {
        public QuestViewModel(Quest quest, Game game, Action<int> resultAction)
        {
            this.Quest = quest;
            this.Game = game;
            this.ResultAction = resultAction;
        }

        public ObservableCollection<ChoiceOptionViewModel> Options { get; }
        public Quest Quest { get; }
        public Game Game { get; }
        public Action<int> ResultAction { get; }
        public Microsoft.Maui.Controls.Command SelectCommand => new(() => ResultAction(SelectedOption.Id));
        public ChoiceOptionViewModel SelectedOption { get; set; }
        public string Text => this.Quest.Description;
    }
}