using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GunController : MonoBehaviourPunCallbacks
{
    [Header("Weapon")]
    public Gun SelectedWeapon;
    private bool _canFire = true;
    private float _offset = 0.8f;
    private Rect _screenRect = new Rect(0,0, Screen.width, Screen.height);

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _zapSfx;

    [Header("References")]
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunBarrel;
    private Camera _playerCam;


    private void Start()
    {
        if (!photonView.IsMine) enabled = false;
        _playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        HandleGunRotation();
        HandleShootGun();
    }

    private void HandleGunRotation()
    {
        Vector3 difference = _playerCam.ScreenToWorldPoint(Input.mousePosition) - _pivotPoint.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _pivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + _offset);
    }

    private void HandleShootGun()
    {
        if (Input.GetMouseButton(0) && _canFire)
        {
            _canFire = false;
            Vector3 screenMousePos = Input.mousePosition;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);  
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction.Normalize();
            direction *= SelectedWeapon.BulletSpeed;

            Bullet bullet = Instantiate(_bulletPrefab, _gunBarrel.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Velocity = direction;

            _audioSource.PlayOneShot(_zapSfx);
            StartCoroutine(ReloadGun());
        }
    }

    private IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(SelectedWeapon.FireDelay);
        _canFire = true;
    }
}
