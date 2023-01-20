using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public int maxHp = 100;
    public int hp;
    public Slider hpSlider;
    public Image dmgImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    private PlayerController playerCtrl;
    private GameManager gmg;
    private SoundPlayer soundPlayer;
    Animator animator;

    public bool isDead;
    public bool isDamage = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerCtrl = GetComponent<PlayerController>();
        soundPlayer = GetComponent<SoundPlayer>();
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = hp;

        if (hp >= maxHp)
        {
            hp = maxHp;
        }

        if (isDamage && !isDead)
        {
            dmgImage.color = flashColor;
        }

        else
        {
            dmgImage.color = Color.Lerp(dmgImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamage = false;
    }

    public void TakeDamage(int amount)
    {
        isDamage = true;

        soundPlayer.SoundPlay(1);

        hp -= amount;

        if (hp <= 0 && !isDead)
        {
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;

        playerCtrl.enabled = false;

        soundPlayer.SoundPlay(2);

        animator.SetTrigger("Dead");

        GameManager.gmg.OpenGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Aid")
        {
            hp = hp + 20;

            other.gameObject.SetActive(false);
        }
    }
}
