using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Inn
    {
        public static void RecoverHP(Player player)
        {
            Display.DisplayInn(player);

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    Console.WriteLine("\n\n휴식하며 체력을 회복 중...");
                    Thread.Sleep(1000);
                    Console.WriteLine("\n 체력이 회복되었습니다!");
                    player.Health = Player.maxHealth;
                    player.Gold -= 500;
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
            
        }
    }
}
