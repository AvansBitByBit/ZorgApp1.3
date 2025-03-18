using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoSceneManager : MonoBehaviour
{
    public Button GoToPoll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoToPoll.onClick.AddListener(PlayPoll);
    }

    // Update is called once per frame
   public void PlayPoll()
    {
        SceneManager.LoadScene("PollScene");
    }
}
