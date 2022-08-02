using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionService
{
	public static event Action OnDeliveriesUpdate;
	public static Queue<Collection> collectionsQueue = new Queue<Collection>();
	public static int collectionsDelivered = 0;
	public static int collectionThatExisted = 0;

	public static Collection curColl = null;
	
	public static void StartQueue()
	{
		collectionsQueue.Enqueue(GetRandomCollection());
		collectionsQueue.Enqueue(GetRandomCollection());
		collectionsQueue.Enqueue(GetRandomCollection());
		collectionsQueue.Enqueue(GetRandomCollection());
		collectionsQueue.Enqueue(GetRandomCollection());

		curColl = collectionsQueue.Peek();

		OnDeliveriesUpdate?.Invoke();
    }

	public static void DeliveryCollection()
    {
		Collection colllDelivered = collectionsQueue.Dequeue(); // remove current, or first, whatever

		ResourceService.RemoveResource(Resource.TypeEnum.Gem, colllDelivered.GemQtd);
		ResourceService.RemoveResource(Resource.TypeEnum.Gold, colllDelivered.GoldQtd);
		ResourceService.RemoveResource(Resource.TypeEnum.Mineral, colllDelivered.MineralQtd);
		ResourceService.RemoveResource(Resource.TypeEnum.Rubin, colllDelivered.RubinQtd);

		collectionsQueue.Enqueue(GetRandomCollection()); // add another
		curColl = collectionsQueue.Peek();
		collectionsDelivered++;

		OnDeliveriesUpdate?.Invoke();
	}

	public static void CheckIfCollectionIsReady()
	{
		bool goldOk = false;
		bool gemOk = false;
		bool minOk = false;
		bool ruOk = false;

		foreach (Resource res in ResourceService.resourcesAv)
		{
			if (res.type == Resource.TypeEnum.Gem && res.amount >= curColl.GemQtd)
			{
				gemOk = true;
			}

			if (res.type == Resource.TypeEnum.Gold && res.amount >= curColl.GoldQtd)
			{
				goldOk = true;
			}

			if (res.type == Resource.TypeEnum.Mineral && res.amount >= curColl.MineralQtd)
			{
				minOk = true;
			}

			if (res.type == Resource.TypeEnum.Rubin && res.amount >= curColl.RubinQtd)
			{
				ruOk = true;
			}
		}

		if (gemOk && goldOk && minOk && ruOk)
        {
			DeliveryCollection();
		}
	}

	public static Collection GetRandomCollection()
    {
		collectionThatExisted++;
		if (collectionsDelivered < 5)
		{
			var collN = UnityEngine.Random.Range(0, 2); // 0  1
			var res1 = UnityEngine.Random.Range(10, 31);
			var res2 = UnityEngine.Random.Range(10, 31);
			var res3 = UnityEngine.Random.Range(10, 31);
			var res4 = UnityEngine.Random.Range(10, 31);

			if (collN == 0)
            {
				return new Collection(res1, res2, 0, 0, collectionThatExisted);
			} else
            {
				return new Collection(0, 0, res3, res4, collectionThatExisted);
			}
        } else
        {
			var res1 = UnityEngine.Random.Range(20, 101);
			var res2 = UnityEngine.Random.Range(20, 101);
			var res3 = UnityEngine.Random.Range(20, 101);
			var res4 = UnityEngine.Random.Range(20, 101);

			return new Collection(res1, res2, res3, res4, collectionThatExisted);
		}
    }
}
