using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
  
using UnityEditor;
 
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CamEffect : MonoBehaviour
{
    public Material material;

    // https://docs.unity3d.com/kr/530/ScriptReference/MonoBehaviour.OnRenderImage.html
    // OnRenderImage is called after all rendering is complete to render image.
    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if(material == null) {
            // https://docs.unity3d.com/kr/560/ScriptReference/Graphics.Blit.html
            // Copies source texture into destination render texture with a shader.
            Graphics.Blit(src, dest);
            return;
        }
        Graphics.Blit(src, dest, material);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
