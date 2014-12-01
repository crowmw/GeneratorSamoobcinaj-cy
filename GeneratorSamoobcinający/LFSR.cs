﻿using System;
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
            bool bnew = false;
            string v = "12345";
            var x = v.Count();
            var y = v.Length;


            for (int j = 0; j < 2; j++)
            {
                switch(taps.Count())
                {
                    case 2: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]];
                        break;
                    case 3: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]] ^ bits[bits.Count()-1 - taps[2]];
                        break;
                    case 4: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]] ^ bits[bits.Count()-1 - taps[2]] ^ bits[bits.Count()-1 - taps[3]];
                        break;
                    case 5: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]] ^ bits[bits.Count()-1 - taps[2]] ^ bits[bits.Count()-1 - taps[3]] ^ bits[bits.Count()-1 - taps[4]];
                        break;
                    case 6: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]] ^ bits[bits.Count()-1 - taps[2]] ^ bits[bits.Count()-1 - taps[3]] ^ bits[bits.Count()-1 - taps[4]] ^ bits[bits.Count()-1 - taps[5]];
                        break;
                    case 7: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]] ^ bits[bits.Count()-1 - taps[2]] ^ bits[bits.Count()-1 - taps[3]] ^ bits[bits.Count()-1 - taps[4]] ^ bits[bits.Count()-1 - taps[5]] ^ bits[bits.Count()-1 - taps[6]];
                        break;
                    default: bnew = bits[bits.Count()-1 - taps[0]] ^ bits[bits.Count()-1 - taps[1]];
                        break;
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
