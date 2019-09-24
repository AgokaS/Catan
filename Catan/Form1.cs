using System;
using System.Drawing;
using System.Windows.Forms;

namespace Catan
{

    /********************************************
     *                                          *
     *  Name:Catan                              *
     *  Author: Sidorov Egor   PIn-118          *
     *                                          *
     *  Курсовя рабоат Сидорова Егора Юрьевича  *
     *                                          *
     *                       xx/xx/2019         *
      ****************************************/
    public partial class Form1 : Form
    {
        int ProgresTwo;
        int MaxPlaer;
        int plaer;
        int StartTour;
        bool FOne = false, FGetAny = false;
        public int maxarmy, maxroad;
        public bool StartGame = true;
        public Bitmap game_map_back = null;
        public Bitmap game_map_roge = null;
        public Label Info_Plaer_Count = null;
        public Label Info_Wood = null;
        public Label Info_Stone = null;
        public Label Info_Glay = null;
        public Label Info_Wheat = null;
        public Label Info_Wool = null;
        public Label Info_Army = null;
        public Label Info_Town = null;
        public Label Info_City = null;
        public Label Info_LRoad = null;
        public Label Info_WinPoint = null;
        
        Button StartHext = null;
        Button StartRoad = null;
        Button StartEndTour = null;
        public PictureBox Game_map = null;
        private Game g1 = null;
        GroupBox GroupRadioRoad = null;
        GroupBox GroupRadioHext = null;
        GroupBox GroupRoge = null;
        GroupBox GRes = null;
        Dase d = null;
        int lastx = 1, lasty = 1;
        Hex[][] Hexs = null;
        UTraidTSA TraidAcept = null;
        InvateTraid UTraid = null;
        TraidWithBancControl UTraidBank = null;
        ResetCart UReset = null;
        int RogeRobPlaer;
        public int HexRadio(int i)
        {
            int num;
            switch (i)
            {
                case 0:
                case 5:
                    num = 7;
                    break;
                case 1:
                case 4:
                    num = 9;
                    break;
                default:
                    num = 11;
                    break;
            }
            return num;
        }
        public Form1()
        {

            InitializeComponent();

            var result = MessageBox.Show("Создать расширенную игру на четверых?\n ДА - Создать для четверых, НЕТ - Создать для троих", "Создание игры", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                MaxPlaer = 4;
            else MaxPlaer = 3;
            // Win32.AllocConsole();//Консоль для тестирования 
            maxarmy = 2;
            maxroad = 4;
            RogeRobPlaer=0;
            plaer = 1;
            g1 = new Game(MaxPlaer);//Иницилизация основного класса. Все важные условия при старте (игровое поле, карточки развития)
            d = new Dase();//Класс кубиков. Генерация событий бросков

            /*  Cоздание гексов игрового поля*/
            {
                int[,] loc = new int[2, 19] { { 0, 0, 0, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 1, 2, 3, 3, 2, 2 }, { 0, 1, 2, 3, 4, 3, 2, 1, 0, 0, 0, 0, 1, 2, 3, 2, 1, 1, 2 } };

                Hexs = new Hex[5][];
                Hexs[0] = new Hex[3];
                Hexs[1] = new Hex[4];
                Hexs[2] = new Hex[5];
                Hexs[3] = new Hex[4];
                Hexs[4] = new Hex[3];
                int c = 0;
                for (int i = 0; i < 19; i++)
                {
                    if (g1.Info_deck_type_s(i) != 0)
                    {
                        HexResource h = new HexResource(loc[0, i], loc[1, i], g1.Info_deck_type_s(i), d, g1.Info_deck_num(c), g1);
                        c++;
                        Hexs[loc[0, i]][loc[1, i]] = h;
                        h.RefreshInfo += Info_Plaer;
                    }
                    else
                    {
                        Hex h = new Hex(g1.Info_deck_type_s(i), d);
                        Hexs[loc[0, i]][loc[1, i]] = h;
                        lastx = loc[0, i];
                        lasty = loc[1, i];
                    }
                }
            }


            /* отрисовка формы  */

            CreateTown.Visible = false;
            AceptTown.Visible = false;
            CloseTown.Visible = false;
            CreatRoad.Visible = false;
            AceptRoad.Visible = false;
            button6.Visible = false;
            Roll.Visible = false;
            button8.Visible = false;
            EndTour.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            CloseRoad.Visible = false;
            BTraidBank.Visible = false;
            BTraidPlaers.Visible = false;
            BuyCart.Visible = false;
            UseArmy.Visible = false;
            One.Visible = false;
            TroRoad.Visible = false;
            GetAnyRes.Visible = false;
            label2.Visible = false;
            label1.Visible = false;

            Draw_map();

            StartButton();
            CreateInfoZon();
            Info_Plaer();



            /*  Подписки */
            d.EventRoll7 += RogeRes;
        }
        /* Кнопки начала Игры */
        private void StartButton()
        {
            StartTour = 1;

            StartHext = new Button
            {
                Location = new Point(10, 700),
                Text = "Построить поселение",
                Size = new Size(150, 25),
            };
            StartHext.Click += new EventHandler(StartHext_clic);
            StartRoad = new Button
            {
                Location = new Point(10, 730),
                Text = "Построить дорогу ",
                Size = new Size(150, 25),
                Visible = false,
            };
            StartRoad.Click += new EventHandler(StartRoad_clic);
            StartEndTour = new Button
            {
                Location = new Point(10, 760),
                Text = "Закончить ход",
                Size = new Size(150, 25),
                Visible = false,
            };
            StartEndTour.Click += new EventHandler(StartEndTour_clic);
            RadHext();

            RightPanel.Controls.Add(StartHext);
            RightPanel.Controls.Add(StartRoad);
            RightPanel.Controls.Add(StartEndTour);
        }
        private void StartRoad_clic(object sender, EventArgs e)
        {
            for (int index = 0; index < GroupRadioRoad.Controls.Count; ++index)
            {
                if (((RadioButton)GroupRadioRoad.Controls[index]).Checked)
                {
                    string[] strArray = GroupRadioRoad.Controls[index].Tag.ToString().Split(' ');
                    g1.Set_road(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), plaer);
                    Pen mypen;
                    if (plaer == 1)
                    { mypen = new Pen(Color.Red, 5); }
                    else if (plaer == 2)
                    { mypen = new Pen(Color.Blue, 5); }
                    else if (plaer == 3)
                    { mypen = new Pen(Color.Green, 5); }
                    else { mypen = new Pen(Color.Magenta, 5); }
                    Bitmap bmp = (Bitmap)game_map_back.Clone();
                    Graphics g = Graphics.FromImage(bmp);

                    Console.WriteLine(GroupRadioRoad.Controls[index].Tag.ToString());

                    if (Convert.ToInt32(strArray[2]) % 2 != 0)
                    { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X - 10, GroupRadioRoad.Controls[index].Location.Y + 10, GroupRadioRoad.Controls[index].Location.X + 20, GroupRadioRoad.Controls[index].Location.Y + 10); }
                    else if ((Convert.ToInt32(strArray[2]) < 5 & Convert.ToInt32(strArray[3]) % 2 > 0) || (Convert.ToInt32(strArray[2]) > 5 & Convert.ToInt32(strArray[3]) % 2 == 0))
                    { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X - 8, GroupRadioRoad.Controls[index].Location.Y - 8, GroupRadioRoad.Controls[index].Location.X + 10, GroupRadioRoad.Controls[index].Location.Y + 20); }
                    else
                    { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X + 15, GroupRadioRoad.Controls[index].Location.Y - 12, GroupRadioRoad.Controls[index].Location.X - 5, GroupRadioRoad.Controls[index].Location.Y + 20); }
                    game_map_back =(Bitmap)bmp.Clone();

                    g.DrawImage(game_map_roge, 0, 0);
                    Game_map.Image = bmp;
                    GroupRadioRoad.Controls.Clear();
                    Game_map.Controls.Remove(GroupRadioRoad);
                    StartRoad.Visible = false;
                    StartEndTour.Visible = true;
                }
            }

        }
        private void StartEndTour_clic(object sender, EventArgs e)
        {
            if (StartTour == MaxPlaer * 2)
            {
                StartGame = false;
                Roll.Visible = true;
                label2.Visible = true;
                d.StartResourse();

                RightPanel.Controls.Remove(StartHext);
                RightPanel.Controls.Remove(StartRoad);
                RightPanel.Controls.Remove(StartEndTour);
            }
            else
            {
                if (StartTour < MaxPlaer)
                    plaer++;
                else if (StartTour > MaxPlaer)
                    plaer--;
                Info_Plaer();
                RadHext();
                StartTour++;
                StartEndTour.Visible = false;
                StartHext.Visible = true;
            }

        }
        private void StartHext_clic(object sender, EventArgs e)
        {
            for (int index = 0; index < GroupRadioHext.Controls.Count; ++index)
            {
                if (((RadioButton)GroupRadioHext.Controls[index]).Checked)
                {
                    string[] strArray = GroupRadioHext.Controls[index].Tag.ToString().Split(' ');
                    g1.Set_hex_t(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), plaer);
                    g1.PriseTraid(plaer-1, Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
                    Pen mypen;
                    if (plaer == 1)
                    { mypen = new Pen(Color.Red, 5); }
                    else if (plaer == 2)
                    { mypen = new Pen(Color.Blue, 5); }
                    else if (plaer == 3)
                    { mypen = new Pen(Color.Green, 5); }
                    else { mypen = new Pen(Color.Magenta, 5); }
                    Bitmap bmp = (Bitmap)game_map_back.Clone();
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawRectangle(mypen, GroupRadioHext.Controls[index].Location.X, GroupRadioHext.Controls[index].Location.Y, 10, 10);

                    game_map_back = (Bitmap)bmp.Clone();
                    g.DrawImage(game_map_roge, 0, 0);
                    Game_map.Image = bmp;

                    GroupRadioHext.Controls.Clear();

                    Game_map.Controls.Remove(GroupRoge);
                    Game_map.Controls.Remove(GroupRadioHext);
                    Game_map.Controls.Add(GroupRadioRoad);

                    for (int i = 0; i < 11; ++i)
                    {
                        for (int j = 0; j < (i % 2 == 0 ? (i < 5 ? 6 + i : 16 - i) : (i < 6 ? i / 2 + 4 : 8 - i / 2)); ++j)
                        {
                            g1.RoadToRAM(i, j, out int r1, out int r2);
                            g1.RoadToHext(i, j, out int h1i, out int h1j, out int h2i, out int h2j);
                            if (g1.Info_road(r1, r2) == 0 & ((Convert.ToInt32(strArray[0]) == h1i && Convert.ToInt32(strArray[1]) == h1j) || (Convert.ToInt32(strArray[0]) == h2i && Convert.ToInt32(strArray[1]) == h2j)))
                            {
                                RadioButton radioButton = new RadioButton();
                                radioButton.Location = new Point(110 + i * 50, i < 6 ? (i % 2 == 0 ? 260 + 55 * j - 55 * (i / 2) : 230 + 110 * j - 55 * (i / 2)) : (i % 2 == 0 ? 150 + 55 * j + 55 * ((i - 6) / 2) : 120 + 110 * j + 55 * ((i - 5) / 2)));
                                radioButton.Size = new Size(20, 20);
                                radioButton.Tag = string.Format("{0} {1} {2} {3}", r1, r2, i, j);
                                GroupRadioRoad.Controls.Add(radioButton);
                            }
                        }
                    }
                    StartHext.Visible = false;
                    StartRoad.Visible = true;
                }
            }
        }
        /*  Методы работы с собственными элементов управления  */
        private void AskTraid()
        {         
            var traid = UTraid.WriteTraid();
            var adres = UTraid.WriteAdres();
            RightPanel.Controls.Remove(UTraid);

        }
        private void CloseTraidBank()
        {
            RightPanel.Controls.Remove(UTraidBank);
            Info_Plaer();
        }
        private void CloseTraid()
        {
            var adres = UTraid.WriteAdres();
            var traid = UTraid.WriteTraid();
            RightPanel.Controls.Remove(UTraid);
            TraidAcept = new UTraidTSA
            {
                Location = new Point(10, 200),
            };
            TraidAcept.ReadInfo(plaer-1, g1.Plaers, adres, traid);
            TraidAcept.EClose += CloseAcept;
            RightPanel.Controls.Add(TraidAcept);          
        }
        private void CloseAcept()
        {
            RightPanel.Controls.Remove(TraidAcept);
            Info_Plaer();
        }
        private void CloseTraidNull()
        {
            RightPanel.Controls.Remove(UTraid);
        }
        /* игровые события и методы их отрисовки  на игровом поле */
        private void Draw_map()//отрисовка игорой карты 
        {
            Game_map = new PictureBox();
            Game_map.Size = new Size(GameMap.Width, GameMap.Height);
            GameMap.Controls.Add(Game_map);

            GroupRadioHext = new GroupBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            GroupRadioRoad = new GroupBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            GroupRoge = new GroupBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            Bitmap bitmap = new Bitmap(Game_map.Width, Game_map.Height);
            game_map_roge = new Bitmap(Game_map.Width, Game_map.Height);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            for (int index = 0; index < 7; ++index)
            {
                graphics.DrawImage(ImgMap.hexsea, index * 100, index < 4 ? 170 - 56 * index : 5 + 56 * (index - 3));
                graphics.DrawImage(ImgMap.hexsea, index * 100, index < 4 ? 510 + 56 * index : 678 - 56 * (index - 3));
            }
            graphics.DrawImage(ImgMap.hexsea, 0, 282);
            graphics.DrawImage(ImgMap.hexsea, 0, 394);
            graphics.DrawImage(ImgMap.hexsea, 600, 282);
            graphics.DrawImage(ImgMap.hexsea, 600, 394);
            int savex = 0, savey = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < (i < 2 ? 3 + i : 7 - i); j++)
                {
                    switch (Hexs[i][j].Info_Type())
                    {
                        case 0:
                            graphics.DrawImage(ImgMap.hexdesert, 100 + 100 * i, i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2));
                            savex = 100 + 100 * i;
                            savey = i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2);
                            break;
                        case 1:
                            graphics.DrawImage(ImgMap.hexstone, 100 + 100 * i, i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2));
                            break;
                        case 2:
                            graphics.DrawImage(ImgMap.hexglay, 100 + 100 * i, i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2));
                            break;
                        case 3:
                            graphics.DrawImage(ImgMap.hexforest, 100 + 100 * i, i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2));
                            break;
                        case 4:
                            graphics.DrawImage(ImgMap.hexwhael, 100 + 100 * i, i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2));
                            break;
                        case 5:
                            graphics.DrawImage(ImgMap.hexwool, 100 + 100 * i, i < 2 ? 228 + 112 * j - 56 * i : 116 + 112 * j + 56 * (i - 2));
                            break;
                    }

                    if (Hexs[i][j].Info_Type() != 0)
                    {
                        HexResource t = (HexResource)Hexs[i][j];
                        Label r = new Label
                        {
                            Location = new Point(160 + 100 * i, i < 3 ? 315 + 110 * j - 55 * i : 200 + 110 * j + 55 * (i - 2)),
                            Text = t.Roll_number.ToString(),
                            AutoSize = true,
                        };
                        Game_map.Controls.Add(r);
                    }
                }
            Graphics gb = Graphics.FromImage(game_map_roge);
            game_map_back = (Bitmap)bitmap.Clone();
            gb.DrawImage(ImgMap.Roge, savex + 50, savey + 30, 30, 50);
            graphics.DrawImage(game_map_roge, 0, 0);
            Game_map.Image = bitmap;
        }
        private void CreateInfoZon()
        {
            int y = 0;
            Info_Wood = new Label
            {
                Location = new Point(10, 40 + 25 * y)
            };
            Info_Army = new Label
            {
                Location = new Point(150, 40 + 25 * y)
            };
            y++;
            Info_Wool = new Label
            {
                Location = new Point(10, 40 + 25 * y)
            };
            Info_Town = new Label
            {
                Location = new Point(150, 40 + 25 * y)
            };

            y++;
            Info_Stone = new Label
            {
                Location = new Point(10, 40 + 25 * y)
            };
            Info_City = new Label
            {
                Location = new Point(150, 40 + 25 * y)
            };

            y++;
            Info_Glay = new Label
            {
                Location = new Point(10, 40 + 25 * y)
            };
            Info_LRoad = new Label
            {
                Location = new Point(150, 40 + 25 * y)
            };

            y++;
            Info_Wheat = new Label
            {
                Location = new Point(10, 40 + 25 * y)
            };
            Info_WinPoint = new Label
            {
                Location = new Point(150, 40 + 25 * y),
                AutoSize = true,
            };
            Info_Plaer_Count = new Label
            {
                Location = new Point(10, 10),
                AutoSize = true, 
            };
            RightPanel.Controls.Add(Info_Plaer_Count);
            RightPanel.Controls.Add(Info_Army);
            RightPanel.Controls.Add(Info_Town);
            RightPanel.Controls.Add(Info_City);
            RightPanel.Controls.Add(Info_LRoad);
            RightPanel.Controls.Add(Info_WinPoint);
            RightPanel.Controls.Add(Info_Wood);
            RightPanel.Controls.Add(Info_Wool);
            RightPanel.Controls.Add(Info_Wheat);
            RightPanel.Controls.Add(Info_Stone);
            RightPanel.Controls.Add(Info_Glay);
        }
        private void Info_Plaer()//Отображает информацию о игроке
        {
            g1.Plaers[plaer - 1].LRoad = g1.StartPoint(plaer);           
            if (g1.FLonger(plaer - 1, maxroad))
            {
                g1.Plaers[plaer-1].FLongerRoad = true;
                maxroad = g1.Plaers[plaer - 1].LRoad;
                for (int i = 0; i < MaxPlaer; i++)
                    if (i != plaer-1)
                        g1.Plaers[i].FLongerRoad = false;
            };

            if (g1.FBiggest(plaer - 1, maxarmy))
            {
                g1.Plaers[plaer-1].FBiggestArmy = true;
                maxarmy = g1.Plaers[plaer - 1].Army;
                for (int i = 0; i < MaxPlaer; i++)
                    if (i != plaer-1)
                        g1.Plaers[i].FBiggestArmy = false;
            };
          
            g1.ToCountWinPoint(plaer - 1);

            label1.Text = g1.StartPoint(plaer).ToString();
            Info_Plaer_Count.Text = $"Ход игрока под №{plaer}";
            Info_Wood.Text = $"Древесины: {g1.Plaers[plaer - 1].Wood}";
            Info_Stone.Text = $"Руды: {g1.Plaers[plaer - 1].Stone}";
            Info_Wool.Text = $"Шерсти: {g1.Plaers[plaer - 1].Wool}";
            Info_Wheat.Text = $"Пшеницы: {g1.Plaers[plaer - 1].Wheat}";
            Info_Glay.Text = $"Глины: {g1.Plaers[plaer - 1].Glay}";
            Info_WinPoint.Text = $"Победных очков: {g1.Plaers[plaer - 1].WinPoint}";
            Info_Army.Text = $"Рыцарей: {g1.Plaers[plaer - 1].Army}";
            Info_Town.Text = $"Поселений: {g1.Plaers[plaer - 1].Town} из 5";
            Info_City.Text = $"Городов: {g1.Plaers[plaer - 1].City} из 4";
            Info_LRoad.Text = $"Длина тракта: {g1.Plaers[plaer - 1].LRoad}";

            if (g1.Plaers[plaer - 1].WinPoint >= 10)
                MessageBox.Show($"Игра закончина. Победил Игрок №{plaer}");
            

        }

        private void RogeRes()
        {
            RogeRobPlaer++;
            if (RogeRobPlaer < MaxPlaer+1)
            {               
                RightPanel.Controls.Remove(UReset);
                UReset = new ResetCart
                {
                    Location = new Point(10, 200)
                };
                RightPanel.Controls.Add(UReset);
                UReset.E_Close += RogeRes;
                UReset.ReadInfo(RogeRobPlaer-1, g1.Plaers);                                                                           
            }
            else
            {
                RightPanel.Controls.Remove(UReset);
                RogeDrive();
            }

        }
        private void RogeDrive()//отрисовка и логка события разбойников
        {
            
            Game_map.Controls.Remove(GroupRadioHext);
            Game_map.Controls.Remove(GroupRadioRoad);
            Game_map.Controls.Add(GroupRoge);

            CreateTown.Visible = false;
            CreatRoad.Visible = false;
            EndTour.Visible = false;
            BTraidBank.Visible = false;
            BTraidPlaers.Visible = false;
            BuyCart.Visible = false;
            CreatRoad.Visible = false;
            button8.Visible = true;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < (i < 2 ? 3 + i : 7 - i); j++)
                    if (!(i == lastx & j == lasty))
                    {
                        RadioButton BRoge = new RadioButton
                        {
                            Location = new Point(160 + 100 * i, i < 3 ? 290 + 110 * j - 55 * i : 175 + 110 * j + 55 * (i - 2)),
                            Size = new Size(20, 20),
                            Tag = string.Format("{0} {1}", i, j),
                        };
                        GroupRoge.Controls.Add(BRoge);
                    }
        }
        private void RadHext()//Создание RadioButton на перекрестках шестиугольников с учетом игровых ситуаций
        {
            Game_map.Controls.Remove(GroupRadioRoad);
            Game_map.Controls.Remove(GroupRoge);
            Game_map.Controls.Add(GroupRadioHext);
            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < HexRadio(i); ++j)
                {
                    if (StartGame ? g1.Info_hex_t(i, j) == 0 : g1.Info_hex_t(i, j) == 0 & g1.RAMinHext(i, j, plaer))
                    {
                        RadioButton radioButton = new RadioButton();
                        radioButton.Location = new Point(i < 3 ? (j % 2 == 0 ? 120 + 100 * i : 100 + 100 * i) : (j % 2 == 0 ? 100 + 100 * i : 120 + 100 * i), i < 3 ? 230 + j * 56 - 52 * i : 125 + j * 56 + 52 * (i - 3));
                        radioButton.Size = new Size(20, 20);
                        radioButton.Tag = string.Format("{0} {1}", i, j);
                        GroupRadioHext.Controls.Add(radioButton);
                    }
                }
            }
        }    
        /* Функции по нажытию кнопок */
        private void button1_Click(object sender, EventArgs e)// отображение выбра мест для строителства поселения
        { if (g1.Plaers[plaer - 1].Wood >= 1 && g1.Plaers[plaer - 1].Glay >= 1 && g1.Plaers[plaer - 1].Wheat >= 1 && g1.Plaers[plaer - 1].Wool >= 1&& g1.Plaers[plaer - 1].Town<5)
            {
                AceptTown.Visible = true;
                CloseTown.Visible = true;
                RadHext();
            }
            else
                MessageBox.Show("Недостаточно ресурсов постороенно максимальное число поселений ", "Ошибка стоительства", MessageBoxButtons.OK);
        }
        private void button2_Click(object sender, EventArgs e)//строительство поселения
        {
            for (int index = 0; index < GroupRadioHext.Controls.Count; ++index)
            {
                if (((RadioButton)GroupRadioHext.Controls[index]).Checked)
                {
                    string[] strArray = GroupRadioHext.Controls[index].Tag.ToString().Split(' ');
                    g1.Set_hex_t(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), plaer);
                    g1.PriseTraid(plaer-1, Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
                    Bitmap bmp = new Bitmap(Game_map.Width, Game_map.Height);
                    bmp = (Bitmap)game_map_back.Clone();
                    Pen mypen;
                    if (plaer == 1)
                    { mypen = new Pen(Color.Red, 5); }
                    else if (plaer == 2)
                    { mypen = new Pen(Color.Blue, 5); }
                    else if (plaer == 3)
                    { mypen = new Pen(Color.Green, 5); }
                    else { mypen = new Pen(Color.Magenta, 5); }
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawRectangle(mypen, GroupRadioHext.Controls[index].Location.X, GroupRadioHext.Controls[index].Location.Y, 10, 10);
                    game_map_back = (Bitmap)bmp.Clone();
                    Game_map.Image = bmp;

                    GroupRadioHext.Controls.Clear();
                    AceptTown.Visible = false;
                    CloseTown.Visible = false;
                        
                    g1.Plaers[plaer-1].Wood--;
                    g1.Plaers[plaer - 1].Glay--;
                    g1.Plaers[plaer - 1].Wheat--;
                    g1.Plaers[plaer - 1].Wool--;
                    g1.Plaers[plaer - 1].Town++;                     
                    Info_Plaer();
                }
            }
        }  
        private void button3_Click(object sender, EventArgs e)//отмена поселения
        {
            GroupRadioHext.Controls.Clear();
            AceptTown.Visible = false;
            CloseTown.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(g1.Plaers[plaer-1].Wood >= 1 && g1.Plaers[plaer-1].Glay >= 1)
            {
                Game_map.Controls.Remove(GroupRoge);
                Game_map.Controls.Remove(GroupRadioHext);
                Game_map.Controls.Add(GroupRadioRoad);
                CreateRoadRadioButton();
                AceptRoad.Visible = true;
                CloseRoad.Visible = true;

            }
            else
                MessageBox.Show("Недостаточно ресурсов", "Ошибка стоительства", MessageBoxButtons.OK);
        }
        private void CreateRoadRadioButton()
        {
            for (int i = 0; i < 11; ++i)
            {
                for (int j = 0; j < (i % 2 == 0 ? (i < 5 ? 6 + i : 16 - i) : (i < 6 ? i / 2 + 4 : 8 - i / 2)); ++j)
                {
                    g1.RoadToRAM(i, j, out int r1, out int r2);
                    g1.RoadToHext(i, j, out int h1i, out int h1j, out int h2i, out int h2j);
                    if (g1.Info_road(r1, r2) == 0 & (g1.Info_hex_t(h1i, h1j) == plaer || g1.Info_hex_t(h2i, h2j) == plaer || g1.RAMinHext(h1i, h1j, plaer) || g1.RAMinHext(h2i, h2j, plaer)))
                    {
                        RadioButton radioButton = new RadioButton();
                        radioButton.Location = new Point(110 + i * 50, i < 6 ? (i % 2 == 0 ? 260 + 55 * j - 55 * (i / 2) : 230 + 110 * j - 55 * (i / 2)) : (i % 2 == 0 ? 150 + 55 * j + 55 * ((i - 6) / 2) : 120 + 110 * j + 55 * ((i - 5) / 2)));
                        radioButton.Size = new Size(20, 20);
                        radioButton.Tag = string.Format("{0} {1} {2} {3}", r1, r2, i, j);
                        GroupRadioRoad.Controls.Add(radioButton);
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < GroupRadioRoad.Controls.Count; ++index)
            {
                if (((RadioButton)GroupRadioRoad.Controls[index]).Checked)
                {
                    string[] strArray = GroupRadioRoad.Controls[index].Tag.ToString().Split(' ');
                    g1.Set_road(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]),plaer);
                    Bitmap bmp = new Bitmap(Game_map.Width, Game_map.Height);
                    bmp = (Bitmap)game_map_back.Clone();
                    Pen mypen;
                    if (plaer == 1)
                    { mypen = new Pen(Color.Red, 5); }
                    else if (plaer == 2)
                    { mypen = new Pen(Color.Blue, 5); }
                    else if (plaer == 3)
                    { mypen = new Pen(Color.Green, 5); }
                    else { mypen = new Pen(Color.Magenta, 5); }
                    Graphics g = Graphics.FromImage(bmp);

                    Console.WriteLine(GroupRadioRoad.Controls[index].Tag.ToString());

                    if (Convert.ToInt32(strArray[2]) % 2 != 0)
                    { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X - 10, GroupRadioRoad.Controls[index].Location.Y+10, GroupRadioRoad.Controls[index].Location.X + 20, GroupRadioRoad.Controls[index].Location.Y+10); }
                    else if ((Convert.ToInt32(strArray[2])<5 & Convert.ToInt32(strArray[3]) % 2 > 0)||(Convert.ToInt32(strArray[2]) > 5 & Convert.ToInt32(strArray[3]) % 2 == 0))
                    { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X - 8, GroupRadioRoad.Controls[index].Location.Y - 8, GroupRadioRoad.Controls[index].Location.X + 10, GroupRadioRoad.Controls[index].Location.Y + 20); }
                    else
                    { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X + 15, GroupRadioRoad.Controls[index].Location.Y - 12, GroupRadioRoad.Controls[index].Location.X - 5, GroupRadioRoad.Controls[index].Location.Y + 20); }
                    GroupRadioRoad.Controls.Clear();
                    game_map_back = (Bitmap)bmp.Clone();
                    Game_map.Image = bmp;
                    g1.Plaers[plaer - 1].Wood--;
                    g1.Plaers[plaer - 1].Glay--;
                    Info_Plaer();
                }
            }
            Game_map.Controls.Remove(GroupRadioRoad);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < GroupRadioHext.Controls.Count; ++index)
            {
                if (((RadioButton)GroupRadioHext.Controls[index]).Checked)
                {
                    string[] strArray = GroupRadioHext.Controls[index].Tag.ToString().Split(' ');
                    int PlaerDown = g1.Info_hex_t(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
                    if (PlaerDown > 4)
                        PlaerDown -= 4;
                    g1.Rob(PlaerDown - 1, plaer - 1);
                    Info_Plaer();

                }
            }
            Game_map.Controls.Remove(GroupRadioHext);
            button6.Visible = false;
            CreateTown.Visible = true;
            CreatRoad.Visible = true;
            EndTour.Visible = true;
            BTraidBank.Visible = true;
            BTraidPlaers.Visible = true;
            BuyCart.Visible = true;
        }   
        private void button7_Click(object sender, EventArgs e)
        {
            CreateTown.Visible = true;
            CreatRoad.Visible = true;
            button10.Visible = true;
            BTraidBank.Visible = true;
            BTraidPlaers.Visible = true;
            EndTour.Visible = true;
            BuyCart.Visible = true;
            Roll.Visible = false;
            if(g1.Plaers[plaer-1].CartTwoRoad>=1)
            TroRoad.Visible = true;
            if (g1.Plaers[plaer - 1].CartGetAny >= 1)
                GetAnyRes.Visible = true;
            if (g1.Plaers[plaer - 1].CartOne >= 1)
                One.Visible = true;
            if (g1.Plaers[plaer - 1].CartArmy >= 1)
                UseArmy.Visible = true;

            label2.Text = d.Roll().ToString();          
        }
        private void button8_Click(object sender, EventArgs e)
        {
            RogeRobPlaer = 0;
            bool Check = false;
            for (int index = 0; index < GroupRoge.Controls.Count; index++)
            {
                if (((RadioButton)GroupRoge.Controls[index]).Checked)
                {
                    string[] strArray = GroupRoge.Controls[index].Tag.ToString().Split(' ');
                    Hexs[Convert.ToInt32(strArray[0])][Convert.ToInt32(strArray[1])].Set_Roge();
                    lastx = Convert.ToInt32(strArray[0]);
                    lasty = Convert.ToInt32(strArray[1]);                                     
                    Bitmap bmp =  (Bitmap)game_map_back.Clone();
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawImage(ImgMap.Roge, GroupRoge.Controls[index].Location.X, GroupRoge.Controls[index].Location.Y,30,50);
                    Game_map.Image = bmp;
                    Bitmap bmp1 = new Bitmap(Game_map.Width, Game_map.Height);
                    Graphics gb = Graphics.FromImage(bmp1);
                    gb.DrawImage(ImgMap.Roge, GroupRoge.Controls[index].Location.X, GroupRoge.Controls[index].Location.Y,30,50);

                    Check = true;
                    GroupRoge.Controls.Clear();
                    Game_map.Controls.Remove(GroupRoge);
                    Game_map.Controls.Add(GroupRadioHext);

                    Hexs[(Convert.ToInt32(strArray[0]))][(Convert.ToInt32(strArray[1]))].HexXYTOHext(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), out int Ai, out int Aj, out int Bi, out int Bj, out int Ci, out int Cj, out int Di, out int Dj, out int Ei, out int Ej, out int Fi, out int Fj);
                    for (int i = 0; i < 6; ++i)
                    {
                        for (int j = 0; j < HexRadio(i); ++j)
                        {
                            if ((g1.Info_hex_t(i, j) != 0 & g1.Info_hex_t(i, j) != 9 & g1.Info_hex_t(i, j) != plaer) & ((i == Ai && j == Aj) || (i == Bi && j == Bj) || (i == Ci && j == Cj) || (i == Di && j == Dj) || (i == Ei && j == Ej) || (i == Fi && j == Fj)))
                            {
                                RadioButton radioButton = new RadioButton();
                                radioButton.Location = new Point(i < 3 ? (j % 2 == 0 ? 120 + 100 * i : 100 + 100 * i) : (j % 2 == 0 ? 100 + 100 * i : 120 + 100 * i), i < 3 ? 230 + j * 56 - 52 * i : 125 + j * 56 + 52 * (i - 3));
                                radioButton.Size = new Size(20, 20);
                                radioButton.Tag = string.Format("{0} {1}", i, j);
                                GroupRadioHext.Controls.Add(radioButton);
                            }
                        }
                    }
                }
            }
            if (Check)
            {

                if (GroupRadioHext.Controls.Count == 0)
                {
                    button8.Visible = false;
                    CreateTown.Visible = true;
                    CreatRoad.Visible = true;
                    button10.Visible = true;
                    BTraidBank.Visible = true;
                    BTraidPlaers.Visible = true;
                    BuyCart.Visible = true;
                    EndTour.Visible = true;

                }
                else
                {
                    button8.Visible = false;
                    button6.Visible = true;
                }
            }
            else MessageBox.Show("Переместите разбойников", "Ошибка не выбрано ни одного поля", MessageBoxButtons.OK);
        }
        private void button12_Click(object sender, EventArgs e)
        {

        }
        private void button13_Click(object sender, EventArgs e)
        {
            GroupRadioRoad.Controls.Clear();
            AceptRoad.Visible = false;
            CloseRoad.Visible = false;

        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (g1.Plaers[plaer-1].Wheat >= 2 && g1.Plaers[plaer-1].Stone >= 3&& g1.Plaers[plaer - 1].City<4)
            {
                Game_map.Controls.Remove(GroupRoge);
                Game_map.Controls.Remove(GroupRadioRoad);
                Game_map.Controls.Add(GroupRadioHext);
                for (int i = 0; i < 6; ++i)
                {
                    for (int j = 0; j < HexRadio(i); ++j)
                    {
                        if ( g1.Info_hex_t(i, j) == plaer )
                        {
                            RadioButton radioButton = new RadioButton();
                            radioButton.Location = new Point(i < 3 ? (j % 2 == 0 ? 120 + 100 * i : 100 + 100 * i) : (j % 2 == 0 ? 100 + 100 * i : 120 + 100 * i), i < 3 ? 230 + j * 56 - 52 * i : 125 + j * 56 + 52 * (i - 3));
                            radioButton.Size = new Size(20, 20);
                            radioButton.Tag = string.Format("{0} {1}", i, j);
                            GroupRadioHext.Controls.Add(radioButton);
                        }
                    }
                }
                button11.Visible = true;
                button12.Visible = true;

            }
            else MessageBox.Show("Недостаточно ресурсов или построенно максимальное число городов", "Ошибка стоительства", MessageBoxButtons.OK);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < GroupRadioHext.Controls.Count; index++)
            {
                if (((RadioButton)GroupRadioHext.Controls[index]).Checked)
                {
                    string[] strArray = GroupRadioHext.Controls[index].Tag.ToString().Split(' ');
                    g1.UpToSity(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[0]));
                    Bitmap bmp = new Bitmap(Game_map.Width, Game_map.Height);
                    bmp = game_map_back;
                    Pen mypen;
                    if (plaer == 1)
                    { mypen = new Pen(Color.Red, 5); }
                    else if (plaer == 2)
                    { mypen = new Pen(Color.Blue, 5); }
                    else if (plaer == 3)
                    { mypen = new Pen(Color.Green, 5); }
                    else { mypen = new Pen(Color.Magenta, 5); }
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawRectangle(mypen, GroupRadioHext.Controls[index].Location.X, GroupRadioHext.Controls[index].Location.Y, 20, 20);
                    game_map_back = (Bitmap)bmp.Clone();
                    Game_map.Image = bmp;

                   GroupRadioHext.Controls.Clear();
                    g1.Plaers[plaer - 1].Wheat -= 2;
                    g1.Plaers[plaer - 1].Stone -= 3;
                    g1.Plaers[plaer - 1].Town --;
                    g1.Plaers[plaer - 1].City++;
                }

            } 

        }
        private void button14_Click(object sender, EventArgs e)
        {
            RightPanel.Controls.Remove(UTraidBank);
            RightPanel.Controls.Remove(UTraid);

            UTraidBank = new TraidWithBancControl
            {
                Location = new Point(10, 200),
            };
            UTraidBank.ReadInfo(plaer-1, g1.Plaers);
            UTraidBank.ETraidBankClose += CloseTraidBank;
            RightPanel.Controls.Add(UTraidBank);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            RightPanel.Controls.Remove(UTraidBank);
            RightPanel.Controls.Remove(UTraid);

            UTraid = new InvateTraid
            {
                Location = new Point(10, 200),
            };
            UTraid.ReadInfo(plaer-1, g1.Plaers,MaxPlaer);
            UTraid.ETraidComplite +=CloseTraid;
            UTraid.EClose +=CloseTraidNull;
            RightPanel.Controls.Add(UTraid);

        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (g1.Plaers[plaer-1].Wheat >= 1 && g1.Plaers[plaer-1].Wool >= 1 && g1.Plaers[plaer-1].Stone >= 1)
            {
                var result = g1.Get_cart();
                if (result != 6)
                {
                    g1.Plaers[plaer-1].Wheat--;
                    g1.Plaers[plaer-1].Wool--;
                    g1.Plaers[plaer-1].Stone--;
                    string cart = "";
                    if (result == 0)
                    {
                        g1.Plaers[plaer - 1].CartArmy++;
                        cart = "Вы получили карточку \"Рыцарь\".Ее можно будет применить со следующего хода.";
                    }
                    if (result == 1)
                    {
                        g1.Plaers[plaer - 1].CartOne++;
                        cart = "Вы получили карточку \"Монополия\".Ее можно будет применить со следующего хода.";
                    }
                    if (result == 2)
                    {
                        g1.Plaers[plaer - 1].CartTwoRoad++;
                        cart = "Вы получили карточку \"Прорыв\".Ее можно будет применить со следующего хода.";
                    }
                    if (result == 3)
                    {
                        g1.Plaers[plaer - 1].CartGetAny++;
                        cart = "Вы получили карточку \"Изобретение\".Ее можно будет применить со следующего хода.";
                    }
                    if (result == 5)
                    {
                        g1.Plaers[plaer - 1].CartWinPoint++;
                        cart = "Вы получили карточку \"Победное очко\".";
                    }

                    MessageBox.Show(cart, "Покупка карты", MessageBoxButtons.OK);
                    Info_Plaer();
                }
                else
                MessageBox.Show("Карты развития закончились", "Ошибка покупка карты", MessageBoxButtons.OK);


            }
            else MessageBox.Show("Недостаточно ресурсов для покупки", "Ошибка покупки карточки развития ", MessageBoxButtons.OK);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            g1.Plaers[plaer - 1].Army++;
            g1.Plaers[plaer - 1].CartArmy--;
            UseArmy.Visible = false;
            One.Visible = false;
            TroRoad.Visible = false;
            GetAnyRes.Visible = false;

                RogeDrive();

        }
        Label labelOne=null;
        private void button18_Click(object sender, EventArgs e)
        {
            ResRadio();
            FOne = true;
            labelOne = new Label
            {
                Location = new Point(10, 200),
                Text = "Выберете русурс.",
                AutoSize = true,
            };
            RightPanel.Controls.Add(labelOne);
            UseArmy.Visible = false;
            One.Visible = false;
            TroRoad.Visible = false;
            GetAnyRes.Visible = false;
            g1.Plaers[plaer - 1].CartOne--;


        }
        private void ResRadio()
        {
            GRes = new GroupBox
            {
                Location = new Point(10,230),
                AutoSize = true,
            };
            RightPanel.Controls.Add(GRes);

            RadioButton res1 = new RadioButton
            {
                Location = new Point(10, 30),
                Text = "Руда",
                Size = new Size(70, 20),
                Tag = "1",
            };
            res1.Click += CloseCartOne;
            GRes.Controls.Add(res1);

            RadioButton res2 = new RadioButton
            {
                Location = new Point(10, 50),
                Text = "Глина",
                Size = new Size(70, 20),
                Tag = "2",
            };
            res2.Click += CloseCartOne;
            GRes.Controls.Add(res2);

            RadioButton res3 = new RadioButton
            {
                Location = new Point(10, 70),
                Text = "Дерево",
                Size = new Size(70, 20),
                Tag = "3",
            };
            res3.Click += CloseCartOne;
            GRes.Controls.Add(res3);

            RadioButton res4 = new RadioButton
            {
                Location = new Point(10, 90),
                Text = "Шерсть",
                Size = new Size(70, 20),
                Tag = "4",
            };
            res4.Click += CloseCartOne;
            GRes.Controls.Add(res4);

            RadioButton res5 = new RadioButton
            {
                Location = new Point(10, 110),
                Text = "Пшеница",
                Size = new Size(70, 20),
                Tag = "5",
            };
            res5.Click += CloseCartOne;
            GRes.Controls.Add(res5);
        }
        private void CloseCartOne(object sender, EventArgs e)
        {
            if (FOne)
                for (int i = 0; i < GRes.Controls.Count; i++)
                    if (((RadioButton)GRes.Controls[i]).Checked)
                    {
                        int TypeRes = Convert.ToInt32((GRes.Controls[i]).Tag);
                        g1.CartOne(plaer - 1, MaxPlaer, TypeRes);
                        Info_Plaer();
                        RightPanel.Controls.Remove(GRes);
                        RightPanel.Controls.Remove(labelOne);
                        FOne = false;
                    }
            if (FGetAny)
                for (int i = 0; i < GRes.Controls.Count; i++)
                    if (((RadioButton)GRes.Controls[i]).Checked)
                    {
                        int TypeRes = Convert.ToInt32((GRes.Controls[i]).Tag);
                        switch (TypeRes)
                        {
                            case 1: g1.Plaers[plaer - 1].Stone++; break;
                            case 2: g1.Plaers[plaer - 1].Glay++; break;
                            case 3: g1.Plaers[plaer - 1].Wood++; break;
                            case 4: g1.Plaers[plaer - 1].Wool++; break;
                            case 5: g1.Plaers[plaer - 1].Wheat++; break;
                        }
                        if (ProgresTwo < 2)
                            ProgresTwo++;
                        else
                        {
                            RightPanel.Controls.Remove(GRes);
                        }
                    }
        }
        Button AceptTwo;
        private void button19_Click(object sender, EventArgs e)
        {
            Game_map.Controls.Remove(GroupRoge);
            Game_map.Controls.Remove(GroupRadioHext);
            Game_map.Controls.Add(GroupRadioRoad);
            Game_map.Controls.Add(GroupRadioRoad);
            ProgresTwo = 0;
            AceptTwo = new Button
            {
                Location = new Point(10, 200),
                Text = "Построить дорогу"
            };
            AceptTwo.Click += new EventHandler(AceptTwo_Click);
            RightPanel.Controls.Add(AceptTwo);
            CreateRoadRadioButton();
            UseArmy.Visible = false;
            One.Visible = false;
            TroRoad.Visible = false;
            GetAnyRes.Visible = false;
            g1.Plaers[plaer - 1].CartTwoRoad--;

        }
        private void AceptTwo_Click(object sender, EventArgs e)
        {
            if (ProgresTwo < 2)
            {
                for (int index = 0; index < GroupRadioRoad.Controls.Count; ++index)
                {
                    if (((RadioButton)GroupRadioRoad.Controls[index]).Checked)
                    {
                        ProgresTwo++;
                        string[] strArray = GroupRadioRoad.Controls[index].Tag.ToString().Split(' ');
                        g1.Set_road(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), plaer);
                        Bitmap bmp = new Bitmap(Game_map.Width, Game_map.Height);
                        bmp = (Bitmap)game_map_back.Clone();
                        Pen mypen;
                        if (plaer == 1)
                        { mypen = new Pen(Color.Red, 5); }
                        else if (plaer == 2)
                        { mypen = new Pen(Color.Blue, 5); }
                        else if (plaer == 3)
                        { mypen = new Pen(Color.Green, 5); }
                        else { mypen = new Pen(Color.Magenta, 5); }
                        Graphics g = Graphics.FromImage(bmp);

                        Console.WriteLine(GroupRadioRoad.Controls[index].Tag.ToString());

                        if (Convert.ToInt32(strArray[2]) % 2 != 0)
                        { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X - 10, GroupRadioRoad.Controls[index].Location.Y + 10, GroupRadioRoad.Controls[index].Location.X + 20, GroupRadioRoad.Controls[index].Location.Y + 10); }
                        else if ((Convert.ToInt32(strArray[2]) < 5 & Convert.ToInt32(strArray[3]) % 2 > 0) || (Convert.ToInt32(strArray[2]) > 5 & Convert.ToInt32(strArray[3]) % 2 == 0))
                        { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X - 8, GroupRadioRoad.Controls[index].Location.Y - 8, GroupRadioRoad.Controls[index].Location.X + 10, GroupRadioRoad.Controls[index].Location.Y + 20); }
                        else
                        { g.DrawLine(mypen, GroupRadioRoad.Controls[index].Location.X + 15, GroupRadioRoad.Controls[index].Location.Y - 12, GroupRadioRoad.Controls[index].Location.X - 5, GroupRadioRoad.Controls[index].Location.Y + 20); }
                        GroupRadioRoad.Controls.Clear();
                        game_map_back = (Bitmap)bmp.Clone();
                        Game_map.Image = bmp;
                    }
                }
                CreateRoadRadioButton();
            }
            else
            {
                RightPanel.Controls.Remove(AceptTwo);
                Game_map.Controls.Remove(GroupRadioRoad);
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            ProgresTwo = 0;
            FGetAny = true;
            Label labelOne = new Label
            {
                Location = new Point(10, 200),
                Text = "Выберете русурс.",
                AutoSize = true,
            };
            RightPanel.Controls.Add(labelOne);
            RightPanel.Controls.Add(GRes);
            ResRadio();
            UseArmy.Visible = false;
            One.Visible = false;
            TroRoad.Visible = false;
            GetAnyRes.Visible = false;
            g1.Plaers[plaer - 1].CartGetAny--;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (plaer == MaxPlaer)
                plaer = 1;
            else plaer++;

            Roll.Visible = true;
            EndTour.Visible = false;

            CreateTown.Visible = false;
            AceptTown.Visible = false;
            CloseTown.Visible = false;
            CreatRoad.Visible = false;
            AceptRoad.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            CloseRoad.Visible = false;
            BTraidBank.Visible = false;
            BTraidPlaers.Visible = false;
            BuyCart.Visible = false;

            UseArmy.Visible = false;
            if (g1.Plaers[plaer-1].CartArmy>=1)
                UseArmy.Visible = true;
            One.Visible = false;
            if (g1.Plaers[plaer - 1].CartOne >= 1)
                One.Visible = true;
            TroRoad.Visible = false;
            if (g1.Plaers[plaer - 1].CartTwoRoad >= 1)
                TroRoad.Visible = true;
            GetAnyRes.Visible = false;
            if (g1.Plaers[plaer - 1].CartGetAny >= 1)
                GetAnyRes.Visible = true;

            GroupRadioHext.Controls.Clear();
            GroupRadioRoad.Controls.Clear();
            Info_Plaer();
        }      
    }
}
