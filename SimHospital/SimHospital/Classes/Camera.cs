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
		int mouseWheel = 1;
		float speed = 1;
		int counter = 0;
		float scrollSpeed = 1f;
		float smoothPar=0.3F;
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

			MouseWheelZoom();
			TranslateCamera();
			base.Update(gameTime);
		}

		private void CreateLookAt()
		{
			view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
			
		}

		/* MouseWheelZoom() O zoom terá que ser suave e não abrupto. Para isso, por cada unidade de mouswheel, o
		 * zoom será aplicado num número prédefinido de iterações (counter) para que seja gradual e dê a assim
		 * a noção de que é suave.
		 * 
		 * Uma outra opção seria aplicar uma aceleração à camera. Aí sim obter-se-ia o resultado óptimo.
		 */
		private void MouseWheelZoom()
		{
			if (Mouse.GetState().ScrollWheelValue > mouseWheel && cameraPosition.Z > 0) {
				System.Console.WriteLine("zoomIn");
				cameraPosition += (cameraDirection * scrollSpeed);
			}
			if (Mouse.GetState().ScrollWheelValue < mouseWheel && cameraPosition.Z >= 0) {
				System.Console.WriteLine("ZoomOut");
				cameraPosition -= cameraDirection * scrollSpeed;
			}
			if (counter < 10 && Mouse.GetState().ScrollWheelValue != mouseWheel) {
				CreateLookAt();
				counter++;
			} else if (counter == 10) {
				mouseWheel = (int)Mouse.GetState().ScrollWheelValue;
				counter = 0;
			}
		}

		private void TranslateCamera()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.W)) {
				cameraPosition += Vector3.UnitY;
				CreateLookAt();
			}
			if (Keyboard.GetState().IsKeyDown(Keys.S)) {
				cameraPosition -= Vector3.UnitY;
				CreateLookAt();
			}
			if (Keyboard.GetState().IsKeyDown(Keys.A)) {
				cameraPosition -= Vector3.UnitX;
				CreateLookAt();
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D)) {
				cameraPosition += Vector3.UnitX;
				CreateLookAt();
			}

		}


		private void smoothZoom()
		{
			view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
		}
	}
}
