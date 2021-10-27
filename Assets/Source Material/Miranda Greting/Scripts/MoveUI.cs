using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MoveUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform movableObject;
    private Vector3 offset;
    private float zCoord;
    private Vector2 startPos;
    private Vector2 currentPos;
    private List <Vector2> previousPositions;
    [SerializeField] private GameObject[] scalableObjects;
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private GameObject rebindingMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private InputAction pause;
    [SerializeField] private GameObject editModePanel;


    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    void Start()
    {
        startPos = movableObject.anchoredPosition;
        currentPos = startPos;
        previousPositions = new List<Vector2>();
        previousPositions.Add(currentPos);
        scaleSlider.onValueChanged.AddListener(delegate { ScaleUI(); });
        rebindingMenu.SetActive(false);
        pause.performed += _ => TogglePauseMenu();
        editModePanel.SetActive(false);
        pauseMenu.SetActive(false);
        uiMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTransform();
        }


        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            UndoTransform();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            //ToggleMenu();
        }

    }

    private void ResetTransform()
    {
        movableObject.anchoredPosition = startPos;
    }

    private void UndoTransform()
    {
        if (currentPos == previousPositions[previousPositions.Count - 1] && currentPos != previousPositions[0])
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
            movableObject.anchoredPosition = previousPositions[previousPositions.Count - 1];
            currentPos = previousPositions[previousPositions.Count - 1];
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        movableObject.anchoredPosition += eventData.delta;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        currentPos = movableObject.anchoredPosition;
        previousPositions.Add(currentPos);
        Debug.Log("AddedPosCurrent");
    }

    public void ActivateEditMode()
    {
        editModePanel.SetActive(true);
    }

    private void ScaleUI()
    {
        foreach(GameObject gameObject in scalableObjects)
        {
            gameObject.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, 1);
        }
    }

    public void ChangeAnchoredPos(string buttonPos)
    {
        foreach (GameObject gameObject in scalableObjects)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 rectMinMax = rectTransform.anchorMin;


            if (buttonPos.Equals("UpLeft"))
            {
                rectMinMax = new Vector2(0, 1);
            }
            else if (buttonPos.Equals("UpMid"))
            {
                rectMinMax = new Vector2(0.5f, 1);
            }
            else if(buttonPos.Equals("UpRight"))
            {
                rectMinMax = new Vector2(1, 1);
                rectTransform.anchoredPosition = new Vector2(-rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            }

            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;

        }
    }

    public void ToggleRebindMenu()
    {
        if (rebindingMenu.activeInHierarchy)
        {
            rebindingMenu.SetActive(false);
        }
        else
        {
            rebindingMenu.SetActive(true);
        }
        //rebindingMenu.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ToggleUIMenu()
    {
        if (uiMenu.activeInHierarchy)
        {
            uiMenu.SetActive(false);
            editModePanel.SetActive(false);
        }
        else
        {
            uiMenu.SetActive(true);
            editModePanel.SetActive(true);
        }
    }

}
