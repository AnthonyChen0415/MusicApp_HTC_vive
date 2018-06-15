using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {
    public int selected = 0;
    public int direction = 0;
    public int moving = 0;
    public Animator anim;
    public GameObject[] ins;
    public GameObject[] basement;
    public string[] instrument;
    public bool[] moved;
    public Recognizor rec;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();      
    }
    void AnimationControl(int index_instrument)
    {
        switch (index_instrument)
        {
            case 1:
                anim.Play("Kick_Play", 0,0);
                break;
            case 2:
                anim.Play("Clap_Play", 0,0);
                break;
            case 3:
                anim.Play("Cymbals_Play", 0, 0);
                break;
            case 4:
                anim.Play("Snare_Play", 0, 0);
                break;
            case 5:
                anim.Play("HiHats_Play",0,0);
                break;
            case 6:
                anim.Play("Maraca_Play",0,0);
                break;
            default:
                Debug.Log("Wrong Input! Please Select the instrument first!");
                break;
        }
    }

    /*void Movement(int index_instrument, Vector3 direction)
    {
        float speed = 3f;
        GameObject obj = ins[index_instrument];
        obj.transform.Translate(direction.normalized * Time.deltaTime * speed);
    }*/
    void Movement(int index_instrument)
    {
        float speed = 0.3f;
        GameObject obj = ins[index_instrument];
        Vector3 target = obj.transform.localPosition;
        
        if (rec.up_slide_stage)
        {
            target = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z-0.6f);
            rec.up_slide_stage = false;
        }
        if (rec.down_slide_stage)
        {
            target = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z+0.6f);
            rec.down_slide_stage = false;
        }
        if (rec.left_slide_stage)
        {
            target = new Vector3(obj.transform.localPosition.x+0.6f, obj.transform.localPosition.y, obj.transform.localPosition.z);
            rec.left_slide_stage = false;
        }
        if(rec.right_slide_stage)
        {
            target = new Vector3(obj.transform.localPosition.x-0.6f, obj.transform.localPosition.y, obj.transform.localPosition.z);
            rec.right_slide_stage = false;
        }
        if (target.x < 1.0f) target.x = 1.2f;
        if (target.x > 4.7f) target.x = 4.5f;
        if (target.z < -1.6) target.z = -1.5f;
        if (target.z > 2.15) target.z = 2.1f;
        //obj.GetComponent<Rigidbody>().velocity = (target - obj.transform.localPosition) * speed* Time.deltaTime;
        obj.transform.localPosition = Vector3.MoveTowards(obj.transform.localPosition, target, speed);
    }
    void StopMove(int index)
    {
        GameObject obj = ins[index];
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y-0.4f, obj.transform.position.z);
    }
    void PutBack(int index)
    {
        //float speed = 5f;
        GameObject obj = ins[index];
        GameObject origin = basement[index];
        obj.transform.position = new Vector3(origin.transform.position.x, obj.transform.position.y + 0.4f, origin.transform.position.z);
    }
    void Movement_new(int index, float horizontal, float vertical)
    {
        float speed = 1f;
        GameObject obj = ins[index];
        Vector3 target;
        target = new Vector3((4.8f - (4.8f - 1.5f) * horizontal), obj.transform.localPosition.y, (2f - (2f + 1.4f) * vertical));
        //obj.transform.position = Vector3.MoveTowards(obj.transform.position,target , speed*Time.deltaTime);
        obj.transform.localPosition = Vector3.MoveTowards(obj.transform.localPosition, target, speed * Time.deltaTime);
    }
    void Update()
    {
        float horizontal = -Input.GetAxisRaw("Horizontal");
        float vertical = -Input.GetAxisRaw("Vertical");
        #region 1st controller
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad7))
        {
            selected = 1;
            AnimationControl(selected);
        }
        else
        {
            if (moving != 0 & Input.GetKeyDown(KeyCode.Keypad7))
            {
                Debug.Log("You can't select other instruments while you're moving " + instrument[moving]);
            }
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad4))
        {
            selected = 2;
            AnimationControl(selected);
        }
        else
        {
            if (moving != 0 & Input.GetKeyDown(KeyCode.Keypad4))
            {
                Debug.Log("You can't select other instruments while you're moving " + instrument[moving]);
            }
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad1))
        {
            selected = 3;
            AnimationControl(selected);
        }
        else
        {
            if (moving != 0 & Input.GetKeyDown(KeyCode.Keypad1))
            {
                Debug.Log("You can't select other instruments while you're moving " + instrument[moving]);
            }
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad8))
        {
            selected = 4;
            AnimationControl(selected);
        }
        else
        {
            if (moving != 0 & Input.GetKeyDown(KeyCode.Keypad8))
            {
                Debug.Log("You can't select other instruments while you're moving " + instrument[moving]);
            }
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad5))
        {
            selected = 5;
            AnimationControl(selected);
        }
        else
        {
            if (moving != 0 & Input.GetKeyDown(KeyCode.Keypad5))
            {
                Debug.Log("You can't select other instruments while you're moving " + instrument[moving]);
            }
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad2))
        {
            selected = 6;
            AnimationControl(selected);
        }
        else
        {
            if (moving != 0 & Input.GetKeyDown(KeyCode.Keypad2))
            {
                Debug.Log("You can't select other instruments while you're moving " + instrument[moving]);
            }
        }
        // Movement
        if (selected != 0 & (horizontal !=0 | vertical != 0) & (moving == 0 | moving == selected))
        {
            //Movement(selected, rec.horizontal2, rec.vertical2);
            moving = selected;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) & moving != selected)
            {
                Debug.Log("You can't move this" + instrument[selected] + "while you are moving " + instrument[moving]);
            }
        }
        
        if ((selected != 0) & (moving != 0) & (Input.GetKeyDown(KeyCode.Escape)))
        {
            moved[selected] = true;
            Debug.Log(instrument[selected] + "is at is position");
            //StopMove(selected);
            selected = 0;
            moving = 0;

        }
        if (selected != 0 & moved[selected] & Input.GetKeyDown(KeyCode.Escape))
        {
            PutBack(selected);
            moved[selected] = false;
            Debug.Log(instrument[selected] + "is put back to its original place");
            selected = 0;
            
        }
        #endregion
        #region 2nd controller
        if (moving == 0 & rec.selected_stage[1])
        {
            selected = 1;
            AnimationControl(selected);
            rec.selected_stage[1] = false;
        }
        if (moving == 0 & rec.selected_stage[2])
        {
            selected = 2;
            AnimationControl(selected);
            rec.selected_stage[2] = false;
        }
        if (moving == 0 & rec.selected_stage[3])
        {
            selected = 3;
            AnimationControl(selected);
            rec.selected_stage[3] = false;
        }
        if (moving == 0 & rec.selected_stage[4])
        {
            selected = 4;
            AnimationControl(selected);
            rec.selected_stage[4] = false;
        }
        if (moving == 0 & rec.selected_stage[5])
        {
            selected = 5;
            AnimationControl(selected);
            rec.selected_stage[5] = false;
        }
        if (moving == 0 & rec.selected_stage[6])
        {
            selected = 6;
            AnimationControl(selected);
            rec.selected_stage[6] = false;
        }
        // new move function
        if (selected != 0 & (rec.direct) & (moving == 0 | moving == selected))
        {
            //Movement_new(selected, rec.horizontal, rec.vertical);
            Movement(selected);
            moving = selected;
        }
        else
        {
            if (rec.direct & moving != selected)
            {
                Debug.Log("You can't move this" + instrument[selected] + "while you are moving " + instrument[moving]);
            }
        }


        if ((selected != 0) & (moving != 0) & rec.esc_stage)
        {
            moved[selected] = true;
            Debug.Log(instrument[selected] + "is at is position");
            //StopMove(selected);
            selected = 0;
            moving = 0;
            rec.esc_stage = false;
        }
        if (selected != 0 & moved[selected] & rec.esc_stage)
        {
            PutBack(selected);
            moved[selected] = false;
            Debug.Log(instrument[selected] + "is put back to its original place");
            selected = 0;
            rec.esc_stage = false;
        }
        #endregion
    }
}

