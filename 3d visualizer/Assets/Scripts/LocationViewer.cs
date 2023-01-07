using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LocationViewer : MonoBehaviour
{
    public Image image;

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

        int srcTextureXStartPoint = (int)(renderWidth - srcTexture.width) / 2;
        int srcTextureYStartPoint = (int)(renderHeight - srcTexture.height) / 2;

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
        for(int i = 0; i < srcTextureColor.Length; i ++) {
            int x = i % srcTexture.width;
            int y = i / srcTexture.width;
            try {
                bgSpriteColor[(y + srcTextureYStartPoint) * SOURCE_SIZE + srcTextureXStartPoint + x] = srcTextureColor[y * srcTexture.width + x];
                // bgSpriteColor[(srcTextureYStartPoint + y) * SOURCE_SIZE + srcTextureXStartPoint + x] = Color.black;
            } catch (Exception e) {
                Debug.Log("x " + x.ToString() + " y " + y.ToString() + " idx " + ((srcTextureYStartPoint + y) * SOURCE_SIZE + srcTextureXStartPoint + x).ToString());
                break;
            }
            
        }
        Debug.Log("pix " + bgSpriteColor[0].ToString());
        Debug.Log("pix " + bgSpriteColor[srcTextureYStartPoint * SOURCE_SIZE + srcTextureXStartPoint].ToString());
        bgSprite.texture.SetPixels(bgSpriteColor);
        bgSprite.texture.Apply(false);
        return bgSprite;
    }

    private void Start() {
        StartCoroutine(SetImageFromURL("https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/drawImage/canvas_drawimage.jpg", (texture) => {
            if(texture) {
                float w = image.GetComponent<RectTransform>().rect.width;
                float h = image.GetComponent<RectTransform>().rect.height;
                Debug.Log("w: " + w.ToString() + " h: " + h.ToString());
                image.sprite = ConvertTextureToSprite((Texture2D)texture, w, h);
            }
        }));
    }

}
