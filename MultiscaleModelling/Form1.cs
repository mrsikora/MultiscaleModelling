using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiscaleModelling
{
    public partial class Form1 : Form
    {
        Structure structure;
        Random rand = new Random();
        bool isProcessing = false;
        bool wasProcessed = false;
        bool boundaryColoringDone = false;
        Thread thread;
        List<Color> CAList = new List<Color>();
        List<Color> BoundaryList = new List<Color>();
        List<int> stateOfMCNeighbors = new List<int>();
        ///do random
        static List<Cell> notMCCheckedList = new List<Cell>();
        /// do random
        Dictionary<Color, int> matchState = new Dictionary<Color, int>();
        Color colorToOmit = new Color();
        Dictionary<int, int> checkState = new Dictionary<int, int>(); //do sprawdzenia ktorego state jest najwiecej

        public Form1()
        {
            InitializeComponent();
            //form = this;
            growthButton.Enabled = false;
            generateButton.Enabled = false;
            mcButton.Enabled = false;
            energyPictureBox.Visible = false;
            structureBox.Enabled = true;
            this.CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isProcessing = false;
            Environment.Exit(Environment.ExitCode);
        }

        class Structure
        {
            Cell[,] prevCells, currentCells;
            int structWidth, structHeight;

            public Structure(int widthUpDown, int heightUpDown)
            {
                structWidth = widthUpDown;
                structHeight = heightUpDown;

                prevCells = new Cell[structWidth, structHeight];
                currentCells = new Cell[structWidth, structHeight];

                for (int i = 0; i < structWidth; i++)
                {
                    for (int j = 0; j < structHeight; j++)
                    {
                        prevCells[i, j] = new Cell(i, j);
                        currentCells[i, j] = new Cell(i, j);
                    }
                }

            }

            public Cell[,] PrevCells { get => prevCells; set => prevCells = value; }
            public Cell[,] CurrentCells { get => currentCells; set => currentCells = value; }
            public int StructHeight { get => structHeight; set => structHeight = value; }
            public int StructWidth { get => structWidth; set => structWidth = value; }

            public Bitmap setColor(int structureBoxWidth, int structureBoxHeight)
            {
                float cellWidth = (float)structureBoxWidth / (float)structWidth;
                float cellHeight = (float)structureBoxHeight / (float)structHeight;
                float minCellSize = Math.Min(cellWidth, cellHeight);
                //ZMIANA
                //form.structureBox.Width = structureBoxWidth;
                //form.structureBox.Height = structureBoxHeight;
                Bitmap bm = new Bitmap(Convert.ToInt32(structWidth * minCellSize), Convert.ToInt32(structHeight * minCellSize));

                using (Graphics g = Graphics.FromImage(bm))
                {
                    for (int i = 0; i < structWidth; i++)
                    {
                        for (int j = 0; j < structHeight; j++)
                        {
                            g.FillRectangle(new SolidBrush(prevCells[i, j].Color), minCellSize * prevCells[i, j].X, minCellSize * prevCells[i, j].Y, minCellSize, minCellSize);
                        }
                    }
                }
                return bm;
            }

            public Bitmap setEnergyColor(int structureBoxWidth, int structureBoxHeight)
            {
                float cellWidth = (float)structureBoxWidth / (float)structWidth;
                float cellHeight = (float)structureBoxHeight / (float)structHeight;
                float minCellSize = Math.Min(cellWidth, cellHeight);
                //ZMIANA
                //form.structureBox.Width = structureBoxWidth;
                //form.structureBox.Height = structureBoxHeight;
                Bitmap bm = new Bitmap(Convert.ToInt32(structWidth * minCellSize), Convert.ToInt32(structHeight * minCellSize));

                using (Graphics g = Graphics.FromImage(bm))
                {
                    for (int i = 0; i < structWidth; i++)
                    {
                        for (int j = 0; j < structHeight; j++)
                        {
                            g.FillRectangle(new SolidBrush(prevCells[i, j].EnergyColor), minCellSize * prevCells[i, j].X, minCellSize * prevCells[i, j].Y, minCellSize, minCellSize);
                        }
                    }
                }
                return bm;
            }
        }

        class Cell
        {
            int x, y = 0;
            int state = 0;
            int energy = 0;
            Color color;
            Color energyColor;


            public Cell()
            {
                this.x = 0;
                this.y = 0;
                this.state = 0;
                this.energy = 0;
                this.color = Color.FromArgb(255, 255, 255);
                this.energyColor = Color.FromArgb(255, 255, 255);
            }

            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.state = 0;
                this.energy = 0;
                this.color = Color.FromArgb(255, 255, 255);
                this.energyColor = Color.FromArgb(255, 255, 255);
            }

            public Cell(int x, int y, Color color)
            {
                this.x = x;
                this.y = y;
                this.state = 0;
                this.energy = 0;
                this.color = color;
                this.energyColor = Color.FromArgb(255, 255, 255);
            }

            public Color Color { get => color; set => color = value; }
            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }
            public int State { get => state; set => state = value; }
            public int Energy { get => energy; set => energy = value; }
            public Color EnergyColor { get => energyColor; set => energyColor = value; }
        }

        /////// EXPORTS AND IMPORTS /////// 
        public void exportBmp()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = "untitled",
                Filter = "Bitmap (*.bmp)|*.bmp"
            };
            sfd.ShowDialog();
            string FilePath = sfd.FileName;
            try
            {
                //ZMIANA
                Bitmap bm = structure.setColor(structureBox.Width, structureBox.Height);
                //Bitmap bm = structure.setColor(structure.StructWidth, structure.StructHeight);
                // string FilePath = "F:\\IS\\Multiscale modelling\\microstructure.bmp";
                bm.Save(FilePath, ImageFormat.Bmp);
            }
            catch
            {
                MessageBox.Show("Error exporting .bmp file");
            }
            finally
            {
                MessageBox.Show("Successfully exported .bmp file");
            }
        }

        public void importBmp()
        {

            OpenFileDialog sfd = new OpenFileDialog();
            sfd.ShowDialog();
            string FilePath = sfd.FileName;
            try
            {
                // string FilePath = "F:\\IS\\Multiscale modelling\\microstructure.bmp";
                Bitmap bm = new Bitmap(FilePath);
                structureBox.Image = bm;
            }
            catch
            {
                MessageBox.Show("Error importing .bmp file");
            }

        }

        public void exportTxt()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = "untitled",
                Filter = "Text (*.txt)|*.txt"
            };
            sfd.ShowDialog();
            string FilePath = sfd.FileName;

            try
            {
                List<string> linesToWrite = new List<string>();
                linesToWrite.Add(structure.StructWidth + "," + structure.StructHeight);
                for (int i = 0; i < structure.StructWidth; i++)
                {
                    for (int j = 0; j < structure.StructHeight; j++)
                    {
                        StringBuilder line = new StringBuilder();
                        line.Append(structure.PrevCells[i, j].X);
                        line.Append(",");
                        line.Append(structure.PrevCells[i, j].Y);
                        line.Append(",");
                        line.Append(structure.PrevCells[i, j].Color.R);
                        line.Append(",");
                        line.Append(structure.PrevCells[i, j].Color.G);
                        line.Append(",");
                        line.Append(structure.PrevCells[i, j].Color.B);
                        linesToWrite.Add(line.ToString());
                    }
                }
                System.IO.File.WriteAllLines(@FilePath, linesToWrite.ToArray());
            }
            catch
            {
                MessageBox.Show("Error exporting .txt file");
            }
            finally
            {
                MessageBox.Show("Successfully exported .txt file");
            }
        }

        public void importTxt()
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.ShowDialog();
            string FilePath = sfd.FileName;
            try
            {
                string[] linesToRead = System.IO.File.ReadAllLines(FilePath);
                string[] imageToRead = new string[linesToRead.Length - 1];
                Array.Copy(linesToRead, 1, imageToRead, 0, linesToRead.Length - 1);
                string[] dimensions = linesToRead[0].Split(',');
                widthUpDown.Value = Convert.ToDecimal(dimensions[0]);
                heightUpDown.Value = Convert.ToDecimal(dimensions[1]);
                structure = new Structure(Convert.ToInt32(dimensions[0]), Convert.ToInt32(dimensions[0]));
                foreach (string line in imageToRead)
                {
                    string[] values = line.Split(',');
                    Color color = Color.FromArgb(Convert.ToInt32(values[2]), Convert.ToInt32(values[3]), Convert.ToInt32(values[4]));
                    structure.PrevCells[Convert.ToInt32(values[0]), Convert.ToInt32(values[1])].Color = color;
                }
                refreshStructureBox();
            }
            catch
            {
                MessageBox.Show("Error importing .txt file");
            }
        }
        ///////END OF EXPORTS AND IMPORTS /////// 

        private void refreshStructureBox()
        {
            //ZMIANA
            //structureBox.Image = structure.setColor(structure.StructWidth, structure.StructHeight);
            structureBox.Image = structure.setColor(structureBox.Width, structureBox.Height);
        }

        private void clearStructureBox(Cell[,] tab)
        {
            string caMethod = CABox.SelectedItem.ToString();

            for (int i = 0; i < structure.StructWidth; i++)
            {
                for (int j = 0; j < structure.StructHeight; j++)
                {
                    if (CAList.Count > 0 && boundaryColoringDone == false)
                    {
                        if (caMethod == "Substructure")
                        {
                            if (CAList.Count == 1)
                            {
                                colorToOmit = CAList.First();
                                if (tab[i, j].Color != colorToOmit)
                                    tab[i, j].Color = Color.FromArgb(255, 255, 255);
                            }
                            else
                            {
                                if (!CAList.Contains(tab[i, j].Color))
                                {
                                    tab[i, j].Color = Color.FromArgb(255, 255, 255);
                                }
                            }
                        }
                        else
                        {
                            colorToOmit = Color.FromArgb(255, 51, 204); //ustawia kolor na rozowy
                            if (CAList.Contains(tab[i, j].Color))
                                tab[i, j].Color = colorToOmit;
                            else
                                tab[i, j].Color = Color.FromArgb(255, 255, 255);
                        }
                    }
                    else if (boundaryColoringDone)
                    {
                        if (tab[i, j].Color != Color.FromArgb(0, 0, 0))
                        {
                            tab[i, j].Color = Color.FromArgb(255, 255, 255);
                        }
                    }
                    else
                        tab[i, j].Color = Color.FromArgb(255, 255, 255);
                }
            }
        }

        private void setRandomColor(Cell cell)
        {
            int r, g, b, wsp;
            do
            {
                wsp = rand.Next(1000);
                r = rand.Next(0, (256 + wsp) + 1) % 256;
                g = rand.Next(0, (512 + wsp) + 1) % 256;
                b = rand.Next(0, (1024 + wsp) + 1) % 256;
            } while (Color.FromArgb(r, g, b).Equals(Color.FromArgb(255, 255, 255)) || Color.FromArgb(r, g, b).Equals(Color.FromArgb(0, 0, 0)) || CAList.Contains(Color.FromArgb(r, g, b)));

            cell.Color = Color.FromArgb(r, g, b);
        }

        private void structureBox_Click(object sender, EventArgs e)  //pobranie współrządnych klikniętego punktu z structureBox, czyli od 0 do 500
        {
            // if (wasProcessed)
            // {
            MouseEventArgs me = (MouseEventArgs)e;
            int X = me.X * (int)widthUpDown.Value / structureBox.Width; // pozycja myszki x razy ilość kwadratów (szerokość, którą podaję) przez szerokość okna (500)
            int Y = me.Y * (int)heightUpDown.Value / structureBox.Height;

            //MessageBox.Show(string.Format("X: {0},Y: {1} ", X, Y));
            MessageBox.Show(string.Format("Selected {0}", structure.CurrentCells[X, Y].Color.ToString()));
            if (caRadioButton.Checked)
                CAList.Add(structure.CurrentCells[X, Y].Color);
            else
                BoundaryList.Add(structure.CurrentCells[X, Y].Color);
            // }
        }

        private void updatePrevCells()
        {
            bool allNotWhite = true;

            for (int i = 0; i < structure.StructWidth; i++)
            {
                for (int j = 0; j < structure.StructHeight; j++)
                {
                    structure.PrevCells[i, j].Color = structure.CurrentCells[i, j].Color;
                    structure.PrevCells[i, j].EnergyColor = structure.CurrentCells[i, j].EnergyColor;
                    if (structure.PrevCells[i, j].Color.Equals(Color.FromArgb(255, 255, 255)))
                        allNotWhite = false;
                }
            }
            if (allNotWhite)
                isProcessing = false;
        }

        void growthMoore()
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            do
            {
                methodMoore(wUpDown, hUpDown);
                refreshStructureBox();
                Thread.Sleep(10);
            } while (isProcessing);
            MessageBox.Show("Growth finished");
            wasProcessed = true;
            CAList.Clear();
            BoundaryList.Clear();
        }

        void growth4rule()
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            do
            {
                method4Rule(wUpDown, hUpDown);
                refreshStructureBox();
                Thread.Sleep(10);
            } while (isProcessing);
            MessageBox.Show("Growth finished");
            wasProcessed = true;

        }

        private void setCircleInclusions()
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int numOfInclusions = (int)InclusionsUpDown.Value;
            int r = (int)InclusionsSizeUpDown.Value;
            int x, y, a, b, c, d, xprev, xnext, yprev, ynext = 0;

            for (int i = 0; i < numOfInclusions; i++)
            {
                do
                {
                    x = rand.Next(wUpDown);
                    y = rand.Next(hUpDown);
                    a = x + r;
                    b = y + r;
                    c = x - r;
                    d = y - r;

                } while (a > wUpDown - 1 || b > hUpDown - 1 || c <= 0 || d <= 0);

                if (x == 0)
                    xprev = wUpDown - 1;
                else
                    xprev = x - 1;

                if (x == wUpDown - 1)
                    xnext = 0;
                else
                    xnext = x + 1;

                if (y == 0)
                    yprev = hUpDown - 1;
                else
                    yprev = y - 1;

                if (y == hUpDown - 1)
                    ynext = 0;
                else
                    ynext = y + 1;

                Color color = structure.CurrentCells[x, y].Color;

                if (wasProcessed)
                {
                    if (structure.CurrentCells[xprev, yprev].Color != color || structure.CurrentCells[xprev, y].Color != color || structure.CurrentCells[xprev, ynext].Color != color || structure.CurrentCells[x, ynext].Color != color
                        || structure.CurrentCells[xnext, ynext].Color != color || structure.CurrentCells[xnext, y].Color != color || structure.CurrentCells[xnext, yprev].Color != color || structure.CurrentCells[x, yprev].Color != color)
                    {
                        for (int j = r; j > 0; j--)
                        {
                            r = j;
                            for (int k = 0; k < 360; k += 1)
                            {
                                double angle = k * System.Math.PI / 180;
                                int xx = (int)(x + r * System.Math.Cos(angle));
                                int yy = (int)(y + r * System.Math.Sin(angle));

                                structure.CurrentCells[xx, yy].Color = Color.FromArgb(0, 0, 0);
                            }
                        }
                        r = (int)InclusionsSizeUpDown.Value;
                    }
                    else
                        i--;
                }
                else
                {
                    for (int j = r; j > 0; j--)
                    {
                        r = j;
                        for (int k = 0; k < 360; k += 1)
                        {
                            double angle = k * System.Math.PI / 180;
                            int xx = (int)(x + r * System.Math.Cos(angle));
                            int yy = (int)(y + r * System.Math.Sin(angle));

                            structure.CurrentCells[xx, yy].Color = Color.FromArgb(0, 0, 0);
                        }
                    }
                    r = (int)InclusionsSizeUpDown.Value;
                }
            }

            updatePrevCells();
            refreshStructureBox();

        }

        private void setSquareInclusions()
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int numOfInclusions = (int)InclusionsUpDown.Value;
            int d = (int)InclusionsSizeUpDown.Value;
            int sizeOfInclusions = (int)Math.Round(d / 1.414); //rozmiar boku
            int xprev, xnext, yprev, ynext = 0;


            for (int i = 0; i < numOfInclusions; i++)
            {
                int x = rand.Next(wUpDown);
                int y = rand.Next(hUpDown);
                int a = x + sizeOfInclusions;
                int b = y + sizeOfInclusions;

                if (x == 0)
                    xprev = wUpDown - 1;
                else
                    xprev = x - 1;

                if (x == wUpDown - 1)
                    xnext = 0;
                else
                    xnext = x + 1;

                if (y == 0)
                    yprev = hUpDown - 1;
                else
                    yprev = y - 1;

                if (y == hUpDown - 1)
                    ynext = 0;
                else
                    ynext = y + 1;

                Color color = structure.CurrentCells[x, y].Color;
                if (wasProcessed)
                {
                    if (structure.CurrentCells[xprev, yprev].Color != color || structure.CurrentCells[xprev, y].Color != color || structure.CurrentCells[xprev, ynext].Color != color || structure.CurrentCells[x, ynext].Color != color
                        || structure.CurrentCells[xnext, ynext].Color != color || structure.CurrentCells[xnext, y].Color != color || structure.CurrentCells[xnext, yprev].Color != color || structure.CurrentCells[x, yprev].Color != color)
                    {
                        if (a < wUpDown && b < hUpDown)
                        {
                            for (int j = x; j < a; j++)
                            {
                                for (int k = y; k < b; k++)
                                {
                                    structure.CurrentCells[j, k].Color = Color.FromArgb(0, 0, 0);
                                }
                            }
                        }
                        else
                        {
                            for (int j = a; j > x; j--)
                            {
                                for (int k = b; k < y; k--)
                                {
                                    structure.CurrentCells[j, k].Color = Color.FromArgb(0, 0, 0);
                                }
                            }

                        }
                    }
                    else
                        i--;
                }
                else
                {
                    if (a < wUpDown && b < hUpDown)
                    {
                        for (int j = x; j < a; j++)
                        {
                            for (int k = y; k < b; k++)
                            {
                                structure.CurrentCells[j, k].Color = Color.FromArgb(0, 0, 0);
                            }
                        }
                    }
                    else
                    {
                        for (int j = a; j > x; j--)
                        {
                            for (int k = b; k < y; k--)
                            {
                                structure.CurrentCells[j, k].Color = Color.FromArgb(0, 0, 0);
                            }
                        }
                    }
                }
            }
            updatePrevCells();
            refreshStructureBox();
            growthButton.Enabled = true;

        }

        private void grainBoundaries()
        {
            int gbSize = (int)gbSizeUpDown.Value;
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int iprev, inext, jprev, jnext = 0;


            for (int i = 0; i < structure.StructWidth; i++)
            {
                for (int j = 0; j < structure.StructHeight; j++)
                {
                    if (i == 0)
                        iprev = wUpDown - 1;
                    else
                        iprev = i - 1;

                    if (i == wUpDown - 1)
                        inext = 0;
                    else
                        inext = i + 1;

                    if (j == 0)
                        jprev = hUpDown - 1;
                    else
                        jprev = j - 1;

                    if (j == hUpDown - 1)
                        jnext = 0;
                    else
                        jnext = j + 1;

                    int coordinateXPlusGbSize = i + gbSize;
                    int coordinateYPlusGbSize = j + gbSize;

                    Color color = structure.CurrentCells[i, j].Color;

                    if (BoundaryList.Count == 0)
                    {
                        if (wasProcessed && color != Color.FromArgb(0, 0, 0))
                        {
                            if (structure.CurrentCells[iprev, jprev].Color != color && structure.CurrentCells[iprev, jprev].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[iprev, j].Color != color && structure.CurrentCells[iprev, j].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[iprev, jnext].Color != color && structure.CurrentCells[iprev, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[i, jnext].Color != color && structure.CurrentCells[i, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, jnext].Color != color && structure.CurrentCells[inext, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, j].Color != color && structure.CurrentCells[inext, j].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, jprev].Color != color && structure.CurrentCells[inext, jprev].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[i, jprev].Color != color && structure.CurrentCells[i, jprev].Color != Color.FromArgb(0, 0, 0))
                            {
                                if (coordinateXPlusGbSize < wUpDown && coordinateYPlusGbSize < hUpDown)
                                    for (int k = i; k < coordinateXPlusGbSize; k++)
                                    {
                                        for (int l = j; l < coordinateYPlusGbSize; l++)
                                        {
                                            structure.CurrentCells[k, l].Color = Color.FromArgb(0, 0, 0);
                                        }
                                    }
                            }
                        }
                    }
                    else
                    {
                        if (wasProcessed && color != Color.FromArgb(0, 0, 0) && BoundaryList.Contains(color))
                        {
                            if (structure.CurrentCells[iprev, jprev].Color != color && structure.CurrentCells[iprev, jprev].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[iprev, j].Color != color && structure.CurrentCells[iprev, j].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[iprev, jnext].Color != color && structure.CurrentCells[iprev, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[i, jnext].Color != color && structure.CurrentCells[i, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, jnext].Color != color && structure.CurrentCells[inext, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, j].Color != color && structure.CurrentCells[inext, j].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, jprev].Color != color && structure.CurrentCells[inext, jprev].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[i, jprev].Color != color && structure.CurrentCells[i, jprev].Color != Color.FromArgb(0, 0, 0))
                            {
                                if (coordinateXPlusGbSize < wUpDown && coordinateYPlusGbSize < hUpDown)
                                    for (int k = i; k < coordinateXPlusGbSize; k++)
                                    {
                                        for (int l = j; l < coordinateYPlusGbSize; l++)
                                        {
                                            structure.CurrentCells[k, l].Color = Color.FromArgb(0, 0, 0);
                                        }
                                    }
                            }
                        }
                    }
                }
            }

            updatePrevCells();
            refreshStructureBox();
            growthButton.Enabled = true;
            boundaryColoringDone = true;

        }

        /////////// BUTTONS ///////////

        private void setButton_Click(object sender, EventArgs e)
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            structure = new Structure(wUpDown, hUpDown);
            refreshStructureBox();
            generateButton.Enabled = true;
            mcButton.Enabled = true;
            wasProcessed = false;
            CAList.Clear();
            BoundaryList.Clear();
            isProcessing = false;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                clearStructureBox(structure.CurrentCells);
                clearStates();
                int wUpDown = (int)widthUpDown.Value;
                int hUpDown = (int)heightUpDown.Value;
                int numOfGrains = (int)grainsUpDown.Value;

                for (int i = 0; i < numOfGrains; i++)
                {
                    int randX = rand.Next(wUpDown);
                    int randY = rand.Next(hUpDown);
                    if (structure.CurrentCells[randX, randY].Color.Equals(Color.FromArgb(255, 255, 255)))
                        setRandomColor(structure.CurrentCells[randX, randY]);
                    else
                        i--;
                }
                updatePrevCells();
                refreshStructureBox();
                growthButton.Enabled = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Please set the structure first");
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                string choose = ExpImpcomboBox.SelectedItem.ToString();

                if (choose == ".bmp")
                    importBmp();
                else if (choose == ".txt")
                    importTxt();
            }
            catch
            {
                MessageBox.Show("Please choose the method for import.");
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                string choose = ExpImpcomboBox.SelectedItem.ToString();
                if (choose == ".bmp")
                    exportBmp();
                else if (choose == ".txt")
                    exportTxt();
            }
            catch
            {
                MessageBox.Show("Please choose the method for export.");
            }
        }

        private void AddInclusionsButton_Click(object sender, EventArgs e)
        {
            try
            {
                string choose = TypeOfInclusionsComboBox.SelectedItem.ToString();
                if (choose == "circle")
                    setCircleInclusions();
                else if (choose == "square")
                    setSquareInclusions();
            }
            catch
            {
                MessageBox.Show("Please choose the method for inclusions.");
            }
        }

        private void growthButton_Click(object sender, EventArgs e)
        {
            isProcessing = true;
            // generateButton.Enabled = false;

            try
            {
                string choose = methodComboBox.SelectedItem.ToString();
                if (choose == "Moore")
                {
                    thread = new Thread(new ThreadStart(growthMoore));
                    thread.IsBackground = true;
                    thread.Start();
                }

                else if (choose == "4rule")
                {
                    thread = new Thread(new ThreadStart(growth4rule));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch
            {
                MessageBox.Show("Please choose the method.");
            }
            finally
            {
                // CAList.Clear();
                //   BoundaryList.Clear();
            }
        }

        private void boundariesColoringButton_Click(object sender, EventArgs e)
        {
            grainBoundaries();
        }

        private void ClearGrainsButton_Click(object sender, EventArgs e)
        {
            int numOfWhite = 0;
            for (int i = 0; i < structure.StructWidth; i++)
            {
                for (int j = 0; j < structure.StructHeight; j++)
                {
                    if (structure.CurrentCells[i, j].Color != Color.FromArgb(0, 0, 0))
                    {
                        structure.CurrentCells[i, j].Color = Color.FromArgb(255, 255, 255);
                        numOfWhite++;
                    }
                }
            }
            updatePrevCells();
            refreshStructureBox();

            double gbPercent = (double)(structure.CurrentCells.Length - numOfWhite) / (double)structure.CurrentCells.Length * 100.0;
            gbPercent = Math.Round(gbPercent, 2);
            gbLabel.Text = gbPercent.ToString() + "% of GB";
        }
        ///////////END OF BUTTONS ///////////

        public void methodMoore(int wUpDown, int hUpDown)
        {
            int iprev, inext, jprev, jnext = 0;

            for (int i = 0; i < wUpDown; i++)
            {
                if (i == 0)
                    iprev = wUpDown - 1;
                else
                    iprev = i - 1;

                if (i == wUpDown - 1)
                    inext = 0;
                else
                    inext = i + 1;

                for (int j = 0; j < hUpDown; j++)
                {

                    // czy ziarno jest "w srodku" (otoczone tym samym kolorem)-jego nie procesujemy
                    if (!structure.CurrentCells[i, j].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.CurrentCells[i, j].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[i, j].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[i, j].Color))
                    {

                        if (j == 0)
                            jprev = hUpDown - 1;
                        else
                            jprev = j - 1;

                        if (j == hUpDown - 1)
                            jnext = 0;
                        else
                            jnext = j + 1;
                        //mamy kolorowa komorke i j i odpalamy sprawdzanie zmiany koloru dla kazdego z jej sasiadow
                        changeMooreColor(iprev, jprev, wUpDown, hUpDown);
                        changeMooreColor(iprev, j, wUpDown, hUpDown);
                        changeMooreColor(iprev, jnext, wUpDown, hUpDown);
                        changeMooreColor(i, jnext, wUpDown, hUpDown);
                        changeMooreColor(inext, jnext, wUpDown, hUpDown);
                        changeMooreColor(inext, j, wUpDown, hUpDown);
                        changeMooreColor(inext, jprev, wUpDown, hUpDown);
                        changeMooreColor(i, jprev, wUpDown, hUpDown);
                    }
                }
            }
            //przepisuje z current do prev po wykonaniu step moore
            updatePrevCells();

        }

        void changeMooreColor(int x, int y, int wUpDown, int hUpDown)
        {
            if (structure.CurrentCells[x, y].Color.Equals(Color.FromArgb(255, 255, 255)))
            {
                int xprev, xnext, yprev, ynext = 0;
                Dictionary<Color, int> matchColor = new Dictionary<Color, int>();
                List<Color> neighbours = new List<Color>();

                if (x == 0)
                    xprev = wUpDown - 1;
                else
                    xprev = x - 1;

                if (x == wUpDown - 1)
                    xnext = 0;
                else
                    xnext = x + 1;

                if (y == 0)
                    yprev = hUpDown - 1;
                else
                    yprev = y - 1;

                if (y == hUpDown - 1)
                    ynext = 0;
                else
                    ynext = y + 1;

                if (!structure.PrevCells[xprev, y].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xprev, y].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xprev, y].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xprev, y].Color))
                {
                    neighbours.Add(structure.PrevCells[xprev, y].Color);
                }

                if (!structure.PrevCells[xprev, yprev].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xprev, yprev].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xprev, yprev].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xprev, yprev].Color))
                {
                    neighbours.Add(structure.PrevCells[xprev, yprev].Color);
                }

                if (!structure.PrevCells[x, yprev].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[x, yprev].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[x, yprev].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[x, yprev].Color))
                {
                    neighbours.Add(structure.PrevCells[x, yprev].Color);
                }

                if (!structure.PrevCells[xnext, yprev].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xnext, yprev].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xnext, yprev].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xnext, yprev].Color))
                {
                    neighbours.Add(structure.PrevCells[xnext, yprev].Color);
                }

                if (!structure.PrevCells[xnext, y].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xnext, y].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xnext, y].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xnext, y].Color))
                {
                    neighbours.Add(structure.PrevCells[xnext, y].Color);
                }

                if (!structure.PrevCells[xnext, ynext].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xnext, ynext].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xnext, ynext].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xnext, ynext].Color))
                {
                    neighbours.Add(structure.PrevCells[xnext, ynext].Color);
                }

                if (!structure.PrevCells[x, ynext].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[x, ynext].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[x, ynext].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[x, ynext].Color))
                {
                    neighbours.Add(structure.PrevCells[x, ynext].Color);
                }

                if (!structure.PrevCells[xprev, ynext].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xprev, ynext].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xprev, ynext].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xprev, ynext].Color))
                {
                    neighbours.Add(structure.PrevCells[xprev, ynext].Color);
                }
                //sprawdza jeszcze siebie samego
                if (!structure.PrevCells[x, y].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[x, y].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[x, y].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[x, y].Color))
                {
                    neighbours.Add(structure.PrevCells[x, y].Color);
                }
                //CHECK COLOR
                foreach (Color color in neighbours)
                {
                    if (!matchColor.ContainsKey(color))
                        matchColor.Add(color, 1);
                    else
                        matchColor[color] = matchColor[color] + 1;
                }

                Color maxKey = getMaxKey(matchColor);
                if (matchColor.Count != 0 && !structure.CurrentCells[x, y].Color.Equals(Color.FromArgb(0, 0, 0)))
                {
                    structure.CurrentCells[x, y].Color = maxKey;
                }
                matchColor.Clear();
            }
        }

        public void method4Rule(int wUpDown, int hUpDown)
        {
            int iprev, inext, jprev, jnext = 0;

            for (int i = 0; i < wUpDown; i++)
            {
                if (i == 0)
                    iprev = wUpDown - 1;
                else
                    iprev = i - 1;

                if (i == wUpDown - 1)
                    inext = 0;
                else
                    inext = i + 1;

                for (int j = 0; j < hUpDown; j++)
                {

                    // czy ziarno jest "w srodku" (otoczone tym samym kolorem)-jego nie procesujemy
                    if (!structure.CurrentCells[i, j].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.CurrentCells[i, j].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[i, j].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[i, j].Color))
                    {

                        if (j == 0)
                            jprev = hUpDown - 1;
                        else
                            jprev = j - 1;

                        if (j == hUpDown - 1)
                            jnext = 0;
                        else
                            jnext = j + 1;
                        //mamy kolorowa komorke i j i odpalamy sprawdzanie zmiany koloru dla kazdego z jej sasiadow
                        change4RuleColor(iprev, jprev, wUpDown, hUpDown);
                        change4RuleColor(iprev, j, wUpDown, hUpDown);
                        change4RuleColor(iprev, jnext, wUpDown, hUpDown);
                        change4RuleColor(i, jnext, wUpDown, hUpDown);
                        change4RuleColor(inext, jnext, wUpDown, hUpDown);
                        change4RuleColor(inext, j, wUpDown, hUpDown);
                        change4RuleColor(inext, jprev, wUpDown, hUpDown);
                        change4RuleColor(i, jprev, wUpDown, hUpDown);
                    }
                }
            }
            //przepisuje z current do prev po wykonaniu step moore
            updatePrevCells();
        }

        void change4RuleColor(int x, int y, int wUpDown, int hUpDown)
        {
            if (structure.CurrentCells[x, y].Color.Equals(Color.FromArgb(255, 255, 255)))
            {
                int xprev, xnext, yprev, ynext = 0;
                Dictionary<Color, int> matchAllColor = new Dictionary<Color, int>();
                Dictionary<Color, int> matchCloseColor = new Dictionary<Color, int>();
                Dictionary<Color, int> matchFarColor = new Dictionary<Color, int>();
                List<Color> neighbours = new List<Color>();
                List<Color> closeNeighbours = new List<Color>();
                List<Color> farNeighbours = new List<Color>();

                if (x == 0)
                    xprev = wUpDown - 1;
                else
                    xprev = x - 1;

                if (x == wUpDown - 1)
                    xnext = 0;
                else
                    xnext = x + 1;

                if (y == 0)
                    yprev = hUpDown - 1;
                else
                    yprev = y - 1;

                if (y == hUpDown - 1)
                    ynext = 0;
                else
                    ynext = y + 1;
                //sprawdzanie sąsiadów i dodawanie do listy
                if (!structure.PrevCells[xprev, y].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xprev, y].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xprev, y].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xprev, y].Color))
                {
                    neighbours.Add(structure.PrevCells[xprev, y].Color);
                    closeNeighbours.Add(structure.PrevCells[xprev, y].Color);
                }

                if (!structure.PrevCells[xprev, yprev].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xprev, yprev].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xprev, yprev].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xprev, yprev].Color))
                {
                    neighbours.Add(structure.PrevCells[xprev, yprev].Color);
                    farNeighbours.Add(structure.PrevCells[xprev, yprev].Color);
                }

                if (!structure.PrevCells[x, yprev].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[x, yprev].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[x, yprev].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[x, yprev].Color))
                {
                    neighbours.Add(structure.PrevCells[x, yprev].Color);
                    closeNeighbours.Add(structure.PrevCells[x, yprev].Color);
                }

                if (!structure.PrevCells[xnext, yprev].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xnext, yprev].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xnext, yprev].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xnext, yprev].Color))
                {
                    neighbours.Add(structure.PrevCells[xnext, yprev].Color);
                    farNeighbours.Add(structure.PrevCells[xnext, yprev].Color);
                }

                if (!structure.PrevCells[xnext, y].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xnext, y].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xnext, y].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xnext, y].Color))
                {
                    neighbours.Add(structure.PrevCells[xnext, y].Color);
                    closeNeighbours.Add(structure.PrevCells[xnext, y].Color);
                }

                if (!structure.PrevCells[xnext, ynext].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xnext, ynext].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xnext, ynext].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xnext, ynext].Color))
                {
                    neighbours.Add(structure.PrevCells[xnext, ynext].Color);
                    farNeighbours.Add(structure.PrevCells[xnext, ynext].Color);
                }

                if (!structure.PrevCells[x, ynext].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[x, ynext].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[x, ynext].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[x, ynext].Color))
                {
                    neighbours.Add(structure.PrevCells[x, ynext].Color);
                    closeNeighbours.Add(structure.PrevCells[x, ynext].Color);
                }

                if (!structure.PrevCells[xprev, ynext].Color.Equals(Color.FromArgb(255, 255, 255)) && !structure.PrevCells[xprev, ynext].Color.Equals(Color.FromArgb(0, 0, 0)) && !structure.CurrentCells[xprev, ynext].Color.Equals(colorToOmit) && !CAList.Contains(structure.CurrentCells[xprev, ynext].Color))
                {
                    neighbours.Add(structure.PrevCells[xprev, ynext].Color);
                    farNeighbours.Add(structure.PrevCells[xprev, ynext].Color);
                }

                //for rule1
                foreach (Color color in neighbours)
                {
                    if (!matchAllColor.ContainsKey(color))
                        matchAllColor.Add(color, 1);
                    else
                        matchAllColor[color] = matchAllColor[color] + 1;
                }

                Color maxKeyAllNeighbours = getMaxKey(matchAllColor); // klucz- kolor, value - liczba wystąpień koloru

                //for rule2
                foreach (Color color in closeNeighbours)
                {
                    if (!matchCloseColor.ContainsKey(color))
                        matchCloseColor.Add(color, 1);
                    else
                        matchCloseColor[color] = matchCloseColor[color] + 1;
                }

                Color maxKeyCloseNeighbours = getMaxKey(matchCloseColor); // klucz- kolor, value - liczba wystąpień koloru

                //for rule3
                foreach (Color color in farNeighbours)
                {
                    if (!matchFarColor.ContainsKey(color))
                        matchFarColor.Add(color, 1);
                    else
                        matchFarColor[color] = matchFarColor[color] + 1;
                }

                Color maxKeyFarNeighbours = getMaxKey(matchFarColor); // klucz- kolor, value - liczba wystąpień koloru

                //rule 1
                if (matchAllColor.Count != 0 && matchAllColor[maxKeyAllNeighbours] >= 5 && !structure.CurrentCells[x, y].Color.Equals(Color.FromArgb(0, 0, 0)))
                {
                    structure.CurrentCells[x, y].Color = maxKeyAllNeighbours;
                }
                //rule2
                else if (matchCloseColor.Count != 0 && matchCloseColor[maxKeyCloseNeighbours] >= 3 && !structure.CurrentCells[x, y].Color.Equals(Color.FromArgb(0, 0, 0)))
                {
                    structure.CurrentCells[x, y].Color = maxKeyCloseNeighbours;
                }
                //rule3
                else if (matchFarColor.Count != 0 && matchFarColor[maxKeyFarNeighbours] >= 3 && !structure.CurrentCells[x, y].Color.Equals(Color.FromArgb(0, 0, 0)))
                {
                    structure.CurrentCells[x, y].Color = maxKeyFarNeighbours;
                }
                //rule4
                else
                {
                    int posibility = (int)PosibilityUpDown.Value;
                    int random = rand.Next(1, 101);

                    if (random <= posibility)
                    {
                        changeMooreColor(x, y, wUpDown, hUpDown);
                    }
                }
            }
        }

        Color getMaxKey(Dictionary<Color, int> map)
        {
            int max = 0;

            foreach (var item in map)  // item - kolor i ilosc jego wystapien
            {
                if (item.Value > max)  //  szuka koloru, ktory wystapil najwiecej razy (item.Value - ilość wystąpień)
                    max = item.Value;
            }
            Color maxKey = map.FirstOrDefault(x => x.Value == max).Key;  //zwraca kolor(klucz), który wystąpił najwięcej razy

            return maxKey;
        }

        /// //////////////...............MONTE CARLO............///////////////////////

        private void mcButton_Click(object sender, EventArgs e)
        {
            generateMCstructure();
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;

            thread = new Thread(new ThreadStart(monteCarloMethod));
            thread.IsBackground = true;
            thread.Start();
        }

        public void generateMCstructure()
        {
            int numOfStates = (int)numOfStatesUpDown.Value;
            try
            {
                //clearStructureBox(structure.CurrentCells);
                clearStates();
                int wUpDown = (int)widthUpDown.Value;
                int hUpDown = (int)heightUpDown.Value;
                int addedState = 0;
                int temp = 0;
                Color colorToSet;
                Color tempColor;

                for (int i = 0; i < numOfStates; i++)   //tworzenie mapy state - color
                {
                    int r, g, b, wsp;

                    wsp = rand.Next(1000);
                    r = rand.Next(0, (256 + wsp) + 1) % 256;
                    g = rand.Next(0, (512 + wsp) + 1) % 256;
                    b = rand.Next(0, (1024 + wsp) + 1) % 256;
                    matchState.Add(Color.FromArgb(r, g, b), i);
                }

                for (int i = 0; i < wUpDown; i++)
                {
                    for (int j = 0; j < hUpDown; j++)
                    {
                        if (!CAList.Contains(structure.CurrentCells[i, j].Color) && structure.CurrentCells[i, j].Color != colorToOmit)
                        {

                            temp = rand.Next(numOfStates);
                            colorToSet = matchState.First(x => x.Value == temp).Key;

                            structure.CurrentCells[i, j].Color = colorToSet;
                            structure.CurrentCells[i, j].State = temp;
                        }
                        else if (matchState.ContainsKey(structure.CurrentCells[i, j].Color))
                        {
                            tempColor = structure.CurrentCells[i, j].Color;
                            structure.CurrentCells[i, j].State = matchState[tempColor];  //dopasuje state z listy do koloru 
                        }
                        else
                        {
                            addedState = matchState.Count + 1;
                            matchState.Add(structure.CurrentCells[i, j].Color, addedState);
                            structure.CurrentCells[i, j].State = addedState;
                        }

                    }
                }
                updatePrevCells();
                refreshStructureBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Najpierw ustaw ");
            }

        }

        void monteCarloMethod()
        {
            int steps = (int)mcUpDown.Value;
            int i = 0;
            do
            {
                monteCarlo();
                updatePrevCells();
                refreshStructureBox();
                Thread.Sleep(10);
                i++;
            } while (i < steps);
            CAList.Clear();
            BoundaryList.Clear();
        }

        public void monteCarlo()
        {
            // int iprev, inext, jprev, jnext, sum = 0;
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int numberOfStates = (int)numOfStatesUpDown.Value;
            double jgb = 1.0;
            double eBefore, eAfter, deltaE;

            ///////dodane do randa
            int randNumber = wUpDown * hUpDown;
            int xprev, xnext, yprev, ynext, sum = 0;

            for (int i = 0; i < wUpDown; i++)
            {
                for (int j = 0; j < hUpDown; j++)
                {
                    notMCCheckedList.Add(structure.CurrentCells[i, j]);
                }
            }

            do
            {
                Cell randCell = notMCCheckedList[rand.Next(randNumber)];
                int x = randCell.X;
                int y = randCell.Y;

                notMCCheckedList.Remove(randCell);
                randNumber--;

                if (x == 0)
                    xprev = wUpDown - 1;
                else
                    xprev = x - 1;

                if (x == wUpDown - 1)
                    xnext = 0;
                else
                    xnext = x + 1;

                if (y == 0)
                    yprev = hUpDown - 1;
                else
                    yprev = y - 1;

                if (y == hUpDown - 1)
                    ynext = 0;
                else
                    ynext = y + 1;

                //setState(wUpDown,hUpDown);
                Color color = structure.CurrentCells[x, y].Color;



                if (!CAList.Contains(color) && color != colorToOmit && (structure.CurrentCells[xprev, yprev].Color != color || structure.CurrentCells[xprev, y].Color != color
                || structure.CurrentCells[xprev, ynext].Color != color || structure.CurrentCells[x, ynext].Color != color
                || structure.CurrentCells[xnext, ynext].Color != color || structure.CurrentCells[xnext, y].Color != color
                || structure.CurrentCells[xnext, yprev].Color != color || structure.CurrentCells[x, yprev].Color != color)
                 )
                {

                    stateOfMCNeighbors.Add(structure.CurrentCells[xprev, y].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[xprev, yprev].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[x, yprev].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[xnext, yprev].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[xnext, y].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[xnext, ynext].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[x, ynext].State);

                    stateOfMCNeighbors.Add(structure.CurrentCells[xprev, ynext].State);


                    sum = toCalculateEnergy(x, y);
                    eBefore = jgb * sum;
                    int stateBefore = structure.CurrentCells[x, y].State;
                    int numOfNewState = rand.Next(stateOfMCNeighbors.Count);   //random z liczby elementow na liscie
                    int newState = stateOfMCNeighbors[numOfNewState];   //stan, ktory znajduje sie w wylosowanym elemencie listy
                    int state = 0;
                    Color newKey = matchState.First(s => s.Value == newState).Key;
                    while (CAList.Contains(newKey) || newKey == colorToOmit)
                    {
                        numOfNewState = rand.Next(stateOfMCNeighbors.Count);
                        newState = stateOfMCNeighbors[numOfNewState];
                        newKey = matchState.First(s => s.Value == newState).Key;
                    }
                    structure.CurrentCells[x, y].State = newState;
                    sum = toCalculateEnergy(x, y);
                    eAfter = jgb * sum;
                    deltaE = eAfter - eBefore;


                    if (deltaE > 0)
                    {
                        //CHECK MAX STATE
                            foreach (int State in stateOfMCNeighbors)
                        {
                            if (!checkState.ContainsKey(State))
                                checkState.Add(State, 1);
                            else
                                checkState[State] = checkState[State] + 1;
                        }

                        int maxState = getMaxKeyInt(checkState);
                      
                        structure.CurrentCells[x, y].State = maxState;
                        Color myKey = matchState.First(s => s.Value == maxState).Key;
                        structure.CurrentCells[x, y].Color = myKey;      
                        checkState.Clear();

                       // structure.CurrentCells[x, y].State = stateBefore;
                    }
                    else
                    {
                        state = structure.CurrentCells[x, y].State;
                        Color myKey = matchState.First(s => s.Value == state).Key;
                        structure.CurrentCells[x, y].Color = myKey;
                    }
                    stateOfMCNeighbors.Clear();
                }
            } while (randNumber > 0);
        }

        int getMaxKeyInt(Dictionary<int, int> map)   //(checkState) pierwszy int - klucz - state, drugi - wartosc - ilosc wystapien
        {
            int max = 0;
            int maxKeyInt = 0;

            foreach (var item in map)  // item - kolor i ilosc jego wystapien
            {
                if (item.Value > max)  //  szuka koloru, ktory wystapil najwiecej razy (item.Value - ilość wystąpień)
                    max = item.Value;
            }
            if (max == 1)
            {
                maxKeyInt = map.ElementAt(rand.Next(8)).Key;
            }
            else
            {
                maxKeyInt = map.FirstOrDefault(x => x.Value == max).Key;  //zwraca stan(klucz), który wystąpił najwięcej razy
            }

            return maxKeyInt;
        }

        int toCalculateEnergy(int x, int y)
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int xprev, xnext, yprev, ynext = 0;

            if (x == 0)
                xprev = wUpDown - 1;
            else
                xprev = x - 1;

            if (x == wUpDown - 1)
                xnext = 0;
            else
                xnext = x + 1;

            if (y == 0)
                yprev = hUpDown - 1;
            else
                yprev = y - 1;

            if (y == hUpDown - 1)
                ynext = 0;
            else
                ynext = y + 1;

            int[] neighborsTab = { 0, 0, 0, 0, 0, 0, 0, 0 };   //1-deltaKroneckera
            int sum = 0;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[xprev, y].State)
                neighborsTab[0] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[xprev, yprev].State)
                neighborsTab[1] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[x, yprev].State)
                neighborsTab[2] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[xnext, yprev].State)
                neighborsTab[3] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[xnext, y].State)
                neighborsTab[4] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[xnext, ynext].State)
                neighborsTab[5] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[x, ynext].State)
                neighborsTab[6] = 1;

            if (structure.CurrentCells[x, y].State != structure.CurrentCells[xprev, ynext].State)
                neighborsTab[7] = 1;

            for (int i = 0; i < neighborsTab.Length; i++)
            {
                sum += neighborsTab[i];
            }

            return sum;
        }

        void clearStates()
        {
            List<Color> statesToDelete = new List<Color>();
            foreach (KeyValuePair<Color, int> entry in matchState)
            {
                if (!CAList.Contains(entry.Key))
                    statesToDelete.Add(entry.Key);
            }

            foreach (Color item in statesToDelete)
            {
                matchState.Remove(item);
            }
        }

        /////////////////...............ENERGY DISTRIBUTION............///////////////////////


            /// <summary>
            /// TO DO: dodanie jeszcze nucleation type - increasing itd
            /// </summary>
        Dictionary<Color, int> colorToEnergy = new Dictionary<Color, int>();
        bool colorToEnergySet = false;

        private void distributeButton_Click(object sender, EventArgs e)
        {
            if (!colorToEnergySet)
            {
                prepareScaleOfEnergy();
            }
            distributeEnergy();
            //colorToEnergy.Clear();
        }

        private void button1_Click(object sender, EventArgs e)  //showEnergyButton
        {
            Form energyForm = new Form();
            energyForm.Size = new Size(515, 560);

            energyForm.Show();
            BackgroundWorker bg = new BackgroundWorker();
            bg.RunWorkerAsync(energyForm);
            bg.DoWork += bg_DoWork;

          

            //Image img = structure.setEnergyColor(structureBox.Width, structureBox.Height);
            // Bitmap bm = structure.setEnergyColor(structureBox.Width, structureBox.Height);
            //PictureBox energyBox = new PictureBox();
            //energyBox.Size = new Size(structureBox.Width, structureBox.Height);
            //energyBox.Image = structure.setEnergyColor(structureBox.Width, structureBox.Height);
            //Form energyForm = new Form();
            //energyForm.Size = new Size(515,560);
            //energyForm.Controls.Add(energyBox);       
            //energyForm.Show();
        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            Form form = (Form)e.Argument;
            PictureBox pb = new PictureBox();
            Image img;
            if (form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => { form.Controls.Add(pb); }));
            else
                form.Controls.Add(pb);
            do
            {
                img = structure.setEnergyColor(structureBox.Width, structureBox.Height);
                if (pb.InvokeRequired)
                    pb.Invoke(new MethodInvoker(() =>
                    {
                        pb.Size = new Size(structureBox.Width, structureBox.Height);
                        pb.Image = img;
                        pb.Refresh();
                    }));
                else
                {
                    pb.Size = new Size(structureBox.Width, structureBox.Height);
                    pb.Image = img;
                    pb.Refresh();
                }
                Thread.Sleep(10);

            } while (true);
        }



        //private void showEnergyButton_MouseHover(object sender, EventArgs e)
        //{
        //energyPictureBox.Visible = true;
        //structureBox.Visible = false;
        //energyPictureBox.Image = structure.setEnergyColor(structureBox.Width, structureBox.Height);
        //Bitmap bm = structure.setEnergyColor(structureBox.Width, structureBox.Height);
        //}

        //private void showEnergyButton_Click(object sender, EventArgs e) { 

        //    Image img = structure.setEnergyColor(structureBox.Width, structureBox.Height);
        //      Bitmap bm = structure.setEnergyColor(structureBox.Width, structureBox.Height);
        //    PictureBox energyBox = new PictureBox();
        //    energyBox.Size = new Size(500, 500);
        //    energyBox.Image = structure.setEnergyColor(structureBox.Width, structureBox.Height);

        //    Form energyForm = new Form();
        //    energyForm.Size = new Size(500, 500);
        //    energyForm.Controls.Add(energyBox);
        //    energyForm.Show();
        //}

        //private void showEnergyButton_MouseLeave(object sender, EventArgs e)
        //{
        //    energyPictureBox.Visible = false;
        //    structureBox.Visible = true;
        //}

        void prepareScaleOfEnergy()   
        {
            colorToEnergy.Add(Color.FromArgb(82, 206, 0), 0); //strong green
            colorToEnergy.Add(Color.FromArgb(146, 210, 0), 1);   //green
            colorToEnergy.Add(Color.FromArgb(213, 213, 0), 2); //vivid yellow
            colorToEnergy.Add(Color.FromArgb(217, 152, 0), 3);  //vivid orange
            colorToEnergy.Add(Color.FromArgb(221, 89, 0), 4);// orange
            colorToEnergy.Add(Color.FromArgb(255, 23, 0), 5); //red
            colorToEnergySet = true;


        }

        void distributeEnergy()
        {

            int energyInside = (int)energyInsideNumericUpDown.Value;
            int energyOnEdges = (int)energyOnEdgesNumericUpDown.Value;

            if (homogenousRadioButton.Checked)
            {
                for (int i = 0; i < structure.StructWidth; i++)
                {
                    for (int j = 0; j < structure.StructHeight; j++)
                    {
                        structure.CurrentCells[i, j].Energy = energyInside;
                        structure.CurrentCells[i, j].EnergyColor = colorToEnergy.First(x => x.Value == energyInside).Key;
                    }
                }
            }
            else
            {
                int wUpDown = (int)widthUpDown.Value;
                int hUpDown = (int)heightUpDown.Value;
                int iprev, inext, jprev, jnext = 0;

                for (int i = 0; i < structure.StructWidth; i++)
                {
                    for (int j = 0; j < structure.StructHeight; j++)
                    {
                        if (i == 0)
                            iprev = wUpDown - 1;
                        else
                            iprev = i - 1;

                        if (i == wUpDown - 1)
                            inext = 0;
                        else
                            inext = i + 1;

                        if (j == 0)
                            jprev = hUpDown - 1;
                        else
                            jprev = j - 1;

                        if (j == hUpDown - 1)
                            jnext = 0;
                        else
                            jnext = j + 1;

                        Color color = structure.CurrentCells[i, j].Color;

                        if (color != Color.FromArgb(0, 0, 0))
                        {
                            if (structure.CurrentCells[iprev, jprev].Color != color && structure.CurrentCells[iprev, jprev].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[iprev, j].Color != color && structure.CurrentCells[iprev, j].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[iprev, jnext].Color != color && structure.CurrentCells[iprev, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[i, jnext].Color != color && structure.CurrentCells[i, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, jnext].Color != color && structure.CurrentCells[inext, jnext].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, j].Color != color && structure.CurrentCells[inext, j].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[inext, jprev].Color != color && structure.CurrentCells[inext, jprev].Color != Color.FromArgb(0, 0, 0) ||
                                structure.CurrentCells[i, jprev].Color != color && structure.CurrentCells[i, jprev].Color != Color.FromArgb(0, 0, 0))
                            {
                                structure.CurrentCells[i, j].Energy = energyOnEdges;
                                structure.CurrentCells[i, j].EnergyColor = colorToEnergy.First(x => x.Value == energyOnEdges).Key;
                            }
                            else
                            {
                                structure.CurrentCells[i, j].Energy = energyInside;
                                structure.CurrentCells[i, j].EnergyColor = colorToEnergy.First(x => x.Value == energyInside).Key;
                            }
                        }
                    }
                }
            }
            updatePrevCells();  
        }

        /////////////////...............NUCLEATION............///////////////////////

      void startRecrystalizationButton_Click(object sender, EventArgs e)
        {
            setState();


            //////Rekrystalizacja
            thread = new Thread(new ThreadStart(recrystalization));
            thread.IsBackground = true;
            thread.Start();

        }

        public void setState()
        {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int stateToGrain = 0;
            Color color;

            for (int i = 0; i < wUpDown; i++)
            {
                for (int j = 0; j < hUpDown; j++)
                {
                    color = structure.CurrentCells[i, j].Color;
                    if (!matchState.ContainsKey(color))
                    {
                        while (matchState.ContainsValue(stateToGrain))
                            stateToGrain++;
                        structure.CurrentCells[i, j].State = stateToGrain;
                        matchState.Add(color, stateToGrain);
                        stateToGrain++;

                    }
                    else
                        structure.CurrentCells[i, j].State = matchState[color];
                }
            }
        }

        void nucleation(int numOfNucleations) {

            //int nucleonsOnStart = (int)nucleonsOnStartNumericUpDown.Value;
            int x, y, xprev, xnext, yprev, ynext = 0;
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;

            if (onEdgeCheckBox.Checked)
            {
                for (int i = 0; i < numOfNucleations; i++)
                {

                    x = rand.Next(wUpDown);
                    y = rand.Next(hUpDown);

                    if (x == 0)
                        xprev = wUpDown - 1;
                    else
                        xprev = x - 1;

                    if (x == wUpDown - 1)
                        xnext = 0;
                    else
                        xnext = x + 1;

                    if (y == 0)
                        yprev = hUpDown - 1;
                    else
                        yprev = y - 1;

                    if (y == hUpDown - 1)
                        ynext = 0;
                    else
                        ynext = y + 1;

                    Color color = structure.CurrentCells[x, y].Color;

                    if (structure.CurrentCells[x, y].Energy!=0 && (structure.CurrentCells[xprev, yprev].Color != color || structure.CurrentCells[xprev, y].Color != color || structure.CurrentCells[xprev, ynext].Color != color || structure.CurrentCells[x, ynext].Color != color
                        || structure.CurrentCells[xnext, ynext].Color != color || structure.CurrentCells[xnext, y].Color != color || structure.CurrentCells[xnext, yprev].Color != color || structure.CurrentCells[x, yprev].Color != color))
                    {
                        int r = rand.Next(255);
                        structure.CurrentCells[x, y].Color = Color.FromArgb(r, 0, 0);
                        if (matchState.ContainsKey(structure.CurrentCells[x, y].Color))
                        {
                            structure.CurrentCells[x, y].State = matchState[structure.CurrentCells[x, y].Color];  //dopasuje state z listy do koloru 
                        }
                        else
                        {
                            int addedState = matchState.Count + 1;
                            matchState.Add(structure.CurrentCells[x, y].Color, addedState);
                            structure.CurrentCells[x, y].State = addedState;
                        }


                        structure.CurrentCells[x, y].Energy = 0;
                        structure.CurrentCells[x, y].EnergyColor = colorToEnergy.First(a => a.Value == 0).Key;

                    }
                    else
                        i--;
                }
                // colorToEnergy.Clear();
                updatePrevCells();
                refreshStructureBox();
            }
            else
            {

                for (int i = 0; i < numOfNucleations; i++)
                {
                    int randX = rand.Next(wUpDown);
                    int randY = rand.Next(hUpDown);

                    setRandomColor(structure.CurrentCells[randX, randY]);
                    structure.CurrentCells[randX, randY].Energy = 0;
                    structure.CurrentCells[randX, randY].EnergyColor = colorToEnergy.First(a => a.Value == 0).Key;
                }
                updatePrevCells();
                refreshStructureBox();
            }


        }
        void recrystalization()
        {
            int i = 0;
            int nucleationOnStart = 0;
          
            int counter = 1;
            string value = null;
            int iterationCounter = 0;

            if (nucleonsOnStartNumericUpDown.InvokeRequired)
            {
                nucleonsOnStartNumericUpDown.Invoke(new MethodInvoker(() => { nucleationOnStart = (int)nucleonsOnStartNumericUpDown.Value; }));
            }

            if (nucleationTypeComboBox.InvokeRequired)
            {
                nucleationTypeComboBox.Invoke(new MethodInvoker(() => { value = nucleationTypeComboBox.SelectedItem.ToString(); }));
            }

            if (iterationsNumericUpDown.InvokeRequired)
            {
                iterationsNumericUpDown.Invoke(new MethodInvoker(() => { iterationCounter = (int)iterationsNumericUpDown.Value; }));
            }

            nucleation(nucleationOnStart);

            do
            {
                if (value.Contains("Constant"))
                {
                    if (i % iterationCounter == 0)
                    {
                        nucleation(nucleationOnStart);
                    }
                }
                else if (value.Contains("Increasing"))
                {
                    if (i % iterationCounter == 0)
                    {
                            nucleation(nucleationOnStart*counter);
                    }

                }
                counter++;


                recrystal();
                updatePrevCells();
                refreshStructureBox();
                Thread.Sleep(10);
                i++;
            } while (!allRecrystalized());
            CAList.Clear();
            BoundaryList.Clear();
        }

        public bool allRecrystalized() {
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            bool recrystal = true;

            for (int i = 0; i < wUpDown; i++)
            {
                for (int j = 0; j < hUpDown; j++)
                {
                    if (structure.CurrentCells[i, j].Energy != 0)
                        recrystal = false;
                }
            }
            return recrystal;
        }

        public void recrystal()
        {
            // int iprev, inext, jprev, jnext, sum = 0;
            int wUpDown = (int)widthUpDown.Value;
            int hUpDown = (int)heightUpDown.Value;
            int numberOfStates = (int)numOfStatesUpDown.Value;
            double jgb = 1.0;
            double eBefore, eAfter, deltaE;

            ///////dodane do randa
            int randNumber = wUpDown * hUpDown;
            int xprev, xnext, yprev, ynext, sum = 0;

            for (int i = 0; i < wUpDown; i++)
            {
                for (int j = 0; j < hUpDown; j++)
                {
                    notMCCheckedList.Add(structure.CurrentCells[i, j]);
                }
            }

            do
            {
                Cell randCell = notMCCheckedList[rand.Next(randNumber)];
                int x = randCell.X;
                int y = randCell.Y;

                notMCCheckedList.Remove(randCell);
                randNumber--;

                if (x == 0)
                    xprev = wUpDown - 1;
                else
                    xprev = x - 1;

                if (x == wUpDown - 1)
                    xnext = 0;
                else
                    xnext = x + 1;

                if (y == 0)
                    yprev = hUpDown - 1;
                else
                    yprev = y - 1;

                if (y == hUpDown - 1)
                    ynext = 0;
                else
                    ynext = y + 1;

                if (structure.CurrentCells[xprev, yprev].Energy == 0 || structure.CurrentCells[xprev, y].Energy == 0
                || structure.CurrentCells[xprev, ynext].Energy == 0 || structure.CurrentCells[x, ynext].Energy == 0
                || structure.CurrentCells[xnext, ynext].Energy == 0 || structure.CurrentCells[xnext, y].Energy == 0
                || structure.CurrentCells[xnext, yprev].Energy == 0 || structure.CurrentCells[x, yprev].Energy == 0)
                {

                    //setState(wUpDown,hUpDown);
                    Color color = structure.CurrentCells[x, y].Color;
                    
                    if (!CAList.Contains(color) && color != colorToOmit && (structure.CurrentCells[xprev, yprev].Color != color || structure.CurrentCells[xprev, y].Color != color
                    || structure.CurrentCells[xprev, ynext].Color != color || structure.CurrentCells[x, ynext].Color != color
                    || structure.CurrentCells[xnext, ynext].Color != color || structure.CurrentCells[xnext, y].Color != color
                    || structure.CurrentCells[xnext, yprev].Color != color || structure.CurrentCells[x, yprev].Color != color)
                     )
                    {

                        stateOfMCNeighbors.Add(structure.CurrentCells[xprev, y].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[xprev, yprev].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[x, yprev].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[xnext, yprev].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[xnext, y].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[xnext, ynext].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[x, ynext].State);

                        stateOfMCNeighbors.Add(structure.CurrentCells[xprev, ynext].State);


                        sum = toCalculateEnergy(x, y);
                        eBefore = jgb * sum + structure.CurrentCells[x, y].Energy;
                        int stateBefore = structure.CurrentCells[x, y].State;
                        int maxNeighbourState = 0;
                        foreach (int State in stateOfMCNeighbors)
                        {
                            if (State >= maxNeighbourState)
                                maxNeighbourState = State;
                        }

                        int newState = maxNeighbourState;   //najwyzszy stan z sasiadow
                        structure.CurrentCells[x, y].State = newState;
                        sum = toCalculateEnergy(x, y);
                        eAfter = jgb * sum;
                        deltaE = eAfter - eBefore;

                        if (deltaE > 0)
                        {
                            structure.CurrentCells[x, y].State = stateBefore;
                        }
                        else
                        {
                            structure.CurrentCells[x, y].Energy = 0;
                            structure.CurrentCells[x,y].EnergyColor = colorToEnergy.First(a => a.Value == 0).Key;
                            //int state = structure.CurrentCells[x, y].State;
                            Color myKey = matchState.First(s => s.Value == newState).Key;
                            structure.CurrentCells[x, y].Color = myKey;
                        }
                        stateOfMCNeighbors.Clear();
                    }
                }
            } while (randNumber > 0);
        }
    }
}
