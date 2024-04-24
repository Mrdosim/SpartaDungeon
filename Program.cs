namespace SpartaDungeon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        

        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 50); // 창의 너비와 높이 설정
            Console.SetBufferSize(150, 50);

            Player player = null;
            bool isRunning = true;

            // 게임 시작 메뉴
            Console.Clear();
            Console.SetCursorPosition(60, 18);
            Console.WriteLine("1. 새 게임 시작");
            Console.SetCursorPosition(60, 20);
            Console.WriteLine("2. 게임 로드");
            Console.SetCursorPosition(55, 24);
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(60, 26);
            Console.WriteLine(">> ");
            Console.SetCursorPosition(63, 26);
            string initialChoice = Console.ReadLine();
            Console.Clear();
            switch (initialChoice)
            {
                case "1":
                    player = new Player(1, "", "전사", 10, 5, 100, 1500);
                    while (true)
                    {
                        Console.SetCursorPosition(60, 18);
                        Console.Write("당신의 영웅의 이름을 입력하세요");
                        Console.SetCursorPosition(65, 22);
                        Console.WriteLine(">> ");
                        Console.SetCursorPosition(68, 22);
                        player.Name = Console.ReadLine();

                        // 입력받은 이름이 비어 있지 않은지 확인
                        if (!string.IsNullOrWhiteSpace(player.Name))
                        {
                            break;  // 이름이 유효하면 반복문을 종료
                        }
                        else
                        {
                            Console.SetCursorPosition(50, 20);
                            Console.WriteLine("이름을 입력해주세요. 이름은 비어 있을 수 없습니다.");
                            Thread.Sleep(500);
                            Console.Clear();
                        }
                    }
                    break;
                case "2":
                    player = GameData.LoadGame();
                    if (player == null)
                    {
                        Console.WriteLine("저장된 게임이 없습니다. 새 게임을 시작합니다.");
                        player = new Player(1, player.Name, "전사", 10, 5, 100, 1500);
                    }
                    break;
            }

            Console.Clear();
            Display.DrawTitle();
            Console.SetCursorPosition(65,15);
            Console.WriteLine("PRESS ENTER");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { } // 엔터 키가 눌릴 때까지 대기

            while (isRunning)
            {

                Display.DrawMain(player);

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Display.DisplayStatus(player);
                        break;
                    case "2":
                        Inventory.ManageInventory(player);
                        break;
                    case "3":
                        Display.DisplayShop(player, Item.Items);
                        break;
                    case "4":
                        Dungeon.ManageDungeon(player);
                        break;
                    case "5":
                        Inn.RecoverHP(player);
                        break;
                    case "6":
                        GameData.ManageSave(player);
                        break;
                    case "7":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        break;
                }
                
                
            }
        }

        

        
    }
}
