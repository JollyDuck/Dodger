using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dodger
{
    class MinionManager
    {
        private Minion[] minArray;
        private Minion currentMinion;
        public int minionsAlive;

        public MinionManager(int p)
        {
            minArray = new Minion[p];
            minionsAlive = p;
            for (int i = 0; i < p; i++)
            {
                minArray[i] = new Minion();
            }
        }

        public void moveMinion(int location)
        {
            if (minInMid())
            {
                currentMinion.position = location;
            }

        }


        private bool minInMid()
        {

            foreach (Minion element in minArray)
            {
                if (element.position == 1)
                {
                    currentMinion = element;
                    return true;
                }
            }

            return false;

        }


        public int minionsIn(int loc)
        {
            int count = 0;
            foreach (Minion element in minArray)
            {
                if (element.position == loc) count++;
            }
            return count;
        }



        internal void killMinionsIn(int p)
        {
            foreach (Minion element in minArray)
            {
                if (element.position == p)
                {
                    element.position = -1;
                    minionsAlive--;
                }
            }
        }

        internal void reset()
        {
            foreach (Minion element in minArray)
            {
                if (element.position != -1)
                {
                    element.position = 1;
                }
            }
        }
    }
}
