using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public List<GameObject> redResources = new();
    public List<GameObject> greenRedWarehouse = new();
    public List<GameObject> greenResources = new();
    public List<GameObject> blueRedWarehouse = new();
    public List<GameObject> blueResources = new();
    public List<GameObject> blueGreenWarehouse = new();

    public GameObject redResourcePrefab;
    public GameObject greenResourcePrefab;
    public GameObject blueResourcePrefab;

    public readonly float verticalSpacing = 1f;
    [SerializeField] private float transitionDuration;

    public Transform parentObjectForResourceInstances;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResourceInstance(GameObject resPrefab, Transform spawnPoint, List<GameObject> resources)
    {
        Vector3 newPosition = spawnPoint.position + new Vector3(0, resources.Count * verticalSpacing, 0); // позиція з врахуванням кількості ресурсів у стовбчику
        GameObject newResource = Instantiate(resPrefab, newPosition, spawnPoint.rotation);
        newResource.transform.SetParent(parentObjectForResourceInstances, false); // організуємо усі нові одиниці ресурсів, як дочірні об'єкта на сцені, для зручності
        resources.Add(newResource);
    }

    public void GetLatestResource(Transform storePoint, List<GameObject> spawnResources, List<GameObject> storeResources, Building building)
    {
        if (spawnResources.Count > 0)
        {
            StartCoroutine(TransitionResource(spawnResources[^1], storePoint, storeResources ,building));
            spawnResources.RemoveAt(spawnResources.Count - 1);
        }
    }

    private IEnumerator TransitionResource(GameObject resource, Transform storePoint, List<GameObject> storeResources, Building building)
    {
        Vector3 startPosition = resource.transform.position;
        Quaternion startRotation = resource.transform.rotation;

        float startTime = Time.time;  // початковий час

        while (Time.time - startTime < transitionDuration) // відраховуємо рівно час transitionDuration 
        {
            Vector3 endPosition = storePoint.position + new Vector3(0, storeResources.Count * verticalSpacing, 0);
            Quaternion endRotation = storePoint.rotation;

            float normalizedTime = (Time.time - startTime) / transitionDuration;
            resource.transform.SetPositionAndRotation(
                Vector3.Lerp(startPosition, endPosition, normalizedTime),
                Quaternion.Lerp(startRotation, endRotation, normalizedTime)
            );
            yield return null;
        }

        storeResources.Add(resource);
        building.isResourceInTransition = false;  // завершення переміщення
    }
}