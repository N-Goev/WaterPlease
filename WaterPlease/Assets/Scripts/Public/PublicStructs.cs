using System;
using UnityEngine;

[Serializable]
public struct DailyWeatherInfo
{
    public WeatherType weatherType;
    public int maxTemperature;
    public int minTemperature;
    public int percent;
}

[Serializable]
public struct NewsComment
{
    public Sprite Icon;
    public string Username;
    public string Comment;
}