using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Inventory
    {
        public static void ManageInventory(Player player)
        {
            bool isInventory = true;

            while (isInventory)
            {
                Display.DisplayInventory(player);
                
                string menuInput = Console.ReadLine();
                int menuChoice;

                if (int.TryParse(menuInput, out menuChoice))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Console.WriteLine("\n0. 취소");
                            Console.Write("\n아이템 이름을 입력하세요 (장착/해제): ");
                            string itemName = Console.ReadLine();
                            if (int.TryParse(itemName, out int inputNum) && inputNum == 0)
                            {
                                break;
                            }
                            HandleItemByName(itemName, player);
                            break;
                        case 0:
                            isInventory = false;
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다. 유효한 선택을 입력해주세요.");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해야 합니다.");
                    Thread.Sleep(1000);
                }
            }
        }

        private static string NormalizeItemName(string itemName)
        {
            // 정규 표현식을 사용하여 괄호와 괄호 안의 숫자 및 기타 문자 제거
            return Regex.Replace(itemName, @"\s*\([^)]*\)", "");
        }

        private static void HandleItemByName(string itemName, Player player)
        {
            // 아이템 이름 정규화
            string normalizedInput = NormalizeItemName(itemName);

            // 정규화된 이름으로 아이템 검색
            var item = player.Inventory.FirstOrDefault(i => NormalizeItemName(i.Name).Equals(normalizedInput, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                ToggleEquip(item, player);
            }
            else
            {
                Console.WriteLine("\n입력한 이름의 아이템이 인벤토리에 없습니다.");
                Thread.Sleep(1000);
            }
        }

        private static void ToggleEquip(Item item, Player player)
        {
            // 이미 장착된 아이템이 있는지 확인
            Item existingItem = player.EquippedItems.FirstOrDefault(i => i.Type == item.Type);

            // 이미 장착된 아이템이 있고, 장착하려는 아이템과 같지 않다면, 기존 아이템을 먼저 제거
            if (existingItem != null && !existingItem.Equals(item))
            {
                player.EquippedItems.Remove(existingItem);
                Console.WriteLine($"\n{existingItem.Name}의 장착을 해제했습니다.");
                Item.ApplyItemEffect(existingItem, player, -Item.ParseEffectValue(existingItem.EffectDescription)); // 기존 아이템 효과 제거
            }

            // 새 아이템을 장착하거나 이미 장착된 아이템을 제거
            if (player.EquippedItems.Contains(item))
            {
                player.EquippedItems.Remove(item);
                Console.WriteLine($"\n{item.Name}의 장착을 해제했습니다.");
                Item.ApplyItemEffect(item, player, -Item.ParseEffectValue(item.EffectDescription)); // 효과 제거
            }
            else
            {
                player.EquippedItems.Add(item);
                Console.WriteLine($"\n{item.Name}을(를) 장착했습니다.");
                Item.ApplyItemEffect(item, player, Item.ParseEffectValue(item.EffectDescription)); // 효과 적용
            }
            Thread.Sleep(1000);
        }



    }
}
