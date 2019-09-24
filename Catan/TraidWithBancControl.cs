using System;
using System.Windows.Forms;
using static Catan.Game;

namespace Catan
{
    public partial class TraidWithBancControl : UserControl
    {
        public delegate void State_D();
        public event State_D ETraidBankClose;
        int Plaer;
        PlaerItem[] Item;
        int ClicIn;
        int TraidOn;
        public TraidWithBancControl()
        {
            InitializeComponent();
            ClicIn = 0;

            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
        }


        public void ReadInfo(int Plaer, PlaerItem[] Item)
        {
            this.Plaer = Plaer;
            this.Item = Item;
            label1.Text = $"{Item[Plaer].StonePrise}:1";
            label2.Text = $"{Item[Plaer].GlayPrise}:1";
            label3.Text = $"{Item[Plaer].WoodPrise}:1";
            label4.Text = $"{Item[Plaer].WoolPrise}:1";
            label5.Text = $"{Item[Plaer].WheatPrise}:1";
        }

        private void SetVisableTrue()
        {
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
        }
        private void MesEr()
        {
            MessageBox.Show("Вы не можете отдать больше чем у вас есть", "Ошибка торговли");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Item[Plaer].Stone >= Item[Plaer].StonePrise)
            {
                SetVisableTrue();
                ClicIn = 1;
            }
            else MesEr();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Item[Plaer].Glay >= Item[Plaer].GlayPrise)
            {
                SetVisableTrue();
                ClicIn = 2;
            }
            else MesEr();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Item[Plaer].Wood >= Item[Plaer].WoodPrise)
            {
                SetVisableTrue();
                ClicIn = 3;
            }
            else MesEr();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Item[Plaer].Wool >= Item[Plaer].WoolPrise)
            {
                SetVisableTrue();
                ClicIn = 4;
            }
            else MesEr();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Item[Plaer].Wheat >= Item[Plaer].WheatPrise)
            {
                SetVisableTrue();
                ClicIn = 5;
            }
            else MesEr();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TraidOn = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TraidOn = 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TraidOn = 3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TraidOn = 4;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TraidOn = 5;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ETraidBankClose();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            switch (ClicIn)
            {
                case 1: Item[Plaer].Stone -= Item[Plaer].StonePrise;break;
                case 2: Item[Plaer].Glay -= Item[Plaer].GlayPrise;break;
                case 3: Item[Plaer].Wood -= Item[Plaer].WoodPrise;break;
                case 4: Item[Plaer].Wool -= Item[Plaer].WoolPrise;break;
                case 5: Item[Plaer].Wheat -= Item[Plaer].WheatPrise;break;
            }
            switch (TraidOn)
            {
                case 1: Item[Plaer].Stone ++; break;
                case 2: Item[Plaer].Glay ++; break;
                case 3: Item[Plaer].Wood ++; break;
                case 4: Item[Plaer].Wool ++; break;
                case 5: Item[Plaer].Wheat ++; break;
            }
            ETraidBankClose();
        }

    }
}
