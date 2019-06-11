using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state { SEED = 0, PLANTED = 1, BLOOMED = 2, WITHERED = 3 };

public class flower: ScriptableObject
{
    private state current_state;

    private GameObject flower_prefab;
    private GameObject flower_obj;

    private Animator flower_animator;


    private float x_pos;
    private float y_pos;
    private GameObject bloom;
    private GameObject bloom_obj;

    public flower()
    {

    }
    public void OnEnable()
    {
        
        flower_prefab = Resources.Load<GameObject>("Prefabs/flower");
       
        x_pos = Random.Range(-5.0f, 11.0f);
        y_pos = Random.Range(-4.0f, 0.0f);
        bloom = Resources.Load<GameObject>("Prefabs/Sound1");

        bloom_obj = Instantiate(bloom);
        flower_obj = Instantiate(flower_prefab, new Vector3(x_pos, y_pos, 0.0f), Quaternion.identity);
        flower_animator = flower_obj.GetComponent<Animator>();

        current_state = state.SEED;
       

    }
    public state getState()
    {
        return current_state;
    }

    public void incState()
    {
        flower_animator.SetInteger("States", flower_animator.GetInteger("States") + 1);
    }
    public void update()
    {
        

    }

    public void update_timer()
    {
        

    }
};