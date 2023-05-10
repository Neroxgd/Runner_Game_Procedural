using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LeaderBoard))]
public class LeaderBoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LeaderBoard leaderBoardScript = (LeaderBoard)target;
        
        if (GUILayout.Button("Reset LeaderBoard"))
            PlayerPrefs.DeleteAll();
        
        base.OnInspectorGUI();
    }
}
