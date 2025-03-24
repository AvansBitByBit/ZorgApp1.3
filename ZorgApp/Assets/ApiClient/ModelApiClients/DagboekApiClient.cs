using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DagboekApiClient : MonoBehaviour
{
    public WebClient webClient;

    public async Awaitable<IWebRequestReponse> PushDagboek(Dagboek dagboek)
    {
        string route = "/Dagboek";
        string data = JsonUtility.ToJson(dagboek);

        return await webClient.SendPostRequest(route, data);
    }

    public async Awaitable<IWebRequestReponse> FetchAllDagboeken()
    {
        string route = "/Dagboek";
        return await webClient.SendGetRequest(route);
    }

    public async Awaitable<IWebRequestReponse> UpdateDagboek(Dagboek dagboek)
    {
        string route = $"/Dagboek/{dagboek.ID}";
        string data = JsonUtility.ToJson(dagboek);

        return await webClient.SendPutRequest(route, data);
    }

    public async Awaitable<IWebRequestReponse> DeleteDagboek(int dagboekId)
    {
        string route = $"/Dagboek/{dagboekId}";
        return await webClient.SendDeleteRequest(route);
    }
}