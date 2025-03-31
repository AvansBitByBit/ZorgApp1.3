using System.Collections.Generic;
using System;
using UnityEngine;


public static class JsonHelper
{
    public static List<T> ParseJsonArray<T>(string jsonArray)
    {
        if (!jsonArray.Trim().StartsWith("["))
        {
            Debug.LogError("❌ JSON array expected but got something else!");
            return new List<T>();
        }

        string wrappedJson = "{ \"list\": " + jsonArray + " }";
        JsonList<T> parsedList = JsonUtility.FromJson<JsonList<T>>(wrappedJson);
        return parsedList.list;
    }

    public static string ExtractToken(string data)
    {
        Token token = JsonUtility.FromJson<Token>(data);
        return token.accessToken;
    }

    // public static List<T> ParseJsonArrays<T>(string jsonArray)
    // {
    //     return JsonConvert.DeserializeObject<List<T>>(jsonArray);
    // }
}

[Serializable]
public class JsonList<T>
{
    public List<T> list;
}