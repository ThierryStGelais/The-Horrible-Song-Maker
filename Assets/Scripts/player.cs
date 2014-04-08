using UnityEngine;
using System.Collections;
using scMessage;
using System.Globalization;
using System.Timers;
using System.Collections.Generic;





public class player
{
    private scObject song;

    private AudioClip snd1;
    private AudioClip snd2;
    private AudioClip snd3;
    private AudioClip snd4;

    private controler_game _controller_game_ref;


    public player(controler_game controller_game_ref, scObject chanson)
    {
        _controller_game_ref = controller_game_ref;
        song = chanson;
        snd1 = Resources.Load("sounds/C3") as AudioClip;
        snd2 = Resources.Load("sounds/E3") as AudioClip;
        snd3 = Resources.Load("sounds/G3") as AudioClip;
        snd4 = Resources.Load("sounds/C4") as AudioClip;

    }




    public void read_note(int cur)
    {
        for (int i = 0; i < song.getSCObjectCount(); i++)
        {
            if (!song.getSCObject(i).getBool("played"))
            {
                if (cur - song.getSCObject(i).getInt("time") >= 0)
                {
                    song.getSCObject(i).addBool("played", true);
                    playNote(song.getSCObject(i).getInt("type"), 100);
                }
            }
        }
    }


    public void playNote(int idNote, int volume)
    {
        AudioClip snd= snd1;
        switch (idNote)
        {
            case 0:
                
                break;
            case 1:
                    snd = snd1;
                break;
            case 2:
                    snd = snd2;
                break;
            case 3:
                   snd = snd3;
                break;
            case 4:
                    snd = snd4;
                break;
           
        }
        AudioSource.PlayClipAtPoint(snd, new Vector3(0,0,0), volume);
    }

    public void destroy()
    {
    }

}
