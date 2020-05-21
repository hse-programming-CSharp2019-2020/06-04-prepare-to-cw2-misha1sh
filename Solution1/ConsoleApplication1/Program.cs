using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Lib;

namespace ConsoleApplication1 {
    internal class Program {
        public static int ReadInt(string name, Predicate<int> check) {
            Console.WriteLine("Введите " + name);
            int res;
            while (!int.TryParse(Console.ReadLine(), out res) || !check(res)) {
                Console.WriteLine("Ошибка. Введите " + name);
            }

            return res;
        }

        private static Random random = new Random();

        private static string RandomString() {
            char[] chars = new char[random.Next(3, 8)];
            for (int i = 0; i < chars.Length; i++) {
                chars[i] = (char) random.Next('a', 'z' + 1);
            }
            return new string(chars);
        }
        
        private static Street RandomStreet() {
            int[] houses = new int[random.Next(2, 10)];
            for (int i = 0; i < houses.Length; i++) {
                houses[i] = random.Next(1, 101);
            }
            return new Street(RandomString(), houses);
        }
        
        public static void Main(string[] args) {
            do {
                int N = ReadInt("количество улиц", n => n > 0);
                Console.WriteLine("Нажмите escape, чтобы выйти из прграммы");
                Street[] streetsArray = new Street[N];
                
                try {
                    string[] lines = File.ReadAllLines("data.txt", Encoding.GetEncoding(1251));
                    streetsArray = new Street[lines.Length];
                    for (int i = 0; i < lines.Length; i++) {
                        string[] spl = lines[i].Split();
                        string name = spl[0];
                        int[] houses = spl.Skip(1).Select((x, _) => int.Parse(x)).ToArray();
                        if (houses.Length == 0 || !houses.All(x => x > 0 && x < 100))
                            throw new Exception("Непоhвильный формат файла");
                        streetsArray[i] = new Street(name, houses);
                    }
                }
                catch (IOException exception) {
                    Console.WriteLine("Не удалось прочитать файл");
                }
                catch (Exception exception) {
                    Console.WriteLine("Файл имеет неправильный формат");
                    streetsArray = new Street[N];
                    for (int i = 0; i < streetsArray.Length; i++) {
                        streetsArray[i] = RandomStreet();
                    }
                }


                foreach (var street in streetsArray) {
                    Console.WriteLine(street);
                }

                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(Street[]));
                    using (var stream = File.Open("out.ser", FileMode.Create, FileAccess.Write)) {
                        serializer.Serialize(stream, streetsArray);
                    }
                }
                catch (IOException exception) {
                    Console.WriteLine("Ошибка при записи в файл " + exception.Message);
                }
                catch (Exception exception) {
                    Console.WriteLine("Неизвестная ошибка при записи в файл " + exception.Message);
                }
                
                Console.WriteLine("Нажмите escape для выхода из приложения");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}