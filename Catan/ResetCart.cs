using System;
using System.Windows.Forms;
using static Catan.Game;

namespace Catan
{
    public partial class ResetCart : UserControl
    {
        public delegate void D_Completed();
        public event D_Completed E_Close;
        int CountItem;//количество ресурсов
        int Plaer;
        PlaerItem[] Item;

        public ResetCart()
        {
            InitializeComponent();

        }

        /* Метод для передачи данных из вне на компонет */
        public void ReadInfo(int plaer, PlaerItem[] Item )
        {
            this.Plaer = plaer;
            this.Item = Item;
            CountItem = Item[Plaer].Stone;
            CountItem += Item[Plaer].Wood;
            CountItem += Item[Plaer].Wool;
            CountItem += Item[Plaer].Wheat;
            CountItem += Item[Plaer].Glay;
            if (CountItem < 7)
                E_Close();
            else
            CountItem = CountItem / 2;

            InfoReset();

            HaveStone.Text = Item[Plaer].Stone.ToString();
            HaveGlay.Text = Item[Plaer].Glay.ToString();
            HaveWheat.Text = Item[Plaer].Wheat.ToString();
            HaveWood.Text = Item[Plaer].Wood.ToString();
            HaveWool.Text = Item[Plaer].Wool.ToString();
        }

        private void InfoReset()
        {
            label1.Text = $"Игроку {Plaer+1} нужно сбросить ещё {CountItem} ресурсов";
        }



        /* Реализация внутренней логики компонета */
        private void Back_Click(object sender, EventArgs e)
        {
            ResetGlay.Text = "0";
            ResetStone.Text = "0";
            ResetWood.Text = "0";
            ResetWool.Text = "0";
            ResetWheat.Text = "0";
            CountItem = Item[Plaer].Stone;
            CountItem += Item[Plaer].Wood;
            CountItem += Item[Plaer].Wool;
            CountItem += Item[Plaer].Wheat;
            CountItem += Item[Plaer].Glay;
            InfoReset();
            if (CountItem < 7)
                E_Close();
            CountItem = CountItem / 2;
        }

        private void Glay_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(HaveGlay.Text) - 1 >= Convert.ToInt32(ResetGlay.Text)&CountItem!=0)
            {
                ResetGlay.Text = (Convert.ToInt32(ResetGlay.Text) + 1).ToString();
                CountItem--;
                InfoReset();          
            }
        }

        private void Stone_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(HaveStone.Text) - 1 >= Convert.ToInt32(ResetStone.Text) & CountItem != 0)
            {
                ResetStone.Text = (Convert.ToInt32(ResetStone.Text) + 1).ToString();
                CountItem--;
                InfoReset();
            }
        }

        private void Wood_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(HaveWood.Text) - 1 >= Convert.ToInt32(ResetWood.Text) & CountItem != 0)
            { ResetWood.Text = (Convert.ToInt32(ResetWood.Text) + 1).ToString();
                CountItem--;
                InfoReset();
            }
        }

        private void Wool_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(HaveWool.Text) - 1 >= Convert.ToInt32(ResetWool.Text) & CountItem != 0)
            { ResetWool.Text = (Convert.ToInt32(ResetWool.Text) + 1).ToString();
                CountItem--;
                InfoReset();
            }
        }

        private void Whaet_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(HaveWheat.Text) - 1 >= Convert.ToInt32(ResetWheat.Text) & CountItem != 0)
            { ResetWheat.Text = (Convert.ToInt32(ResetWheat.Text) + 1).ToString();
                CountItem--;
                InfoReset();
            }
        }

        private void Acept_Click(object sender, EventArgs e)
        {
            if (CountItem == 0)
            {
                Item[Plaer].Stone -= Convert.ToInt32(ResetStone.Text);
                Item[Plaer].Glay -= Convert.ToInt32(ResetGlay.Text);
                Item[Plaer].Wood -= Convert.ToInt32(ResetWood.Text);
                Item[Plaer].Wool -= Convert.ToInt32(ResetWool.Text);
                Item[Plaer].Wheat -= Convert.ToInt32(ResetWheat.Text);
                E_Close();
            }

        }
    }
}
