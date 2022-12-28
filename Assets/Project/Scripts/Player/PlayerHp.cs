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
    Animator animator;

    public bool isDead;
    public bool isDamage = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerCtrl = GetComponent<PlayerController>();

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamage)
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

        hp -= amount;

        hpSlider.value = hp;

        if (hp <= 0 && !isDead)
        {
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;

        playerCtrl.enabled = false;

        animator.SetTrigger("Dead");

        GameManager.gmg.OpenGameOver();
    }
}
