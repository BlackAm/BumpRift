using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon : MonoBehaviour
{
    public GameObject A;  //A라는 GameObject변수 선언
    Transform AT;
    public int camdistance = 40;

    void Start()
    {
        AT = A.transform;
    }
    void Update()
    {
        this.transform.position = new Vector3(AT.position.x, AT.position.y + camdistance, AT.position.z);
    }
}
