using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        return tokyoCrawlList.Where(data => data.location == location).ToList();
    }

    public List<string> GetImageUrlListFromTokyoCrawlListFilterByLocation(string location) {
        return FilterTokyoCrawlListByLocation(location).Where(data => data.content_img.Contains("https")).Select(data => data.content_img).ToList();
    }
}
