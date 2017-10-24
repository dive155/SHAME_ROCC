using System;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnManager : NetworkBehaviour
{
    /// <summary>
    /// Спаун пули из оружия типа Firearm
    /// </summary>
    [Command]
    public void CmdSpawnBullet(GameObject caller)
    {
        var weapon = caller.GetComponent<Firearm>();
        GameObject bullet = Instantiate(weapon.bulletPrefab, weapon.bulletSpawnPoint.position, weapon.bulletSpawnPoint.rotation);
        weapon.SpawnBulletSetup(bullet);

        NetworkServer.Spawn(bullet);
        RpcSpawnBullet(caller, bullet);
    }

    /// <summary>
    /// Обновляет скорость и направление пули на клиенте
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="bullet"></param>
    [ClientRpc]
    private void RpcSpawnBullet(GameObject caller, GameObject bullet)
    {
        var weapon = caller.GetComponent<Firearm>();
        weapon.SpawnBulletSetup(bullet);
        bullet.transform.position = weapon.bulletSpawnPoint.position;
        bullet.transform.rotation = weapon.bulletSpawnPoint.rotation;
    }
}
