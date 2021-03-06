﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEvent : MonoBehaviour
{
	public string ObjectName; 
	public float Delay; 
	public float TimeCount; 
	public Animator ObjectAnimator; 
	public string PressKey; 
	public int AnimationCounter; 
	public Vector3 ObjectLocation; 
    public bool isDay; 
    public bool NightShift; 
    public int AnimationNightCounter; 
    public int NightShiftRequired; 
    public bool Shifted;
    public bool one_time;
    public bool able_at_night;
    public bool limited_resources;
    public int resource_counter;

	private GameObject Object; 

    // Start is called before the first frame update
    void Start()
    {
    	TimeCount = 0; 
    	AnimationCounter = 0; 
        AnimationNightCounter = 0;
    	Object = Instantiate(Resources.Load<GameObject>("Prefabs/"+ ObjectName), ObjectLocation, Quaternion.identity);
        ObjectAnimator = Object.GetComponent<Animator> ();
        Shifted = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        isDay = GameObject.Find("Scriptholder").GetComponent<DayNight>().IsDay;

    	if (Input.GetKeyDown(PressKey) && (isDay || able_at_night))
    	{
    		Interact(); 
    		Shifted = false; 
    	}
        if (!isDay && NightShift && AnimationCounter > NightShiftRequired && !Shifted)
        {
            Result(); 
        }

        
    }

    void Interact()
    {
    	if (Delay == 0)
    	{
            if (one_time)
            {
                ObjectAnimator.SetTrigger("Trigger");
                Debug.Log("BOUNCE");
            }
            else
            {
                if (limited_resources)
                {
                    if (resource_counter > 0)
                    {
                        resource_counter--;
                        ObjectAnimator.SetInteger("States", AnimationCounter);
                        AnimationCounter++;
                    }
                }
                else
                {
                    ObjectAnimator.SetInteger("States", AnimationCounter);
                    AnimationCounter++;
                }
            }
    	}
    	else if (TimeCount < Delay)
    	{
    		TimeCount++; 
    	}
    	else if (TimeCount >= Delay)
    	{
    		ObjectAnimator.SetInteger("States", AnimationCounter); 
    		AnimationCounter ++; 
    	}
    }
    void Result()
    {
        AnimationNightCounter ++; 
        ObjectAnimator.SetInteger("NightStates", AnimationNightCounter);
        Shifted = true; 
    }
}
