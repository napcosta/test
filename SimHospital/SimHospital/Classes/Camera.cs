using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace SimHospital
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class Camera : Microsoft.Xna.Framework.GameComponent
	{
		public Vector3 cameraPosition
		{
			get;
			protected set;
		}

		Vector3 cameraDirection;
		Vector3 cameraUp;
		float speed = 3;
		public Matrix view
		{
			get;
			protected set;
		}

		public Matrix projection
		{
			get;
			protected set;
		}

		public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
			: base(game)
		{
			cameraPosition = pos;
			cameraDirection = target - pos;
			cameraDirection.Normalize();
			cameraUp = up;
			CreateLookAt();

			projection = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.PiOver4,
				(float)Game.Window.ClientBounds.Width /
				(float)Game.Window.ClientBounds.Height,
				1, 3000);
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			// TODO: Add your initialization code here

			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// TODO: Add your update code here
			if (Keyboard.GetState().IsKeyDown(Keys.W))
				cameraPosition += cameraDirection * speed;
			if (Keyboard.GetState().IsKeyDown(Keys.S))
				cameraPosition -= cameraDirection * speed;

			System.Console.WriteLine(cameraPosition);
			CreateLookAt();
			base.Update(gameTime);
		}

		private void CreateLookAt()
		{
			view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
		}
	}
}
