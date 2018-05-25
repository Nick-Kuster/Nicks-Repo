using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.View
{
    public class GameIO
    {
        public int WelcomeScreen()
        {
            Console.WriteLine("Welcome to Uno");
            var valid = false;
            var numPlayers =
            ChooseNumberBetween("How Many Players would you like?", 0, 5);
            Console.WriteLine("You chose " + numPlayers + " Players");
            Console.WriteLine("Let's Begin!");
            return numPlayers;
        }
        public string GetPlayerName(int playerNumber)
        {
            playerNumber++;
            Console.WriteLine("What is Player " + playerNumber + "'s name?");
            return Console.ReadLine();
        }
        public void PrintPlayerList(List<Player> players)
        {
            Console.WriteLine("List of players");
            Console.WriteLine("---------------");
            var i = 1;
            foreach (Player p in players)
            {
                Console.WriteLine("Player " + i + " : " + p.PlayerName);
                i++;
            }

            Console.ReadKey();
        }
        public void PrintPlayerTurn(Player player, Game game)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("     *****It is now " + player.PlayerName + "'s turn****");
            Console.WriteLine("Discard: " + game.DiscardPile.cards.Count 
                                          + " Deck: " + game.Deck.Cards.Count
                                          + " Direction: " + game.Direction);
            Console.WriteLine("===============================================");
        }
        public void PrintTopDiscardCard(Card card, bool isWild = false, string wildColor = "" )
        {
            Console.WriteLine("Top Card: ");
            if (isWild)
            {
                Console.WriteLine("|" + wildColor + "_" + card.Face + "|");

            }
            Console.WriteLine("|" + card.Color + "_" + card.Face + "|");
        }
        public void PrintPlayerHand(Hand hand)
        {
            Console.WriteLine("Your Hand: ");
            var i = 1;
            foreach (var card in hand.Cards)
            {
                Console.Write("|" + i + ": " + card.Color + "_" + card.Face + "|");
                if (i % 5 == 0 && i != 0)
                {
                    Console.WriteLine("");
                }
                i++;
            }
        }
        public long ChooseWildCardColor()
        {
            Console.WriteLine("Player 1, Please Choose a Color: ");
            Console.WriteLine("1: Red 2: Green 3: Yellow 4: Blue");
            var choice = ChooseNumberBetween("Choose from the above options ", 1, 4);
            switch (choice)
            {
                case 1:
                    return Convert.ToInt64(Color.RED);
                case 2:
                    return Convert.ToInt64(Color.GREEN);
                case 3:
                    return Convert.ToInt64(Color.YELLOW);
                case 4:
                    return Convert.ToInt64(Color.BLUE);
                default:
                    throw new ArgumentException("Oops something went wrong. Shouldn't be this far");
            }
        }
        public int GetPlayerCardChoice(Hand hand)
        {
            Console.WriteLine();
            return 
                ChooseNumberBetween(
                    "Please Enter Number of Card you want to play./n" +
                    "Enter 0 to draw a card.", 0, hand.Cards.Count);
        }
        public void InvalidCardMessage()
        {
            Console.WriteLine("Sorry, you cannot play this card.");
        }
        public UnoCaller UnoMessage()
        {
            Console.WriteLine("You have one card left... UNO!!!");
            Console.WriteLine("Did you call it?");
            Console.WriteLine("Type 'yes' or 'no'");
            var answer = Console.ReadLine();
            switch (answer.ToLower())
            {
                case "yes":
                    return UnoCaller.SELF;
                case "no":
                    break;
                default:
                    throw new ArgumentException("Please type Yes or No");
            }
            Console.WriteLine("Did Someone Else Call It?");
            Console.WriteLine("Type 'yes' or 'no'");
            var answer2 = Console.ReadLine();
            switch (answer2.ToLower())
            {
                case "yes":
                    break;
                case "no":
                    return UnoCaller.NOBODY;
                default:
                    throw new ArgumentException("Please type Yes or No");
            }
            Console.WriteLine("Did Someone else call Uno incorrectly?");
            Console.WriteLine("Type 'yes' or 'no'");
            var answer3 = Console.ReadLine();
            switch (answer3.ToLower())
            {
                case "yes":
                    return UnoCaller.OTHER_PLAYER_INCORRECT;
                case "no":
                    return UnoCaller.OTHER_PLAYER;
                default:
                    throw new ArgumentException("Please type Yes or No");
            }

        }
        public void UnoPenaltyForSelf()
        {
            Console.WriteLine("Sorry, you draw 2 cards for someone else calling it.");
        }
        public Player UnoPenaltyForOther(List<Player> players)
        {
            Console.WriteLine("Which player called it incorrectly?");
            int i = 1;
            foreach (Player p in players)
            {
                Console.WriteLine(i + ": " + p.PlayerName);
                i++;
            }

            var message = "Please enter the corresponding number of the player.";
            int playerNumber = ChooseNumberBetween(message, 1, players.Count);
            return players[playerNumber - 1];
        }
        public void UnoNoPenalty()
        {
            Console.WriteLine("OK, enjoy your UNO!!");
        }
        public int GameOver(Game g)
        {
            Console.WriteLine(g.Winner.PlayerName + " is the Winner!!!");
            Console.WriteLine("Adding up all remaining cards, " + g.Winner.PlayerName + "'s score is: ");
            Console.WriteLine(g.WinnerScore);
            var message = "Enter 1 to play again or 2 to Exit";
            return ChooseNumberBetween(message, 1, 2);
        }

        public void DeckOutMessage(Player p)
        {
            Console.WriteLine("Uh oh, it appears the Deck is out of cards");
            Console.WriteLine(p.PlayerName + "'s hand will be shuffled into deck and removed from game.");
        }
        private int ChooseNumberBetween(string message, int min, int max)
        {
            var valid = false;
            var numPlayers = 0;
            while (!valid)
            {
                Console.WriteLine(message);
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out numPlayers))
                {
                    if (numPlayers < min || numPlayers > max)
                    {
                        Console.WriteLine("Please enter a number between " + min + " and " + max);
                    }
                    else
                    {
                        valid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }

            return numPlayers;
        }

        public enum UnoCaller
        {
            SELF,
            OTHER_PLAYER,
            OTHER_PLAYER_INCORRECT,
            NOBODY
        }

        
    }
}
