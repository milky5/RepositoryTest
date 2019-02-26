using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPorker
{
    class Program
    {

        static void Main(string[] args)
        {

        }

        void TempMain()
        {
            Dealer dealer = new Dealer();
            Cpu cpu = new Cpu();
            Player player = new Player();
            //Game game = new Game();
            //Menu menu = new Menu();

            //cpu.CardScreen.Add();
            //player.CardScreen.Add();

            List<Participant> participants = new List<Participant>();
            participants.Add(cpu);
            participants.Add(player);

            dealer.ShuffleCard();
            dealer.DealCard(participants);
            //(コンピュータとプレイヤーに手札を受け渡す)
            cpu.Judge();
            cpu.ThinkExchangeCards(cpu.rankName);
            player.SelectExchangeCard();
            dealer.ReDealCard(participants);
            player.Judge();
            //(DealerクラスのCompareRankにそれぞれの役を受け渡す)
            string result = dealer.CompareRank(participants);
            //結果をTextBox.Textに代入
            //TextBox.Text = result;
        }
    }
}
