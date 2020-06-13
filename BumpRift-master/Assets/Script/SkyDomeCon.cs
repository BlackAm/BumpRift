using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDomeCon : MonoBehaviour
{
    public float GetSmallStartTime = 10f;
    public float GetSmallScale = 0.003f;
    private Transform tr;
    int smallLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetSmallStartTime <= Time.realtimeSinceStartup)
        {
            tr.localScale = new Vector3(tr.localScale.x - GetSmallScale, tr.localScale.y, tr.localScale.z - GetSmallScale);
            GetSmallStartTime += 90;
            smallLevel++;
        }
    }
}
