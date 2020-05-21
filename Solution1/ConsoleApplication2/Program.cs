using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Lib;

namespace ConsoleApplication2 {
    internal class Program {
        public static void Main(string[] args) {
            do {
                Street[] streetsArray;
                try {
                
                    XmlSerializer serializer = new XmlSerializer(typeof(Street[]));
                    using (var stream = File.Open("../../../ConsoleApplication1/bin/Debug/out.ser", FileMode.Open, FileAccess.Read)) {
                        streetsArray = (Street[]) serializer.Deserialize(stream);
                    }
                }
                catch (IOException exception) {
                    Console.WriteLine("Ошибка при чтении в файла " + exception.Message);
                    continue;
                }
                catch (Exception exception) {
                    Console.WriteLine("Неизвестная ошибка при чтении в файла " + exception.Message);
                    continue;
                }

                var magicStreets = (from street in streetsArray
                    where (~street) % 2 == 1 && +street
                    select street).ToArray();

                Console.WriteLine("Волшебные улицы:");
                foreach (var street in magicStreets) {
                    Console.WriteLine(street);
                }
                
                Console.WriteLine("Нажмите escape для выхода из приложения");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
          
        }
    }
}