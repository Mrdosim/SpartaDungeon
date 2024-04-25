using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeon
{
    internal class Display
    {
        public static void DrawTitle()
        {
            Console.SetCursorPosition(15, 2);
            Console.WriteLine(" $$$$$$\\ $$$$$$$\\  $$$$$$\\ $$$$$$$\\$$$$$$$$\\ $$$$$$\\        $$$$$$$\\ $$\\   $$\\$$\\   $$\\ $$$$$$\\ $$$$$$$$\\ $$$$$$\\ $$\\   $$\\ ");
            Console.SetCursorPosition(15, 3);
            Console.WriteLine("$$  __$$\\$$  __$$\\$$  __$$\\$$  __$$\\__$$  __$$  __$$\\       $$  __$$\\$$ |  $$ $$$\\  $$ $$  __$$\\$$  _____$$  __$$\\$$$\\  $$ |" );
            Console.SetCursorPosition(15, 4);
            Console.WriteLine   ("$$ /  \\__$$ |  $$ $$ /  $$ $$ |  $$ | $$ |  $$ /  $$ |      $$ |  $$ $$ |  $$ $$$$\\ $$ $$ /  \\__$$ |     $$ /  $$ $$$$\\ $$ |" );
            Console.SetCursorPosition(15, 5);
            Console.WriteLine   ("\\$$$$$$\\ $$$$$$$  $$$$$$$$ $$$$$$$  | $$ |  $$$$$$$$ |      $$ |  $$ $$ |  $$ $$ $$\\$$ $$ |$$$$\\$$$$$\\   $$ |  $$ $$ $$\\$$ |" );
            Console.SetCursorPosition(15, 6);
            Console.WriteLine   (" \\____$$\\$$  ____/$$  __$$ $$  __$$<  $$ |  $$  __$$ |      $$ |  $$ $$ |  $$ $$ \\$$$$ $$ |\\_$$ $$  __|  $$ |  $$ $$ \\$$$$ |" );
            Console.SetCursorPosition(15, 7);
            Console.WriteLine   ("$$\\   $$ $$ |     $$ |  $$ $$ |  $$ | $$ |  $$ |  $$ |      $$ |  $$ $$ |  $$ $$ |\\$$$ $$ |  $$ $$ |     $$ |  $$ $$ |\\$$$ |" );
            Console.SetCursorPosition(15, 8); 
            Console.WriteLine   ("\\$$$$$$  $$ |     $$ |  $$ $$ |  $$ | $$ |  $$ |  $$ |      $$$$$$$  \\$$$$$$  $$ | \\$$ \\$$$$$$  $$$$$$$$\\ $$$$$$  $$ | \\$$ |" );
            Console.SetCursorPosition(15, 9); 
            Console.WriteLine   (" \\______/\\__|     \\__|  \\__\\__|  \\__| \\__|  \\__|  \\__|      \\_______/ \\______/\\__|  \\__|\\______/\\________|\\______/\\__|  \\__|n\n\n\n\n\n" );

        }

        public static void DrawMain(Player player)
        {
            Console.Clear();
            Display.DrawTitle();

            Console.WriteLine($"{player.Name}님! 스파르타 마을에 오신 것을 환영합니다!");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 게임 저장");
            Console.WriteLine("7. 게임 종료");
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
        }

        public static void DisplayStatus(Player player)
        {
            bool status = true;
            
            
            while (status)
            {
                Console.Clear();
                DrawTitle();

                Console.WriteLine($"\nLv. {player.Level} \n\n이름 : {player.Name} ( {player.CharacterClass} )  \n\n공격력 : {player.Attack} ({Item.GetStatChange(player,ItemType.Weapon)}) \n\n방어력 : {player.Defense} ({Item.GetStatChange(player, ItemType.Armor)}) \n\n체력 : {player.Health} \n\n골드 : {player.Gold} G");
                Console.WriteLine("\n\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. >> ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int numInput) && numInput == 0)
                {
                    status = false;
                }
            }
        }

        public static void DisplayInventory(Player player)
        {
            Console.Clear();
            DrawTitle();

            Console.WriteLine("[인벤토리]\n");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    string equipStatus = player.EquippedItems.Contains(player.Inventory[i]) ? "[E]" : "   ";
                    Console.WriteLine($"{equipStatus}{player.Inventory[i].Name,-20} | {player.Inventory[i].EffectDescription,20} | {player.Inventory[i].Description,20}");
                }
            }
            Console.WriteLine("\n1. 장비 장착");
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
        }
        public static void DisplayShop(Player player, List<Item> items)
        {

                Console.Clear();
                DrawTitle();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]: " + player.Gold + " G\n");
                
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. >> ");
           
        }
        public static void DisplayBuyItem(Player player, List<Item> items)
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("[보유 골드]: " + player.Gold + " G\n");
            Console.WriteLine("[아이템 목록]\n");
            for (int i = 0; i < items.Count; i++)
            {
                string purchased = player.Inventory.Contains(items[i]) ? "구매완료" : items[i].Price + " G";
                Console.WriteLine($"{i + 1}. {items[i].Name,-20}|     {items[i].EffectDescription,20}|     {items[i].Description,20}     |{purchased,20}");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n구매할 아이템 번호를 입력하세요: ");
        }
        public static void DisplaySellItem(Player player, List<Item> items)
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("[보유 골드]: " + player.Gold + " G\n");
            Console.WriteLine("[보유 목록]\n");
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
            }
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Inventory[i].Name,-20}|     {items[i].EffectDescription,20}|     {items[i].Description,20}      {Math.Floor(player.Inventory[i].Price * 0.85)} G");
            }

            Console.WriteLine("\n0. 뒤로가기");
            Console.Write("\n판매할 아이템 번호를 입력하세요: ");
        }

       public static void DisplayDungeon()
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("[던전 입장]");
            Console.WriteLine("이곳은 던전의 입구입니다. 어느 던전으로 들어가시겠습니까?");

            Console.WriteLine("\n1.  쉬운 던전\t| 방어력 5이상 권장");
            Console.WriteLine("\n2.  일반 던전\t| 방어력 11이상 권장");
            Console.WriteLine("\n3.  어려운 던전\t| 방어력 17이상 권장");
            Console.WriteLine("\n0. 나가기");

            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
        }

        public static void DisplayDungeonClear(string dungeonName)
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("\n던전 탐험 중...");
            Thread.Sleep(1000);
            Console.WriteLine($"\n축하합니다!! {dungeonName}을 클리어 하였습니다.");
        }
        public static void DisplayDungeonFail(string dungeonName)
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("\n던전 탐험 중...");
            Thread.Sleep(1000);
            Console.WriteLine($"\n{dungeonName}에 실패하였습니다.");
        }

        public static void DisplayInn(Player player)
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("[휴식하기]");
            Console.WriteLine($"\n500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)");
            Console.WriteLine("\n1. 휴식하기");
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
        }

        public static void DisplaySaveGame()
        {
            Console.Clear();
            Display.DrawTitle();
            Console.WriteLine("[저장하기]");
            Console.WriteLine("\n1. 저장하기");
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요. >> ");
        }
        
    }
}
