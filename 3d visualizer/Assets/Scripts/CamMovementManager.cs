using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovementManager : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/RangeAttribute.html
    [Range(0.5f, 3)]
    public float movementSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -2.3 <= x <= 1.25
        // -0.6 <= y <= 3.3
        // https://docs.unity3d.com/kr/2021.3/ScriptReference/KeyCode.html
        if(Input.GetKey(KeyCode.W)) {
            // https://docs.unity3d.com/kr/530/ScriptReference/Mathf.Clamp.html
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x, 
                Mathf.Clamp(gameObject.transform.position.y + movementSpeed * Time.deltaTime, -0.6f, 3.3f),
                gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A)) {
            gameObject.transform.position = new Vector3(
                Mathf.Clamp(gameObject.transform.position.x - movementSpeed * Time.deltaTime, -2.3f, 1.25f),
                gameObject.transform.position.y, 
                gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S)) {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x, 
                Mathf.Clamp(gameObject.transform.position.y - movementSpeed * Time.deltaTime, -0.6f, 3.3f),
                gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D)) {
            gameObject.transform.position = new Vector3(
                Mathf.Clamp(gameObject.transform.position.x + movementSpeed * Time.deltaTime, -2.3f, 1.25f), 
                gameObject.transform.position.y, 
                gameObject.transform.position.z);
        }
    }
}
