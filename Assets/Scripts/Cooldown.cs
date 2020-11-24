using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] Image _bar = null;
    [SerializeField] GameObject wActiveHUD = null;

    private void Update()
    {
        _bar.fillAmount += Time.deltaTime * 0.15f;

        if(_bar.fillAmount == 1) {
            this.gameObject.SetActive(false);

            wActiveHUD.SetActive(false);
        }
    }

    public void SetHealthBar(float amount)
    {
        _bar.fillAmount = amount;
    }
}
