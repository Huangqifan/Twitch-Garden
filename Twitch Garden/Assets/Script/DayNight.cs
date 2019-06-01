using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
	public bool IsDay; 
	public float TimetoSwitch; 
	public float TimeLeft; 
	public GameObject DayView; 
	public GameObject NightView; 
    // Start is called before the first frame update
    void Start()
    {
        IsDay = true; 
        TimeLeft = TimetoSwitch; 
    }

    // Update is called once per frame
    void Update()
    {
    	if (TimeLeft > 0)
    	{
     	TimeLeft -= Time.deltaTime;
    	}
    	else
    	{
    		TimeLeft = TimetoSwitch; 
    		IsDay = !IsDay; 
    		if (IsDay)
    		{
    			DayView.SetActive(true); 
    			NightView.SetActive(false); 
    		}
    		if (!IsDay)
    		{
    			DayView.SetActive(false); 
    			NightView.SetActive(true); 
    		}
    	}

        
    }
}
