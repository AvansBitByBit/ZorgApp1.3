using UnityEngine;
using UnityEngine.UI;

public class CustomDropdown : MonoBehaviour
{
    public GameObject dropdownPanel; // Reference to the panel

    void Start()
    {
        dropdownPanel.SetActive(false); // Hide panel initially
    }

    public void ToggleDropdown()
    {
        dropdownPanel.SetActive(!dropdownPanel.activeSelf);

    }


   
}
