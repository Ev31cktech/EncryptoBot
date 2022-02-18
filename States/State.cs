using System;
using System.Collections.Generic;
using System.Numerics;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using EncryptoBot.Moves;

namespace EncryptoBot.States
{
	public abstract class IState
	{
		public String Name { get; private set; }
		public float Priority { get{return getPriority(); }set { priority = value;} } // should move to State
		public int Index {get;set;}
		private float priority;
		public List<IMove> moves = new List<IMove>();
		public void Initialize(string Name,int index)
		{
			this.Name = Name;
			this.Index = index;
		}
		public float getPriority()
		{
			return Math.Max(Math.Min(Priority,0), 100);
		}
		public abstract void Run(EncryptoAgent bot, IMove move); //TODO IMove is temporary. State should decide best path by itself.
		public abstract void Update(EncryptoAgent bot);
	}
	public class DriveToBall : IState
	{
		public override void Run(EncryptoAgent bot,IMove move)
		{
			this.moves.Add(move);
		}

		public override void Update(EncryptoAgent bot)
		{
			bot.targetLoc = bot.packet.Ball.Physics.Location;
			Priority = 1;
		}
	}
	public class GetSmallBoost : IState
	{
		public override void Update(EncryptoAgent bot)
		{
			Priority = Math.Max(25 - bot.BoostAmount, 100);
			int nearestBPIndex = 0;
			BoostPad[] bps = bot.packet.BoostPadStates;
			for(int i = 1; i < bps.Length; i++)
			{
				if (Vector3.Distance(bot.Location,bps[i].Location) < Vector3.Distance(bot.Location, bps[nearestBPIndex].Location))
					nearestBPIndex = i;
			}
			bot.targetLoc = bps[nearestBPIndex].Location;
		}
		public override void Run(EncryptoAgent bot, IMove move)
		{
			this.moves.Add(move);
		}
	}
	public class GetBigBoost : IState
	{
		public override void Update(EncryptoAgent bot)
		{
			Priority = 100 - bot.BoostAmount;
			int nearestBPIndex = 0;
			BoostPad[] bps = bot.packet.BoostPadStates;
			for(int i = 1; i < bps.Length; i++)
			{
				if (Vector3.Distance(bot.Location,bps[i].Location) < Vector3.Distance(bot.Location, bps[nearestBPIndex].Location))
					nearestBPIndex = i;
			}
			bot.targetLoc = bps[nearestBPIndex].Location;

		}
		public override void Run(EncryptoAgent bot, IMove move)
		{
			this.moves.Add(move);
		}
	}
	public class Recover : IState
	{
		public override void Run(EncryptoAgent bot,IMove move)
		{
			this.moves.Add(move);
		}

		public override void Update(EncryptoAgent bot)
		{}
	}
	
	public class KickOff : IState
	{
		public override void Run(EncryptoAgent bot,IMove move)
		{
			this.moves.Add(move);
			bot.targetLoc = bot.packet.Ball.Physics.Location;
		}

		public override void Update(EncryptoAgent bot)
		{
		}
	}
}
