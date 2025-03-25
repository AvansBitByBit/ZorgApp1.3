using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AfspraakApiClient : MonoBehaviour
{
    public WebClient webClient;

    public async Task<IWebRequestReponse> CreateAfspraak(Afspraak afspraak)
    {
        string route = "/Afspraak";
        string data = JsonUtility.ToJson(afspraak);

        IWebRequestReponse response = await webClient.SendPostRequest(route, data);

        if (response is WebRequestError error)
        {
            Debug.LogError($"Failed to create Afspraak: ");
        }

        return response;
    }

    public async Task<IWebRequestReponse> FetchAfspraken()
    {
        string route = "/Afspraak";
        return await webClient.SendGetRequest(route);
    }

    public async Task<IWebRequestReponse> DeleteAfspraak(int afspraakId)
    {
        string route = $"/Afspraak/{afspraakId}";
        return await webClient.SendDeleteRequest(route);
    }
}