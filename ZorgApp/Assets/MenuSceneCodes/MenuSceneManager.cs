using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{

    public Button SwitchToVideo1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchToVideo1.onClick.AddListener(PlayVideo1);
        
    }

    public void PlayVideo1()
    {
        SceneManager.LoadScene("Video1Scene");
    }
}
