using System;

namespace Lib {
    [Serializable]
    public class Street {
        public string name;
        public int[] houses;

        public Street() {}
        public Street(string name, int[] houses) {
            this.name = name;
            this.houses = houses;
        }

        public static int operator ~(Street street) {
            return street.houses.Length;
        }

        public static bool operator +(Street street) {
            foreach (var house in street.houses) {
                if (house == 7) return true;
            }

            return false;
        }

        public override string ToString() {
            return "Street " + name + " houses: " + String.Join(",", houses);
        }
    }
}