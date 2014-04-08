using UnityEngine;
using System.Collections;
using System.Timers;
using scMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;





public class modele_game
{

    private float  _difficulty = 1f;
    private int _tick = 0, _offTick = 0, _atkToken = 0, _howToPlaySt=0;
    private float _subTick = 0;
    public int offTick { get { return this._offTick; } set { this._offTick = value; } }
    public int atkToken { get { return this._atkToken; } set { this._atkToken = value; } }

    public int tick { get { return this._tick; } set { this._tick = value; } }
    public float subTick { get { return this._subTick; } set { this._subTick = value; } }
    public float difficulty { get { return this._difficulty; } set { this._difficulty = value; } }
    private bool _newBeat = false, _isPlaying=false, _credit=false;
    public bool newBeat { get { return this._newBeat; } set { this._newBeat = value; } }
    public bool credit { get { return this._credit; } set { this._credit = value; } }

    public bool isPlaying { get { return this._isPlaying; } set { this._isPlaying = value; } }
    public int howToPlaySt { get { return this._howToPlaySt; } set { this._howToPlaySt = value; } }
    private scObject _chansons=new scObject("chansons");
    public scObject chansons { get { return this._chansons; } set { this._chansons = value; } }



    private scObject _scoreData=new scObject("scores");
    public scObject scoreData { get { return this._scoreData; } set { this._scoreData = value; } }

    private int _scoreP1 = 0, _scoreP2 = 0, _streak = 0, _multiplier = 1, _hit = 0, _success = 0, _manche = 0, _scoreTemp = 0, _hitTemp = 0, _successTemp = 0;
    public int scoreP1 { get { return this._scoreP1; } set { this._scoreP1 = value; } }
    public int scoreP2 { get { return this._scoreP2; } set { this._scoreP2 = value; } }

    public int scoreTemp { get { return this._scoreTemp; } set { this._scoreTemp = value; } }
    public int hitTemp { get { return this._hitTemp; } set { this._hitTemp = value; } }
    public int successTemp { get { return this._successTemp; } set { this._successTemp = value; } }
    
    public int streak { get { return this._streak; } set { this._streak = value; } }
    public int multiplier { get { return this._multiplier; } set { this._multiplier = value; } }
    public int hit { get { return this._hit; } set { this._hit = value; } }
    public int success { get { return this._success; } set { this._success = value; } }
    public int manche { get { return this._manche; } set { this._manche = value; } }





    public modele_game()
    {
    }
}
