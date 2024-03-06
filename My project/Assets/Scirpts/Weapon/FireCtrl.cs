using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    //public GameObject bullet;
    //// �Ѿ� �߻� ��ǥ
    public Transform firepos;
    public AudioClip firSFX;

    private new AudioSource audio;
    private MeshRenderer muzzleFlash;
    
    private RaycastHit hit;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firepos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }
    private void Update()
    {
        //Ray�� �ð�ȭ ���� ���
        Debug.DrawRay(firepos.position, firepos.forward * 10.0f, Color.green);

        if (Input.GetMouseButton(0))
        {
            Fire();
            if (Physics.Raycast(firepos.position,firepos.forward,10.0f,1<<6))
            {
                Debug.Log($"Hit={hit.transform.name}");
                //hit.transform.GetComponent<BattleSystem>()?.OnDamage(hit.point, 0.0f, hit.normal,bool);
            }
        }
    }
    void Fire()
    {
       // Instantiate(bullet, firepos.position, firepos.rotation);
        audio.PlayOneShot(firSFX, 1.0f);
        
        StartCoroutine(ShowMuzzleFlash());
    }
    IEnumerator ShowMuzzleFlash()
    {
        // ������ ��ǥ���� ���� �Լ��� ����
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2) * 0.5f);
        // �ؽ�ó�ǿ����� �� ����
        muzzleFlash.material.mainTextureOffset = offset;
        // muzzleflash�� ȸ�� ����
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);
        //muzzleflash ũ�� ����
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }
}
