using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ZzzLog : MonoBehaviour
{
    uint qsize = 8;  // number of messages to keep
    Queue<string> myLogQueue = new Queue<string>();
    public Color logColor = Color.white;
    public int logFontSize = 14;

    void Start()
    {
        Debug.Log("Started up logging.");
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string formattedLog = "[" + type + "] : " + logString;
        myLogQueue.Enqueue(formattedLog);
        if (type == LogType.Exception)
            myLogQueue.Enqueue(stackTrace);
        while (myLogQueue.Count > qsize)
            myLogQueue.Dequeue();
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = logColor;
        style.fontSize = logFontSize;

        GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()), style);
        GUILayout.EndArea();
    }
}
