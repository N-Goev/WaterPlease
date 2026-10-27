using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DailyWeatherInfo))]
public class DailyWeatherInfoPropertyDrawer : PropertyDrawer
{
    //public override VisualElement CreatePropertyGUI(SerializedProperty property)
    //{
    //    // Create property container element.
    //    var container = new VisualElement();

    //    // Create property fields.
    //    var weatherTypeField = new PropertyField(property.FindPropertyRelative("weatherType"));
    //    var maxTemperatureField = new PropertyField(property.FindPropertyRelative("maxTemperature"));
    //    var minTemperatureField = new PropertyField(property.FindPropertyRelative("minTemperature"));
    //    var percentField = new PropertyField(property.FindPropertyRelative("percent"));

    //    // Add fields to the container.
    //    container.Add(weatherTypeField);
    //    container.Add(maxTemperatureField);
    //    container.Add(minTemperatureField);
    //    container.Add(percentField);

    //    return container;
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var weatherTypeRect = new Rect(position.x, position.y, 75, position.height);
        var maxTemperatureRect = new Rect(position.x + 85, position.y, 30, position.height);
        var minTemperatureRect = new Rect(position.x + 120, position.y, 30, position.height);
        var percentRect = new Rect(position.x + 160, position.y, 30, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(weatherTypeRect, property.FindPropertyRelative("weatherType"), GUIContent.none);
        EditorGUI.PropertyField(maxTemperatureRect, property.FindPropertyRelative("maxTemperature"), GUIContent.none);
        EditorGUI.PropertyField(minTemperatureRect, property.FindPropertyRelative("minTemperature"), GUIContent.none);
        EditorGUI.PropertyField(percentRect, property.FindPropertyRelative("percent"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
