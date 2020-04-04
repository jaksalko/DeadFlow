using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{

    public enum FireMode { Auto, Single , Burst };
    public FireMode fireMode;

    public Transform[] projectileSpawn;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public int burstCount;
    public int projectilesPerMag;
    public float reloadTime = .3f;
    public int damage = 1;

    [Header("Recoil")]
    public Vector2 kickMinMax = new Vector2(.05f,.2f);
    public Vector2 recoilAngleMinMax = new Vector2(3,5);
    public float recoilMoveSettleTime =.1f;
    public float recoilRotationSettleTime = .1f;



    [Header("Effects")]
    public Transform shell;
    public Transform shellEjection;
    MuzzleFlash muzzleflash;
    float nextShotTime;

    bool triggerReleasedSinceLastShot;
    int shotsRemainingInBurst;
    int projectilesRemainingInMag;
    bool isReloading;
    Vector3 recoilSmoothDampVelocity;
    float recoilAngle;
    float recoilRotSmoothDampVelocity;

   
    Text projectile_txt;
    Image weapon_img;



    void Start()
    {
        


        projectile_txt = GameObject.Find("BulletCount").GetComponent<Text>();
        weapon_img = GameObject.Find("Weapon").GetComponent<Image>();

        muzzleflash = GetComponent<MuzzleFlash>();
        shotsRemainingInBurst = burstCount;
        projectilesRemainingInMag = projectilesPerMag;
        projectile_txt.text = projectilesRemainingInMag + "/*"; // inifinite reload

    }

    private void LateUpdate()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, .1f);
        recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, .1f);
        //transform.localEulerAngles = transform.localEulerAngles + Vector3.left * recoilAngle; //has bug code with player.cs

        if(!isReloading && projectilesRemainingInMag == 0)
        {
            Reload(); 

        }
    }

    

    void Shoot()
    {

        if (!isReloading && Time.time > nextShotTime && projectilesRemainingInMag > 0)
        {
            if (fireMode == FireMode.Burst)
            {
                if (shotsRemainingInBurst == 0)
                {
                    return;
                }
                shotsRemainingInBurst--;
            }
            else if (fireMode == FireMode.Single)
            {
                if (!triggerReleasedSinceLastShot)
                {
                    return;
                }
            }

            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                if(projectilesRemainingInMag == 0)
                {
                    break;
                }
                projectilesRemainingInMag--;//총알소비
                nextShotTime = Time.time + msBetweenShots / 1000;
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;//총구번호에 따라 총알발사
                newProjectile.SetSpeed_Damage(muzzleVelocity,damage);

                projectile_txt.text = projectilesRemainingInMag + "/*"; // inifinite reload
            }

            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleflash.Activate();
            transform.localPosition -= Vector3.forward * Random.Range(kickMinMax.x,kickMinMax.y);
            recoilAngle += Random.Range(recoilAngleMinMax.x,recoilAngleMinMax.y);
            recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);
        }
    }
    public void Reload()
    {
        if(!isReloading && projectilesRemainingInMag != projectilesPerMag)//꽉차지 않은상태라면
            StartCoroutine(AnimateReload());
    }

    IEnumerator AnimateReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(.2f);
        float reloadSpeed = 1f / reloadTime;
        float percent = 0;
        Vector3 initalRot = transform.localEulerAngles;
        float maxReloadAngle = 30;
        while(percent < 1)//애니메이션 구문
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle , interpolation);
            transform.localEulerAngles = initalRot + Vector3.left * reloadSpeed;
            yield return null;
        }
        isReloading = false;
        projectilesRemainingInMag = projectilesPerMag;//탄창확보
        projectile_txt.text = projectilesRemainingInMag + "/*"; // inifinite reload
    }
    public void Aim(Vector3 aimPoint)
    {
        if(!isReloading)
            transform.LookAt(aimPoint);
    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;//단발을 적용하기 위해 한발당 false 로 바꿔줘서 한발밖에 못쏘게
    }

    public void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;//마우스를 놓으면 다시 true로 해줘서 다시누르면 한발 쏠수 있게
        shotsRemainingInBurst = burstCount;//마우스를 놓으면 버스트모드에서 최대 쏠수있는 연사력을 재충전해줌
    }


}