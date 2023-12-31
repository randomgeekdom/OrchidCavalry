using OrchidCavalry.Domain.Quests;
using OrchidCavalry.Models;
using OrchidCavalry.Services;
using OrchidCavalry.ViewModels;

namespace OrchidCavalry.Views;

public partial class QuestView : ContentPage
{
	public QuestView()
	{
		InitializeComponent();
    }

    internal void LoadViewModel(Quest choice, Game game, IChoicePopupService choicePopupService)
    {
        this.BindingContext = new QuestViewModel(choice, game, choicePopupService, this.Navigation);
    }
}