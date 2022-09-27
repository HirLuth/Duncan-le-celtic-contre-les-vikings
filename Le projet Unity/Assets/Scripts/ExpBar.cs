using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ExpBar : MonoBehaviour
{
  public Slider slider;
  public Gradient gradient;
  public Image fill;

  public void Update()
  {
    slider.value = PlayerStat.instance.currentExp;
    slider.maxValue = PlayerStat.instance.requiredExp;

    fill.color = gradient.Evaluate(slider.normalizedValue);
  }
}
