using UnityEngine;

public class SPAnimatorManager : MonoBehaviour
{
    public int JumpCount { get; set; }
    public float Speed { get; set; }
    public float XAxis { get; set; }
    public float ZAxis { get; set; }
    public int PickupCount { get; set; }
    public int WieldCount { get; set; }
    public int TwoHandWieldCount { get; set; }

    private void Awake()
    {
        JumpCount = 0;
        PickupCount = 0;
        WieldCount = 0;
        TwoHandWieldCount = 0;
    }
}
