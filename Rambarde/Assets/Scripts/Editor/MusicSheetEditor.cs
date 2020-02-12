using Partition;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MusicSheet))]
public class MusicSheetEditor : Editor {

    public override void OnInspectorGUI() {
        MusicSheet sheet = (MusicSheet)target;

        if (DrawDefaultInspector ()) {
            if (sheet.autoUpdate) {
                sheet.Generate();
            }
        }

        if (GUILayout.Button ("Apply Changes")) {
            sheet.Generate();
        }
    }
}