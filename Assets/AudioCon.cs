using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCon : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deliveryDone;
    public AudioClip slimeAssigned;

    void Awake()
    {
        CollectionService.OnDeliveriesUpdate += CollectionManagerOnDeliveriesUpdate;
    }


    void OnDestroy()
    {
        CollectionService.OnDeliveriesUpdate -= CollectionManagerOnDeliveriesUpdate;
    }

    void CollectionManagerOnDeliveriesUpdate()
    {
        if (CollectionService.collectionsDelivered == 0) return;

        audioSource.PlayOneShot(deliveryDone);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
