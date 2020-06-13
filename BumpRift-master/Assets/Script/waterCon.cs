using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCon : MonoBehaviour
{
    public float GetSmallStartTime = 180f;
    public float GetSmallScale = 0.1f;
    private Transform tr;
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
        }
    }
}
