using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RaketAnimatieManager : MonoBehaviour
{
    public Animator raketAnimatie;
    public List<PlanetButtonInfo> planetButtons;

    private int lastPlanetNumber = -1;
    private string lastBodyPart = "";

    private void Start()
    {
        foreach (var planet in planetButtons)
        {
            planet.button.onClick.AddListener(() => OnPlanetClicked(planet));
        }
    }

    private void OnPlanetClicked(PlanetButtonInfo planet)
    {
        string triggerToSet;

        // Special case: Arm1 -> Arm3 = "ToArm3.1"
        if (lastBodyPart == "Arm" && lastPlanetNumber == 1 && planet.bodyPart == "Arm" && planet.planetNumber == 3)
        {
            triggerToSet = "ToArm3.1";
        }
        else
        {
            triggerToSet = $"To{planet.bodyPart}{planet.planetNumber}";
        }

        Debug.Log($"Trigger: {triggerToSet}");
        raketAnimatie.SetTrigger(triggerToSet);

        // Update last clicked info
        lastPlanetNumber = planet.planetNumber;
        lastBodyPart = planet.bodyPart;

        // Start coroutine to load scene
        StartCoroutine(LoadSceneAfterAnimation($"M{planet.planetNumber}_{planet.bodyPart}en"));
    }

    private IEnumerator LoadSceneAfterAnimation(string sceneName)
    {
        yield return null; // Wait one frame to let animator update

        var state = raketAnimatie.GetCurrentAnimatorStateInfo(0);
        float animationLength = state.length+0.5f;

        Debug.Log($"Playing animation: {state.fullPathHash} | Length: {animationLength}");

        yield return new WaitForSeconds(animationLength + 0.5f);
        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class PlanetButtonInfo
{
    public Button button;
    public string bodyPart;     // e.g. "Arm", "Been", "Rib"
    public int planetNumber;    // e.g. 1, 2, 3
}