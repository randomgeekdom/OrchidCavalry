using OrchidCavalry.Domain;
using System.Collections.ObjectModel;

namespace OrchidCavalry.ViewModels
{
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(Choice choice, Action<Guid> resultAction)
        {
            this.Choice = choice;
            this.ResultAction = resultAction;
            this.Options = new ObservableCollection<ChoiceOptionViewModel>(choice.Options.Select(x => new ChoiceOptionViewModel { Id = x.Key, Text = x.Value }));
        }

        public Choice Choice { get; }
        public ObservableCollection<ChoiceOptionViewModel> Options { get; }
        public Action<Guid> ResultAction { get; }
        public Microsoft.Maui.Controls.Command SelectCommand => new(() => ResultAction(SelectedOption.Id));
        public ChoiceOptionViewModel SelectedOption { get; set; }
        public string Text => this.Choice.Value;
    }
}