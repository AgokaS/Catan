
using System;
using System.Windows.Forms;
using static Catan.Game;

namespace Catan
{
    public partial class InvateTraid : UserControl
    {
        public delegate void Traid_D();
        public event Traid_D ETraidComplite;
        public event Traid_D EClose;
        int MaxPlaer;
        int plaer;
        int[,] Traid;
        bool[] Adres;
        PlaerItem[] Item = null;
        public InvateTraid()
        {
            InitializeComponent();

            Traid= new int[2, 5];
            for (int i = 0; i < 5; i++)
            {
                Traid[0, i] = 0;
                Traid[1, i] = 0;
            }
        }

        public void ReadInfo(int plaer,PlaerItem[] Item,int MaxPlaer)
        {
            this.plaer = plaer;
            this.Item = Item;
            this.MaxPlaer = MaxPlaer;
            if (plaer == 0)
                checkBox1.Visible = false;
            else if (plaer == 1)
                checkBox2.Visible = false;
            else if (plaer == 2)
                checkBox3.Visible = false;
            if (MaxPlaer < 4 || plaer == 3)
                checkBox4.Visible = false;
            Adres = new bool[MaxPlaer];
        }
        public bool[] WriteAdres()
        {
            return Adres;
        }
        public int[,] WriteTraid()
        {
            return Traid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) > Item[plaer].Stone || Convert.ToInt32(textBox2.Text) > Item[plaer].Glay || Convert.ToInt32(textBox3.Text) > Item[plaer].Wood || Convert.ToInt32(textBox4.Text) > Item[plaer].Wool || Convert.ToInt32(textBox5.Text) > Item[plaer].Wheat)
            { MessageBox.Show("ВЫ не можете отдать больше чем у вас есть", "Ошибка торговли",MessageBoxButtons.OK); }
            else
            {
                Adres[0] = checkBox1.Checked;
                Adres[1] = checkBox2.Checked;
                Adres[2] = checkBox3.Checked;
                if (MaxPlaer == 4)
                    Adres[3] = checkBox4.Checked;

                Traid[0, 0] = Convert.ToInt32(textBox1.Text);
                Traid[0, 1] = Convert.ToInt32(textBox2.Text);
                Traid[0, 2] = Convert.ToInt32(textBox3.Text);
                Traid[0, 3] = Convert.ToInt32(textBox4.Text);
                Traid[0, 4] = Convert.ToInt32(textBox5.Text);
                Traid[1, 0] = Convert.ToInt32(textBox10.Text);
                Traid[1, 1] = Convert.ToInt32(textBox9.Text);
                Traid[1, 2] = Convert.ToInt32(textBox8.Text);
                Traid[1, 3] = Convert.ToInt32(textBox7.Text);
                Traid[1, 4] = Convert.ToInt32(textBox6.Text);

                ETraidComplite();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EClose();
        }
    }
}
