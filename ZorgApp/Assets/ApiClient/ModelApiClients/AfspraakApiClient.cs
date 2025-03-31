using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Handles communication with the Afspraak Web API.
/// </summary>
public class AfspraakApiClient : MonoBehaviour
{
    public WebClient webClient;

    /// <summary>
    /// Fetches the list of afspraken for the authenticated user.
    /// </summary>
    /// <returns>A list of afspraken.</returns>
    public async Task<List<Afspraak>> GetAfspraken()
    {
        string route = "/Afspraak";
        var response = await webClient.SendGetRequest(route);

        if (response is WebRequestData<string> jsonResponse)
        {
            return JsonHelper.ParseJsonArray<Afspraak>(jsonResponse.Data);
        }
        else
        {
            Debug.LogError($"Error fetching afspraken: {((WebRequestError)response).ErrorMessage}");
            return new List<Afspraak>();
        }
    }

    /// <summary>
    /// Creates a new afspraak.
    /// </summary>
    /// <param name="afspraak">The afspraak model.</param>
    /// <returns>True if the afspraak is successfully created, otherwise false.</returns>
    /// <example>
    /// Example JSON body:
    /// {
    ///     "Titel": "Doktersafspraak",
    ///     "NaamDokter": "Dr. Smith",
    ///     "DatumTijd": "2025-01-01T00:00:00",
    ///     "UserId": "user-id",
    ///     "Actief": 1
    /// }
    /// </example>
    public async Task<bool> CreateAfspraak(Afspraak afspraak)
    {
        string route = "/Afspraak";
        string data = JsonUtility.ToJson(afspraak);
        var response = await webClient.SendPostRequest(route, data);

        return response is WebRequestData<string>;
    }

    /// <summary>
    /// Deletes an afspraak by ID.
    /// </summary>
    /// <param name="id">The ID of the afspraak to delete.</param>
    /// <returns>True if the afspraak is successfully deleted, otherwise false.</returns>
    public async Task<bool> DeleteAfspraak(Guid id)
    {
        string route = $"/Afspraak/{id}";
        var response = await webClient.SendDeleteRequest(route);

        return response is WebRequestData<string>;
    }
}