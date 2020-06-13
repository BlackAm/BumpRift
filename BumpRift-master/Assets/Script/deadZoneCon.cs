using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZoneCon : MonoBehaviour
{

    public float endTime = 30.0f;
    private MoveShip moveship;

    // Start is called before the first frame update
    void Start()
    {

        moveship = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveShip>();
        Destroy(this.gameObject, endTime);
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }else if(other.tag == "Player")
        {
            moveship.SetDead();
        }
    }
}
