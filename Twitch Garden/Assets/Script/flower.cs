using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state { SEED = 0, PLANTED = 1, BLOOMED = 2, WITHERED = 3 };

public class flower: ScriptableObject
{
    private state current_state;

    private GameObject water_timer_prefab;
    private GameObject water_timer_obj;
    private GameObject flower_prefab;
    private GameObject flower_obj;
    private GameObject water_level_prefab;
    private GameObject water_level_obj;
    private water_level_controller water_script;
    private GameObject water_bucket_prefab;
    private GameObject water_bucket_obj;
    private GameObject text_prefab;
    private GameObject text_obj;

    private Animator flower_animator;

    private float max_water_time;
    private float time;
    private float x_pos;
    private float y_pos;
    private int water_level;
    private int id;
    public bool to_destroy;
    private GameObject bloom;
    private GameObject bloom_obj;

    public flower()
    {

    }
    public void OnEnable()
    {
        water_timer_prefab = Resources.Load<GameObject>("Prefabs/timer_");
        water_level_prefab = Resources.Load<GameObject>("Prefabs/water_level");
        water_bucket_prefab = Resources.Load<GameObject>("Prefabs/water_bucket");
        flower_prefab = Resources.Load<GameObject>("Prefabs/flower");
        text_prefab = Resources.Load<GameObject>("Prefabs/text");
        x_pos = Random.Range(-8.0f, 11.0f);
        y_pos = Random.Range(-4.0f, 0.0f);
        bloom = Resources.Load<GameObject>("Prefabs/Sound1");

        bloom_obj = Instantiate(bloom);
        flower_obj = Instantiate(flower_prefab, new Vector3(x_pos, y_pos, -0.5f), Quaternion.identity);
        flower_animator = flower_obj.GetComponent<Animator>();

        current_state = state.SEED;
        time = -1.0f;

        max_water_time = 30.0f;
        to_destroy = false;

    }
    public state getState()
    {
        return current_state;
    }

    public void setState(state s)
    {
        if (s == state.PLANTED)
        {
            flower_animator.SetInteger("grow_flower", 1);
            text_obj = Instantiate(text_prefab, new Vector3(flower_obj.transform.position.x - 2.25f, flower_obj.transform.position.y + 2.0f,
                                          flower_obj.transform.position.z), Quaternion.identity);
            text_obj.GetComponent<TextMesh>().text = id.ToString();
            time = 0.0f;
            water_timer_obj = Instantiate(water_timer_prefab, new Vector3(flower_obj.transform.position.x, flower_obj.transform.position.y - 2.0f,
                                         flower_obj.transform.position.z), Quaternion.identity);
            water_level_obj = Instantiate(water_level_prefab, new Vector3(flower_obj.transform.position.x, flower_obj.transform.position.y - 2.5f,
                                          flower_obj.transform.position.z), Quaternion.identity);
            water_script = water_level_obj.GetComponent<water_level_controller>();
        }
        if (s == state.BLOOMED)
        {
            flower_animator.SetInteger("grow_flower", 2);
            bloom_obj.GetComponent<AudioSource>().volume=0.5f;
            bloom_obj.GetComponent<AudioSource>().Play(0);
        }
        //if (s == state.WITHERED)
            //flower_animator.SetTrigger("withered");
         
        current_state = s;
    }
    public void setId(int i)
    {
        id = i+1;
    }

    public int getId()
    {
        return id;
    }

    public float getTime()
    {
        return time;
    }
    public void setTime(float f)
    {
        time = f;
    }

    public void increment_water_level()
    {
        Debug.Log(water_level);
        water_script.increment();

        water_level++;


        if (water_level == 5)
        {
            this.setState(state.BLOOMED);
            Destroy(water_timer_obj);
            Destroy(water_level_obj);
            water_bucket_obj = Instantiate(water_bucket_prefab, new Vector3(flower_obj.transform.position.x + 2.0f, flower_obj.transform.position.y +2.5f,
                                          flower_obj.transform.position.z), Quaternion.identity);
            
            time = -2.0f;
        }
        else
        {
            time = 0.0f;
            water_timer_obj.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public void decrement_water_level()
    {
        water_script.decrement();
        water_level--;
    }
    public int get_water_level()
    {
        return water_level;
    }
    public void update()
    {
        if (time >= 0.0f)
        {
            update_timer();
        }
        else if(time <= -1.0f)
        {
            time += Time.deltaTime;
        }
        else
        {
            if(current_state == state.BLOOMED)
            {
                Destroy(water_bucket_obj);
            }
        }

    }

    public void update_timer()
    {
        if (time >= max_water_time)
        {
            if (water_level == 0)
            {
                Destroy(water_timer_obj);
                Destroy(water_level_obj);
                time = -1.0f;
                to_destroy = true;
                Destroy(flower_obj);
                Destroy(text_obj);
            }
            else
            {
                decrement_water_level();
                time = 0.0f;
                water_timer_obj.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            time += Time.deltaTime;
            water_timer_obj.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(((max_water_time - time) / max_water_time) + 0.01f, 1.0f, 1.0f);
        }

    }
};