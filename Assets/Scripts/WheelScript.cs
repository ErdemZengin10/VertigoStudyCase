using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelScript : MonoBehaviour
{

     public WheelSettings wheelSettings;
   
    public GameObject currentReward;

    private Rigidbody2D rbody;
    int inRotate;
    
    

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
      
    }

    float t;
    private void Update()
    {           
        
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= wheelSettings.StopPower*Time.deltaTime;

            rbody.angularVelocity =  Mathf.Clamp(rbody.angularVelocity, 0 , 1440);
        }

        if(rbody.angularVelocity == 0 && inRotate == 1) 
        {
            t +=1*Time.deltaTime;
            if(t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                t = 0;
            }
        }
    }


    public void Rotate() 
    {
        if(inRotate == 0)
        {
            rbody.AddTorque(wheelSettings.RotatePower);
            inRotate = 1;
        }
    }



    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 0+22 && rot <= 45+22)
        {
            transform.DORotate(new Vector3(0, 0, 45), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[1].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[1].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[1].gameObject;
            //1
        }
        else if (rot > 45+22 && rot <= 90+22)
        {
            transform.DORotate(new Vector3(0, 0, 90), 0.3f).SetEase(Ease.OutCubic);
            //image2
            Win(UIManager.Instance.wheelImageList[2].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[2].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[2].gameObject;
        }
        else if (rot > 90+22 && rot <= 135+22)
        {
            transform.DORotate(new Vector3(0, 0, 135), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[3].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[3].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[3].gameObject;
            //3
        }
        else if (rot > 135+22 && rot <= 180+22)
        {
            transform.DORotate(new Vector3(0, 0, 180), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[4].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[4].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[4].gameObject;
        }
        else if (rot > 180+22 && rot <= 225+22)
        {
            transform.DORotate(new Vector3(0, 0, 225), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[5].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[5].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[5].gameObject;
            //5
        }
        else if (rot > 225+22 && rot <= 270+22)
        {
            //GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,270);
            transform.DORotate(new Vector3(0, 0, 270), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[6].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[6].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[6].gameObject;
          
            //6
        }
        else if (rot > 270+22 && rot <= 315+22)
        {
            transform.DORotate(new Vector3(0, 0, 315), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[7].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[7].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[7].gameObject;
            
        }
        else if (rot > 315+22 && rot <= 360+22)
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.OutCubic);
            Win(UIManager.Instance.wheelImageList[0].GetComponent<Image>().sprite,UIManager.Instance.wheelImageList[0].GetComponent<ImageSliceScript>().amount);
            currentReward = UIManager.Instance.wheelImageList[0].gameObject;
         
        
        }

        GameManager.Instance.isRotating = false;

        UIManager.Instance.quitButton.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.OutBack);
    }
     public void Win(Sprite currentSprite,int amount)
    {
        if (currentSprite.name == "ui_card_icon_death")
        {
            //Death condition;
            UIManager.Instance.backgroundUI.GetComponent<Image>().color = Color.red;;
            UIManager.Instance.indicator.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            UIManager.Instance.panelWheel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            UIManager.Instance.quitButton.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            UIManager.Instance.spinButton.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                UIManager.Instance.deathScreen.transform.DOScale(new Vector3(15,15,15), 0.3f).SetEase(Ease.OutBack);
                UIManager.Instance.retryButton.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.3f).SetEase(Ease.OutBack);
                UIManager.Instance.mainText.GetComponent<TextMeshProUGUI>().text = "OH NO, BOMB EXPLODED!";
                UIManager.Instance.mainText.GetComponent<TextMeshProUGUI>().color = Color.white;
                UIManager.Instance.quitButton.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);

            });
            
        }
        else
        {
            TimeManager.Instance.transform.DOMoveX(1, 0.5f).OnComplete(() =>
            { 
                GameManager.Instance.CheckSpinCount();
             currentReward.GetComponent<ImageSliceScript>().amount *= 2;
             currentReward.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text ="x"+ currentReward.GetComponent<ImageSliceScript>().amount.ToString();
                var newReward = new Reward(currentSprite, amount);
                GameManager.Instance.rewardList.Add(newReward);
            });
        }
    }
     
}
