using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using BlackJack.Models;

namespace BlackJack.Hubs
{
    public class GameHub : Hub
    {
        private static List<User> Users = new List<User>();
        private static Table table = new Table();
        private static Card[] cards;
        private static int qtaMazzi = 6;
        private static DeckHelper dh = new DeckHelper();
        private static Hand dealerH;
        private static Hand playerH;
        private Random rnd;
        private static bool isPlayerBust = false;
        private static bool isDealerBust = false;
        private static int playerScore;
        private static int dealerScore;

        public override Task OnConnected()
        {
            foreach (var u in Users)
            {
                Clients.Caller.newNickName(Context.ConnectionId, u.Nome);
            }
            User user = new User();
            user.ConnectionId = Context.ConnectionId;
            Users.Add(user);
            return base.OnConnected();
        }

        public void SetNickname(string name)
        {
            User me = (from u in Users
                       where u.ConnectionId == Context.ConnectionId
                       select u).First();
            me.Nome = name;
            Clients.All.newNickName(Context.ConnectionId, name);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User me = (from u in Users
                       where u.ConnectionId == Context.ConnectionId
                       select u).First();
            Users.Remove(me);
            Clients.All.updNickName();
            foreach (var u in Users)
            {
                Clients.All.newNickName(Context.ConnectionId, u.Nome);
            }
            return base.OnDisconnected(stopCalled);
        }

       public GameHub()
        {
            rnd = new Random();
        }
        
        /**
         * Metodo che prepara una nuova partita
         */       
        public void NewGame()
        {
            Clients.Caller.flushTable();
            cards = dh.CreateDeck(qtaMazzi);
            cards = cards.Skip(1).ToArray();
            cards =  dh.ShuffleCards(cards);
            dealerH = new Hand("Dealer");
            playerH = new Hand("Player");
            playerScore = 0;
            dealerScore = 0;
            playerH.AddCard(GetCard());
            playerH.AddCard(GetCard());
            dealerH.AddCard(GetCard());
            List<Card> cP = playerH.GetCards();
            List<Card> cD = dealerH.GetCards();
            for (int i = 0; i < cP.Count; i++)
            {
                //Clients.Caller.printCardP(cP[i].Rank.ToString()+" "+ cP[i].Suit.ToString() + "Val:" + dh.GetCardScore(cP[i]));
                Clients.Caller.printCardP("/Content/cards/" + cP[i].Rank.ToString() + "_of_" + cP[i].Suit.ToString() + ".png");
            }
            for (int i = 0; i < cD.Count; i++)
            {
                //Clients.Caller.printCardD(cD[i].Rank.ToString() + " " + cD[i].Suit.ToString() + "Val:" + dh.GetCardScore(cD[i]));
                Clients.Caller.printCardD("/Content/cards/" + cD[i].Rank.ToString() + "_of_" + cD[i].Suit.ToString() + ".png");
            }
            playerScore = playerH.HandScore();
            dealerScore = dealerH.HandScore();
            Clients.Caller.printPoints(dealerScore, playerScore);
        }
        /**
         * Metodo ce fa terminare una partita e ritorna il vincitore
         */
        public Hand EndGame()
        {
            if (isPlayerBust)
            {
                isPlayerBust = false;
                return dealerH;
            }
            else if (isDealerBust)
            {
                isDealerBust = false;
                return playerH;
            }
            else if(playerScore > dealerScore && playerScore <= 21)
            {
                return playerH;
            }
            else
            {
                return dealerH;
            }
        }

        public Card GetCard()
        {
            if(cards.Length < 1)
            { 
                cards = dh.ShuffleCards(cards);
            }
            Card c = cards[0];
            cards = cards.Skip(1).ToArray();
            return c;
        }

       
        public void Hit()
        {
            Card carta = GetCard();
            playerH.AddCard(carta);
            Clients.Caller.printCardP("/Content/cards/" + carta.Rank.ToString() + "_of_" + carta.Suit.ToString() + ".png");
            playerScore = playerScore + dh.GetCardScore(carta);
            Clients.Caller.printPoints(dealerScore, playerScore);
            if (playerScore > 21)
            {
                isPlayerBust = true;
                Clients.Caller.printWinner(EndGame().Utente.Nome);
            }            
        }

        //metodo che effettua il controllo se l'utente a fa terminare il turno al player
        public void Stay()
        {
            playerH.HandScore();
            DealerTurn();
            
        }
        /**
         *metodo che simula il più fedelmente possibile il "funzionamento" del dealer 
         * */
        public void DealerTurn() {
            if (dealerScore < 17)
            {
                Card carta = GetCard();
                dealerH.AddCard(carta);
                Clients.Caller.printCardD("/Content/cards/"+carta.Rank.ToString() + "_of_" + carta.Suit.ToString() + ".png");
                dealerScore = dealerScore + dh.GetCardScore(carta);
                Clients.Caller.printPoints(dealerScore, playerScore);
                if (dealerScore > 21)
                {
                    isDealerBust = true;
                    Clients.Caller.printWinner(EndGame().Utente.Nome);
                }
                else
                {
                    DealerTurn();
                }
            }
            else if(dealerScore > 20) 
            {
                Clients.Caller.printWinner(EndGame().Utente.Nome);
            }
            else
            {
                if (rnd.Next(0, 2) == 0)
                {
                    Card carta = GetCard();
                    dealerH.AddCard(carta);
                    Clients.Caller.printCardD("/Content/cards/" + carta.Rank.ToString() + "_of_" + carta.Suit.ToString() + ".png");
                    dealerScore = dealerScore + dh.GetCardScore(carta);
                    Clients.Caller.printPoints(dealerScore, playerScore);
                    if (dealerScore > 21)
                    {
                        isDealerBust = true;
                        Clients.Caller.printWinner(EndGame().Utente.Nome);
                    }
                    else
                    {
                        Clients.Caller.printWinner(EndGame().Utente.Nome);
                    }
                }
                else
                {
                    Clients.Caller.printWinner(EndGame().Utente.Nome);
                }
            }
            
        }

        //Metodo che controlla se nella manno si ha Sballato (Bust), in modo da far terminare il gioco
        public bool BustCheck(Hand h)
        {
            if(h.Score > 21)
            {
                EndGame();
                return true;
            }
            return false;
        }

    }
}