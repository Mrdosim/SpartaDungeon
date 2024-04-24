using Newtonsoft.Json;

namespace SpartaDungeon
{
    internal class GameData
    {
        public static void ManageSave(Player player)
        {
            Display.DisplaySaveGame();

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    SaveGame(player);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    break;

            }
        }
        private static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SaveGame.json");

        // 게임 데이터를 JSON으로 저장하기
        public static void SaveGame(Player player)
        {
            string json = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("\n게임 데이터가 저장되었습니다.");
            Thread.Sleep(1000);
        }

        // JSON 파일에서 게임 데이터 로드하기
        public static Player LoadGame()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("저장된 게임 데이터가 없습니다.");
                    return null;
                }
                string json = File.ReadAllText(filePath);
                Player player = JsonConvert.DeserializeObject<Player>(json);
                Console.WriteLine("게임 데이터가 로드되었습니다.");
                return player;
            }
            catch (Exception e)
            {
                Console.WriteLine("게임 데이터 로드 중 오류가 발생했습니다: " + e.Message);
                return null;
            }
        }
    }

    
}
