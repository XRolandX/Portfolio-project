
using System.Collections;
using TMPro;
using UnityEngine;

public class RedBuilding : Building
{
    private void Start()
    {
        resourceProductionRate = 1f;
        productionInterval = 5f;
        resourceColor = "red";
        resourceType = "Red";
    }

    protected override void ProduceResource()
    {

        ResourceManager.Instance.ResourceInstance();
        
    }

    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Red TMP").GetComponent<TextMeshPro>();
    }

    protected override void GetResource()
    {
        
    }
}
