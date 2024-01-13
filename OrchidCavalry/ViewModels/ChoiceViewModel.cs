using OrchidCavalry.Domain;
using System.Collections.ObjectModel;

namespace OrchidCavalry.ViewModels
{
    public class ChoiceViewModel : ViewModelBase
    {
        private readonly Action cancelAction;
        private ChoiceOptionViewModel selectedOption;

        public ChoiceViewModel(Choice choice, Action<Guid> resultAction, Action cancelAction)
        {
            this.Choice = choice;
            this.ResultAction = resultAction;
            this.cancelAction = cancelAction;
            this.Options = new ObservableCollection<ChoiceOptionViewModel>(choice.Options.Select(x => new ChoiceOptionViewModel { Id = x.Key, Text = x.Value }));
        }

        public Microsoft.Maui.Controls.Command CancelCommand => new(() => cancelAction());
        public Choice Choice { get; }
        public bool EnableSelect => this.SelectedOption != null;
        public ObservableCollection<ChoiceOptionViewModel> Options { get; }
        public Action<Guid> ResultAction { get; }
        public Microsoft.Maui.Controls.Command SelectCommand => new(() => ResultAction(SelectedOption.Id));
        public ChoiceOptionViewModel SelectedOption
        {
            get => selectedOption; set
            {
                selectedOption = value;
                this.OnPropertyChanged(nameof(SelectedOption));
                this.OnPropertyChanged(nameof(EnableSelect));
            }
        }
        public string Text => this.Choice.Value;
    }
}