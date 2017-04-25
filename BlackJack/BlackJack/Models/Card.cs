namespace BlackJack.Models
{
    public class Card
    {
        public int Rank { get; private set; }
        public string Suit { get; private set; }
        public Card(int rank, string suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        
    }
}