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
    public readonly float transitionDuration = 1.0f;

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

    public void GetLatestResource(Transform storePoint, List<GameObject> spawnResources, List<GameObject> storeResources)
    {
        if (spawnResources.Count > 0)
        {
            storeResources.Add(spawnResources[^1]);
            spawnResources.RemoveAt(spawnResources.Count - 1);
            StartCoroutine(TransitionResource(storeResources[^1], storePoint, storeResources));
        }
    }

    private IEnumerator TransitionResource(GameObject resource, Transform storePoint, List<GameObject> storeResources)
    {
        Vector3 startPosition = resource.transform.position;
        Vector3 endPosition = storePoint.position + new Vector3(0, (storeResources.Count - 1) * verticalSpacing, 0);
        Quaternion startRotation = resource.transform.rotation;
        Quaternion endRotation = storePoint.rotation;

        for (float t = 0f; t < transitionDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / transitionDuration;
            resource.transform.SetPositionAndRotation(Vector3.Lerp(startPosition, endPosition, normalizedTime),
                Quaternion.Lerp(startRotation, endRotation, normalizedTime));
            yield return null;
        }

        resource.transform.SetPositionAndRotation(endPosition, endRotation);

    }
}