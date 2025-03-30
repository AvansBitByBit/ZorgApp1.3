using UnityEngine;
using UnityEngine.UI;

public class KindGegevensInvvoeren : MonoBehaviour
{
    public Button saveButton;
    public TMPro.TMP_InputField nameInputField;
    public TMPro.TMP_InputField lastnameInputField;
    public Button ArmLinks;
    public Button ArmRechts;
    public Button BeenLinks;
    public Button BeenRechts;
    public Button Ribben;
    
    void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        ArmLinks.onClick.AddListener(OnArmLinksButtonClicked);
        ArmRechts.onClick.AddListener(OnArmRechtsButtonClicked);
        BeenLinks.onClick.AddListener(OnBeenLinksButtonClicked);
        BeenRechts.onClick.AddListener(OnBeenRechtsButtonClicked);
        Ribben.onClick.AddListener(OnRibbenButtonClicked);
    }

    private void OnArmRechtsButtonClicked()
    {
        
    }

    private void OnBeenLinksButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private void OnRibbenButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private void OnBeenRechtsButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private void OnArmLinksButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private void OnSaveButtonClicked()
    {
        throw new System.NotImplementedException();
    }
}
