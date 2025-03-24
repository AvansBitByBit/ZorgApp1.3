using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddLogToDagboek : MonoBehaviour
{
    public TMP_InputField titleInputField;
    public TMP_InputField contentsInputField;
    public Button pushButton;
    public DagboekApiClient dagboekApiClient;

    void Start()
    {
        pushButton.onClick.AddListener(OnPushButtonClicked);
    }

    private async void OnPushButtonClicked()
    {
        string title = titleInputField.text;
        string contents = contentsInputField.text;

        Dagboek dagboek = new Dagboek
        {
            Title = title,
            Contents = contents,
            UserId = "currentUserId" // Replace with the actual user ID
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
}