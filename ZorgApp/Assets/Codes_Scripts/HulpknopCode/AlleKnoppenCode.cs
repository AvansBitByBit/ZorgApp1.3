using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlleKnoppenCode : MonoBehaviour
{
    public List<Button> enlargeOnHoverButtons;
    public List<Button> disableIfNotLoggedInButtons;
    public List<Button> UnclickableButtons;
    private UserApiClient userApiClient;
    private Vector3 originalScale;
    public WebClient _webclient;

    void Start()
    {

        foreach (Button button in enlargeOnHoverButtons)
        {
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => { OnHoverEnter(button); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => { OnHoverExit(button); });
            trigger.triggers.Add(entryExit);
        }

        UpdateButtonStates();
    }

    void OnHoverEnter(Button button)
    {
        originalScale = button.transform.localScale;
        button.transform.localScale = originalScale * 1.2f; 
    }

    void OnHoverExit(Button button)
    {
        button.transform.localScale = originalScale;
    }

    void UpdateButtonStates()
    {
        bool isLoggedIn = _webclient.CheckToken();

        // Disable buttons if user is NOT logged in
        foreach (Button button in disableIfNotLoggedInButtons)
        {
            button.interactable = isLoggedIn;

            ColorBlock colors = button.colors;
            colors.normalColor = isLoggedIn ? Color.white : Color.gray;
            button.colors = colors;
        }

        // These buttons are ALWAYS unclickable
        foreach (Button button in UnclickableButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            button.interactable = false;
        }
    }


}