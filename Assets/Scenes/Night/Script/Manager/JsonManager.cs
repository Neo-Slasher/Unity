using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class JsonManager
{
    public static T LoadJsonData<T>(string name)
    {
        T gameData;

        string path = Application.dataPath + "/Scenes/Night/";
        string directory = "JsonData/";
        string appender1 = name;
        string dotJson = ".txt";

        StringBuilder builder = new StringBuilder(path);
        builder.Append(directory);
        builder.Append(appender1);
        builder.Append(dotJson);

        string jsonString = File.ReadAllText(builder.ToString());

        gameData = JsonUtility.FromJson<T>(jsonString.ToString());

        return gameData;
    }
}