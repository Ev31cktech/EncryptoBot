using System.Numerics;

namespace Bot.Utilities.Processed.Packet
{
	public class Player
	{
		public int Boost;
		public bool DoubleJumped;
		public bool HasWheelContact;
		public bool IsSupersonic;
		public bool Jumped;
		public string Name;
		private Physics physics;
		public Vector3 Location => physics.Location;
		public Vector3 Velocity => physics.Location;
		public Vector3 AngularVelocity => physics.Location;
		public Orientation Rotation => physics.Rotation;
		public int Team;
		public Vector3 HitBox;

		public Player(rlbot.flat.PlayerInfo playerInfo)
		{
			physics = new Physics(playerInfo.Physics.Value);
			IsSupersonic = playerInfo.IsSupersonic;
			HasWheelContact = playerInfo.HasWheelContact;
			Jumped = playerInfo.Jumped;
			DoubleJumped = playerInfo.DoubleJumped;
			Name = playerInfo.Name;
			Team = playerInfo.Team;
			Boost = playerInfo.Boost;
			HitBox = HitboxToVector3(playerInfo.Hitbox);
		}
		public Vector3 HitboxToVector3(rlbot.flat.BoxShape? _bs)
		{
			Vector3 hb = new Vector3();
			if (_bs.HasValue)
			{
				rlbot.flat.BoxShape bs = (rlbot.flat.BoxShape)_bs;
				hb.X = bs.Length / 2;
				hb.Y = bs.Width / 2;
				hb.Z = bs.Height / 2;
			}
			return hb;
		}

		public class Physics
		{
			public Vector3 AngularVelocity;
			public Vector3 Location;
			public Orientation Rotation;
			public Vector3 Velocity;

			public Physics(rlbot.flat.Physics physics)
			{
				Location = physics.Location.Value.ToVector3();
				Velocity = physics.Velocity.Value.ToVector3();
				AngularVelocity = physics.AngularVelocity.Value.ToVector3();
				Rotation = new Orientation(physics.Rotation);
			}
		}
	}
}