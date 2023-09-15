using OrchidCavalry.Domain;
using OrchidCavalry.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class ChoiceViewModel: ViewModelBase
    {
        public Choice Choice { get; }


        public ChoiceViewModel(Choice choice)
        {
            this.Choice = choice;
            this.Options = new ObservableCollection<ChoiceOptionViewModel>(choice.Options.Select(x => new ChoiceOptionViewModel { Id = x.Key, Text = x.Value }));
        }

        public string Text => this.Choice.Value;
        public ObservableCollection<ChoiceOptionViewModel> Options { get; }
    }
}
