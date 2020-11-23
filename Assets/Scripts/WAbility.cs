using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAbility : MonoBehaviour
{
    /*
     * Ability States:
     * 0 = Not Active; Not affected by ability
     * 1 = 3 Orbs left; Active
     * 2 = 2 Orbs left; Active
     * 3 = 1 Orb left; Active
     * 4 = Big Shield; Active
     * 5 = On Cooldown; Not affected by ability
     */

    [Header("W Ability State Images")]
    [SerializeField] GameObject orb1 = null;
    [SerializeField] GameObject orb2 = null;
    [SerializeField] GameObject orb3 = null;
    [SerializeField] GameObject BigShield = null;

    [Header("Diana")]
    [SerializeField] Character Diana = null;

    public int wAbilityState = 0;
    bool DianaHasOrbs = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && WAbleToActivate()) {
            StartCoroutine(PaleCascadeStart(Diana));
            Feedback();
        }
    }

    public bool WAbleToActivate()
    {
        if(wAbilityState == 0) {
            return true;
        }
        return false;
    }

    // The Orbs and Shield on Diana, not the ability's cooldown
    IEnumerator PaleCascadeStart(Character _Diana)
    {
        PaleCascade();

        yield return new WaitForSeconds(5);

        StopCascading();
    }

    void PaleCascade()
    {
        DianaHasOrbs = true;
        Diana.ChangeShieldHealthByAmount(30);
        wAbilityState += 1;

        Feedback();
    }

    void StopCascading()
    {
        orb1.SetActive(false);
        orb2.SetActive(false);
        orb3.SetActive(false);
        BigShield.SetActive(false);

        wAbilityState = 0;
        DianaHasOrbs = false;

        Diana.SetShieldHealth(0);
    }

    public void Feedback()
    {
        if(wAbilityState == 1) {
            orb1.SetActive(true);
            orb2.SetActive(true);
            orb3.SetActive(true);
        }

        if(wAbilityState == 4) {
            BigShield.SetActive(true);
        }

        // UI tie-in (gold bar that goes around ability)
    }
}