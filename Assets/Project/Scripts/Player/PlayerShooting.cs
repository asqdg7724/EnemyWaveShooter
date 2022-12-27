using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damage = 10;
    public float shootDelay = 0.15f;
    public float range = 100f;

    private float timer;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private int shootMask;
    private LineRenderer shootLine;
    private Light gunLight;

    // Start is called before the first frame update
    void Awake()
    {
        shootLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && timer >= shootDelay && Time.timeScale == 0)
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

        if (Physics.Raycast(shootRay, out shootHit, range))
        {

        }
    }
}
