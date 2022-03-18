using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    Slider Hpslider;
    // Start is called before the first frame update
    void Start()
    {
        Hpslider=GetComponent<Slider>();
        float maxHp = 200f;
        float nowHp = 100f;
        Hpslider.maxValue = maxHp;
        Hpslider.value = nowHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
