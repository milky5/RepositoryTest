using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPorker
{
    class Cpu : Participant
    {
        //CPUの思考ルーチン
        public void ThinkExchangeCards(string rankName)
        {
            //持ち札のnumber部分だけ取り出す
            List<int> numberTemp = new List<int>();
            for (int i = 0; i < handCards.Count; i++)
            {
                numberTemp.Add(handCards[i].number);
            }
            //持ち札のmark部分だけ取り出す
            List<string> markTemp = new List<string>();
            for (int i = 0; i < handCards.Count; i++)
            {
                markTemp.Add(handCards[i].mark);
            }

            Card modelCard = new Card("0", 0);

            switch (rankName)
            {
                case "ワンペア":
                    for (int i = 0; i < numberTemp.Count; i++)
                    {
                        if (numberTemp.Where(n => n == i).Count() == 2)
                        {
                            wantExchangeCards = numberTemp.Where(n => n != i)
                                                          .Select(n => n)
                                                          .ToList();
                            break;
                        }
                    }
                    break;

                case "ツーペア":
                    for (int i = 0; i < numberTemp.Count; i++)
                    {
                        if (numberTemp.Where(n => n == i).Count() == 2)
                        {
                            for (int j = i + 1; j < numberTemp.Count; j++)
                            {
                                if (numberTemp.Where(n => n == j).Count() == 2)
                                {
                                    wantExchangeCards = numberTemp.Where(n => n != i && n != j)
                                                                  .Select(n => n)
                                                                  .ToList();
                                    break;
                                }
                            }
                        }
                    }
                    break;

                case "スリーカード":
                    for (int i = 0; i < numberTemp.Count; i++)
                    {
                        if (numberTemp.Where(n => n == i).Count() == 3)
                        {
                            wantExchangeCards = numberTemp.Where(n => n != i)
                                                          .Select(n => n)
                                                          .Select(n => n)
                                                          .ToList();
                            break;
                        }
                    }
                    break;

                case "ストレート":
                    //フラッシュ狙えるか？
                    //以下のパターンAと、このままいくパターンB
                    for (int i = 0; i < modelCard.marks.Length; i++)
                    {
                        int countt = 0;
                        foreach (var h in handCards)
                        {
                            //マークiを含むならばカウントを1増やす
                            if (h.mark.Contains($"{modelCard.marks[i]}"))
                            {
                                countt++;
                            }
                        }
                        //4枚がマークiを含むのなら
                        if (countt == 4)
                        {
                            for (int j = 0; j < numberTemp.Count; j++)
                            {
                                if (!markTemp[j].Contains($"{modelCard.marks[i]}"))
                                {
                                    wantExchangeCards.Add(j);
                                    break;
                                }
                            }
                        }
                    }
                    break;

                case "フラッシュ":
                    //ストレート狙えるか？
                    //以下のパターンAと、このままいくパターンB
                    //連番判定のために配列をソートする
                    numberTemp.Sort();
                    //次のインデックスの中身、インデックスの中身に1を足したものを比較し、
                    //同じならcountに1を足す
                    int count = 0;
                    for (int i = 0; i < numberTemp.Count - 1; i++)
                    {
                        if (numberTemp[i + 1] == numberTemp[i] + 1)
                        {
                            count++;
                        }
                    }
                    //全ての要素を判定した後、比較した4/3が一致していれば連番候補
                    if (count == 3)
                    {
                    }
                    //(1,2,3,4,13)(1,2,3,12,13)(1,2,11,12,13)(1,10,11,12,13)の場合の一つ抜けもストレート候補
                    else if (count == 2 && numberTemp[0] == 1 && numberTemp[4] == 13)
                    {
                    }
                    break;

                case "ストレートフラッシュ":
                    var rsf = new List<int> { 1, 10, 11, 12, 13 };
                    for (int i = 0; i < numberTemp.Count; i++)
                    {
                        if (numberTemp[i] != rsf[i])
                        {
                            wantExchangeCards.Add(i);
                        }
                    }
                    break;

                case "役はありません":
                    //フラッシュ狙えるか？
                    for (int i = 0; i < modelCard.marks.Length; i++)
                    {
                        int countt = 0;
                        foreach (var h in handCards)
                        {
                            //マークiを含むならばカウントを1増やす
                            if (h.mark.Contains($"{modelCard.marks[i]}"))
                            {
                                countt++;
                            }
                        }
                        //4枚がマークiを含むのなら
                        if (countt == 4)
                        {
                            for (int j = 0; j < numberTemp.Count; j++)
                            {
                                if (!markTemp[j].Contains($"{modelCard.marks[i]}"))
                                {
                                    wantExchangeCards.Add(j);
                                    break;
                                }
                            }
                        }
                    }

                    //ストレート狙えるか？
                    //以下のパターンAと、このままいくパターンB
                    //連番判定のために配列をソートする
                    numberTemp.Sort();
                    //次のインデックスの中身、インデックスの中身に1を足したものを比較し、
                    //同じならcountに1を足す
                    int counttt = 0;
                    for (int i = 0; i < numberTemp.Count - 1; i++)
                    {
                        if (numberTemp[i + 1] == numberTemp[i] + 1)
                        {
                            counttt++;
                        }
                    }
                    //全ての要素を判定した後、比較した4/3が一致していれば連番候補
                    if (counttt == 3)
                    {
                    }
                    //(1,2,3,4,13)(1,2,3,12,13)(1,2,11,12,13)(1,10,11,12,13)の場合の一つ抜けもストレート候補
                    else if (counttt == 2 && numberTemp[0] == 1 && numberTemp[4] == 13)
                    {
                    }
                    break;



                case "フルハウス":
                case "フォーカード":
                case "ロイヤルストレートフラッシュ":
                    break;
                default:
                    break;
            }
        }

    }
}
