using System.Collections.Generic;
using System.Linq;
using ApiClient.ModelApiClients;
using UnityEngine;
using ApiClient.Models;
using TMPro;


public class GetPatientName : MonoBehaviour
{
    public PatientApiClient patientApiClient;
    private List<Patient> kinderen = new();
    public TextMeshProUGUI patientNameText;

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
            kinderen = JsonHelper.ParseJsonArray<Patient>(dataResponse.Data);


            foreach (Patient kind in kinderen)
            {
                Debug.Log("Patient: " + kind.voornaam + " " + kind.achternaam);
            }

            FetchAndDisplayPatientName();
        }
    }

    public string FetchAndDisplayPatientName()
    {
        var lastPatient = kinderen.LastOrDefault();
        if (lastPatient != null)
        {
            string patientName = lastPatient.voornaam + " " + lastPatient.achternaam;
            Debug.Log("Patient Name: " + patientName);
            patientNameText.text = patientName;
            return patientName;
        }
        else
        {
            Debug.Log("No patients found.");
            patientNameText.text = "Welkom, Log in / maak een kind aan in de Instellingen ";
            return "No patients found.";
        }
    }
}
