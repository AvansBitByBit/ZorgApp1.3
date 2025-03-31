using UnityEngine;
using UnityEngine.UI;

public class Avatarcostumizer : MonoBehaviour
{
    [Header("Haar")]
    public Image haarImage;
    public Sprite[] haarStijlen;
    public Button haarBtnLinks;
    public Button haarBtnRechts;
    private int haarIndex = 0;

    [Header("Ogen")]
    public Image [] oogImages;
    public Sprite[] oogStijlen;
    public Button oogBtnLinks;
    public Button oogBtnRechts;
    private int oogIndex = 0;

    [Header("Huidkleur")]
    public Image [] huidImages;
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

    void Start()
    {
        // Haar
        haarBtnLinks.onClick.AddListener(() => VeranderHaar(-1));
        haarBtnRechts.onClick.AddListener(() => VeranderHaar(1));

        // Ogen
        oogBtnLinks.onClick.AddListener(() => VeranderOgen(-1));
        oogBtnRechts.onClick.AddListener(() => VeranderOgen(1));

        // Huid
        huidBtnLinks.onClick.AddListener(() => VeranderHuid(-1));
        huidBtnRechts.onClick.AddListener(() => VeranderHuid(1));

        // Pak
        pakBtnLinks.onClick.AddListener(() => VeranderPak(-1));
        pakBtnRechts.onClick.AddListener(() => VeranderPak(1));

        // Startwaarden toepassen
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

    void UpdateHaar() => haarImage.sprite = haarStijlen[haarIndex];
    void UpdateOgen()
    {
        foreach (Image img in oogImages)
        {
            img.sprite = oogStijlen[oogIndex];
        }
    }

    void UpdateHuid()
    {
        foreach (Image img in huidImages)
        {
            img.sprite = huidStijlen[huidIndex];
        }
    }


    void UpdatePak()
    {
        foreach (Image img in pakImages)
        {
            img.sprite = pakStijlen[oogIndex];
        }
    }
}
