using OrchidCavalry.Models;

namespace OrchidCavalry.ViewModels
{
    public class CouncilMemberViewModel : ViewModelBase
    {
        private readonly CouncilMember councilMember;

        public CouncilMemberViewModel(CouncilMember councilMember, Action openCouncilPage)
        {
            this.councilMember = councilMember;
            this.OpenCouncilPage = openCouncilPage;
        }

        public string Name => this.councilMember.Character?.Name ?? "Select";
        public string Title => this.councilMember.CouncilPosition?.ToString();
        public Action OpenCouncilPage;
    }
}