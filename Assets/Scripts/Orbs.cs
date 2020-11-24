using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    [SerializeField] Character Diana = null;
    [SerializeField] WAbility wAbility = null;
    [SerializeField] ParticleSystem particles = null;


    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    private void Update()
    {
        transform.RotateAround(Diana.gameObject.transform.position, Diana.gameObject.transform.up, 350 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            wAbility.OrbDied(other.gameObject);

            particles.Play();
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}