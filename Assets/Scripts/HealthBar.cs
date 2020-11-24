using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _bar = null;

    private void Update()
    {
        _bar.fillAmount += Time.deltaTime * 0.1f;
    }

    public void SetHealthBar(float amount)
    {
        _bar.fillAmount -= amount;
    }

    public bool IsEmpty()
    {
        if(_bar.fillAmount == 0) {
            return true;
        }
        return false;
    }
}