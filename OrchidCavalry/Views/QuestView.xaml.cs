using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class QuestView : ContentPage
{
	public QuestView()
	{
		InitializeComponent();
    }

    internal void LoadViewModel(Quest choice, Game game, Action<int> resultAction)
    {
        this.BindingContext = new QuestViewModel(choice, game, resultAction);
    }
}