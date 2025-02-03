namespace MobWx.Lib.Models.Base
{
    /// <summary>
    /// Supports Position and NullPosition objects.
    /// </summary>
    public abstract class PositionBase
    {
        public abstract string GetLatitude();
        public abstract string GetLongitude();
        public abstract string GetAltitude();
    }
}
