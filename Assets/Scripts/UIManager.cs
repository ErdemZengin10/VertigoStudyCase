using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SpriteAtlas spriteAtlas;
    public List<GameObject> wheelImageList;
    public List<Sprite> bronzeSpriteList;
    public List<Sprite> silverSpriteList;
    public List<Sprite> goldSpriteList;
    public Sprite bronzeIndicator;
    public Sprite silverIndicator;
    public Sprite goldIndicator;
    public Sprite bronzeWheel;
    public Sprite silverWheel;
    public Sprite goldWheel;
    public GameObject panelWheel;
    public GameObject wheelIndicator;
    public GameObject quitButton;
    public GameObject indicator;
    public GameObject spinButton;
    public GameObject backgroundUI;
    public GameObject deathScreen;
    public GameObject retryButton;
    public GameObject mainText;
    
    

    private int i = 0;

    #region Instance Method //Singleton

    public static UIManager Instance;

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
        wheelImageList = GameObject.FindGameObjectsWithTag("SliceImage").ToList();
        panelWheel = GameObject.FindGameObjectWithTag("WheelPanel");
        DeclareSpritesFromAtlas();
        
    }

    #endregion

    void Start()
    {
        SetImages(bronzeSpriteList);
        quitButton.transform.localScale = new Vector3(0, 0, 0);

    }

    public void SetImages(List<Sprite> imageList)
    {
        foreach (var image in wheelImageList)
        {
            //Iterates each image for setting.
            
            
            image.GetComponent<ImageSliceScript>().image.sprite = imageList[i];
            i++;
            if (image.GetComponent<ImageSliceScript>().image.sprite.name == "ui_card_icon_death")
            {
                image.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        i = 0;
    }

    public void SetIndicator(Sprite indicator)
    {
        wheelIndicator.GetComponent<Image>().sprite = indicator;
        

    }

    public void SetWheel(Sprite wheel)
    {
        panelWheel.GetComponent<Image>().sprite = wheel;
    }

    public void DeclareSpritesFromAtlas()
    {
        
        //Getting sprites from atlas.
        bronzeIndicator = spriteAtlas.GetSprite("ui_spin_bronze_indicator");
        silverIndicator = spriteAtlas.GetSprite("ui_spin_silver_indicator");
        goldIndicator = spriteAtlas.GetSprite("ui_spin_golden_indicator");
        
        bronzeWheel = spriteAtlas.GetSprite("ui_spin_bronze_base");
        silverWheel = spriteAtlas.GetSprite("ui_spin_silver_base");
        goldWheel = spriteAtlas.GetSprite("ui_spin_golden_base");

    }
}