using System;
using System.Collections.Generic;
using System.Linq;

namespace NeedForSpeed
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, double> mileageByCar = new Dictionary<string, double>();
            Dictionary<string, double> fuelByCar = new Dictionary<string, double>();

            int numCars = int.Parse(Console.ReadLine());            

            for (int i = 0; i < numCars; i++)
            {
                string[] carsData = Console.ReadLine().Split("|");  // {car}|{mileage}|{fuel}
                string carName = carsData[0];
                double carMileage = double.Parse(carsData[1]);
                double carFuel = double.Parse(carsData[2]);

                if (!mileageByCar.ContainsKey(carName))
                {
                    mileageByCar.Add(carName, carMileage);
                    fuelByCar.Add(carName, carFuel);
                }
            }

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "Stop")
                {
                    Dictionary<string, double> sortedMileageByCar = mileageByCar
                        .OrderByDescending(x => x.Value)
                        .ThenBy(x => x.Key)
                        .ToDictionary(x =>x.Key, x => x.Value);

                    foreach (var kvp in sortedMileageByCar)
                    {
                        string currNameCar = kvp.Key;
                        double currKmsCar = kvp.Value;

                        Console.WriteLine($"{currNameCar} -> Mileage: {currKmsCar} kms, " +
                            $"Fuel in the tank: {fuelByCar[currNameCar]} lt.");
                    }

                    break;
                }

                string[] dataCmds = line.Split(" : ", StringSplitOptions.RemoveEmptyEntries);
                string command = dataCmds[0];
                string carName = dataCmds[1];

                if (command == "Drive")
                {
                    double distance = double.Parse(dataCmds[2]);
                    double neededFuel = double.Parse(dataCmds[3]);

                    if (fuelByCar[carName] < neededFuel)
                    {
                        Console.WriteLine("Not enough fuel to make that ride");
                    }
                    else // if (fuelByCar[carName] >= neededFuel)
                    {
                        mileageByCar[carName] += distance;
                        fuelByCar[carName] -= neededFuel;
                        Console.WriteLine($"{carName} driven for {distance} kilometers." +
                            $" {neededFuel} liters of fuel consumed.");
                    }

                    if (mileageByCar[carName] >= 100000)
                    {
                        Console.WriteLine($"Time to sell the {carName}!");
                        fuelByCar.Remove(carName);
                        mileageByCar.Remove(carName);
                    }
                }
                else if (command == "Refuel")
                { //Refuel : {car} : {fuel}   // max 75 liters can holf the Tank
                    double litersFuel = double.Parse(dataCmds[2]);
                    double freeLitersToFill = 75 - fuelByCar[carName];

                    if (freeLitersToFill < litersFuel)
                    {
                        fuelByCar[carName] += freeLitersToFill;
                        Console.WriteLine($"{carName} refueled with {freeLitersToFill} liters");
                    }
                    else // if (freeLitersToFill >= litersFuel)
                    {
                        fuelByCar[carName] += litersFuel;
                        Console.WriteLine($"{carName} refueled with {litersFuel} liters");
                    }
                }
                else // if (command == "Revert") 
                { // Revert : {car} : {kilometers}
                    double mileageOfferedToRevert = double.Parse(dataCmds[2]);
                    double currKms = mileageByCar[carName];

                    if (currKms - mileageOfferedToRevert >= 10000)
                    {
                        mileageByCar[carName] -= mileageOfferedToRevert;
                        Console.WriteLine($"{carName} mileage decreased by {mileageOfferedToRevert}" +
                            $" kilometers");
                    }
                    else // if (currKms - mileageOfferedToRevert < 10000)
                    {
                        mileageByCar[carName] = 10000;
                    }
                }
            }
        }
    }
}
