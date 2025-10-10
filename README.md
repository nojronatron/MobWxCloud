# MobWxCloud

This project aims to provide back-end to MobWxApp, a mobile application that provides weather information to users.

## Overview

This solution provides a REST API to MobWxApp, abstracting-away the complexities of fetching data from the NWS API.

Components:

- MobWx.API: REST API endpoints for MobWxApp to fetch data from.
- MobWx.Lib: Business logic to process NWS API responses as requested by MobWx.API.
- MobWx.Web: (Future) Web interface to manage MobWx.API.
- Tests, AppHost, and ServiceDefaults: Support dev and test time activities.

## Status

This project is under development, with a goal of deploying to Azure by June 2025.

### Features

(incomplete list while in early development)

- Fetch and display weather data to users, leveraging custom API.
- Fetch latitude, longitude from city and state inputs (US only).
- Fetch current weather conditions from NWS API and convert for MobWx client consumption.
- Fetch active alerts from NWS API and convert for MobWx client consumption.
- Fetch forecast data from NWS API and convert for MobWx client consumption.
- Leverage .NET Aspire v9.0 for dev time productivity, debugging, and Service Discovery.

## Releases

- 0.0.1-SNAPSHOT: Initial version

## License

## Attributions

- NOAA NWS [API](https://www.weather.gov/documentation/services-web-api) for weather data.
- [Google Fonts](https://fonts.google.com/) for the Cloud Alert icon.
- OpenStreetMaps [Nominatim API](https://nominatim.org/release-docs/develop/api/Search/) for geocoding.
