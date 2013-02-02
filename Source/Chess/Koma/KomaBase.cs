using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Chess
{
    abstract class KomaBase
    {
        protected int MAX_LEFT = 8;
        protected int MAX_HEIGHT = 8;
        public Bitmap MyPicture { get; private set; }
        public abstract KomaKind Kind { get;}
        public Color MyColor { get; private set; }
        public int Height { get { return Location.Item2; } }
        public int Left { get { return Location.Item1; } }
        public int Index { get; private set; }
        public Tuple<int, int> Location { get; private set; }
        public bool IsDead { get; set; }
        public KomaBase(Color color, int index)
        {
            MyColor = color;
            Index = index;

            CheckIndex();
            this.MyPicture = GetPicture();
        }

        public abstract void SetDefaultPoint();

        public abstract int GetMaxCount();

        public abstract List<Tuple<MoveType, int,int>> GetMovableLoacation();
        protected abstract Bitmap GetPicture();
        public void SetLocation(int left, int height)
        {
            this.Location = new Tuple<int, int>(left, height);
        }

        private void CheckIndex()
        {
            if (Index +1 > GetMaxCount())
            {
                throw new ArgumentException("Count is larger than max count!!");
            }

        }
        protected virtual int getHeight()
        {
            return GetObjectByColor(7, 0);
        }

        protected T GetObjectByColor<T>(T whiteObject, T blackObject)
        {
            return MyColor == Color.White ? whiteObject : blackObject;
        }
    }
}
