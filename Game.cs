using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BattleShipConsole
{
    class Game
    {
        ShipList playersShips;
        ShipList computerShips;
        Settings settings;

        public Game()
        {
            this.settings = new Settings();
            this.playersShips = new ShipList(this.settings);
            this.computerShips = new ShipList(this.settings);
        }

        public void Start()
        {
            string str1 = "+";
            string str2 = "|";
            for (int i = 0; i < 75; i++)
            {
                str1 += "=";
                str2 += " ";
            }
            str1 += "+";
            str2 += "|";
            Console.WriteLine(str1);
            Console.WriteLine(str2);
            string str3 = "|";
            for (int i = 0; i < 15; i++)
                str3 += " ";
            str3 += "Welcome to the Battleship Game -- With a Twist!!";
            for (int i = 0; i < 12; i++)
                str3 += " ";
            str3 += "|";
            Console.WriteLine(str3);
            Console.WriteLine(str2);
            Console.WriteLine(str1);
            Console.WriteLine("The game will use the grid size defined in the settings file");
            Console.WriteLine($"Playing grid size set as ({this.settings.Size} X {this.settings.Size})");
            Console.WriteLine($"Maximum number of ships allowed as {this.settings.numberShips}");
            Console.WriteLine($"Multiple hits allowed per ships set as {this.settings.hitsAllowed}");
            Console.Write("Computer Ships Visible: ");
            if (this.settings.computerShipsVisible)
                Console.Write("ON");
            else
                Console.Write("OFF");
            Console.WriteLine();
            Console.WriteLine("Loading player settings: ");
            this.playersShips.Generate();
            Console.WriteLine("Loading computer settings: ");
            this.computerShips.Generate();
            Console.WriteLine("Computer settings generated!");
            int roundNum = 1;
            int playerScore = 0;
            int computerScore = 0;
            int numDamagedPlayerShips = 0;
            int numDamagedComputerShips = 0;
            string str = "";
            while (true)
            {
                Console.WriteLine($"Beginning round {roundNum}");
                Console.WriteLine($"Player Score: {playerScore}");
                Console.WriteLine($"Computer Score: {computerScore}");
                Console.WriteLine("Displaying the Playing Grid");
                Console.WriteLine(this.playersShips);
                for (int i = 0; i < 35; i++)
                    Console.Write("-");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Displaying the Computer Grid");
                Console.WriteLine(this.computerShips);
                Console.WriteLine();
                Console.WriteLine("Player to make a guess");
                int PlayerxPos = Validation.CheckXposition(this.settings);
                int PlayeryPos = Validation.CheckYposition(this.settings);
                if (this.computerShips[PlayerxPos, PlayeryPos] != null)
                {
                    this.computerShips[PlayerxPos, PlayeryPos].HitMade();
                    if (this.computerShips[PlayerxPos, PlayeryPos].Damaged)
                    {
                        Console.WriteLine($"Unfortunately, the Computer Ship {this.computerShips[PlayerxPos, PlayeryPos].Name} has been destroyed");
                        numDamagedComputerShips++;
                        if (numDamagedComputerShips == this.settings.numberShips)
                        {
                            Console.WriteLine("Congratulations!!! Player Wins!!!!");
                            str += $"Player Wins. Final Score Player({playerScore}) and Computer({computerScore})";
                            File.WriteAllText("outcome.txt", str);
                            break;
                        }
                    }
                    else if (this.computerShips[PlayerxPos, PlayeryPos].Hit)
                        Console.WriteLine("PLAYER HITTTTTT!!!!");
                    playerScore += 10;
                }
                else
                    Console.WriteLine("PLAYER MISSSS!!!!");
                Console.WriteLine("Computer to make a guess");
                int CompxPos = new CoordinateGenerator(0, this.settings.Size - 1).Generate();
                Console.WriteLine($"Computer x guess: {CompxPos}");
                int CompyPos = new CoordinateGenerator(0, this.settings.Size - 1).Generate();
                Console.WriteLine($"Computer y guess: {CompyPos}");
                if (this.playersShips[CompxPos, CompyPos] != null)
                {
                    this.playersShips[CompxPos, CompyPos].HitMade();
                    if (this.playersShips[CompxPos, CompyPos].Damaged)
                    {
                        Console.WriteLine($"Unfortunately, the Player Ship {this.playersShips[CompxPos, CompyPos].Name} has been destroyed");
                        numDamagedPlayerShips++;
                        if (numDamagedPlayerShips == this.settings.numberShips)
                        {
                            Console.WriteLine("Congratulations!!! Computer Wins!!!!");
                            str += $"Computer Wins. Final Score Player({playerScore}) and Computer({computerScore})";
                            File.WriteAllText("outcome.txt", str);
                            break;
                        }                      
                    }
                    else if (this.playersShips[CompxPos, CompyPos].Hit)
                        Console.WriteLine("COMPUTER HITTTTTT!!!!");
                    computerScore += 10;
                }
                else
                    Console.WriteLine("COMPUTER MISSSSS!!!!");
                roundNum++;
            }
        }
    }
}
