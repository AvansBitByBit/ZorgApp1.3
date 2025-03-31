using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Avatarcostumizer : MonoBehaviour
{
    [Header("Naam")]
    public TMP_InputField naamInput;

    [Header("Haar")]
    public Image haarImage;
    public Sprite[] haarStijlen;
    public Button haarBtnLinks;
    public Button haarBtnRechts;
    private int haarIndex = 0;

    [Header("Ogen")]
    public Image[] oogImages;
    public Sprite[] oogStijlen;
    public Button oogBtnLinks;
    public Button oogBtnRechts;
    private int oogIndex = 0;

    [Header("Huidkleur")]
    public Image[] huidImages;
    public Sprite[] huidStijlen;
    public Button huidBtnLinks;
    public Button huidBtnRechts;
    private int huidIndex = 0;

    [Header("Pak")]
    public Image[] pakImages;
    public Sprite[] pakStijlen;
    public Button pakBtnLinks;
    public Button pakBtnRechts;
    private int pakIndex = 0;

    [Header("Opslaan")]
    public Button opslaanBtn;
    public string avatarId = "avatar1"; // Uniek ID voor opslaan

    void Start()
    {
        // Koppel de knoppen
        haarBtnLinks.onClick.AddListener(() => VeranderHaar(-1));
        haarBtnRechts.onClick.AddListener(() => VeranderHaar(1));

        oogBtnLinks.onClick.AddListener(() => VeranderOgen(-1));
        oogBtnRechts.onClick.AddListener(() => VeranderOgen(1));

        huidBtnLinks.onClick.AddListener(() => VeranderHuid(-1));
        huidBtnRechts.onClick.AddListener(() => VeranderHuid(1));

        pakBtnLinks.onClick.AddListener(() => VeranderPak(-1));
        pakBtnRechts.onClick.AddListener(() => VeranderPak(1));

        opslaanBtn.onClick.AddListener(SlaOp);

        // Laad opgeslagen opties als deze bestaan
        if (PlayerPrefs.HasKey(avatarId + "_haar"))
        {
            haarIndex = PlayerPrefs.GetInt(avatarId + "_haar");
            oogIndex = PlayerPrefs.GetInt(avatarId + "_ogen");
            huidIndex = PlayerPrefs.GetInt(avatarId + "_huid");
            pakIndex = PlayerPrefs.GetInt(avatarId + "_pak");
        }

        // Laad de naam als deze opgeslagen is
        if (PlayerPrefs.HasKey(avatarId + "_naam"))
        {
            string savedName = PlayerPrefs.GetString(avatarId + "_naam");
            if (naamInput != null)
                naamInput.text = savedName;
        }

        // Pas de startwaarden toe
        UpdateHaar();
        UpdateOgen();
        UpdateHuid();
        UpdatePak();
    }

    void VeranderHaar(int richting)
    {
        haarIndex = (haarIndex + richting + haarStijlen.Length) % haarStijlen.Length;
        UpdateHaar();
    }

    void VeranderOgen(int richting)
    {
        oogIndex = (oogIndex + richting + oogStijlen.Length) % oogStijlen.Length;
        UpdateOgen();
    }

    void VeranderHuid(int richting)
    {
        huidIndex = (huidIndex + richting + huidStijlen.Length) % huidStijlen.Length;
        UpdateHuid();
    }

    void VeranderPak(int richting)
    {
        pakIndex = (pakIndex + richting + pakStijlen.Length) % pakStijlen.Length;
        UpdatePak();
    }

    void UpdateHaar()
    {
        if (haarImage != null && haarStijlen.Length > 0)
            haarImage.sprite = haarStijlen[haarIndex];
    }

    void UpdateOgen()
    {
        foreach (Image img in oogImages)
        {
            if (img != null && oogStijlen.Length > 0)
                img.sprite = oogStijlen[oogIndex];
        }
    }

    void UpdateHuid()
    {
        foreach (Image img in huidImages)
        {
            if (img != null && huidStijlen.Length > 0)
                img.sprite = huidStijlen[huidIndex];
        }
    }

    void UpdatePak()
    {
        foreach (Image img in pakImages)
        {
            if (img != null && pakStijlen.Length > 0)
                img.sprite = pakStijlen[pakIndex];
        }
    }

    void SlaOp()
    {
        PlayerPrefs.SetInt(avatarId + "_haar", haarIndex);
        PlayerPrefs.SetInt(avatarId + "_ogen", oogIndex);
        PlayerPrefs.SetInt(avatarId + "_huid", huidIndex);
        PlayerPrefs.SetInt(avatarId + "_pak", pakIndex);

        if (naamInput != null)
            PlayerPrefs.SetString(avatarId + "_naam", naamInput.text);

        PlayerPrefs.Save();

        string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        Debug.Log("Avatar en naam opgeslagen als: " + avatarId + " op " + timestamp);
    }
}
