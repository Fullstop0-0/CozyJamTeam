using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    [SerializeField] AudioMixerSnapshot storeEnter, oneItem, twoItems, threeItems, bossAggro, bossDefeat;

    public void StoreEnter()
    {
        storeEnter.TransitionTo(0);
    }
    public void OneItem()
    {
        oneItem.TransitionTo(0);
    }
    public void TwoItems()
    {
        twoItems.TransitionTo(0);
    }
    public void ThreeItems()
    {
        threeItems.TransitionTo(0);
    }
    public void BossAggro()
    {  
        bossAggro.TransitionTo(0);
    }
    public void BossDefeat() 
    {
        bossDefeat.TransitionTo(0);
    }

}
