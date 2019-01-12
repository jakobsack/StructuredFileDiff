using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlDiffLib;
using SettingsDiff.Result;

namespace StructuredFileGui
{
    public partial class Form1 : Form
    {
        private ComparableXmlFile leftFile;
        private ComparableXmlFile rightFile;
        private Font monospace = new Font(FontFamily.GenericMonospace, 10);
        private float indent = 16;
        private int lineHeight = 16;

        private Brush brushCharAdded = "#83c47e".ToSolidBrush();
        private Brush brushCharAddedEven = "#73b46e".ToSolidBrush();
        private Brush brushCharRemoved = "#c4847e".ToSolidBrush();
        private Brush brushCharRemovedEven = "#b4746e".ToSolidBrush();

        private Brush brushLineUnchanged = "#ffffff".ToSolidBrush();
        private Brush brushLineUnchangedEven = "#f1f4f2".ToSolidBrush();
        private Brush brushLineAdded = "#a3f49e".ToSolidBrush();
        private Brush brushLineAddedEven = "#93e48e".ToSolidBrush();
        private Brush brushLineRemoved = "#f4a49e".ToSolidBrush();
        private Brush brushLineRemovedEven = "#e4948e".ToSolidBrush();
        private Brush brushLineMovedOrigin = "#e4e4f4".ToSolidBrush();
        private Brush brushLineMovedOriginEven = "#d4d4e4".ToSolidBrush();
        private Brush brushLineMoved = "#c4c4f4".ToSolidBrush();
        private Brush brushLineMovedEven = "#b4b4e4".ToSolidBrush();

        public Form1()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void switchSidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComparableXmlFile temp = leftFile;
            leftFile = rightFile;
            rightFile = temp;

            RenderFiles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox1.Text = openFileDialog1.FileName;

            try
            {
                leftFile = new ComparableXmlFile(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Ups");
            }

            RenderFiles();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox2.Text = openFileDialog1.FileName;

            try
            {
                rightFile = new ComparableXmlFile(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Ups");
            }

            RenderFiles();
        }

        private void RenderFiles()
        {
            if (leftFile == null && rightFile == null)
            {
                return;
            }

            Block result = null;
            if (leftFile == null)
            {
                result = rightFile.DiffWith(rightFile);
            }
            else if (rightFile == null)
            {
                result = leftFile.DiffWith(leftFile);
            }
            else
            {
                result = leftFile.DiffWith(rightFile);
            }

            // Render result :-)
            Graphics tempGraphics = CreateGraphics();
            float longestLine = GetLongestLine(result, tempGraphics);
            int lines = GetNumberOfLines(result);

            int width = (int)Math.Ceiling(longestLine);

            bool evenLine = false;
            RenderResult graphics = RenderBlock(result, width, ref evenLine);

            // Set the fiels where we have something to set
            panelLeft.Controls.Clear();
            panelRight.Controls.Clear();

            if (leftFile != null)
            {
                panelLeft.Controls.Add(new PictureBox
                {
                    Top = 0,
                    Left = 0,
                    Height = graphics.Left.Height,
                    Width = graphics.Left.Width,
                    Image = graphics.Left,
                });
            }

            if (rightFile != null)
            {
                panelRight.Controls.Add(new PictureBox
                {
                    Top = 0,
                    Left = 0,
                    Height = graphics.Right.Height,
                    Width = graphics.Right.Width,
                    Image = graphics.Right,
                });
            }
        }

        private int GetNumberOfLines(Block result)
        {
            int children = result
                .Children
                .Select(x => GetNumberOfLines(x))
                .DefaultIfEmpty()
                .Sum();

            return children + result.After.Count + result.Before.Count;
        }

        private float GetLongestLine(Block result, Graphics tempGraphics)
        {
            float before = result
                .Before
                .SelectMany(x => new[] { tempGraphics.MeasureString(x.Left, monospace), tempGraphics.MeasureString(x.Right, monospace) })
                .Select(x => x.Width)
                .DefaultIfEmpty()
                .Max();
            float after = result
                .After
                .SelectMany(x => new[] { tempGraphics.MeasureString(x.Left, monospace), tempGraphics.MeasureString(x.Right, monospace) })
                .Select(x => x.Width)
                .DefaultIfEmpty()
                .Max();
            float children = result
                .Children
                .Select(x => GetLongestLine(x, tempGraphics))
                .DefaultIfEmpty()
                .Max();

            if (children > 0)
            {
                children += indent;
            }

            if (before > children && before > after)
            {
                return before;
            }
            else if (children > after)
            {
                return children;
            }
            else
            {
                return after;
            }
        }

        private RenderResult RenderBlock(Block block, float width, ref bool evenLine)
        {
            int lines = GetNumberOfLines(block);

            int intWidth = (int)Math.Ceiling(width);
            int intHeight = lines * lineHeight;

            Bitmap leftBitmap = new Bitmap(intWidth, intHeight);
            Bitmap rightBitmap = new Bitmap(intWidth, intHeight);
            using (Graphics leftGraphics = Graphics.FromImage(leftBitmap))
            using (Graphics rightGraphics = Graphics.FromImage(rightBitmap))
            {
                PrepareBackground(block.EntryType, leftGraphics, rightGraphics, intWidth, intHeight, evenLine);

                int currentLine = 0;
                foreach (Line line in block.Before)
                {
                    RenderLine(line, leftGraphics, rightGraphics, currentLine++);
                    evenLine = !evenLine;
                }

                int childrenHeight = currentLine * lineHeight;

                foreach (Block child in block.Children)
                {
                    RenderResult result = RenderBlock(child, width - indent, ref evenLine);
                    Rectangle sourceRectangle = new Rectangle(0, 0, result.Left.Width, result.Left.Height);
                    Rectangle destinationRegion = new Rectangle(intWidth - result.Left.Width, childrenHeight, result.Left.Width, result.Left.Height);

                    leftGraphics.DrawImage(result.Left, destinationRegion, sourceRectangle, GraphicsUnit.Pixel);
                    rightGraphics.DrawImage(result.Right, destinationRegion, sourceRectangle, GraphicsUnit.Pixel);
                    childrenHeight += result.Left.Height;
                    evenLine = !evenLine;
                }

                currentLine = (lines - block.After.Count());
                foreach (Line line in block.Before)
                {
                    RenderLine(line, leftGraphics, rightGraphics, currentLine++);
                    evenLine = !evenLine;
                }

            }

            return new RenderResult
            {
                Left = leftBitmap,
                Right = rightBitmap,
            };
        }

        private void RenderLine(Line line, Graphics leftGraphics, Graphics rightGraphics, int currentLine)
        {

            if (!string.IsNullOrWhiteSpace(line.Left))
            {
                leftGraphics.DrawString(line.Left, monospace, Brushes.Black, 0, currentLine * lineHeight);
            }

            if (!string.IsNullOrWhiteSpace(line.Right))
            {
                rightGraphics.DrawString(line.Right, monospace, Brushes.Black, 0, currentLine * lineHeight);
            }

        }

        private void PrepareBackground(EntryType entryType, Graphics left, Graphics right, int width, int height, bool evenLine)
        {
            if (entryType == EntryType.Unchanged)
            {
                left.FillRectangle(evenLine ? brushLineUnchangedEven: brushLineUnchanged, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineUnchangedEven : brushLineUnchanged, left, width, height);
                right.FillRectangle(evenLine ? brushLineUnchangedEven : brushLineUnchanged, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineUnchangedEven : brushLineUnchanged, right, width, height);
            }
            else if(entryType == EntryType.Added)
            {
                right.FillRectangle(evenLine ? brushLineAddedEven : brushLineAdded, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineAddedEven : brushLineAdded, right, width, height);
            }
            else if (entryType == EntryType.Removed)
            {
                left.FillRectangle(evenLine ? brushLineRemovedEven : brushLineRemoved, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineRemovedEven : brushLineRemoved, left, width, height);
            }
            else if (entryType == EntryType.MovedOrigin)
            {
                left.FillRectangle(evenLine ? brushLineMovedOriginEven: brushLineMovedOrigin, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineMovedOriginEven : brushLineMovedOrigin, left, width, height);
            }
            else
            {
                left.FillRectangle(evenLine ? brushLineMovedEven : brushLineMoved, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineMovedEven : brushLineMoved, left, width, height);
                right.FillRectangle(evenLine ? brushLineMovedEven : brushLineMoved, 0, 0, width, height);
                AlternateBackground(!evenLine ? brushLineMovedEven : brushLineMoved, right, width, height);
            }
        }

        private void AlternateBackground(Brush color, Graphics graphics, int width, int height)
        {
            int offset = lineHeight;
            while(offset < height)
            {
                graphics.FillRectangle(color, 0, offset, width, lineHeight);
                offset += 2 * lineHeight;
            }
        }

        private class RenderResult
        {
            public Bitmap Left { get; set; }
            public Bitmap Right { get; set; }
        }

        private class RenderInformation
        {
            public int Lines { get; set; }
            public int LongestLine { get; set; }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panelLeft.Height = splitContainer1.Panel1.Height - 32;
            panelRight.Height = splitContainer1.Panel2.Height - 32;
        }
    }
}
