using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    private bool playing = false;
    public GameObject[] collectionsINScreen;
    public GameObject deliveryHighAn;
    public GameObject deliveryTextHighAn;
    public GameObject deliveryDoneText;

    public TMPro.TextMeshProUGUI deliveriesDone;

    Coroutine cor;

    void Awake()
    {
        FlowManager.OnGameStateChange += CollectionManagerOnGameStateChanged;
        CollectionService.OnDeliveriesUpdate += CollectionManagerOnDeliveriesUpdate;
    }

    private void Start()
    {
        CollectionService.StartQueue();
    }

    private void Update()
    {
        if (!playing) return;

        CollectionService.CheckIfCollectionIsReady();
    }

    private void CollectionManagerOnDeliveriesUpdate()
    {
        var collections = CollectionService.collectionsQueue.ToArray();

        for (int i = 0; i < 5; i++)
        {
            var collectionController = collectionsINScreen[i].GetComponent<CollectionController>();
 
            collectionController.title.text = "Delivery n - " + collections[i].id.ToString();
            collectionController.gemText.text = collections[i].GemQtd.ToString();
            collectionController.goldText.text = collections[i].GoldQtd.ToString();
            collectionController.minText.text = collections[i].MineralQtd.ToString();
            collectionController.rubinText.text = collections[i].RubinQtd.ToString();
        }

        deliveriesDone.text = "Total deliveries done: " + CollectionService.collectionsDelivered;

        if (cor != null)
        {

            StopCoroutine(cor);
        }
        cor = StartCoroutine(DeliveryHighlightAnim());
    }

    IEnumerator DeliveryHighlightAnim()
    {
            deliveryHighAn.SetActive(true);

            if (CollectionService.collectionsDelivered != 0)
            {
                deliveryDoneText.SetActive(true);
            }

            deliveryTextHighAn.SetActive(true);
            yield return new WaitForSeconds(3);
            deliveryHighAn.SetActive(false);
            deliveryTextHighAn.SetActive(false);
            deliveryDoneText.SetActive(false);
    }

    private void CollectionManagerOnGameStateChanged(BaseGameState state)
    {
        playing = state == FlowManager.Instance.PlayState;
    }

    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= CollectionManagerOnGameStateChanged;
        CollectionService.OnDeliveriesUpdate -= CollectionManagerOnDeliveriesUpdate;
    }
}