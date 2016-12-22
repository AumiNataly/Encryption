using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AESLibrary;
using RSALibrary;
using SHA_256;
using System.IO;
using System.Text;

namespace Encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string value;
        string keyAES = "0x2b7e151628aed2a6abf7158809cf4f3c";
        string otv2, strFin,pod,otv1;
        int min = 1, max = 100;

        private void button1_Click(object sender, EventArgs e)
        {
            AESCrypto aes = new AESCrypto();
            value = textBox1.Text;
            otv1 = aes.Encrypt(value, keyAES);
            textBox2.Text = Convert.ToString(keyAES);
            textBox3.Text = Convert.ToString(otv1);


            RSALibrary.KluczRSA publiczny;
            RSALibrary.KluczRSA prywatny;
            RSA.WygenerujKlucze(min, max, out publiczny, out prywatny);
            string PublicznyE = publiczny.e.ToString();
            string PublicznyN = publiczny.n.ToString();
            string PrywatnyD = prywatny.d.ToString();
            string PrywatnyN = prywatny.n.ToString();

            textBox4.Text = Convert.ToString(PublicznyE);
            textBox8.Text = Convert.ToString(PublicznyN);
            otv2= RSA.szyfruj(value, publiczny);
            textBox5.Text = Convert.ToString(otv2);


            sha256 sha = new sha256();
            MemoryStream sourceDataStream = new MemoryStream(Encoding.Default.GetBytes(value));
            MemoryStream shaStream = new MemoryStream();
            sha.SHAFile(sourceDataStream, shaStream);
            byte[] a = new byte[32];
            shaStream.Position = 0;
            shaStream.Read(a, 0, a.Length);
            strFin = sha.ArrayToString(a);
            textBox6.Text = Convert.ToString(strFin);

            SHA_256.KluczRSA publica;
            SHA_256.KluczRSA prywat;
            rsa.WygenerujKlucze(min, max, out publica, out prywat);

            string PrywatD = prywat.d.ToString();
            string PrywatN = prywat.n.ToString();

           pod= rsa.szyfruj(strFin, prywat);
            textBox7.Text = Convert.ToString(pod);
        }
    }
}
