using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeeklyWeatherHandler : MonoBehaviour
{
    [SerializeField] private WeatherMapIconsHandler weatherMapIconsHandler;
    [SerializeField] private TextMeshProUGUI RegionTitle;
    [SerializeField] private List<Image> DailyWeatherIcons;
    [SerializeField] private List<TextMeshProUGUI> DailyWeatherTypes;
    [SerializeField] private List<TextMeshProUGUI> DailyTemperatures;
    [SerializeField] private List<TextMeshProUGUI> DailyPercentages;
    [SerializeField] private List<GameObject> DailyHighlights;
    
    private CanvasGroup canvasGroup;

    private LevelScriptableObject levelInfo;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        CloseWeeklyWeather();
    }

    private void OnEnable()
    {
        EventBus.Add<LoadLevelInfoEvent>(GetLevelInfo);
        EventBus.Add<RegionButtonClickedEvent>(OnRegionOpened);
    }

    private void OnDisable()
    {
        EventBus.Remove<LoadLevelInfoEvent>(GetLevelInfo);
        EventBus.Remove<RegionButtonClickedEvent>(OnRegionOpened);
    }

    private void OpenWeeklyWeather()
    {
        if (!canvasGroup) { return; }

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void CloseWeeklyWeather()
    {
        if (!canvasGroup) { return; }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void GetLevelInfo(LoadLevelInfoEvent loadLevelInfoEvent)
    {
        levelInfo = loadLevelInfoEvent.LevelScriptableObject;

        DailyHighlights[(int)levelInfo?.DayOfTheWeek].SetActive(true);
    }

    private void OnRegionOpened(RegionButtonClickedEvent regionButtonClickedEvent)
    {
        MapRegion currentRegion = regionButtonClickedEvent.Region;

        switch (currentRegion)
        {
            case MapRegion.CenterNedschee:
                RegionTitle.text = "Center Nedschee";
                UpdateWeeklyWeatherData(levelInfo.CenterNedscheeWeeklyWeather);

                break;
            case MapRegion.NorthNedschee:
                RegionTitle.text = "North Nedschee";
                UpdateWeeklyWeatherData(levelInfo.NorthNedscheeWeeklyWeather);

                break;
            case MapRegion.SouthNedschee:
                RegionTitle.text = "South Nedschee";
                UpdateWeeklyWeatherData(levelInfo.SouthNedscheeWeeklyWeather);

                break;
            case MapRegion.EastNedschee:
                RegionTitle.text = "East Nedschee";
                UpdateWeeklyWeatherData(levelInfo.EastNedscheeWeeklyWeather);

                break;
            case MapRegion.WestNedschee:
                RegionTitle.text = "West Nedschee";
                UpdateWeeklyWeatherData(levelInfo.WestNedscheeWeeklyWeather);

                break;
            case MapRegion.CenterLogehen:
                RegionTitle.text = "Center Logehen";
                UpdateWeeklyWeatherData(levelInfo.CenterLogehenWeeklyWeather);

                break;
            case MapRegion.SouthLogehen:
                RegionTitle.text = "South Logehen";
                UpdateWeeklyWeatherData(levelInfo.SouthLogehenWeeklyWeather);

                break;
            case MapRegion.EastLogehen:
                RegionTitle.text = "East Logehen";
                UpdateWeeklyWeatherData(levelInfo.EastLogehenWeeklyWeather);

                break;
            default:
                break;
        }

        OpenWeeklyWeather();
    }

    private void UpdateWeeklyWeatherData(List<WeatherType> weatherForTheWeek) {
        for (int i = 0; i < 7; i++)
        {
            weatherMapIconsHandler.UpdateWeatherImage(DailyWeatherIcons[i], weatherForTheWeek[i]);
            weatherMapIconsHandler.UpdateWeatherText(DailyWeatherTypes[i], weatherForTheWeek[i]);

            DailyTemperatures[i].text = GetRandomTemperature();
            DailyPercentages[i].text = GetRandomPercent();
        }
    }

    private string GetRandomTemperature()
    {
        int randTempMax = (int)Random.Range(5f, 19f);
        int randTempMin = (int)Random.Range(randTempMax - 8f, randTempMax - 3f);

        return randTempMax.ToString() + "° / " + randTempMin.ToString() + "°";
    }

    private string GetRandomPercent()
    {
        int randPercent = (int)Random.Range(2f, 31f);

        return randPercent.ToString() + "%";
    }
}
