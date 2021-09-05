using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTime
{
    public int day;
    public int hour;
    public int minute;
    public int weekday { get { return day % DayTimeManager.WEEKDAYS.Length; } }
    public DayTime(float time)
    {
        day = Mathf.FloorToInt( time / (60 * 24*60));
        time %= (60 * 24*60);
        hour = Mathf.FloorToInt(time / (60*60));
        time %= (60*60);
        minute = Mathf.FloorToInt(time/60);
    }

    public DayTime(int d,int h, int m)
    {
        day = d;
        hour = h;
        minute = m;
    }

    public DayTime diffHour(DayTime time2)
    {
        return new DayTime(0,(hour - time2.hour) ,(minute - time2.minute));
    }
    public int diff(DayTime time2)
    {
        return (hour - time2.hour) * 60 + (minute - time2.minute);
    }
    public static bool operator <(DayTime a, DayTime b)
    {
        return a.diff(b) < 0;
    }
    public static bool operator >(DayTime a, DayTime b)
    {
        return a.diff(b) > 0;
    }

    public static bool operator <(DayTime a, int b)
    {
        return a.diff(new DayTime(0, b, 0)) < 0;
    }

    public static bool operator >(DayTime a, int b)
    {
        return a.diff(new DayTime(0, b, 0)) > 0;
    }
}



public class DayTimeManager : Singleton<DayTimeManager>
{
    static public string[] WEEKDAYS = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Sunday" };
    public int startHour = 4;
    float time;
    public int timeScaleScale = 1;
    int timeScale = 60 * 24 ;//2 minutes in real life= 1 day in game
    public WorldColorController worldColor;
    int lastHour = -1;
    int lastDay = -1;
    public DayTime gameTime;
    // Start is called before the first frame update
    void Start()
    {
        timeScale = timeScale / timeScaleScale;
        time += startHour * 60 * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueUtils.Instance.isInDialogue)
        {
            return;
        }
        time += Time.deltaTime*timeScale;
        gameTime = currentTime();
        var output = JsonUtility.ToJson(gameTime, true);
        //Debug.Log(time + " "+time * timeScale);
        //Debug.Log(output);
        Clock.Instance.updateTime(gameTime.hour, gameTime.minute);
        worldColor.updateTime(gameTime);
        if (lastHour != gameTime.hour)
        {
            EventPool.Trigger("hourChange");
            lastHour = gameTime.hour;
        }
        if (lastDay != gameTime.day)
        {

            EventPool.Trigger("dayChange");
            lastDay = gameTime.day;
        }
    }

    public DayTime currentTime()
    {
        return new DayTime(time);
    }
}
