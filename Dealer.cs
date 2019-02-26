using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPorker
{
    class Dealer
    {
        Queue<Card> shuffledCards = new Queue<Card>(52);

        //シャッフル済カードを作るメソッド
        public void ShuffleCard()
        {
            var createdCards = new List<Card>(52);
            var shufflingCards = new List<Card>(52);
            var modelCard = new Card("0",0);

            for (int i = 0; i < modelCard.marks.Length; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    //4種のうち1種のマーク、1～13のうち1つの数字を順番に渡し、インスタンスを作成
                    //作成したインスタンスを、List<Card>型リストに追加する
                    createdCards.Add(new Card(modelCard.marks[i], j));
                }
            }
            //cardsをシャッフルし、shuffleCardsに追加する
            shufflingCards = createdCards.OrderBy(i => Guid.NewGuid()).ToList();

            //キュー型に変換
            foreach (var s in shufflingCards)
            {
                shuffledCards.Enqueue(s);
            }
        }

        //カードを配るメソッド
        public void DealCard(List<Participant> participants)
        {
            //プレイヤーとコンピュータにそれぞれ5枚、計10枚配る
            for (int i = 0; i < (participants.Count) * 5 ; i++)
            {
                //もしiが奇数なら、プレイヤーに配る
                if (i % 2 == 1)
                {
                    participants[i].handCards.Add(shuffledCards.Dequeue());
                }
                //もしiが0か偶数なら、コンピュータに配る
                else
                {
                    participants[i].handCards.Add(shuffledCards.Dequeue());
                }
            }
        }

        //選択されたカードの枚数だけ再配布するメソッド
        public void ReDealCard(List<Participant> participants)
        {
            foreach (var p in participants)
            {
                for (int i = p.handCards.Count; i < 5; i++)
                {
                    p.handCards.Add(shuffledCards.Dequeue());
                }
            }
        }

        public string CompareRank(List<Participant> participants)
        {
            if (participants[0].rankStrength > participants[1].rankStrength)
            {
                return "CPUの勝ち";
            }
            else if (participants[0].rankStrength < participants[1].rankStrength)
            {
                return "あなたの勝ち";
            }
            else
            {
                return "引き分けです";
            }
        }

    }
}
