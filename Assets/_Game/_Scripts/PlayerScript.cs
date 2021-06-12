using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] WeaponData defaultWeapon;
    [SerializeField] AnimationCurve glitchCurve;

    private Camera cam; //Main Camera Ref
    private WeaponData currentWeapon;
    private Transform childFx; 
    private DigitalGlitch glitchFx; //glitch effects

    void Start()
    {
        cam = GetComponent<Camera>(); //Get Camera Ref
        glitchFx = GetComponent<DigitalGlitch>();
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null)
            currentWeapon.WeaponUpdate();
    }

    public void SwitchWeapon(WeaponData weapon = null)
    {
        currentWeapon = weapon != null? weapon: defaultWeapon; //if no weapon, switch to default weapon
        currentWeapon.SetupWeapon(cam, this);
    }

    public void SetMuzzleFx(Transform fx)
    {
        if (childFx != null)
            Destroy(childFx.gameObject);

        fx.SetParent(transform);
        childFx = fx;
    }

    IEnumerator DoCameraShake(float timer, float amp, float freq)
    {
        Vector3 initPos = transform.position;
        Vector3 newPos = transform.position;
        float duration = timer;
        
        yield return new WaitForSeconds(0.2f);

        while(duration > 0f)
        {
            if ((newPos - transform.position).sqrMagnitude < 0.01f)
            {
                newPos = initPos;
                newPos.x += Random.Range(-1f, 1f) * amp;
                newPos.y += Random.Range(-1f, 1f) * amp;
            }

            transform.position = Vector3.Lerp(transform.position, newPos, freq * Time.deltaTime);

            glitchFx.intensity = glitchCurve.Evaluate(duration / timer);

            duration -= Time.deltaTime;
            yield return null;
        }

        transform.position = initPos;
    }

    public void ShakeCamera(float timer, float amp, float freq)
    {
        StartCoroutine(DoCameraShake(timer, amp, freq));
    }
}
