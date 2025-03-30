using System.Threading.Tasks;
using ApiClient.Models;
using UnityEngine;

namespace ApiClient.ModelApiClients
{
    public class PatientApiClient : MonoBehaviour
    {
        public WebClient webClient;

        public async Task<IWebRequestReponse> GetPatientsAsync()
        {
            string route = "/Patient";
            return await webClient.SendGetRequest(route);
        }

        public async Task<IWebRequestReponse> CreatePatientAsync(Patient patient)
        {
            string route = "/Patient";
            string data = JsonUtility.ToJson(patient);
            Debug.Log("Sending POST request to " + route + " with data: " + data);

            IWebRequestReponse response = await webClient.SendPostRequest(route, data);

            if (response is WebRequestError error)
            {
                Debug.LogError($"Failed to create Patient: {error.ErrorMessage}");
            }

            return response;
        }

        public async Task<IWebRequestReponse> UpdatePatientAsync(int id, Patient patient)
        {
            string route = $"/Patient/{id}";
            string data = JsonUtility.ToJson(patient);
            return await webClient.SendPutRequest(route, data);
        }

        public async Task<IWebRequestReponse> DeletePatientAsync(int id)
        {
            string route = $"/Patient/{id}";
            return await webClient.SendDeleteRequest(route);
        }

        public void SetAuthorizationHeader(string token)
        {
            webClient.SetToken(token);
        }
    }
}