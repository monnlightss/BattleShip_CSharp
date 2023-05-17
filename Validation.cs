using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipConsole
{
    class Validation
    {
        public static bool isNameValid(string name)
        {
            return name.Length >= 3 && name.Length <= 15;
        }

        public static bool isPositionValid(int num, Settings settings)
        {
            return num >= 0 && num <= settings.Size - 1;
        }

        public static string CheckName()
        {
            string name = "";
            do
            {
                Console.WriteLine("shipName: ");
                name = Console.ReadLine();
                if (!Validation.isNameValid(name))
                    Console.WriteLine("The wrong length of the ship name");
            }
            while (!Validation.isNameValid(name));
            return name;
        }

        public static int CheckXposition (Settings settings)
        {
            int xPos = -1;
            do
            {
                try
                {
                    Console.WriteLine($"Ship x Position (0 - {settings.Size - 1})");
                    xPos = int.Parse(Console.ReadLine());
                    if (!Validation.isPositionValid(xPos, settings))
                        Console.WriteLine($"Ship x Position Must Be between 0 and {settings.Size - 1}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ship x Position Must Be Numeric");
                }
            }
            while (!Validation.isPositionValid(xPos, settings));
            return xPos;
        }

        public static int CheckYposition (Settings settings)
        {
            int yPos = -1;
            do
            {
                try
                {
                    Console.WriteLine($"Ship y Position (0 - {settings.Size - 1})");
                    yPos = int.Parse(Console.ReadLine());
                    if (!Validation.isPositionValid(yPos, settings))
                        Console.WriteLine($"Ship y Position Must Be between 0 and {settings.Size - 1}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ship y Position Must Be Numeric");
                }
            }
            while (!Validation.isPositionValid(yPos, settings));
            return yPos;
        }
    }
}
