using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    //public GameObject bullet;
    //// 총알 발사 좌표
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
        //Ray를 시각화 위해 사용
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
        // 오프셋 좌표값을 랜덤 함수로 생성
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2) * 0.5f);
        // 텍스처의오프셋 값 설정
        muzzleFlash.material.mainTextureOffset = offset;
        // muzzleflash의 회전 변경
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);
        //muzzleflash 크기 조절
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }
}
