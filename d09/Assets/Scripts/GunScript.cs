using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private RaycastHit raycastHit;
    private AudioSource audioSource;
    public float shootRate = 0.5f;
    public GameObject impactParticles;
    public AudioClip shootAudioClip;
    public Material mat;
    new public GameObject camera;
    private bool canShoot = true;
    public int damages = 1;
    public string key = "1";
    public GameObject otherWeapon;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camera = Camera.main.gameObject;
    }

    void CreateLine(Vector3 begin, Vector3 end)
    {
        GameObject line = new GameObject();
        line.transform.position = transform.position;
        line.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.material = mat;
        lineRenderer.SetPosition(0, begin);
        lineRenderer.SetPosition(1, end);
        Destroy(line, 0.5f);
    }

    private IEnumerator Shoot()
    {
        GameObject particles;

        canShoot = false;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit))
        {
            particles = Instantiate(impactParticles, raycastHit.point, transform.rotation);
            CreateLine(transform.position, raycastHit.point);
            Destroy(particles, 0.2f);
        }
        audioSource.PlayOneShot(shootAudioClip);
        yield return new WaitForSeconds(shootRate);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(key))
        {
            otherWeapon.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
