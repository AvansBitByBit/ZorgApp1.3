using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PollSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button BackToHomeButton;
    
    void Start()
    {
        BackToHomeButton.onClick.AddListener(BackToHome);
        
    }

 public void BackToHome()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
