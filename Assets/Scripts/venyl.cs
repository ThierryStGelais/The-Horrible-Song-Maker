using UnityEngine;
using System.Collections;
using scMessage;
using System.Globalization;
using System.Timers;
using System.Collections.Generic;





public class venyl
{
    public ParticleSystem partTete { get { return this._partTete; } set { this._partTete = value; } }
    public ParticleSystem partMult { get { return this._partMult; } set { this._partMult = value; } }
    public int nbshake { get { return this._nbshake; } set { this._nbshake = value; } }


    private int time_tohit, _nbshake = 0;
    private float throttle = 3.0f;
    private modele_venyl _modele_venyl_ref;
    private GameObject moi;
    private GameObject note1;
    private GameObject note2;
    private GameObject note3;
    private GameObject note4;
    private GameObject bar;
    private GameObject barUp;
    private GameObject barDwn;
    private GameObject barMr;

    private GameObject tete;
    private GameObject table;
    private GameObject btvert;
    private GameObject btvertLight;



    private GameObject instanceTable;
    private GameObject instance;
    private GameObject instance1;
    private GameObject instance2;
    private GameObject instance3;
    private GameObject instance4;
    private GameObject instanceBar;
    private GameObject instanceTete;
    private GameObject instancebtnMult;
    private GameObject instancebtnStreak;
    private GameObject instanceSlider;
    private GameObject instanceScore;
    private GameObject instanceToken1;
    private GameObject instanceToken2;


    private ParticleSystem _partTete;
    private ParticleSystem _partMult;

    private GameObject slider;
    private GameObject btnMult;
    private GameObject btnStreak;
    private GameObject scoreBox;

    private AudioClip snd0;
    private AudioClip snd1;
    private AudioClip snd2;
    private AudioClip snd3;
    private AudioClip snd4;


    private int lignesId=0;

    private controler_game _controller_game_ref;

    private List<GameObject> notesSprites = new List<GameObject>();
    private List<GameObject> barsSprites = new List<GameObject>();
    private List<floating_text> floatingText = new List<floating_text>();
    private List<note_anim> noteAnim = new List<note_anim>();

    private int idNoteLecture;

    public venyl(controler_game controller_game_ref)
    {
        _controller_game_ref = controller_game_ref;
        _modele_venyl_ref = new modele_venyl();
        time_tohit = 74; //* _controller_game_ref.modele_game_ref.difficulty;
        moi = Resources.Load("prefabs/disc") as GameObject;
        note1 = Resources.Load("prefabs/note1") as GameObject;
        note2 = Resources.Load("prefabs/note2") as GameObject;
        note3 = Resources.Load("prefabs/note3") as GameObject;
        note4 = Resources.Load("prefabs/note4") as GameObject;
        bar = Resources.Load("prefabs/bar") as GameObject;
        barUp = Resources.Load("prefabs/barUp") as GameObject;
        barDwn = Resources.Load("prefabs/barDwn") as GameObject;
        barMr = Resources.Load("prefabs/barMr") as GameObject;
        btvert = Resources.Load("prefabs/led_closed") as GameObject;
        btvertLight = Resources.Load("prefabs/led_light") as GameObject;


        tete = Resources.Load("prefabs/tete") as GameObject;
        table = Resources.Load("prefabs/table") as GameObject;
        scoreBox = Resources.Load("prefabs/scoreBox") as GameObject;

        slider = Resources.Load("prefabs/slider") as GameObject;
        btnMult = Resources.Load("prefabs/btnMult") as GameObject;
        btnStreak = Resources.Load("prefabs/btnStreak") as GameObject;

        snd0 = Resources.Load("sounds/Miss") as AudioClip;
        snd1 = Resources.Load("sounds/C3") as AudioClip;
        snd2 = Resources.Load("sounds/E3") as AudioClip;
        snd3 = Resources.Load("sounds/G3") as AudioClip;
        snd4 = Resources.Load("sounds/C4") as AudioClip;


        instanceTable = (GameObject)Object.Instantiate(table);
        instanceScore = (GameObject)Object.Instantiate(scoreBox);
        instance = (GameObject)Object.Instantiate(moi);
        instanceTete = (GameObject)Object.Instantiate(tete);
        instancebtnStreak = (GameObject)Object.Instantiate(btnStreak);
        instancebtnMult = (GameObject)Object.Instantiate(btnMult);
        instanceSlider = (GameObject)Object.Instantiate(slider);
        instanceToken1 = (GameObject)Object.Instantiate(btvert);
        instanceToken1.transform.position = new Vector3(2.5f, -3.5f, 0);
        instanceToken2 = (GameObject)Object.Instantiate(btvert);
        _partTete = instanceTete.transform.GetChild(0).particleSystem;
        _partMult = instancebtnMult.transform.GetChild(0).particleSystem;

    
        instanceBar = (GameObject)Object.Instantiate(bar);
    }

    public void particules_Tete()
    {
        if (_controller_game_ref.modele_game_ref.multiplier == 0)
        {
            _partTete.Stop();
        }
        if (_controller_game_ref.modele_game_ref.multiplier >= 3)
        {
            _partTete.Play();
            if (_controller_game_ref.modele_game_ref.multiplier == 5)
            {
                _partTete.startSize = 1.0f;
            }
            else
            {
                _partTete.startSize = 0.5f;

            }
        }

    }

    public void setLed(int idLed, bool state)
    {
        if (idLed == 1)
        {
            if (state)
            {
                Object.Destroy(instanceToken1);
                instanceToken1 = (GameObject)Object.Instantiate(btvertLight);
                instanceToken1.transform.position = new Vector3(2.5f, -3.5f, 0);
            }
            else
            {
                Object.Destroy(instanceToken1);
                instanceToken1 = (GameObject)Object.Instantiate(btvert);
                instanceToken1.transform.position = new Vector3(2.5f, -3.5f, 0);
            }
        }
        else
        {
            if (state)
            {
                Object.Destroy(instanceToken2);
                instanceToken2 = (GameObject)Object.Instantiate(btvertLight);
                instanceToken2.transform.position = new Vector3(3.5f, -3.5f, 0);
            }
            else
            {
                Object.Destroy(instanceToken2);
                instanceToken2 = (GameObject)Object.Instantiate(btvert);
                instanceToken2.transform.position = new Vector3(3.5f, -3.5f, 0);
            }
        }
    }

    public void rotate(int cur)
    {
        instance.transform.Rotate(0, 0, -36 * Time.fixedDeltaTime * _controller_game_ref.modele_game_ref.difficulty);


        for (int i = 0; i < _modele_venyl_ref.notes.getSCObjectCount(); i++)
        {
            if (!_modele_venyl_ref.notes.getSCObject(i).getBool("deleted"))
            {
                
                if (cur - _modele_venyl_ref.notes.getSCObject(i).getInt("time") > time_tohit + 4)
                {
                    _controller_game_ref.augment_score(0);

                    noteAnim.Add(new note_anim(_modele_venyl_ref.notes.getSCObject(i).getInt("type"), false, 10, 4));
                    _modele_venyl_ref.notes.getSCObject(i).setBool("deleted", true);
                    notesSprites[i].transform.parent = null;
                    Object.Destroy( notesSprites[i]);
                }
            }
        }
        if ((cur - (100/**_controller_game_ref.modele_game_ref.difficulty*/)) / 5 > lignesId)
        {
            if (lignesId < barsSprites.Count)
            {
                Object.Destroy(barsSprites[lignesId]);
                lignesId++;
            }
        }
    }

    public void rotate_Tete()
    {
        instanceTete.transform.Rotate(0, 0, throttle * Time.fixedDeltaTime);

        if (_controller_game_ref.modele_game_ref.tick % 5 == 0)
        {
            throttle = throttle * (-1);
        }
    }


    public void read_note(int idTypeNote, int cur)
    {
        bool miss = true;
        for (int i = 0; i < _modele_venyl_ref.notes.getSCObjectCount(); i++)
        {
            if (_modele_venyl_ref.notes.getSCObject(i).getInt("type") == idTypeNote && !_modele_venyl_ref.notes.getSCObject(i).getBool("deleted"))
            {

                if (cur - _modele_venyl_ref.notes.getSCObject(i).getInt("time") >= time_tohit - 2 && cur - _modele_venyl_ref.notes.getSCObject(i).getInt("time") <= time_tohit+2)
                {
                    if (cur - _modele_venyl_ref.notes.getSCObject(i).getInt("time") >= time_tohit - 1 && cur - _modele_venyl_ref.notes.getSCObject(i).getInt("time") <= time_tohit+1)
                    {
                        if (cur - _modele_venyl_ref.notes.getSCObject(i).getInt("time") == time_tohit)
                        {
                            //perfect
                            floatingText.Add(new floating_text("WOW PERFECT!!", Screen.width - 760, 300, 75, 70));
                            _modele_venyl_ref.notes.getSCObject(i).setBool("deleted", true);
                            noteAnim.Add(new note_anim((_modele_venyl_ref.notes.getSCObject(i).getInt("type") + 8), true, 4, 4));

                            notesSprites[i].transform.parent = null;
                            
                            playNote(idTypeNote,100);
                            Object.Destroy(notesSprites[i]);
                            _controller_game_ref.augment_score(3);
                            miss = false;
                        }
                        else
                        {

                            miss = false;
                            floatingText.Add(new floating_text("Much Great!", Screen.width - 760, 300, 75, 70));
                            _modele_venyl_ref.notes.getSCObject(i).setBool("deleted", true);
                            noteAnim.Add(new note_anim((_modele_venyl_ref.notes.getSCObject(i).getInt("type") + 4), true, 2, 4));
                            playNote(idTypeNote, 66);
                            notesSprites[i].transform.parent = null;

                            Object.Destroy(notesSprites[i]);
                            _controller_game_ref.augment_score(2);

                        }
                        //good

                    }
                    else
                    {

                        floatingText.Add(new floating_text("Such Good", Screen.width - 760, 300, 75, 70));
                        _modele_venyl_ref.notes.getSCObject(i).setBool("deleted", true);
                        noteAnim.Add(new note_anim((_modele_venyl_ref.notes.getSCObject(i).getInt("type") + 4), true, 2, 4));
                        miss = false;

                        playNote(idTypeNote, 33);
                        notesSprites[i].transform.parent = null;

                        Object.Destroy(notesSprites[i]);
                        _controller_game_ref.augment_score(1);

                    }
                    //great

                }

            }
        }
            if (miss)
            {
                floatingText.Add(new floating_text("So Miss", Screen.width - 760, 350, 75, 70));
                playNote(0, 100);
                nbshake = 20;
                _controller_game_ref.augment_score(0);

            }
    }


    public void playNote(int idNote, int volume)
    {
        AudioClip snd= snd1;
        switch (idNote)
        {
            case 0:
                    snd = snd0;
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
        AudioSource.PlayClipAtPoint(snd, instance.transform.position, volume);
    }

    public void add_note(int idTypeNote, int time)
    {
        GameObject noteObj = note1;
        if (_modele_venyl_ref.nbNotes < 2)
        {
            switch (idTypeNote)
            {
                case 1:
                    if (!_modele_venyl_ref.note1)
                    {
                        instance1 = (GameObject)Object.Instantiate(note1);
                        _modele_venyl_ref.note1 = true;
                        _modele_venyl_ref.nbNotes++;
                    }
                    else
                    {
                        Object.Destroy(instance1);
                        _modele_venyl_ref.note1 = false;
                        _modele_venyl_ref.nbNotes--;
                    }

                    break;
                case 2:
                    if (!_modele_venyl_ref.note2)
                    {
                        instance2 = (GameObject)Object.Instantiate(note2);
                        _modele_venyl_ref.note2 = true;
                        _modele_venyl_ref.nbNotes++;
                    }
                    else
                    {
                        Object.Destroy(instance2);
                        _modele_venyl_ref.note2 = false;
                        _modele_venyl_ref.nbNotes--;
                    }
                    break;
                case 3:
                    if (!_modele_venyl_ref.note3)
                    {
                        instance3 = (GameObject)Object.Instantiate(note3);
                        _modele_venyl_ref.note3 = true;
                        _modele_venyl_ref.nbNotes++;
                    }
                    else
                    {
                        Object.Destroy(instance3);
                        _modele_venyl_ref.note3 = false;
                        _modele_venyl_ref.nbNotes--;
                    }
                    break;
                case 4:
                    if (!_modele_venyl_ref.note4)
                    {
                        instance4 = (GameObject)Object.Instantiate(note4);
                        _modele_venyl_ref.note4 = true;
                        _modele_venyl_ref.nbNotes++;
                    }
                    else
                    {
                        Object.Destroy(instance4);
                        _modele_venyl_ref.note4 = false;
                        _modele_venyl_ref.nbNotes--;
                    }
                    break;
            }
        }

    }


    public bool atk_note(int type)
    {
        bool freeToken = false;
        if (_modele_venyl_ref.typeAccordSvt > 0)
        {
            freeToken = true;
        }
        _modele_venyl_ref.typeAccordSvt = type;
        Object.Destroy(instanceBar);
        switch (_modele_venyl_ref.typeAccordSvt)
        {
            case 0:
                instanceBar = (GameObject)Object.Instantiate(bar);
                break;
            case 1:
                instanceBar = (GameObject)Object.Instantiate(barUp);
                break;
            case 2:
                instanceBar = (GameObject)Object.Instantiate(barDwn);
                break;
            case 3:
                instanceBar = (GameObject)Object.Instantiate(barMr);
                break;
        }
        return freeToken;

    }

    public void clipNotes(int tick)
    {
            barsSprites.Add(instanceBar);
            barsSprites[(barsSprites.Count) - 1].transform.parent = instance.transform;
            instanceBar = (GameObject)Object.Instantiate(bar);
            if (!_modele_venyl_ref.note1 && !_modele_venyl_ref.note2 && !_modele_venyl_ref.note3 && !_modele_venyl_ref.note4 && _modele_venyl_ref.typeAccordSvt > 0)
            {
                _controller_game_ref.incrementToken();
            }

        if (_modele_venyl_ref.note1)
        {
            notesSprites.Add(instance1);
            notesSprites[(notesSprites.Count) - 1].transform.parent = instance.transform;
            instance1 = new GameObject();
           
        }
        if (_modele_venyl_ref.note2)
        {
            notesSprites.Add(instance2);
            notesSprites[(notesSprites.Count) - 1].transform.parent = instance.transform;
            instance2 = new GameObject();
            
        }
        if (_modele_venyl_ref.note3)
        {
            notesSprites.Add(instance3);
            notesSprites[(notesSprites.Count) - 1].transform.parent = instance.transform;
            instance3 = new GameObject();
        }
        if (_modele_venyl_ref.note4)
        {
            notesSprites.Add(instance4);
            notesSprites[(notesSprites.Count) - 1].transform.parent = instance.transform;
            instance4 = new GameObject();
        }   

            bool temp = false;
            switch (_modele_venyl_ref.typeAccordSvt)
            {
                case 1:
                        temp = _modele_venyl_ref.note1;
                        _modele_venyl_ref.note1=_modele_venyl_ref.note2;
                        _modele_venyl_ref.note2 = _modele_venyl_ref.note3;
                        _modele_venyl_ref.note3 = _modele_venyl_ref.note4;
                        _modele_venyl_ref.note4 = temp;
                    break;
                case 2:
                          temp = _modele_venyl_ref.note4;
                        _modele_venyl_ref.note4 = _modele_venyl_ref.note3;
                        _modele_venyl_ref.note3 = _modele_venyl_ref.note2;
                        _modele_venyl_ref.note2 = _modele_venyl_ref.note1;
                        _modele_venyl_ref.note1 = temp;
                    break;
                case 3:
                        temp = _modele_venyl_ref.note1;
                        _modele_venyl_ref.note1=_modele_venyl_ref.note4;
                        _modele_venyl_ref.note4=temp;

                        temp = _modele_venyl_ref.note2;
                        _modele_venyl_ref.note2=_modele_venyl_ref.note3;
                        _modele_venyl_ref.note3=temp;
                    break;

            }
            _modele_venyl_ref.typeAccordSvt=0;
        scObject note = new scObject("");

        if (_modele_venyl_ref.note1)
        {
            _modele_venyl_ref.note1 = false;
            _modele_venyl_ref.nbNotes--;
            note = new scObject("note1");
            note.addInt("time", tick);
            note.addInt("type", 1);
            note.addBool("deleted", false);
            _modele_venyl_ref.notes.addSCObject(note);
        }
        if (_modele_venyl_ref.note2)
        {
            _modele_venyl_ref.note2 = false;
            _modele_venyl_ref.nbNotes--;
            note = new scObject("note2");
            note.addInt("time", tick);
            note.addInt("type", 2);
            note.addBool("deleted", false);
            _modele_venyl_ref.notes.addSCObject(note);
        }
        if (_modele_venyl_ref.note3)
        {
            _modele_venyl_ref.note3 = false;
            _modele_venyl_ref.nbNotes--;
            note = new scObject("note3");
            note.addInt("time", tick);
            note.addInt("type", 3);
            note.addBool("deleted", false);
            _modele_venyl_ref.notes.addSCObject(note);
        }
        if (_modele_venyl_ref.note4)
        {
            _modele_venyl_ref.note4 = false;
            _modele_venyl_ref.nbNotes--;
            note = new scObject("note4");
            note.addInt("time", tick);
            note.addInt("type", 4);
            note.addBool("deleted", false);
            _modele_venyl_ref.notes.addSCObject(note);
        }   
    }

    public void uiLoop()
    {
        for (int i = 0; i < floatingText.Count; i++)
        {
            if (floatingText[i].ui_loop())
            {
                floatingText.RemoveAt(i);
                i--;
            }
            
        }
        for (int i = 0; i < noteAnim.Count; i++)
        {
            if (noteAnim[i].game_loop())
            {
                noteAnim.RemoveAt(i);
                i--;
            }

        }
        if (nbshake == 0)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(2, 0, -14);
        }
        else if (_controller_game_ref.modele_game_ref.tick % 2 == 0)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(1.9f, 0, -14);
            nbshake--;

        }
        else
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(2.1f, 0, -14);
            nbshake--;

        }
    }

    public void ajusteBtnMult(int changement)
    {
        instancebtnMult.transform.Rotate(0, 0, changement*2);
    }

    public void ajusteBtnStreak(float chang)
    {
        instancebtnStreak.transform.Rotate(0, 0, chang);
    }

    public void ajusteBtnSlider(float chang)
    {
        instanceSlider.transform.position= new Vector3(6, (0-2.53f + chang/45), 0);
    }

    public void game_loop()
    {
        Object.Destroy(GameObject.Find("New Game Object"));
        
    }

    public scObject destroy()
    {
        Object.Destroy(instance);
        Object.Destroy(instance1);
        Object.Destroy(instance2);
        Object.Destroy(instance3);
        Object.Destroy(instance4);
        Object.Destroy(instanceBar);
        Object.Destroy(instanceTete);
        Object.Destroy(instanceTable);
        Object.Destroy(instancebtnMult);
        Object.Destroy(instancebtnStreak);
        Object.Destroy(instanceScore);
        Object.Destroy(instanceSlider);
        Object.Destroy(instanceToken1);
        Object.Destroy(instanceToken2);

        for (int i = lignesId; i< barsSprites.Count;i++)
        {
            Object.Destroy(barsSprites[i]);
        }
        for (int i = (barsSprites.Count-1); i >=0; i--)
        {
            barsSprites.RemoveAt(i);
        }
        for (int i = (notesSprites.Count - 1); i >= 0; i--)
        {
            notesSprites.RemoveAt(i);
        }
        notesSprites=new List<GameObject>();
        barsSprites = new List<GameObject>();
        /*while (GameObject.Find("New Game Object"))
        {
            Object.Destroy(GameObject.Find("New Game Object"));
        }*/
        return _modele_venyl_ref.notes;
    }

}
