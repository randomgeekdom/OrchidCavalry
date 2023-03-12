using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.ViewModels
{
    public class NewGameViewModel : INotifyPropertyChanged
    {
        private string characterName;

        public string CharacterName
        {
            get => characterName;
            set
            {
                characterName = value;
                OnPropertyChanged(string.Empty);
            }
        }

        public bool CanStart => !string.IsNullOrWhiteSpace(this.CharacterName);
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
