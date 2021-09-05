using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateViewController : MonoBehaviour
{
    public TMP_Text dateText;
    public TMP_Text weekdayText;
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("dayChange", dayChange);
    }
    void dayChange()
    {
        DayTime time = DayTimeManager.Instance.gameTime;
        int date = time.day;
        dateText.text = "Day " + (date + 1).ToString();
        weekdayText.text = DayTimeManager.WEEKDAYS[time.weekday];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
