using System;
using System.Threading.Tasks;
using UnityEngine;

public class DagboekApiClient : MonoBehaviour
{
    public WebClient webClient;

    public async Task<IWebRequestReponse> PushDagboek(Dagboek dagboek)
    {
        string route = "/Dagboek";
        string data = JsonUtility.ToJson(dagboek);

        IWebRequestReponse response = await webClient.SendPostRequest(route, data);

        if (response is WebRequestError error)
        {
            Debug.LogError($"Failed to push Dagboek entry:");
        }

        return response;
    }

    public async Task<IWebRequestReponse> FetchAllDagboeken()
    {
        string route = "/Dagboek";
        return await webClient.SendGetRequest(route);
    }

    public async Task<IWebRequestReponse> UpdateDagboek(Dagboek dagboek)
    {
        string route = $"/Dagboek/{dagboek.ID}";
        string data = JsonUtility.ToJson(dagboek);

        return await webClient.SendPutRequest(route, data);
    }

    public async Task<IWebRequestReponse> DeleteDagboek(int dagboekId)
    {
        string route = $"/Dagboek/{dagboekId}";
        return await webClient.SendDeleteRequest(route);
    }
}