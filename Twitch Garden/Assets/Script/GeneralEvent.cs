using System.Collections;
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



	private GameObject Object; 

    // Start is called before the first frame update
    void Start()
    {
    	TimeCount = 0; 
    	AnimationCounter = 0; 
        AnimationNightCounter = 0;
    	Object = Instantiate(Resources.Load<GameObject>("Prefabs/"+ ObjectName), ObjectLocation, Quaternion.identity);
        ObjectAnimator = Object.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        isDay = GameObject.Find("Scriptholder").GetComponent<DayNight>().IsDay;

    	if (Input.GetKeyDown(PressKey) && isDay)
    	{
    		Interact(); 
    	}
        if (!isDay && NightShift && AnimationCounter > NightShiftRequired )
        {
            Result(); 
        }

        
    }

    void Interact()
    {
    	if (Delay == 0)
    	{
    		ObjectAnimator.SetInteger("States",AnimationCounter); 
    		AnimationCounter ++; 
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
    }
}
