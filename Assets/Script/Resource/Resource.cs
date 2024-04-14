using UnityEngine;

public class Resource : MonoBehaviour
{
    private int _resourceIndex;

    public int GetResourceIndex()
    {
        return _resourceIndex;
    }

    public void AssignResourceIndex(int resourceIndex)
    {
        _resourceIndex = resourceIndex;
    }
}
