using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionDemolition : MonoBehaviour
{

    public enum GameMode
    {
        idle,
        playing,
        levelEnd
    }

    static private MissionDemolition S; // a private singleton
    [Header("Set in Inspectator")]
    public Text uitLevel;       // text_level text
    public Text uitShots;       // the shots text
    public Text uitButton;      // the text on the ui button
    public Vector3 castlePos;   // the place to put castles
    public GameObject[] castles;    // all the castles
    [Header("Set dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();

    }

    void StartLevel()
    {
        if (castle != null)

        {
            Destroy(castle);

        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;

    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: "   + shotsTaken;


    }

    void update()
    {
        UpdateGUI();
        if ((mode == GameMode.levelEnd) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");

            Invoke("NextLevel", 2f);
        }

    }


    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;

        }
        StartLevel();
    }


    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
            uitButton.text = "Show Castle";
            break;

            case "Show Castle":
            FollowCam.POI = S.castle;
            uitButton.text = "Show Both";
            break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
                
        }
    }
            public static void ShotFired()
    {
        S.shotsTaken++;
    }

   }


    