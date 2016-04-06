using UnityEngine;
using System.Collections;

public class FaceSnapshot : MonoBehaviour
{

    private Texture2D snapshot;
    [SerializeField]
    private Material temporaryFaceMat;

    public GameObject FaceGameObject;
    public Camera GUICamera;
    public Camera VufCamera;
    public GameObject IdleMenu;

    public GameObject BackgroundPlane;
    private Material BGMaterial;


    public Texture2D Snapshot
    {
        get { return snapshot; }
        set { snapshot = value; }
    }

    private void Awake()
    {
        UnityEngine.Assertions.Assert.IsNotNull(FaceGameObject, "Missing ref to face GO");
        UnityEngine.Assertions.Assert.IsNotNull(GUICamera, "Missing ref to GUI CAM");
        UnityEngine.Assertions.Assert.IsNotNull(IdleMenu, "Missing ref to Idle menu");

        //BGMaterial = BackgroundPlane.GetComponent<Renderer>().material;
    }

    public IEnumerator TakeSnapshot(int width, int height)
    {
        //GUICamera.enabled = false;
        //IdleMenu.SetActive(false);

        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();
        snapshot = texture;

        var mainTex = FaceGameObject.GetComponent<Renderer>().material.mainTexture;
        mainTex = snapshot;
        FaceGameObject.GetComponent<Renderer>().material.mainTexture = mainTex;

        //GUICamera.enabled = true;
        //IdleMenu.SetActive(true);
    }

    public void TakeSnapshot()
    {
        StartCoroutine(TakeSnapshot(Screen.width, Screen.height));
    }
}
