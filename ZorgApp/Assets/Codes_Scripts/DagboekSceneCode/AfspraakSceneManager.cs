using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AfspraakSceneManager : MonoBehaviour
{
    public AfspraakApiClient afspraakApiClient;
    public TMP_Text feedbackText;
    public Button[] afspraakButtons;
    public Button deleteButton, refreshButton, deleteAllButton; // Add deleteAllButton
    private int selectedAfspraakId;
    private string selectedAfspraakTitle;
    public Button Afspraak1;
    public Button Afspraak2;
    public Button Afspraak3;
    public Button Afspraak4;
    public Button Afspraak5;
    public Button Afspraak6;

    void Start()
    {
        refreshButton.onClick.AddListener(LoadAfspraken);
        deleteButton.onClick.AddListener(DeleteSelectedAfspraak);
        deleteAllButton.onClick.AddListener(DeleteAllAfspraken); // Add listener for deleteAllButton

        LoadAfspraken();
    }

    public async void LoadAfspraken()
    {
        feedbackText.text = "Loading appointments...";
        Debug.Log("Starting appointment fetch...");

        IWebRequestReponse response = await afspraakApiClient.FetchAfspraken();

        if (response is WebRequestError error)
        {
            feedbackText.text = "❌ Error: " + error.ErrorMessage;
            Debug.LogError("Failed to fetch appointments: " + error.ErrorMessage);
            return;
        }

        if (response is WebRequestData<string> dataResponse)
        {
            Debug.Log("Raw API response: " + dataResponse.Data);

            try
            {
                List<Afspraak> afspraken = JsonHelper.ParseJsonArray<Afspraak>(dataResponse.Data);
                Debug.Log($"Successfully parsed {afspraken.Count} appointments");

                if (afspraken.Count == 0)
                {
                    feedbackText.text = "No appointments found";
                    ClearAppointmentButtons();
                }
                else
                {
                    feedbackText.text = $"Found {afspraken.Count} appointments";
                    DisplayAfspraken(afspraken);
                }
            }
            catch (System.Exception ex)
            {
                feedbackText.text = "❌ JSON parsing error!";
                Debug.LogError($"JSON parsing error: {ex.Message}");
                Debug.LogError($"JSON data: {dataResponse.Data}");
            }
        }
    }

    private void ClearAppointmentButtons()
    {
        for (int i = 0; i < afspraakButtons.Length; i++)
        {
            TMP_Text buttonText = afspraakButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = "Leeg Afspraak";
            afspraakButtons[i].onClick.RemoveAllListeners();
            afspraakButtons[i].interactable = false;
        }
    }

 private void DisplayAfspraken(List<Afspraak> afspraken)
{
    Debug.Log($"Displaying {afspraken.Count} appointments on {afspraakButtons.Length} buttons");

    // Clear all buttons first
    for (int i = 0; i < afspraakButtons.Length; i++)
    {
        TMP_Text buttonText = afspraakButtons[i].GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = "Leeg Afspraak";
        }
        else
        {
            Debug.LogError($"Button {i} does not have a TMP_Text component.");
        }
        afspraakButtons[i].onClick.RemoveAllListeners();
        afspraakButtons[i].interactable = false;
    }

    // Display the appointments if available
    if (afspraken.Count > 0)
    {
        for (int i = 0; i < Mathf.Min(afspraken.Count, afspraakButtons.Length); i++)
        {
            Afspraak afspraak = afspraken[i];
            int afspraakId = afspraak.ID;
            string afspraakTitle = afspraak.Titel;
            string naamDokter = afspraak.NaamDokter;
            string datumTijd = afspraak.DatumTijd;

            Debug.Log($"Afspraak {i}: Titel={afspraakTitle}, NaamDokter={naamDokter}, DatumTijd={datumTijd}");

            TMP_Text buttonText = afspraakButtons[i].GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                // Set button text with the title
                buttonText.text = afspraakTitle;
                Debug.Log($"Button {i} text set to: {buttonText.text}");
            }
            else
            {
                Debug.LogError($"Button {i} does not have a TMP_Text component.");
            }

            // Clear previous listeners to avoid duplicates
            afspraakButtons[i].onClick.RemoveAllListeners();

            // Add new listener with captured values
            afspraakButtons[i].onClick.AddListener(() => SelectAfspraak(afspraakId, afspraakTitle, naamDokter, datumTijd));
            afspraakButtons[i].interactable = true;

            Debug.Log($"Set button {i} with appointment ID {afspraakId}: {afspraakTitle}");
        }
    }
}

private void SelectAfspraak(int afspraakId, string afspraakTitle, string naamDokter, string datumTijd)
{
    selectedAfspraakId = afspraakId;
    selectedAfspraakTitle = afspraakTitle;
    feedbackText.text = $"✅ Geselecteerde afspraak:\nTitel: {afspraakTitle}\nNaam Dokter: {naamDokter}\nDatum en Tijd: {datumTijd}";
}


    public async void DeleteSelectedAfspraak()
    {
        if (selectedAfspraakId == 0)
        {
            feedbackText.text = "❌ Geen afspraak geselecteerd om te verwijderen!";
            return;
        }

        IWebRequestReponse response = await afspraakApiClient.DeleteAfspraak(selectedAfspraakId);

        if (response is WebRequestError errorResponse)
        {
            feedbackText.text = "❌ Kon afspraak niet verwijderen!";
        }
        else
        {
            feedbackText.text = "✅ Afspraak verwijderd!";
            selectedAfspraakId = 0;
            selectedAfspraakTitle = null;
            LoadAfspraken();
        }
    }

    public async void DeleteAllAfspraken()
    {
        feedbackText.text = "Deleting all appointments...";
        Debug.Log("Starting delete all appointments...");

        IWebRequestReponse response = await afspraakApiClient.FetchAfspraken();

        if (response is WebRequestError error)
        {
            feedbackText.text = "❌ Error: " + error.ErrorMessage;
            Debug.LogError("Failed to fetch appointments: " + error.ErrorMessage);
            return;
        }

        if (response is WebRequestData<string> dataResponse)
        {
            Debug.Log("Raw API response: " + dataResponse.Data);

            try
            {
                List<Afspraak> afspraken = JsonHelper.ParseJsonArray<Afspraak>(dataResponse.Data);
                Debug.Log($"Successfully parsed {afspraken.Count} appointments");

                foreach (var afspraak in afspraken)
                {
                    await afspraakApiClient.DeleteAfspraak(afspraak.ID);
                }

                feedbackText.text = "✅ All appointments deleted!";
                LoadAfspraken();
            }
            catch (System.Exception ex)
            {
                feedbackText.text = "❌ JSON parsing error!";
                Debug.LogError($"JSON parsing error: {ex.Message}");
                Debug.LogError($"JSON data: {dataResponse.Data}");
            }
        }
    }
}