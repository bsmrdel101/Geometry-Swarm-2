using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviourPunCallbacks
{
    [Header("Weapon")]
    private bool _canFire = true;
    private float _offset = 0.8f;
    public Gun SelectedWeapon;
    private Rect _screenRect = new Rect(0, 0, Screen.width, Screen.height);

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _zapSfx;

    [Header("Cursors")]
    [SerializeField] private Sprite _defaultCrosshair;
    [SerializeField] private Sprite _reloadCrosshair;

    [Header("References")]
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunBarrel;
    private Image _cursorObj;
    private Camera _playerCam;


    private void Start()
    {
        if (!photonView.IsMine) enabled = false;
        _playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        SetGunOffset();
    }

    private void Update()
    {
        if (!_screenRect.Contains(Input.mousePosition)) return;
        HandleGunRotation();
        HandleShootGun();
    }

    private void HandleGunRotation()
    {
        Vector3 dir = _playerCam.ScreenToWorldPoint(Input.mousePosition) - _pivotPoint.position;
        dir.Normalize();
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _pivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    private void SetGunOffset()
    {
        _gunPos.position = new Vector2(_offset, _gunPos.position.y);
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
