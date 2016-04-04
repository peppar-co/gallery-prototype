using UnityEngine;
using System.Collections;

public class CreateTexture : MonoBehaviour
{
    public Texture2D Snapshot;
    public GameObject ControlledGO;

    public IEnumerator TakeSnapshot(int width, int height)
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();
        Snapshot = texture;

        var mainTex = ControlledGO.GetComponent<Renderer>().material.mainTexture;
        mainTex = Snapshot;
        ControlledGO.GetComponent<Renderer>().material.mainTexture = mainTex;
    }


    public void TakeSnapshot()
    {
        StartCoroutine(TakeSnapshot(Screen.width, Screen.height));
    }
}
