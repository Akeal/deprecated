using System;
using Godot;

public partial class AnimationCurvePoint : Node {
    [Export]
    public float MovementSpeed = 1.0f;
    [Export]
    public string AnimationName;
    [Export]
    public bool ScaleAnimationFrameDurationBySpeed;
    private Vector2 _pointPosition;
    public Vector2 Position { get { return _pointPosition; } }

    private float _percentPosition;
    public float PercentPosition { get { return _percentPosition; } }

    public void AttachPosition(Vector2 pointPosition){
        _pointPosition = pointPosition;
    }

    public void AttachPercent(float percentAlongPath){
        _percentPosition = percentAlongPath;
    }

    public void SetAnimation(AnimatedSprite2D animatedSprite2D, bool scaleFPSByMovementSpeed = false){
        if(!String.IsNullOrEmpty(AnimationName))
        {
            if(scaleFPSByMovementSpeed){
                // This will make the animation play faster if the object is moving faster
                animatedSprite2D.Play(AnimationName, MovementSpeed);
            }
            else{
                // This will make the animation always play at the original speed
                animatedSprite2D.Play(AnimationName);
            }
        }
    }
}
