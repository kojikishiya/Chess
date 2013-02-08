using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    enum ImageSize
    {
        Thirty,
        FortyFive
    }
     partial class ChessBoard : UserControl
    {
         BoardManager boardManager;

         ImageSize komaImageSize;

         int PictureSize = 0;
         [Browsable(true), Category("ImageSize"), DefaultValue(ImageSize.FortyFive)]
         public ImageSize KomaImageSize
         {
             get { return komaImageSize; }
             set { komaImageSize = value; }
         }
        
        public ChessBoard(Context context)
        {
            InitializeComponent();

            boardManager = new BoardManager(context);
            this.komaImageSize = context.Size;
            this.PictureSize = getImageSize();
            ClientSize = new Size(PictureSize * 8, PictureSize * 8);
            createKoma();

        }

        private void createKoma()
        {

            boardManager.KomaCreate();
        }

        private void drawKoma(Graphics graphics)
        {
            foreach (var kom in boardManager.GetAliveKoma())
            {
                graphics.DrawImage(new Bitmap( kom.MyPicture,PictureSize,PictureSize), PictureSize * kom.Left, PictureSize * kom.Height);
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

            int left = e.X / PictureSize;
            int height = e.Y / PictureSize;
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
                            MessageBox.Show(boardManager.GetPlayerName() + "の勝ち");
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

        private int getImageSize()
        {
            switch (komaImageSize)
            {
                case ImageSize.Thirty:
                    return 30;
                case ImageSize.FortyFive:
                    return 45;
                default:
                    throw new ArgumentException("unexpected ");
            }
        }


    }
}
