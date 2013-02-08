using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Chess
{
    static class KomaListExtention
    {
        public static T Clone<T>(this T target)
        {

            BinaryFormatter b = new BinaryFormatter();
            T result;
            using (var mem = new MemoryStream())
            {

                b.Serialize(mem, target);
                mem.Position = 0;
                result = (T)b.Deserialize(mem);
            }
            return result;

        }
    }


}
