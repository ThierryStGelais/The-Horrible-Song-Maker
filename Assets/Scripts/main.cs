using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using scMessage;
using System.Security.Cryptography;
using System.Text;
using System.Timers;

public class main : MonoBehaviour
{
    private controler_game controler_game;
    private static main main_ref;
    public static main Main
    {
        get
        {
            return main_ref;
        }
    }

    private bool initialised = false;

    void Awake()
    {

        main_ref = this;
        DontDestroyOnLoad(transform.gameObject);
        controler_game = new controler_game(this);


        //
    }


    void Start()
    {
        Application.runInBackground = true;

    }
    
    void OnGUI()
    { 
        controler_game.ui_loop();
    }

    void Update()
    {
        controler_game.game_loop();
        loop_input();
    }

    void  FixedUpdate()
    {
        controler_game.rotate_loop();
    }

    private void loop_input()
    {
        if (Input.GetKeyDown("q"))
        {
            controler_game.keyinput("q");
        }
        if (Input.GetKeyDown("w"))
        {
            controler_game.keyinput("w");
        }
        if (Input.GetKeyDown("e"))
        {
            controler_game.keyinput("e");
        }
        if (Input.GetKeyDown("r"))
        {
            controler_game.keyinput("r");
        }
        if (Input.GetKeyDown("u"))
        {
            controler_game.keyinput("u");
        }
        if (Input.GetKeyDown("i"))
        {
            controler_game.keyinput("i");
        }
        if (Input.GetKeyDown("o"))
        {
            controler_game.keyinput("o");
        }
        if (Input.GetKeyDown("p"))
        {
            controler_game.keyinput("p");
        }
        if (Input.GetKeyDown("8"))
        {
            controler_game.keyinput("8");
        }
        if (Input.GetKeyDown("9"))
        {
            controler_game.keyinput("9");
        }
        if (Input.GetKeyDown("0"))
        {
            controler_game.keyinput("0");
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

    }

    private void OnApplicationQuit()
    {
        //controler_game.destroy();
    }


}