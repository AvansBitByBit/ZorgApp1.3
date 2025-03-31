using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AfspraakManager : MonoBehaviour
{
    private int geselecteerdeIndex = -1; // nieuw
    [Header("Buttons")]
    public List<Button> afspraakButtons;
    public Button refreshButton;
    public Button deleteButton;
    public Button deleteAllButton;
    public Button saveButton;
    

    [Header("Input Fields")]
    public TMP_InputField titleInput;
    public TMP_InputField doctorInput;
    public TMP_InputField dateInput;

    [Header("WebClient Reference")]
    public WebClient webClient;

    private List<Afspraak> huidigeAfspraken = new();
    private Afspraak geselecteerdeAfspraak;

    private void Start()
    {
        refreshButton.onClick.AddListener(LaadAfspraken);
        saveButton.onClick.AddListener(PostAfspraak);
        deleteButton.onClick.AddListener(DeleteSelectedAfspraak);
        deleteAllButton.onClick.AddListener(DeleteAlleAfspraken);

        LaadAfspraken();
    }

    private async void LaadAfspraken()
    {
        geselecteerdeAfspraak = null;

        var response = await webClient.SendGetRequest("/Afspraak");

        if (response == null)
        {
            Debug.LogError("❌ response is null");
            return;
        }

        if (response is WebRequestData<string> data)
        {
            Debug.Log("📥 Ontvangen JSON:\n" + data.Data);
            huidigeAfspraken = JsonHelper.ParseJsonArray<Afspraak>(data.Data);
            ToonAfspraken();
        }
        else if (response is WebRequestError error)
        {
            Debug.LogError("❌ WebRequestError: " + error.ErrorMessage);
        }
        else
        {
            Debug.LogWarning("⚠️ Onbekend response type: " + response.GetType());
        }
    }


    private void ToonAfspraken()
    {
        Debug.Log("📣 ToonAfspraken() aangeroepen met " + huidigeAfspraken.Count + " afspraken.");

        for (int i = 0; i < afspraakButtons.Count; i++)
        {
            Button button = afspraakButtons[i];
            button.onClick.RemoveAllListeners(); // reset listeners altijd

            if (i < huidigeAfspraken.Count)
            {
                Afspraak afspraak = huidigeAfspraken[i];
                var id = afspraak.ID;

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = afspraak.Titel;
                }

                button.interactable = true;
                button.name = id;

                button.onClick.AddListener(() => SelectAfspraakById(id));
                Debug.Log($"📌 Knop {i} toegewezen aan afspraak: {afspraak.Titel} (ID: {id})");
            }
            else
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = "Leeg Afspraak";
                }

                button.interactable = false;
                button.name = "leeg";
            }
        }
    }
    private void SelectAfspraakById(string id)
    {
        geselecteerdeAfspraak = huidigeAfspraken.Find(a => a.ID == id);

        if (geselecteerdeAfspraak != null)
        {
            titleInput.text = geselecteerdeAfspraak.Titel;
            doctorInput.text = geselecteerdeAfspraak.NaamDokter;
            dateInput.text = geselecteerdeAfspraak.DatumTijd;
            Debug.Log($"✅ Geselecteerde afspraak: {geselecteerdeAfspraak.Titel} (ID: {geselecteerdeAfspraak.ID})");
        }
        else
        {
            Debug.LogWarning("⚠️ Geen afspraak gevonden met ID: " + id);
        }
    }


    private void SelecteerAfspraak(int index)
    {
        if (index < 0 || index >= huidigeAfspraken.Count)
        {
            Debug.LogWarning("⚠️ Ongeldige afspraak index.");
            return;
        }

        geselecteerdeIndex = index;
        Afspraak afspraak = huidigeAfspraken[index];

        titleInput.text = afspraak.Titel;
        doctorInput.text = afspraak.NaamDokter;
        dateInput.text = afspraak.DatumTijd;

        Debug.Log($"✅ Geselecteerde afspraak: {afspraak.Titel} (ID: {afspraak.ID})");
    }


    /// <summary>
    /// Creates a new afspraak by posting to the WebAPI.
    /// <para>Correct JSON body (do NOT include ID field):</para>
    /// <code>
    /// {
    ///   "Titel": "Doktersafspraak1",
    ///   "NaamDokter": "Dr. Smith",
    ///   "DatumTijd": "2025-01-01T00:00:00",
    ///   "UserId": "user-id",
    ///   "Actief": 1
    /// }
    /// </code>
    /// <para><b>⚠️ Do NOT include "ID" in the JSON body — even "ID": "" will cause a server error!</b></para>
    /// </summary>
    private async void PostAfspraak()
    {
        Afspraak nieuweAfspraak = new Afspraak
        {
            Titel = titleInput.text,
            NaamDokter = doctorInput.text,
            DatumTijd = dateInput.text,
            UserId = "placeholder", // Backend handles actual UserId
            Actief = 1
        };

        string json = JsonUtility.ToJson(nieuweAfspraak);
        json = RemoveIdFieldFromJson(json); // Extra safety step to strip "ID" if it somehow appears

        var response = await webClient.SendPostRequest("/Afspraak", json);

        if (response is WebRequestData<string>)
        {
            Debug.Log("✅ Afspraak opgeslagen.");
            LaadAfspraken();
        }
        else
        {
            Debug.LogError("❌ Afspraak kon niet worden opgeslagen.");
        }
    }

    /// <summary>
    /// Removes the "ID" field from a JSON string, if present.
    /// This is important because sending an empty or null ID will cause an error on the backend.
    /// </summary>
    /// <param name="json">The original JSON string</param>
    /// <returns>A sanitized JSON string without the ID field</returns>
    private string RemoveIdFieldFromJson(string json)
    {
        return json.Replace("\"ID\":\"\",", "");
    }


    private async void DeleteSelectedAfspraak()
    {
        if (geselecteerdeIndex == -1 || geselecteerdeIndex >= huidigeAfspraken.Count)
        {
            Debug.LogWarning("⚠️ Geen afspraak geselecteerd.");
            return;
        }

        string id = huidigeAfspraken[geselecteerdeIndex].ID;
        var response = await webClient.SendDeleteRequest("/Afspraak/" + id);

        if (response is WebRequestData<string> || response is WebRequestData<object>)
        {
            Debug.Log("🗑️ Afspraak verwijderd.");
            LaadAfspraken();
        }
        else
        {
            Debug.LogError("❌ Verwijderen mislukt.");
        }
    }


    private async void DeleteAlleAfspraken()
    {
        foreach (var afspraak in huidigeAfspraken)
        {
            await webClient.SendDeleteRequest("/Afspraak/" + afspraak.ID);
        }

        Debug.Log("🧹 Alle afspraken verwijderd.");
        LaadAfspraken();
    }
}
