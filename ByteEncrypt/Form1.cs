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
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace ByteEncrypt
{
    public partial class Form1 : Form
    {
        string text;
        public Form1()
        {
            InitializeComponent();
            text = "";
        }

        bool check_terminator()
        {
            return text.Contains("$finish$");
        }

        int extract_text(string location)
        {
            try
            {
                /*Bitmap bt = new Bitmap(location);

                int height = bt.Height;
                int width = bt.Width;
                int culoare ;
                int contor=0;

                byte litera=0x00;
                byte rez = 0x00;
                byte masca = 0x03;
                byte r, g, b;

                Color c;

                int i = 0, j = 0;

                while (i < height)
                {
                    while (j < width)
                    {
                        c = bt.GetPixel(j, i);
                        culoare = 0;
                        while (culoare < 3) 
                        {
                            if (culoare == 0)//r
                            {
                                r = c.R;
                                rez = (byte)(r & masca);
                                rez = (byte)(rez << contor % 8);
                                litera = (byte)(litera | rez);

                            }
                            else if (culoare==1)//g
                            {
                                g = c.G;
                                rez = (byte)(g & masca);
                                rez = (byte)(rez << contor % 8);
                                litera = (byte)(litera | rez);
                            }
                            else if (culoare==2)//b
                            {
                                b = c.B;
                                rez = (byte)(b & masca);
                                rez = (byte)(rez << contor % 8);
                                litera = (byte)(litera | rez);
                            }

                            contor += 2;
                            if (contor%8==0)
                            {
                                text += (char)(litera);
                                litera = 0x00;
                            }
                            culoare++;
                            //if (check_terminator())
                              //  return 1;
                        }
                        Console.WriteLine(j);
                        j++;
                    }
                    i++;
                }*/

                byte masca = 0x03;
                byte rezultat;
                byte cuvant = 0x00;
                byte[] data=File.ReadAllBytes(location);

                int contor = 200, i=0;

                while(contor<data.Length-200)
                {
                    Console.WriteLine(data[contor]);
                    rezultat = (byte)(data[contor] & masca);
                    rezultat = (byte)(rezultat << i % 8);
                    cuvant = (byte)(cuvant | rezultat);
                    if(i%8==0)
                    {
                        text += (char)cuvant;
                        cuvant = 0x00;
                    }
                    i += 2;
                    contor++;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fisier deschis de alt proces" + ex.ToString());
                return 2;
            }

            return 0;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = false;
            if (od.ShowDialog() == DialogResult.OK)
            {
                txtPathMask.Text = od.FileName;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if(!File.Exists(txtPathMask.Text))
            {
                MessageBox.Show("Fisierul nu exista");
                return;
            }
            if(String.IsNullOrEmpty(txtText.Text))
            {
                MessageBox.Show("Introduceti textul pt codificat");
                return;
            }

            try
            {

                /* byte[] text = Encoding.ASCII.GetBytes(txtText.Text+"$finish$");
                 int a=text.Length;
                 byte[] ab =File.ReadAllBytes(txtPathMask.Text);
                 Bitmap bm = new Bitmap(txtPathMask.Text);
                 int height=bm.Height;
                 int width=bm.Width;

                 int i = 0, j = 0;//pixeli
                 int cuvant = 0;//cuvant/4=>actual word, word%4=>byte
                 int colour = 0;

                 byte masca = 0x03;
                 byte rezultat,r=0,g=0,b=0;

                 Color c;

                 if (text.Length>height*width/4)
                 {
                     MessageBox.Show("Textul este prea lung pentru imaginea aleasa!");
                     return;
                 }
                 while(i<=height&& cuvant<text.Length*8)
                 {

                     while(j<=width&& cuvant <text.Length*8)
                     {
                         c=bm.GetPixel(j, i);
                         colour = 0;
                         while(colour<3&& cuvant < text.Length * 8)
                         {
                             if(colour==0)//r
                             {
                                 rezultat = (byte)(masca & text[cuvant / 8]);
                                 rezultat = (byte)(rezultat >> cuvant % 8);//shiftam pt a aduce cei 2 butu de informatii pe poz 0,1
                                 rezultat = (byte)(rezultat|0xFC);//punem bitii 7..2 pe 1
                                 r = c.R;
                                 r = (byte)(r & rezultat);
                             }
                             else if(colour==1)//g
                             {
                                 rezultat = (byte)(masca & text[cuvant / 8]);
                                 rezultat = (byte)(rezultat >> cuvant % 8);
                                 rezultat = (byte)(rezultat | 0xFC);
                                 g = c.G;
                                 g = (byte)(g & rezultat);
                             }
                             else if(colour==2)//b
                             {
                                 rezultat = (byte)(masca & text[cuvant / 8]);
                                 rezultat = (byte)(rezultat >> cuvant % 8);
                                 rezultat = (byte)(rezultat | 0xFC);
                                 b = c.B;
                                 b = (byte)(b & rezultat);
                             }

                             cuvant+=2;
                             colour++;

                             if(masca==0xC0)
                             {
                                 masca = 0x03;
                             }
                             else
                             {
                                 masca=(byte)(masca << 2);
                             }
                         }

                         bm.SetPixel(j, i, Color.FromArgb((int)r,(int) g,(int) b));

                         ++j;   
                     }
                     ++i;
                 }
                 String fileExt = Path.GetExtension(txtPathMask.Text);
                 SaveFileDialog sd = new SaveFileDialog();
                 sd.Filter = "Files (" + fileExt + ") | " + fileExt;
                 if (sd.ShowDialog() == DialogResult.OK)
                 {
                     bm.Save(sd.FileName);
                 }*/

                byte[] text = Encoding.ASCII.GetBytes(txtText.Text + "$finish$");
                byte[] data= File.ReadAllBytes(txtPathMask.Text);

                byte masca = 0x03;
                byte rezultat = 0x00;

                int i = 200,contor=0;

                while(i<data.Length-200&&contor<text.Length*8)
                {
                    masca = 0x03;
                    masca = (byte)(masca << contor % 8);
                    rezultat = (byte)(masca & text[contor/8]);
                    rezultat = (byte)(rezultat >> contor % 8);
                    rezultat = (byte)(rezultat | 0xFC);
                    data[i] = (byte)(data[i]&rezultat);
                    i++;
                    contor +=2;
                    Console.WriteLine(i);
                }
                String fileExt = Path.GetExtension(txtPathMask.Text);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Files (" + fileExt + ") | " + fileExt;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sd.FileName, data);
                }
                }
            catch(Exception ex)
            {
                MessageBox.Show("Efisierul nu s-a putut deschide"+ex.ToString());
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtPathMask.Text))
            {
                MessageBox.Show("Fisierul nu exista");
                return;
            }
            
            int rez = extract_text( txtPathMask.Text);
            if(rez==0)
            {
                string fileExt = ".txt";
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Files (" + fileExt + ") | " + fileExt;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sd.FileName, text);
                }
            }
            else if (rez==1)
            {
                MessageBox.Show("Nu a fost cryptat niciun mesaj in acest fisier");
            }
        }
    }
}
