using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wygaszacz
{
    public partial class wygaszacz : Form
    {
        public wygaszacz()
        {
            InitializeComponent();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string[] Pictures { get; set; }
        public int CurrentPicture { get; set; }
        private const string pictureFolder= @"D:\obrazy";

        private void SetMousePosition()
        {
            X = Cursor.Position.X;
            Y = Cursor.Position.Y;
        }

        private void LoadPicturesFromTheFolder(string folderPath)
        {
            try
            {
                Pictures = Directory.GetFiles(folderPath);
            }
            catch (Exception)
            {

                MessageBox.Show($"Błąd w trakcie pobierania plików z {folderPath}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(Pictures.Length == 0)
            {
                MessageBox.Show($"W folderze {folderPath} nie ma plików", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetBackgroundImage(int pos)
        {
            try
            {
                this.BackgroundImage = Image.FromFile(Pictures[pos]);
                CurrentPicture = pos;
            }
            catch (Exception)
            {

                MessageBox.Show($"Nie udało się załadować obrazka {Pictures[pos]}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            var lastPicture = Pictures.Length - 1;

            if (CurrentPicture == lastPicture)
            {
                SetBackgroundImage(0);
            }
            else
            {
                SetBackgroundImage(CurrentPicture + 1);
            }
        }

        private void Wygaszacz_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void Wygaszacz_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Exit();
        }

        private void Wygaszacz_MouseMove(object sender, MouseEventArgs e)
        {
            var x = Cursor.Position.X;
            var y = Cursor.Position.Y;

            if (x != X || y != Y)
            {
                Application.Exit();
            }
        }

        private void Wygaszacz_Load(object sender, EventArgs e)
        {
            SetMousePosition();
            LoadPicturesFromTheFolder(pictureFolder);
            SetBackgroundImage(0);
            Cursor.Hide();
            this.WindowState = FormWindowState.Maximized;
            timer1.Start();
        }
    }
}
