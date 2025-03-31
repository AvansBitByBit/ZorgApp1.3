using ApiClient.ModelApiClients;
using UnityEngine;
using System.Threading.Tasks;

public class OphalenTraject : MonoBehaviour
{
    public PatientApiClient patientApiClient;

    public void Start()
    {
        FetchPatientData();
    }

    public async void FetchPatientData()
    {
        Debug.Log("Fetching patient data...");
        IWebRequestReponse response = await patientApiClient.GetPatientsAsync();

        if (response is WebRequestError error)
        {
            Debug.LogError("Error fetching patient data: " + error.ErrorMessage);
            return;
        }

        if (response is WebRequestData<string> dataResponse)
        {
            Debug.Log("Patient data: " + dataResponse.Data);
            
        }
    }
}