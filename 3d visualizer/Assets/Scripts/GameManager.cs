using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null)
                Debug.Log("Game manager is null");
            return _instance;
        }
    }

    public List<TokyoCrawlModel> tokyoCrawlList {get; set;}
    
    private void Awake() {
        _instance = this;
        tokyoCrawlList = new List<TokyoCrawlModel>();
    }

    public List<TokyoCrawlModel> FilterTokyoCrawlListByLocation(string location) {
        return tokyoCrawlList.Where(data => data.location == location);
    }

    // public List<TokyoCrawlModel> GetAreaGroupByTokyoCrawlListLocation(String location) {
        
    // }
}
