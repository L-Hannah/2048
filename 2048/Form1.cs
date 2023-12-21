using System.Diagnostics.Metrics;

namespace _2048
{
    public partial class Root : Form
    {
        List<List<DataItem>> data = new List<List<DataItem>>();
        Dictionary<int, Bitmap> ImageNames = new Dictionary<int, Bitmap>
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
            InitializeComponent();
            KeyPreview = true;
        }
        private void ConstructData()
        {
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
                    PictureBox newBox = new()
                    {
                        Name = i.ToString() + j.ToString(),
                        Image = Properties.Resources.blank,
                        Location = new Point(i * 120 +13, j * 120 +13),
                        Size = new Size(112, 112)
                    };
                    this.Controls.Add(newBox);
                    newBox.BringToFront();
                }
            }
        }
        private void ShowData()
        {
            for (int i =0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox CurrentBox = (PictureBox)Controls[i.ToString() + j.ToString()];
                    CurrentBox.Image = ImageNames[data[i][j].Num];
                }
            }
        }
        private void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    for (int i = 0;i<4;i++)
                    {
                        for (int j=0; j<4 ; j++)
                        {
                            for (int x = 0; x < j; x++)
                            {
                                DataItem current = data[i][j - x];
                                if (current.Moved || j == 0 || current.Num == 0 || (j-x)==0) { continue; }
                                DataItem above = data[i][j - x -1];
                                MessageBox.Show("Current num: " + current.Num + " Above num: " + above.Num +" i: "+i+" j: "+j);
                                if (above.Num == 0)
                                {
                                    data[i][j - x] = current;
                                    data[i][j] = above;
                                }
                                else if (above.Num == current.Num)
                                {
                                    data[i][j - x].Num = current.Num * 2;
                                    data[i][j].Num = 0;
                                }
                            }
                            ShowData();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void CreateStartingData()
        {
            var Rand = new Random();
            int counter = 0;
            bool fourcounter = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Rand.Next(2) == 1 && counter < 2)
                    {
                        if (Rand.Next(2) == 1 && !fourcounter)
                        {
                            fourcounter = true;
                            counter++;
                            data[i][j] = new DataItem(4);
                        }
                        else
                        {
                            counter++;
                            data[i][j] = new DataItem(2);
                        }
                    }
                    else
                    {
                        data[i][j] = new DataItem(0);
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int[] Arrows = { 37, 38, 39, 40 };
            if (!Arrows.Contains(e.KeyValue)) { return; }

            //Loop below is used for debugging to show the data in a string format
            /*for (int i = 0; i<4 ; i++)
            {
                for (int j = 0; j < 4 ; j++)
                {
                    if (data[i][j]!=null) 
                    {
                        string test = "X: " + data[i][j].X.ToString() + " Y: " + data[i][j].Y.ToString() + "\nValue: "+data[i][j].Num.ToString();
                        MessageBox.Show(test); 
                    }
                }
            }*/
            switch (e.KeyValue)
            {
                case (37):
                    //Left arrow
                    break;
                case (38):
                    //Up arrow
                    Move("up");
                    break;
                case (39):
                    //Right arrow
                    break;
                case (40):
                    //Down arrow
                    break;
                default:
                    //Literally shouldn't happen
                    break;
            }
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            //This function should access the multidimensional array and then use this to update the image widgets
            for (int i=0;i<4;i++)
            {
                for (int j=0;j<4;j++) 
                {
                    
                }
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
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