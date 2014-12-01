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
        int[] taps;

        public LFSR(int bitCount, string seed, int[] tap)
        {
            bits = new bool[bitCount];
            taps = tap;
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
                bool bnew = bits[taps[0]] ^ bits[taps[1]];
                for (int i = 2; i < taps.Length; i++)
                {
                    bnew = bnew ^ bits[taps[i]];
                }

                for (int i = this.bits.Count()-1; i > 0; i--)
                {
                    this.bits[i] = this.bits[i - 1];
                }
                this.bits[0] = bnew;
            }
        }
    }
}
