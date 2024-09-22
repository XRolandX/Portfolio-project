public class RedBuilding : Building
{
    private readonly float redResProdInterval = 1.33f;
    private readonly float redResMaxStore = 5f;

    protected string redColor = "red";
    protected string redResourceType = "Red";

    private void Start()
    {
        productionInterval = redResProdInterval;
        maxResourceCount = redResMaxStore;

        resourceColor = redColor;
        resourceType = redResourceType;
        resourceCountDisplay = ResourceManager.Instance.redResources;
    }

    public override void ProduceResource()
    {
        if (ResourceManager.Instance.redResources.Count < maxResourceCount)
        {
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.redResourcePrefab,
                resSpawnPoint.transform, ResourceManager.Instance.redResources);
        }
        
    }
    public override void GetResource()
    {
        // null
    }
}
