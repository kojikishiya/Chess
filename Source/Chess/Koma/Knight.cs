using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    internal class Knight : KomaBase
    {
        public Knight(Color color, int index)
            : base(color, index)
         {

         }

        public override void SetDefaultPoint()
        {
            SetLocation(1 + 5 * Index, getHeight());
        }

        public override int GetMaxCount()
        {
            return 2;
        }

        public override List<Tuple<MoveType, int, int>> GetMovableLoacation()
        {
            var list = new List<Tuple<MoveType, int, int>>();
            foreach (var l in new[] { -2,-1, 1,2 })
            {
                foreach (var h in new[] { -2, -1, 1, 2 })
                {
                    if (l != h && h+l != 0)
                        list.Add(new Tuple<MoveType, int, int>(MoveType.Normal, Left + l, Height + h));
                }
            }
            return list;
        }

        public override KomaKind Kind
        {
            get { return KomaKind.Knight; }
        }

        protected override System.Drawing.Bitmap GetPicture()
        {
            return GetObjectByColor(
                Properties.Resources._45px_Chess_klt45_svg
                , Properties.Resources._45px_Chess_kdt45_svg);
        }
    }
}
