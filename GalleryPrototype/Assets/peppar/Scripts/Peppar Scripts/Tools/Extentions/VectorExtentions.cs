using UnityEngine;

public static class VectorExtentions
{ 
    public static Vector3 Undefined(this Vector3 instance)
    {
        instance = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);

        return instance;
    }

    public static bool IsUndefined(this Vector3 instance)
    {
        return instance == new Vector3().Undefined();
    }
}
