using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}


public class DayNight : MonoBehaviour
{
	public bool IsDay; 
	public float TimetoSwitch; 
	public float TimeLeft;
    //public GameObject DayView; 
    //public GameObject NightView; 
    private bool has_watered;
    private bool watering;
    private List<flower> flower_collection;
    private int growth_num;
    private float night_timer;
    

    // Start is called before the first frame update
    void Start()
    {
        IsDay = true; 
        TimeLeft = TimetoSwitch;
        
        growth_num = 0;
        night_timer = 0.0f;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        flower_collection = GameObject.Find("Scriptholder").GetComponent<Commands>().Flowers_collection;
        if (!IsDay && watering)
        {
            night_timer += Time.deltaTime;
            if (night_timer > TimetoSwitch / flower_collection.Count)
            {
                night_timer = 0.0f;
                if (growth_num < flower_collection.Count)
                {
                    flower_collection[growth_num++].incState();

                    Debug.Log("im growing!");
                }
            }
        }

        if (TimeLeft > 0)
    	{
     	TimeLeft -= Time.deltaTime;
    	}
    	else
    	{
    		TimeLeft = TimetoSwitch; 
    		IsDay = !IsDay;
            if (!IsDay)
                on_enter_night();
            else
                on_enter_day();
    		
    	}

        
    }
    void on_enter_night()
    {
        has_watered = GameObject.Find("Scriptholder").GetComponent<Commands>().has_watered;
        Debug.Log("starting night");
        flower_collection.Shuffle();
        if (has_watered)
            watering = true;
    }
    void on_enter_day()
    {
        has_watered = GameObject.Find("Scriptholder").GetComponent<Commands>().has_watered = false;
        Debug.Log("starting day");
        growth_num = 0;
        watering = false;
    }
}
