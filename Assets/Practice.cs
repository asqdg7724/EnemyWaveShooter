using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    delegate void TestDelegate(int number);
    TestDelegate testDelegate;

    void Start()
    {
        testDelegate = Number;
        testDelegate(5);

        testDelegate = DoubleNumber;
        testDelegate(5);
    }

    void Number(int num)
    {
        Debug.Log($"숫자 : {num}");
    }

    void DoubleNumber(int num)
    {
        Debug.Log($"숫자 : {num * 2}");
    }

    void Sample()
    {
        Debug.Log("샘플");
    }
}
