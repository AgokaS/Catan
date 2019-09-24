using System;

namespace Catan
{
    class Dase
    {
        private int Count_Roll;//Сумма броска
        Random rang = null;
        public delegate void Dase_roll();
        public event Dase_roll EventRoll;//Собыитие при котором выдаются ресурсы
        public event Dase_roll EventRoll7;// Событие при котором ходят разбойники
        public Dase()
        { rang = new Random(); }
        /* Бросок кубиков  */
        public int Roll()
        {          
            Count_Roll = rang.Next(1, 6) + rang.Next(1, 6);//имитируем два шестигранных кубика. Что бы не изменить вероятность две генерации чисел от 1 до 6, а не одна от 2 до 12  
            if (Count_Roll != 7)
                EventRoll();
            else EventRoll7();
            return Count_Roll;
        }
        public void StartResourse()
        {
            for (int i = 2; i < 13; i++)
                if (i != 7)
                {
                    Count_Roll = i;
                    EventRoll();
                }
        }
        public int Info_Coint_Roll//Метод просмотра выпавшего числа для логикии игры.
        {
            get { return Count_Roll; }
        }
    }
}