using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    internal class DataItem
    {
        private int num; //Private attribute of the number value for the dataitem
        private bool moved = false; //Private boolean attribute for whether the piece has been moved (when it merges)
        public DataItem(int number)
        {
            num = number; //Sets the attribute to whatever number is passed in
        }
        public int Num
        {
            get => num; //When doing DataItem.Num in a different file, this will get the num value and return it
            set { num = value; } //When doing DataItem.Num=value in a different file, this will change the num value to the one given
        }
        public bool Moved
        {
            get => moved; //Same as Num
            set { moved = value; } //Same as num
        }
    }
}
