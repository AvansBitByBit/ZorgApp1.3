using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Addafspraak : MonoBehaviour
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

        Afspraak afspraak = new Afspraak
        {
            Titel = titel,
            NaamDokter = naamDokter,
            DatumTijd = datumTijd,
            UserId = "currentUserId"
        };

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