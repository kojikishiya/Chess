using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
     class Pawn : KomaBase
    {
         public Pawn(Color color, int index)
             : base(color, index)
         {

         }
        public override void SetDefaultPoint()
        {
            SetLocation(Index, getHeight());
        }

        public override int GetMaxCount()
        {
            return 10;
        }

        public override List<Tuple<MoveType, int, int>> GetMovableLoacation()
        {
            int h = MyColor == Color.White ? -1 : 1;
            var list = new List<Tuple<MoveType, int, int>>();
            list.Add(new Tuple<MoveType, int, int>(MoveType.NotExistAny, Left, Height+h));
            list.Add(new Tuple<MoveType, int, int>(MoveType.ExistsEnemy, Left - 1, Height + h));
            list.Add(new Tuple<MoveType, int, int>(MoveType.ExistsEnemy, Left + 1, Height + h));

            return list;
        }

        public override KomaKind Kind
        {
            get { return KomaKind.Pone; }
        }

        protected override System.Drawing.Bitmap GetPicture()
        {
            return GetObjectByColor(
                Properties.Resources.Chess_plt45_svg
                ,Properties.Resources._45px_Chess_pdt45_svg);
        }
        protected override int getHeight()
        {
            return GetObjectByColor(6, 1);
        }
    }
}
