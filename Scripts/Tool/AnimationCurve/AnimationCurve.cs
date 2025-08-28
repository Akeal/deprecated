using Godot;
using Deprecated.States;
using Deprecated.Interaction;
using Godot.Collections;
using System;

public partial class AnimationCurve : Path2D {
    [Export]
    AnimatedSprite2D AnimationTarget;
    [Export]
    public PathFollow2D PathFollow;

    private Array<AnimationCurvePoint> AnimationPoints = new Array<AnimationCurvePoint>();
    private int pointIndex = 0;
    private double distanceMoved = 0;
    private bool playing = false;
    private float bakedPathLength;
    private double speed;
    private Callable? _onComplete;
    public override void _Ready()
    {
        base._Ready();
        
        Array<Node> childNodes = GetChildren();
        int pathPointIndex = 0;

        // Each animation curve point corresponds to one point along the original, non-baked (see below), path.
        foreach(Node childNode in childNodes)
        {
            // We only care about child nodes that are our animation curves.
            if(childNode is AnimationCurvePoint animationCurvePoint)
            {
                try{
                    // Set the animation curve point position to the base curve point position.
                    animationCurvePoint.AttachPosition(Curve.GetPointPosition(pathPointIndex));
                }
                catch(ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException("More AnimationCurvePoint nodes found than points exist on the curve!");
                }

                AnimationPoints.Add(animationCurvePoint);
                pathPointIndex++;
            }
        }

		if(AnimationPoints.Count < 2){
            throw new Exception($"{Name} AnimationCurve expects at least two points!");
        }

        if(Curve.PointCount != AnimationPoints.Count){
            throw new Exception($"{Name} AnimationCurve expects exactly one animation curve point ({AnimationPoints.Count}) for each curve point ({Curve.PointCount})!");
        }

        // Curves aren't necessarily strictly linear between two points.
        // Each point can potentially have have two control points. If you've ever drawn curves in art programs, it's those two points that dictate the curve between "THIS" point and the next and prior points.
        // Basically it might not be a straight path from point [1 -> 2] or [2 -> 3].
        // We can get baked points along the curve to get a better approximation of point positions along the curve.
        // Baking the points takes the original set of curve points, with their control points, and generates additional points while removing the control points.
        // This then allows us to assume the path between any two baked points IS linear.
        Vector2[] bakedPathPoints = Curve.GetBakedPoints();
        bakedPathLength = Curve.GetBakedLength(); // We need the total length to determine what percentage along the path each point is.

        // We want to find whichever baked point is closest to each animation curve point.
        // We'll be triggering a given animation once the thing taking the curve passes this -baked- path point.
        foreach(AnimationCurvePoint animationCurvePoint in AnimationPoints){
            float minimumDistance = float.MaxValue; // The original min distance is as big as we can go for a float, so that ANY potential value is a shorter distance.
            float bakedPointPercentAlongPath = 0;
            float totalDistanceAlongPath = 0;
            Vector2 priorPointPosition = bakedPathPoints[0];
            for(int p = 0; p < bakedPathPoints.Length - 1; p++){
                float distanceBetweenBakedAndoriginal = bakedPathPoints[p].DistanceTo(animationCurvePoint.Position);
                totalDistanceAlongPath += bakedPathPoints[p].DistanceTo(priorPointPosition);
                if(distanceBetweenBakedAndoriginal < minimumDistance){
                    minimumDistance = distanceBetweenBakedAndoriginal;
                    bakedPointPercentAlongPath = (totalDistanceAlongPath / bakedPathLength);
                }
                priorPointPosition = bakedPathPoints[p];
            }
            // At this point in time we should have whichever baked point is closest to our original point.
            // bakedPointPercentAlongPath should contain how for along the path, percentage-wise, the baked point is.
            // Let's attach the found percent to our animation curve point so we can use it later for triggering the animation.
            animationCurvePoint.AttachPercent(bakedPointPercentAlongPath);
        }
        // After the above loop, each animation curve point should know what % along the path it should trigger on.
    }

    public void Play(Callable? onComplete = null){
        pointIndex = 0; // We're beginning the animation from the 0th curve point index.
        AnimationPoints[pointIndex].SetAnimation(AnimationTarget); // Play the 0th index curve animation
        speed = AnimationPoints[pointIndex].MovementSpeed; // Set our speed from the 0th index...
        pointIndex++; // We're moving to the 1st curve point index.
        playing = true;
        _onComplete = onComplete; // If we had some logic we want to execute when the curve traversal is complete, remember it for later.
    }

    public override void _PhysicsProcess(double delta){
        base._PhysicsProcess(delta);
        if(playing){
            distanceMoved += (delta * speed);
            PathFollow.ProgressRatio = (float)distanceMoved / bakedPathLength;
            if(PathFollow.ProgressRatio >= AnimationPoints[pointIndex].PercentPosition)
            {
                // We've reached the next point, play the animation for it.
                AnimationPoints[pointIndex].SetAnimation(AnimationTarget);
                // Set the movement speed along path for this animation
                speed = AnimationPoints[pointIndex].MovementSpeed;
                // We're now moving to the next next point
                pointIndex++;
            }
            // If we've reached the end of the curve, we're no longer playing the curve animation.
            if(pointIndex >= AnimationPoints.Count){
                playing = false;
                if(_onComplete.HasValue){
                    _onComplete.Value.Call(); // Our curve is now complete. Execute the logic, if any, we provided.
                }
            }
        }
    }
}
