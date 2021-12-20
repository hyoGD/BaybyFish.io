using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class PlayerSpine : MonoBehaviour
{

    public static PlayerSpine instance3;
    private void Awake()
    {
        if (instance3 == null)
        {
            // DontDestroyOnLoad(this);
            instance3 = this;
        }
    
    }
    public SkeletonAnimation anim;

    [SpineSkin] public string[] skins;
    [SpineAnimation] public string moving, die, bootSpeed;
    public int n;
    // Start is called before the first frame update
    void Start()
    {
      //  n = PlayerPrefs.GetInt("SelecOption", 0);
       // skins[n] = anim.initialSkinName;
        ChangeSkin(anim, skins[n]);

    }

    // Update is called once per frame
    void Update()
    {
        ChooseSkin();
    }

    public static void ChangeSkin(SkeletonAnimation skAnim, string ssSkinChange)
    {
        //var skeleton = skAnim.Skeleton;
        //var skeletonData = skeleton.Data;
        //var newSkin = new Skin("new-skin");

        //newSkin.AddSkin(skeletonData.FindSkin(ssSkinChange));

        //newSkin.AddSkin(skAnim.Skeleton.Skin);
        //skeleton.SetSkin(newSkin);
        //skeleton.SetSlotsToSetupPose();
        //skAnim.AnimationState.Apply(skeleton);
        skAnim.Skeleton.SetSkin(ssSkinChange);
    }
    public void PlayAninmation(string _strAnim)
    {
        if (!anim.AnimationName.Equals(_strAnim))
        {

            anim.AnimationState.SetAnimation(0, _strAnim, true);
        }
    }

    void ChooseSkin()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //ChangeSkin(anim, default);
            n++;
            if (n == skins.Length)
            {
                n = 0;
            }
            ChangeSkin(anim, skins[n]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            n--;
            if (n == -1)
            {
                n = skins.Length - 1;
            }
            ChangeSkin(anim, skins[n]);
        }
    }
}
