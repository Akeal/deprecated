using Godot;

public partial class CryoPod : Node2D {

	public static class Animations {
		public const string CLOSED = "Closed";
		public const string OPENING = "Opening";
		public const string OPEN = "Open";
	}

	private Callable? _currentAnimationOnComplete;

	public void PlayAnimation(string animation, float speed = 1, Callable? onComplete = null){

		Lid.Play(animation, speed);

		if(onComplete != null)
		{
			_currentAnimationOnComplete = Callable.From(() => {
				onComplete.Value.Call();
				Lid.Disconnect(AnimatedSprite2D.SignalName.AnimationFinished, _currentAnimationOnComplete.Value);
			});

			Lid.Connect(AnimatedSprite2D.SignalName.AnimationFinished, _currentAnimationOnComplete.Value);
		}
		else{
			_currentAnimationOnComplete = null;
		}
		
	}

}