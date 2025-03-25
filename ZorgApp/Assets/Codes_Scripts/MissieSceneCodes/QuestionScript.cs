using UnityEngine;

public class QuestionScript : MonoBehaviour
{
  public GameObject Answer1;
  public GameObject Answer2;
  public GameObject Answer3;
  public GameObject NextMission;

  public void OnAnswer1Clicked()
  {
    Debug.Log("Answer 1 clicked");
  }

  public void OnAnswer2Clicked()
  {
    Debug.Log("Answer 2 clicked");
    NextMission.SetActive(true);

  }

  public void OnAnswer3Clicked()
  {
    Debug.Log("Answer 3 clicked");
  }



}
