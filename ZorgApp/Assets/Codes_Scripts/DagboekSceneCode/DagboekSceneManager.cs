using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DagboekSceneManager : MonoBehaviour
{
    [Header("Buttons")] public List<Button> logButtons;
    public Button refreshButton;
    public Button deleteButton;
    public Button deleteAllButton;

    private List<Dagboek> huidigeDagboeken = new();
    private Dagboek geselecteerdeDagboek;

    [Header("WebClient Reference")]
    public WebClient webClient;

    [Header("Dagboek Output")] public TextMeshProUGUI Titel;
    public TextMeshProUGUI Content;



    private void Start()
    {
        refreshButton.onClick.AddListener(LaadDagboeken);
        deleteButton.onClick.AddListener(DeleteSelectedDagboek);
        deleteAllButton.onClick.AddListener(DeleteDagboek);

        LaadDagboeken();
    }

    private async void LaadDagboeken()
    {
        geselecteerdeDagboek = null;

        var response = await webClient.SendGetRequest("/Dagboek");

        if (response == null)
        {
            Debug.LogError("❌ response is null");
            return;
        }

        if (response is WebRequestData<string> data)
        {

            Debug.Log("📥 Ontvangen JSON:\n" + data.Data);

            huidigeDagboeken = JsonHelper.ParseJsonArray<Dagboek>(data.Data);


            for (int i = 0; i < huidigeDagboeken.Count; i++)
            {
                Dagboek d = huidigeDagboeken[i];
                Debug.Log($"🧾 [{i}] Titel: {d.title} | Datum: {d.date} | id: {d.id}");

            }

            SetTextOnLogButtons();
        }
    }

    private void SetTextOnLogButtons()
    {
        for (int i = 0; i < logButtons.Count; i++)
        {
            if (logButtons[i] != null)
            {
                TextMeshProUGUI buttonText = logButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    if (i < huidigeDagboeken.Count)
                    {
                        buttonText.text = huidigeDagboeken[i].title;
                        int index = i; // Capture the current index
                        logButtons[i].onClick.RemoveAllListeners(); // Remove any existing listeners
                        logButtons[i].onClick.AddListener(() => DisplayDagboekDetails(index)); // Add new listener
                    }
                    else
                    {
                        buttonText.text = "Geen dagboek";
                        logButtons[i].onClick.RemoveAllListeners(); // Remove any existing listeners
                    }
                }
                else
                {
                    Debug.LogWarning($"Button at index {i} is missing a TextMeshProUGUI component.");
                }
            }
            else
            {
                Debug.LogWarning($"Button at index {i} is not assigned.");
            }
        }
    }

    private void DisplayDagboekDetails(int index)
    {
        if (index >= 0 && index < huidigeDagboeken.Count)
        {
            geselecteerdeDagboek = huidigeDagboeken[index];
            Titel.text = geselecteerdeDagboek.title;
            Content.text = geselecteerdeDagboek.contents;
            Debug.Log($"✅ Geselecteerde dagboek: {geselecteerdeDagboek.title} (id: {geselecteerdeDagboek.id})");
        }
        else
        {
            Debug.LogWarning("⚠️ Ongeldige dagboek index.");
        }
    }

    private async void DeleteSelectedDagboek()
    {

        var response = await webClient.SendDeleteRequest("/dagboek/" + geselecteerdeDagboek.id);
        if (response is WebRequestData<string> || response is WebRequestData<object>)
        {
            Debug.Log("🗑️ Afspraak verwijderd.");
            LaadDagboeken();
        }
        else
        {
            Debug.LogError("❌ Verwijderen mislukt.");
        }

    }

    private async void DeleteDagboek()
    {
        foreach (var log in huidigeDagboeken)
        {
            await webClient.SendDeleteRequest($"/Dagboek/" + log.id);
        }
        Debug.Log("🗑️ Alle dagboeken zijn verwijderd.");
        LaadDagboeken();

    }


}
