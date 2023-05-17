using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BattleShipConsole
{
    public class Settings
    {
        public int Size { get; set; }
        public bool hitsAllowed { get; set; }
        public bool computerShipsVisible { get; set; }
        public int numberShips { get; set; }

        public Settings() 
        {
            ReadingSettings();
        }

        public void ReadingSettings()
        {
            string str = File.ReadAllText("gamesettings.txt");
            string[] mas = str.Split(',');
            Size = Convert.ToInt32(mas[0]);
            hitsAllowed = Convert.ToBoolean(mas[1]);
            computerShipsVisible = Convert.ToBoolean(mas[2]);
            numberShips = Convert.ToInt32(mas[3]);
        }
    }
}
