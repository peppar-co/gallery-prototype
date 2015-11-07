using UnityEngine;
using System.Collections;


public class WorldObjectManager : MonoBehaviour
{
    [SerializeField]
    Transform child;

    [SerializeField]
    public bool GotHit = false;

    [SerializeField]
    float speed = 10;


    // Update is called once per frame
    void Update()
    {
        if (GotHit)
        {
            child.transform.Rotate(Vector3.up, speed);
        }
        else
        {
            child.transform.Rotate(Vector3.up, 0f);
        }
    }
}
