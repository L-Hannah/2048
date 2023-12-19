using System.Diagnostics.Metrics;

namespace _2048
{
    public partial class Root : Form
    {
        List<List<DataItem>> data = new List<List<DataItem>>();
        int[,]? buttons = null;
        Dictionary<int, string> ImageNames = new Dictionary<int, string>
        {
            {2, "two"},
            {4, "four"},
            {8, "eight"},
            {16, "sixteen"},
            {32, "thirtytwo"},
            {64, "sixtyfour"},
            {128, "onehundredtwentyeight"},
            {256, "twohundredfiftysix"},
            {512, "fivehundredtwelve"},
            {1024, "onethousandtwentyfour"}
        };
        public Root()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i=0; i<4; i++) {
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
            int curX = 0;
            int curY = 0;
            data[curX][curY] = new DataItem(curX, curY,2);
            CreateStartingImages();
        }
        private void CreateStartingImages()
        {
            var Rand = new Random();
            int counter = 0;
            bool fourcounter = false;
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
                    if (Rand.Next(2) == 1 && counter < 2)
                    {
                        counter++;
                        newBox.Image = Properties.Resources.two;
                    } else if (Rand.Next(2) == 0 && counter < 2 && !fourcounter)
                    {
                        fourcounter = true;
                        counter++;
                        newBox.Image = Properties.Resources.four;
                    }
                    this.Controls.Add(newBox);
                    newBox.BringToFront();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int[] Arrows = { 37, 38, 39, 40 };
            if (!Arrows.Contains(e.KeyValue)) { return; }
            if (this.data!=null) { MessageBox.Show(this.data[1][1].ToString()); }
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