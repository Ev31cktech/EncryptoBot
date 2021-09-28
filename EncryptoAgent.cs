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
		/// Trail:	Matrix
		/// 
		/// </summary>
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
			Renderer.DrawLine3D(Color.Black, new Vector3(0,0,0),  Orientation.RelativeLocation(new Vector3(3,3,3),carObject.Velocity, carObject.Rotation));
			Vector3 targetRelLoc = Orientation.RelativeLocation(carObject.Location, targetLoc, carObject.Rotation);
			//Vector3 iVect = Orientation.RelativeLocation(Vector3.Zero, carObject.Velocity, carObject.Rotation);
			//iVect = Orientation.RelativeLocation(Vector3.Zero, iVect, carObject.Rotation);
			//Renderer.DrawLine3D(Color.Green, Vector3.Zero, iVect);
			if (distance > CLOSE_TARGET)
			{
				targetRelLoc.X = Math.Abs(targetRelLoc.X);
			}
			ctrl.Throttle = Vector3.Clamp(targetRelLoc, Vectors.Vector3Min, Vectors.Vector3Max).X;
			ctrl.Steer = Vector3.Clamp(targetRelLoc, Vectors.Vector3Min, Vectors.Vector3Max).Y - carObject.Velocity.Y;
			Renderer.DrawString2D(String.Format("0000.00", carObject.AngularVelocity),Color.White,new Vector2(20,20), 1,1);
			//ctrl.Steer = 1;
			return ctrl;
		}
		internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
		internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
	}
}
