using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Net;
using System.IO;
using System.Globalization;

public class DataLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // CsvReader
        // CsvHelper.CsvReader

        // var csv = new CsvReader( null );
        // var myCustomTypeList = csv.GetRecords<MyCustomType>();
        GetCSVAndParsing("https://next-top-instagram.github.io/python-data-crawler/go-tokyo-crawling-result-with-content-detail.csv");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCSVAndParsing(string url)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
        using (CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture)) {
            // csv.ReadHeader();
            csv.Read();
            TokyoCrawlModel model = csv.GetRecord<TokyoCrawlModel>();
            Debug.Log("test: " + csv.GetField<string>(4) + ", " + model.location + ", " + model.area + ", " + model.category);
            Debug.Log("loaded csv len: " + csv.ColumnCount.ToString());
            csv.Read();
            Debug.Log("test: " + csv.GetField<string>(4));
            Debug.Log("loaded csv len: " + csv.ColumnCount.ToString());
        }
    } 
}
