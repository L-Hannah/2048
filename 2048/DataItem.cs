using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    internal class DataItem
    {
        private int num;
        private int[,] position;
        private DataItem? left;
        private DataItem? right;
        private DataItem? above;
        private DataItem? below;
        public DataItem(int[,] position, int number)
        {
            this.num = number;
            this.position = position;
        }
        public int Num {
            get => num;
            set { num = value; }
        }
        public int[,] Position
        {
            get => position;
            set { position = value; }
        }
        public DataItem Left
        {
            get => left;
            set { left = value; }
        }
        public DataItem Right
        {
            get => right;
            set { right = value; }
        }
        public DataItem Above
        {
            get => above;
            set { above = value; }
        }
        public DataItem Below
        {
            get => below;
            set { below = value; }
        }
    }
}
