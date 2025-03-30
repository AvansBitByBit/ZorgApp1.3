using System.Collections.Generic;
using System;
using UnityEngine;


public static class JsonHelper
{
    public static List<T> ParseJsonArray<T>(string jsonArray)
    {
        string extendedJson = "{\"list\":" + jsonArray + "}";
        JsonList<T> parsedList = JsonUtility.FromJson<JsonList<T>>(extendedJson);
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