using MobWx.API.Common;
using Xunit;

namespace MobWx.Tests.Api
{
    public class OsmEndpointPathsTests
    {
        [Fact]
        public void GetGeocodeRequestUrl_ShouldReturnExpectedUrl()
        {
            // Arrange
            string city = "New York";
            string state = "NY";
            string expectedUrl = "https://nominatim.openstreetmap.org/search?city=New York&state=NY&country=United States&format=geojson&limit=1";

            // Act
            var result = OsmEndpointPaths.GetGeocodeRequestUrl(city, state);

            // Assert
            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public void GetGeocodeRequestUrl_ShouldTrimCityAndState()
        {
            // Arrange
            string city = " New York ";
            string state = " NY ";
            string expectedUrl = "https://nominatim.openstreetmap.org/search?city=New York&state=NY&country=United States&format=geojson&limit=1";

            // Act
            var result = OsmEndpointPaths.GetGeocodeRequestUrl(city, state);

            // Assert
            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public void GetGeocodeStatusUrl_ShouldReturnExpectedUrl()
        {
            // Arrange
            string expectedUrl = "https://nominatim.openstreetmap.org/status";

            // Act
            var result = OsmEndpointPaths.GetGeocodeStatusUrl();

            // Assert
            Assert.Equal(expectedUrl, result);
        }
    }
}
