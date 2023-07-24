using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene2Setter : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        if (string.Compare(DefaultTrackableEventHandler.getBottiglia(),"Strappelli") == 0)
        {
            Text titolo_Vino = GameObject.Find("Canvas/Panel/Panel/txtTitoloVino").GetComponent<Text>();
            titolo_Vino.text = ("Cantina Strappelli");
            Text sottotitolo_Vino = GameObject.Find("Canvas/Panel/Panel/txtSottotitoloVino").GetComponent<Text>();
            sottotitolo_Vino.text = ("Colline Teramane Montepulciano d'Abruzzo DOCG");
        }
        else
        {
            Text titolo_Vino = GameObject.Find("Canvas/Panel/Panel/txtTitoloVino").GetComponent<Text>();
            titolo_Vino.text = ("Altro Vino");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
