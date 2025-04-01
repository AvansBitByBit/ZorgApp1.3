using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarLoader : MonoBehaviour
{
    public GameObject avatar;

    [Header("Haar")]
    public Image haarImage;
    public Sprite[] haarStijlen;

    [Header("Ogen")]
    public Image[] oogImages;
    public Sprite[] oogStijlen;

    [Header("Huidkleur")]
    public Image[] huidImages;
    public Sprite[] huidStijlen;

    [Header("Pak")]
    public Image[] pakImages;
    public Sprite[] pakStijlen;

    private string avatarId = "avatar1"; // Zelfde ID als in Avatarcostumizer

    void Start()
    {
        if (IsPlayerTokenValid())
        {
            avatar.SetActive(false);
            // Laad opgeslagen waarden
            int haarIndex = PlayerPrefs.GetInt(avatarId + "_haar", 0);
            int oogIndex = PlayerPrefs.GetInt(avatarId + "_ogen", 0);
            int huidIndex = PlayerPrefs.GetInt(avatarId + "_huid", 0);
            int pakIndex = PlayerPrefs.GetInt(avatarId + "_pak", 0);
            string naam = PlayerPrefs.GetString(avatarId + "_naam", "Geen Naam");

            // Pas de sprites en naam toe
            if (haarImage != null && haarStijlen.Length > 0)
                haarImage.sprite = haarStijlen[haarIndex];

            foreach (Image img in oogImages)
                if (img != null && oogStijlen.Length > 0)
                    img.sprite = oogStijlen[oogIndex];

            foreach (Image img in huidImages)
                if (img != null && huidStijlen.Length > 0)
                    img.sprite = huidStijlen[huidIndex];

            foreach (Image img in pakImages)
                if (img != null && pakStijlen.Length > 0)
                    img.sprite = pakStijlen[pakIndex];
        }
        else
        {
            avatar.SetActive(true);
        }
    }

    private bool IsPlayerTokenValid()
    {
        // Check if player token exists
        if (!PlayerPrefs.HasKey(avatarId + "_naam"))
        {
            return false;
        }

        return true;
    }
}