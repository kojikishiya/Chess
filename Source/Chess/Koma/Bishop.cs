using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    [Serializable]
    internal class Bishop : KomaBase
    {
        public Bishop(PlayerNo color, int index)
             : base(color, index)
         {

         }

        public override void SetDefaultPoint()
        {
            SetLocation(2 + 3 * Index, getHeight());
        }

        public override int GetMaxCount()
        {
            return 2;
        }

        public override List<Tuple<MoveType, int, int>> GetMovableLoacation()
        {
            var list = new List<Tuple<MoveType, int, int>>();
            foreach (int l in new[] { -1, 1 })
            {
                foreach (int h in new[] { -1, 1 })
                {
                    list.Add(new Tuple<MoveType, int, int>(MoveType.Direction, l+this.Left, h+this.Height));
                }
            }

            return list;
        }

        public override KomaKind Kind
        {
            get { return KomaKind.Bishop; }
        }


        protected override System.Drawing.Bitmap GetPicture()
        {
            return GetObjectByColor(
                     Properties.Resources._45px_Chess_blt45_svg
                    ,Properties.Resources._45px_Chess_bdt45_svg);
        }
    }
}
