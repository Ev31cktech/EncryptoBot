using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot
{
	class EncryptoAgent : RLBotDotNet.Bot
	{
		/// Customization<summary>
		/// Body:	Breakout Type-S
		/// Livery:	Encryption
		/// Hat:	Top-Hat
		/// Wheels:	Stella inverted
		/// Trail:	Binary
		/// 
		/// </summary>
		DateTime time;
		int fieldLocInt = 0;
		float carHeigt = 0;
		//readonly LookAheadDist = 
		Packet packet;

		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{
			EncryptoBotGui.AddBot(this);
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			packet = new Packet(gameTickPacket);

			Vector3 tarLoc = new Vector3(Field.DiamondPath[fieldLocInt], carHeigt);
			Vector3 carLocation = packet.Players[Index].Physics.Location;

			float Distance = Vector3.Distance(carLocation, tarLoc);
			if (Distance < 200)
				//fieldLocInt = ++fieldLocInt % 4;
				;
			//tarLoc = Vector3.Zero;
			return DriveTo(tarLoc);
		}
		public Controller DriveTo(Vector3 targetLoc) { return DriveTo(targetLoc, new Controller()); }
		public Controller DriveTo(Vector3 targetLoc, Controller ctrl)
		{
			const int CLOSE_TARGET = 1000;
			Player carObject = packet.Players[Index];
			float distance = Vector3.Distance(carObject.Physics.Location, targetLoc);
			Vector3 RelVelocity = Orientation.RelativeLocation(carObject.Physics.Location, carObject.Physics.Location + carObject.Physics.Velocity, carObject.Physics.Rotation);
			Vector3 targetRelLoc = Orientation.RelativeLocation(carObject.Physics.Location, targetLoc, carObject.Physics.Rotation);
			Vector3 Controller = new Vector3();
			if (carObject.HasWheelContact)
			{
				if (distance > CLOSE_TARGET)
				{
					targetRelLoc.X = Math.Abs(targetRelLoc.X);
				}
				Controller.X = targetRelLoc.X - RelVelocity.X / 4;
				Controller.Y = targetRelLoc.Y - RelVelocity.Y / 4;
				//Controller.X = 1;
				Renderer.DrawLine3D(Color.Green, Vector3.Zero, targetRelLoc);
				Renderer.DrawLine3D(Color.Yellow, carObject.Physics.Location, targetLoc);
			}
			Controller = Vector3.Clamp(Controller, -Vector3.One, Vector3.One);
			ctrl.Throttle = Controller.X;
			ctrl.Steer = Controller.Y;
			return ctrl;
		}
		internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
		internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
	}
}
