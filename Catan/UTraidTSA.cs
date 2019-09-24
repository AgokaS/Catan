using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Catan.Game;

namespace Catan
{
    public partial class UTraidTSA : UserControl
    {
        public delegate void UTraidTSA_D();
        public event UTraidTSA_D EClose;
        RadioButton r1 = null, r2 = null, r3 = null, r4 = null;
        PlaerItem[] Item = null;
        int plaer;
        int[,] traid;

        public UTraidTSA()
        {
            InitializeComponent();
            checkBox4.Visible = false;

            r1 = new RadioButton
            {
                Location = new Point(50, 100),
                Size= new Size(20,20),
                Visible = false
            };
            Controls.Add(r1);
            r2 = new RadioButton
            {
                Location = new Point(140, 100),
                Size = new Size(20, 20),
                Visible = false
            };
            Controls.Add(r2);
            r3 = new RadioButton
            {
                Location = new Point(230, 100),
                Size = new Size(20, 20),
                Visible = false
            };
            Controls.Add(r3);
            r4 = new RadioButton
            {
                Location = new Point(315, 100),
                Size = new Size(20, 20),
                Visible = false
            };
            Controls.Add(r4);

        }

        public void ReadInfo(int Plaer, PlaerItem[] Items, bool[] adres, int[,] traid)
        {
            this.plaer = Plaer;
            this.Item = Items;
            this.traid = traid;

            checkBox1.Visible = adres[0];
            checkBox2.Visible = adres[1];
            checkBox3.Visible = adres[2];
            if (adres.Length==4)
                checkBox4.Visible = adres[3];

            int PorPs = 0;
            foreach (bool x in adres)
                if (x)
                    PorPs++;

            label1.Text = $"Игрок №{plaer} предлагает {(PorPs > 1 ? $"игрокам {(adres[0]?"1":"")} {(adres[1] ? "2" : "")} {(adres[2] ? "3" : "")} {(adres.Length==4?(adres[4] ? "4" : ""):"")}": $"игроку {(adres[0] ? "1" : "")} {(adres[1] ? "2" : "")} {(adres[2] ? "3" : "")} {(adres.Length == 4 ? (adres[4] ? "4" : "") : "")}")}";
            label2.Text = $"обменять Руда*{traid[1, 0]} Глина*{traid[1, 1]} Дерево*{traid[1, 2]} Шерсть*{traid[1, 3]} Пшеница*{traid[1, 4]}";
            label3.Text = $"на Руда*{traid[0, 0]} Глина*{traid[0, 1]} Дерево*{traid[0, 2]} Шерсть*{traid[0, 3]} Пшеница*{traid[0, 4]}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (r1.Checked)
            {
                GoTraid(plaer, 0, 1);
                GoTraid(0, 1, 0);
                EClose();
            }
            if (r2.Checked)
            {
                GoTraid(plaer, 0, 1);
                GoTraid(1, 1, 0);
                EClose();
            }
            if (r3.Checked)
            {
                GoTraid(plaer, 0, 1);
                GoTraid(2, 1, 0);
                EClose();
            }
            if (r4.Checked)
            {
                GoTraid(plaer, 0, 1);
                GoTraid(3, 1, 0);
                EClose();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Item[0].Stone >= traid[1, 0] && Item[0].Glay >= traid[1, 1] && Item[0].Wood >= traid[1, 2] && Item[0].Wool >= traid[1, 3] && Item[0].Wheat >= traid[1, 4])
                r1.Visible = checkBox1.Checked;
            else MessageBox.Show("Вы не можете отдать больше чем у вас есть", "Ошибка торговли", MessageBoxButtons.OK);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (Item[1].Stone >= traid[1, 0] && Item[1].Glay >= traid[1, 1] && Item[1].Wood >= traid[1, 2] && Item[1].Wool >= traid[1, 3] && Item[1].Wheat >= traid[1, 4])
                r2.Visible = checkBox2.Checked;
            else MessageBox.Show("Вы не можете отдать больше чем у вас есть", "Ошибка торговли", MessageBoxButtons.OK);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (Item[2].Stone >= traid[1, 0] && Item[2].Glay >= traid[1, 1] && Item[2].Wood >= traid[1, 2] && Item[2].Wool >= traid[1, 3] && Item[2].Wheat >= traid[1, 4])
                r3.Visible = checkBox3.Checked;
            else MessageBox.Show("Вы не можете отдать больше чем у вас есть", "Ошибка торговли", MessageBoxButtons.OK);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (Item[3].Stone >= traid[1, 0] && Item[3].Glay >= traid[1, 1] && Item[3].Wood >= traid[1, 2] && Item[3].Wool >= traid[1, 3] && Item[3].Wheat >= traid[1, 4])
                r4.Visible = checkBox4.Checked;
            else MessageBox.Show("Вы не можете отдать больше чем у вас есть", "Ошибка торговли", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EClose();
        }

        private void GoTraid( int plaer, int away, int gets )//осуществление обмена
        {
            Item[plaer].Stone -= traid[away,0];
            Item[plaer].Glay -= traid[away,1];
            Item[plaer].Wood -= traid[away,2];
            Item[plaer].Wool -= traid[away,3];
            Item[plaer].Wheat -= traid[away,4];

            Item[plaer].Stone += traid[gets, 0];
            Item[plaer].Glay += traid[gets, 1];
            Item[plaer].Wood += traid[gets, 2];
            Item[plaer].Wool += traid[gets, 3];
            Item[plaer].Wheat += traid[gets, 4];
        }
    }
}
