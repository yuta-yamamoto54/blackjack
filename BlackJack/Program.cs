using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> cards = new List<int>();
            List<int> cpu_cards = new List<int>();
            List<int> player_cards = new List<int>();
            int player_score = 0;
            int cpu_score = 0;
            (string mark, string number) card_name;
            int card;
            string player_choice = "y";
            Random rand = new Random();

            for (int i = 0; i < 52; i++)
            {
                cards.Add(i);
            }
            
            for (int i = 0; i < 2; i++)
            {
                card = cards[rand.Next(cards.Count())];
                cards.Remove(card);
                cpu_cards.Add(card);
            }
            card_name = GetCardName(cpu_cards[0]);
            Console.WriteLine("ディーラーの引いた一枚目のカード：{0}の{1}", card_name.mark,card_name.number);
            Console.WriteLine("ディーラーの引いた二枚目のカード：？？？");

            for (int i = 0; i < 2; i++)
            {
                card = cards[rand.Next(cards.Count())];
                cards.Remove(card);
                player_cards.Add(card);
            }
            card_name = GetCardName(player_cards[0]);
            Console.WriteLine("プレイヤーの引いた一枚目のカード：{0}の{1}", card_name.mark, card_name.number);
            card_name = GetCardName(player_cards[1]);
            Console.WriteLine("プレイヤーの引いた二枚目のカード：{0}の{1}", card_name.mark, card_name.number);

            Console.WriteLine("あなたの現在の得点は：{0}", GetScore(player_cards));

            while (player_choice == "y") {
                Console.WriteLine("カードを引きますか？y/n");
                player_choice = Console.ReadLine();

                if (player_choice == "y")
                {
                    card = cards[rand.Next(cards.Count())];
                    cards.Remove(card);
                    player_cards.Add(card);
                    card_name = GetCardName(card);
                    Console.WriteLine("プレイヤーの引いたカード：{0}の{1}", card_name.mark, card_name.number);
                }
                player_score = GetScore(player_cards);
                Console.WriteLine("あなたの現在の得点は{0}です",player_score);

                if (player_score >= 21 || player_choice == "n") break;

            }
            cpu_score = GetScore(cpu_cards);
            Console.WriteLine("ディーラーの現在の得点は：{0}", cpu_score);

            while (cpu_score < 17)
            {
                card = cards[rand.Next(cards.Count())];
                cards.Remove(card);
                cpu_cards.Add(card);
                card_name = GetCardName(card);
                Console.WriteLine("ディーラーの引いたカード：{0}の{1}", card_name.mark, card_name.number);
                cpu_score = GetScore(cpu_cards);
                Console.WriteLine("ディーラーの現在の得点は：{0}", cpu_score);
            }

            if (player_score > 21)
            {
                if (cpu_score > 21)
                {
                    Console.WriteLine("引き分け");
                }
                else
                {
                    Console.WriteLine("ディーラーの勝利");
                }
            }
            else
            {
                if (cpu_score > 21)
                {
                    Console.WriteLine("プレイヤーの勝ち");
                }
                else if (cpu_score < player_score)
                {
                    Console.WriteLine("プレイヤーの勝ち");
                }
                else
                {
                    Console.WriteLine("ディーラーの勝ち");
                }
            }
        
            Console.ReadLine();
        }

        static public (string mark,string number) GetCardName(int card)
        {
            string mark = "";
            string number = "";
            int tmp = card / 13;
            if (tmp == 0) { mark = "ハート"; }
            else if (tmp == 1) { mark = "ダイヤ"; }
            else if (tmp == 2) { mark = "エース"; }
            else if (tmp == 3) { mark = "クローバー"; }

            number = (1 + card % 13).ToString();
            if (number == "1") { number = "A"; }
            else if (number == "11") { number = "J"; }
            else if (number == "12") { number = "Q"; }
            else if (number == "13") { number = "K"; }

            return (mark, number);
        }

        static public int GetScore(List<int> hand_cards)
        {
            int sum_score = 0;
            for (int i = 0; i < hand_cards.Count(); i++)
            {
                int score = (1 + hand_cards[i] % 13);
                if (score >= 10) score = 10;
                sum_score += score;
            }
            return sum_score;
        }
    }
}
