using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterConfig : MonoBehaviour
{
    public List<GameObject> HairList = new List<GameObject>();
    public List<GameObject> PantsList = new List<GameObject>();
    public List<GameObject> ShirtList = new List<GameObject>();

    class CharacterHair
    {
        public int Index;

        public void Next()
        {

        }

        public void Prev()
        {

        }
    }

    class CharacterShirt
    {
        public int Index;

        public void Next()
        {

        }

        public void Prev()
        {

        }
    }

    class CharacterPants
    {
        public int Index;

        public void Next()
        {

        }

        public void Prev()
        {

        }
    }

    class CharacterFace
    {
        public GameObject FaceGameObject;
        Material FaceMaterial;

        void AddFace(Texture2D faceTexture)
        {
            FaceMaterial.mainTexture = faceTexture;
            FaceGameObject.GetComponent<Renderer>().material = FaceMaterial;
        }


    }
}
