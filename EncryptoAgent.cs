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
		DateTime time;
		int fieldLocInt = 0;
		float carHeigt = 0;
		Packet packet;

		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			packet = new Packet(gameTickPacket);

			Vector3 tarLoc = new Vector3(Field.DiamondPath[fieldLocInt], carHeigt);
			Vector3 carLocation = packet.Players[Index].Physics.Location;

			float Distance = Vector3.Distance(carLocation, tarLoc);
			if (Distance < 200)
				fieldLocInt =++fieldLocInt % 4;
			return DriveTo(tarLoc);
		}
		public Controller DriveTo(Vector3 targetLoc) { return DriveTo(targetLoc, new Controller()); }
		public Controller DriveTo(Vector3 targetLoc, Controller ctrl)
		{
			const int CLOSE_TARGET = 1000;
			Physics carObject = packet.Players[Index].Physics;
			float distance = Vector3.Distance(carObject.Location, targetLoc);
			Renderer.DrawLine3D(Color.Yellow, carObject.Location, targetLoc);
			Vector3 targetRelLoc = Orientation.RelativeLocation(carObject.Location, targetLoc, carObject.Rotation);

			if (distance > CLOSE_TARGET)
			{
				targetRelLoc.X = Math.Abs(targetRelLoc.X);
			}
			Renderer.DrawString2D(String.Format("Velocity: {0}", carObject.Velocity.ToString("0000.00")), Color.White, new Vector2(2, 2), 1, 1);
			Renderer.DrawString2D(String.Format("Angular: {0}", carObject.AngularVelocity.ToString("0000.00")), Color.White, new Vector2(2, 20), 1, 1);
			Renderer.DrawString2D(String.Format("Dist: {0}", distance.ToString("0000.00")), Color.White, new Vector2(2, 40), 1, 1);
			ctrl.Throttle = Vector3.Clamp(targetRelLoc,Vectors.Vector3Min, Vectors.Vector3Max).X;
			ctrl.Steer = Vector3.Clamp(targetRelLoc,Vectors.Vector3Min, Vectors.Vector3Max).Y;
			return ctrl;
		}
		internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
		internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
	}
}
