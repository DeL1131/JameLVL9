using System.Collections;
using UnityEngine;

public interface IBulletPattern
{
    IEnumerator Execute(BulletShootContext context, BulletSpawner spawner);
}