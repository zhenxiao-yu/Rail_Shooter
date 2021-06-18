using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AudioGetter))]
public class AudioGetterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position = EditorGUI.PrefixLabel(position, label);
        property.FindPropertyRelative("id").intValue = EditorGUI.Popup(position, property.FindPropertyRelative("id").intValue, AudioLibrary.audioNamesList.ToArray());
        property.FindPropertyRelative("audioName").stringValue = AudioLibrary.audioNamesList[property.FindPropertyRelative("id").intValue];
    }
}
