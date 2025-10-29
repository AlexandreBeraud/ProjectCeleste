#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameController controller = (GameController)target;
        if (GUILayout.Button("Update Keycodes"))
        {
            controller.UpdateKeycodes();
        }
    }
}
#endif