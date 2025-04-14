using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_Hotkey_Controller : MonoBehaviour
{
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    private KeyCode myHotKey;
    private TextMeshProUGUI myText => GetComponentInChildren<TextMeshProUGUI>();

    private Blackhole_Skill_Controller blackHole;
    private Transform myEnemy;

    public void SetupHotKey(KeyCode _myNewHotKey,Transform _myEnemy,Blackhole_Skill_Controller _blackHole)
    {
        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();

        myEnemy = _myEnemy;
        blackHole = _blackHole;
    }
    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(myEnemy);

            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
