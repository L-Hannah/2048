using System.Diagnostics.Metrics;

namespace _2048
{
    public partial class Root : Form
    {
        List<List<DataItem>> data = new List<List<DataItem>>();
        int[,]? buttons = null;
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
            ConstructData();
            //int curX = 0;
            //int curY = 0;
            //How to add or edit a DataItem within the data array:
            //data[curX][curY] = new DataItem(curX, curY,2);
            CreateStartingData();
            MakeImages();
            ShowData();
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
                            data[i][j] = new DataItem(i, j, 4);
                        }
                        else
                        {
                            counter++;
                            data[i][j] = new DataItem(i, j, 2);
                        }
                    }
                    else
                    {
                        data[i][j] = new DataItem(i, j, 0);
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int[] Arrows = { 37, 38, 39, 40 };
            if (!Arrows.Contains(e.KeyValue)) { return; }
            //if (this.data!=null) { MessageBox.Show(this.data[1][1].ToString()); }
            for (int i = 0; i<4 ; i++)
            {
                for (int j = 0; j < 4 ; j++)
                {
                    if (data[i][j]!=null) 
                    {
                        string test = "X: " + data[i][j].X.ToString() + " Y: " + data[i][j].Y.ToString() + "\nValue: "+data[i][j].Num.ToString();
                        MessageBox.Show(test); 
                    }
                }
            }
            switch (e.KeyValue)
            {
                case (37):
                    //Left arrow
                    break;
                case (38):
                    //Up arrow
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