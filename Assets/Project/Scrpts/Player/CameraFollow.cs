using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0f;
    // ī�޶�� Ÿ���� ����
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        
        // Vector3.Lerp(��������, ��������, �ð�)
        // => ���� ���� ������ �� ���� �ʰ� �÷��̾ ���󰣴�. (�ε巴�� ����)
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}