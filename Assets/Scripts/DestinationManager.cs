using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    [System.Serializable]
    public struct Path
    {
        public Transform[] destinations; // Array of destination points for this path
    }

    public Path[] carPaths; // Array of paths, one for each car

    // Get the path (array of destinations) for a specific car by path index
    public Transform[] GetPath(int pathIndex)
    {
        if (pathIndex >= 0 && pathIndex < carPaths.Length && carPaths[pathIndex].destinations != null)
        {
            return carPaths[pathIndex].destinations;
        }
        Debug.LogWarning("Invalid path index or empty path: " + pathIndex);
        return new Transform[0]; // Return empty array if invalid
    }

    // Get the number of available paths
    public int GetPathCount()
    {
        return carPaths.Length;
    }
}