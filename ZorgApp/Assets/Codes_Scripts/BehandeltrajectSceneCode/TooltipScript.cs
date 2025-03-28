using UnityEngine;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour
{
    public GameObject dropdownPanel, dropdownPanel1, dropdownPanel2, dropdownPanel3, dropdownPanel4, dropdownPanel5, dropdownPanel6, dropdownPanel7, dropdownPanel8, dropdownPanel9, dropdownPanel10, dropdownPanel11;

    void Start()
    {
        dropdownPanel.SetActive(false);
        dropdownPanel1.SetActive(false);
        dropdownPanel2.SetActive(false);
        dropdownPanel3.SetActive(false);
        dropdownPanel4.SetActive(false);
        dropdownPanel5.SetActive(false);
        dropdownPanel6.SetActive(false);
        dropdownPanel7.SetActive(false);
        dropdownPanel8.SetActive(false);
        dropdownPanel9.SetActive(false);
        dropdownPanel10.SetActive(false);
        dropdownPanel11.SetActive(false);
    }

    public void ToggleDropdown(int index)
    {
        GameObject[] dropdownPanels = { dropdownPanel, dropdownPanel1, dropdownPanel2, dropdownPanel3, dropdownPanel4, dropdownPanel5, dropdownPanel6, dropdownPanel7, dropdownPanel8, dropdownPanel9, dropdownPanel10, dropdownPanel11 };

        if (index >= 0 && index < dropdownPanels.Length)
        {
            // Toggle visibility of the selected panel
            dropdownPanels[index].SetActive(!dropdownPanels[index].activeSelf);
        }
    }
}
