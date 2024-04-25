using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Dungeon
    {

        public static void ManageDungeon(Player player)
        {
            bool isDungeon = true;
            
            while (isDungeon)
            {
                Display.DisplayDungeon();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n던전 진입중...");
                        Thread.Sleep(1000);
                        AttemptDungeon(player, 5, 1000, "쉬운 던전");
                        
                        break;
                    case "2":
                        Console.WriteLine("\n던전 진입중...");
                        Thread.Sleep(1000);
                        AttemptDungeon(player, 11, 1700, "일반 던전");
                        
                        break;
                    case "3":
                        Console.WriteLine("\n던전 진입중...");
                        Thread.Sleep(1000);
                        AttemptDungeon(player, 17, 2500, "어려운 던전");
                        
                        break;
                    case "0":
                        isDungeon = false;
                        return;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        break;
                }

            }
                
        }

        private static void AttemptDungeon(Player player, int recommendedDefense, int baseReward, string dungeonName)
        {
            if (player.Health > 0)
            {
                Random random = new Random();
                int healthDecrease = random.Next(20, 36);
                double finalHealthDecrease = healthDecrease + (player.Defense - recommendedDefense);
                int originReward = baseReward;
                int finalReward;
                

                if (player.Defense >= recommendedDefense)
                {
                    Display.DisplayDungeonClear(dungeonName);
                    int tempLevel = player.Level;
                    Player.LevelUp(player);
                    double tempHealth = player.Health;
                    double newHealth = player.Health - Math.Max(0, finalHealthDecrease);  // 체력 감소는 0 이하로 내려가지 않음
                    player.Health = Math.Max(0, newHealth); // 체력이 0보다 작아지지 않도록 보장
                    int bonusRewardCal = (int)(originReward * (player.Attack * 0.01));
                    int bonusReward = random.Next(bonusRewardCal, bonusRewardCal * 2);
                    finalReward = originReward + bonusReward;
                    player.Gold += originReward;
                    Console.WriteLine($"\n[탐험 결과]\n\nLV . {tempLevel} => {player.Level}\n체력 {tempHealth} => {player.Health}\nGold {player.Gold - originReward} G + {originReward} G + (추가 골드 {bonusReward} G) => {player.Gold} G");
                    Console.WriteLine("\n0. 나가기");

                    Console.Write("\n원하시는 행동을 입력해주세요. >> ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "0":
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;

                    }
                }
                else
                {
                    // 40프로 확률로 던전 실패
                    if (random.NextDouble() < 0.4)
                    {
                        Display.DisplayDungeonFail(dungeonName);
                        double tempHealth = player.Health;
                        double newHealth = player.Health - (player.maxHealth / 2);
                        if (newHealth > 0)
                        {
                            player.Health = newHealth;
                        }
                        else
                        {
                            player.Health = 0;
                        }
                        Console.WriteLine($"\n[탐험 결과]\n\n체력 {tempHealth} => {player.Health}");
                        Console.WriteLine("\n0. 나가기");

                        Console.Write("\n원하시는 행동을 입력해주세요. >> ");
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "0":
                                return;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;

                        }


                    }
                    // 던전 성공
                    else
                    {
                        Display.DisplayDungeonClear(dungeonName);
                        int tempLevel = player.Level;
                        Player.LevelUp(player);
                        double tempHealth = player.Health;
                        double newHealth = player.Health - finalHealthDecrease;
                        player.Health = Math.Max(0, newHealth);
                        int bonusRewardCal = (int)(originReward * (player.Attack * 0.01));
                        int bonusReward = random.Next(bonusRewardCal, bonusRewardCal * 2);
                        finalReward = originReward + bonusReward;
                        player.Gold += originReward;
                        Console.WriteLine($"\n[탐험 결과]\n\nLV . {tempLevel} => {player.Level}\n체력 {tempHealth} => {player.Health}\nGold {player.Gold - originReward} G + {originReward} G + (추가 골드 {bonusReward} G) => {player.Gold} G");
                        Console.WriteLine("\n0. 나가기");

                        Console.Write("\n원하시는 행동을 입력해주세요. >> ");
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "0":
                                return;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\n체력이 부족합니다!!");
            }
            
        }



    }
}
