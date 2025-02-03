using MobWx.Lib.Models.Base;

namespace MobWx.Lib.Models
{
    public class NullPosition : PositionBase
    {
        public override string GetAltitude()
        {
            throw new NotImplementedException();
        }

        public override string GetLatitude()
        {
            throw new NotImplementedException();
        }

        public override string GetLongitude()
        {
            throw new NotImplementedException();
        }
    }
}
