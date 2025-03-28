using UnityEngine;
using UnityEngine.UI;

public class IsUserLoggedIn : MonoBehaviour
{

    public WebClient webClient;
    public Button Account;
    public Button Instellingen;
    void Start()
    {
        if(PlayerPrefs.GetString("access_token", null) == null)
        {
            UserNotLoggedIn();

        }
        else
        {

        }


    }

    public void UserNotLoggedIn()
    {
        ChangeColor();

    }

    public void ChangeColor()
    {
        //button kleur veranderen
        Account.GetComponent<Image>().color = Color.black;
        Instellingen.GetComponent<Image>().color = Color.black;

    }


}
