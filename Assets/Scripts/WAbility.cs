using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAbility : MonoBehaviour
{
    [Header("W Ability State Feedback")]
    [SerializeField] GameObject orb1 = null;
    [SerializeField] GameObject orb2 = null;
    [SerializeField] GameObject orb3 = null;
    [SerializeField] GameObject BigShield = null;

    [Header("UI")]
    [SerializeField] GameObject wActiveHUD = null;
    [SerializeField] HealthBar manaBar = null;
    [SerializeField] Cooldown cooldown = null;

    [Header("Audio")]
    [SerializeField] AudioSource orbSound1 = null;
    [SerializeField] AudioSource orbSound2 = null;
    [SerializeField] AudioSource shieldSound = null;
    [SerializeField] AudioSource endOfAbilitySound = null;

    [Header("Diana")]
    [SerializeField] Character Diana = null;

    public enum wAS
    {
        NotActive,
        ThreeOrbs,
        TwoOrbs,
        OneOrb,
        BigShield,
        OnCooldown
    }

    public wAS wAbilityState = wAS.NotActive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && WAbleToActivate()) {
            StartCoroutine(PaleCascadeStart(Diana));
        }
    }

    public bool WAbleToActivate()
    {
        if(wAbilityState == wAS.NotActive && !manaBar.IsEmpty()) {
            return true;
        }
        return false;
    }

    IEnumerator PaleCascadeStart(Character _Diana)
    {
        PaleCascade();

        yield return new WaitForSeconds(5);
        
        cooldown.gameObject.SetActive(true);

        StopCascading();

        yield return new WaitForSeconds(7);

        wAbilityState = wAS.NotActive;
    }

    void PaleCascade()
    {
        manaBar.GetComponent<HealthBar>().SetHealthBar(0.9f);
        Diana.ChangeShieldHealthByAmount(30);
        wAbilityState = wAS.ThreeOrbs;
        wActiveHUD.SetActive(true);

        ManageOrbs();
    }

    void StopCascading()
    {

        orb1.SetActive(false);
        orb2.SetActive(false);
        orb3.SetActive(false);
        BigShield.SetActive(false);

        wAbilityState = wAS.OnCooldown;

        Diana.SetShieldHealth(0);

        endOfAbilitySound.Play();
    }

    public void OrbDied(GameObject collider)
    {
        switch (wAbilityState) {
            case wAS.ThreeOrbs:
                wAbilityState = wAS.TwoOrbs;
                Diana.DealDamage(10, collider);
                orbSound1.Play();
                break;

            case wAS.TwoOrbs:
                wAbilityState = wAS.OneOrb;
                Diana.DealDamage(10, collider);
                orbSound2.Play();
                break;

            case wAS.OneOrb:
                wAbilityState = wAS.BigShield;
                Diana.DealDamage(10, collider);
                shieldSound.Play();
                ManageOrbs();
                Diana.ChangeShieldHealthByAmount(30);
                break;

            default:
                Debug.Log("OrbDied unexpected value  " + wAbilityState);
                wAbilityState = wAS.OnCooldown;
                break;
        }
    }

    public void ManageOrbs()
    {
        if(wAbilityState == wAS.ThreeOrbs) {
            orb1.SetActive(true);
            orb2.SetActive(true);
            orb3.SetActive(true);
        }

        if(wAbilityState == wAS.BigShield) {
            BigShield.SetActive(true);
        }
    }
}