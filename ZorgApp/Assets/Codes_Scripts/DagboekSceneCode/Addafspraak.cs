using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddAfspraak : MonoBehaviour
{
    public GameObject CreateAfspraak;
    public TMP_InputField Titel;
    public TMP_InputField NaamDokter;
    public TMP_InputField DatumTijd;
    public WebClient webClient;
    public AfspraakApiClient afspraakApiClient;
    public Button saveButton;

    void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    public async void OnSaveButtonClicked()
    {
        string titel = Titel.text;
        string naamDokter = NaamDokter.text;
        string datumTijd = DatumTijd.text;

        // Convert DatumTijd to ISO 8601 format
        DateTime parsedDate;
        if (!DateTime.TryParse(datumTijd, out parsedDate))
        {
            Debug.LogError("Invalid date format. Please use a valid date format.");
            return;
        }
        string isoDatumTijd = parsedDate.ToString("yyyy-MM-ddTHH:mm:ss");

        Afspraak afspraak = new Afspraak
        {
            Id = Guid.NewGuid().ToString(),
            Titel = titel,
            NaamDokter = naamDokter,
            DatumTijd = isoDatumTijd,
            Actief = 0
        };

        string jsonData = JsonUtility.ToJson(afspraak);
        Debug.Log("Sending JSON data: " + jsonData);

        IWebRequestReponse response = await afspraakApiClient.CreateAfspraak(afspraak);

        if (response is WebRequestData<string> data)
        {
            Debug.Log("Afspraak created successfully: " + data.Data);
        }
        else
        {
            Debug.LogError("Failed to create Afspraak: " + response);
        }
    }

    public void CreateAfspraakButton()
    {
        CreateAfspraak.SetActive(true);
    }
}