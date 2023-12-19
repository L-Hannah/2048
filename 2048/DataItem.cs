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
        private int x;
        private int y;
        private DataItem? left;
        private DataItem? right;
        private DataItem? above;
        private DataItem? below;
        private bool moved = false;
        public DataItem(int x, int y, int number)
        {
            this.num = number;
            this.x = x;
            this.y = y;
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
        public int X
        {
            get => x;
            set { x = value; }
        }
        public int Y
        {
            get => y;
            set { y = value; }
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
