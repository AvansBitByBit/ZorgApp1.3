using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class RaketAnimatieManager : MonoBehaviour
{
    [CanBeNull] public Button PlaneetArm1,
        PlaneetArm2,
        PlaneetArm3,
        PlaneetBeen1,
        PlaneetBeen2,
        PlaneetBeen3,
        PlaneetRib1,
        PlaneetRib2,
        PlaneetRib3;
    public Animator raketAnimatie;
    private bool isPlanet3Pressed = false;

    public void Start()
    {
        PlaneetArm1?.onClick.AddListener(() => OnPlanetButtonClicked(1, "Arm"));
        PlaneetArm2?.onClick.AddListener(() => OnPlanetButtonClicked(2, "Arm"));
        PlaneetArm3?.onClick.AddListener(() => OnPlanetButtonClicked(3, "Arm"));
        PlaneetBeen1?.onClick.AddListener(() => OnPlanetButtonClicked(1, "Been"));
        PlaneetBeen2?.onClick.AddListener(() => OnPlanetButtonClicked(2, "Been"));
        PlaneetBeen3?.onClick.AddListener(() => OnPlanetButtonClicked(3, "Been"));
        PlaneetRib1?.onClick.AddListener(() => OnPlanetButtonClicked(1, "Rib"));
        PlaneetRib2?.onClick.AddListener(() => OnPlanetButtonClicked(2, "Rib"));
        PlaneetRib3?.onClick.AddListener(() => OnPlanetButtonClicked(3, "Rib"));
    }

    private void OnPlanetButtonClicked(int planetNumber, string bodyPart)
    {
        if (planetNumber == 3)
        {
            raketAnimatie.SetTrigger($"Planeet{planetNumber}{bodyPart}Clicked");
            isPlanet3Pressed = true;
        }
        else
        {
            if (isPlanet3Pressed)
            {
                raketAnimatie.SetTrigger("Planet3ToPlanet1");
                isPlanet3Pressed = false;
            }
            else
            {
                raketAnimatie.SetTrigger($"Start{bodyPart}");
            }
        }

        StartCoroutine(LoadSceneAfterAnimation($"M{planetNumber}_{bodyPart}en"));
    }

    private IEnumerator LoadSceneAfterAnimation(string sceneName)
    {
        AnimatorStateInfo animationState = raketAnimatie.GetCurrentAnimatorStateInfo(0);
        float animationLength = animationState.length+0.5f;

        yield return new WaitForSeconds(animationLength);
        SceneManager.LoadScene(sceneName);
    }
}