using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SportCar sportCar = new SportCar(100, 50);
            sportCar.Drive(200);
            Console.WriteLine($"{sportCar.GetType().Name} {sportCar.Fuel}");
        }
    }
}
