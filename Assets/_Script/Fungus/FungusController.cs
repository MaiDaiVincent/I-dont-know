using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using static EventManager;

public abstract class FungusController : MonoBehaviour
{

    #region Variables

    public bool IsDashCoolingDown { get; set; }
    public bool IsDied { get; set; }
    public bool IsDashing { get; set; }
    public bool IsPressDash { get; set; }
    public bool IsUsingSkill { get; set; }


    public bool IsPressNA { get; set; }
    public bool IsNA_ing { get; set; }
    public bool CanNA => !IsNA_ing && !IsES_ing && !IsEB_ing;


    public bool IsPressES { get; set; }
    public bool IsES_ing { get; set; }
    public bool CanES => !IsES_ing && !IsEB_ing && FungusAttack.eSTimeIsCooling == 0;


    public bool IsPressEB { get; set; }
    public bool IsEB_ing { get; set; }
    public bool CanEB => !IsEB_ing && !IsES_ing && FungusAttack.eBTimeIsCooling == 0;

    public Vector2 MoveDirection { get; set; }
    public Vector2 LastDirection { get; set; }
    public Vector2 AttackDirection { get; set; }

    protected virtual bool CanMove => !IsDashing;
    protected bool CanDash => StaminaEnough && !IsDashing && !IsUsingSkill && !IsDashCoolingDown;
    protected bool StaminaEnough => fungusManager.fungusStamina.CurrentStamina >= FungusInfo.FungusData.dashStamina;

    public Rigidbody2D rb2d { get; private set; }
    public CapsuleCollider2D capsuleCollider2D { get; private set; }
    public FungusAniEvent fungusAniEvent { get; private set; }
    public CharacterAnimator fungusAnimator { get; private set; }
    public TargetDetector TargetDetector { get; private set; }

    public FungusHealth FungusHealth { get; private set; }
    public FungusStamina FungusStamina { get; private set; }
    public FungusInfoReader FungusInfo { get; private set; }
    public FungusAttack FungusAttack { get; private set; }


    public Action OnStopDashEvent;


    private Coroutine fungusDieCoroutine;
    private Coroutine stopDashCoroutine;
    private Coroutine dashCoolDownCoroutine;
    private Coroutine hurtCoroutine;

    protected CameraCollider cameraCollider => CameraCollider.Instance;
    protected FungusManager fungusManager => FungusManager.Instance;
    protected GameplayManager gameplayController => GameplayManager.Instance;
    #endregion


    protected virtual void Awake()
    {
        _GetComponents();
        _ListenEvents();
    }
    protected void OnEnable()
    {
        IsDashCoolingDown = false;
        IsDashing = false;
    }
    protected void Update()
    {
        UpdateInput();
        UpdateAnimation();
    }
    protected virtual void _GetComponents()
    {
        FungusStamina = GetComponent<FungusStamina>();
        FungusHealth = GetComponent<FungusHealth>();
        FungusInfo = GetComponent<FungusInfoReader>();
        FungusAttack = GetComponent<FungusAttack>();

        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        fungusAniEvent = GetComponentInChildren<FungusAniEvent>();
        fungusAnimator = GetComponentInChildren<CharacterAnimator>();
        TargetDetector = GetComponentInChildren<TargetDetector>();
    }
    protected virtual void _ListenEvents()
    {

        FungusHealth.OnTakeDamageEvent += OnTakeDamage;

        EventManager.onFungusDie += OnDied;

        fungusAniEvent.OnStartNA_SkillEvent.AddListener(() => OnStartNA_Skill());
        fungusAniEvent.OnEndNA_SkillEvent.AddListener(() => OnEndNA_Skill());

        fungusAniEvent.OnStartES_SkillEvent.AddListener(() => OnStartES_Skill());
        fungusAniEvent.OnEndES_SkillEvent.AddListener(() => OnEndES_Skill());

        fungusAniEvent.OnStartEB_SkillEvent.AddListener(() => OnStartEB_Skill());
        fungusAniEvent.OnEndEB_SkillEvent.AddListener(() => OnEndEB_Skill());
    }

    protected virtual void _RemoveEvents() { }

    protected virtual void UpdateInput()
    {
        if (IsDied) return;


        if (IsUsingSkill)
        {
            rb2d.velocity = new Vector2(MoveDirection.x, MoveDirection.y);
            return;
        }

        Skill();
        Move();
        Dash();
    }
    protected virtual void UpdateAnimation()
    {
        fungusAnimator.SetMoveDirection(MoveDirection);

        fungusAnimator.SetLastMoveDirection(LastDirection);

        fungusAnimator.SetAttackDirection(AttackDirection);

        fungusAnimator.SetMoveState(MoveDirection != Vector2.zero);
    }

    protected virtual void Move()
    {
        float pressMoveX = Input.GetAxisRaw("Horizontal");
        float pressMoveY = Input.GetAxisRaw("Vertical");
        if (CanMove)
        {
            bool isIdle = pressMoveX == 0 && pressMoveY == 0;
            if (isIdle && (MoveDirection.x != 0 || MoveDirection.y != 0))
            {
                LastDirection = MoveDirection;
            }

            float moveSpeed = FungusInfo.FungusData.moveSpeed;
            rb2d.velocity = new Vector2(pressMoveX * moveSpeed, pressMoveY * moveSpeed);
            MoveDirection = new Vector2(pressMoveX, pressMoveY).normalized;
            fungusAnimator.SetMoveDirection(MoveDirection);
        }
    }
    protected virtual void Dash()
    {
        IsPressDash = Input.GetMouseButtonDown(1);
        if (IsPressDash && CanDash)
        {
            if (MoveDirection == Vector2.zero)
            {
                SetDirectionInit();
            }
            rb2d.velocity = MoveDirection.normalized * FungusInfo.FungusData.dashForce;

            fungusManager.fungusStamina.CurrentStamina -= FungusInfo.FungusData.dashStamina;

            if (stopDashCoroutine != null) StopCoroutine(stopDashCoroutine);
            stopDashCoroutine = StartCoroutine(StopDashCoroutine());
        }

    }
    public void SetDirectionInit()
    {
        if (LastDirection.x == 0 && LastDirection.y == 0) 
            LastDirection = new Vector2(0, -1);
        
        MoveDirection = LastDirection;
    }
    IEnumerator StopDashCoroutine()
    {
        IsDashing = true;
        fungusManager.InteractSlotState(false);

        yield return new WaitForSeconds(FungusInfo.FungusData.dashTime);

        IsDashing = false;
        fungusManager.InteractSlotState(true);

        if (dashCoolDownCoroutine != null) StopCoroutine(dashCoolDownCoroutine);
        dashCoolDownCoroutine = StartCoroutine(DashCoolDownCoroutine());
    }

    IEnumerator DashCoolDownCoroutine()
    {
        IsDashCoolingDown = true;
        yield return new WaitForSeconds(PlayerConfig.dashCoolDown);       
        IsDashCoolingDown = false;
    }


    protected virtual void Skill()
    {
        IsPressNA = Input.GetMouseButtonDown(0);
        IsPressES = Input.GetKeyDown(KeyCode.E);
        IsPressEB = Input.GetKeyDown(KeyCode.Q);

        NA(); ES(); EB();
    }

    protected virtual void NA()
    {
        if (IsPressNA && CanNA)
        {
            if (cameraCollider.CheckTargetDetection() != null)
            {
                Vector3 targetPos = cameraCollider.GetTargetTransform().position;

                Vector3 direction = (targetPos - transform.position).normalized;

                AttackDirection = direction;
                LastDirection = AttackDirection;
            }
            else
            {
                SetAttackDirectionWithOutTarget();
            }
            fungusAnimator.SetTriggerNA();
        }
    }

    protected virtual void ES()
    {
        if (IsPressES && CanES)
        {
            if (TargetDetector.Target() != null)
            {
                Vector3 targetPos = TargetDetector.Target().position;

                Vector3 direction = (targetPos - transform.position).normalized;

                AttackDirection = direction;
                LastDirection = AttackDirection;
            }
            else
            {
                SetAttackDirectionWithOutTarget();
            }
            fungusAnimator.SetTriggerES();
            IsNA_ing = false;
        }
    }

    protected virtual void EB()
    {
        if (IsPressEB && CanEB)
        {
            if (TargetDetector.Target() != null)
            {
                Vector3 targetPos = TargetDetector.Target().position;

                Vector3 direction = (targetPos - transform.position).normalized;

                AttackDirection = direction;
                LastDirection = AttackDirection;
            }
            else
            {
                SetAttackDirectionWithOutTarget();
            }
            fungusAnimator.SetTriggerEB();
            IsNA_ing = false;
        }
    }

    public void SetAttackDirectionWithOutTarget()
    {
        if (MoveDirection.x == 0 && MoveDirection.y == 0)
        {
            AttackDirection = LastDirection;
        }
        else
        {
            AttackDirection = LastDirection = MoveDirection;
        }
    }

    public Vector3 DirectionAttackWithOutTarget()
    {
        Vector3 direction;
        if (LastDirection.x == 0 && LastDirection.y == 0) 
            direction = new Vector2(0, -1);

        else if (MoveDirection.x == 0 && MoveDirection.y == 0)
            direction = new Vector2(LastDirection.x, LastDirection.y);

        else direction = MoveDirection;

        return direction;
    }
    public IEnumerator FungusDieCoroutine()
    {

        IsDied = true;
        DiedState();

        int fungusAliveIndex = fungusManager.GetFungusAliveIndex();
        if (fungusAliveIndex >= 0)
        {
            yield return new WaitForSeconds(PlayerConfig.dyingWaitTime);

            fungusManager.SwitchFungus(fungusAliveIndex);
        }
        else
        {
            Debug.Log("Game over");
        }
    }

    public IEnumerator HurtCoroutine()
    {
        FungusInfo.model.color = FungusInfo.hurtColor;
        yield return new WaitForSeconds(0.1f);
        FungusInfo.model.color = Color.white;

    }

    #region Set State
    public void DiedState()
    {
        MoveDirection = Vector2.zero;
        rb2d.velocity = MoveDirection;    

        capsuleCollider2D.enabled = false;
        fungusAnimator.SetTriggerDie();

    }
    public void ResetState()
    {
        IsDashing = false;
        IsDied = false;
        capsuleCollider2D.enabled = true;
    }

    public void UsingSkillState(bool state)
    {
        IsUsingSkill = state;
        fungusManager.InteractSlotState(!state);
    }
    #endregion


    #region On Event
    void OnTakeDamage(int value)
    {
        if (hurtCoroutine != null) StopCoroutine(hurtCoroutine);
        hurtCoroutine = StartCoroutine(HurtCoroutine());
    }
    protected virtual void OnDied()
    {
        if (gameObject.activeSelf)
        {
            if (fungusDieCoroutine != null) StopCoroutine(fungusDieCoroutine);
            fungusDieCoroutine = StartCoroutine(FungusDieCoroutine());
        }
    }

    //Event khi dùng kỹ năng đánh thường
    protected virtual void OnStartNA_Skill()
    {
        NA_State(true);
    }

    protected virtual void OnEndNA_Skill()
    {
        FungusAttack.NA_Skill();
        NA_State(false);
    }

    //Event khi dùng kỹ năng nguyên tố

    protected virtual void OnStartES_Skill()
    {
        ES_State(true);
    }

    protected virtual void OnEndES_Skill()
    {
        FungusAttack.ES_Skill();
        ES_State(false);
    }

    //Event khi dùng kỹ năng nộ

    public virtual void OnStartEB_Skill()
    {
        EB_State(true);
    }

    public virtual void OnEndEB_Skill()
    {
        FungusAttack.EB_Skill();
        EB_State(false);
    }

    //trạng thái skill
    public void NA_State(bool state)
    {
        IsNA_ing = state;
        UsingSkillState(state);
    }
    public void ES_State(bool state)
    {
        IsES_ing = state;
        UsingSkillState(state);
    }
    public void EB_State(bool state)
    {
        IsEB_ing = state;
        UsingSkillState(state);
    }
    #endregion
}
