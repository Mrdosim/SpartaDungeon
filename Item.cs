using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public enum ItemType
    {
        Weapon,  // 무기
        Armor,   // 방어구
        Consumable // 소모품
    }

    class Item
    {
        public string Name { get;  set; }
        public int Price { get;  set; }
        public string Description { get;  set; }
        public string EffectDescription { get;  set; }
        public ItemType Type { get;  set; }  // 아이템 타입 속성 추가

        // 생성자에 아이템 타입 매개변수 추가
        public Item(string name, int price, string effectDescription, string description, ItemType type)
        {
            Name = name;
            Price = price;
            EffectDescription = effectDescription;
            Description = description;
            Type = type;  // 새 속성 초기화
        }


        // 아이템 리스트에 아이템 타입을 포함하여 초기화
        public static List<Item> Items = new List<Item>
        {
            new Item("수련자 갑옷", 1000, "방어력 +5", "수련에 도움을 주는 갑옷입니다.", ItemType.Armor),
            new Item("무쇠갑옷", 1500, "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor),
            new Item("스파르타의 갑옷", 3500, "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Armor),
            new Item("낡은 검", 600, "공격력 +2", "쉽게 볼 수 있는 낡은 검입니다.", ItemType.Weapon),
            new Item("청동 도끼", 1500, "공격력 +5", "어디선가 사용됐던 것 같은 도끼입니다.", ItemType.Weapon),
            new Item("스파르타의 창", 3000, "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon),
            new Item("나무 몽둥이(+9)", 1, "공격력 +9", "평해보이는 나뭇가지다 .누군가 장난삼아 강화했다.", ItemType.Weapon)
        };
        //아이템 효과 값 찾아오기
        public static int ParseEffectValue(string effectDescription)
        {
            var parts = effectDescription.Split(new[] { ' ', '+' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int value))
                {
                    return value;
                }
            }
            return 0;
        }
        //아이템 효과 캐릭터에 반영
        public static void ApplyItemEffect(Item item, Player player, int effectValue)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    player.Attack += effectValue;
                    break;
                case ItemType.Armor:
                    player.Defense += effectValue;
                    break;
            }
        }
        //상태창에 표시
        public static string GetStatChange(Player player, ItemType itemType)
        {
            int change = player.EquippedItems.Where(i => i.Type == itemType).Sum(i => ParseEffectValue(i.EffectDescription));
            return change > 0 ? $"+{change}" : $"{change}";
        }
    }
}
