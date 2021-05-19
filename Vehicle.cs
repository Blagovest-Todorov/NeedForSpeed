using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class Vehicle
    {
        private const double DefaultFuelCosumption = 1.25;
        public Vehicle(int horsePower, double fuel)
        {
            this.Fuel = fuel;
            this.HorsePower = horsePower;
        }

        public int HorsePower { get; set; }
        public double Fuel { get; set; }        

        public virtual double FuelConsumption => DefaultFuelCosumption;  // per kms 

        public virtual void Drive(double kilometers) 
        {
            this.Fuel = this.Fuel - kilometers * this.FuelConsumption;
        }
    }
}
