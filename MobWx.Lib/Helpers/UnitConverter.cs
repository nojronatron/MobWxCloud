namespace MobWx.Lib.Helpers;

public class UnitConverter
{
    public static int ToFarenheit(double celsius)
    {
        double mult = 1.8;
        int offset = 32;
        return (int)Math.Round((celsius * mult + offset));
    }

    public static int ToMiles(int meters)
    {
        double mult = 0.000621371;
        return (int)Math.Round((meters * mult), 0);
    }

    public static int ToMilesPerHour(int kilometersPerHour)
    {
        double mult = 0.621371192;
        return (int)Math.Round(kilometersPerHour * mult);
    }

    public static decimal ToInchesMercury(int pascals)
    {
        // divide pascals by 3386.4 to get inches of mercury
        decimal mult = 3386.4m;
        return Math.Round(pascals / mult, 2);
    }

    public static decimal ToMillibars(int pascals)
    {
        // millibars are one 100th of a Pascal
        decimal mult = 0.01m;
        return Math.Round(pascals * mult, 1);
    }

    public static int ToInches(int millimeters)
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
