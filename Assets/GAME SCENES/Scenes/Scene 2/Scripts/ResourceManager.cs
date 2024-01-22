using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public GameObject resourcePrefab;
    public List<GameObject> redResources;
    public List<GameObject> greenWarehouse;
    public Transform spawnPoint;
    public Transform transitionPoint;
    public readonly float verticalSpacing = 1f;
    public readonly int maxResources = 10;
    private int currentIndex = 0;

public int CurrentIndex { get { return currentIndex; } set { currentIndex = value; }
}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        redResources = new List<GameObject>();
            
    }

    
    public void ResourceInstance()
    {
        if (redResources.Count < maxResources)
        {
            Vector3 newPosition = spawnPoint.position + new Vector3(0, redResources.Count * verticalSpacing, 0);
            GameObject newResource = Instantiate(resourcePrefab, newPosition, spawnPoint.rotation);
            redResources.Add(newResource);
        }
    }

    public void GetLatestResource()
    {
        if (redResources.Count > 0)
        {
            int lastIndex = redResources.Count - 1;

            if (redResources[lastIndex] != null)
            {
                greenWarehouse.Add(redResources[lastIndex]);
                StartCoroutine(TransitionResourceToGreenBuilding(greenWarehouse[^1]));
                redResources.RemoveAt(lastIndex);
            }
            

            

        }
    }

    private IEnumerator TransitionResourceToGreenBuilding(GameObject resource)
    {
        // Implement transition logic here, e.g., moving the resource to the green building
        
        float duration = 1f;
        Vector3 startPosition = resource.transform.position;
        Vector3 endPosition = transitionPoint.position;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            resource.transform.position = Vector3.Lerp(startPosition, endPosition, normalizedTime);
            yield return null;
        }
        
    }
}