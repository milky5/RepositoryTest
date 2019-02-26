using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPorker
{
    class Card
    {
        //カードのマークを代入する変数
        public string mark;
        //カードの数字を代入する変数
        public int number;
        //カード画像ののファイルパスを保持する変数
        public string filePass;
        //マーク4種が入る配列を宣言
        public string[] marks = new string[] { "Spade", "Diamond", "Heart", "Clover" };

        //コンストラクタ
        public Card(string mark, int number)
        {
            this.mark = mark;
            this.number = number;
            filePass = $@"..\..\Resources\CardPicture\{this.mark}{this.number}.png";
        }
    }
}
