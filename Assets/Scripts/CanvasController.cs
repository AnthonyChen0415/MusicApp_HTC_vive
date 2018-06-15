using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    #region variable
    public Animator anim;
    public int selected = 0;
    public int selected2 = 0;
    public int direction = 0;
    public int moving = 0;
    public string[] instrument;
    public bool[] moved;
    public GameObject[] ins;
    public GameObject[] basement;
    private bool flag;
    public Recognizor rec;
    public bool[] select;
    [System.Serializable]
    public class musicAttribute
    {
        public float volume;
        public int complex;
    }
    public musicAttribute[] ma;
    #endregion
    // Use this for initialization
    #region initialization
    void Start()
    {
        flag = false;
        anim = GetComponent<Animator>();
        for (int i = 0;i<=6; i++)
        {
            ma[i].complex = 0;
            ma[i].volume = 0f;
        }
    }
    #endregion
    #region Methods
    void AnimationControl(int index_instrument)
    {
        switch (index_instrument)
        {
            case 1:
                anim.Play("Kick_2D", 0, 0);
                break;
            case 2:
                anim.Play("Clap_2D", 0, 0);
                break;
            case 3:
                anim.Play("Cymbals_2D", 0, 0);
                break;
            case 4:
                anim.Play("Snare_2D", 0, 0);
                break;
            case 5:
                anim.Play("HiHats_2D");
                break;
            case 6:
                anim.Play("Maraca_2D");
                break;
            default:
                Debug.Log("Wrong Input! Please Select the instrument first!");
                break;
        }
    }
    void Movement(int index_instrument)
    {
        float speed = 0.2f;
        GameObject obj = ins[index_instrument];
        Vector2 target = obj.transform.localPosition;
        if (rec.up_slide_canvas)
        {
            target = new Vector2(obj.transform.localPosition.x, obj.transform.localPosition.y+0.14f);
            rec.up_slide_canvas = false;
        }
        if (rec.down_slide_canvas)
        {
            target = new Vector2(obj.transform.localPosition.x, obj.transform.localPosition.y-0.14f);
            rec.down_slide_canvas = false;
        }
        if (rec.left_slide_canvas)
        {
            target = new Vector2(obj.transform.localPosition.x - 0.11f, obj.transform.localPosition.y);
            rec.left_slide_canvas = false;
        }
        if (rec.right_slide_canvas)
        {
            target = new Vector2(obj.transform.localPosition.x + 0.11f, obj.transform.localPosition.y);
            rec.right_slide_canvas = false;
        }
        if (target.y > 0.42f) target.y = 0.40f;
        if (target.y < -0.42f) target.y = -0.40f;
        if (target.x > 0.21f) target.x = 0.20f;
        if (target.x < -0.44f) target.x = -0.40f;
        obj.transform.localPosition = Vector2.MoveTowards(obj.transform.localPosition, target, speed);
    }
    void Movement_new(int index, float horizontal, float vertical) {
        float speed = 0.2f;
        GameObject obj = ins[index];

        if (obj.transform.localPosition.x < 0.21)
        {
            flag = true;
        }
        if (obj.transform.localPosition.x > 0.21 & flag)
        {
            obj.transform.localPosition = new Vector3(0.21f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
        if (obj.transform.localPosition.x < -0.44)
        {
            obj.transform.localPosition = new Vector3(-0.44f, obj.transform.localPosition.y, obj.transform.localPosition.z);
        }
        if (obj.transform.localPosition.y < -0.455)
        {
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, -0.455f, obj.transform.localPosition.z);
        }
        if (obj.transform.localPosition.y > 0.455)
        {
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, 0.455f, obj.transform.localPosition.z);
        }
        Vector2 target;
        target = new Vector2((0.21f - (0.21f + 0.44f) * horizontal), (0.455f - (0.455f + 0.455f) * vertical));
        //obj.transform.position = Vector3.MoveTowards(obj.transform.position,target , speed*Time.deltaTime);
        obj.transform.localPosition = Vector2.MoveTowards(obj.transform.localPosition, target, speed * Time.deltaTime);
        //obj.transform.Translate(direction.normalized * Time.deltaTime * speed);
    }
    void PutBack(int index)
    {
        //float speed = 5f;
        GameObject obj = ins[index];
        GameObject origin = basement[index];
        obj.transform.position = origin.transform.position;
        flag = false;
    }
    void detect()
    {
        for(int i = 1; i <= 6; i++)
        {
            if (moved[i])
            {
                if (ins[i].transform.localPosition.x > -.44f & ins[i].transform.localPosition.x < 0.22f ) {
                    ma[i].volume = (ins[i].transform.localPosition.y + 0.455f) / 0.91f;
                    //Debug.Log(ma[i].volume);
                    ma[i].complex = Mathf.RoundToInt((ins[i].transform.localPosition.x + 0.44f) / 0.11f);
                    if(ma[i].complex ==0) { ma[i].complex = 1; }
                }

            }
        }
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical, 0 );
        detect();
        #region 1st controller
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad7))
        {
            AnimationControl(1);
            selected = 1;
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad4))
        {
            AnimationControl(2);
            selected = 2;
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad1))
        {
            AnimationControl(3);
            selected = 3;
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad8))
        {
            AnimationControl(4);
            selected = 4;
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad5))
        {
            AnimationControl(5);
            selected = 5;
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad2))
        {
            AnimationControl(6);
            selected = 6;
        }
        if (selected != 0 & (horizontal != 0 | vertical != 0) & (moving == 0 | moving == selected))
        {
            //Movement(selected, direction);
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
        if (moving == 0 & rec.selected_canvas[1] )
        {
            AnimationControl(1);
            selected = 1;
            rec.selected_canvas[1] = false;
            //rec.selected[1] = false;
        }
        if (moving == 0 & rec.selected_canvas[2])
        {
            AnimationControl(2);
            selected = 2;
            rec.selected_canvas[2] = false;
            //rec.selected[2] = false;
        }
        if (moving == 0 & rec.selected_canvas[3])
        {
            AnimationControl(3);
            selected = 3;
            rec.selected_canvas[3] = false;
            //rec.selected[3] = false;
        }
        if (moving == 0 & rec.selected_canvas[4])
        {
            AnimationControl(4);
            selected = 4;
            rec.selected_canvas[4]  = false;
            //rec.selected[4] = false;
        }
        if (moving == 0 & rec.selected_canvas[5])
        {
            AnimationControl(5);
            selected = 5;
            rec.selected_canvas[5] = false;
            //rec.selected[5] = false;
        }
        if (moving == 0 & rec.selected_canvas[6])
        {
            AnimationControl(6);
            selected = 6;
            rec.selected_canvas[6] = false;
            //rec.selected[6] = false;
        }
        if ((selected != 0) & (moving != 0) & rec.esc_canvas)
        {
            moved[selected] = true;
            Debug.Log(instrument[selected] + "is at is position");
            selected = 0;
            moving = 0;
            rec.esc_canvas = false;
        }
        if (selected != 0 & moved[selected] & rec.esc_canvas)
        {
            PutBack(selected);
            moved[selected] = false;
            Debug.Log(instrument[selected] + "is put back to its original place");
            selected = 0;
            rec.esc_canvas = false;
        }
        // new movement
        if (selected != 0 & rec.direct & (moving == 0 | moving == selected))
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
        #endregion
    }
}
