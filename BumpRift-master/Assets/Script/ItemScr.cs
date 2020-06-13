using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScr : MonoBehaviour
{
    private UiScript uiscript;
    private MoveShip moveship;
    
    string txt;
    private void Start()
    {
        uiscript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UiScript>();
        moveship = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveShip>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<MoveShip>().Score = 3;
            switch (Random.Range(0, 3))
            {
                case 0: txt = "체력회복"; uiscript.Healing(); break;
                case 1: txt = "속도증가"; moveship.PlusSpeed(); break;
                case 2: txt = "최대hp증가"; uiscript.PlusMaxHealth(); break;
                case 3: txt = "부스터!"; uiscript.SetNitro(true); break;
            }
            uiscript.SetItemText(txt);
            Destroy(this.gameObject);
        }
    }
}
