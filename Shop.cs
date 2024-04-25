using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Shop
    {
        public static void ShopManagement(Player player, List<Item> items)
        {
            
            bool shopping = true;
            
            while (shopping)
            {
                Display.DisplayShop(player, items);
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Shop.BuyItem(player, items);
                        break;
                    case "2":
                        Shop.SellItem(player, items);
                        break;
                    case "0":
                        shopping = false;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
        public static void BuyItem(Player player, List<Item> items)
        {
            bool isBuyItem = true;

            while (isBuyItem)
            {
                Display.DisplayBuyItem(player, items);

                string itemInput = Console.ReadLine();
                if (itemInput == "0")
                {
                    isBuyItem = false;
                    continue;  // 0을 입력하면 구매 메뉴 종료
                }

                if (int.TryParse(itemInput, out int itemNumber) && itemNumber > 0 && itemNumber <= items.Count)
                {
                    itemNumber -= 1; // 사용자 입력을 배열 인덱스에 맞게 조정
                    if (!player.Inventory.Contains(items[itemNumber]))
                    {
                        if (player.Gold >= items[itemNumber].Price)
                        {
                            player.Inventory.Add(items[itemNumber]);
                            player.Gold -= items[itemNumber].Price;
                            Console.WriteLine("\n구매를 완료했습니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine("\nGold가 부족합니다.");
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n이미 구매한 아이템입니다.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("\n유효한 아이템 번호를 입력해주세요.");
                    Thread.Sleep(1000);
                }
            }
        }

        public static void SellItem(Player player, List<Item> items)
        {
            bool isSellItem = true;

            while (isSellItem)
            {
                Display.DisplaySellItem(player, items);

                string itemInput = Console.ReadLine();
                if (itemInput == "0")
                {
                    isSellItem = false;
                    continue;  // 0을 입력하면 구매 메뉴 종료
                }

                if (int.TryParse(itemInput, out int itemNumber) && itemNumber > 0 && itemNumber <= items.Count)
                {
                    itemNumber -= 1; // 사용자 입력을 배열 인덱스에 맞게 조정
                    Item itemToSell = player.Inventory[itemNumber];
                    player.Inventory.RemoveAt(itemNumber);
                    if (player.EquippedItems.Contains(itemToSell))
                    {
                        player.EquippedItems.Remove(itemToSell);
                    }
                    player.Gold += (int)Math.Floor(itemToSell.Price * 0.85);
                    Console.WriteLine("\n아이템을 판매했습니다.");
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
            
        }
    }
}
