using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceService
{
	public static event Action OnResourcesUpdate;

	public static List<Resource> resourcesAv = new List<Resource>
	{
		new Resource(Resource.TypeEnum.Gem, 0),
		new Resource(Resource.TypeEnum.Gold, 0),
		new Resource(Resource.TypeEnum.Mineral, 0),
		new Resource(Resource.TypeEnum.Rubin, 0)
	};

	public static void AddResource(Resource.TypeEnum type, int amount)
	{
		foreach (Resource res in resourcesAv)
        {
			if (res.type == type)
            {
				res.amount += amount;
            }
        }

		OnResourcesUpdate?.Invoke();
	}

	public static void RemoveResource(Resource.TypeEnum type, int amount)
	{
		foreach (Resource res in resourcesAv)
		{
			if (res.type == type)
			{
				res.amount -= amount;
			}
		}

		OnResourcesUpdate?.Invoke();
	}
}
