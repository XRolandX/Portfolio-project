using TMPro;
using UnityEngine;

public class GreenBuilding : Building
{
    
    private void Start()
    {
        maxResourceStorage = 5f;
        maxResourceCount = 5f;
        currentResourceCount = 0f;
        currentStorageCount = 0f;
        resourceProductionRate = 1f;
        productionInterval = 2.77f;
        resourceColor = "green";
        resourceType = "Green";
        displayResource = ResourceManager.Instance.greenResources;
    }

    

    

    public override void ProduceResource()
    {

        if (ResourceManager.Instance.greenResources.Count < maxResourceCount && ResourceManager.Instance.RedGreenTransition && ResourceManager.Instance.greenRedWarehouse.Count > 0)
        {
            ResourceManager.Instance.RedGreenTransition = false;
            Destroy(ResourceManager.Instance.greenRedWarehouse[^1]);
            ResourceManager.Instance.greenRedWarehouse.RemoveAt(ResourceManager.Instance.greenRedWarehouse.Count - 1);
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.greenResourcePrefab, resSpawnPoint.transform, ResourceManager.Instance.greenResources);

            currentResourceCount = ResourceManager.Instance.greenResources.Count;
        }
    }
    public override void GetResource()
    {
        if (ResourceManager.Instance.greenRedWarehouse.Count < maxResourceStorage && ResourceManager.Instance.redResources.Count > 0)
        {
            ResourceManager.Instance.RedGreenTransition = true;
            ResourceManager.Instance.GetLatestResource(resStorePoint.transform, ResourceManager.Instance.redResources, ResourceManager.Instance.greenRedWarehouse);
            currentStorageCount = ResourceManager.Instance.greenRedWarehouse.Count;
        }
        
    }


    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Green TMP").GetComponent<TextMeshPro>();
    }
    

    

}
