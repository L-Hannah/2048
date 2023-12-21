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
        private DataItem? left;
        private DataItem? right;
        private DataItem? above;
        private DataItem? below;
        private bool moved = false;
        public DataItem(int number)
        {
            this.num = number;
        }
        public int Num {
            get => num;
            set { num = value; }
        }
        public bool Moved
        {
            get => moved;
            set { moved = value; }
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
