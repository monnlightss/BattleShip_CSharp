using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipConsole
{
    class CoordinateGenerator
    {
        int minimumValue;
        int maximumValue;
        static Random rnd = new Random(DateTime.Now.Millisecond);

        public CoordinateGenerator(int minimumValue, int maximumValue)
        {
            this.minimumValue = minimumValue;
            this.maximumValue = maximumValue;
        }

        public int Generate()
        {
            return rnd.Next(this.minimumValue, this.maximumValue);
        }
    }
}
