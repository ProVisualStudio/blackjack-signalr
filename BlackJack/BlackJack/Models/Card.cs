namespace BlackJack.Models
{
    public class Card
    {
        public int rank { get; private set; }
        public string suit { get; private set; }
        public Card(int rank, string suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        
    }
}