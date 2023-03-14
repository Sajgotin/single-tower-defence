using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void DestroyAtAnimEnd()
    {
        Destroy(this.gameObject);
    }
}
