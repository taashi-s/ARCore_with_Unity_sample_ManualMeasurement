using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using OpenCvSharp.Demo;


public class Coloring : MonoBehaviour
{
    public GameObject canvas;
    public RawImage viewL, viewR;
    UnityEngine.Rect capRect;
    Texture2D capTexture;
    Texture2D colTexture;
    Texture2D binTexture;

    Mat bgr, bin;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Coloring Start ...");
        int scr_w = Screen.width;
        int scr_h = Screen.height;

        // int sx = (int)(scr_w * (0.2));
        // int sy = (int)(scr_h * (0.3));
        // int w = (int)(scr_w * 0.6);
        // int h = (int)(scr_h * 0.4);
        int sx = 160;
        int sy = 384;
        int w = (int)(scr_w - sx * 2);
        int h = (int)(scr_h - sy * 2);

        capRect = new UnityEngine.Rect(sx, sy, w, h);
        capTexture = new Texture2D(w, h, TextureFormat.RGB24, false);
    }

    IEnumerator ImageProcessing()
    {
        Debug.unityLogger.logEnabled = true;

        Debug.LogError("Coloring ImageProcessing in ...");
        canvas.SetActive(false);

        yield return new WaitForEndOfFrame();

        CreateImage();
        ShowImage();

        bgr.Release();
        bin.Release();

        canvas.SetActive(true);
        Debug.LogError("... Coloring ImageProcessing out");
    }

    void CreateImage()
    {
        capTexture.ReadPixels(capRect, 0, 0);
        capTexture.Apply();

        bgr = OpenCvSharp.Unity.TextureToMat(capTexture);
        bin = bgr.CvtColor(ColorConversionCodes.BGR2GRAY);

        bin = bin.Threshold(100, 255, ThresholdTypes.Otsu);
        Cv2.BitwiseNot(bin, bin);
    }

    void ShowImage()
    {
        if (colTexture != null)
        {
            DestroyImmediate(colTexture);
        }
        if (binTexture != null)
        {
            DestroyImmediate(binTexture);
        }

        colTexture = OpenCvSharp.Unity.MatToTexture(bgr);
        binTexture = OpenCvSharp.Unity.MatToTexture(bin);

        viewL.texture = colTexture;
        viewR.texture = binTexture;
    }

    public void StartCV()
    {
        StartCoroutine(ImageProcessing());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
