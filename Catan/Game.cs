using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Catan
{

    class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }

    public class Game
    {
        public struct PlaerItem//Хранение всей информации игроке
        {
            public int WinPoint;
            public int CartWinPoint;
            public int CartArmy;
            public int CartOne;
            public int CartGetAny;
            public int CartTwoRoad;
            public int Town;
            public int City;
            public int Army;
            public int Stone;
            public int Wood;
            public int Wool;
            public int Glay;
            public int WheatPrise;
            public int StonePrise;
            public int WoodPrise;
            public int WoolPrise;
            public int GlayPrise;
            public int Wheat;
            public int LRoad;
            public bool FLongerRoad;
            public bool FBiggestArmy;
        }

        /* Объявление полей класса */
        public PlaerItem[] Plaers = null;

        private int[] hex_type = new int[19]
        {
      0,
      1,
      1,
      1,
      2,
      2,
      2,
      3,
      3,
      3,
      3,
      4,
      4,
      4,
      4,
      5,
      5,
      5,
      5
        };//Массив с типами шестигранников используется для старта игры (исходные данные)
        private int[] hex_num = new int[18]
        {
      5,
      2,
      6,
      3,
      8,
      10,
      9,
      12,
      11,
      4,
      8,
      10,
      9,
      4,
      5,
      6,
      3,
      11
        };//Массив с номерами бросков кубика используется для старта игры (исходные данные)
        private int[] deck_del = new int[25]
        {
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2,2,3,3,5,5, 5, 5, 5
        };//Колода карт развития 
        private int num_cart_up;//верхняя карта в колоде. Используется для определения взятия
        private int[][] hex_t;//Матрица прекрестков используется для отслеживание состояния перекрестков(Возможные значения: 0-пустой можно строить, 1,2,3,4- занят соответствующим игроком 9-пустой, нельзя стоить по правилу соседства )
        private int[,] RAM;//Матрица(Граф) дорог используется для отслеживание существования дороги и поиска тракта

        public Game(int MaxPlaer)
        {
            /* Создание нулевых записей о игроках */
            Plaers = new PlaerItem[MaxPlaer];
            for (int i = 0; i < MaxPlaer; i++)
            {
                Plaers[i].WinPoint = 0;
                Plaers[i].CartWinPoint = 0;
                Plaers[i].CartArmy=0;
                Plaers[i].CartOne=0;
                Plaers[i].CartGetAny=0;
                Plaers[i].CartTwoRoad=0;
                Plaers[i].Army = 0;
                Plaers[i].Town = 2;
                Plaers[i].City = 0;
                Plaers[i].Wood = 0;
                Plaers[i].Wool = 0;
                Plaers[i].Wheat = 0;
                Plaers[i].Stone = 0;
                Plaers[i].Glay = 0;
                Plaers[i].WoodPrise = 4;
                Plaers[i].WoolPrise = 4;
                Plaers[i].WheatPrise = 4;
                Plaers[i].StonePrise = 4;
                Plaers[i].GlayPrise = 4;
                Plaers[i].LRoad = 0;
                Plaers[i].FLongerRoad = false;
                Plaers[i].FBiggestArmy = false;
            }

            Shuffle(ref hex_type);
            Shuffle(ref deck_del);
            /* Создание пустой матрицы дорог */
            
                RAM = new int[54, 54];
                for (int index1 = 0; index1 < 54; ++index1)
                {
                    for (int index2 = 0; index2 < 54; ++index2)
                        RAM[index1, index2] = 0;
                }

            /* Создание пустого массива перекрестков */

                hex_t = new int[6][];
                hex_t[0] = new int[7];
                hex_t[1] = new int[9];
                hex_t[2] = new int[11];
                hex_t[3] = new int[11];
                hex_t[4] = new int[9];
                hex_t[5] = new int[7];
                for (int index1 = 0; index1 < 6; ++index1)
                {
                    for (int index2 = 0; index2 < hex_t[index1].Length; ++index2)
                        hex_t[index1][index2] = 0;
                }
                num_cart_up = -1;
            
        }

        private void Shuffle(ref int[] deck)//Алгоритм тасования Фишера.
        {
            Random random = new Random();
            for (int maxValue = deck.Length - 1; maxValue > 1; --maxValue)
            {
                int index = random.Next(0, maxValue);
                deck[maxValue] = deck[maxValue] - deck[index];
                deck[index] = deck[index] + deck[maxValue];
                deck[maxValue] = deck[index] - deck[maxValue];
            }

            foreach (int x in deck)
                Console.Write(x);
            Console.WriteLine();
        }

       

        /* Просмотр состояния элемета матрицы. Функции проверки условий*/

        public int Info_hex_t(int i, int j)
        {
            return hex_t[i][j];
        }
        public int Info_road(int i, int j)
        {
            return RAM[i, j];
        }
        public int Info_deck_type_s(int i)
        {
            return hex_type[i];
        }
        public int Info_deck_type(int i, int j)
        {
            int t=0;
            switch (i)
            {
                case 0: t = j;break;
                case 1: t = 3+j;break;
                case 2: t = 7+j;break;
                case 3: t = 12+j;break;
                case 4: t = 16+j;break;
            }
            return hex_type[t];
        }
        public int Info_deck_num(int i)
        {
            return hex_num[i];
        }
        public bool RAMinHext(int i, int j, int plaer)
        {
            int t = HextToRAM(i, j);
            for (int x = 0; x < 54; x++)
                if (RAM[t, x] == plaer)
                    return true;
            return false;
        }

        /*   Установление значания элемента матрицы   */
        public void UpToSity(int i, int j)
        {
            hex_t[i][j] += 4;
        }
        public void Set_hex_t(int i, int j,int plaer)
        {
            this.hex_t[i][j] = plaer; ;
            if (j > 0)
                hex_t[i][j - 1] = 9;
            if ((i == 0 | i == 5) & j != 6 || (i == 1 | i == 4) & j != 8 || (i == 2 | i == 3) & j != 10)
                hex_t[i][j + 1] = 9;
            if (i < 2)
            {
                if (j % 2 == 0)
                    hex_t[i + 1][j + 1] = 9;
                if (i > 0 & (j % 2) > 0)
                    hex_t[i - 1][j - 1] = 9;
            }
            if (i > 3)
            {
                if (i < 5 & (j % 2) > 0)
                    hex_t[i + 1][j - 1] = 9;
                if (j % 2 == 0)
                    hex_t[i - 1][j + 1] = 9;
            }
            if (i == 2)
            {
                if (j % 2 == 0)
                    hex_t[i + 1][j] = 9;
                if ((j % 2) > 0)
                    hex_t[i - 1][j - 1] = 9;
            }
            if (i>0&j<(i==0?6:i==1?8:10))
            if (j % 2 == 0)
                hex_t[i - 1][j] = 9;
            if ((j % 2) > 0&i<5&i!=2)
                hex_t[i + 1][j - 1] = 9;
        }
        public void Set_road(int q, int p, int plaer)
        {
            RAM[q, p] = plaer;
            RAM[p, q] = plaer;
        }


        /*   Функции перевода координат  */

        public void RoadToHext(
          int road_i,
          int road_j,
          out int hext1_i,
          out int hext1_j,
          out int hext2_i,
          out int hext2_j)
        {
            if (road_i % 2 == 0)
            {
                hext1_i = road_i / 2;
                hext1_j = road_j;
                hext2_i = road_i / 2;
                hext2_j = road_j + 1;
            }
            else
            {
                hext1_i = road_i / 2;
                hext2_i = road_i / 2 + 1;
                if (road_i == 5)
                    hext1_j = hext2_j = road_j * 2;
                else if (road_i < 5)
                {
                    hext1_j = road_j*2;
                    hext2_j = road_j*2 + 1;
                }
                else
                {
                    hext1_j = road_j*2 + 1;
                    hext2_j = road_j*2;
                }
            }
        }
        public void RAMToHext(int t, out int i, out int j)
        {
            if (t < 7)
            {
                i = 0;
                j = t;
            }
            else if (t < 16)
            {
                i = 1;
                j = t-7;
            }
            else if (t < 27)
            {
                i = 2;
                j = t - 16;
            }
            else if (t < 38)
            {
                i = 3;
                j = t - 27;
            }
            else if (t < 47)
            {
                i = 4;
                j = t - 38;
            }
            else 
            {
                i = 5;
                j = t - 47;
            }

        }
        public int HextToRAM(int hext_i, int t)
        {
            switch (hext_i)
            {
                case 0:
                    return t;
                case 1:
                    return 7 + t;
                case 2:
                    return 16 + t;
                case 3:
                    return 27 + t;
                case 4:
                    return 38 + t;
                case 5:
                    return 47 + t;
                default:
                    return -1;
            }
        }
        public void RoadToRAM(int i, int j, out int r1, out int r2)
        {
            RoadToHext(i, j, out int hext1_i, out int hext1_j, out int hext2_i, out int hext2_j);
            r1 = HextToRAM(hext1_i, hext1_j);
            r2 = HextToRAM(hext2_i, hext2_j);
        }
        /*Подсчет победных очков*/
        public void ToCountWinPoint(int plaer)
        {
            Plaers[plaer].WinPoint = Plaers[plaer].CartWinPoint + Plaers[plaer].Town + Plaers[plaer].City * 2;
            if (Plaers[plaer].FBiggestArmy)
                Plaers[plaer].WinPoint++;
            if (Plaers[plaer].FLongerRoad)
                Plaers[plaer].WinPoint++;
        }
        public bool FBiggest(int plaer,int maxarmy)
        {
            return Plaers[plaer].Army > maxarmy;              
        }
        public bool FLonger(int plaer, int maxroad)
        {
            Plaers[plaer].LRoad = StartPoint(plaer+1);
            return Plaers[plaer].LRoad > maxroad;
        }
        /* Метод подсчета тракта */
        public int StartPoint(int plaer)
        {
            bool f = false;
            int countRoad;
            int last = 0;
            int max = 0;
            List<string> visit = new List<string>();
            for (int i = 0; i < 54; i++)
            {               
                int count_r=0;
                for (int j = 0; j < 54; j++)
                {
                    if (RAM[i, j] == plaer|| RAM[i, j] == plaer+4)
                    {
                        count_r++;
                        last = j;
                    }
                }
                if (count_r == 1)
                {
                    f = true;
                    countRoad = ToCountRoad(plaer,i, visit, 0);
                    if (countRoad > max)
                        max = countRoad;
                }
            }
                if (f==false)
                {
                    countRoad = ToCountRoad(plaer, last, visit: visit, 0);
                    if (countRoad > max)
                        max = countRoad;
                }

                        
            return max;
        }
        private int ToCountRoad(int plaer, int now, List<string> visit,int count)
                {
                    RAMToHext(now, out int a, out int b);
                    if (Info_hex_t(a, b) == 0 || Info_hex_t(a, b) == 9 || Info_hex_t(a, b) == plaer || Info_hex_t(a, b) == plaer + 4)
                        for (int j =0; j<54;j++)
                            if (RAM[now,j]==plaer|| RAM[now, j] == plaer+4)
                            {
                                bool f = false;
                                foreach(string x in visit)
                                    if (x == $"{now} {j}" )
                                         f = true;
                                if (f == false)
                                {
                                    visit.Add($"{now} {j}");
                                    visit.Add($"{j} {now}");
                                    return ToCountRoad(plaer, j, visit, ++count);
                                }
                            }
                    return count;
                }
        /* Взыимодействие игроков и банком игры */
        public int Get_cart()
        {
            ++num_cart_up;
            if (num_cart_up < deck_del.Length)
                return deck_del[num_cart_up];
            return 6;
        }
        public void PriseTraid(int plaer, int hext_i, int hext_j)
        {
            if ((hext_i == 0 && hext_j == 0) || (hext_i == 0 && hext_j == 1) || (hext_i == 0 && hext_j == 6) || (hext_i == 1 && hext_j == 7) || (hext_i == 2 && hext_j == 9) || (hext_i == 2 && hext_j == 10) || (hext_i == 3 && hext_j == 1) || (hext_i == 4 && hext_j == 0) || (hext_i == 4 && hext_j == 7) || (hext_i == 5 && hext_j == 6))
            {
                if (Plaers[plaer].WoodPrise > 2)
                    Plaers[plaer].WoodPrise = 3;
                if (Plaers[plaer].StonePrise > 2)
                    Plaers[plaer].StonePrise = 3;
                if (Plaers[plaer].GlayPrise > 2)
                    Plaers[plaer].GlayPrise = 3;
                if (Plaers[plaer].WoolPrise > 2)
                    Plaers[plaer].WoolPrise = 3;
                if (Plaers[plaer].WheatPrise > 2)
                    Plaers[plaer].WheatPrise = 3;
            }
            if ( Plaers[plaer].WoodPrise > 2 && (hext_i == 5 && hext_j == 3) || (hext_i == 5 && hext_j == 4))
                Plaers[plaer].WoodPrise = 2;
            if (Plaers[plaer].WoolPrise > 2 && (hext_i == 1 && hext_j == 0) || (hext_i == 2 && hext_j == 1))
                Plaers[plaer].WoolPrise = 2;
            if (Plaers[plaer].StonePrise > 2 && (hext_i == 3 && hext_j == 9) || (hext_i == 3 && hext_j == 10))
                Plaers[plaer].StonePrise = 2;
            if (Plaers[plaer].GlayPrise > 2 && (hext_i == 5 && hext_j == 0) || (hext_i == 5 && hext_j == 1))
                Plaers[plaer].GlayPrise = 2;
            if (Plaers[plaer].WheatPrise > 2 && (hext_i == 0 && hext_j == 3) || (hext_i == 0 && hext_j == 4))
                Plaers[plaer].WheatPrise = 2;
        }
        /* Методы взаимодействия игроков */
        public void Rob(int PlaerDown, int PlaerUp)
        {
            int i = 0;
            List < int > res = new List<int>();
            Random r = new Random();

            while (  i < Plaers[PlaerDown].Stone)
            {
                res.Add(0);i++;
            };
            i = 0;
            while (i < Plaers[PlaerDown].Glay)
            {
                res.Add(1); i++;
            };
            i = 0;
            while (i < Plaers[PlaerDown].Wood)
            {
                res.Add(2); i++;
            }; i = 0;

            while (i < Plaers[PlaerDown].Wool)
            {
                res.Add(3); i++;
            }; i = 0;
            while (i < Plaers[PlaerDown].Wheat)
            {
                res.Add(4); i++;
            }

            if (res.Count!=0)
            switch (res[r.Next(0, res.Count -1)])
            {
                case 0:
                    {
                        Plaers[PlaerDown].Stone--;
                        Plaers[PlaerUp].Stone++;
                    };break;
                case 1:
                    {
                        Plaers[PlaerDown].Glay--;
                        Plaers[PlaerUp].Glay++;
                    }; break;
                case 2:
                    {
                        Plaers[PlaerDown].Wood--;
                        Plaers[PlaerUp].Wood++;
                    }; break;
                case 3:
                    {
                        Plaers[PlaerDown].Wool--;
                        Plaers[PlaerUp].Wool++;
                    }; break;
                case 4:
                    {
                        Plaers[PlaerDown].Wheat--;
                        Plaers[PlaerUp].Wheat++;
                    }; break;
            }
        }
        public void CartOne(int plaer,int max, int type)
        {
            if (type == 1)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i != plaer)
                    {
                        Plaers[plaer].Stone += Plaers[i].Stone;
                        Plaers[i].Stone = 0;
                    }
                }
            }
            if (type == 2)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i != plaer)
                    {
                        Plaers[plaer].Glay += Plaers[i].Glay;
                        Plaers[i].Glay = 0;
                    }
                }
            }
            if (type == 3)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i != plaer)
                    {
                        Plaers[plaer].Wood += Plaers[i].Wood;
                        Plaers[i].Wood = 0;
                    }
                }
            }
            if (type == 4)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i != plaer)
                    {
                        Plaers[plaer].Wool += Plaers[i].Wool;
                        Plaers[i].Wool = 0;
                    }
                }
            }
            if (type == 5)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i != plaer)
                    {
                        Plaers[plaer].Wheat += Plaers[i].Wheat;
                        Plaers[i].Wheat = 0;
                    }
                }
            }
        }
    }   
}
