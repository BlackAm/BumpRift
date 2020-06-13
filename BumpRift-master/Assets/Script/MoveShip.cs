using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveShip : MonoBehaviour
{
    public float speed = 15.0f; // 속력
    private Rigidbody rb, Erb; // 물리
    float moveHorizontal; // 가로이동
    float moveVertical; // 세로이동
    Vector3 movement; // 3차원 이동값
    public float MaxSpeed = 30.0f; // 최대 스피드
    Vector3 rotation; 
    Vector3 unrotation;
    Vector3 tempVec = new Vector3();
    int P_camdistance;
    private bool isDead = false; // 사망판정
    private bool isBooster = false; // 부스터를 사용 중인것인가
    private float score = 0;
    public GameObject ScorePanel;
    public Text ScoreText;
    private bool isFirstEnter = false;
    private UiScript uiscript;
    private float Gpower = 20f;


    List<string> animArray;
    Animation anim;
    int index = 0;

    public float Score
    {
        set
        {
            score += value;
        }
    }

    public void AnimationArray()
    {
        foreach (AnimationState state in anim)
        {
            animArray.Add(state.name);
            index++;
        }
    }

    public void setBooster()
    {
        isBooster = true;
    }

    public void SetDead()
    {
        isDead = true;
    }

    private void Start()
    {
        uiscript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UiScript>();
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animation>();
        animArray = new List<string>();
        AnimationArray();
    }

    public void PlusSpeed()
    {
        speed += 2;
        MaxSpeed += 5;
    }

    private IEnumerator BoosterTime()
    {
        yield return new WaitForSeconds(5f); //5초 기다리기
        MaxSpeed -= 10;
    }

    private IEnumerator dead()
    {
        isDead = false;
        ScorePanel.SetActive(true);
        ScoreText.text = score.ToString();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = false;
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (isBooster)
        {
            isBooster = false;
            MaxSpeed += 10;
            StartCoroutine(BoosterTime()); //0.0f초 후
        }
    /*
        mouseLog = new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z);
        this.transform.rotation = Quaternion.Euler(0,-Mathf.Atan2((mouseLog.y - transform.position.y), (mouseLog.x - transform.position.x)) * Mathf.Rad2Deg,0);

        Debug.Log(mouseLog);
        Debug.Log(this.transform.rotation);
        */
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (isDead)
        {
            anim.Play(animArray[0]);
            StartCoroutine(dead());
        }
    }
    void FixedUpdate()
    {
            rotation = new Vector3(0.0f, moveHorizontal, 0.0f) / 2;
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
            if (rb.velocity.x >= MaxSpeed)
            {
                tempVec.x = MaxSpeed;
                tempVec.y = rb.velocity.y;
                tempVec.z = rb.velocity.z;
                rb.velocity = tempVec;
            }
            if (rb.velocity.x <= -MaxSpeed)
            {
                tempVec.x = -MaxSpeed;
                tempVec.y = rb.velocity.y;
                tempVec.z = rb.velocity.z;
                rb.velocity = tempVec;

            }
            if (rb.velocity.z >= MaxSpeed)
            {
                tempVec.x = rb.velocity.x;
                tempVec.y = rb.velocity.y;
                tempVec.z = MaxSpeed;
                rb.velocity = tempVec;
            }
            if (rb.velocity.z <= -MaxSpeed)
            {
                tempVec.x = rb.velocity.x;
                tempVec.y = rb.velocity.y;
                tempVec.z = -MaxSpeed;
                rb.velocity = tempVec;
            }
            rb.AddForce(movement);
            rb.AddTorque(rotation);
        //Debug.Log(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFirstEnter)
        {
            isFirstEnter = true;
            this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        if(collision.collider.tag == "Emeny")
        {
            Erb = collision.gameObject.GetComponent<Rigidbody>();
            uiscript.Health = MaxSpeed/2;
            //튕기는 판정!
            //if(Erb.velocity.x <0 || Erb.velocity.z < 0)
            //{
            //    if (Erb.velocity.x < 0)
            //    {
            //        if (Erb.velocity.z < 0)
            //        {
            //            rb.AddForce(new Vector3(Erb.velocity.x - Gpower, Erb.velocity.y, Erb.velocity.z - Gpower), ForceMode.Acceleration);
            //        }
            //        else
            //        {
            //            rb.AddForce(new Vector3(Erb.velocity.x - Gpower, Erb.velocity.y, Erb.velocity.z + Gpower), ForceMode.Acceleration);
            //        }
            //    }
            //    else
            //    {
            //        rb.AddForce(new Vector3(Erb.velocity.x + Gpower, Erb.velocity.y, Erb.velocity.z - Gpower), ForceMode.Acceleration);
            //    }
            //}
            //else {
            //    rb.AddForce(new Vector3(Erb.velocity.x + Gpower, Erb.velocity.y, Erb.velocity.z + Gpower), ForceMode.Acceleration);
            //}
            if (Erb.velocity.x < 0 || Erb.velocity.z < 0)
            {
                if (Erb.velocity.x < 0)
                {
                    if (Erb.velocity.z < 0)
                    {
                        rb.AddForce(new Vector3(Erb.velocity.x - Gpower, Erb.velocity.y, Erb.velocity.z - Gpower), ForceMode.Impulse);
                    }
                    else
                    {
                        rb.AddForce(new Vector3(Erb.velocity.x - Gpower, Erb.velocity.y, Erb.velocity.z + Gpower), ForceMode.Impulse);
                    }
                }
                else
                {
                    rb.AddForce(new Vector3(Erb.velocity.x + Gpower, Erb.velocity.y, Erb.velocity.z - Gpower), ForceMode.Impulse);
                }
            }
            else
            {
                rb.AddForce(new Vector3(Erb.velocity.x + Gpower, Erb.velocity.y, Erb.velocity.z + Gpower), ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit))
        {
            isDead = true;
        }
    }
}