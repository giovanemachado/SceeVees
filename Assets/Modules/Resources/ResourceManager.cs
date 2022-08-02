using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI goldText;
    public TMPro.TextMeshProUGUI rubinText;
    public TMPro.TextMeshProUGUI mineralText;
    public TMPro.TextMeshProUGUI gemText;

    void Awake()
    {
        ResourceService.OnResourcesUpdate += ResourceManagerOnResourcesUpdate;
    }

    void OnDestroy()
    {
        ResourceService.OnResourcesUpdate -= ResourceManagerOnResourcesUpdate;
    }

    void ResourceManagerOnResourcesUpdate()
    {
        foreach (Resource res in ResourceService.resourcesAv)
        {
            if (res.type == Resource.TypeEnum.Gem)
            {
                gemText.text = res.amount.ToString();
            }

            if (res.type == Resource.TypeEnum.Gold)
            {
                goldText.text = res.amount.ToString();
            }

            if (res.type == Resource.TypeEnum.Mineral)
            {
                mineralText.text = res.amount.ToString();
            }

            if (res.type == Resource.TypeEnum.Rubin)
            {
                rubinText.text = res.amount.ToString();
            }
        }
    }
}
