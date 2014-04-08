using UnityEngine;
using System.Timers;





public class note_anim
{
    private float duration;
    private int speed;
    private GameObject instance;
    private GameObject moi;
    private float scale;
    private bool upscale;
    public note_anim(int type, bool _upscale, int dur, int spd)
    {
        upscale = _upscale;
        scale = 0.7f;
        duration = dur;
        //Debug.Log(type);
        speed = spd;
        switch (type)
        {
            case 1:
                    moi = Resources.Load("prefabs/note1_m") as GameObject;
                break;
            case 2:
                    moi = Resources.Load("prefabs/note2_m") as GameObject;
                break;
            case 3:
                    moi = Resources.Load("prefabs/note3_m") as GameObject;
                break;
            case 4:
                    moi = Resources.Load("prefabs/note4_m") as GameObject;
                break;
            case 5:
                moi = Resources.Load("prefabs/note1_h") as GameObject;
                break;
            case 6:
                moi = Resources.Load("prefabs/note2_h") as GameObject;
                break;
            case 7:
                moi = Resources.Load("prefabs/note3_h") as GameObject;
                break;
            case 8:
                moi = Resources.Load("prefabs/note4_h") as GameObject;
                break;
            case 9:
                moi = Resources.Load("prefabs/note1_p") as GameObject;
                break;
            case 10:
                moi = Resources.Load("prefabs/note2_p") as GameObject;
                break;
            case 11:
                moi = Resources.Load("prefabs/note3_p") as GameObject;
                break;
            case 12:
                moi = Resources.Load("prefabs/note4_p") as GameObject;
                break;
        }
        instance = (GameObject)Object.Instantiate(moi);
        
    }


    public bool game_loop()
    {
        //Debug.Log(scale);
        if(upscale)
        {
            scale += speed / duration/3 * Time.deltaTime;
        }
        else
        {

            scale -= speed / duration * Time.deltaTime;
            if (scale < 0)
            { 
                scale = 0; 
            }
        }
        instance.transform.localScale= new Vector3(scale, scale, scale);
        
        duration -= speed * Time.deltaTime;
        if (duration <= 0)
        {
            Object.Destroy(instance);
            return true;
        }
        else
        {
            return false;
        }
    }
}