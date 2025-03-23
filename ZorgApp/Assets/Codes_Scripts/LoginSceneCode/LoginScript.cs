using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public UserApiClient userApiClient;
    public Button GaVerderButton;

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    private async void OnLoginButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        User user = new User { email = email, password = password };
        IWebRequestReponse response = await userApiClient.Login(user);

        if (response is WebRequestData<string> data)
        {
            Debug.Log("Login successful, token: " + data.Data);
            GaVerderButton.GetComponentInChildren<TMP_Text>().text = "Ga ingelogd verder";

        }
        else
        {
            Debug.LogError("Login failed: " + response);
        }
    }
}