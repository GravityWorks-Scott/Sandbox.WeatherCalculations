using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.WeatherCalculations
{
    public static class Calculations
    {
        public static double CalculateWindchill(double TemperatureInFahrenheit, double WindSpeedInMPH)
        {
            return (WindSpeedInMPH == 0) ? TemperatureInFahrenheit : (TemperatureInFahrenheit >= 76) ? TemperatureInFahrenheit : (35.74 + (0.62515 * TemperatureInFahrenheit) - 35.75 * (Math.Pow(WindSpeedInMPH, 0.16)) + (0.4275 * TemperatureInFahrenheit) + (Math.Pow(WindSpeedInMPH, 0.16)));
        }

        public static double CalculateHeatIndex(double TemperatureInFahrenheit, int Humidity)
        {
            int TempIndex = Convert.ToInt32(Math.Round(TemperatureInFahrenheit)) + 57;
            int HumidityIndex = (Humidity / 5);
            if ((TempIndex < 0) || (TempIndex > HeatIndexTable.Table.Length))
                throw new Exception("Invalid temperature + humidity");
            else
                return HeatIndexTable.Table[TempIndex][HumidityIndex];
        }

        public static double CalculateDewPoint_Method1(double TemperatureInCelsius, int RelativeHumidity)
        {
            double VapourPressureValue = RelativeHumidity * 0.01 * 6.112 * Math.Exp((17.62 * TemperatureInCelsius) / (TemperatureInCelsius + 243.12));
            double Numerator = 243.12 * Math.Log(VapourPressureValue) - 440.1;
            double Denominator = 19.43 - (Math.Log(VapourPressureValue));
            return Numerator / Denominator;
        }

        public static double CalculateDewPoint_Method2(double TempertatureInF, int RelativeHumidity)
        {

            double TemperatureInC = ((TempertatureInF - 32.0) * 5.0) / 9.0;
            double relativeHumidityPercent = RelativeHumidity / 100.0;
            double d = (relativeHumidityPercent * 6.112) * Math.Exp((17.62 * TemperatureInC) / (TemperatureInC + 243.12));
            double num4 = ((243.12 * Math.Log(d)) - 440.1) / (19.42 - Math.Log(d));
            return Math.Round((double)((num4 * 1.8) + 32.0), 1);
        }

        public static double CalculateHumidity(double TemperatureInF, double DewPointInF)
        {
            double TemperatureInC = ((TemperatureInF - 32.0) * 5.0) / 9.0;
            double DewPointInC = ((DewPointInF - 32.0) * 5.0) / 9.0;
            double RelativeHumidityNumerator = 6.112 * Math.Exp((17.62 * TemperatureInC) / (243.12 + TemperatureInC));
            double RelativeHumidityDenominator = 6.112 * Math.Exp((17.62 * DewPointInC) / (243.12 + DewPointInC));
            return Math.Round((double)((RelativeHumidityDenominator / RelativeHumidityNumerator) * 100.0), 0);
        }
    }
}
