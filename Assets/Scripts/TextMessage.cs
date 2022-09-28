using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMessage : MonoBehaviour {
    public float moveAmount = 1.5f;

    public void MoveUp()
    {
        this.transform.position += new Vector3(0, moveAmount, 0);
    }
}
