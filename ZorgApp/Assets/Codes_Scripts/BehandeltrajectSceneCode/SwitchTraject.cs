using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchContainers : MonoBehaviour
{
    public GameObject ContainerA;
    public GameObject ContainerB;
    public GameObject ContainerR;

    public Button ButtonA; 
    public Button ButtonB;
    public Button ButtonR;

    void Start()
    {
        // Set up the button listeners with lambda functions for brevity.
        ButtonA.onClick.AddListener(() => SwitchTo(ContainerA));
        ButtonB.onClick.AddListener(() => SwitchTo(ContainerB));
        ButtonR.onClick.AddListener(() => SwitchTo(ContainerR));
    }

    // This method will only show the specified container while hiding the others.
    void SwitchTo(GameObject containerToShow)
    {
        ContainerA.SetActive(containerToShow == ContainerA);
        ContainerB.SetActive(containerToShow == ContainerB);
        ContainerR.SetActive(containerToShow == ContainerR);
    }

}
