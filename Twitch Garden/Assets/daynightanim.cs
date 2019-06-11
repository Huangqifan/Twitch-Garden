using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class daynightanim : MonoBehaviour
{
    private DirectoryInfo dir;
    private FileInfo[] info;
    [SerializeField] float time_interval;
    private int cur_sprite_num;
    private float start_time;
    List<GameObject> sprite_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        cur_sprite_num = 0;
        start_time = 0.0f;
        
        Object[] obj = Resources.LoadAll("Prefabs/day_night_prefabs");

        foreach (var v in obj)
        {
           
            sprite_list.Add((GameObject)Instantiate(v));
        }
        
        
        foreach (GameObject s in sprite_list)
        {
            
            var c = s.GetComponent<SpriteRenderer>().color;
            s.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        start_time += Time.deltaTime;
        Debug.Log(cur_sprite_num);
        if (start_time > time_interval)
        {
            //Debug.Log("here");
            start_time = 0.0f;
            cur_sprite_num++;
           
        }
        if (cur_sprite_num == 12)
            cur_sprite_num = 0;
        var c0 = sprite_list[cur_sprite_num].gameObject.GetComponent<SpriteRenderer>().color;
        sprite_list[cur_sprite_num].gameObject.GetComponent<SpriteRenderer>().color = new Color(c0.r, c0.g, c0.b, 1.0f - (start_time / time_interval));

        if (cur_sprite_num == 11)
        {
            
            var c1 = sprite_list[0].gameObject.GetComponent<SpriteRenderer>().color;
            sprite_list[0].gameObject.GetComponent<SpriteRenderer>().color = new Color(c1.r, c1.g, c1.b,  (start_time / time_interval));
        }
        else
        {
            var c1 = sprite_list[cur_sprite_num + 1].gameObject.GetComponent<SpriteRenderer>().color;
            sprite_list[cur_sprite_num + 1].gameObject.GetComponent<SpriteRenderer>().color = new Color(c1.r, c1.g, c1.b,  (start_time / time_interval));
        }
        
    }
}
