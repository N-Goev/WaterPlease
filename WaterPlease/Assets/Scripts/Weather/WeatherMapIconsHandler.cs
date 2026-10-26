using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeatherMapIconsHandler : MonoBehaviour
{
    [SerializeField] private Sprite sunnyIcon;
    [SerializeField] private Sprite cloudyIcon;
    [SerializeField] private Sprite rainyIcon;

    private Image[] weatherIcons;

    private void Awake()
    {
        weatherIcons = GetComponentsInChildren<Image>();
    }

    private void OnEnable()
    {
        EventBus.Add<LoadLevelInfoEvent>(UpdateWeatherIcons);
    }

    private void OnDisable()
    {
        EventBus.Remove<LoadLevelInfoEvent>(UpdateWeatherIcons);
    }

    private void UpdateWeatherIcons(LoadLevelInfoEvent levelInfoEvent)
    {
        if (weatherIcons.Length != 8)
        {
            return;
        }

        LevelScriptableObject levelInfo = levelInfoEvent.LevelScriptableObject;

        //List of the weekly weather for each region
        List<List<WeatherType>> weatherTypes = new();
        weatherTypes.Add(levelInfo.CenterNedscheeWeeklyWeather);
        weatherTypes.Add(levelInfo.NorthNedscheeWeeklyWeather);
        weatherTypes.Add(levelInfo.SouthNedscheeWeeklyWeather);
        weatherTypes.Add(levelInfo.EastNedscheeWeeklyWeather);
        weatherTypes.Add(levelInfo.WestNedscheeWeeklyWeather);
        weatherTypes.Add(levelInfo.CenterLogehenWeeklyWeather);
        weatherTypes.Add(levelInfo.SouthLogehenWeeklyWeather);
        weatherTypes.Add(levelInfo.EastLogehenWeeklyWeather);

        //Set the weather icon for each region depending on the day of the week
        for (int i = 0; i < 8; i++)
        {
            if (weatherTypes[i].Count != 7) {
                Debug.LogError("Weekly weather set incorrectly for Scene " + SceneManager.GetActiveScene().buildIndex);
                break;
            }

            WeatherType currentWeatherType = weatherTypes[i][(int)levelInfo.DayOfTheWeek];

            UpdateWeatherImage(weatherIcons[i], currentWeatherType);
        }
    }

    public void UpdateWeatherImage(Image weatherImage, WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.Sunny:
                weatherImage.sprite = sunnyIcon;
                break;
            case WeatherType.Cloudy:
                weatherImage.sprite = cloudyIcon;
                break;
            case WeatherType.Rainy:
                weatherImage.sprite = rainyIcon;
                break;
            default:
                break;
        }
    }

    public void UpdateWeatherText(TextMeshProUGUI weatherText, WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.Sunny:
                weatherText.text = "Sunny";
                break;
            case WeatherType.Cloudy:
                weatherText.text = "Cloudy";
                break;
            case WeatherType.Rainy:
                weatherText.text = "Rainy";
                break;
            default:
                break;
        }
    }
}
