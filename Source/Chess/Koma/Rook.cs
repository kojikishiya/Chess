using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    internal class Rook : KomaBase
    {
        public Rook(Color color, int index)
             : base(color, index)
         {

         }

        public override void SetDefaultPoint()
        {
            SetLocation(0 + 7 * Index, getHeight());
        }

        public override int GetMaxCount()
        {
            return 2;
        }

        public override List<Tuple<MoveType, int, int>> GetMovableLoacation()
        {
            var list = new List<Tuple<MoveType, int, int>>();
            foreach (var l in new[] { -1, 0, 1 })
            {
                foreach (var h in new[] { -1, 0, 1 })
                {
                    if (h == 0 || l == 0)
                        list.Add(new Tuple<MoveType, int, int>(MoveType.Direction, Left + l, Height + h));
                }
            }

            return list;

        }

        public override KomaKind Kind
        {
            get
            {
                return KomaKind.Rook;
            }
        }

        protected override System.Drawing.Bitmap GetPicture()
        {
            return GetObjectByColor(
                 Properties.Resources._45px_Chess_rlt45_svg
                ,Properties.Resources._45px_Chess_rdt45_svg);
        }
    }
}
