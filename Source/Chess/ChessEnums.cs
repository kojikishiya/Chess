using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    enum KomaKind
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pone
    }
    enum Color
    {
        Black,
        White
    }
    enum MoveType
    {
        Normal,
        NotExistAny,
        ExistsEnemy,
        Direction

    }
}
