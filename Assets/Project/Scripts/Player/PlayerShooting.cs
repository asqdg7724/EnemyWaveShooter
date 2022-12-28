using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damage = 10;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    private float timer = 0f;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private int layerMask;
    private LineRenderer shootLine;
    private Light gunLight;

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
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer = 0f;

        Debug.Log("�߻��");

        gunLight.enabled = true;

        shootLine.enabled = true;
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
                    eh.TakeDamage(damage, shootHit.point);
                }
            }
        }
    }
}
