using Music;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor (typeof (MusicSheet))]
    public class MusicSheetEditor : UnityEditor.Editor {

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
}