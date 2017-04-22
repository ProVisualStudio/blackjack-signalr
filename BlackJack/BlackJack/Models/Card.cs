namespace BlackJack.Models
{
    public class Card
    {
        private int rank;
        private string suit;

        public Card(int rank, string suit)
        {
            this.rank = rank;
            this.suit = suit;
        }
    }
}