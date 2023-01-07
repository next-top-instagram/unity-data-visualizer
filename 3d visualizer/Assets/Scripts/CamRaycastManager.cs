using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CamRaycastManager : MonoBehaviour
{
    public Camera camera;
    public LocationViewer locationViewer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // https://docs.unity3d.com/ScriptReference/Input-mousePosition.html
        // Input.mousePosition

        // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        RaycastHit hit;
        // https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html
        Ray ray = camera.ScreenPointToRay (Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);
        // https://docs.unity3d.com/Manual/CameraRays.html
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit)) {
            // https://en.wikipedia.org/wiki/Short-circuit_evaluation
            // if and op return false, stop expression
            // if or op return true, stop expression

            Debug.Log("hit: " + hit.transform.name + ", " + hit.transform.tag + ", " + hit.transform.CompareTag("Area") + ", " + hit.transform.position.ToString());
            // List<string> test = GameManager.Instance.GetImageUrlListFromTokyoCrawlListFilterByLocation(hit.transform.name);
            locationViewer.imgUrls = GameManager.Instance.GetImageUrlListFromTokyoCrawlListFilterByLocation(hit.transform.name);
            locationViewer.RenderImage(0);

            // foreach (var item in test)
            // {
            //     Debug.Log(" url: " + item);
            // }
            gameObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 1, gameObject.transform.position.z);
        }
    }
}
