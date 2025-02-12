using Microsoft.Extensions.Logging;
using MobWx.API.Common;
using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
using MobWx.Lib.NwsAlertModels;
using MobWx.Lib.PointModels;
using MobWx.Tests.Api.TestFiles;
using Moq;
using System.Text.Json;
using Xunit;

namespace MobWx.Tests.API;

public class JsonHandlerTests
{
    private readonly Mock<ILogger<JsonHandler>> _mockLogger;
    private readonly JsonHandler _jsonHandler;
    private readonly TestFilesManager _testFilesManager;

    public JsonHandlerTests()
    {
        _mockLogger = new Mock<ILogger<JsonHandler>>();
        _jsonHandler = new JsonHandler(_mockLogger.Object);
        _testFilesManager = new TestFilesManager();
    }

    [Fact]
    public void TryDeserializePointsResponse_ValidJson_ShouldReturnPointsResponse()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("portland-points-response.json");

        // Act
        var result = _jsonHandler.TryDeserializePointsResponse(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("https://api.weather.gov/gridpoints/PQR/116,107/forecast", result.Forecast);
        Assert.Equal("https://api.weather.gov/offices/PQR", result.ForecastOfficeUrl);
        Assert.Equal("https://api.weather.gov/gridpoints/PQR/116,107/stations", result.ObservationStations);
        Assert.Equal("PQR", result.GridId);
        Assert.Equal(116, result.GridX);
        Assert.Equal(107, result.GridY);
    }

    [Fact]
    public void TryDeserializePointsResponse_InvalidJson_ShouldReturnNull()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("invalid-points-context-only.json");

        // Act
        var result = _jsonHandler.TryDeserializePointsResponse(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.ObservationStations);
        Assert.Null(result.Forecast);
        Assert.Null(result.GridId);
        Assert.Null(result.GridX);
        Assert.Null(result.GridY);
    }

    [Fact]
    public void TryDeserializeForecastResponse_ValidJson_ShouldReturnForecastResponse()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("portland-forecast-response-two-periods.json");

        // Act
        var result = _jsonHandler.TryDeserializeForecastResponse(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result);
        Assert.Equal(2, result.Periods.Count);
        DateTime? parsedStartTime0 = DateTime.Parse("2025-02-08T09:00:00-08:00");
        Assert.Equal(parsedStartTime0, result.Periods[0].StartTime);
        DateTime? parsedStartTime1 = DateTime.Parse("2025-02-08T18:00:00-08:00");
        Assert.Equal(parsedStartTime1, result.Periods[1].StartTime);
    }

    [Fact]
    public void TryDeserializeForecastResponse_InvalidJson_ShouldReturnNull()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("invalid-forecast-contextonly.json");


        // Act
        var result = _jsonHandler.TryDeserializeForecastResponse(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Periods);
    }

    [Fact]
    public void TryDeserializeObservation_ValidJson_ShouldReturnObservation()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("portland-observation-response.json");

        // Act
        var result = _jsonHandler.TryDeserializeObservation(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("POINT(-122.6 45.6)", result.PointLocation);
        Assert.Equal("https://api.weather.gov/stations/KPDX", result.Station);
        DateTime? parsedDateTime = DateTime.Parse("2025-02-08T15:53:00+00:00");
        Assert.Equal(parsedDateTime, result.Timestamp);
    }

    [Fact]
    public void TryDeserializeObservation_InvalidJson_ShouldReturn()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("invalid-observation-context-only.json");

        // Act
        var result = _jsonHandler.TryDeserializeObservation(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.PointLocation);
        Assert.Empty(result.Station);
        Assert.Null(result.Timestamp);
        Assert.Empty(result.TextDescription);
        Assert.Empty(result.RawMessage);
    }

    [Fact]
    public void GetObservationStationsList_ValidJson_ShouldReturnList()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("portland-observation-stations-limit12.json");

        // Act
        var result = _jsonHandler.GetObservationStationsList(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(12, result.Count());
        Assert.True(result.Any());
        Assert.False(string.IsNullOrWhiteSpace(result.FirstOrDefault((item) => item!.Contains("KPDX"))));
        Assert.Contains(result, (item) => item!.Contains("KTTD"));
    }

    [Fact]
    public void GetObservationStationsList_InvalidJson_ShouldReturnEmptyList()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("invalid-observation-stations.json");

        // Act
        var result = _jsonHandler.GetObservationStationsList(jsonString);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetObservationStationsUrl_ValidJson_ShouldReturnUrl()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("portland-points-response.json");

        // Act
        var result = _jsonHandler.GetObservationStationsUrl(jsonString);

        // Assert
        Assert.Equal("https://api.weather.gov/gridpoints/PQR/116,107/stations", result);
    }

    [Fact]
    public void GetObservationStationsUrl_InvalidJson_ShouldReturnEmptyString()
    {
        // Arrange
        var jsonString = "{\"invalidJson}";

        // Act
        var result = _jsonHandler.GetObservationStationsUrl(jsonString);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void TryDeserializeActiveAlerts_ValidJson_ShouldReturnActiveAlerts()
    {
        // Arrange
        var jsonString = _testFilesManager.LoadFileContent("spokane-active-alerts-response.json");

        // Act
        var result = _jsonHandler.TryDeserializeActiveAlerts(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Current watches, warnings, and advisories for 46.9 N, 116.9 W",
            result.AlertTitle);
    }

    [Fact]
    public void TryDeserializeActiveAlerts_InvalidJson_ShouldReturnNull()
    {
        // Arrange
        var jsonString = "{\"invalidJson}";

        // Act
        var result = _jsonHandler.TryDeserializeActiveAlerts(jsonString);

        // Assert
        Assert.Null(result);
    }
}
