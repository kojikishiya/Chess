using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class BoardManager
    {
        List<KomaBase> komaList = null;

        Color playerColor;
        KomaBase clickedKoma;
        List<Tuple<int, int>> movableLoacation = new List<Tuple<int,int>>();

        public BoardManager()
        {

        }

        public List<KomaBase> KomaCreate()
        {
            if (komaList != null)
            {
                return komaList;
            }
            komaList = new List<KomaBase>();
            foreach (KomaKind komaKind in Enum.GetValues(typeof(KomaKind)))
            {
                int index = 0;
                while(true)
                {
                    var komaKuro = createKoma(komaKind, Color.White, index);
                    komaKuro.SetDefaultPoint();
                    var komaShiro = createKoma(komaKind, Color.Black, index);
                    komaShiro.SetDefaultPoint();
                    komaList.AddRange(new[] { komaKuro, komaShiro });

                    if (komaShiro.GetMaxCount() == index + 1)
                        break;
                    ++index;

                   
                }
            }


            return komaList;

        }

        public IEnumerable<KomaBase> GetAliveKoma()
        {
            return komaList.Where((koma) => !koma.IsDead);
        }

        public List<Tuple<int,int>> GetMovableLocation()
        {
            return movableLoacation;
        }

        private List<Tuple< int, int>> getCanMove(
            IEnumerable<Tuple<MoveType, int, int>> locateInfos,
            KomaBase koma)
        {
            var inBoardLocates = this.getInBoardLocate(locateInfos);
            var canMoveLocates = new List<Tuple< int, int>>();

            canMoveLocates.AddRange(this.getNotExistsAny(inBoardLocates));
            canMoveLocates.AddRange(this.getExistsEnemy(inBoardLocates));
            canMoveLocates.AddRange(this.getDirection(inBoardLocates, koma));
            canMoveLocates.AddRange(this.getNormal(inBoardLocates));

            return canMoveLocates;

        }
        private IEnumerable<Tuple<int, int>> getNormal(
    IEnumerable<Tuple<MoveType, int, int>> locationInfos)
        {
            return locationInfos.Where((location) =>
                location.Item1 == MoveType.Normal
                && !existsKoma(location.Item2, location.Item3,playerColor))
                .Select(location => new Tuple<int, int>(location.Item2, location.Item3));
        }
        private IEnumerable<Tuple< int, int>> getDirection(
            IEnumerable<Tuple<MoveType, int, int>> locationInfos
            ,KomaBase koma)
        {
            var directions = locationInfos.Where((location) =>
                location.Item1 == MoveType.Direction);
            List<Tuple< int, int>> list = new List<Tuple< int, int>>();

            foreach (var direction in directions)
            {
               int vL = direction.Item2 - koma.Left ;
               int vH =direction.Item3 - koma.Height ;
                for(int i =0;i<8;++i)
                {
                    int left = i * vL + direction.Item2;
                    int height = i * vH + direction.Item3;
                    if (!isInBoard(left) || !isInBoard(height))
                    {
                        break;

                    }
                    else if(this.existsKoma(left,height,playerColor))
                    {
                        break;
                    }
                    else if (this.existsEnemy(left, height))
                    {
                        list.Add(new Tuple<int, int>(left, height));
                        break;
                    }
                    else
                    {
                        list.Add(new Tuple<int, int>(left, height));
                    }
                }
            }
            return list;
           
        }
        private IEnumerable<Tuple< int, int>> getExistsEnemy(
             IEnumerable<Tuple<MoveType, int, int>> locationInfos)
        {
            var l = locationInfos.Where((location) =>
                location.Item1 == MoveType.ExistsEnemy
                && existsEnemy(location.Item2, location.Item3));
                var n =l
                .Select(location => new Tuple<int, int>(location.Item2, location.Item3));
            return n;
            

        }
        private IEnumerable<Tuple< int, int>> getNotExistsAny(
                IEnumerable<Tuple<MoveType, int, int>> locationInfos)
        {
            return locationInfos.Where((location) =>
                location.Item1 == MoveType.NotExistAny
                && !existsAny(location.Item2, location.Item3))
                .Select(location => new Tuple<int, int>(location.Item2, location.Item3));
        }

                private bool existsPlayer(int left, int heigth)
        {
            return this.existsKoma(left,heigth,this.playerColor);
        }

        private bool existsEnemy(int left, int heigth)
        {
            var color = this.playerColor == Color.White?Color.Black:Color.White;
            return this.existsKoma(left,heigth,color);
        }
        private bool existsKoma(int left, int heigth,Color komaColor)
        {
            return this.GetAliveKoma().Where((koma) =>
            koma.MyColor == komaColor
            && koma.Left == left
            && koma.Height == heigth).Count() > 0;
        }
        private bool existsAny(int left, int height)
        {
            return this.GetAliveKoma().Where((koma) =>
                koma.Left == left && koma.Height == height).Count() > 0;
        }
        private IEnumerable<Tuple<MoveType, int, int>>
            getInBoardLocate(IEnumerable<Tuple<MoveType, int, int>> locateInfos)
        {
            return locateInfos.Where((locateinfo) => isInBoard(locateinfo.Item2)
                && isInBoard(locateinfo.Item3));
        }

        private bool isInBoard(int point)
        {
            if (0 <= point && point <= 7)
            {
                return true;
            }
            return false;
        }
        private KomaBase getKoma(int left, int height)
        {
            var komas = komaList.Where((koma)
                => !koma.IsDead
                && koma.Left == left && koma.Height == height);
            if (komas.Count() > 0)
            {
                return komas.First();
            }
            return null;
        }

        private KomaBase createKoma(KomaKind kind, Color color, int index)
        {
            switch (kind)
            {
                case KomaKind.King:
                    return new King(color, index);
                case KomaKind.Queen:
                    return new Queen(color, index);
                case KomaKind.Bishop :
                    return new Bishop(color,index);
                case KomaKind.Knight:
                    return new Knight(color, index);
                case KomaKind.Pone:
                    return new Pawn(color, index);
                case KomaKind.Rook:
                    return new Rook(color,index);
                default:
                    throw new ArgumentException("Unexpected Kind!!");
            }
        }

        internal bool IsSameKoma(int left, int height)
        {
            return (clickedKoma.Left == left && clickedKoma.Height == height);
        }

        internal void ClearClickedKoma()
        {
            clickedKoma = null;
            movableLoacation.Clear();


        }

        internal bool IsInsicateMovableLocation()
        {
            return movableLoacation.Count > 0;
        }

        internal bool CanMove(int left, int height)
        {
            return
                this.movableLoacation.Where((location) => 
                    location.Item1 == left &&
                    location.Item2 == height).Count() > 0;
        }

        internal void SetLocation(int left, int height)
        {
            foreach (var koma in this.GetAliveKoma().Where((koma) =>
                koma.Left == left &&
                koma.Height == height &&
                koma.MyColor != playerColor))
            {
                koma.IsDead = true;
            }
            clickedKoma.SetLocation(left, height);

        }

        internal bool IsPlayerKoma(int left, int height)
        {
            return this.GetAliveKoma().Where((location) =>
                 location.MyColor == playerColor
                 && location.Left == left
                 && location.Height == height).Count() > 0;
        }

        internal void ChangePlayer()
        {
            playerColor = playerColor == Color.White ? Color.Black : Color.White;
            ClearClickedKoma();
        }
        internal void setClicedKoma(int left, int height)
        {
            movableLoacation = new List<Tuple<int, int>>();

            clickedKoma = this.getKoma(left, height);
            if (clickedKoma != null)
            {
                foreach (var locateInfo in this.getCanMove(clickedKoma.GetMovableLoacation(), clickedKoma))
                {
                    movableLoacation.Add(locateInfo);
                }
            }

        }

        internal bool IsKingDead()
        {
            return this.komaList.Where((koma) =>
                koma.Kind == KomaKind.King
                && koma.IsDead
                && koma.MyColor != playerColor).Count() > 0;
        }

        internal string GetPlayerColor()
        {
            return playerColor.ToString();
        }
    }

}
