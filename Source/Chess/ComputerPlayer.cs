using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
     class ComputerPlayer
    {
         private Color cpuColor;
         public ComputerPlayer(Color cpuColor)
         {
             this.cpuColor = cpuColor;
         }
         public Tuple<int, int> Think(BoardManager komaList)
         {

             var komaListClone = komaList.Clone();
             komaListClone[new Random().Next(komaListClone.Count() - 1)]
                 .GetMovableLoacation();
         }
    }

   
}
