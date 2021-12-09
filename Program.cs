using System;

namespace SimpleCasinoCliApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player() { Cash = 100, Name = "The player" };
            Casino casino = new Casino();

            while(player.Cash > 0)
            {
                player.WriteMyInfo();

                casino.RandomOdds();
                casino.PrintOdds();

                casino.StartBet(player);
            }

            casino.Defeat();
        }
    }

    class Casino
    {
        public double odds;
        Random random = new Random();
        public int pot;
        public int casinoBank;

        public void RandomOdds()
        {
            this.odds = Math.Round(random.NextDouble(), 2);
        }

        public void PrintOdds()
        {
            Console.WriteLine("A new round has started. your odds are " + odds);
        }

        public void StartBet(Player player)
        {

            string betAmount = AskForBetAmount();

            if( int.TryParse(betAmount, out int amount))
            {
                TakeMoney(player.GiveCash(amount));

                if(this.pot > 0)
                {
                    double playerDice = Math.Round(random.NextDouble(), 2);
                    Console.WriteLine(player.Name + " rolls a " + playerDice);

                    if (playerDice > odds)
                    {
                        int winnings = GiveMoneyToPlayer();
                        player.ReciveCash(winnings);
                        Console.WriteLine(player.Name + " Wins " + winnings);
                    }
                    else
                    {
                        Console.WriteLine("Bad luck, The Casino wins");
                        GiveMoneyToCasino();
                    }
                }
            }
            else 
            {
                Console.WriteLine("Please enter a valid number.");
                StartBet(player);
            }
        }

        public string AskForBetAmount()
        {
            Console.WriteLine("How much do you want to bet: ");
            return Console.ReadLine();
        }

        public void TakeMoney(int amount)
        {
            Console.WriteLine("The casino took you " + amount + " bet.");
            this.pot += amount * 2;
        }

        public int GiveMoneyToPlayer()
        {
            int winnings = this.pot;
            this.pot = 0;
            return winnings;
        }

        public void GiveMoneyToCasino()
        {
            int winnings = this.pot;
            this.pot = 0;
            this.casinoBank += winnings;
        }

        public void Defeat()
        {
            Console.WriteLine("The Casino always wins.");
        }
        
    }

    class Player
    {
        public string Name;
        public int Cash;

        public void WriteMyInfo()
        {
            Console.WriteLine(Name + " has: " + Cash + "bucks.");
        }

        public int GiveCash(int amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("The player can't give that amount, sorry");
                return 0;
            }
            else if(amount > Cash)
            {
                Console.WriteLine("The player does't have that amount to give.");
                return 0;
            }
            Cash -= amount;
            return amount;
        }

        public void ReciveCash(int amount)
        {
            if(amount < 0)
            {
                Console.WriteLine("Sorry, The player can't recive that amount.");
            }
            else
            {
                Cash += amount;
            }
        }
    }
}
