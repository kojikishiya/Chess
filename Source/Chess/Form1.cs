using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        BoardManager boardManager = new BoardManager();

        private const int PictureSize = 45;
        public Form1()
        {
            InitializeComponent();
            Text = "Chess";
            ClientSize = new Size(PictureSize * 8, PictureSize * 8);
            createKoma();
            // king = Properties.Resources._30px_Chess_kdt45_svg;

        }
        private void createKoma()
        {

            boardManager.KomaCreate();
        }

        private void drawKoma(Graphics graphics)
        {
            foreach (var kom in boardManager.GetAliveKoma())
            {
                graphics.DrawImage(kom.MyPicture, PictureSize * kom.Left, PictureSize * kom.Height);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Bisque, x * PictureSize, y * PictureSize, PictureSize, PictureSize);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.BurlyWood, x * PictureSize, y * PictureSize, PictureSize, PictureSize);
                    }
                }
            }
            this.drawKoma(e.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            int left = e.X / 45;
            int height = e.Y / 45;
            if (boardManager.IsInsicateMovableLocation())
            {
                if (boardManager.IsSameKoma(left, height))
                {
                    boardManager.ClearClickedKoma();

                    base.Refresh();
                    return;
                }
                else
                {
                    if (boardManager.CanMove(left, height))
                    {
                        boardManager.SetLocation(left, height);
                        if (boardManager.IsKingDead())
                        {
                            MessageBox.Show(boardManager.GetPlayerColor() + "の勝ち");
                        }
                        boardManager.ChangePlayer();
                        this.Refresh();
                        return;
                    }
                }
            }

            if (boardManager.IsPlayerKoma(left, height))
            {
                boardManager.setClicedKoma(left, height);
                var locations = boardManager.GetMovableLocation();
                this.drawMovableLocation(locations);
            }
        }

        private void drawMovableLocation(IList<Tuple<int, int>> locations)
        {
            Graphics g = Graphics.FromHwnd(this.Handle);

            foreach (var loaction in locations)
            {
                g.FillRectangle(Brushes.Red, loaction.Item1 * PictureSize, loaction.Item2 * PictureSize, PictureSize, PictureSize);

            }

            drawKoma(g);
        }
    }
}

   
  

 
   
