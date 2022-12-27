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

        Debug.Log("발사됨");

        gunLight.enabled = true;

        shootLine.enabled = true;
        shootLine.SetPosition(0, transform.position);

        // 레이가 시작되는 곳
        shootRay.origin = transform.position;
        // 레이의 방향 설정
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range))
        {

        }
    }
}
