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
        bullet.GetComponent<BaseBullet>().Team = weapon.Team;
        bullet.GetComponent<BaseBullet>().Holder = weapon.Holder;
        bullet.GetComponent<Rigidbody>().velocity += weapon.GetComponent<Rigidbody>().velocity;

        foreach (Collider col in weapon.GetAllColliders())
        {
            Physics.IgnoreCollision(col, bullet.GetComponent<Collider>());
        }

        NetworkServer.Spawn(bullet);
        RpcSpawnBullet(caller, bullet);
    }

    /// <summary>
    /// Обновляет скорость и направление пули на клиенте
    /// </summary>
    /// <param name="caller"></param>
    /// <param name="bullet"></param>
    [ClientRpc]
    public void RpcSpawnBullet(GameObject caller, GameObject bullet)
    {
        var weapon = caller.GetComponent<Firearm>();
        bullet.transform.position = weapon.bulletSpawnPoint.position;
        bullet.transform.rotation = weapon.bulletSpawnPoint.rotation;
        bullet.GetComponent<Rigidbody>().velocity = weapon.GetComponent<Rigidbody>().velocity;
    }
}
