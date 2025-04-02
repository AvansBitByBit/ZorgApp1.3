using TMPro;
using UnityEngine;

public class QuestionScript : MonoBehaviour
{
    public GameObject Answer1;
    public GameObject Answer2;
    public GameObject Answer3;
    public GameObject NextMission;
    public TMP_Text Status;

    public void OnAnswer1Clicked()
    {
        Debug.Log("Answer 1 clicked");
        Status.text = "Fout! Probeer het opnieuw.";
    }

    public void OnAnswer2Clicked()
    {
        Debug.Log("Answer 2 clicked");
        Status.text = "Goed gedaan!";
        NextMission.SetActive(true);
    }

    public void OnAnswer3Clicked()
    {
        Debug.Log("Answer 3 clicked");
        Status.text = "Fout! Probeer het opnieuw.";
    }
}
