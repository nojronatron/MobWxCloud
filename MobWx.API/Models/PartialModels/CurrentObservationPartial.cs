namespace MobWx.API.Models;

public partial class CurrentObservation
{
    public static int ToFarenheit(double celsius)
    {
        double mult = 1.8;
        int offset = 32;
        return (int)Math.Round((celsius * mult + offset));
    }

    public static int ToVisibleMiles(int meters)
    {
        double mult = 0.000621371;
        return (int)Math.Round((meters * mult), 0);
    }

    public static int ToMilesPerHour(int kilometersPerHour)
    {
        double mult = 0.621371192;
        return (int)Math.Round(kilometersPerHour * mult);
    }

    public static double ToInchesMercury(double millibars)
    {
        double mult = 0.029529983071445;
        return Math.Round(millibars * mult, 2);
    }

    public static double ToMillibars(double inchesMercury)
    {
        double mult = 33.863886666667;
        return Math.Round(inchesMercury * mult, 2);
    }

    public static int ToInchesPrecip(int millimeters)
    {
        double mult = 0.0393700787;
        return (int)Math.Round(millimeters * mult);
    }

    public static int ToFeet(int meters)
    {
        double mult = 3.2808399;
        return (int)Math.Round(meters * mult);
    }
}
