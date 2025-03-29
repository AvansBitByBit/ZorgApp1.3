using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TooltipGenerator : MonoBehaviour
{
    public GameObject[] tooltipPanels;
    public TextMeshProUGUI[] tipTexts;
    public Button generateTooltipButton;

    private List<string> tips = new List<string>()
    {

    "Neem je pijnstillers zoals de dokter het heeft uitgelegd.",
    "Rust goed uit, je lichaam heeft tijd nodig om te herstellen.",
    "Probeer afleiding te zoeken als je pijn hebt, zoals een leuke film kijken.",
    "Vraag hulp als iets pijn doet, je hoeft het niet alleen te doen.",
    "Gebruik een kussen om je arm of been wat hoger te leggen (dat helpt tegen zwelling).",
    "Blijf kalm als het een beetje pijn doet – dat hoort bij beter worden.",
    "Je mag verdrietig of boos zijn – praat erover met iemand die je vertrouwt.",
    "Probeer je spieren zachtjes te bewegen als de dokter dat goedvindt.",
    "Laat een volwassene helpen bij lastige dingen, zoals aankleden of traplopen.",
    "Lach veel – vrolijk zijn helpt je sneller beter te voelen.",
    "Houd je gips droog – gebruik een plastic zak of speciale hoes als je gaat douchen.",
    "Laat je gips niet versieren met stickers die nat kunnen worden of loslaten.",
    "Schrijf of laat iets leuks op je gips tekenen (als de dokter dat goed vindt).",
    "Sla niet met je gips – het is geen wapen.",
    "Ruikt je gips vreemd? Vraag even aan je ouders of het normaal is.",
    "Laat niemand op je gips leunen of eraan trekken.",
    "Zorg dat je gips niet tegen scherpe dingen aan stoot.",
    "Als je gips scheurt of los zit, zeg het meteen tegen je ouders of verzorger.",
    "Probeer elke dag iets leuks te doen, zoals tekenen, gamen of lezen.",
    "Start een ‘gips-dagboek’ – schrijf of teken wat je meemaakt.",
    "Nodig vriendjes uit om langs te komen of samen online te spelen.",
    "Leer iets nieuws, zoals goocheltrucs of origami.",
    "Speel bordspelletjes of puzzels met familie.",
    "Luister naar luisterboeken of podcasts voor kinderen.",
    "Maak je eigen stripverhaal over een superheld met gips!",
    "Probeer een nieuwe hobby: tekenen, modelbouw, muziek luisteren.",
    "Schrijf een brief naar jezelf over wat je gaat doen als je gips eraf mag.",
    "Probeer te lachen om je gips – geef het een gekke naam of versier het vrolijk.",
    "Draag makkelijke kleding die over je gips past.",
    "Draag schoenen met klittenband of slippers (als je gips aan je voet hebt).",
    "Laat iemand je helpen met je schooltas als die te zwaar is.",
    "Gebruik een rugzak in plaats van losse tassen.",
    "Vraag op school of je hulp kunt krijgen bij gym of naar het toilet gaan.",
    "Probeer je gips niet te laten schuren aan meubels.",
    "Als je gips op je been hebt, gebruik krukken op de juiste manier.",
    "Oefen met krukken in huis voordat je naar buiten gaat.",
    "Vraag een rolstoel of buggy als lopen niet lukt – dat is oké!",
    "Vraag op school om een andere plek in de klas als dat fijner is met gips.",
    "Wees niet bang voor de gipszaag – hij klinkt eng, maar doet geen pijn.",
    "De controle bij de dokter is meestal snel klaar.",
    "Vertel de dokter hoe het echt met je gaat – hij of zij is er om te helpen.",
    "Het kan even raar voelen om je arm/been weer te gebruiken, dat gaat vanzelf beter.",
    "Blijf rustig bewegen als je gips eraf is – overhaast niks.",
    "Vraag wat je wel en niet mag doen na het verwijderen van het gips.",
    "Je spieren kunnen een beetje slap voelen – dat wordt snel weer sterker!",
    "Eet gezond om je botten sterker te maken (melk, kaas, groenten!).",
    "Wees trots op jezelf – je hebt dit doorstaan als een echte doorzetter!"
};

    void Start()
    {
        foreach (var panel in tooltipPanels)
        {
            panel.SetActive(false);
        }

        generateTooltipButton.onClick.AddListener(ShowRandomTooltip);
    }

    void ShowRandomTooltip()
    {
        foreach (var panel in tooltipPanels)
        {
            panel.SetActive(false);
        }

        int randomPanelIndex = Random.Range(0, tooltipPanels.Length);
        tooltipPanels[randomPanelIndex].SetActive(true);

        int randomTipIndex = Random.Range(0, tips.Count);

        tipTexts[randomPanelIndex].text = tips[randomTipIndex];
    }
}
