using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public Button registerButton;
    public UserApiClient userApiClient;
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    private async void OnRegisterButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;


        User user = new User { email = email, password = password };
        IWebRequestReponse response = await userApiClient.Register(user);

        if (response is WebRequestData<string> data)
        {
            Debug.Log("Registration successful, token: " + data.Data);
            LoginPanel.SetActive(true);
            RegisterPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Registration failed: " + response);
        }
    }
}