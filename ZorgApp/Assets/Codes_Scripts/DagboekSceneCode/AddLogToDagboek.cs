using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class AddLogToDagboek : MonoBehaviour
{
    public TMP_InputField titleInputField;
    public TMP_InputField contentsInputField;
    public Button pushButton;
    public DagboekApiClient dagboekApiClient;
    public WebClient webClient;
    public GameObject GetDagboekPanel;
    public GameObject Notitiespanel;
    public GameObject NieuweNotitiePanel;



    void Start()
    {

        webClient.CheckToken();
        pushButton.onClick.AddListener(OnPushButtonClicked);
    }

    private async void OnPushButtonClicked()
    {
        string title = titleInputField.text;
        string contents = contentsInputField.text;
        Random random = new Random();
        int randomId = random.Next(0, 2147483647);

        Dagboek dagboek = new Dagboek
        {

            id = randomId,
            title = title,
            contents = contents,
            date = DateTime.Now.ToString("yyyy-MM-dd"),
            userId = "currentUserId" // Replace with the actual user ID
        };

        IWebRequestReponse response = await dagboekApiClient.PushDagboek(dagboek);

        if (response is WebRequestData<string> data)
        {
            Debug.Log("Dagboek entry pushed successfully: " + data.Data);
        }
        else
        {
            Debug.LogError("Failed to push Dagboek entry: " + response);
        }
    }
    public async void OnFetchButtonClicked()
    {
        IWebRequestReponse response = await dagboekApiClient.FetchAllDagboeken();

        if (response is WebRequestData<string> data)
        {
            Debug.Log("Dagboek entries fetched successfully: " + data.Data);

            GetDagboekPanel.SetActive(true);
            Notitiespanel.SetActive(false);
            NieuweNotitiePanel.SetActive(false);

        }
        else
        {
            Debug.LogError("Failed to fetch Dagboek entries: " + response);
        }

    }


}