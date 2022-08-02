using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceeVeeController : MonoBehaviour
{
    public float zSpeed = 1.0f; //1.0 unit per second

    [HideInInspector] public Position workingInPosition;

    bool moving = false;
    GameObject movingTo = null;
    bool playing = false;

    public GameObject selectionCircle;
    public GameObject audio;
    void Awake()
    {
        FlowManager.OnGameStateChange += SceeVeeControllerOnGameStateChanged;
    }


    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= SceeVeeControllerOnGameStateChanged;
    }


    private void SceeVeeControllerOnGameStateChanged(BaseGameState state)
    {
        playing = state == FlowManager.Instance.PlayState;
    }

    private void Update()
    {
        if (!playing) return;

        if (!moving) return;

        transform.position = Vector3.MoveTowards(transform.position, movingTo.transform.position, 10f * Time.deltaTime);

        float dist = Vector3.Distance(movingTo.transform.position, transform.position);

        if (dist < 0.1)
        {
            FinishMove();
        }
    }

    public void StartMove(GameObject res)
    {
        var d = audio.GetComponent<AudioCon>();
        d.audioSource.PlayOneShot(d.slimeAssigned);
        moving = true;
        movingTo = res;
    }

    void FinishMove()
    {
        moving = false;
        movingTo = null;
    }

    private void OnMouseDown()
    {
        Select(true);
    }

    public void Select() { Select(false); }

    public void Select(bool clearSelection)
    {
        if (SelectionService.SELECTED_UNITS.Contains(this)) return;
        if (clearSelection)
        {
            List<SceeVeeController> selectedUnits = new List<SceeVeeController>(SelectionService.SELECTED_UNITS);
            foreach (SceeVeeController um in selectedUnits)
                um.Deselect();
        }
        SelectionService.SELECTED_UNITS.Add(this);
        selectionCircle.SetActive(true);
    }

    public void Deselect()
    {
        if (!SelectionService.SELECTED_UNITS.Contains(this)) return;
        SelectionService.SELECTED_UNITS.Remove(this);
        selectionCircle.SetActive(false);
    }
}
