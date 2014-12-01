using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneratorSamoobcinający
{
    public partial class Form1 : Form
    {
        private StringBuilder outputCiag;
        int[] taps;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string seed = textBox1.Text;
            taps = textBox6.Text.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
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
            if (checkBox1.Checked)
            {
                SaveToTXT();
            }

            if (checkBox2.Checked)
            {
                SaveToBIN();
            }

            WriteToTextBox();
        }

        private void WriteToTextBox()
        {
            textBox2.Text = "";
            textBox2.Text = outputCiag.ToString();
        }

        private void SaveToBIN()
        {
            FileStream fs = new FileStream("out.bin", FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(StringToByte(outputCiag.ToString()));
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
            folderBrowserDialog1.ShowDialog();
            textBox4.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(textBox4.Text);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in fileBytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            //File.WriteAllText(outputFilename, sb.ToString());
        }
    }
}
