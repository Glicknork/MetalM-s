using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BaseBlock))]
public class BaseBlockEditor : Editor {

    void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            BaseBlock Block = target as BaseBlock;
            if (Block != null)
            {
                //Block.StartCounter();
               // EditorGUILayout.
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
