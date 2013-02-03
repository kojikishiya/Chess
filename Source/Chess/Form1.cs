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
            board.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            Text = "Chess";
            //this.Controls.Add(board);
            ClientSize = new Size(board.ClientSize.Width + 50, board.ClientSize.Height) ;
            var panel = new FlowLayoutPanel();
            panel.Size = ClientSize;
            this.Controls.Add(panel);

            panel.Controls.Add(board);
            var pnl2 = createPanel(board.ClientSize.Height, 0);
            panel.Controls.Add(pnl2);
            pnl2.Controls.Add(createPanel(board.ClientSize.Height/2, 1));
            pnl2.Controls.Add(createPanel(board.ClientSize.Height / 2, 2));

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

        }
        private Control createPanel(int height,int player)
        {
            var panel2 = new FlowLayoutPanel();
            panel2.Size = new Size(50, height);
            panel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            panel2.BackColor = System.Drawing.Color.Moccasin;
            if (player != 0)
            {
                var lablel1 = new Label();
                lablel1.Text = "Player" + player;
                panel2.Controls.Add(lablel1);
            }
            return panel2;

        }
        private Button createButton()
        {
            var btn = new Button();
            btn.Size = new Size(45, 20);
            return btn;
        }
    }
}

   
  

 
   
