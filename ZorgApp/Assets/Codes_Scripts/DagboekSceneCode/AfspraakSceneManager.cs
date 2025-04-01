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
    public string AfspraakId;

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

            for (int i = 0; i < huidigeAfspraken.Count; i++)
            {
                Afspraak a = huidigeAfspraken[i];
                Debug.Log($"🧾 [{i}] Titel: {a.titel} | Dokter: {a.naamDokter} | Datum: {a.datumTijd} | id: {a.id}");
            }

            ToonAfspraken();
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
                var id = afspraak.id;

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = afspraak.titel;
                }

                button.interactable = true;
                button.name = id;

                button.onClick.AddListener(() => SelectAfspraakByid(id));
                Debug.Log($"📌 Knop {i} toegewezen aan afspraak: {afspraak.titel} (id: {id})");
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
    private void SelectAfspraakByid(string id)
    {
        geselecteerdeAfspraak = huidigeAfspraken.Find(a => a.id == id);

        if (geselecteerdeAfspraak != null)
        {
            titleInput.text = geselecteerdeAfspraak.titel;
            doctorInput.text = geselecteerdeAfspraak.naamDokter;
            dateInput.text = geselecteerdeAfspraak.datumTijd;
            Debug.Log($"✅ Geselecteerde afspraak: {geselecteerdeAfspraak.titel} (id: {geselecteerdeAfspraak.id})");
            // logica voor het selecteren van een afspraak die slaat id op in een variabele
            AfspraakId = id;
        }
        else
        {
            Debug.LogWarning("⚠️ Geen afspraak gevonden met id: " + id);
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

        titleInput.text = afspraak.titel;
        doctorInput.text = afspraak.naamDokter;
        dateInput.text = afspraak.datumTijd;

        Debug.Log($"✅ Geselecteerde afspraak: {afspraak.titel} (id: {afspraak.id})");
    }


    /// <summary>
    /// Creates a new afspraak by posting to the WebAPI.
    /// <para>Correct JSON body (do NOT include id field):</para>
    /// <code>
    /// {
    ///   "titel": "Doktersafspraak1",
    ///   "naamDokter": "Dr. Smith",
    ///   "datumTijd": "2025-01-01T00:00:00",
    ///   "userid": "user-id",
    ///   "actief": 1
    /// }
    /// </code>
    /// <para><b>⚠️ Do NOT include "id" in the JSON body — even "id": "" will cause a server error!</b></para>
    /// </summary>
    private async void PostAfspraak()
    {
        Afspraak nieuweAfspraak = new Afspraak
        {
            titel = titleInput.text,
            naamDokter = doctorInput.text,
            datumTijd = dateInput.text,
            userId = "placeholder", // Backend handles actual userid
            actief = 1
        };

        string json = JsonUtility.ToJson(nieuweAfspraak);
        json = RemoveidFieldFromJson(json); // Extra safety step to strip "id" if it somehow appears

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
    /// Removes the "id" field from a JSON string, if present.
    /// This is important because sending an empty or null id will cause an error on the backend.
    /// </summary>
    /// <param name="json">The original JSON string</param>
    /// <returns>A sanitized JSON string without the id field</returns>
    private string RemoveidFieldFromJson(string json)
    {
        return json.Replace("\"id\":\"\",", "");
    }


    private async void DeleteSelectedAfspraak()
    {


        var response = await webClient.SendDeleteRequest("/Afspraak/" + geselecteerdeAfspraak.id);

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
            await webClient.SendDeleteRequest("/Afspraak/" + afspraak.id);
        }

        Debug.Log("🧹 Alle afspraken verwijderd.");
        LaadAfspraken();
    }
}
