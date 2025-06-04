using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public int health;
    public float speed;
    public int power;

    public void Init(int h, float s, int p)
    {
        health = h;
        speed = s;
        power = p;
    }

    // Logic đối thủ của bạn giữ nguyên!
}
