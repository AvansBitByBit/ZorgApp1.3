using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AfspraakSceneManager : MonoBehaviour
{
    public AfspraakApiClient afspraakApiClient;
    public TMP_Text feedbackText;
    public Button[] afspraakButtons;
    public Button deleteButton, refreshButton, deleteAllButton;
    private string selectedAfspraakId;
    private string selectedAfspraakTitle;
    public GameObject notitiepaneel, afspraakpaneel;
    public Button notitieknop;

    void Start()
    {
        refreshButton.onClick.AddListener(LoadAfspraken);
        deleteButton.onClick.AddListener(DeleteSelectedAfspraak);
        deleteAllButton.onClick.AddListener(DeleteAllAfspraken);
        notitieknop.onClick.AddListener(HideAfsrpaakpaneel);

        LoadAfspraken();
    }

    public void HideAfsrpaakpaneel()
    {
        afspraakpaneel.SetActive(false);
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
            Debug.Log("Raw API response: " + dataResponse.Data); // dataResponse.Data is gewoon met alle afspramen

            try
            {
                List<Afspraak> afspraken = JsonHelper.ParseJsonArray<Afspraak>(dataResponse.Data); // hier moet tie parsen doet tie niet
                Debug.Log($"Successfully parsed {afspraken.Count} appointments"); // hij ziet de count, door de id maar derest is null.

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

        if (afspraken.Count > 0)
        {
            for (int i = 0; i < Mathf.Min(afspraken.Count, afspraakButtons.Length); i++)
            {
                Afspraak afspraak = afspraken[i];
                string afspraakId = afspraak.ID;
                string afspraakTitle = afspraak.Titel;
                string naamDokter = afspraak.NaamDokter;
                string datumTijd = afspraak.DatumTijd;

                Debug.Log($"Afspraak {i}: Titel={afspraakTitle}, NaamDokter={naamDokter}, DatumTijd={datumTijd}");

                TMP_Text buttonText = afspraakButtons[i].GetComponentInChildren<TMP_Text>();
                if (buttonText != null)
                {
                    buttonText.text = afspraakTitle;
                    Debug.Log($"Button {i} text set to: {buttonText.text}");
                }

                afspraakButtons[i].onClick.RemoveAllListeners();
                afspraakButtons[i].onClick.AddListener(() => SelectAfspraak(afspraakId, afspraakTitle, naamDokter, datumTijd));
                afspraakButtons[i].interactable = true;

                Debug.Log($"Set button {i} with appointment ID {afspraakId}: {afspraakTitle}");
            }
        }
    }

    private void SelectAfspraak(string afspraakId, string afspraakTitle, string naamDokter, string datumTijd)
    {
        selectedAfspraakId = afspraakId;
        selectedAfspraakTitle = afspraakTitle;
        feedbackText.text = $"✅ Geselecteerde afspraak:\nTitel: {afspraakTitle}\nNaam Dokter: {naamDokter}\nDatum en Tijd: {datumTijd}";
    }

    public async void DeleteSelectedAfspraak()
    {
        IWebRequestReponse response = await afspraakApiClient.DeleteAfspraak(selectedAfspraakId);

        if (response is WebRequestError errorResponse)
        {
            feedbackText.text = "❌ Kon afspraak niet verwijderen!";
        }
        else
        {
            feedbackText.text = "✅ Afspraak verwijderd!";
            selectedAfspraakId = null;
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

            List<Afspraak> afspraken = JsonHelper.ParseJsonArray<Afspraak>(dataResponse.Data);
            Debug.Log($"Successfully parsed {afspraken.Count} appointments");

            foreach (var afspraak in afspraken)
            {
                await afspraakApiClient.DeleteAfspraak(afspraak.ID);
            }

            feedbackText.text = "✅ All appointments deleted!";
            LoadAfspraken();
        }
    }
}

