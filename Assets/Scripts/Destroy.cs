using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //used in animation event
    void DestroyAtAnimEnd()
    {
        Destroy(this.gameObject);
    }
}
