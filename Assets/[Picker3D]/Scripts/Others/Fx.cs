using System.Collections;
using UnityEngine;

public class Fx : MonoBehaviour
{
    #region Variables

    public FXData myFxData;

    private ParticleSystem myParticle;
    private ParticleSystem MyParticle { get { return myParticle == null ? myParticle = GetComponent<ParticleSystem>() : myParticle; }}

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        StartCoroutine(DestroyWithDelay(MyParticle.main.duration));
    }

    #endregion

    #region Other Methods

    private IEnumerator DestroyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        myFxData.myFxPool.ReturnObjToPool(MyParticle);
    }

    #endregion
}
