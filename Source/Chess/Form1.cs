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

        public Form1()
        {
            InitializeComponent();

            var board = new ChessBoard(ImageSize.Thirty);

            Text = "Chess";
            this.Controls.Add(board);
            ClientSize = board.ClientSize;
            //ClientSize = new Size(PictureSize * 8, PictureSize * 8);
            // king = Properties.Resources._30px_Chess_kdt45_svg;

        }
    }
}

   
  

 
   
