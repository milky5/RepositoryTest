using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPorker
{
    class Participant
    {
        public List<Card> handCards = new List<Card>();
        public List<int> wantExchangeCards = new List<int>();
        public List<string> fileName = new List<string>();
        public string rankName;
        public int rankStrength;

        #region DisplayCards()
        //List<PictureBox> CardScreen = new List<PictureBox>();

        //void DisplayCards()
        //{
        //    for (int i = 0; i < handCards.Count; i++)
        //    {
        //        CardScreen.Image[i] = fileName[i];
        //    }
        //}
        #endregion

        public void Judge()
        {
            #region デバッグ用＆詳細ルール
            //ジャッジテスト用の代入
            //handCards[0].mark = "Spade";
            //handCards[1].mark = "Diamond";
            //handCards[2].mark = "Club";
            //handCards[3].mark = "Heart";
            //handCards[4].mark = "Spade";
            //handCards[0].number = 9;
            //handCards[1].number = 9;
            //handCards[2].number = 9;
            //handCards[3].number = 2;
            //handCards[4].number = 2;

            //同じカードでも数字の高い方が基本的に強いカードです
            //マークはスペード＞ダイヤ＞ハート＞クラブと強さちがいます。
            //⑨ロイヤル・ストレート・フラッシュ １０・Ｊ・Ｑ・Ｋ・Ａの全て同じマーク
            //⑧ストレート・フラッシュ 例：４，５，６，７，８などの並んでいる数字でマークが全て同じ
            //⑦フォーカード 例：７、７，７，７、１０などのマークばらばら
            //⑥フルハウス ５枚のカードの内３枚が同じ数字２枚が同じ数字 例：４，４，４、８，８、などのマークばらばら
            //⑤フラッシュ ５枚全てのマークが同じ
            //④ストレート 例：６，７，８，９、１０などのカードばらばら ＫからＡにもつなげることが可能
            //③スリー・カード 例：Ａ、Ａ、Ａ、５、７のマークばらばら
            //②ツウ・ペア 例：６，６、９，９、３のマークばらばら
            //①ワン・ペア 例；４、４、６、８、２のマークばらばら
            #endregion
            int cnResult = 5;
            bool smResult = false;
            string smTrueMark = null;

            //手札の数字だけを抜き出し、配列に入れる
            var tempNumber = new List<int>();
            tempNumber.Clear();
            for (int i = 0; i < handCards.Count; i++)
            {
                tempNumber.Add(handCards[i].number);
            }

            //⑨ロイヤルストレートフラッシュを判定する
            cnResult = JudgeContinueNumber();
            if (cnResult == 2)
            {
                (smResult, smTrueMark) = JudgeSameMark();
                if (smResult)
                {
                    rankName = "ロイヤルストレートフラッシュ";
                    rankStrength = 9;
                    return;
                }
            }

            //⑧ストレートフラッシュを判定する
            //変数を初期化
            cnResult = 5;
            smResult = false;
            smTrueMark = null;
            cnResult = JudgeContinueNumber();
            if (cnResult == 1 || cnResult == 2)
            {
                (smResult, smTrueMark) = JudgeSameMark();
                if (smResult)
                {
                    rankName = "ストレートフラッシュ";
                    rankStrength = 8;
                    return;
                }
            }

            //⑦フォーカードを判定する
            for (int i = 0; i < 13; i++)
            {
                if (tempNumber.Where(n => n == i).Count() == 4)
                {
                    rankName = "フォーカード";
                    rankStrength = 7;
                    return;
                }
            }

            //⑥フルハウスを判定する
            //変数を初期化
            cnResult = 5;
            smResult = false;
            smTrueMark = null;
            for (int i = 1; i <= 13; i++)
            {
                if (tempNumber.Where(n => n == i).Count() == 3)
                {
                    Console.WriteLine(i);

                    for (int j = 1; j <= 13; j++)
                    {
                        if (tempNumber.Where(n => n == j).Count() == 2)
                        {
                            rankName = "フルハウス";
                            rankStrength = 6;
                            return;
                        }
                    }
                }
            }

            //⑤フラッシュを判定する
            //変数を初期化
            cnResult = 5;
            smResult = false;
            smTrueMark = null;
            (smResult, smTrueMark) = JudgeSameMark();
            if (smResult)
            {
                rankName = "フラッシュ";
                rankStrength = 5;
                return;
            }

            //④ストレートを判定する
            //変数を初期化
            cnResult = 5;
            smResult = false;
            smTrueMark = null;
            cnResult = JudgeContinueNumber();
            if (cnResult == 1 || cnResult == 2)
            {
                rankName = "ストレート";
                rankStrength = 4;
                return;
            }

            //③スリーカードを判定する
            for (int i = 0; i < 13; i++)
            {
                if (tempNumber.Where(n => n == i).Count() == 3)
                {
                    rankName = "スリーカード";
                    rankStrength = 3;
                    return;
                }
            }

            //②ツーペアを判定する
            tempNumber.Sort();
            for (int i = 1; i <= 13; i++)
            {
                if (tempNumber.Where(n => n == i).Count() == 2)
                {
                    for (int j = ++i; j <= 13; j++)
                    {
                        if (tempNumber.Where(n => n == j).Count() == 2)
                        {
                            rankName = "ツーペア";
                            rankStrength = 2;
                            return;
                        }
                    }
                }
            }

            //①ワンペアを判定する
            for (int i = 1; i <= 13; i++)
            {
                if (tempNumber.Where(n => n == i).Count() == 2)
                {
                    rankName = "ワンペア";
                    rankStrength = 1;
                    return;
                }
            }

            //どれにも当てはまらない
            rankName = "役はありません";
            rankStrength = 0;

        }

        int JudgeContinueNumber()
        {
            //手札の数字だけを抜き出し、配列に入れる
            var temp = new List<int>();
            for (int i = 0; i < handCards.Count; i++)
            {
                temp.Add(handCards[i].number);
            }

            //連番判定のために配列をソートする
            temp.Sort();

            //次のインデックスの中身、インデックスの中身に1を足したものを比較し、
            //同じならcountに1を足す
            int count = 0;
            for (int i = 0; i < temp.Count - 1; i++)
            {
                if (temp[i + 1] == temp[i] + 1)
                {
                    count++;
                }
            }
            //全ての要素を判定した後、比較したすべてが一致していれば連番
            if (count == 4)
            {
                //数字が連続しているだけをパターン1とみなし1を返す
                return 1;
            }
            // 1,10,?,?,?でcountが3なら1,10,11,12,13ということになる
            else if (count == 3 && temp[0] == 1 && temp[1] == 10)
            {
                //ロイヤルストレートフラッシュの可能性があるものをパターン2とみなし2を返す
                return 2;
            }
            //(1,2,3,4,13)(1,2,3,12,13)(1,2,11,12,13)の場合もストレート
            else if (count == 3 && temp[0] == 1 && temp[4] == 13)
            {
                //数字が連続しているだけをパターン1とみなし1を返す
                return 1;
            }
            else
            {
                //0をfalseと同義とみなし、0を返す
                return 0;
            }

        }

        (bool, string) JudgeSameMark()
        {
            var modelCards = new Card("0", 0);

            for (int i = 0; i < modelCards.marks.Length; i++)
            {
                int count = 0;
                foreach (var h in handCards)
                {
                    //マークiを含むならばカウントを1増やす
                    if (h.mark.Contains($"{modelCards.marks[i]}"))
                    {
                        count++;
                    }
                }
                //マークiを5枚判定した後で、5枚すべてがマークiを含むのならば全て同じマーク
                if (count == 5)
                {
                    //Console.WriteLine($"TEST    全て{marks[i]}です");
                    //すべて同じマークであることと、どのマークなのかを返す
                    return (true, modelCards.marks[i]);
                }
            }
            //すべて同じマークではないことと、nullを返す
            return (false, null);
        }

    }
}
