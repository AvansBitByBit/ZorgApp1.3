using System.Collections.Generic;
using System.Linq;
using ApiClient.ModelApiClients;
using UnityEngine;
using ApiClient.Models;
using System.Threading.Tasks;

public class OphalenTraject : MonoBehaviour
{
    public GameObject Armen;
    public GameObject Benen;
    public GameObject Ribben;
    private List<Patient> kinderen = new();
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
            kinderen = JsonHelper.ParseJsonArray<Patient>(dataResponse.Data);


            foreach (Patient kind in kinderen)
            {
                Debug.Log("Patient: " + kind.voornaam + " " + kind.achternaam);
            }

            GetTrajectId();

        }
    }

    public void GetTrajectId()
    {
        var lastPatient = kinderen.LastOrDefault();
        if (lastPatient != null)
        {
            int lastTrajectId = lastPatient.trajectID;
            Debug.Log("Last Traject ID: " + lastTrajectId);
            JuisteBehandelTraject(lastTrajectId);
        }
        else
        {
            Debug.Log("No patients found.");
        }
    }

    private void JuisteBehandelTraject(int lastTrajectId)
    {
        switch (lastTrajectId)
        {
            case 1:
                Debug.Log("Behandeltraject: Arm ");
                Armen.SetActive(true);
                Benen.SetActive(false);
                Ribben.SetActive(false);
                break;
            case 2:
                Debug.Log("Behandeltraject: Been ");
                Benen.SetActive(true);
                Armen.SetActive(false);
                Ribben.SetActive(false);
                break;
            case 3:
                Debug.Log("Behandeltraject: Ribben");
                Ribben.SetActive(true);
                Benen.SetActive(false);
                Armen.SetActive(false);
                break;
            default:
                Debug.Log("Behandeltraject: Default Arm");
                Armen.SetActive(true);
                Benen.SetActive(false);
                Ribben.SetActive(false);
                break;
        }

    }
}