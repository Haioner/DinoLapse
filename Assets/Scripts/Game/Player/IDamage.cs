using UnityEngine;

public interface IDamage
{
    public void TakeDamage(float damageValue) { }
    public AudioSource HitAudio {  get; set; }
}
