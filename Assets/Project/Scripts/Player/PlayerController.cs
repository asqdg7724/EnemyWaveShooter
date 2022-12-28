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
        // 벡터 값을 세팅 해줌
        movement.Set(h, 0f, v);

        // 오브젝트의 균일한 이동을 위하여 벡터를 정규화해준다
        movement = movement.normalized * speed * Time.deltaTime;

        // rigidbody를 이용해서 물체를 이동 시키기 위해 이 스크립트 객체의 포지션을 이용
        rb.MovePosition(transform.position + movement);
    }

    void Turn()
    {
        // 마우스 위치로 레이를 만들어줌
        // ScreenPointToRay를 이용하여 2D 화면을 클릭했을 때 3D 좌표계로 계산함
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(camRay, out hitInfo, camRayLength, floorMask))
        {
            // 캐릭터의 방향 계산
            Vector3 playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0;

            // 방향으로 회전시킨다.
            Quaternion rot = Quaternion.LookRotation(playerToMouse);

            // 회전값을 적용시킨다.
            rb.MoveRotation(rot);
        }

        // 레이가 잘 나오는지 확인하기 위하여 디버그 모드로 레이를 그려줌
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
