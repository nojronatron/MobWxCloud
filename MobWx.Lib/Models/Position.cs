namespace MobWx.Lib.Models
{
    public class Position : PositionBase
    {
        private string _latitude;
        public string _longitude;
        public string _altitude; // optional

        public bool IsEmpty
        {
            get => string.IsNullOrWhiteSpace(_latitude)
                && string.IsNullOrWhiteSpace(_longitude);
        }

        private Position()
        {
            _latitude = string.Empty;
            _longitude = string.Empty;
            _altitude = string.Empty;
        }

        public override string GetLatitude()
        {
            return _latitude;
        }

        public override string GetLongitude()
        {
            return _longitude;
        }

        public override string GetAltitude()
        {
            return _altitude;
        }

        public static bool IsValidLatitude(string latitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
            {
                return false;
            }

            return
            (
                -90.0 <= double.Parse(latitude)
                && double.Parse(latitude) <= 90.0
            );
        }

        public static bool IsValidLongitude(string longitude)
        {
            if (string.IsNullOrWhiteSpace(longitude))
            {
                return false;
            }

            return
            (
                -180.0 <= double.Parse(longitude)
                && double.Parse(longitude) <= 180.0
            );
        }

        public static PositionBase Create(string? lattitude, string? longitude)
        {
            if (string.IsNullOrWhiteSpace(lattitude) || string.IsNullOrWhiteSpace(longitude))
            {
                return new NullPosition();
            }

            string tempLatitude = LimitToFourDecimalPlaces(lattitude);
            string tempLongitude = LimitToFourDecimalPlaces(longitude);

            if (IsValidLatitude(tempLatitude) && IsValidLongitude(tempLongitude))
            {
                return new Position
                {
                    _latitude = tempLatitude,
                    _longitude = tempLongitude
                };
            }
            else
            {
                return new NullPosition();
            }
        }

        public static string LimitToFourDecimalPlaces(string coordinate)
        {
            string coord = coordinate.Trim();
            string[] coordinateParts = coord.Split('.');
            if (coordinateParts[1].Length > 4)
            {
                return $"{coordinateParts[0]}.{coordinateParts[1].Substring(0, 4)}";
            }
            else
            {
                return coord;
            }
        }

        public override string ToString()
        {
            return $"{_latitude},{_longitude}";
        }
    }
}
