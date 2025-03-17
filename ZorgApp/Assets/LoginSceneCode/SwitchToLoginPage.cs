using UnityEngine;
using UnityEngine.UI;

public class SwitchToLoginPage : MonoBehaviour
{
    public GameObject Registercontainer, LoginContainer;
    public Button SwitchToLoginButton, SwitchToRegisterButton;

    void Start()
    {
        SwitchToLoginButton.onClick.AddListener(HideRegisterContainer);
        SwitchToRegisterButton.onClick.AddListener(ShowRegisterContainer);
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