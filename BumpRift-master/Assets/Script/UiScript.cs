using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    #region SerializeField Init
    [SerializeField] GameObject Panel;
    [SerializeField] Image HPbar;
    [SerializeField] Image ItemImage;
    [SerializeField] Sprite NitroImage;
    [SerializeField] Sprite NoNitroImage;
    [SerializeField] Text timeText;
    [SerializeField] float MaxHealth;
    [SerializeField] float time = 300;
    #endregion

    #region Local Variables, Instances Init
    public Text ItemText, Hp_Text;
    private bool Nitro;
    private float health;
    MoveShip moveShip;
    public float Health
    {
        set
        {
            health -= value;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        moveShip = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveShip>();
    }

    public void SetItemText(string txt)
    {
        ItemText.text = txt;
    }

    private void Update()
    {        
        UpdateHealthBar();
        ItemImage.sprite = Nitro ? NitroImage : NoNitroImage;

        if (Input.GetKeyDown(KeyCode.Space) && Nitro)
        {
            moveShip.setBooster();
            Nitro = false;
        }
    }

    private void FixedUpdate()
    {        
        time -= Time.deltaTime;

        int minutes = (int)(time / 60); //Divide the guiTime by sixty to get the minutes.
        int seconds = (int)(time - minutes * 60);//Use the euclidean division for the seconds.

        //update the label value
        if (time > 0)
        {
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        else
        {
            moveShip.SetDead();
        }
        //ApplyDamage();        
    }    

    void UpdateHealthBar()
    {
        if (health <= 0) moveShip.SetDead();
        float percentage = health * 1f / MaxHealth;
        Hp_Text.text = string.Format("{0} / {1:000}", health, MaxHealth);
        HPbar.fillAmount = percentage;
    }

    public void Healing()
    {
        health = (health + 30 > MaxHealth) ? health + 30 : MaxHealth;
    }

    public void PlusMaxHealth()
    {
        MaxHealth += 20;
    }

    public void SetNitro(bool nit)
    {
        Nitro = nit;
    }
}
