using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.EventSystems;

public class ScriptManager : MonoBehaviour
{
    public GameObject SoundManager;
    private string currentTime = "";
    private string[] splitDuration;
    List<string> scriptRedListed = new List<string>();
    List<string> scriptBlackListed = new List<string>();
    Text txtRecensione;
    private Dictionary<int, string> scriptDict = new Dictionary<int, string>();
    private List<String> scriptList = new List<string>();

    [SerializeField] ScrollRect autoScrollRect;
    [SerializeField] private RectTransform contentRectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        txtRecensione = GameObject
            .Find(
                "Canvas/Panel/Panel/ScrollViewAscolta/Viewport/Content/ScrollViewRecensione/Viewport/Content/txtRecensione")
            .GetComponent<Text>();

        scriptDict[1] = "Salve a tutti dal vostro personal sommelier.";
        scriptDict[3] = "Ci troviamo nel cuore del Valpolicella classico";
        scriptDict[6] = "e abbiamo nel bicchiere uno dei prodotti";
        scriptDict[9] = "che sono il fiore all'occhiello dell'enologia nazionale: un";
        scriptDict[12] = "amarone del Valpolicella classico. L'azienda che ce lo";
        scriptDict[15] = "presenta, la Bonazzi Boscalini dei fratelli Giovanni e Luigi";
        scriptDict[19] = "ci da oggi in degustazione un'annata 2006.";
        scriptDict[23] = "Quali sono i vitigni dell'amarone innanzitutto";

        foreach (KeyValuePair<int, string> scriptScorePair in scriptDict)
        {
            scriptList.Add(scriptScorePair.Value);
        }

        txtRecensione.text = (string.Join("\n", scriptList));

        if (autoScrollRect == null) autoScrollRect = GetComponent<ScrollRect>();

    }



    // Update is called once per frame
    void Update()
    {
        currentTime = SoundManager.GetComponent<SoundManager>().getCurrentTime();

        int script_minutes = 0;
        int script_seconds = 0;

        splitDuration = currentTime.Split(':');

        if (splitDuration.Length > 1)
        {
            if (!(String.IsNullOrEmpty(splitDuration[0])) || !(String.IsNullOrEmpty(splitDuration[1])))
            {
                script_minutes = Int32.Parse(splitDuration[0]);
                script_seconds = Int32.Parse(splitDuration[1]);
            }
        }

        int totalSeconds = 0;

        totalSeconds = script_minutes * 60 + script_seconds;

        scriptRedListed = scriptDict
            .Where(item => item.Key <= totalSeconds)
            .Select(item => item.Value)
            .ToList();
        
   
        scriptBlackListed = scriptDict
            .Where(item => item.Key > totalSeconds)
            .Select(item => item.Value)
            .ToList();

        if (scriptRedListed.Count > 0)
        {
            scriptBlackListed.Insert(0, "");
        }
        
        txtRecensione.text = "<color=blue>" + string.Join("\n", scriptRedListed) + "</color>" +
                             string.Join("\n", scriptBlackListed);

        // Calculate the total height of the content
        float totalContentHeight = contentRectTransform.rect.height;

        // Calculate the height of a single line of text in the ScrollView
        float lineHeight = txtRecensione.fontSize + txtRecensione.lineSpacing;

        // Calculate the position of the highlighted text (in pixels) from the top of the content
        float highlightedTextPosition = (scriptRedListed.Count * lineHeight) - lineHeight;

        // Calculate the height of the ScrollView viewport
        float scrollViewViewportHeight = autoScrollRect.viewport.rect.height;

        // Calculate the maximum vertical scroll position (bottom-most position of the ScrollView)
        float maxVerticalScrollPosition = totalContentHeight - scrollViewViewportHeight;

        // Calculate the target vertical scroll position based on the highlighted text position
        float targetVerticalScrollPosition = Mathf.Clamp(highlightedTextPosition, 0f, maxVerticalScrollPosition);

        // Calculate the normalized vertical scroll position (between 0 and 1)
        float normalizedScrollPosition = targetVerticalScrollPosition / maxVerticalScrollPosition;
   
        // Set the vertical scroll position of the Scrollbar directly
        autoScrollRect.verticalScrollbar.value = 1f - normalizedScrollPosition;
        
        
    }

}