using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    [SerializeField] Character Diana = null;
    [SerializeField] WAbility wAbility = null;

    private void Update()
    {
        transform.RotateAround(Diana.gameObject.transform.position, Diana.gameObject.transform.up, 100 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Diana.DealDamage(10, other.gameObject);

            this.gameObject.GetComponent<ParticleSystem>().Play();

            wAbility.wAbilityState += 1;

            if (wAbility.wAbilityState == 4) {
                wAbility.Feedback();
                Diana.ChangeShieldHealthByAmount(30);
            }

            this.gameObject.SetActive(false);
        }
    }
}