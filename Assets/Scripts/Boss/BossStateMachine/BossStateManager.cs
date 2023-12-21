using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossStateManager : MonoBehaviour
{
    public CheckBossDoorCollision _door;

    public Rigidbody _bossRigidbody;

    public ShakeScreen _shakeScreen;

    public Transform _bulletSpawnPoint;

    public GameObject _bulletPrefab;

    public ShootBullet _shootBullet;

    public Transform _rotateBoss;

    public float _bulletSpeed = 10;

    public bool _isInSecondPhase;

    public int _nbShoot;
    public int _alreadyShoot =0;
    public float _speed;

    BossBaseState _currentState;

    public BossAwaitPlayerState _awaitPlayer = new BossAwaitPlayerState();

    public BossIdleState _idleState = new BossIdleState();

    public BossStartLongJumpState _startLongJumpState = new BossStartLongJumpState();
    public BossInAirLongState _inAirLongState = new BossInAirLongState();
    public BossLongJumpLandState _longJumpLandState = new BossLongJumpLandState();

    public BossStartShortJumpState _startShortJumpState = new BossStartShortJumpState();
    public BossInAirShortState _inAirShortState = new BossInAirShortState();
    public BossShortJumpLandState _shortJumpLandState = new BossShortJumpLandState();

    public BossPrepareShootingState _prepareShootingState = new BossPrepareShootingState();
    public BossShootState _shootingState = new BossShootState();
    public BossTimeAfterShootingState _timeAfterShootingState = new BossTimeAfterShootingState();

    // Start is called before the first frame update
    void Start()
    {
        _shootBullet = GetComponent<ShootBullet>();
        _shakeScreen = GetComponentInChildren<ShakeScreen>();
        _bossRigidbody = GetComponent<Rigidbody>();
        _currentState = _awaitPlayer;
        _currentState.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _currentState.OnCollisionEnter(this,collision);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(BossBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
