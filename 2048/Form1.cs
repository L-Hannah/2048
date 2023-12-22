using System.Diagnostics.Metrics;
using System.Globalization;
using System;



namespace _2048
{
    public partial class Root : Form
    {
        List<List<DataItem>> data = new List<List<DataItem>>();
        Dictionary<int, Bitmap> ImageNames = new Dictionary<int, Bitmap> //Literally a dictionary of the project resources, aka the images
        {
            {0, Properties.Resources.blank},
            {2, Properties.Resources.two},
            {4, Properties.Resources.four},
            {8, Properties.Resources.eight},
            {16, Properties.Resources.sixteen},
            {32, Properties.Resources.thirtytwo},
            {64, Properties.Resources.sixtyfour},
            {128, Properties.Resources.onehundredtwentyeight},
            {256, Properties.Resources.twohundredfiftysix},
            {512, Properties.Resources.fivehundredtwelve},
            {1024, Properties.Resources.onethousandtwentyfour},
            {2048, Properties.Resources.twothousandfourtyeight }
        };
        public Root()
        {
            InitializeComponent();//Not my code
            KeyPreview = true;//I dont know why this is added I think it does nothing
        }
        private void ConstructData()
        {
            //This just makes the matrix have null items in, it makes a list of lists and sets each index (all 16 of them) to just null values.
            //The reason for this is because doing data[x][y] wouldn't work if there was no value there so setting them to null instantly avoids any index error
            for (int i = 0; i < 4; i++)
            {
                data.Add(
                new List<DataItem>()
                {
                null,
                null,
                null,
                null
                }
            );
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ConstructData(); //Literally makes 4 lists within the data array and sets their contents to null.
            CreateStartingData();//Accesses each data item within the array and sets their values to either blank, 2 or 4.
            MakeImages();//Creates the blank images
            ShowData();//Updates images
        }
        private void MakeImages()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j=0; j < 4; j++)
                {
                    PictureBox newBox = new() //New picturebox, options below
                    {
                        Name = i.ToString() + j.ToString(), //Sets a name for the buttons to be accessed with later
                        Image = Properties.Resources.blank, //Uses blank image by default
                        Location = new Point(i * 120 +13, j * 120 +13), //This was literally trial and error but it does work lmao
                        Size = new Size(112, 112) //Also trial and error
                    };
                    this.Controls.Add(newBox); //Adds it to the controls so the form includes it
                    newBox.BringToFront(); //Shows the picturebox above the main window
                }
            }
        }
        private void ShowData()
        {
            for (int i =0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox CurrentBox = (PictureBox)Controls[i.ToString() + j.ToString()]; //Gets picturebox using the i and j for the name
                    //Makes the image whatever value is within the number, so if 0 it uses blank (look at dictionary "ImageNames")
                    CurrentBox.Image = ImageNames[data[i][j].Num];
                }
            }
        }

        private void GridMove(string direction)
        {
            switch (direction)
            {
                case "up":
                    bool moved=true; //Initial true flag
                    while (moved)
                    {
                        moved = false; //Set to false instantly so if no move, the while statement does not iterate any further
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 3; j > -1; j--) //Uses a for loop that decreases to check the rows by going up
                            {
                                DataItem current = data[i][j]; //Gets current DataItem
                                if (current.Moved || j == 0 || current.Num == 0) { continue; } //Random checks to avoid bugs.
                                DataItem above = data[i][j - 1]; //Gets the DataItem above the current (Must be done after above statement otherwise index error)
                                if (above.Num == 0) //Number above is 0, slot is empty.
                                {
                                    //Simple swap, similar to bubble sort
                                    data[i][j - 1] = current;
                                    data[i][j] = above;
                                    moved = true;
                                }
                                else if (above.Num == current.Num) //Number above is equal, numbers should be merged
                                {
                                    data[i][j - 1].Num = current.Num * 2; //Multiplied by 2 as addition unnecessary
                                    data[i][j-1].Moved = true; //Set moved to true so no other merges in this move occur. This value should be changed after the move (LMFAO sure it will)
                                    data[i][j].Num = 0;//Set current to 0 as no longer a value there
                                    moved = true;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < 4; i++) // Loops for all collums on the matrix
                    {
                        for (int j = 0; j < 4; j++) // Loops for all Rows on the matrix
                        {
                            data[i][j].Moved = false; //does the impossible and allows the title to move agian (not clickbait)
                        }
                    }
                    ShowData(); //Update images
                    NewTwoOrFour(256);
                    break;
                case "down":
                    bool moveder = true; //Initial true flag
                    while (moveder)
                    {
                        moveder = false; //Set to false instantly so if no move, the while statement does not iterate any further
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 3; j++) //Uses a for loop that increases to check the rows by going down
                            {
                                DataItem current = data[i][j]; //Gets current DataItem
                                if (current.Moved || j == 4 || current.Num == 0) { continue; } //Random checks to avoid bugs.
                                DataItem below = data[i][j + 1]; //Gets the DataItem below the current (Must be done after above statement otherwise index error)
                                if (below.Num == 0) //Number above is 0, slot is empty.
                                {
                                    //Simple swap, similar to bubble sort
                                    data[i][j + 1] = current;
                                    data[i][j] = below;
                                    moveder = true;
                                }
                                else if (below.Num == current.Num) //Number above is equal, numbers should be merged
                                {
                                    data[i][j + 1].Num = current.Num * 2; //Multiplied by 2 as addition unnecessary
                                    data[i][j + 1].Moved = true; //Set moved to true so no other merges in this move occur. This value should be changed after the move (LMFAO sure it will)
                                    data[i][j].Num = 0;//Set current to 0 as no longer a value there
                                    moveder = true;
                                }
                            }
                        }
                    }
                    ShowData();
                    for (int i = 0; i < 4; i++) // Loops for all collums on the matrix
                    {
                        for (int j = 0; j < 4; j++) // Loops for all Rows on the matrix
                        {
                            data[i][j].Moved = false; //does the impossible and allows the title to move agian (not clickbait)
                        }
                    }
                    NewTwoOrFour(256);
                    ShowData();
                    break;
                case "left":
                    
                    NewTwoOrFour(0);
                    ShowData();
                    break;
                default:
                    break;
            }
        }
        private void CreateStartingData()
        {
            for (int i = 0; i < 4; i++) // Loops for all collums on the matrix
            {
                for (int j = 0; j < 4; j++) // Loops for all Rows on the matrix
                {
                    data[i][j] = new DataItem(0); //Makes a DataItem and sets the value to 0
                }
            }
            NewTwoOrFour(2); // Adds a number 2 to the grid, rigging it by putting (2)
            NewTwoOrFour(4); // Adds a number 2 to the grid, rigging it by putting (4)
        }
        private void NewTwoOrFour(int rigged) // Takes in a interger which will be the number placed on the grid, however if the number is 0 it will randomise between a 2 or 4
        {
            int twoorfour = 0; 
            var Rand = new Random(); //Built in random class
            if (rigged==0) // Checks if it is a random number we are adding or predetermied 
            {
                int twoorfourrandomiser = Rand.Next(1, 5); // random chance of it being a 4, rand is being weird so idfk if its a 1/4 chance but it looks right enough
                if (twoorfourrandomiser < 4)
                {
                    twoorfour = 2;
                }
                else
                {
                    twoorfour = 4;
                }
            }
            else
            {
                twoorfour = rigged; // If number is predetermied will set it as the rigged number
            }
            while (true) // Loops till it can find a empty space to add a number since it always adds one each turn
            {
                int wtfy = Rand.Next(0, 4); // random y position on the grid
                int wtfx = Rand.Next(0, 4); // random x posiiton on the grid
                if (data[wtfx][wtfy].Num == 0) // checks if the position selected is emepty or not, if not it just reloops and randomises more coords
                {
                    data[wtfx][wtfy] = new DataItem(twoorfour); // sets the value of the position to be equal to ethier the rigged value or random choice between 2 or 4
                    break; // break dance 
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int[] Arrows = { 37, 38, 39, 40 }; //Got these numbers through figuring it out
            if (!Arrows.Contains(e.KeyValue)) { return; } //If not an arrow key don't bother
            switch (e.KeyValue)
            {
                case (37):
                    //Left arrow
                    GridMove("left");
                    break;
                case (38):
                    //Up arrow
                    GridMove("up");
                    break;
                case (39):
                    //Right arrow
                    break;
                case (40):
                    //Down arrow
                    GridMove("down");
                    break;
                default:
                    //Literally shouldn't happen
                    break;
            }
        }
        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Quite unnecessary
            return;
            /*switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                    //e.IsInputKey = true;
                    break;
            }*/
            //Seems to work on new versions of .NET without needing this
        }
    }
}