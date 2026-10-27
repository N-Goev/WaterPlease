using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    [Header("NPC data")]
    [SerializeField] public List<NPCScriptableObject> NPCList;

    [Header("Weather data")]
    [SerializeField] public Weekday DayOfTheWeek;

    [SerializeField] public List<DailyWeatherInfo> CenterNedscheeWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> NorthNedscheeWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> SouthNedscheeWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> EastNedscheeWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> WestNedscheeWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> CenterLogehenWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> SouthLogehenWeeklyWeather;
    [SerializeField] public List<DailyWeatherInfo> EastLogehenWeeklyWeather;
}
