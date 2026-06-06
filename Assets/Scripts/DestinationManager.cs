using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    [System.Serializable]
    public struct Path
    {
        public Transform[] destinations;  
    }

    public Path[] carPaths;  

     
    public Transform[] GetPath(int pathIndex)
    {
        if (pathIndex >= 0 && pathIndex < carPaths.Length && carPaths[pathIndex].destinations != null)
        {
            return carPaths[pathIndex].destinations;
        }
        //Debug.LogWarning("Invalid path index or empty path: " + pathIndex);
        return new Transform[0]; 
    }

     
    public int GetPathCount()
    {
        return carPaths.Length;
    }
}