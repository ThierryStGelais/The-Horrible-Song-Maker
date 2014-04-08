using UnityEngine;
using System.Collections;
using System.Timers;
using scMessage;
using System.Linq;
using System.Text;





public class controler_game
{
    private modele_game _modele_game_ref;
    private main main_ref;
    private venyl _venyl;
    private GUISkin bt;
    private GUISkin txt;

    private GameObject instanceMenu;
    private GameObject PochMenu;
    private GameObject HowTo;
    private GameObject HowTo2;
    private GameObject Credit;

    private GameObject bar1;
    private GameObject bar2;
    private GameObject meter;

    private GameObject instanceBar1;
    private GameObject instanceBar2;
    private GameObject instanceMeter;


    private player lecteur;



    public modele_game modele_game_ref { get { return this._modele_game_ref; } }
    public controler_game(main main_Ref)
    {
        main_ref = main_Ref;  
        _modele_game_ref = new modele_game();
        bt = Resources.Load("bt") as GUISkin;
        txt = Resources.Load("txt") as GUISkin;
        HowTo = Resources.Load("prefabs/HowTo") as GameObject;
        HowTo2 = Resources.Load("prefabs/HowTo2") as GameObject;
        Credit = Resources.Load("prefabs/Credits") as GameObject;

        PochMenu = Resources.Load("prefabs/Menu") as GameObject;

        bar1 = Resources.Load("prefabs/graph1") as GameObject;
        bar2 = Resources.Load("prefabs/graph2") as GameObject;
        meter = Resources.Load("prefabs/meter") as GameObject;

        instanceMenu = (GameObject)Object.Instantiate(PochMenu);

    }


    public void augment_score(int score)
    {
        if (score == 0)
        {
            _venyl.ajusteBtnMult(15 * (_modele_game_ref.multiplier - 1));
            _venyl.ajusteBtnStreak(9.0f * _modele_game_ref.streak);
            _modele_game_ref.multiplier = 1;
            _venyl.partTete.Stop();
            _modele_game_ref.streak = 0;
            _venyl.nbshake = 20;
        }
        else
        {
            if (score == 3)
            {
                _modele_game_ref.streak ++;
                _venyl.ajusteBtnStreak(-9.0f);
            }

            _modele_game_ref.streak++;

            if (_modele_game_ref.streak >= 10)
            {
                incMult();
                _venyl.ajusteBtnStreak(9.0f * _modele_game_ref.streak);
                _modele_game_ref.streak = 0;
            }
            if (_modele_game_ref.manche % 2 == 1)
            {
                switch (score)
                {
                    case 1:
                        _modele_game_ref.scoreP1 += 25 * _modele_game_ref.multiplier;
                        break;
                    case 2:
                        _modele_game_ref.scoreP1 += 50 * _modele_game_ref.multiplier;
                        break;
                    case 3:
                        _modele_game_ref.scoreP1 += 100 * _modele_game_ref.multiplier;
                        break;
                }
            }
            else
            {
                switch (score)
                {
                    case 1:
                        _modele_game_ref.scoreP2 += 25 * _modele_game_ref.multiplier;
                        break;
                    case 2:
                        _modele_game_ref.scoreP2 += 50 * _modele_game_ref.multiplier;
                        break;
                    case 3:
                        _modele_game_ref.scoreP2 += 100 * _modele_game_ref.multiplier;
                        break;
                }
            }
            _modele_game_ref.success++;
            _venyl.ajusteBtnStreak(-9.0f);
        }
        _modele_game_ref.hit++;
        _venyl.ajusteBtnSlider((_modele_game_ref.success*1f)/_modele_game_ref.hit*100f);
        
    }

    public void incMult()
    {
        if (_modele_game_ref.multiplier < 5)
        {
            _modele_game_ref.multiplier++;
            _venyl.ajusteBtnMult(-15);
            _venyl.particules_Tete();
            _venyl.partMult.Play();
        }
        incrementToken();
    }

    public void decrementToken()
    {
        _modele_game_ref.atkToken--;
        if (_modele_game_ref.atkToken == 1)
        {
            _venyl.setLed(1, true);
            _venyl.setLed(2, false);
        }
        else if (_modele_game_ref.atkToken == 0)
        {
            _venyl.setLed(1, false);
            _venyl.setLed(2, false);
        }
    }

    public void incrementToken()
    {
        if (_modele_game_ref.atkToken < 2)
        {
            _modele_game_ref.atkToken++;
            if (_modele_game_ref.atkToken == 1)
            {
                _venyl.setLed(1, true);
                _venyl.setLed(2, false);
            }
            else if (_modele_game_ref.atkToken == 2)
            {
                _venyl.setLed(1, true);
                _venyl.setLed(2, true);
            }
        }
        else
        {
            _venyl.setLed(1, true);
            _venyl.setLed(2, true);
        }
    }

    public void keyinput(string key)
    {
        if (_modele_game_ref.tick > 0 && _modele_game_ref.tick <= (500 * _modele_game_ref.difficulty))
        {
            switch (key)
            {
                case "u":
                    _venyl.add_note(1, _modele_game_ref.tick);

                    break;
                case "i":
                    _venyl.add_note(2, _modele_game_ref.tick);

                    break;
                case "o":
                    _venyl.add_note(3, _modele_game_ref.tick);

                    break;
                case "p":
                    _venyl.add_note(4, _modele_game_ref.tick);

                    break;
                case "8":
                    if (_modele_game_ref.atkToken > 0)
                    {
                        if (!_venyl.atk_note(1))
                        {
                            decrementToken();
                            

                        }
                    }
                    break;
                case "9":
                    if (_modele_game_ref.atkToken > 0)
                    {
                        if (!_venyl.atk_note(2))
                        {
                            decrementToken();
                        }
                    }
                    break;
                case "0":
                    if (_modele_game_ref.atkToken > 0)
                    {
                        
                        if(!_venyl.atk_note(3))
                        {
                            decrementToken();
                        }
                        
                    }
                    break;
                }
            
            }
        if (_modele_game_ref.tick > 0)
        {
            switch (key)
            {
                case "q":
                    _venyl.read_note(1, _modele_game_ref.tick);

                    break;
                case "w":
                    _venyl.read_note(2, _modele_game_ref.tick);

                    break;
                case "e":
                    _venyl.read_note(3, _modele_game_ref.tick);

                    break;
                case "r":
                    _venyl.read_note(4, _modele_game_ref.tick);

                    break;
            }
            
        }
    }

    public void ui_loop()
    {
        float mod = ((Screen.width * 1.0f) / Screen.height)/3;
        GUI.skin = bt;
        if (_modele_game_ref.tick >= (600 * _modele_game_ref.difficulty))
        {
            _modele_game_ref.tick = 0;
            _modele_game_ref.scoreTemp = _modele_game_ref.scoreP1;
            _modele_game_ref.hitTemp = _modele_game_ref.hit;
            _modele_game_ref.successTemp = _modele_game_ref.success;
            _modele_game_ref.chansons.addSCObject(_venyl.destroy());
            _modele_game_ref.isPlaying = false;
            if (_modele_game_ref.manche % 2 != 0)
            {
                instanceMenu = (GameObject)Object.Instantiate(PochMenu);
            }
            else
            {
                scObject dataP1 = _modele_game_ref.scoreData.getSCObject((_modele_game_ref.manche - 2));
                scObject dataP2 = new scObject(_modele_game_ref.manche.ToString());
                if (_modele_game_ref.hit == 0)
                {
                    _modele_game_ref.hit = 1;
                }
                dataP2.addInt("hit", _modele_game_ref.hit);
                dataP2.addInt("succes", _modele_game_ref.success);
                dataP2.addInt("score", _modele_game_ref.scoreP2);
                float scoreP1= ((dataP1.getInt("succes") * 1f) / dataP1.getInt("hit") * (dataP1.getInt("score")));
                float scoreP2= (((dataP2.getInt("succes") * 1f) / dataP2.getInt("hit")) * (dataP2.getInt("score")));
                instanceMeter = (GameObject)Object.Instantiate(meter);
                // afficher lorsque manche est paire???
                if (scoreP2 < scoreP1)
                {
                    lecteur=new player(this, _modele_game_ref.chansons.getSCObject( _modele_game_ref.chansons.getSCObjectCount()-2));
                    instanceMeter.transform.GetChild(0).transform.Rotate(0,0,(((scoreP1-scoreP2)/(scoreP1+scoreP2))*45));
                }
                else
                {
                    lecteur = new player(this, _modele_game_ref.chansons.getSCObject(_modele_game_ref.chansons.getSCObjectCount() - 1));
                    instanceMeter.transform.GetChild(0).transform.Rotate(0, 0, -(((scoreP2 - scoreP1) / (scoreP1 + scoreP2))*45));
                }

                //instanceFen1 = (GameObject)Object.Instantiate(fenetre);
                //instanceFen2 = (GameObject)Object.Instantiate(fenetre);
                
                instanceBar1 = (GameObject)Object.Instantiate(bar1);
                instanceBar2 = (GameObject)Object.Instantiate(bar2);

                instanceBar1.transform.position = new Vector3(-4f, -4, 0);
                instanceBar2.transform.position = new Vector3(8, -4, 0);
            }


        }
        if (_modele_game_ref.tick == 0)
        {
            if (_modele_game_ref.manche == 0)
            {
                if (_modele_game_ref.howToPlaySt==0)
                {
                    GUI.skin = bt;
                    if (GUI.Button(new Rect((Screen.width / 2) - 235, 220, 210, 50), "Start Game") )
                    {
                        Object.Destroy(instanceMenu);
                        _venyl = new venyl(this);
                        _modele_game_ref.manche++;
                        _modele_game_ref.isPlaying = true;
                    }
                    if (_modele_game_ref.difficulty != 1)
                    {
                        GUI.skin = bt;
                        if (GUI.Button(new Rect((Screen.width / 2) - 235, 280, 210, 50), "Normal"))
                        {
                            _modele_game_ref.difficulty = 1;
                        }
                    }
                    else
                    {
                        GUI.skin = txt;
                        if (GUI.Button(new Rect((Screen.width / 2) - 235, 280, 210, 40), "Normal"))
                        {
                            _modele_game_ref.difficulty = 1;
                        }

                    }
                    if (_modele_game_ref.difficulty != 1.6f)
                    {
                        GUI.skin = bt;
                        if (GUI.Button(new Rect((Screen.width / 2) - 235, 320, 210, 40), "Hard"))
                        {
                            _modele_game_ref.difficulty = 1.6f;
                        }
                    }
                    else
                    {
                        GUI.skin = txt;
                        if (GUI.Button(new Rect((Screen.width / 2) - 235, 320, 210, 40), "Hard"))
                        {
                            _modele_game_ref.difficulty = 1.6f;
                        }
                    }
                    if (_modele_game_ref.difficulty != 2)
                    {
                        GUI.skin = bt;
                        if (GUI.Button(new Rect((Screen.width / 2) - 235, 360, 210, 40), "Expert"))
                        {
                            _modele_game_ref.difficulty = 2;
                        }
                    }
                    else
                    {
                        GUI.skin = txt;
                        if (GUI.Button(new Rect((Screen.width / 2) - 235, 360, 210, 40), "Expert"))
                        {
                            _modele_game_ref.difficulty = 2;
                        }
                    }
                    GUI.skin = bt;
                    if (GUI.Button(new Rect((Screen.width / 2) - 235, 410, 210, 50), "How to play"))
                    {
                        _modele_game_ref.howToPlaySt ++;
                        Object.Destroy(instanceMenu);
                        instanceMenu = (GameObject)Object.Instantiate(HowTo);
                    }
                    if (GUI.Button(new Rect((Screen.width / 2) - 235, 470, 210, 50), "Exit game"))
                    {
                        Application.Quit();
                    }
                }
                else
                {
                    if (GUI.Button(new Rect((Screen.width / 2) - 220, Screen.height - 103, 210, 50), "Back"))
                    {
                        if (_modele_game_ref.howToPlaySt == 1)
                        {
                            Object.Destroy(instanceMenu);
                            instanceMenu = (GameObject)Object.Instantiate(PochMenu);
                            _modele_game_ref.howToPlaySt--;
                        }
                        else
                        {
                            Object.Destroy(instanceMenu);
                            instanceMenu = (GameObject)Object.Instantiate(HowTo);
                            _modele_game_ref.howToPlaySt--;
                        }

                        
                    }
                    if (GUI.Button(new Rect((Screen.width / 2) , Screen.height - 103, 210, 50), "Next"))
                    {
                        if (_modele_game_ref.howToPlaySt == 1)
                        {
                            Object.Destroy(instanceMenu);
                            instanceMenu = (GameObject)Object.Instantiate(HowTo2);
                            _modele_game_ref.howToPlaySt++;
                        }
                        else 
                        {
                            Object.Destroy(instanceMenu);
                            instanceMenu = (GameObject)Object.Instantiate(PochMenu);
                            _modele_game_ref.howToPlaySt=0;
                        }
                    }
                }

            }
            else if (_modele_game_ref.manche % 2 == 0)
            {
                GUI.skin = txt;
                scObject dataP1 = _modele_game_ref.scoreData.getSCObject((_modele_game_ref.manche - 2));
                scObject dataP2 = new scObject(_modele_game_ref.manche.ToString());
                        if (_modele_game_ref.hit == 0)
                        {
                            _modele_game_ref.hit = 1;
                        }
                        dataP2.addInt("hit", _modele_game_ref.hit);
                        dataP2.addInt("succes", _modele_game_ref.success);
                        dataP2.addInt("score", _modele_game_ref.scoreP2);

                // afficher lorsque manche est paire???
                if ((((dataP2.getInt("succes") * 1f) / dataP2.getInt("hit")) * (dataP2.getInt("score"))) < ((dataP1.getInt("succes") * 1f) / dataP1.getInt("hit") * (dataP1.getInt("score"))))
                {
                    GUI.Label(new Rect((Screen.width - 200) / 2, 50, 200, 200), "Player 1 Win!!!!");
                }
                else if ((((dataP2.getInt("succes") * 1f) / dataP2.getInt("hit")) * (dataP2.getInt("score"))) == ((dataP1.getInt("succes") * 1f) / dataP1.getInt("hit") * (dataP1.getInt("score"))))
                {
                    GUI.Label(new Rect((Screen.width-200)/2, 50, 200, 200), "Tie!!!!");

                }
                else
                {
                    GUI.Label(new Rect((Screen.width - 200) / 2, 50, 200, 200), "Player 2 Win!!!!");
                    
                }
                GUI.Label(new Rect(10, 10, 200, 50), "Player 1");
                
                GUI.Label(new Rect(155, 110, 200, 50),(dataP1.getInt("score")).ToString()+" Pts");

                GUI.Label(new Rect(160, 170, 200, 50), (Mathf.Round((dataP1.getInt("succes") * 1f) / dataP1.getInt("hit") * 100f)).ToString() + " l 100");
                instanceBar1.transform.localScale = new Vector3(2, ((dataP1.getInt("succes") * 1f) / dataP1.getInt("hit"))* 2, 1);

                //GUI.Label(new Rect(100, 200, 400, 200), "Normalised score: " + Mathf.Round(((dataP1.getInt("succes") * 1f) /dataP1.getInt("hit") * (dataP1.getInt("score")))).ToString() + "Pts");

                GUI.Label(new Rect(Screen.width-210, 10, 200, 50), "Player 2");
                GUI.Label(new Rect(Screen.width-400, 110, 200, 50), (dataP2.getInt("score")).ToString() + " Pts");

                GUI.Label(new Rect(Screen.width-360, 170, 200, 50), (Mathf.Round((dataP2.getInt("succes") * 1f) / dataP2.getInt("hit") * 100f)).ToString() + " l 100");
                instanceBar2.transform.localScale = new Vector3(2, ((dataP2.getInt("succes") * 1f) / dataP2.getInt("hit"))*2, 1);

                //GUI.Label(new Rect(600, 200, 400, 200), "Normalised score: " + Mathf.Round((((dataP2.getInt("succes") * 1f) / dataP2.getInt("hit")) * (dataP2.getInt("score")))).ToString() + "Pts");


                GUI.skin = bt;
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - 50, Screen.width / 3, 50), "Start New Game"))
                {
                    Object.Destroy(instanceBar1);
                    Object.Destroy(instanceBar2);
                    Object.Destroy(instanceMeter);

  
                        scObject dataScore = new scObject(_modele_game_ref.manche.ToString());
                        dataScore.addInt("hit", _modele_game_ref.hit);
                        dataScore.addInt("succes", _modele_game_ref.success);
                        dataScore.addInt("score", _modele_game_ref.scoreP2);
                        _modele_game_ref.scoreData.addSCObject(dataScore);
                        _modele_game_ref.hit = 0;
                        _modele_game_ref.success = 0;
                        _modele_game_ref.scoreP1 = 0;
                        _modele_game_ref.multiplier = 1;
                        _modele_game_ref.streak = 0;
                        _modele_game_ref.manche++;
                        _modele_game_ref.offTick = 0;
                    _venyl = new venyl(this);
                    _modele_game_ref.isPlaying = true;
                   // t_timer.Start();
                }
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 3), Screen.height - 50, Screen.width / 3, 50), "Exit Game"))
                {
                    Application.Quit();
                }
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 3 * 3), Screen.height - 50, Screen.width / 3, 50), "Credit"))
                {
                    _modele_game_ref.credit = true;
                    instanceMenu = (GameObject)Object.Instantiate(Credit);
                }
                if (_modele_game_ref.credit)
                {
                    if (GUI.Button(new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - 100, Screen.width / 4, 50), "Close"))
                    {
                        _modele_game_ref.credit = false;
                        Object.Destroy(instanceMenu);
                    }
                }
            }
            else
            {

                

                if (GUI.Button(new Rect((Screen.width / 2) - 235,  250, 250, 50), "Switch Side"))
                {
                    Object.Destroy(instanceMenu);

                    scObject dataScore = new scObject(_modele_game_ref.manche.ToString());
                    if (_modele_game_ref.hit == 0)
                    {
                        _modele_game_ref.hit = 1;
                    }

                    dataScore.addInt("hit", _modele_game_ref.hit);
                    dataScore.addInt("succes", _modele_game_ref.success);
                    dataScore.addInt("score", _modele_game_ref.scoreP1);
                    _modele_game_ref.scoreData.addSCObject(dataScore);
                    _modele_game_ref.hit=0;
                    _modele_game_ref.success=0;
                    _modele_game_ref.scoreP1 = 0;
                    _modele_game_ref.multiplier = 1;
                    _modele_game_ref.streak = 0;
                    _modele_game_ref.tick = 1;

                    _modele_game_ref.manche++;
                    _venyl = new venyl(this);
                    _modele_game_ref.isPlaying = true;
                   /// t_timer.Start();
                }
            }
        }

        int hit = 1;
        if (_modele_game_ref.hit != 0)
        {
            hit = _modele_game_ref.hit;
        }
        if (_modele_game_ref.isPlaying)
        {
            //GUI.Label(new Rect(Screen.width-220, 40, 400, 200), "Raw Score:");
            //GUI.Label(new Rect(600, 75, 400, 200), "P2 Raw Score:");
            //GUI.Label(new Rect(Screen.width - 200, 125, 400, 200), "Notes Hit: ");
            //GUI.Label(new Rect(600, 125, 400, 200), "P1 Notes Hit: ");
            //GUI.Label(new Rect(Screen.width - 200, 175, 400, 200), "Final Score:");
           // GUI.Label(new Rect(600, 175, 400, 200), "P1 Final Score:");
            GUI.skin = txt;
            //GUI.Label(new Rect(600, 95, 400, 200), (scorePlayer2).ToString());
            if (_modele_game_ref.manche % 2 == 1)
            {
                GUI.Label(new Rect(Screen.width - 600*mod, 150*((Screen.height*1f)/Screen.width), 200, 50), (_modele_game_ref.scoreP1).ToString());
                if (_modele_game_ref.hit > 0)
                {
                    //GUI.Label(new Rect(Screen.width - 200, 145, 400, 200), Mathf.Round((_modele_game_ref.success * 1f) / _modele_game_ref.hit * 100f).ToString() + " %");
                    //GUI.Label(new Rect(Screen.width - 200, 195, 400, 200), Mathf.Round((((_modele_game_ref.success * 1f) / _modele_game_ref.hit) * (scorePlayer1))).ToString() + " points");
                }
            }
            else
            {
                GUI.Label(new Rect(Screen.width - 600 * mod, 150 * ((Screen.height * 1f) / Screen.width), 200, 50), (_modele_game_ref.scoreP2).ToString());
                //GUI.Label(new Rect(Screen.width - 200, 145, 400, 200), Mathf.Round((_modele_game_ref.successTemp * 1f) / _modele_game_ref.hitTemp * 100f).ToString() + " %");
                //GUI.Label(new Rect(Screen.width - 200, 195, 400, 200), Mathf.Round((((_modele_game_ref.successTemp * 1f) / _modele_game_ref.hitTemp) * (_modele_game_ref.scoreTemp))).ToString() + " points");
                /*if (_modele_game_ref.hit > 0)
                {
                    GUI.Label(new Rect(600, 145, 400, 200), Mathf.Round((_modele_game_ref.success * 1f) / _modele_game_ref.hit * 100f).ToString() + " %");
                    GUI.Label(new Rect(600, 195, 400, 200), Mathf.Round((((_modele_game_ref.success * 1f) / _modele_game_ref.hit) * (scorePlayer2))).ToString() + " points");
                }*/

            }
            /*GUI.Label(new Rect(500, 30, 400, 200), "Time:");
            GUI.Label(new Rect(500, 50, 400, 200), Mathf.Round((_modele_game_ref.tick / 10f)).ToString());*/
            _venyl.uiLoop();
        }
    }


    public void rotate_loop()
    {
        if (_modele_game_ref.isPlaying)
        {
            _modele_game_ref.subTick += Time.fixedDeltaTime;
            if (_modele_game_ref.subTick >= (0.1f / _modele_game_ref.difficulty))
            {
                float excedent = _modele_game_ref.subTick % (0.1f / _modele_game_ref.difficulty);

                _modele_game_ref.tick += (int)((_modele_game_ref.subTick - excedent) * (10 * _modele_game_ref.difficulty));
                _modele_game_ref.subTick = excedent;
                if (_modele_game_ref.tick % 5 == 0)
                {
                    _modele_game_ref.newBeat = true;
                }
            }
        }
        else
        {
            _modele_game_ref.subTick += Time.fixedDeltaTime;
            if (_modele_game_ref.subTick >= (0.1f / _modele_game_ref.difficulty))
            {
                float excedent = _modele_game_ref.subTick % (0.1f / _modele_game_ref.difficulty);

                _modele_game_ref.offTick += (int)((_modele_game_ref.subTick - excedent) * (10 * _modele_game_ref.difficulty));
                _modele_game_ref.subTick = excedent;
                lecteur.read_note(_modele_game_ref.offTick);
            }
        }

        if (_modele_game_ref.isPlaying)
        {

            _venyl.rotate(_modele_game_ref.tick);
            _venyl.rotate_Tete();
        }

    }
    public void game_loop()
    {

        if (_modele_game_ref.tick > 0)
        {
            if (_modele_game_ref.newBeat && _modele_game_ref.tick <= (505 * _modele_game_ref.difficulty))
            {
                _modele_game_ref.newBeat = false;
                _venyl.clipNotes(_modele_game_ref.tick);
            }
            else
            {
                Object.Destroy(GameObject.Find("New Game Object")); 
            }
        }
       
    }

}
