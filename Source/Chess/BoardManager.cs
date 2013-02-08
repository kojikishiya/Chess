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

        PlayerNo playerNo = PlayerNo.Two;
        KomaBase clickedKoma;
        List<Tuple<int, int>> movableLoacation = new List<Tuple<int,int>>();
        Context context;
        public BoardManager(Context context)
        {
            this.context = context;
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
                    var komaKuro = createKoma(komaKind, PlayerNo.Two, index);
                    komaKuro.SetDefaultPoint();
                    var komaShiro = createKoma(komaKind, PlayerNo.One, index);
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
                && !existsKoma(location.Item2, location.Item3,playerNo))
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
                    else if(this.existsKoma(left,height,playerNo))
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
            return this.existsKoma(left,heigth,this.playerNo);
        }

        private bool existsEnemy(int left, int heigth)
        {
            var no = this.playerNo == PlayerNo.Two?PlayerNo.One:PlayerNo.Two;
            return this.existsKoma(left, heigth, no);
        }
        private bool existsKoma(int left, int heigth,PlayerNo player)
        {
            return this.GetAliveKoma().Where((koma) =>
            koma.Player == player
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

        private KomaBase createKoma(KomaKind kind, PlayerNo playerNo, int index)
        {
            switch (kind)
            {
                case KomaKind.King:
                    return new King(playerNo, index);
                case KomaKind.Queen:
                    return new Queen(playerNo, index);
                case KomaKind.Bishop :
                    return new Bishop(playerNo,index);
                case KomaKind.Knight:
                    return new Knight(playerNo, index);
                case KomaKind.Pone:
                    return new Pawn(playerNo, index);
                case KomaKind.Rook:
                    return new Rook(playerNo,index);
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
                koma.Player != playerNo))
            {
                koma.IsDead = true;
            }
            clickedKoma.SetLocation(left, height);

        }

        internal bool IsPlayerKoma(int left, int height)
        {
            return this.GetAliveKoma().Where((location) =>
                 location.Player == playerNo
                 && location.Left == left
                 && location.Height == height).Count() > 0;
        }

        internal void ChangePlayer()
        {
            playerNo = playerNo == PlayerNo.Two ? PlayerNo.One : PlayerNo.Two;
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
                && koma.Player != playerNo).Count() > 0;
        }

        internal string GetPlayerName()
        {
            return this.context.Players[playerNo].PlayerName;
        }
    }

}
