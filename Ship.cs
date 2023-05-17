using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipConsole
{
    public class Ship
    {
        string shipName;
        int xPos;
        int yPos;
        int noOfHitsMade;
        int noOfHitsNeeded;
        bool isPlayer;
        Settings settings;

        public Ship(string shipName, int xPos, int yPos, int noOfHitsMade, int noOfHitsNeeded, bool isPlayer)
        {
            this.shipName = shipName;
            this.xPos = xPos;
            this.yPos = yPos;
            this.noOfHitsMade = noOfHitsMade;
            this.noOfHitsNeeded = noOfHitsNeeded;
            this.isPlayer = isPlayer;
            this.settings = new Settings();
        }

        public void HitMade()
        {
            this.noOfHitsMade++;
        }

        public bool Whole
        {
            get
            {
                return this.noOfHitsMade == 0;
            }
        }

        public bool Hit
        {
            get
            {
                return this.noOfHitsMade > 0 && this.noOfHitsMade < this.noOfHitsNeeded;
            }
        }

        public bool Damaged
        {
            get
            {
                return this.noOfHitsMade == this.noOfHitsNeeded;
            }
        }

        public string Name
        {
            get
            {
                return this.shipName;
            }
            set
            {
                this.shipName = value;
            }
        }

        public int xPosition
        {
            get
            {
                return this.xPos;
            }
            set
            {
                this.xPos = value;
            }
        }

        public int yPosition
        {
            get
            {
                return this.yPos;
            }
            set
            {
                this.yPos = value;
            }
        }

        public int NumberOfHitsMade
        {
            get
            {
                return this.noOfHitsMade;
            }
            set
            {
                this.noOfHitsMade = value;
            }
        }

        public int NumberOfHitsNeeded
        {
            get
            {
                return this.noOfHitsNeeded;
            }
            set
            {
                this.noOfHitsNeeded = value;
            }
        }

        public override string ToString()
        {
            if (!this.isPlayer && !this.settings.computerShipsVisible)
            {
                if (Hit)
                    return "X";
                if (Damaged)
                    return "D";
                return " ";
            }
            if (this.isPlayer || this.settings.computerShipsVisible)
            {
                if (Whole)
                    return "O";
            }
            if (Hit)
                return "X";
            if (Damaged)
                return "D";
            return "";
        }
    }
}
