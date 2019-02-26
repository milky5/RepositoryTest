using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPorker
{
    class Player : Participant
    {
        public void SelectExchangeCard()
        {
            //プレイヤーに交換するカードを選択させるために表示
            for (int i = 0; i < handCards.Count; i++)
            {
                Console.WriteLine($"{i + 1}番目のカード:{handCards[i].mark}{handCards[i].number}");
            }
            Console.WriteLine("1,2,3,4,5番目、どのカードを交換しますか");
            Console.WriteLine("1枚選んでEnterを繰り返し、最後に0を押してください");

            while (true)
            {
                bool result = int.TryParse(Console.ReadLine(), out int temp);
                if (temp == 0)
                {
                    break;
                }
                else if (result)
                {
                    if (0 < temp && temp <= 5)
                    {
                        wantExchangeCards.Add(temp);
                    }
                    else
                    {
                        Console.WriteLine("1～5の数字を入力してください");
                    }
                }
                else
                {
                    Console.WriteLine("正しく入力してください");
                }
            }
            foreach (var w in wantExchangeCards)
            {
                Console.WriteLine($"交換するカード {w}番目");
            }

            //逆順にソートする
            //(Listの後ろの要素から消さないと狂うため)
            wantExchangeCards.Sort();
            wantExchangeCards.Reverse();
            for (int i = 0; i < wantExchangeCards.Count; i++)
            {
                handCards.RemoveAt(wantExchangeCards[i] - 1);
            }
        }
    }
}
