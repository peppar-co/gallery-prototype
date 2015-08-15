using UnityEngine;
using System.Collections;

public class WebCamManager : MonoBehaviour 
{
	// public values


	// private values

	private WebCamTexture _webCamTex;


	private void Awake()
	{
		_webCamTex = new WebCamTexture(800, 600);
		if(_webCamTex.deviceName != "no camera available.")
		{
			gameObject.GetComponent<Renderer>().material.mainTexture = _webCamTex;
			_webCamTex.Play();
			Debug.Log (_webCamTex.deviceName);
		}
	}

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
