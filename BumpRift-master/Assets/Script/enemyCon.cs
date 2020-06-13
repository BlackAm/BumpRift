using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyCon : MonoBehaviour
{

    public enum enemystatuses
    {
        rest, chase, attack, shot, itemf
    }
    public enemystatuses enemycurstatuse = enemystatuses.rest;

    private Transform _transform;
    private Transform Ptransform;
    private NavMeshAgent nvagent;
    public float health = 100.0f;
    public float chaseline = 50.0f;
    public float attackline = 10.0f;
    private bool isDead = false;
    public GameObject[] items;
    public Transform itemTr;
    public float itemline = 13.0f;
    private bool itemchase;
    int i;
    private bool isBoosted;
    private Rigidbody rigid, Prigid;
    private float Gpower = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Prigid = collision.collider.gameObject.GetComponent<Rigidbody>();
            health -= collision.gameObject.GetComponent<MoveShip>().MaxSpeed + 10;
            collision.gameObject.GetComponent<MoveShip>().Score = 5;
            //if (Prigid.velocity.x < 0 || Prigid.velocity.z < 0)
            //{
            //    if (Prigid.velocity.x < 0)
            //    {
            //        if (Prigid.velocity.z < 0)
            //        {
            //            rigid.AddForce(new Vector3(Prigid.velocity.x - Gpower, Prigid.velocity.y, Prigid.velocity.z - Gpower), ForceMode.Acceleration);
            //        }
            //        else
            //        {
            //            rigid.AddForce(new Vector3(Prigid.velocity.x - Gpower, Prigid.velocity.y, Prigid.velocity.z + Gpower), ForceMode.Acceleration);
            //        }
            //    }
            //    else
            //    {
            //        rigid.AddForce(new Vector3(Prigid.velocity.x + Gpower, Prigid.velocity.y, Prigid.velocity.z - Gpower), ForceMode.Acceleration);
            //    }
            //}
            //else
            //{
            //    rigid.AddForce(new Vector3(Prigid.velocity.x + Gpower, Prigid.velocity.y, Prigid.velocity.z + Gpower), ForceMode.Acceleration);
            //}
            if (Prigid.velocity.x < 0 || Prigid.velocity.z < 0)
            {
                if (Prigid.velocity.x < 0)
                {
                    if (Prigid.velocity.z < 0)
                    {
                        rigid.AddForce(new Vector3(Prigid.velocity.x - Gpower, Prigid.velocity.y, Prigid.velocity.z - Gpower), ForceMode.Impulse);
                    }
                    else
                    {
                        rigid.AddForce(new Vector3(Prigid.velocity.x - Gpower, Prigid.velocity.y, Prigid.velocity.z + Gpower), ForceMode.Impulse);
                    }
                }
                else
                {
                    rigid.AddForce(new Vector3(Prigid.velocity.x + Gpower, Prigid.velocity.y, Prigid.velocity.z - Gpower), ForceMode.Impulse);
                }
            }
            else
            {
                rigid.AddForce(new Vector3(Prigid.velocity.x + Gpower, Prigid.velocity.y, Prigid.velocity.z + Gpower), ForceMode.Impulse);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody>();
        _transform = this.gameObject.GetComponent<Transform>();
        Ptransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvagent = this.gameObject.GetComponent<NavMeshAgent>();
        //items = GameObject.FindGameObjectsWithTag("Item");

        StartCoroutine(this.CheckStatus());
        StartCoroutine(this.CheckStatusForAction());
        
    }

    private void Update()
    {
        if (health <= 0)
            isDead = true;
        if (isDead)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator CheckStatus()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.3f);

            float dist = Vector3.Distance(Ptransform.position, _transform.position);
            i = 0;
            //Debug.Log(dist);
            //부스터구현
            //적 추적
            /*while (true)
            {
                i++;
                itemTr = items[i].GetComponent<Transform>();
                float itemdist = Vector3.Distance(itemTr.position, _transform.position);
                if (itemdist <30)
                {
                    itemchase = true;
                    break;
                }
            }*/
            if (health <= 0)
            {
                isDead = true;
            }
            if (dist < attackline)
            {
                enemycurstatuse = enemystatuses.attack;
            }
            else if (dist < chaseline)
            {
                enemycurstatuse = enemystatuses.chase;

            }
            else if (dist < itemline)
            {
                enemycurstatuse = enemystatuses.itemf;
            }
            else
            {
                enemycurstatuse = enemystatuses.rest;
            }
        }
    }
    IEnumerator CheckStatusForAction()
    {
        if (isDead)
        {

        }
        while (!isDead)
        {
            switch (enemycurstatuse)
            {
                case enemystatuses.rest:
                    if (this.nvagent.speed <= 0)
                    {
                        this.nvagent.speed = 0;
                    }
                    else
                    {
                        --this.nvagent.speed;
                    }
                    nvagent.Stop();
                    break;
                case enemystatuses.chase:
                    if (this.nvagent.speed >= 10)
                    {
                        this.nvagent.speed = 10;
                    }
                    else
                    {
                        ++this.nvagent.speed;
                    }
                    nvagent.destination = Ptransform.position;
                    nvagent.Resume();
                    break;
                case enemystatuses.attack:
                    if (this.nvagent.speed>=15)
                    {
                        this.nvagent.speed = 15;
                    }
                    else
                    {
                        ++this.nvagent.speed;
                    }
                    break;
                case enemystatuses.itemf:
                    if (this.nvagent.speed >= 15)
                    {
                        this.nvagent.speed = 15;
                    }
                    else
                    {
                        ++this.nvagent.speed;
                    }
                    break;

            }
            yield return null;
        }
    }

}
