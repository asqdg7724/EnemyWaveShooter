using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damage = 10;
    public float timeBetweenBullets = 0.15f;
    public float effectsDisplayTime = 0.05f;
    public float range = 100f;

    private float timer = 0f;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private int layerMask;
    private LineRenderer shootLine;
    private Light gunLight;

    GameObject enemy;
    private EnemyHealth eh;

    // Start is called before the first frame update
    void Awake()
    {
        shootLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        layerMask = LayerMask.GetMask("Shootable");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        // deltaTime�� ������Ŵ���ν�, �ܼ��� �帥 �ð��� ���
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer > effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void Shoot()
    {
        timer = 0f;

        Debug.Log("�߻��");
        
        shootLine.SetPosition(0, transform.position);

        // ���̰� ���۵Ǵ� ��
        shootRay.origin = transform.position;
        // ������ ���� ����
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, layerMask))
        {
            shootLine.SetPosition(1, shootHit.point);

            if (shootHit.collider.tag != null)
            {
                if (shootHit.collider.tag == "Enemy")
                {
                    var eh = shootHit.collider.GetComponent<EnemyHealth>();

                    if (eh != null)
                    {
                        eh.TakeDamage(damage, shootHit.point);
                        Debug.Log("����!");
                    }
                }
            }
        }

        EnableEffects();
    }

    private void EnableEffects()
    {
        gunLight.enabled = true;

        shootLine.enabled = true;
    }

    private void DisableEffects()
    {
        gunLight.enabled = false;

        shootLine.enabled = false;
    }

    
}
