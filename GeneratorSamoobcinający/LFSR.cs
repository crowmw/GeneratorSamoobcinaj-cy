using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorSamoobcinający
{
    class LFSR
    {
        bool[] bits;

        public LFSR(int bitCount, string seed)
        {
            bits = new bool[bitCount];
            for (int i = 0; i < bitCount; i++)
                bits[i] = seed[i] == '1' ? true : false;
        }

        public string Registry
        {
            get
            {
                char[] tmp = new char[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    tmp[i] = bits[i] ? '1' : '0';
                return new string(tmp);
            }
        }

        public bool[] Output
        {
            get
            {
                return new bool[] { bits[0], bits[1] };
            }
        }

        public void Shift()
        {
            for (int j = 0; j < 2; j++)
            {
                bool bnew = bits[bits.Length - 1] ^ bits[bits.Length - 2];

                for (int i = bits.Length - 1; i > 0; i--)
                {
                    bits[i] = bits[i - 1];
                }
                bits[0] = bnew;
            }
        }
    }
}
