using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LocationViewer : MonoBehaviour
{
    public Image image;
    public List<string> imgUrls = new List<string>();
    public Sprite defaultSpriteImg;

    private int imgViewIdx = 0;

    IEnumerator SetImageFromURL(string url, System.Action<Texture> callback) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            callback(null);
        }
        else
        {
            callback(((DownloadHandlerTexture)www.downloadHandler).texture);
        }
    }

    private Sprite ConvertTextureToSprite(Texture2D srcTexture, float renderWidth, float renderHeight) {
        // return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        int SOURCE_SIZE = 2048;
        Texture2D bgTexture = new Texture2D(SOURCE_SIZE, SOURCE_SIZE);
        Sprite bgSprite = Sprite.Create(bgTexture, new Rect(0, 0, renderWidth, renderHeight), new Vector2(0.5f, 0.5f));
        Color[] srcTextureColor = srcTexture.GetPixels();
        Color[] bgSpriteColor = bgSprite.texture.GetPixels();
        Debug.Log("src len" + srcTextureColor.Length.ToString() + " bg len" + bgSpriteColor.Length.ToString());
        Debug.Log("src " + srcTexture.width.ToString() + " x " + srcTexture.height.ToString());
        Debug.Log("bg " + bgSprite.texture.width.ToString() + " x " + bgSprite.texture.height.ToString());
        int scale = (int)Mathf.Max(Mathf.Min(renderWidth / srcTexture.width, renderHeight / srcTexture.height), 1);
        Debug.Log("scale: " + scale.ToString() + " asdf" + Mathf.Min(renderWidth / srcTexture.width, renderHeight / srcTexture.height).ToString());

        int srcTextureXStartPoint = (int)(renderWidth - srcTexture.width * scale) / 2;
        int srcTextureYStartPoint = (int)(renderHeight - srcTexture.height * scale) / 2;

        Debug.Log("x start" + srcTextureXStartPoint.ToString() + " y " + srcTextureYStartPoint.ToString());
        // for(int i = 0; i < bgSpriteColor.Length; i ++) {
            // bgSpriteColor[i] = Color.black;
        //     int x = i % SOURCE_SIZE;
        //     int y = i / SOURCE_SIZE;
        //     if (srcTextureXStartPoint <= x && x < srcTextureXStartPoint + srcTexture.width
        //         && srcTextureYStartPoint <= y && y < srcTextureYStartPoint + srcTexture.height) {
        //             bgSpriteColor[y * SOURCE_SIZE + x] = srcTextureColor[];
        //         }
        // }
        Color fillColor = Color.clear;
        for(int i = 0; i < bgSpriteColor.Length; i ++) {
            bgSpriteColor[i] = fillColor;
        }
        for(int i = 0; i < srcTextureColor.Length * Mathf.Pow(scale, 2); i ++) {
            int x = i % (srcTexture.width * scale);
            int y = i / (srcTexture.width * scale);
            int bgIdx = (y + srcTextureYStartPoint) * SOURCE_SIZE + srcTextureXStartPoint + x;
            int srcIdx = (int)y / scale * srcTexture.width + (int)x / scale;
            try {
                // if (x < 0 || x >= renderWidth || y < 0 || y >= renderHeight)
                //     continue;
                if (0 <= bgIdx && bgIdx < bgSpriteColor.Length) {
                    bgSpriteColor[bgIdx] = srcTextureColor[srcIdx];
                }
                // 
                // bgSpriteColor[(srcTextureYStartPoint + y) * SOURCE_SIZE + 
                // srcTextureXStartPoint + x] = Color.black;
            } catch (Exception e) {
                Debug.Log("error x " + x.ToString() + " y " + y.ToString() + " bg idx " + bgIdx.ToString() + " src idx " + srcIdx.ToString());
                break;
            }
            
        }
        Debug.Log("pix " + bgSpriteColor[0].ToString());
        // Debug.Log("pix " + bgSpriteColor[srcTextureYStartPoint * SOURCE_SIZE + srcTextureXStartPoint].ToString());
        bgSprite.texture.SetPixels(bgSpriteColor);
        bgSprite.texture.Apply(false);
        return bgSprite;
    }

    public void RenderImage(int idx) {
        if (imgUrls.Count <= 0) {
            imgViewIdx = 0;
            Debug.Log("Empty img url list");
            image.sprite = defaultSpriteImg;
        } else {
            imgViewIdx = idx % imgUrls.Count;
            StartCoroutine(SetImageFromURL(imgUrls[idx % imgUrls.Count], (texture) => {
                if(texture) {
                    float w = image.GetComponent<RectTransform>().rect.width;
                    float h = image.GetComponent<RectTransform>().rect.height;
                    Debug.Log("w: " + w.ToString() + " h: " + h.ToString());
                    image.sprite = ConvertTextureToSprite((Texture2D)texture, w, h);
                }
            }));
        }
    }

    public void RenderNextImg() {
        RenderImage(imgViewIdx + 1);
    }

    public void RenderPrevImg() {
        RenderImage(imgViewIdx - 1 + imgUrls.Count);
    }

    private void Start() {
        // vertical sample
        // https://images.unsplash.com/photo-1526512340740-9217d0159da9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8dmVydGljYWx8ZW58MHx8MHx8&w=1000&q=80
        // horizontal sample
        // https://www.albert.io/blog/wp-content/uploads/2021/07/horizon-1024x550.jpg
        // StartCoroutine(SetImageFromURL("https://images.unsplash.com/photo-1526512340740-9217d0159da9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8dmVydGljYWx8ZW58MHx8MHx8&w=1000&q=80", (texture) => {
        //     if(texture) {
        //         float w = image.GetComponent<RectTransform>().rect.width;
        //         float h = image.GetComponent<RectTransform>().rect.height;
        //         Debug.Log("w: " + w.ToString() + " h: " + h.ToString());
        //         image.sprite = ConvertTextureToSprite((Texture2D)texture, w, h);
        //     }
        // }));
    }

}
