using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColorController : MonoBehaviour
{
    int startDarkHour = 18;
    int startLightHour = 5;
    int changeHours = 2;
    public Gradient morningGradient;
    public Gradient nightGradient;
    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTime(DayTime dayTime)
    {
        if(dayTime> startLightHour&&dayTime<startDarkHour)
        {
            int diff = dayTime.diff(new DayTime(0, startLightHour,0));
            float value = Mathf.Clamp((float)diff / (float)(changeHours * 60), 0, 1);
            renderer.color = morningGradient.Evaluate(value);
        }else if(dayTime > startDarkHour)
        {

            int diff = dayTime.diff(new DayTime(0, startDarkHour, 0));
            float value = Mathf.Clamp((float)diff / (float)(changeHours * 60), 0, 1);
            renderer.color = nightGradient.Evaluate(value);
        }

    }
}
