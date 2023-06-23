using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSliceScript : MonoBehaviour
{
   public int amount;
   public Image image;
   private void Awake()
   {
      image = GetComponent<Image>();

   }
}
