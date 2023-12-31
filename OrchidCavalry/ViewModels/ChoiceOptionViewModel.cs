namespace OrchidCavalry.ViewModels
{
    public class ChoiceOptionViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}