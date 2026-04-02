using System.Collections;
using EscapeMandrilliaMod;
using UnityEngine;
using VRWeaponInteractor;
public class BananaLogic : MonoBehaviour
{
    public System.Action<RAGDOLLCONTROL, ExtendedCameraWalkerController, MANDRILLCAMERAOVERLAY> OnSlip;
    MANDRILLCAMERAOVERLAY _camera;
    RAGDOLLCONTROL _ragdol;
    ExtendedCameraWalkerController _deathcontroller;
    MANDRILLSCORING _BYGA;
    MANDRILL_HEALTH _health;
    OVERMANAGER _score;
    Plugin _mycentralmod;
    Rigidbody _rb;

    bool _DEVILMODE = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mycentralmod = FindObjectOfType<Plugin>();
        _score = transform.parent.parent.parent.GetComponent<OVERMANAGER>();
    }

    void Update()
    {
        if (_DEVILMODE && _BYGA.sharedLifes >= 1)
        {
            _BYGA.sharedLifes = 0;
            _BYGA.lifesCheck();
        }
        if (_DEVILMODE && _deathcontroller.berserking == false)
        {
            AudioSource _mandrilliasound = gameObject.GetComponent<AudioSource>();
            _BYGA.sharedLifes = 0;
            _BYGA.lifesCheck();
            _health.startBodyPoints = 1000;
            _health.startBrainPoints = 200;
            _health.startLeftArmPoints = 200;
            _health.startLeftLegPoints = 200;
            _health.startRightArmPoints = 200;
            _health.startRightLegPoints = 200;
            _health.poisonStatus = 0;

            _health.Heal(1000);
            _deathcontroller.berserking = true;
            _deathcontroller.attackPoints = 100;
            _deathcontroller.maxAttackPoints = 100;
            _deathcontroller.vocalCtrl.BerserkRoarSound(false);
            _deathcontroller.defaultMovementSpeed *= 2f;
            _deathcontroller.movementSpeed *= 2f;
            _deathcontroller.sprintingSpeed *= 2f;
            _deathcontroller.stealthSpeed *= 2f;
            _deathcontroller.jumpSpeed *= 2f;
            _deathcontroller.ventSprintSpeed *= 2f;
            _deathcontroller.defaultSprintSpeed *= 2f;
            _deathcontroller.stunnedMovementSpeed *= 2f;
            _mandrilliasound.clip = _mycentralmod._doomsound;
            _mandrilliasound.loop = true;
            _mandrilliasound.Play();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "banana(Clone)" && !_DEVILMODE)
        {
            Destroy(other.gameObject);
            _camera ??= FindObjectOfType<MANDRILLCAMERAOVERLAY>();
            _ragdol ??= FindObjectOfType<RAGDOLLCONTROL>();
            _deathcontroller ??= FindObjectOfType<ExtendedCameraWalkerController>();

            if (_camera != null && _ragdol != null &&  _deathcontroller != null)
            {
                _camera.ScreenDamage();
                _ragdol.RagdollOn();
                _deathcontroller.MandrillShock();
                AudioSource _mandrilliasound = gameObject.GetComponent<AudioSource>();
                _mandrilliasound.clip = _mycentralmod._soundbanana;
                _mandrilliasound.Play();

                StartCoroutine(time());
            }
        }

        if (other.name == "bluebanana(Clone)" && !_DEVILMODE)
        {
            Destroy(other.gameObject);
            _camera ??= FindObjectOfType<MANDRILLCAMERAOVERLAY>();
            _deathcontroller ??= FindObjectOfType<ExtendedCameraWalkerController>();

            if (_camera != null &&  _deathcontroller != null)
            {
                _camera.ScreenHeal();
                _deathcontroller.defaultMovementSpeed *= 2f;
                _deathcontroller.movementSpeed *= 2f;
                _deathcontroller.sprintingSpeed *= 2f;
                _deathcontroller.stealthSpeed *= 2f;
                _deathcontroller.jumpSpeed *= 2f;
                _deathcontroller.ventSprintSpeed *= 2f;
                _deathcontroller.defaultSprintSpeed *= 2f;
                _deathcontroller.stunnedMovementSpeed *= 2f;
                AudioSource _mandrilliasound = gameObject.GetComponent<AudioSource>();
                _mandrilliasound.clip = _mycentralmod._bluebananasound;
                _mandrilliasound.loop = false;
                _mandrilliasound.Play();

                StartCoroutine(theishowspeed());
            }
        }

        if (other.name == "redbanana(Clone)" && !_DEVILMODE)
        {
            Destroy(other.gameObject);
            _camera ??= FindObjectOfType<MANDRILLCAMERAOVERLAY>();
            _deathcontroller ??= FindObjectOfType<ExtendedCameraWalkerController>();

            if (_camera != null &&  _deathcontroller != null)
            {
                AudioSource _mandrilliasound = gameObject.GetComponent<AudioSource>();
                _deathcontroller.BerserkRoar();
                _mandrilliasound.clip = _mycentralmod._redbananasound;
                _mandrilliasound.loop = false;
                _mandrilliasound.Play();
            }
        }

        if (other.name == "bananaDEV(Clone)")
        {
            Destroy(other.gameObject);
            _DEVILMODE = true;
            _deathcontroller ??= FindObjectOfType<ExtendedCameraWalkerController>();
            _health = _deathcontroller.monkeyHealth;
            _BYGA = transform.parent.parent.GetComponent<MANDRILLSCORING>();

            if (_deathcontroller != null)
            {
                AudioSource _mandrilliasound = gameObject.GetComponent<AudioSource>();
                
                _BYGA.sharedLifes = 0;
                _BYGA.lifesCheck();
                _health.startBodyPoints = 1000;
                _health.startBrainPoints = 200;
                _health.startLeftArmPoints = 200;
                _health.startLeftLegPoints = 200;
                _health.startRightArmPoints = 200;
                _health.startRightLegPoints = 200;
                _health.poisonStatus = 0;

                _health.Heal(1000);
                _deathcontroller.berserking = true;
                _deathcontroller.attackPoints = 100;
                _deathcontroller.maxAttackPoints = 100;
                _deathcontroller.vocalCtrl.BerserkRoarSound(false);
                _deathcontroller.defaultMovementSpeed *= 2f;
                _deathcontroller.movementSpeed *= 2f;
                _deathcontroller.sprintingSpeed *= 2f;
                _deathcontroller.stealthSpeed *= 2f;
                _deathcontroller.jumpSpeed *= 2f;
                _deathcontroller.ventSprintSpeed *= 2f;
                _deathcontroller.defaultSprintSpeed *= 2f;
                _deathcontroller.stunnedMovementSpeed *= 2f;
                _mandrilliasound.clip = _mycentralmod._doomsound;
                _mandrilliasound.loop = true;
                _mandrilliasound.Play();
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "PLAYER" && _rb.velocity.magnitude >= 7f)
        {
            PLAYERBODY _health = other.gameObject.GetComponentInChildren<PLAYERBODY>();
            _health.Damage((int)_rb.velocity.magnitude);
            Debug.Log(_rb.velocity.magnitude);
        }
    }
    IEnumerator theishowspeed()
    {
        yield return new WaitForSeconds(5f);
        _deathcontroller.defaultMovementSpeed /= 2f;
        _deathcontroller.movementSpeed /= 2f;
        _deathcontroller.sprintingSpeed /= 2f;
        _deathcontroller.stealthSpeed /= 2f;
        _deathcontroller.jumpSpeed /= 2f;
        _deathcontroller.ventSprintSpeed /= 2f;
        _deathcontroller.defaultSprintSpeed /= 2f;
        _deathcontroller.stunnedMovementSpeed /= 2f;
    }
    IEnumerator time()
    {
        yield return new WaitForSeconds(3f);
        _ragdol.RagdollOff();
        _deathcontroller.MandrillShockEnd();
    }
}