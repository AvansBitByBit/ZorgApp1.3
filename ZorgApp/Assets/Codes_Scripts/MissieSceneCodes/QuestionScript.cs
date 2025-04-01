using TMPro;
using UnityEngine;

public class QuestionScript : MonoBehaviour
{
    public GameObject Answer1;
    public GameObject Answer2;
    public GameObject Answer3;
    public GameObject NextMission;
    public TMP_Text Status;

    // Koppel hier je Particle System via Inspector
    public ParticleSystem confettiEffect;

    public void OnAnswer1Clicked()
    {
        Status.text = "Fout! Probeer het opnieuw.";
    }

    public void OnAnswer2Clicked()
    {
        Status.text = "Goed gedaan!";
        NextMission.SetActive(true);

        // Activeer hier de particle animatie
        if (confettiEffect != null)
        {
            confettiEffect.Play();
        }
        else
        {
            Debug.LogError("Confetti Particle System is niet gekoppeld in de Inspector!");
        }
    }

    public void OnAnswer3Clicked()
    {
        Status.text = "Fout! Probeer het opnieuw.";
    }
}
