using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipConsole
{
    public class ShipList
    {
        Ship[,] ships;
        Settings settings;
        int realCount;

        public ShipList(Settings settings)
        {
            this.settings = settings;
            this.ships = new Ship[settings.Size, settings.Size];
        }

        public void Generate()
        {
            this.realCount = this.settings.numberShips;
            for (int i = 0; i < settings.numberShips;)
            {
                string compName = "A" + new Random().Next(0, 20);
                int comPxPos = new CoordinateGenerator(0, this.settings.Size).Generate();
                int comPyPos = new CoordinateGenerator(0, this.settings.Size).Generate();
                int numberOfHitsNeeded = new CoordinateGenerator(1, 5).Generate();
                Ship compShip = new Ship(compName, comPxPos, comPyPos, 0, numberOfHitsNeeded, false);
                if (this.ships[comPxPos, comPyPos] == null)
                {
                    this.ships[comPxPos, comPyPos] = compShip;
                    i++;
                }
                else
                    Console.WriteLine("You can't put two ships on the same coordinates");
            }
        }

        public Ship AddShip(int ind1, int ind2)
        {
            if (this.ships[ind1, ind2] != null)
                return null;
            this.ships[ind1, ind2] = new Ship($"{ind1}{ind2}", ind1, ind2, 0, new CoordinateGenerator(1, 5).Generate(), true);
            this.realCount++;
            return this.ships[ind1, ind2];
        }

        public int Count
        {
            get
            {
                return this.realCount;
            }
        }
        
        public Ship this[int key1, int key2]
        {
            get
            {
                return this.ships[key1, key2];
            }
            set
            {
                this.ships[key1, key2] = value;
            }
        }        
    }
}
