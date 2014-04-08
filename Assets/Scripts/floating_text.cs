using UnityEngine;
using System.Timers;





public class floating_text
{
    private string text;
    private float pos_x;
    private float pos_y;
    private float duration;
    private int speed;
    public floating_text(string msg, int x, int y, int dur, int spd)
    {
        text = msg;
        pos_x = x;
        pos_y = y;
        duration = dur;
        speed = spd;
    }


    public bool ui_loop()
    {

        GUI.Label(new Rect(pos_x, pos_y, 400, 200), text);
        pos_x += speed * Time.deltaTime;
        pos_y += speed/2 * Time.deltaTime;
        duration -= speed * Time.deltaTime;
        if (duration <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
