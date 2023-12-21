using System.Diagnostics.Metrics;

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
            //The reason for this is because doing data[x][y] wouldn't work if there was no value there so setting them to null instantly avoids any index errors.
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
                            for (int j = 3; j > -1; j--) //Uses a  for loop that decreases to check the rows by going up
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
                                    data[i][j-1].Moved = true; //Set moved to true so no other merges in this move occur. This value should be changed after the move
                                    data[i][j].Num = 0;//Set current to 0 as no longer a value there
                                    moved = true;
                                }
                            }
                        }
                    }
                    ShowData(); //Update images
                    break;
                /*case "down": //(DOESNT CURRENTLY WORK WTF)
                    moved = true;
                    while (moved)
                    {
                        moved=false;
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                DataItem current = data[i][j];
                                if (current.Moved || j == 4 || current.Num == 0) { continue; }
                                DataItem below = data[i][j + 1];
                                if (below.Num==0)
                                {
                                    data[i][i + 1] = current;
                                    data[i][j + 1] = below;
                                    moved = true;
                                }
                                else if (below.Num== current.Num)
                                {
                                    data[i][j+1].Num=current.Num * 2;
                                    data[i][j+1].Moved = true;
                                    data[i][j].Num=0;
                                    moved = true;
                                }
                            }
                        }
                    }
                    ShowData();
                    break;*/
                default:
                    break;
            }
        }
        private void CreateStartingData()
        {
            var Rand = new Random(); //Built in random class
            int counter = 0; //Counter for counting number of starting data
            bool fourcounter = false; //Bool flag for fours as only one four at start is possible
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Rand.Next(2) == 1 && counter < 2) //If there arent two values yet, and if random value is 1, we do this
                    {
                        if (Rand.Next(2) == 1 && !fourcounter) //Gets another random value and checks if its 1 and if theres no four already
                        {
                            fourcounter = true; //Set flag to true as one four is placed
                            counter++; //Counter iterated
                            data[i][j] = new DataItem(4); //Makes a DataItem and sets the value to 4
                        }
                        else
                        {
                            counter++; //Counter iterated
                            data[i][j] = new DataItem(2); //Makes a DataItem and sets the value to 2
                        }
                    }
                    else
                    {
                        data[i][j] = new DataItem(0); //Makes a DataItem and sets the value to 0
                    }
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