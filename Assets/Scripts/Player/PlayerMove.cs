using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallMask = 1 << LayerMask.NameToLayer("Wall");
    }

    void Update()
    {
        GetPlayerMoveKeyInput();
        SetPlayerMoveVelocity();
        state = GetPlayerMoveState();
    }

    private void OnDrawGizmos()
    {
        if (transform == null) return;
        DrawGroundBoxCast();
        DrawGroundBoxCastLookAt();
    }

    internal enum State { UP, DOWN, LEFT, RIGHT }   // UP : Back(뒤), DOWN : Front(앞)
    internal State state = State.DOWN;

    private LayerMask wallMask;

    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;

    private KeyCode upAKey = KeyCode.UpArrow;
    private KeyCode downAKey = KeyCode.DownArrow;
    private KeyCode leftAKey = KeyCode.LeftArrow;
    private KeyCode rightAKey = KeyCode.RightArrow;

    public int up;
    public int down;
    public int left;
    public int right;

    public float moveSpeed = 3.9f;

    public bool IsMoveStop = false;

    /// <summary> 플레이어의 이동 키를 설정함. </summary>
    public void UpdateKeySettings(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        upKey = up;
        downKey = down;
        leftKey = left;
        rightKey = right;
    }
    /// <summary> 플레이어의 이동 키를 설정함. </summary>
    public void UpdateArrowKeySettings(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        upAKey = up;
        downAKey = down;
        leftAKey = left;
        rightAKey = right;
    }
    /// <summary> 플레이어의 이동 키 입력을 받음. </summary>
    private void GetPlayerMoveKeyInput()
    {
        if (!IsMoveStop)
        {
            up = Input.GetKey(upKey) || Input.GetKey(upAKey) ? 1 : 0;                     // ↑ 키를 누르면 1, 아니면 0
            down = Input.GetKey(downKey) || Input.GetKey(downAKey) ? -1 : 0;              // ↓ 키를 누르면 -1, 아니면 0
            left = Input.GetKey(leftKey) || Input.GetKey(leftAKey) ? -1 : 0;              // ← 키를 누르면 -1, 아니면 0
            right = Input.GetKey(rightKey) || Input.GetKey(rightAKey) ? 1 : 0;            // → 키를 누르면 1, 아니면 0
        }
        else
        {
            up = down = left = right = 0;
        }
    }
    /// <summary> 플레이어의 이동 방향과 속도를 설정함. </summary>
    private void SetPlayerMoveVelocity()
    {
        float speed = moveSpeed;

        bool boxup = GetGroundBoxCastSelect(transform.position, Vector2.up);          // true면 움직일 수 있음
        bool boxdown = GetGroundBoxCastSelect(transform.position, Vector2.down);
        bool boxleft = GetGroundBoxCastSelect(transform.position, Vector2.left);
        bool boxright = GetGroundBoxCastSelect(transform.position, Vector2.right);

        Vector2 dirNormal = GetDirNormalVector();

        if (dirNormal.x > 0 && dirNormal.y > 0)                         // 오른쪽 위
            MoveInDirection(dirNormal, boxup, boxright, speed);
        else if (dirNormal.x < 0 && dirNormal.y > 0)                    // 왼쪽 위
            MoveInDirection(dirNormal, boxup, boxleft, speed);
        else if (dirNormal.x > 0 && dirNormal.y < 0)                    // 오른쪽 아래
            MoveInDirection(dirNormal, boxdown, boxright, speed);
        else if (dirNormal.x < 0 && dirNormal.y < 0)                    // 왼쪽 아래
            MoveInDirection(dirNormal, boxdown, boxleft, speed);
        else                                                            // 상하좌우 단방향
        {
            if (GetGroundBoxCast())                                         // 충돌 없으면 이동 가능
                rb.velocity = GetDirVector() * speed;
            else                                                            // 충돌 있으면 이동 불가능
                rb.velocity = Vector2.zero;
        }

        void MoveInDirection(Vector2 DirectionNormal, bool boxPrimary, bool boxSecondary, float speed)      // 로컬 함수
        {
            if (boxPrimary && !boxSecondary)                                // 한 축만 충돌하면 다른 축은 이동 가능
                rb.velocity = new Vector2(0, DirectionNormal.y) * speed;
            else if (!boxPrimary && boxSecondary)                           // 한 축만 충돌하면 다른 축은 이동 가능
                rb.velocity = new Vector2(DirectionNormal.x, 0) * speed;
            else if (boxPrimary && boxSecondary)                            // 두 축 모두 충돌하지 않으면 자유롭게 이동 가능
                rb.velocity = DirectionNormal * speed;
            else                                                            // 두 축 모두 충돌하면 이동 불가능
                rb.velocity = Vector2.zero;
        }
    }

    /// <summary> 플레이어의 이동 상태를 반환함. </summary>
    private State GetPlayerMoveState()
    {
        if (rb.velocity.y > 0) return State.UP;
        else if (rb.velocity.y < 0) return State.DOWN;
        else if (rb.velocity.x > 0) return State.RIGHT;
        else if (rb.velocity.x < 0) return State.LEFT;
        else
        {
            if (GetDirVector().y > 0) return State.UP;
            else if (GetDirVector().y < 0) return State.DOWN;
            else if (GetDirVector().x > 0) return State.RIGHT;
            else if (GetDirVector().x < 0) return State.LEFT;
            else return state;
        }
    }

    /// <summary> 플레이어의 이동 방향을 정규 벡터로 반환함. </summary>
    private Vector2 GetDirNormalVector() => new Vector2(right + left, up + down).normalized;
    /// <summary> 플레이어의 이동 방향을 벡터로 반환함. </summary>
    private Vector2 GetDirVector() => new(right + left, up + down);
    /// <summary> 플레이어 Pivot의 중심을 기준으로 Ray Origin 위치를 반환. </summary>
    private Vector2 GetlocalPivotPos(Vector2 pivot) => pivot + GetDirVector() * 0.2f;
    /// <summary> 플레이어의 이동에 따라 Ray Origin 위치를 반환. </summary>
    private Vector2 GetRayOriginPos(Vector2 pivot)
    {
        if (GetDirVector() == Vector2.zero)
        {
            if (state == State.UP) return pivot + Vector2.up * 0.2f;
            else if (state == State.DOWN) return pivot + Vector2.down * 0.2f;
            else if (state == State.RIGHT) return pivot + Vector2.right * 0.2f;
            else if (state == State.LEFT) return pivot + Vector2.left * 0.2f;
            else return Vector2.zero;
        }
        else
            return GetlocalPivotPos(pivot);
    }
    /// <summary> 플레이어의 BoxCast Size를 반환. </summary>
    private Vector2 GetBoxCastSize() => new(0.48f, 0.48f);


    /// <summary> 플레이어의 BoxCol부터 이동하는 방향의 BoxCast 충돌 여부를 반환. </summary>
    private bool GetGroundBoxCast()
    {
        Vector2 origin = GetRayOriginPos(transform.position);
        RaycastHit2D hit = Physics2D.BoxCast(origin, GetBoxCastSize(), 0, Vector2.zero, 0, wallMask);
        return hit.collider == null;    // true면 움직일 수 있음.
    }
    /// <summary> 플레이어의 BoxCol부터 원하는 방향의 BoxCast 충돌 여부를 반환. </summary>
    private bool GetGroundBoxCastSelect(Vector2 pivot, Vector2 direction)
    {
        Vector2 origin = pivot + direction * 0.2f;
        RaycastHit2D hit = Physics2D.BoxCast(origin, GetBoxCastSize(), 0, Vector2.zero, 0, wallMask);
        return hit.collider == null;    // true면 움직일 수 있음.
    }


    /// <summary> 플레이어의 상하좌우 BoxCast를 그림. </summary>
    private void DrawGroundBoxCast()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + Vector2.up * 0.2f, GetBoxCastSize());
        Gizmos.DrawWireCube((Vector2)transform.position + Vector2.down * 0.2f, GetBoxCastSize());
        Gizmos.DrawWireCube((Vector2)transform.position + Vector2.left * 0.2f, GetBoxCastSize());
        Gizmos.DrawWireCube((Vector2)transform.position + Vector2.right * 0.2f, GetBoxCastSize());
    }
    /// <summary> 플레이어의 현재 바라보는 방향의 BoxCast를 그림. </summary>
    private void DrawGroundBoxCastLookAt()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetRayOriginPos(transform.position), GetBoxCastSize());
    }
}
