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
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string seed = textBox1.Text;
            LFSR lsfr = new LFSR(seed.Length, seed);
            outputCiag = new StringBuilder(textBox3.Text);

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
            StreamWriter writer = new StreamWriter(fs);
            writer.Write(outputCiag);
            writer.Close();
            fs.Close();
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
            bool[] outp;
            outp = lsfr.Output;
            if (outp[0] == true)
            {
                if (outp[1] == true)
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
    }
}
