
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

[RequireComponent(typeof(PlayerInput))]
public class ActorController : MonoBehaviour
{
    [Header("-----player prefab------")]
    public GameObject player;

    [Header("-----player setting-----")]
    public float moveVelocity;
    public float runVelocity;
    public float jumpVelocity;

    [Header("--------physic material-----")]
    public PhysicMaterial fullFriction;
    public PhysicMaterial zeroFriction;

    private CapsuleCollider _capsuleCollider;

    private PlayerInput _inputSignal;

    private Animator _animator;
    private Rigidbody _rigidbody;

    private Vector3 _animMovePos;
    public Vector3 AnimMovePos
    {
        set { _animMovePos = value; }
        get { return _animMovePos; }
    }

    public PlayerInput InputSignal
    {
        get { return _inputSignal; }
    }

    private IMachine _machine;

    private readonly IPlayerState _groundState = new GroundState();
    private readonly IPlayerState _airState = new AirState();
    private readonly IPlayerState _attackState = new AttackState();


    private float _forwardValue;

    // Use this for initialization
    void Awake()
    {
        var inputList = GetComponents<PlayerInput>();
        foreach (var input in inputList)
        {
            if (!input.enabled) continue;
            _inputSignal = input;
            break;
        }

        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = player.GetComponent<Animator>();

        _machine = new PlayerFiniteStateMachine(player, this, _groundState, _inputSignal, _animator, _rigidbody);
    }

    // Update is called once per frame
    void Update()
    {

        _forwardValue = _inputSignal.SignalValueMagic * (_inputSignal.Run ? 3.0f : 4.0f);
        if (!_inputSignal.Run)
            _forwardValue = Mathf.Clamp(_forwardValue, 0, 1);

        _animator.SetFloat(ProjectConstant.AnimatorParameter.FORWARD,_forwardValue);

        if (_rigidbody.velocity.magnitude > 8f)
        {
            _animator.SetTrigger(ProjectConstant.AnimatorParameter.ROLL);
        }

        if (_inputSignal.Jump)
        {
            _animator.SetTrigger(ProjectConstant.AnimatorParameter.JUMP);
            _machine.TranslateTo(_airState);
        }

        if (_inputSignal.Attack && _machine.GetCurrentState() != _airState)
        {
            _animator.SetTrigger(ProjectConstant.AnimatorParameter.ATTACK);
            _machine.TranslateTo(_attackState);
        }


        _machine.Update();
    }


    private void FixedUpdate()
    {
        _machine.FixUpdate();
    }


    private void OnGroundEnter()
    {
        _machine.TranslateTo(_groundState);
    }

    private void OnGround()
    {
        _capsuleCollider.material = fullFriction;
        _animator.SetBool(ProjectConstant.AnimatorParameter.ON_GROUND, true);
    }

    private void NotOnGround()
    {
        _capsuleCollider.material = zeroFriction;
        _animator.SetBool(ProjectConstant.AnimatorParameter.ON_GROUND, false);
    }

    private void OnAttackStateUpdate()
    {
        _animator.SetLayerWeight(GetLayerIndex(ProjectConstant.AnimatorLayer.ATTACK),
            Mathf.Lerp(_animator.GetLayerWeight(GetLayerIndex(ProjectConstant.AnimatorLayer.ATTACK)), 1,
                0.1f));
    }

    private void OnIdleStateUpdate()
    {
        _animator.SetLayerWeight(GetLayerIndex(ProjectConstant.AnimatorLayer.ATTACK),
            Mathf.Lerp(_animator.GetLayerWeight(GetLayerIndex(ProjectConstant.AnimatorLayer.ATTACK)), 0,
                0.1f));
    }

    private void OnIdleStateEnter()
    {
        _machine.TranslateTo(_groundState);
    }

    private void AnimatorMove(object movePos)
    {
        _animMovePos += (Vector3) movePos;
    }

    private int GetLayerIndex(string layerName)
    {
        return _animator.GetLayerIndex(layerName);
    }


}
