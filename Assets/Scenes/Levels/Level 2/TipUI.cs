using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUI : MonoBehaviour
{
    private float _countDown = 3f;
    public Canvas canvas;

    // Update is called once per frame
    private void Update()
    {
        if (_countDown > 0)
        {
            _countDown -= Time.deltaTime;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
