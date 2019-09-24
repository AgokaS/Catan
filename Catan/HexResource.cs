using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan
{
    class HexResource:Hex
    {
        public delegate void ResGet();
        public event  ResGet RefreshInfo;

        public int Roll_number { get; set; }
        Dase d = null;
        Game gl = null;
        int x, y;

        public HexResource(int i, int j ,int Type, Dase d, int roll, Game g1) : base(Type, d)
        {
            x = i;
            y = j;
            gl = g1;
            d.EventRoll += GetResource;
            Roll_number = roll;
            this.d = d;

        }
        public void GetResource()//выдает ресурсы если разбойники не стоят на этом шестиугольнике
        {
            if (d.Info_Coint_Roll == Roll_number&!Info_Roge())
            {
                base.HexXYTOHext(x, y, out int Ai, out int Aj, out int Bi, out int Bj, out int Ci, out int Cj, out int Di, out int Dj, out int Ei, out int Ej, out int Fi, out int Fj);
                Shablon(Ai,Aj);
                Shablon(Bi,Bj);
                Shablon(Ci,Cj);
                Shablon(Di,Dj);
                Shablon(Fi,Fj);
                Shablon(Ei,Ej);
               
                RefreshInfo();

            }
        }
        private void Shablon(int Ax,int Ay)
         {
            int t = gl.Info_hex_t(Ax, Ay);
            if (t != 0)
                if (t < 5)
                {
                    switch (Info_Type())
                    {
                        case 1: gl.Plaers[t-1].Stone++; break;
                        case 2: gl.Plaers[t-1].Glay++; break;
                        case 3: gl.Plaers[t-1].Wood++; break;
                        case 4: gl.Plaers[t-1].Wheat++; break;
                        case 5: gl.Plaers[t-1].Wool++; break;
                    }

                }
                else if (t < 9)
                {
                    switch (Info_Type())
                    {
                        case 1: gl.Plaers[t-5].Stone+=2; break;
                        case 2: gl.Plaers[t-5].Glay+=2; break;
                        case 3: gl.Plaers[t-5].Wood+=2; break;
                        case 4: gl.Plaers[t-5].Wheat+=2; break;
                        case 5: gl.Plaers[t-5].Wool+=2; break;
                    }
                }
        }
    }
}
