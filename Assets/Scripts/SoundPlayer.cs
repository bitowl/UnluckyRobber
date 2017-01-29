using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    private AudioSource _audioSource;

    public AudioClip JumpSound;
    public AudioClip DoubleJumpSound;
    public AudioClip PunchSound;
    public AudioClip PickUpSound;
    public AudioClip ThrowSound;
    public AudioClip DeathSound;
    public AudioClip HurtSound;
    public AudioClip ScoreSound;

	// Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}


    public void Jump()
    {
        _audioSource.PlayOneShot(JumpSound);
    }

    public void DoubleJump()
    {
        _audioSource.PlayOneShot(DoubleJumpSound);
    }

    public void Punch()
    {
        _audioSource.PlayOneShot(PunchSound);
    }

    public void PickUp()
    {
        _audioSource.PlayOneShot(PickUpSound);
    }

    public void Throw()
    {
        _audioSource.PlayOneShot(ThrowSound);
    }

    public void Death()
    {
        _audioSource.PlayOneShot(DeathSound);
    }

    public void Hurt()
    {
        _audioSource.PlayOneShot(HurtSound);
    }

    public void Score()
    {
        _audioSource.PlayOneShot(ScoreSound);
    }
}
