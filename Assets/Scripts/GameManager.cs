using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    #region Instance Method //Singleton

    public static GameManager Instance;
    public int spinCount = 0;
    public bool isRotating = false;
    public List<Reward> rewardList = new List<Reward>();
    [SerializeField] private Canvas _canvas;


    private void InstanceMethod()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Awake()
    {
        InstanceMethod();
    }

    #endregion

    [SerializeField] private WheelScript _wheelScript;

    private void Update()
    {
        FireRaycast();
    }

    public void CheckSpinCount()
    { //using mod operation to understand gold or silver spin.
        PanelWheelScaler();
        if (spinCount % 5 == 0)
        {
            if (spinCount % 30 == 0)
            {
                ResetTMP();
              WheelVisualSetter(UIManager.Instance.goldSpriteList,UIManager.Instance.goldIndicator,UIManager.Instance.goldWheel);
            }
            else
            {
                ResetTMP();
                WheelVisualSetter(UIManager.Instance.silverSpriteList,UIManager.Instance.silverIndicator,UIManager.Instance.silverWheel);
             
            }
        }
        else
        {
            WheelVisualSetter(UIManager.Instance.bronzeSpriteList,UIManager.Instance.bronzeIndicator,UIManager.Instance.bronzeWheel);
            ResetTMP();
            SetTMP();
        }
    }

    public void FireRaycast()
    {
        //mouse click options for UI
        if (Input.GetMouseButtonDown(0))
        {
            // Create a new PointerEventData and set its position to the mouse position
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            // Create a list to store the results of the raycast
            List<RaycastResult> results = new List<RaycastResult>();

            // Perform the raycast against the canvas
            GraphicRaycaster raycaster = _canvas.GetComponent<GraphicRaycaster>();
            raycaster.Raycast(pointerEventData, results);

            // Loop through each result and do something with the UI element that was hit
            foreach (RaycastResult result in results)
            {
                // Get a reference to the game object that was hit
                GameObject hitObject = result.gameObject;
                if (hitObject.CompareTag("SpinButton") && !isRotating)
                {
                    isRotating = true;
                    hitObject.transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.2f);
                    _wheelScript.Rotate();
                    UIManager.Instance.quitButton.transform.DOScale(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.InBack);
                    spinCount++;
                    _wheelScript.wheelSettings.RotatePower = Random.Range(800, 2000);
                   _wheelScript.wheelSettings.StopPower = Random.Range(200, 600);
                }
                if (hitObject.CompareTag("QuitButton") && !isRotating)
                {
                    
                    ResetGame();
                    
                }
                if (hitObject.CompareTag("RetryButton") && !isRotating)
                {
                    ResetGame();
                }
                
                
            }
        }
    }

    public void PanelWheelScaler()
    {
        UIManager.Instance.panelWheel.transform.DOScale(new Vector2(0f,0f), 0.3f).OnComplete(() =>
        {
            UIManager.Instance.panelWheel.transform.DOScale(new Vector2(3,3), 0.3f);
        });
        
        UIManager.Instance.indicator.transform.DOScale(new Vector2(0f,0f), 0.3f).OnComplete(() =>
        {
            UIManager.Instance.indicator.transform.DOScale(new Vector2(3f,3f), 0.3f);
        });
        
    }

    public void WheelVisualSetter(List<Sprite> bronzeList,Sprite indicatorSprite,Sprite wheelSprite)
    {
        UIManager.Instance.SetImages(bronzeList);
        UIManager.Instance.SetIndicator(indicatorSprite);
        UIManager.Instance.SetWheel(wheelSprite);
        
        //Sets lists to slices and changing UI elements.
        
    }

    public void ResetGame()
    {
        //resets game
        UIManager.Instance.retryButton.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        UIManager.Instance.deathScreen.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
        UIManager.Instance.mainText.GetComponent<TextMeshProUGUI>().text = "";
        UIManager.Instance.panelWheel.transform.DOScale(new Vector3(3,3,3), 0.3f).SetEase(Ease.OutBack);
        UIManager.Instance.indicator.transform.DOScale(new Vector3(3,3,3), 0.3f).SetEase(Ease.OutBack);
        UIManager.Instance.spinButton.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.3f).SetEase(Ease.OutBack);
        UIManager.Instance.mainText.GetComponent<TextMeshProUGUI>().DOColor(new Color(145, 84, 28, 255), 0.3f);
        UIManager.Instance.backgroundUI.GetComponent<Image>().DOColor(new Color(255, 255, 255, 255), 0.3f);
        
     
        spinCount = 0;
        rewardList.Clear();
        WheelVisualSetter(UIManager.Instance.bronzeSpriteList,UIManager.Instance.bronzeIndicator,UIManager.Instance.bronzeWheel);
        foreach (var image in UIManager.Instance.wheelImageList)
        {
            image.GetComponent<ImageSliceScript>().amount = 2;
            if (image.GetComponent<ImageSliceScript>().image.sprite.name == "ui_card_icon_death")
            {
                image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x2";
            }
            
        }
    }

    public void ResetTMP()
    {
        //resets textmesh
        foreach (var image in UIManager.Instance.wheelImageList)
        {
            image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x2";
            
        }
    }

    public void SetTMP()
    {
        
        //sets textmesh
        foreach (var image in UIManager.Instance.wheelImageList)
        {
            if (image.GetComponent<ImageSliceScript>().image.sprite.name == "ui_card_icon_death")
            {
                image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x"+ image.GetComponent<ImageSliceScript>().amount.ToString();
            }
            
        }
        
    }
}