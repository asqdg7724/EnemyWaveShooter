using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 0f;

    Vector3 movement;
    Animator animator;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Ground");
    }

    // Start is called before the first frame update
    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turn();
        MoveAnim(h, v);
    }

    void Move(float h, float v)
    {
        // ���� ���� ���� ����
        movement.Set(h, 0f, v);

        // ������Ʈ�� ������ �̵��� ���Ͽ� ���͸� ����ȭ���ش�
        movement = movement.normalized * speed * Time.deltaTime;

        // rigidbody�� �̿��ؼ� ��ü�� �̵� ��Ű�� ���� �� ��ũ��Ʈ ��ü�� �������� �̿�
        rb.MovePosition(transform.position + movement);
    }

    void Turn()
    {
        // ���콺 ��ġ�� ���̸� �������
        // ScreenPointToRay�� �̿��Ͽ� 2D ȭ���� Ŭ������ �� 3D ��ǥ��� �����
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(camRay, out hitInfo, camRayLength, floorMask))
        {
            // ĳ������ ���� ���
            Vector3 playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0;

            // �������� ȸ����Ų��.
            Quaternion rot = Quaternion.LookRotation(playerToMouse);

            // ȸ������ �����Ų��.
            rb.MoveRotation(rot);
        }

        // ���̰� �� �������� Ȯ���ϱ� ���Ͽ� ����� ���� ���̸� �׷���
        Debug.DrawRay(camRay.origin, camRay.direction * camRayLength, Color.red, 0.1f);
    }

    void MoveAnim(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
