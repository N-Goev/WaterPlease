using System;

[Serializable]
public struct DailyWeatherInfo
{
    public WeatherType weatherType;
    public int maxTemperature;
    public int minTemperature;
    public int percent;
}