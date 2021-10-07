using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseBar : MonoBehaviour
{
    public Slider slider;

    public void setBar(int phase)
    {
        slider.value = phase;
    }
}
