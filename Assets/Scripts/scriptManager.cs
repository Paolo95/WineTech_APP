using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class scriptManager : MonoBehaviour
{
    public GameObject SoundManager;
    private string currentTime = "";
    private int seconds;
    private string[] splitDuration;
    List<int> script_timestamps = new List<int>();
    List<string> scriptListed = new List<string>();
    Text txtRecensione;
    string test;
    int i = 0;
    int j = 0;

    private void populateList(){
        
        script_timestamps.Add(0);
        script_timestamps.Add(3);
        script_timestamps.Add(6);
        script_timestamps.Add(9);
    }



    private string script = "Salve a tutti dal vostro personal sommelier" + Environment.NewLine +
                             "ci troviamo nel cuore del Valpolicella classico" + Environment.NewLine +
                             "e abbiamo nel bicchiere uno dei prodotti" + Environment.NewLine +
                             "che sono il fiore all'occhiello dell'enologia nazionale, una" + Environment.NewLine +
                             "marrone del Valpolicella classico, l'azienda che ce lo" + Environment.NewLine +
                             "presenta. La bonazzi boscaini dei fratelli Giovanni Luigi" + Environment.NewLine +
                             "ci da oggi in gustazione un'annata, 2006.";
    
    private List<string> stringConvert(string baseScript){
        List<string> scriptOutput = new List<string>();
        StringReader sr = new StringReader(baseScript);
        string line;
        while ((line = sr.ReadLine()) != null) {
            scriptOutput.Add(line);
        }
        return scriptOutput;
    }

    private int timestampToSec(int minutesFunc, int secondsFunc){

        int min_from_sec = minutesFunc * 60;
        return min_from_sec + secondsFunc;
    }


    // Start is called before the first frame update
    void Start()
    {        
       populateList();

       txtRecensione = GameObject.Find("Canvas/Panel/Panel/ScrollViewAscolta/Viewport/Content/ScrollViewRecensione/Viewport/Content/txtRecensione").GetComponent<Text>();
        
       txtRecensione.text = (script);
       scriptListed = stringConvert(script);
    }

    // Update is called once per frame
    void Update()
    {
        

        /* **RAGIONAMENTO DELL'ALGORITMO**

            Se il timestamp è dentro un range di secondi della riga, allora carica tutte le righe fino alla riga corrente con
            il tag del cambio colore e carica gli altri senza modifica al tag.
            
        */

        
        currentTime = SoundManager.GetComponent<SoundManager>().getCurrentTime();
        int script_minutes = 0;
        int script_seconds = 0;
        splitDuration = currentTime.Split(':');

        if (splitDuration.Length > 1){
            if (!(String.IsNullOrEmpty(splitDuration[0])) || !(String. IsNullOrEmpty(splitDuration[1]))){
                script_minutes = Int32.Parse(splitDuration[0]);
                script_seconds = Int32.Parse(splitDuration[1]);
            }
        }

       
        if(i < script_timestamps.Count && script_seconds > 0){

            
            if(i==0){
                test = "<color=red>";
                test += Environment.NewLine + scriptListed[i] + Environment.NewLine;
                test += "</color>";
                txtRecensione.text = test;
                ++i;
                
            }else{ 
                
                Debug.Log("Timestamps " + timestampToSec(script_minutes, script_seconds));
                if(timestampToSec(script_minutes, script_seconds) > script_timestamps[i-1] && timestampToSec(script_minutes, script_seconds) < script_timestamps[i] ){
                    test = "<color=red>";
                                    
                                    if(j <= i){
                                        Debug.Log("i: " + i);
                                        Debug.Log("j: " + j);
                                        test += Environment.NewLine + scriptListed[j];
                                        test += "</color>";
                                        txtRecensione.text = test;
                                        ++j;
                                    }
                                    
                                    ++i;
                                        
                }
            }

        
        
        }else{
            i = 0;
            j = 0;
        }

    
        

    }
}