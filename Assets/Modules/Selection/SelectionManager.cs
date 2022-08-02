using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    bool isSelecting = false;
    Vector3 mousePosition1;
    GameObject[] selectableUnits;
    Ray _ray;
    RaycastHit2D _raycastHit;
    bool playing = false;

    void Awake()
    {
        FlowManager.OnGameStateChange += SelectionManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= SelectionManagerOnGameStateChanged;
    }

    void SelectionManagerOnGameStateChanged(BaseGameState state)
    {
        playing = state == FlowManager.Instance.PlayState;
    }

    private void Start()
    {
        selectableUnits = GameObject.FindGameObjectsWithTag("SceeVee");
    }

    void Update()
    {
        if (!playing) return;

        // If we press the left mouse button, save mouse location and begin selection
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }

        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
        }
        float dist = Vector3.Distance(mousePosition1, Input.mousePosition);

        if (isSelecting && dist > 10)
        {
            SelectUnitsInDraggingBox();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _raycastHit = Physics2D.GetRayIntersection(_ray);
            if (_raycastHit.collider)
            {
                if (_raycastHit.collider.CompareTag("Terrain"))
                {
                    _DeselectAllUnits();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _raycastHit = Physics2D.GetRayIntersection(_ray);
            if (_raycastHit.collider)
            {
                if (_raycastHit.collider.CompareTag("Resource"))
                {
                    var resourCon = _raycastHit.collider.GetComponent<ResourceController>();
                    foreach (SceeVeeController sceeVee in SelectionService.SELECTED_UNITS)
                    {
                        if (sceeVee.workingInPosition == null)
                        {
                            var newPosToWork = resourCon.GetFreePosition();
                            newPosToWork.inUse = true;
                            sceeVee.workingInPosition = newPosToWork;
                            sceeVee.StartMove(newPosToWork.realPos);
                        }

                        if (sceeVee.workingInPosition.resource != resourCon.type)
                        {
                            sceeVee.workingInPosition.inUse = false;
                            sceeVee.workingInPosition = null;

                            var newPosToWork = resourCon.GetFreePosition();
                            newPosToWork.inUse = true;
                            sceeVee.workingInPosition = newPosToWork; 
                            sceeVee.StartMove(newPosToWork.realPos);
                        }
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = SelectionService.GetScreenRect(mousePosition1, Input.mousePosition);

            SelectionService.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            SelectionService.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public void SelectUnitsInDraggingBox()
    {
        Bounds selectionBounds = SelectionService.GetViewportBounds(
            Camera.main,
            mousePosition1,
            Input.mousePosition
        );

        bool inBounds;

        foreach (GameObject unit in selectableUnits)
        {
            inBounds = selectionBounds.Contains(
                Camera.main.WorldToViewportPoint(unit.transform.position)
            );

            if (inBounds)
            {
                unit.GetComponent<SceeVeeController>().Select();
            }
            else
            {
                unit.GetComponent<SceeVeeController>().Deselect();
            }
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            SelectionService.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }

    public void _DeselectAllUnits()
    {
        if (SelectionService.SELECTED_UNITS.Count > 0)
        {
            List<SceeVeeController> selectedUnits = new List<SceeVeeController>(SelectionService.SELECTED_UNITS);
            foreach (SceeVeeController um in selectedUnits)
                um.Deselect();
        }
    }
}
