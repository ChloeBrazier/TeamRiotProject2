using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroClose : MonoBehaviour
{

    public GameObject introBox;

    public void Clicked()
    {
        Destroy(gameObject);
        Destroy(introBox);
    }

}
