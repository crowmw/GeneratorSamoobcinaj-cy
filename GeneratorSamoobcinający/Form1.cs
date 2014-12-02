using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneratorSamoobcinający
{
    public partial class Form1 : Form
    {
        private StringBuilder outputCiag;
        private int[] taps;
        private byte[] o;
        private string codedText = "";
        private string[] splitted;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string seed = textBox1.Text;
            taps = textBox6.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToArray();
            LFSR lsfr = new LFSR(seed.Length, seed, taps);
            outputCiag = new StringBuilder();

            for (int i = 0; i <= Convert.ToInt32(textBox3.Text); i++)
            {
                gen(lsfr);
            }

            SaveData();
        }

        private void SaveData()
        {
            SaveToTXT();
            SaveToBIN();
            WriteToTextBox();
        }

        private void WriteToTextBox()
        {
            textBox2.Clear();
            textBox2.Text = outputCiag.ToString();
        }

        private void SaveToBIN()
        {
            FileStream fs = new FileStream("out.bin", FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fs);
            o = StringToByte(outputCiag.ToString());
            writer.Write(o);
            writer.Close();
            fs.Close();
        }

        private byte[] StringToByte(string str)
        {
            byte[] b;
            b = new byte[Convert.ToInt32(textBox3.Text) + 1];

            int i = 0;
            foreach (char bit in str)
            {
                if (bit == '1')
                    b[i] = 1;
                else
                    b[i] = 0;
                i++;
            }
            return b;
        }

        private void SaveToTXT()
        {
            FileStream fs = new FileStream("out.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);
            writer.Write(outputCiag);
            writer.Close();
            fs.Close();
        }

        private void gen(LFSR lsfr)
        {
            bool[] output;
            output = lsfr.Output;
            if (output[0] == true)
            {
                if (output[1] == true)
                    outputCiag.Append("1");
                else
                    outputCiag.Append("0");
            }
            else
            {
                lsfr.Shift();
                this.gen(lsfr);
            }
            lsfr.Shift();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox4.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "";
            if (textBox5.Text != "")
            {
                s = File.ReadAllText(textBox5.Text);
                var b = ToBinary(ConvertToByteArray(s));
                splitted = b.Split(' ');
            }

            Xorowanie(splitted);

            StringBuilder sb1 = Sklejanie(splitted);

            codedText = sb1.ToString();
            textBox2.Text = codedText;
            //File.WriteAllText(textBox5.Text, sb1.ToString());

            //Xorowanie(splitted);

            //StringBuilder sb2 = Sklejanie(splitted);
            ////textBox2.Text = sb2.ToString();
            //File.WriteAllText(textBox5.Text, sb2.ToString());
        }

        private StringBuilder Sklejanie(string[] splitted)
        {
            StringBuilder sb1 = new StringBuilder();
            foreach (string letter in splitted)
            {
                var let = GetBytesFromBinaryString(letter);
                var txt = Encoding.ASCII.GetString(let);
                sb1.Append(txt);
            }
            return sb1;
        }

        private void Xorowanie(string[] splitted)
        {
            int x = 0;
            int i = 0;
            foreach (string letter in splitted)
            {
                byte[] letterByte = StringToByteArray(letter);
                int j = 0;
                int[] lit = new int[8];
                foreach (var bit in letterByte)
                {
                    lit[j] = bit ^ o[i];
                    j++;
                    i++;
                }
                splitted[x] = string.Join("", lit);
                x++;
            }
        }

        private static byte[] StringToByteArray(string b)
        {
            int i = 0;
            byte[] byteTab = new byte[8];
            foreach (char bit in b)
            {
                if (bit == '1')
                    byteTab[i] = 1;
                else
                    byteTab[i] = 0;
                i++;
            }

            return byteTab;
        }

        //----------
        public Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }

        //--------------
        public static byte[] ConvertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox5.Text = openFileDialog1.FileName;
        }
    }
}