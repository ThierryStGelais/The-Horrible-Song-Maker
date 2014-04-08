using UnityEngine;
using System.Collections;
using System.Timers;
using scMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;





public class modele_venyl
{

    private scObject _notes= new scObject("notes");
    private int _nbNotes = 0, _typeAccordSvt = 0;

    public scObject notes { get { return this._notes; } set { this._notes = value; } }
    private bool _note1 = false, _note2 = false, _note3 = false, _note4 = false;
    public bool note1 { get { return this._note1; } set { this._note1 = value; } }
    public bool note2 { get { return this._note2; } set { this._note2 = value; } }
    public bool note3 { get { return this._note3; } set { this._note3 = value; } }
    public bool note4 { get { return this._note4; } set { this._note4 = value; } }
    public int nbNotes { get { return this._nbNotes; } set { this._nbNotes = value; } }
    public int typeAccordSvt { get { return this._typeAccordSvt; } set { this._typeAccordSvt = value; } }


    public modele_venyl()
    {
    }
}
