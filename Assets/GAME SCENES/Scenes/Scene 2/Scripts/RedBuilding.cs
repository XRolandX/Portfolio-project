using TMPro;
using UnityEngine;

public class RedBuilding : Building
{
    private readonly float redResProdInterval = 1.33f;
    private readonly float redResCount = 0f;
    private readonly float redResMaxStore = 5f;

    protected string redColor = "red";
    protected string redResourceType = "Red";

    private void Start()
    {
        productionInterval = redResProdInterval;
        currentResourceCount = redResCount;
        maxResourceCount = redResMaxStore;
        resourceColor = redColor;
        resourceType = redResourceType;
        displayResource = ResourceManager.Instance.redResources;
    }

    public override void ProduceResource()
    {
        if (ResourceManager.Instance.redResources.Count < maxResourceCount)
        {
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.redResourcePrefab,
                resSpawnPoint.transform, ResourceManager.Instance.redResources);
            currentResourceCount = ResourceManager.Instance.redResources.Count;
        }
        
    }
    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Red TMP").GetComponent<TextMeshPro>();
    }

    public override void GetResource()
    {
        // doesn't get any resources
    }
}
