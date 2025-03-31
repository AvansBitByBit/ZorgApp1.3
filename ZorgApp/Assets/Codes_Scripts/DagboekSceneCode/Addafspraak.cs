using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Handles the creation of a new afspraak using TMP input fields.
/// </summary>
public class AddAfspraak : MonoBehaviour
{
    public TMP_InputField Titel;
    public TMP_InputField NaamDokter;
    public TMP_InputField DatumTijd;
    public AfspraakApiClient afspraakApiClient;
    public Button saveButton;

    void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    /// <summary>
    /// Called when the save button is clicked. Creates a new afspraak.
    /// </summary>
    /// <remarks>
    /// Example JSON body for creating an afspraak:
    /// {
    ///     "titel": "Doktersafspraak",
    ///     "NaamDokter": "Dr. Smith",
    ///     "DatumTijd": "2025-01-01T00:00:00",
    ///     "userId": "user-id",
    ///     "Actief": 1
    /// }
    /// </remarks>
    public async void OnSaveButtonClicked()
    {
        string titel = Titel.text;
        string naamDokter = NaamDokter.text;
        string datumTijd = DatumTijd.text;

        // Convert DatumTijd to ISO 8601 format
        DateTime parsedDate;
        if (!DateTime.TryParseExact(datumTijd, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
        {
            Debug.LogError("Invalid date format. Please use the format dd-MM-yyyy.");
            return;
        }
        string isoDatumTijd = parsedDate.ToString("yyyy-MM-ddTHH:mm:ss");

        Afspraak afspraak = new Afspraak
        {
            titel = titel,
            naamDokter = naamDokter,
            datumTijd = isoDatumTijd,
            userId = "user-id", // Replace with actual user id if available
            actief = 1
        };

        bool success = await afspraakApiClient.CreateAfspraak(afspraak);

        if (success)
        {
            Debug.Log("Afspraak created successfully.");
        }
        else
        {
            Debug.LogError("Failed to create Afspraak.");
        }
    }
}