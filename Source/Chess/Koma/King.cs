using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    [Serializable]
    class King : KomaBase
    {
        public King(Color color,int index)
             : base(color, index)
        {

        }
        public override void SetDefaultPoint()
        {
                SetLocation(4, getHeight());
        }

        public override int GetMaxCount()
        {
            return 1;
        }

        public override List<Tuple<MoveType, int, int>> GetMovableLoacation()
        {

            var list = new List<Tuple<MoveType, int, int>>();
            foreach (var l in new[] { -1, 0, 1 })
            {
                foreach (var h in new[] { -1, 0, 1 })
                {
                        list.Add(new Tuple<MoveType, int, int>(MoveType.Normal, Left + l, Height + h));
                }
            }

            return list;
        }

        public override KomaKind Kind
        {
            get
            {
                return KomaKind.King;
            }
        }

        protected override System.Drawing.Bitmap GetPicture()
        {
            return GetObjectByColor(
                Properties.Resources._45px_Chess_klt45_svg
                , Properties.Resources._45px_Chess_kdt45_svg);

        }

    }
}
