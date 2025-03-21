using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public interface IVFXManager
{
    IEnumerator PlayVFX(MyEnum.VFXType vfxType, Vector3 position);
}

public class VFXManager : MonoBehaviour, IVFXManager
{
    private static IVFXManager instance;
    public static IVFXManager Instance => instance;

    private void Awake()
    {
        instance = this;
        foreach (MyEnum.VFXType vfxType in System.Enum.GetValues(typeof(MyEnum.VFXType)))
        {
            GameObject vfx = Resources.Load<GameObject>(MyConst.VFXPath[vfxType]);
            vfxDic.Add(vfxType, vfx);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(PlayVFX(MyEnum.VFXType.Bomb, new Vector3(8, 14, 0)));
        //}
    }

    private Dictionary<MyEnum.VFXType, GameObject> vfxDic = new Dictionary<MyEnum.VFXType, GameObject>();

    public IEnumerator PlayVFX(MyEnum.VFXType vfxType, Vector3 position)
    {
        if (vfxDic.ContainsKey(vfxType))
        {
            Debug.Log("VFXManager: Play VFX " + vfxType.ToString());
            GameObject vfx = Instantiate(vfxDic[vfxType], position, Quaternion.identity);
            foreach (ParticleSystem ps in vfx.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();
            }
            Destroy(vfx, 1.0f);
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
    }

}
