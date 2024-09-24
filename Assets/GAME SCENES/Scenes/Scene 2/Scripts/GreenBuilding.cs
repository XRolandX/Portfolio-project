public class GreenBuilding : Building
{
    private readonly float maxRedWarehouseStorage = 5f;

    protected override void InitializeBuilding()
    {
        resourceColor = "green";
        resourceType = "Green";
        resourceCountDisplay = ResourceManager.Instance.greenResources;
        produceTimeElapsed = productionResourceInterval;
        gettingTimeElapsed = gettingResourceInterval;
        isResourceInTransition = false;
    }

    protected override void GetResource()
    {
        if (!isResourceInTransition) 
        {
            // Використовуємо спільний метод для перенесення ресурсу
            TransferResource(redResStorePoint, ResourceManager.Instance.redResources, ResourceManager.Instance.greenRedWarehouse, maxRedWarehouseStorage, this);
        }
        
    }

    protected override void ProduceResource()
    {
        if (ResourceManager.Instance.greenResources.Count < maxResourceCount && ResourceManager.Instance.greenRedWarehouse.Count > 0)
        {
            // Використовуємо спільний метод для знищення одного ресурсу
            DestroyResources(ResourceManager.Instance.greenRedWarehouse, 1);
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.greenResourcePrefab, resSpawnPoint.transform, ResourceManager.Instance.greenResources);
        }
    }
}
