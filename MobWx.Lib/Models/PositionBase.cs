namespace MobWx.Lib.Models
{
    /// <summary>
    /// Supports null Position objects.
    /// </summary>
    public abstract class PositionBase
    {
        public abstract string GetLatitude();
        public  abstract string GetLongitude();
        public abstract string GetAltitude();
    }
}
