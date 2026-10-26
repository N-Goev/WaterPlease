using System.Collections.Generic;
using UnityEngine;

public enum WeatherType { Sunny, Cloudy, Rainy };

public enum Weekday { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField] public List<NPCScriptableObject> NPCList;

    [SerializeField] public List<WeatherType> CenterNedscheeWeeklyWeather;
    [SerializeField] public List<WeatherType> NorthNedscheeWeeklyWeather;
    [SerializeField] public List<WeatherType> SouthNedscheeWeeklyWeather;
    [SerializeField] public List<WeatherType> EastNedscheeWeeklyWeather;
    [SerializeField] public List<WeatherType> WestNedscheeWeeklyWeather;
    [SerializeField] public List<WeatherType> CenterLogehenWeeklyWeather;
    [SerializeField] public List<WeatherType> SouthLogehenWeeklyWeather;
    [SerializeField] public List<WeatherType> EastLogehenWeeklyWeather;

    [SerializeField] public Weekday DayOfTheWeek;
}
