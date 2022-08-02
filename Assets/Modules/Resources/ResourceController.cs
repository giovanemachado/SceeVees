using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    List<Position> positionsInResource = new List<Position>();
    public GameObject[] positions;

    public Resource.TypeEnum type;

    bool playing = false;

    int interval = 1;
    float nextTime = 0;

    GameObject[] workers;
    void Awake()
    {
        FlowManager.OnGameStateChange += ResourceControllerOnGameStateChanged;
        foreach(GameObject pos in positions)
        {
            positionsInResource.Add(new Position(type, false, pos));
        }
    }


    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= ResourceControllerOnGameStateChanged;
    }


    private void Start()
    {
        workers = GameObject.FindGameObjectsWithTag("SceeVee");
    }

    void Update()
    {
        if (!playing) return;

        if (Time.time >= nextTime)
        {
            foreach (GameObject worker in workers)
            {
                float dist = Vector3.Distance(transform.position, worker.transform.position);

                if (dist < 1.81f)
                {
                    ResourceService.AddResource(type, 1);
                }
            }

            nextTime += interval;
        }
    }

    public Position GetFreePosition()
    {
        return positionsInResource.Where(pos => pos.inUse == false).FirstOrDefault();
    }

    private void ResourceControllerOnGameStateChanged(BaseGameState state)
    {
        playing = state == FlowManager.Instance.PlayState;
    }
}
