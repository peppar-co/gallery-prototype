using UnityEngine;
using System.Collections;

public class ItemMaterialController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _materialTransforms;

    public void SetMaterial(Material material)
    {
        foreach(var materialTransform in _materialTransforms)
        {
            materialTransform.GetComponent<MeshRenderer>().material = material;
        }
    }
}
