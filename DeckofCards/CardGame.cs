using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq.Expressions;


namespace DeckofCards
{

	public class Deck
	{

		//Initializes a deck of cards using a Stack
        public Deck()
        {
			Cards = new Stack<Card>();
			for (int i = 2; i < 15; i++)
			{
				Cards.Push(new Card() { CardNumber = CardNames[i], Suit = "Diamond", isFaceDown = true });
				Cards.Push(new Card() { CardNumber = CardNames[i], Suit = "Heart", isFaceDown = true });
				Cards.Push(new Card() { CardNumber = CardNames[i], Suit = "Club", isFaceDown = true });
				Cards.Push(new Card() { CardNumber = CardNames[i], Suit = "Spade", isFaceDown = true });
			}
		}

		//Gets Number of cards in deck
		public int Count
		{
			get
			{
				return Cards.Count;
			}
		}

		//Shuffles cards in deck
		public void Shuffle()
        {
			if (Count != 0||Count!=1)
			{
				Card[] temp = new Card[Count];
				temp = Cards.ToArray();
				Random rnd = new Random();
				Cards.Clear();
				for (int j = 0; j < 3; j++)
				{
					for (int i = 0; i < temp.Length; i++)
					{
						int swap = rnd.Next(0, temp.Length);
						Card tempcard = temp[swap];
						temp[swap] = temp[i];
						temp[i] = tempcard;
					}
				}
				for (int i = 0; i < temp.Length; i++)
				{
					Cards.Push(temp[i]);
				}
				Console.WriteLine("Deck Shuffled");
			}
            else
            {
				Console.WriteLine("Deck too small to shuffle");

			}

		}

		//Return top card of deck
		public Card DealOneCard()
        {
			
			return Cards.Pop();
			
        }

		//Add one card to deck
		public void AddOneCard(Card card)
        {
			Cards.Push(card);
        }
		Dictionary<int, string> CardNames = new Dictionary<int, string>()
		{ {2,"2" },
			{3,"3" },
			{4,"4"},
			{5,"5" },
			{6,"6" },
			{7,"7" },
			{8,"8" },
			{9,"9" },
			{10,"10" },
			{11,"Jack" },
			{12,"Queen" },
			{13,"King" },
			{14,"Ace" }
		};



		private Stack<Card> Cards;

	}

	public class Card
	{
		public string CardNumber { get; set; }
		public string Suit { get; set; }

		public bool isFaceDown { get; set; }
		
	}

	public class Player
	{
		public Player()
		{
			Hand = new List<Card>();
		}
		public void AddCard(Card card)
		{
			Hand.Add(card);
		}
		private List<Card> Hand;
		public int HandCount
		{
			get
			{
				return Hand.Count;
			}
		}
		public List<Card>GetHand()
        {
			return Hand;
        }
		public void Discard(Deck deck)
        {
			if(Hand.Count!=0)
            {
				Console.WriteLine("Current deck size is " + deck.Count);
				Console.WriteLine("Current hand size is " + Hand.Count);
				deck.AddOneCard(Hand[Hand.Count-1]);
				Hand.RemoveAt(Hand.Count - 1);
				Console.WriteLine("Card discarded to deck.");
				Console.WriteLine("Deck size is: " + deck.Count);
				Console.WriteLine("Current hand size" + Hand.Count);

			}
            else
            {
				Console.WriteLine("Hand is empty");
            }
			
        }
	}

    public class CardGame
    {
		static void DealPlayer(Deck deck, Player player)
		{
			if (deck.Count != 0)
			{
				player.AddCard(deck.DealOneCard());
				Console.WriteLine("Card dealt to player");
			}
			else
			{
				Console.WriteLine("The deck is empty");
			}
		}
		static void Main()
        {
			Deck deck = new Deck();
			Player player = new Player();
			int count = deck.Count;

			Console.WriteLine("Player hand should be empty. Verifying:");
			Console.WriteLine("Player hand count is: " + player.HandCount);
			Console.WriteLine("Attempting to discard empty hand. Should fail: ");
			player.Discard(deck);

			Console.WriteLine("Draining entire Deck");
			for( int i=0; i<count; i++)
            {
				DealPlayer(deck, player);
            }
			Console.WriteLine("Deck should be empty");
			count = deck.Count;
			Console.WriteLine(deck.Count);
			Console.WriteLine("Verifying can't deal while deck is empty");
			DealPlayer(deck, player);

			Console.WriteLine("Deck should be in player's hand. Verifying");
			int handcount = player.HandCount;
			Console.WriteLine("Hand size is: " + handcount);
			Console.WriteLine("Outputting all cards in deck:");
			for(int i=0; i<handcount; i++)
            {
				Card card = player.GetHand()[i];
				Console.WriteLine(card.CardNumber + " " + card.Suit);
            }
			Console.WriteLine("Putting Cards back in deck");
			for(int i=0; i<handcount; i++)
            {
				player.Discard(deck);
            }
			Console.WriteLine("Hand should be empty. Verifying: ");
			handcount = player.HandCount;
			Console.WriteLine("Hand size is: " + handcount);

			Console.WriteLine("Shuffling deck:");
			deck.Shuffle();
			deck.Shuffle();
			deck.Shuffle();
			Console.WriteLine("Outputting shuffled deck: ");
			count = deck.Count;
			for(int i=0; i<count; i++)
            {
				//Card card= deck.DealOneCard();
				//Console.WriteLine(card.CardNumber + " " + card.Suit);
				DealPlayer(deck, player);
            }
			foreach(var card in player.GetHand())
            {
				Console.WriteLine(card.CardNumber + " " + card.Suit);
            }

			Console.WriteLine("Putting all cards back in deck:");
			Console.WriteLine("Verrifying deck and card are valid after deal.");
			Console.WriteLine("Dealing 7 cards. Deck should be 45. Hand should be 7");

			handcount = player.HandCount;
			for(int i=0; i<handcount; i++)
            {
				player.Discard(deck);
            }
			deck.Shuffle();
			deck.Shuffle();
			deck.Shuffle();
			for(int i=0; i<7; i++)
            {
				DealPlayer(deck, player);
            }
			Console.WriteLine("Number of cards in deck is: " + deck.Count);
			Console.WriteLine("Number of cards in hand is: " + player.HandCount);


			
        }
    }

	


}
