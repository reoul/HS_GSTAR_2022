using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDestroy : MonoBehaviour
{
    public void FireWorksDestroy()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
