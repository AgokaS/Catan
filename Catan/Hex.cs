using System;

namespace Catan
{
    class Hex
    {
        bool Roge;
        int Type;

        public Hex(int Type, Dase d)
        {
            d.EventRoll7 += RogeF;
            this.Roge = false;
            this.Type = Type;
        }

        private void RogeF()
        {
            Roge = false;
        }
        public void Set_Roge()
        {
            Roge = true;
        }
        public bool Info_Roge()
        {
            return Roge;
        }

        public int Info_Type()
        {
            return Type;
        }

        //координаты смежных перекрестков
        public void HexXYTOHext(int x, int y, out int Ai,out int Aj, out int Bi, out int Bj, out int Ci, out int Cj, out int Di, out int Dj,  out int Ei, out int Ej, out int Fi, out int Fj)
        {
            if (x == 2)
            {
                Ai = Bi = Ci = x;
                Di = Ei = Fi = x + 1;
                Aj = y * 2  ;
                Bj = y * 2 +  1;
                Cj = y * 2 +  2;
                Dj = y * 2 ;
                Ej = y * 2 +  1;
                Fj = y * 2 +  2;
            }
            else if (x < 2)
            {
                Ai = Bi = Ci = x;
                Di = Ei = Fi = x + 1;
                Aj = y * 2 ;
                Bj = y * 2 + 1;
                Cj = y * 2 +  2;
                Dj = y * 2 +  1;
                Ej = y * 2 + 2;
                Fj = y * 2 +  3;
            }
            else
            {
                Ai = Bi = Ci = x;
                Di = Ei = Fi = x + 1;
                Aj = y * 2 +1;
                Bj = y * 2 +  2;
                Cj = y * 2 +  3;
                Dj = y * 2  ;
                Ej = y * 2 +  1;
                Fj = y * 2 +  2;
            }
        }
    }
}
