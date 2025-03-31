using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchToLoginPage : MonoBehaviour
{
    public GameObject Registercontainer, LoginContainer;

    public Button SwitchToLoginButton,
        SwitchToRegisterButton,
        ZonderAccountButton1,
        ZonderAccountButton;
    public WebClient webClient;

    void Start()
    {
        SwitchToLoginButton.onClick.AddListener(HideRegisterContainer);
        SwitchToRegisterButton.onClick.AddListener(ShowRegisterContainer);
        ZonderAccountButton.onClick.AddListener(ZonderAccount);
        ZonderAccountButton1.onClick.AddListener(ZonderAccount);
        webClient.SetToken(null);
    }
    

    public void ZonderAccount()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void HideRegisterContainer()
    {
        Registercontainer.SetActive(false);
        LoginContainer.SetActive(true);
    }
    void ShowRegisterContainer()
    {
        Registercontainer.SetActive(true);
        LoginContainer.SetActive(false);
    }
}