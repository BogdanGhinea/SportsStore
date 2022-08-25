using System;

namespace Konstruktoren {
    class Program {
        static void Main(string[] args) {
            Sub2 s = new Sub2(42);
        }
    }
    class Basis {
        public Basis(int value) {
            Console.WriteLine("Basis" + value);
        }
    }
    class Sub1 : Basis {
        public Sub1(int value) : base(value) {
            Console.WriteLine("Sub1" + value);
        }
    }
    class Sub2 : Sub1 {
        public Sub2(int value) : base(value) {
            Console.WriteLine("Sub2" + value);
        }
    }
}
