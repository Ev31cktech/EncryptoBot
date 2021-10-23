using RLBotDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EncryptoBot
{
	//
	// Summary:
	//		A struct that represents the car control in a vector form which can be used to use Vector3.Clamp() with.
	//		This is an easy way to eliminate invalid Controller values.
	//		
	public class CarController
	{
		/// <summary>
		/// A Vector that represents ground movement.
		/// <list type="bullet">
		/// <item> Vector.X represents the steering value </item>
		/// <item> Vector.Y represents Throttle position </item>
		/// <item> Vector.Z is seen as an indicator for boosting and sliding. 
		/// Z = 1 -> boost
		/// z = -1 -> slide
		/// </item>
		/// </list>
		/// </param>
		/// </summary>
		public Vector3 GroundCtrl = new Vector3();
		/// <summary>
		/// A Vector that Represent the 3 Axis of a plane rotation
		/// <param name="X">
		/// Roll
		/// </param>
		/// <param name="Y">
		/// Yaw
		/// </param>
		/// <param name="Z">
		/// Pitch
		/// </param>
		/// </summary>
		public Vector3 AirCtrl = new Vector3();
		/// <summary>
		/// a boolean indication if the jump button should be pressed
		/// </summary>
		public bool Jump;
		/// <summary>
		/// a boolean indication if the UseItem button should be pressed
		/// This is mainly for Rumble and those kind of games
		/// </summary>
		public bool UseItem;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="crController"></param>
		/// <returns></returns>
		public Controller GetController()
		{
			GroundCtrl = Vector3.Clamp(GroundCtrl, -Vector3.One , Vector3.One);
			AirCtrl = Vector3.Clamp(AirCtrl, -Vector3.One , Vector3.One);
			Controller ctrl = new Controller();
			ctrl.Throttle = GroundCtrl.X;
			ctrl.Steer = GroundCtrl.Y;
			ctrl.Roll = AirCtrl.X;
			ctrl.Pitch = AirCtrl.Y;
			ctrl.Yaw = AirCtrl.Z;
			ctrl.Boost = GroundCtrl.Z == 1;
			ctrl.Handbrake = GroundCtrl.Z == -1;
			ctrl.UseItem = UseItem;
			ctrl.Jump = Jump;

			return ctrl;
		}
	}
}