using System.Threading.Tasks;
using ApiClient.ModelApiClients;
using ApiClient.Models;
using UnityEngine;
using UnityEngine.UI;

public class KindGegevensInvoeren : MonoBehaviour
{
    public Button saveButton;
    public TMPro.TMP_InputField nameInputField;
    public TMPro.TMP_InputField lastnameInputField;
    public Button ArmLinks;
    public Button ArmRechts;
    public Button BeenLinks;
    public Button BeenRechts;
    public Button Ribben;
    private int Traject;
    public PatientApiClient patientApiClient;

    void Start()
    {


        saveButton.onClick.AddListener(OnSaveButtonClicked);
        ArmLinks.onClick.AddListener(OnArmLinksButtonClicked);
        ArmRechts.onClick.AddListener(OnArmRechtsButtonClicked);
        BeenLinks.onClick.AddListener(OnBeenLinksButtonClicked);
        BeenRechts.onClick.AddListener(OnBeenRechtsButtonClicked);
        Ribben.onClick.AddListener(OnRibbenButtonClicked);
    }

    private void OnArmRechtsButtonClicked()
    {
        Traject = 1;
    }

    private void OnBeenLinksButtonClicked()
    {
        Traject = 2;
    }

    private void OnRibbenButtonClicked()
    {
        Traject = 3;
    }

    private void OnBeenRechtsButtonClicked()
    {
        Traject = 2;
    }

    private void OnArmLinksButtonClicked()
    {
        Traject = 1;
    }

    public async void OnSaveButtonClicked()
    {
        string name = nameInputField.text;
        string lastname = lastnameInputField.text;

        Patient patient = new Patient
        {
            voornaam = name,
            achternaam = lastname,
            trajectID = Traject
        };

        string jsonData = JsonUtility.ToJson(patient);
        Debug.Log("Sending JSON data: " + jsonData);

        IWebRequestReponse response = await patientApiClient.CreatePatientAsync(patient);

        if (response is WebRequestData<string> data)
        {
            Debug.Log("Patient created successfully: " + data.Data);
        }
        else
        {
            Debug.LogError("Failed to create Patient: " + response);
        }
    }
}