using OrchidCavalry.Domain;
using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Popups;

public partial class ChoiceView : ContentPage
{
    public ChoiceView()
    {
        InitializeComponent();
    }

    internal void LoadViewModel(Choice choice, Action<Guid> resultAction)
    {
        this.BindingContext = new ChoiceViewModel(choice, resultAction);
    }
}