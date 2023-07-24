using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Loader : MonoBehaviour
{
    public string status { get; set; }
    public List<string> head { get; set; }
    public List<List<string>> data { get; set; }
    public int rows { get; set; }


    public void loadData()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://13.51.174.103:6041/rest/sql");
        request.ContentType = "application/json";
        request.Method = "POST";
        request.Headers.Add("Authorization", "Basic " + "cm9vdDp0YW9zZGF0YQ==");
        request.ContentType = "application/json";        
        Stream dataStream = request.GetRequestStream();
        string sql = "select temperature, ts from station1.sensor_data where ts >= NOW - 48h order by ts DESC limit 1";
        byte[] byteArray = Encoding.ASCII.GetBytes(sql);
        dataStream.Write(byteArray, 0, byteArray.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        Scene3Loader tempFogliaLoader = JsonConvert.DeserializeObject<Scene3Loader>(responseString);
        Text txtTemp = GameObject.Find("Canvas/Panel/Panel/ScrollViewOsserva/Viewport/Content/txtTempDB").GetComponent<Text>();
        txtTemp.text = (tempFogliaLoader.data[0][0].ToString().Substring(0, 5) + " °C");
        Text txtUltimoAgg = GameObject.Find("Canvas/Panel/Panel/ScrollViewOsserva/Viewport/Content/txtUltimoAgg").GetComponent<Text>();
        DateTime dataUltimoAgg = DateTime.ParseExact(tempFogliaLoader.data[0][1].ToString().Substring(0, 10), "yyyy-MM-dd" , CultureInfo.InvariantCulture);
        DateTime oraUltimoAgg = DateTime.ParseExact(tempFogliaLoader.data[0][1].ToString().Substring(11, 5), "HH:mm", CultureInfo.InvariantCulture);
        txtUltimoAgg.text = ("Ultimo aggiornamento " + dataUltimoAgg.ToString("dd-MM-yyyy") + " " + oraUltimoAgg.AddHours(2).ToString("HH:mm"));

    }
}
