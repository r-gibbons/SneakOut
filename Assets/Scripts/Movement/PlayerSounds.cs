using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SoundType
{
    noSound,
    walking,
    sprinting,
    sneaking,
    jumping,
}
public class PlayerSounds : MonoBehaviour
{
    [SerializeField] Rigidbody playerRig;
    SphereCollider soundCollider;

    Dictionary<SoundType, float> SoundValues = new Dictionary<SoundType, float> 
    { { SoundType.noSound, 0.0f },{ SoundType.walking, 8.0f },{ SoundType.sprinting, 12.0f },
        { SoundType.jumping,14.0f} };
    SoundType currentSound;
    float time = 0;
    void Awake()
    {
        soundCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        switch(MovePlayer.Instance.isMoving, MovePlayer.Instance.isSprinting,PlayerJump.Instance.grounded)
        {
            case (true,true,true):
                if (currentSound == SoundType.sprinting) { currentSound = SoundType.sprinting; time = 0;}
                ChangeRadius(SoundValues[SoundType.sprinting]);
                break;

            case (true,false,true):
                if (currentSound == SoundType.walking) { currentSound = SoundType.walking; time = 0;}
                ChangeRadius(SoundValues[SoundType.walking]);
                break;
            //applies jump sound when walking, standing, or sprint jumping
            case (false, false, false):
            case (true, false, false):
            case (false, true, false):
            case (true, true, false):
                if (currentSound == SoundType.jumping) { currentSound = SoundType.jumping; time = 0; }
                ChangeRadius(SoundValues[SoundType.jumping]);
                break;
            default:
                if (currentSound == SoundType.noSound) { currentSound = SoundType.noSound; time = 0; }
                ChangeRadius(SoundValues[SoundType.noSound]);
                break;
        }
    }
    void ChangeRadius(float size)
    {
        time += .05f;
        soundCollider.radius = Mathf.Lerp(soundCollider.radius, size, time);
    }
}
