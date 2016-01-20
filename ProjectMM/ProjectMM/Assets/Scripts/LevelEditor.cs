using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    private GUILayoutOption Height40;

    private bool _ShowPrefabUntiSize = true;
    private bool _ShowPrefabUntiCount = true;
    private bool _ShowSaveLoad = true;

    private static GUIStyle _FoldoutStyle;

    void OnEnable()
    {
        Height40 = GUILayout.Height(40f);
        _FoldoutStyle = new GUIStyle(EditorStyles.foldout);
        _FoldoutStyle.normal.textColor = Color.gray;
        _FoldoutStyle.fontStyle = FontStyle.Bold;
    }

    public static bool Foldout(bool foldout, GUIContent content, bool toggleOnLabelClick, GUIStyle style)
    {
        Rect position = GUILayoutUtility.GetRect(content, style);
        // EditorGUI.kNumberW == 40f but is internal
        return EditorGUI.Foldout(position, foldout, content, toggleOnLabelClick, style);
    }

    public static bool Foldout(bool foldout, string content, bool toggleOnLabelClick, GUIStyle style)
    {
        return Foldout(foldout, new GUIContent(content), toggleOnLabelClick, style);
    }

    public override void OnInspectorGUI()
    {
        Level _Script = (Level)target;

        base.OnInspectorGUI();


       // if (Event.current.type == EventType.Layout || Event.current.type == EventType.Repaint)
       // {
            if (_Script.Prefabs != null && _Script.Prefabs.Length > 0 && _Script.Prefabs[0] != null)
            {
                //_ShowPrefabUntiSize = EditorGUI.Foldout(_ShowPrefabUntiSize, new GUIContent("Prefab Unit Size"), true, true, _FoldoutStyle);
                _ShowPrefabUntiSize = Foldout(_ShowPrefabUntiSize, "Prefab Unit Size", true, _FoldoutStyle);
                if (_ShowPrefabUntiSize)
                {
                    _Script.PrefabSizeX = Convert.ToSingle(EditorGUILayout.Slider("Prefab Size X", _Script.PrefabSizeX, 1f, 100f));
                    _Script.PrefabSizeY = Convert.ToSingle(EditorGUILayout.Slider("Prefab Size Y", _Script.PrefabSizeY, 1f, 100f));
                }
                     
                _ShowPrefabUntiCount = Foldout(_ShowPrefabUntiCount, "Prefab Unit Count", true, _FoldoutStyle);
                if (_ShowPrefabUntiCount)
                {
                    _Script.PrefabCountX = Convert.ToInt32(EditorGUILayout.Slider("Prefab Count X", _Script.PrefabCountX, 1, 50));
                    _Script.PrefabCountY = Convert.ToInt32(EditorGUILayout.Slider("Prefab Count Y", _Script.PrefabCountY, 1, 50));
                }

                GUILayout.Space(20f);
                //float w70 = EditorGUIUtility.currentViewWidth * 0.7f;
                float w50 = EditorGUIUtility.currentViewWidth * 0.46f;

               


                if (GUILayout.Button("Update Level", new GUILayoutOption[] { Height40 })) _Script.UpdateLevel();

                GUILayout.Space(20f);
                
                
                _ShowSaveLoad = Foldout(_ShowSaveLoad, "Save and Load Level", true, _FoldoutStyle);
                EditorGUILayout.BeginHorizontal();
                if (_ShowSaveLoad)
                {
                    if (GUILayout.Button("Save Level", new GUILayoutOption[] { Height40, GUILayout.Width(w50) })) _Script.UpdateLevel();
                    if (GUILayout.Button("Load Level", new GUILayoutOption[] { Height40, GUILayout.Width(w50) })) _Script.UpdateLevel();
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Space(20f);
                EditorGUILayout.LabelField("No Prefab selected!", Height40);
            }
        //}
    }
}
