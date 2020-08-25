using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayAudio : MonoBehaviour
{
    public static SinglePlayAudio instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than 1 SinglePlayAudio in scene!");
            return;
        }
        instance = this;
    }

    [System.Serializable]
    public class AudioClips
    {
        public AudioClip buildTurret;
        public AudioClip enemyDeath;
        public AudioClip explosion;
        public AudioClip notEnoughMoney;
        public AudioClip sellTurret;
        public AudioClip shoot;
        public AudioClip upgradeTurret;
        public AudioClip launcherShoot;
    }

    public AudioSource audioSource;

    public AudioClips audioClips;

    public void PlayBuildTurretClip()
    {
        audioSource.PlayOneShot(audioClips.buildTurret);
    }

    public void PlayEnemyDeathClip()
    {
        audioSource.PlayOneShot(audioClips.enemyDeath);
    }

    public void PlayExplosionClip()
    {
        audioSource.PlayOneShot(audioClips.explosion);
    }

    public void PlayNotEnoughMoneyClip()
    {
        audioSource.PlayOneShot(audioClips.notEnoughMoney);
    }

    public void PlaySellTurretClip()
    {
        audioSource.PlayOneShot(audioClips.sellTurret);
    }

    public void PlayShootClip()
    {
        audioSource.PlayOneShot(audioClips.shoot);
    }

    public void PlayUpgradeTurretClip()
    {
        audioSource.PlayOneShot(audioClips.upgradeTurret);
    }

    public void PlayLauncherShootClip()
    {
        audioSource.PlayOneShot(audioClips.launcherShoot);
    }
}
