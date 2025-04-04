using UnityEngine;

public class Animation : MonoBehaviour
{
    public GameObject confettiEffect; // Koppel het confetti particle-systeem hieraan via Inspector

    public void CheckAnswer(string gekozenAntwoord)
    {
        string juistAntwoord = "Rontgenfoto"; // Het goede antwoord

        if (gekozenAntwoord == juistAntwoord)
        {
            // Goed antwoord, speel het effect af
            PlayConfettiEffect();
        }
        else
        {
            Debug.Log("Helaas, probeer opnieuw!");
        }
    }

    void PlayConfettiEffect()
    {
        confettiEffect.SetActive(true); // Activeer het particle-systeem
        confettiEffect.GetComponent<ParticleSystem>().Play();
    }
}
