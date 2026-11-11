using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

[assembly: NeutralResourcesLanguage("en-US")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CompilationRelaxations(8)]
[assembly: Guid("a309a29f-d328-4448-a284-a1b8034f8fef")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyTitle("DPSF")]
[assembly: AssemblyProduct("DPSF")]
[assembly: AssemblyDescription("Dynamic Particle System Framework")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyCopyright("Copyright © Daniel Schroeder 2008")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("2.2.2.0")]
namespace DPSF
{
	/// <summary>
	/// Class to hold and update an object's 3D Pivot Point (point to rotate around), Pivot Velocity, and 
	/// Pivot Acceleration. This class requires a Position3D object, and optionally a Orientation3D object, 
	/// that should be affected by rotations around the Pivot Point.
	/// </summary>
	public class PivotPoint3D
	{
		/// <summary>
		/// The 3D Pivot Point that the object should rotate around.
		/// <para>NOTE: This only has effect when Rotational Pivot Velocity / Acceleration are used.</para>
		/// </summary>
		public Vector3 PivotPoint = Vector3.Zero;

		/// <summary>
		/// The object's Rotational Velocity around the Pivot Point (Position change).
		/// <para>NOTE: Rotations are specified by giving a 3D Vector, where the direction is the axis to 
		/// rotate around, and the vector length is the amount (angle in radians) to rotate.
		/// It can also be thought of as Vector(PitchVelocity, YawVelocity, RollVelocity).</para>
		/// </summary>
		public Vector3 PivotRotationalVelocity = Vector3.Zero;

		/// <summary>
		/// Get / Set the object's Rotational Acceleration around the Pivot Point (Position change).
		/// <para>NOTE: Rotations are specified by giving a 3D Vector, where the direction is the axis to 
		/// rotate around, and the vector length is the amount (angle in radians) to rotate.
		/// It can also be thought of as Vector(PitchAcceleration, YawAcceleration, RollAcceleration).</para>
		/// </summary>
		public Vector3 PivotRotationalAcceleration = Vector3.Zero;

		private Position3D mcPositionData;

		private Orientation3D mcOrientationData;

		private bool mbRotateOrientationToo = true;

		/// <summary>
		/// Get / Set the Position3D object that the Pivot Point should affect
		/// </summary>
		public Position3D PositionData
		{
			get
			{
				return mcPositionData;
			}
			set
			{
				mcPositionData = value;
			}
		}

		/// <summary>
		/// Get / Set the Orientation3D object that the Pivot Point should affect
		/// </summary>
		public Orientation3D OrientationData
		{
			get
			{
				return mcOrientationData;
			}
			set
			{
				mcOrientationData = value;
			}
		}

		/// <summary>
		/// Specify if the Update() function should Rotate the object's Orientation too when it
		/// rotates the object around the Pivot Point
		/// </summary>
		public bool RotateOrientationToo
		{
			get
			{
				return mbRotateOrientationToo;
			}
			set
			{
				mbRotateOrientationToo = value;
			}
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cPivotPointToCopy">The PivotPoint3D object to copy</param>
		public PivotPoint3D(PivotPoint3D cPivotPointToCopy)
		{
			CopyFrom(cPivotPointToCopy);
		}

		/// <summary>
		/// Copy the given PivotPoint3D object's data into this object's data
		/// </summary>
		/// <param name="cPivotPointToCopy">The PivotPoint3D object to copy from</param>
		public void CopyFrom(PivotPoint3D cPivotPointToCopy)
		{
			PivotPoint = cPivotPointToCopy.PivotPoint;
			PivotRotationalVelocity = cPivotPointToCopy.PivotRotationalVelocity;
			PivotRotationalAcceleration = cPivotPointToCopy.PivotRotationalAcceleration;
			mbRotateOrientationToo = cPivotPointToCopy.RotateOrientationToo;
			mcPositionData = new Position3D(cPivotPointToCopy.PositionData);
			mcOrientationData = new Orientation3D(cPivotPointToCopy.OrientationData);
		}

		/// <summary>
		/// Explicit Constructor. Set the Position3D object that should be affected by rotations around
		/// this Pivot Point.
		/// </summary>
		/// <param name="cPosition">Handle to the Position3D object to update</param>
		public PivotPoint3D(Position3D cPosition)
		{
			mcPositionData = cPosition;
			mcOrientationData = null;
		}

		/// <summary>
		/// Explicit Constructor. Set the Position3D and Orientation3D objects that should be affected by 
		/// rotational around this Pivot Point.
		/// </summary>
		/// <param name="cPosition">Handle to the Position3D object to update</param>
		/// <param name="cOrientation">Handle to the Orienetation3D object to update</param>
		public PivotPoint3D(Position3D cPosition, Orientation3D cOrientation)
		{
			mcPositionData = cPosition;
			mcOrientationData = cOrientation;
		}

		/// <summary>
		/// Rotates the object about its center, changing its Orientation, as well as around the 
		/// Pivot Point, changing its Position
		/// </summary>
		/// <param name="sRotationMatrix">The Rotation to apply to the object, rotating it 
		/// around the Pivot Point</param>
		public void RotatePositionAndOrientation(Matrix sRotationMatrix)
		{
			if (mcOrientationData != null)
			{
				mcOrientationData.Rotate(sRotationMatrix);
			}
			RotatePosition(sRotationMatrix);
		}

		/// <summary>
		/// Rotates the object around the Pivot Point, changing its Position, without 
		/// changing its Orientation
		/// </summary>
		/// <param name="sRotationMatrix">The Rotation to apply to the object, rotating it 
		/// around the Pivot Point</param>
		public void RotatePosition(Matrix sRotationMatrix)
		{
			if (PivotPoint != mcPositionData.Position)
			{
				mcPositionData.Position = RotatePosition(sRotationMatrix, PivotPoint, mcPositionData.Position);
			}
		}

		/// <summary>
		/// Update the Position and Orientation according to the Pivot Rotational Velocity / Acceleration
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The time Elapsed in Seconds since the last Update</param>
		public void Update(float fElapsedTimeInSeconds)
		{
			if (PivotRotationalAcceleration != Vector3.Zero)
			{
				PivotRotationalVelocity += PivotRotationalAcceleration * fElapsedTimeInSeconds;
			}
			if (PivotRotationalVelocity != Vector3.Zero)
			{
				Vector3 vector = PivotRotationalVelocity * fElapsedTimeInSeconds;
				if (mbRotateOrientationToo)
				{
					RotatePositionAndOrientation(Matrix.CreateFromYawPitchRoll(vector.Y, vector.X, vector.Z));
				}
				else
				{
					RotatePosition(Matrix.CreateFromYawPitchRoll(vector.Y, vector.X, vector.Z));
				}
			}
		}

		/// <summary>
		/// Rotates the given Position and Orientation around the Pivot Point, changing the Position and Orientation
		/// </summary>
		/// <param name="sRotationMatrix">The Rotation to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around</param>
		/// <param name="srPosition">The Position of the object (to be modified)</param>
		/// <param name="srOrientation">The Orientation of the object (to be modified)</param>
		public static void RotatePositionAndOrientation(Matrix sRotationMatrix, Vector3 sPivotPoint, ref Vector3 srPosition, ref Quaternion srOrientation)
		{
			srOrientation = Orientation3D.Rotate(sRotationMatrix, srOrientation);
			srPosition = RotatePosition(sRotationMatrix, sPivotPoint, srPosition);
		}

		/// <summary>
		/// Returns the new Position after Rotating the given Position around the specified Pivot Point
		/// </summary>
		/// <param name="sRotationMatrix">The Rotation to apply to the Emitter</param>
		/// <param name="sPivotPoint">The Point to rotate the Emitter around</param>
		/// <param name="sPosition">The Position to be rotated around the Pivot Point</param>
		/// <returns>Returns the new Position after Rotating the given Position around the specified Pivot Point</returns>
		public static Vector3 RotatePosition(Matrix sRotationMatrix, Vector3 sPivotPoint, Vector3 sPosition)
		{
			if (sPivotPoint != sPosition)
			{
				sPosition = Vector3.Transform(sPosition - sPivotPoint, sRotationMatrix);
				sPosition += sPivotPoint;
			}
			return sPosition;
		}
	}
	/// <summary>
	///   A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class DPSFResources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		/// <summary>
		///   Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("DPSF.DPSFResources", typeof(DPSFResources).Assembly);
					resourceMan = resourceManager;
				}
				return resourceMan;
			}
		}

		/// <summary>
		///   Overrides the current thread's CurrentUICulture property for all
		///   resource lookups using this strongly typed resource class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static byte[] DPSFDefaultEffectWindowsHiDef
		{
			get
			{
				object obj = ResourceManager.GetObject("DPSFDefaultEffectWindowsHiDef", resourceCulture);
				return (byte[])obj;
			}
		}

		internal static byte[] DPSFDefaultEffectWindowsReach
		{
			get
			{
				object obj = ResourceManager.GetObject("DPSFDefaultEffectWindowsReach", resourceCulture);
				return (byte[])obj;
			}
		}

		internal static byte[] DPSFDefaultEffectXbox360HiDef
		{
			get
			{
				object obj = ResourceManager.GetObject("DPSFDefaultEffectXbox360HiDef", resourceCulture);
				return (byte[])obj;
			}
		}

		internal DPSFResources()
		{
		}
	}
	/// <summary>
	/// Attribute used to mark fields/methods/properties that should be shown in the DPSF Viewer.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	public class DPSFViewerParameterAttribute : Attribute
	{
		private string _description = string.Empty;

		private string _group = string.Empty;

		/// <summary>
		/// A user-friendly description of what the field/method/property does.
		/// This will be displayed to the user of the DPSF Viewer.
		/// </summary>
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		/// <summary>
		/// The group of controls that this one should be grouped with in the DPSF Viewer.
		/// </summary>
		public string Group
		{
			get
			{
				return _group;
			}
			set
			{
				_group = value;
			}
		}
	}
	/// <summary>
	/// Class used to hold a Particle's properties.
	/// This class only holds a Particle's Lifetime information, but may be inherited from
	/// in order to specify additional Particle properties, such as position, size, color, etc.
	/// </summary>
	public class DPSFParticle
	{
		private float mfElapsedTime;

		private float mfLastElapsedTime;

		private float mfNormalizedElapsedTime;

		private float mfLastNormalizedElapsedTime;

		private float mfLifetime;

		private bool mbVisible;

		/// <summary>
		/// Get / Set how much Time has Elapsed since this Particle was born.
		/// <para>NOTE: Setting this to be greater than or equal to Lifetime will
		/// cause the Particle to become InActive and be removed from the Particle 
		/// System (if Lifetime is greater than zero).</para>
		/// <para>NOTE: Setting this also sets the Last Elapsed Time to the given value.</para>
		/// </summary>
		public float ElapsedTime
		{
			get
			{
				return mfElapsedTime;
			}
			set
			{
				mfElapsedTime = value;
				mfLastElapsedTime = mfElapsedTime;
				if (mfLifetime > 0f)
				{
					mfNormalizedElapsedTime = mfElapsedTime / mfLifetime;
					mfLastNormalizedElapsedTime = mfNormalizedElapsedTime;
				}
				else
				{
					mfLastNormalizedElapsedTime = 0f;
					mfNormalizedElapsedTime = 0f;
				}
			}
		}

		/// <summary>
		/// Get / Set the Normalized Elapsed Time (0.0 - 1.0) of this Particle (How far through its life it is).
		/// <para>NOTE: Setting this to be greater than or equal to 1.0 will cause the Particle to become InActive 
		/// and be removed from the Particle System (if Lifetime is greater than zero).</para>
		/// <para>NOTE: If the Particle has a Lifetime of zero (is set to live forever), Setting this has no effect,
		/// and Getting this will always return zero.</para>
		/// </summary>
		public float NormalizedElapsedTime
		{
			get
			{
				return mfNormalizedElapsedTime;
			}
			set
			{
				mfNormalizedElapsedTime = value;
				if (mfLifetime > 0f)
				{
					mfElapsedTime = (mfLastElapsedTime = mfNormalizedElapsedTime * mfLifetime);
					mfLastNormalizedElapsedTime = mfNormalizedElapsedTime;
				}
				else
				{
					mfLastNormalizedElapsedTime = 0f;
					mfNormalizedElapsedTime = 0f;
				}
			}
		}

		/// <summary>
		/// Get the Elapsed Time of the Particle at the previous frame
		/// </summary>
		public float LastElapsedTime => mfLastElapsedTime;

		/// <summary>
		/// Get the Normalized Elapsed Time of the Particle at the previous frame
		/// </summary>
		public float LastNormalizedElapsedTime => mfLastNormalizedElapsedTime;

		/// <summary>
		/// Get / Set the Lifetime of the Particle (How long it should live for).
		/// <para>NOTE: Setting this to zero will make the Particle live forever.</para>
		/// <para>NOTE: Negative Lifetimes are reset to zero.</para>
		/// </summary>
		public float Lifetime
		{
			get
			{
				return mfLifetime;
			}
			set
			{
				mfLifetime = value;
				if (mfLifetime > 0f)
				{
					mfNormalizedElapsedTime = mfElapsedTime / mfLifetime;
					mfLastNormalizedElapsedTime = mfNormalizedElapsedTime;
				}
				else
				{
					mfLastNormalizedElapsedTime = 0f;
					mfNormalizedElapsedTime = 0f;
					mfLifetime = 0f;
				}
			}
		}

		/// <summary>
		/// Get / Set if the Particle should be Visible (i.e. be drawn) or not
		/// </summary>
		public bool Visible
		{
			get
			{
				return mbVisible;
			}
			set
			{
				mbVisible = value;
			}
		}

		/// <summary>
		/// Constructor to initialize Particle variables
		/// </summary>
		public DPSFParticle()
		{
			Reset();
		}

		/// <summary>
		/// Function to update the Elapsed Time associated variables of the Particle. This is done
		/// automatically by DPSF when the particle system's Update() function is called, so this
		/// function does not need to be manually called by the user.
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The amount of time in seconds that 
		/// has passed since this function was last called</param>
		public void UpdateElapsedTimeVariables(float fElapsedTimeInSeconds)
		{
			mfLastElapsedTime = mfElapsedTime;
			mfElapsedTime += fElapsedTimeInSeconds;
			if (mfLifetime > 0f)
			{
				mfLastNormalizedElapsedTime = mfNormalizedElapsedTime;
				mfNormalizedElapsedTime = mfElapsedTime / mfLifetime;
			}
			else
			{
				mfLastNormalizedElapsedTime = 0f;
				mfNormalizedElapsedTime = 0f;
			}
		}

		/// <summary>
		/// Function to tell if a Particle is still Active (alive) or not
		/// </summary>
		/// <returns>Returns true if the Particle is Active (alive), false if it is Inactive (dead)</returns>
		public bool IsActive()
		{
			if (!(mfElapsedTime < mfLifetime))
			{
				return mfLifetime <= 0f;
			}
			return true;
		}

		/// <summary>
		/// Resets the Particles variables to default values
		/// </summary>
		public virtual void Reset()
		{
			mfElapsedTime = (mfLastElapsedTime = 0f);
			mfNormalizedElapsedTime = (mfLastNormalizedElapsedTime = 0f);
			mfLifetime = 0f;
			mbVisible = true;
		}

		/// <summary>
		/// Deep copy the ParticleToCopy's values into this Particle
		/// </summary>
		/// <param name="ParticleToCopy">The Particle whose values should be Copied</param>
		public virtual void CopyFrom(DPSFParticle ParticleToCopy)
		{
			Lifetime = ParticleToCopy.Lifetime;
			Visible = ParticleToCopy.Visible;
			ElapsedTime = ParticleToCopy.ElapsedTime;
		}
	}
	/// <summary>
	/// The Base Particle class from which the Default Particle classes inherit from
	/// </summary>
	public class DPSFDefaultBaseParticle : DPSFParticle
	{
		/// <summary>
		/// The Position of the Particle in 3D space.
		/// <para>NOTE: For 2D Pixel and Sprite Particles, the Z value can still be used to
		/// determine which Particles are drawn in front of others (0.0 = front, 
		/// 1.0 = back) when SpriteBatchOptions.eSortMode = SpriteSortMode.BackToFront
		/// or SpriteSortMode.FrontToBack</para>
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// The Color of the Particle, or if using a Texture, the Color to incorporate into the Particle's Texture.
		/// <para>NOTE: This Color's alpha value controls the transparency of the Particle's Texture.</para>
		/// <para>NOTE: This should be a Non-Premultipilied color.</para>
		/// </summary>
		public Color Color;

		/// <summary>
		/// The Particle's Velocity
		/// </summary>
		public Vector3 Velocity;

		/// <summary>
		/// The Particle's Acceleration
		/// </summary>
		public Vector3 Acceleration;

		/// <summary>
		/// An External Force that may be applied to the Particle
		/// </summary>
		public Vector3 ExternalForce;

		/// <summary>
		/// The Friction to apply to the Particle
		/// </summary>
		public float Friction;

		/// <summary>
		/// The Particle's Color when it is born.
		/// <para>NOTE: This should be a Non-Premultipilied color.</para>
		/// </summary>
		public Color StartColor;

		/// <summary>
		/// The Particle's Color when it dies
		/// <para>NOTE: This should be a Non-Premultipilied color.</para>
		/// </summary>
		public Color EndColor;

		/// <summary>
		/// Get the Color as a Premultiplied color (i.e. premultiplied alpha).
		/// </summary>
		public Color ColorAsPremultiplied => Color.FromNonPremultiplied(Color.ToVector4());

		/// <summary>
		/// Get the Start Color as a Premultiplied color (i.e. premultiplied alpha).
		/// </summary>
		public Color StartColorAsPremultiplied => Color.FromNonPremultiplied(StartColor.ToVector4());

		/// <summary>
		/// Get the End Color as a Premultiplied color (i.e. premultiplied alpha).
		/// </summary>
		public Color EndColorAsPremultiplied => Color.FromNonPremultiplied(EndColor.ToVector4());

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			Position = Vector3.Zero;
			Color = Color.White;
			Velocity = (Acceleration = (ExternalForce = Vector3.Zero));
			Friction = 0f;
			StartColor = (EndColor = Color.White);
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DPSFDefaultBaseParticle dPSFDefaultBaseParticle = (DPSFDefaultBaseParticle)ParticleToCopy;
			base.CopyFrom(dPSFDefaultBaseParticle);
			Position = dPSFDefaultBaseParticle.Position;
			Color = dPSFDefaultBaseParticle.Color;
			Velocity = dPSFDefaultBaseParticle.Velocity;
			Acceleration = dPSFDefaultBaseParticle.Acceleration;
			ExternalForce = dPSFDefaultBaseParticle.ExternalForce;
			Friction = dPSFDefaultBaseParticle.Friction;
			StartColor = dPSFDefaultBaseParticle.StartColor;
			EndColor = dPSFDefaultBaseParticle.EndColor;
		}
	}
	/// <summary>
	/// Interface implemented by all Particle Systems.
	/// Variables of this type can point to any type of Particle System.
	/// </summary>
	public interface IDPSFParticleSystem
	{
		/// <summary>
		/// The path used to load the Texture when the InitializeNonSerializableProperties() function is called.
		/// <para>NOTE: This is automatically set when the SetTexture() function is called.</para>
		/// </summary>
		string DeserializationTexturePath { get; set; }

		/// <summary>
		/// The path used to load the Effect when the InitializeNonSerializableProperties() function is called.
		/// <para>NOTE: This is automatically set when the SetEffectAndTechnique(string, string) function is called. </para>
		/// </summary>
		string DeserializationEffectPath { get; set; }

		/// <summary>
		/// The Name of the Technique to use when the InitializeNonSerializableProperties() function is called.
		/// <para>NOTE: This is automatically set when the SetEffectAndTechnique() and SetTechnique() functions are called.</para>
		/// </summary>
		string DeserializationTechniqueName { get; set; }

		/// <summary>
		/// Returns true if the Particle System is Initialized, false if not.
		/// </summary>
		/// <returns>Returns true if the Particle System is Initialized, false if not.</returns>
		bool IsInitialized { get; }

		/// <summary>
		/// Get / Set if this Particle System should Draw its Particles or not.
		/// <para>NOTE: Setting this to false causes the Draw() function to not draw anything.</para>
		/// </summary>
		bool Visible { get; set; }

		/// <summary>
		/// Get / Set if this Particle System should Update itself and its Particles or not.
		/// <para>NOTE: Setting this to false causes the Update() function to not update anything.</para>
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// The Order in which the Particle System should be Updated relative to other 
		/// DPSF Particle Systems in the same Particle System Manager. Particle Systems 
		/// are Updated in ascending order according to their Update Order (i.e. lowest first).
		/// </summary>
		int UpdateOrder { get; set; }

		/// <summary>
		/// The Order in which the Particle System should be Drawn relative to other
		/// DPSF Particle Systems in the same Particle System Manager. Particle Systems
		/// are Drawn in ascending order according to their Draw Order (i.e. lowest first)
		/// </summary>
		int DrawOrder { get; set; }

		/// <summary>
		/// Get the Game object set in the constructor, if one was given.
		/// </summary>
		Game Game { get; }

		/// <summary>
		/// Get the Graphics Device to draw to
		/// </summary>
		GraphicsDevice GraphicsDevice { get; }

		/// <summary>
		/// Get if the Particle System is inheriting from DrawableGameComponent or not.
		/// <para>If inheriting from DrawableGameComponent, the Particle Systems
		/// are automatically added to the given Game object's Components and the
		/// Update() and Draw() functions are automatically called by the
		/// Game object when it updates and draws the rest of its Components.
		/// If the Update() and Draw() functions are called by the user anyways,
		/// they will exit without performing any operations, so it is suggested
		/// to include them anyways to make switching between inheriting and
		/// not inheriting from DrawableGameComponent seamless; just be aware
		/// that the updates and draws are actually being performed when the
		/// Game object is told to update and draw (i.e. when base.Update() and base.Draw()
		/// are called), not when these functions are being called.</para>
		/// </summary>
		bool InheritsDrawableGameComponent { get; }

		/// <summary>
		/// Get the unique ID of this Particle System.
		/// <para>NOTE: Each Particle System is automatically assigned a unique ID when it is instantiated.</para>
		/// </summary>
		int ID { get; }

		/// <summary>
		/// Get / Set the Type of Particle System this is.
		/// </summary>
		int Type { get; set; }

		/// <summary>
		/// Get the Name of the Class that this Particle System is using. This can be used to 
		/// check what type of Particle System this is at run-time.
		/// </summary>
		string ClassName { get; }

		/// <summary>
		/// Get / Set the Content Manager to use to load Textures and Effects.
		/// </summary>
		ContentManager ContentManager { get; set; }

		/// <summary>
		/// Get the render properties used to draw the particles.
		/// </summary>
		RenderProperties RenderProperties { get; }

		/// <summary>
		/// The Sprite Batch drawing Settings used in the Sprite Batch's Begin() function call.
		/// <para>NOTE: These settings only have effect if this is a Sprite particle system.</para>
		/// </summary>
		SpriteBatchSettings SpriteBatchSettings { get; }

		/// <summary>
		/// The Settings used to control the Automatic Memory Manager.
		/// </summary>
		AutoMemoryManagerSettings AutoMemoryManagerSettings { get; }

		/// <summary>
		/// The Emitter is used to automatically generate new Particles.
		/// </summary>
		ParticleEmitter Emitter { get; set; }

		/// <summary>
		/// Get a Random object used to generate Random Numbers.
		/// </summary>
		RandomNumbers RandomNumber { get; }

		/// <summary>
		/// Get / Set the World Matrix to use for drawing 3D Particles.
		/// </summary>
		Matrix World { get; set; }

		/// <summary>
		/// Get / Set the View Matrix to use for drawing 3D Particles.
		/// </summary>
		Matrix View { get; set; }

		/// <summary>
		/// Get / Set the Projection Matrix to use for drawing 3D Particles.
		/// </summary>
		Matrix Projection { get; set; }

		/// <summary>
		/// Gets the result of multiplying the World, View, and Projection matrices.
		/// </summary>
		Matrix WorldViewProjection { get; }

		/// <summary>
		/// Get / Set the Effect to use to draw the Particles
		/// </summary>
		Effect Effect { get; set; }

		/// <summary>
		/// Get / Set which Technique of the current Effect to use to draw the Particles
		/// </summary>
		EffectTechnique Technique { get; set; }

		/// <summary>
		/// Get / Set the Texture to use to draw the Particles
		/// </summary>
		Texture2D Texture { get; set; }

		/// <summary>
		/// Get / Set how fast the Particle System Simulation should run.
		/// <para>1.0 = normal speed, 0.5 = half speed, 2.0 = double speed.</para>
		/// <para>NOTE: If a negative value is specified, the Speed Scale is set 
		/// to zero (pauses the simulation; has same effect as Enabled = false).</para>
		/// </summary>
		float SimulationSpeed { get; set; }

		/// <summary>
		/// Get / Set how fast the Particle System Simulation should run to look "normal".
		/// <para>1.0 = normal speed, 0.5 = half speed, 2.0 = double speed.</para>
		/// <para>This is provided as a way of speeding up / slowing down the simulation to have 
		/// it look as desired, without having to rescale all of the particle velocities, etc. This allows
		/// you to use the exact same particle system class to create two particle systems, and then have one run
		/// slower or faster than the other, creating two different effects. If you then wanted to speed up or slow down
		/// both effects (i.e. particle systems), you could adjust the SimulationSpeed property on both particle systems 
		/// without having to worry about adjusting this property at all to get the effects back to normal speed; just reset 
		/// the SimulationSpeed property you changed back to 1.0.</para>
		/// <para>NOTE: If a negative value is specified, the Internal Simulation Speed is set to zero 
		/// (pauses the simulation; has the same effect as Enabled = false).</para>
		/// </summary>
		float InternalSimulationSpeed { get; set; }

		/// <summary>
		/// Specify how often the Particle System should be Updated.
		/// <para>NOTE: Specifying a value of zero (default) will cause the Particle 
		/// System to be Updated every time the Update() function is called 
		/// (i.e. as often as possible).</para>
		/// <para>NOTE: If the Update() function is not called often enough to
		/// keep up with this specified Update rate, the Update function
		/// updates the Particle Systems as often as possible.</para>
		/// </summary>
		int UpdatesPerSecond { get; set; }

		/// <summary>
		/// Get / Set if performance timings should be measured or not, such as how long it takes to perform updates and draws.
		/// <para>This should be disabled before building a release version of your application.</para>
		/// <para>Note: Performance profiling is not available on the Reach profile, so this will always return False on the Reach profile.</para>
		/// </summary>
		bool PerformanceProfilingIsEnabled { get; set; }

		/// <summary>
		/// Get how long (in milliseconds) it took to perform the last Update() function call.
		/// <para>Returns 0 if Performance Profiling is not Enabled.</para>
		/// </summary>
		double PerformanceTimeToDoUpdateInMilliseconds { get; }

		/// <summary>
		/// Get how long (in milliseconds) it took to perform the last Draw() function call.
		/// <para>Returns 0 if Performance Profiling is not Enabled.</para>
		/// </summary>
		double PerformanceTimeToDoDrawInMilliseconds { get; }

		/// <summary>
		/// The Particle System Manager whose properties (SimulationSpeed and 
		/// UpdatesPerSecond) this particle system should follow.
		/// <para>NOTE: This Particle System's properties will only clone the Manager's properties
		/// if the Manager's properties are Enabled. For example, the Manager's SimulationSpeed
		/// will only be copied to this Particle System if the Manager's SimulationSpeedIsEnabled
		/// property is true.</para>
		/// <para>NOTE: This value is automatically set to the last Particle System Manager this 
		/// Particle System is added to.</para>
		/// </summary>
		ParticleSystemManager ParticleSystemManagerToCopyPropertiesFrom { get; set; }

		/// <summary>
		/// Get the type of Particles that this Particle System should draw.
		/// </summary>
		ParticleTypes ParticleType { get; }

		/// <summary>
		/// Get / Set the absolute Number of Particles to Allocate Memory for.
		/// <para>NOTE: This value must be greater than or equal to zero.</para>
		/// <para>NOTE: Even if this many particles aren't used, the space for this many Particles 
		/// is still allocated in memory.</para>
		/// </summary>
		int NumberOfParticlesAllocatedInMemory { get; set; }

		/// <summary>
		/// Get / Set the Max Number of Particles this Particle System is Allowed to contain at any given time.
		/// <para>NOTE: The Automatic Memory Manager will never allocate space for more Particles than this.</para>
		/// <para>NOTE: This value must be greater than or equal to zero.</para>
		/// </summary>
		int MaxNumberOfParticlesAllowed { get; set; }

		/// <summary>
		/// Get the number of Particles that are currently Active
		/// </summary>
		int NumberOfActiveParticles { get; }

		/// <summary>
		/// Get the number of Particles being Drawn. That is, how many Particles 
		/// are both Active AND Visible.
		/// </summary>
		int NumberOfParticlesBeingDrawn { get; }

		/// <summary>
		/// Get the number of Particles that may still be added before reaching the
		/// Max Number Of Particles Allowed. If the Max Number Of Particles Allowed is 
		/// greater than the Number Of Particles Allocated In Memory AND the Auto Memory Manager is
		/// set to not increase the amount of Allocated Memory, than this returns the number 
		/// of Particles that may still be added before running out of Memory.
		/// </summary>
		int NumberOfParticlesStillPossibleToAdd { get; }

		/// <summary>
		/// Event Handler that is raised when the UpdateOrder of the Particle System is changed
		/// </summary>
		event EventHandler<EventArgs> UpdateOrderChanged;

		/// <summary>
		/// Event Handler that is raised when the DrawOrder of the Particle System is changed
		/// </summary>
		event EventHandler<EventArgs> DrawOrderChanged;

		/// <summary>
		/// Event Handler that is raised when the Enabled status of the Particle System is changed
		/// </summary>
		event EventHandler<EventArgs> EnabledChanged;

		/// <summary>
		/// Event Handler that is raised when the Visible status of the Particle System is changed
		/// </summary>
		event EventHandler<EventArgs> VisibleChanged;

		/// <summary>
		/// Release all resources used by the Particle System and reset all properties to their default values
		/// </summary>
		void Destroy();

		/// <summary>
		/// This function should be called immediately after deserializing a particle system in order to reinitialize the properties 
		/// that could not be serialized.
		/// <para>NOTE: If this type of particle system requires a Texture, this function will attempt to load the Texture specified
		/// by the DeserializationTexturePath property. If it is unable to load a texture, an ArgumentNullException will be thrown, so 
		/// this function should be wrapped in a try block, and when an ArgumentNullException is caught then the particle system's
		/// texture should be manually set.</para>
		/// <para>NOTE: This will attempt to load the Effect and Technique specified by the DeserializationEffectPath and
		/// DeserializationTechniqueName properties. If either of these are null, the DPSFDefaultEffect will be used, and the default
		/// Technique for this type of particle system will be loaded.</para>
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. You may pass in null for this parameter if not using a Game object.</param>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		void InitializeNonSerializableProperties(Game cGame, GraphicsDevice cGraphicsDevice, ContentManager cContentManager);

		/// <summary>
		/// Set the World, View, and Projection matrices for this Particle System.
		/// </summary>
		/// <param name="cWorld">The World matrix</param>
		/// <param name="cView">The View matrix</param>
		/// <param name="cProjection">The Projection matrix</param>
		void SetWorldViewProjectionMatrices(Matrix cWorld, Matrix cView, Matrix cProjection);

		/// <summary>
		/// Sets the Effect to be the DPSFDefaultEffect, and the Technique to be the default technique for this type of particle system.
		/// This is done automatically when the particle system is initialized.
		/// </summary>
		void SetDefaultEffect();

		/// <summary>
		/// Sets the Effect and Technique to use to draw the Particles
		/// </summary>
		/// <param name="sEffect">The Asset Name of the Effect to use</param>
		/// <param name="sTechnique">The name of the Effect's Technique to use</param>
		void SetEffectAndTechnique(string sEffect, string sTechnique);

		/// <summary>
		/// Sets the Effect and Technique to use to draw the Particles
		/// </summary>
		/// <param name="cEffect">The Effect to use</param>
		/// <param name="sTechnique">The name of the Effect's Technique to use</param>
		void SetEffectAndTechnique(Effect cEffect, string sTechnique);

		/// <summary>
		/// Set which Technique of the current Effect to use to draw the Particles
		/// </summary>
		/// <param name="sTechnique">The name of the Effect's Technique to use</param>
		void SetTechnique(string sTechnique);

		/// <summary>
		/// Set the Texture to use to draw the Particles
		/// </summary>
		/// <param name="sTexture">The Asset Name of the texture file to use (found in
		/// the XNA Properties of the file)</param>
		void SetTexture(string sTexture);

		/// <summary>
		/// Adds a new Particle to the particle system, at the start of the Active Particle List. 
		/// This new Particle is initialized using the particle system's Particle Initialization Function
		/// </summary>
		/// <returns>True if a particle was added, False if there is not enough memory for another Particle</returns>
		bool AddParticle();

		/// <summary>
		/// Adds the specified number of new Particles to the particle system. 
		/// These new Particles are initialized using the particle systems Particle Initialization Function
		/// </summary>
		/// <param name="iNumberOfParticlesToAdd">How many Particles to Add to the particle system</param>
		/// <returns>Returns how many Particles were able to be added to the particle system</returns>
		int AddParticles(int iNumberOfParticlesToAdd);

		/// <summary>
		/// Removes all Active Particles from the Active Particle List and adds them 
		/// to the Inactive Particle List
		/// </summary>
		void RemoveAllParticles();

		/// <summary>
		/// Updates the Particle System. This involves executing the Particle System
		/// Events, updating all Active Particles according to the Particle Events, and 
		/// adding new Particles according to the Emitter settings.
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has 
		/// elapsed since the last time this function was called</param>
		void Update(float fElapsedTimeInSeconds);

		/// <summary>
		/// Updates the Particle System, even if the the Particle Systems inherits from DrawableGameComponent.
		/// <para>Updating the Particle System involves executing the Particle System Events, updating all Active 
		/// Particles according to the Particle Events, and adding new Particles according to the Emitter settings.</para>
		/// <para>NOTE: If inheriting from DrawableGameComponent and this is called, the Particle System will be updated
		/// twice per frame; once when it is called here, and again when automatically called by the Game object.
		/// If not inheriting from DrawableGameComponent, this acts the same as calling Update().</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has 
		/// elapsed since the last time this function was called</param>
		void UpdateForced(float fElapsedTimeInSeconds);

		/// <summary>
		/// Draws all of the Active Particles to the Graphics Device
		/// </summary>
		void Draw();

		/// <summary>
		/// Draws all of the Active Particles to the Graphics Device, even if the the Particle Systems inherits
		/// from DrawableGameComponent.
		/// <para>NOTE: If inheriting from DrawableGameComponent and this is called, the Particle System will be drawn
		/// twice per frame; once when it is called here, and again when automatically called by the Game object.
		/// If not inheriting from DrawableGameComponent, this acts the same as calling Draw().</para>
		/// </summary>
		void DrawForced();

		/// <summary>
		/// Virtual function to Initialize the Particle System with default values.
		/// Particle system properties should not be set until after this is called, as 
		/// they are likely to be reset to their default values.
		/// </summary>
		/// <param name="cGraphicsDevice">The Graphics Device the Particle System should use</param>
		/// <param name="cContentManager">The Content Manager the Particle System should use to load resources</param>
		/// <param name="cSpriteBatch">The Sprite Batch that the Sprite Particle System should use to draw its particles.
		/// If this is not initializing a Sprite particle system, or you want the particle system to use its own Sprite Batch,
		/// pass in null.</param>
		void AutoInitialize(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, SpriteBatch cSpriteBatch);

		/// <summary>
		/// Sets the Camera Position of the particle system, so that the particles know how to make themselves face the camera if needed.
		/// This virtual function does not do anything unless overridden, and all it should do is set an internal Vector3 variable
		/// (e.g. public Vector3 CameraPosition { get; set; }) to match the given Vector3.
		/// </summary>
		/// <param name="cameraPosition">The position that the camera is currently at.</param>
		void SetCameraPosition(Vector3 cameraPosition);
	}
	/// <summary>
	/// The Base Particle System Framework Class.
	/// This class contains the methods and properties needed to keep track of, update, and draw Particles
	/// </summary>
	/// <typeparam name="Particle">The Particle class used to hold a particle's information. The Particle class
	/// specified must be or inherit from the DPSFParticle class</typeparam>
	/// <typeparam name="Vertex">The Particle Vertex struct used to hold a vertex's information used for drawing</typeparam>
	public class DPSF<Particle, Vertex> : IDPSFParticleSystem where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// The function prototype that Particle System Events must follow
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has elapsed since the last update</param>
		public delegate void UpdateParticleSystemDelegate(float fElapsedTimeInSeconds);

		/// <summary>
		/// The function prototype that the Particle Events must follow
		/// </summary>
		/// <param name="cParticle">The Particle to be updated</param>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has elapsed since the last update</param>
		public delegate void UpdateParticleDelegate(Particle cParticle, float fElapsedTimeInSeconds);

		/// <summary>
		/// The function prototype that the Particle Initialization Functions must follow
		/// </summary>
		/// <param name="cParticle">The Particle to be initialized</param>
		public delegate void InitializeParticleDelegate(Particle cParticle);

		/// <summary>
		/// The function prototype that the Vertex Update Functions must follow
		/// </summary>
		/// <param name="sParticleVertexBuffer">The vertex buffer array</param>
		/// <param name="iIndexInVertexBuffer">The index in the vertex buffer that the Particle properties should be written to</param>
		/// <param name="cParticle">The Particle whose properties should be copied to the vertex buffer</param>
		public delegate void UpdateVertexDelegate(ref Vertex[] sParticleVertexBuffer, int iIndexInVertexBuffer, Particle cParticle);

		/// <summary>
		/// Class to hold all of the Particle Events
		/// </summary>
		public class CParticleEvents
		{
			/// <summary>
			/// The Particle Event Types
			/// </summary>
			private enum EParticleEventTypes
			{
				EveryTime,
				OneTime,
				Timed,
				NormalizedTimed
			}

			/// <summary>
			/// Class to hold a Particle Event's information
			/// </summary>
			private class CParticleEvent
			{
				public UpdateParticleDelegate cFunctionToCall;

				public EParticleEventTypes eType;

				public int iExecutionOrder;

				public int iGroup;

				/// <summary>
				/// Explicit Constructor
				/// </summary>
				/// <param name="_cFunctionToCall">The Function the Event should call when it Fires</param>
				/// <param name="_eType">The Type of Event this is</param>
				/// <param name="_iExecutionOrder">The Order, relative to other Events, of when this Event should be Fired</param>
				/// <param name="_iGroup">The Group this Event should belong to</param>
				public CParticleEvent(UpdateParticleDelegate _cFunctionToCall, EParticleEventTypes _eType, int _iExecutionOrder, int _iGroup)
				{
					cFunctionToCall = _cFunctionToCall;
					eType = _eType;
					iExecutionOrder = _iExecutionOrder;
					iGroup = _iGroup;
				}

				/// <summary>
				/// Overload the == operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures have the same values, false if not</returns>
				public static bool operator ==(CParticleEvent c1, CParticleEvent c2)
				{
					if (c1.cFunctionToCall == c2.cFunctionToCall && c1.iExecutionOrder == c2.iExecutionOrder && c1.eType == c2.eType && c1.iGroup == c2.iGroup)
					{
						return true;
					}
					return false;
				}

				/// <summary>
				/// Overload the != operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures do not have the same values, false if they do</returns>
				public static bool operator !=(CParticleEvent c1, CParticleEvent c2)
				{
					return !(c1 == c2);
				}

				/// <summary>
				/// Override the Equals method
				/// </summary>
				public override bool Equals(object obj)
				{
					if (!(obj is CParticleEvent))
					{
						return false;
					}
					return this == (CParticleEvent)obj;
				}

				/// <summary>
				/// Override the GetHashCode method
				/// </summary>
				public override int GetHashCode()
				{
					return iExecutionOrder;
				}
			}

			/// <summary>
			/// Class to hold a Timed Particle Event's information
			/// </summary>
			private class CTimedParticleEvent : CParticleEvent
			{
				public float fTimeToFire;

				/// <summary>
				/// Explicit Constructor
				/// </summary>
				/// <param name="_cFunctionToCall">The Function the Event should call when it Fires</param>
				/// <param name="_eTimedType">The Type of Timed Event this is (Timed or NormalizedTimed)</param>
				/// <param name="_iExecutionOrder">The Order, relative to other Events, of when this Event should Fire</param>
				/// <param name="_iGroup">The Group this Event should belong to</param>
				/// <param name="_fTimeToFire">The Time at which this Event should Fire</param>
				public CTimedParticleEvent(UpdateParticleDelegate _cFunctionToCall, EParticleEventTypes _eTimedType, int _iExecutionOrder, int _iGroup, float _fTimeToFire)
					: base(_cFunctionToCall, _eTimedType, _iExecutionOrder, _iGroup)
				{
					fTimeToFire = _fTimeToFire;
				}

				/// <summary>
				/// Overload the == operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures have the same values, false if not</returns>
				public static bool operator ==(CTimedParticleEvent c1, CTimedParticleEvent c2)
				{
					if (c1.cFunctionToCall == c2.cFunctionToCall && c1.iExecutionOrder == c2.iExecutionOrder && c1.eType == c2.eType && c1.iGroup == c2.iGroup && c1.fTimeToFire == c2.fTimeToFire)
					{
						return true;
					}
					return false;
				}

				/// <summary>
				/// Overload the != operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures do not have the same values, false if they do</returns>
				public static bool operator !=(CTimedParticleEvent c1, CTimedParticleEvent c2)
				{
					return !(c1 == c2);
				}

				/// <summary>
				/// Override the Equals method
				/// </summary>
				public override bool Equals(object obj)
				{
					if (!(obj is CTimedParticleEvent))
					{
						return false;
					}
					return this == (CTimedParticleEvent)obj;
				}

				/// <summary>
				/// Override the GetHashCode method
				/// </summary>
				public override int GetHashCode()
				{
					return iExecutionOrder;
				}
			}

			private List<CParticleEvent> mcParticleEventList = new List<CParticleEvent>();

			private int EventSorter(CParticleEvent c1, CParticleEvent c2)
			{
				return c1.iExecutionOrder.CompareTo(c2.iExecutionOrder);
			}

			/// <summary>
			/// Adds a new EveryTime Event with a default Execution Order and Group of zero. 
			/// EveryTime Events fire every frame (i.e. every time the Update() function is called).
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			public void AddEveryTimeEvent(UpdateParticleDelegate cFunctionToCall)
			{
				AddEveryTimeEvent(cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new EveryTime Event with a default Group of zero. 
			/// EveryTime Events fire every frame (i.e. every time the Update() function is called).
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute. 
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			public void AddEveryTimeEvent(UpdateParticleDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddEveryTimeEvent(cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new EveryTime Event. 
			/// EveryTime Events fire every frame (i.e. every time the Update() function is called).
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddEveryTimeEvent(UpdateParticleDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				mcParticleEventList.Add(new CParticleEvent(cFunctionToCall, EParticleEventTypes.EveryTime, iExecutionOrder, iGroup));
				mcParticleEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all EveryTime Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to remove</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveEveryTimeEvents(UpdateParticleDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.EveryTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all EveryTime Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the EveryTime Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveEveryTimeEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.EveryTime && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes an EveryTime Event with the specified Function, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to remove</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveEveryTimeEvent(UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Remove(new CParticleEvent(cFunction, EParticleEventTypes.EveryTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Removes all EveryTime Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllEveryTimeEvents()
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.EveryTime);
			}

			/// <summary>
			/// Returns if there is an EveryTime Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsEveryTimeEvent(UpdateParticleDelegate cFunction)
			{
				return Extensions.Exists(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.EveryTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is an EveryTime Event with the specifed Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsEveryTimeEvent(UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Contains(new CParticleEvent(cFunction, EParticleEventTypes.EveryTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Adds a new OneTime Event with a default Execution Order and Group of zero. 
			/// OneTime Events fire once then are automatically removed.
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			public void AddOneTimeEvent(UpdateParticleDelegate cFunctionToCall)
			{
				AddOneTimeEvent(cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new OneTime Event with a default Group of zero. 
			/// OneTime Events fire once then are automatically removed.
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			public void AddOneTimeEvent(UpdateParticleDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddOneTimeEvent(cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new OneTime Event. 
			/// OneTime Events fire once then are automatically removed.
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddOneTimeEvent(UpdateParticleDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				mcParticleEventList.Add(new CParticleEvent(cFunctionToCall, EParticleEventTypes.OneTime, iExecutionOrder, iGroup));
				mcParticleEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all OneTime Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to remove</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveOneTimeEvents(UpdateParticleDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.OneTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all OneTime Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the OneTime Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveOneTimeEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.OneTime && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes a OneTime Event with the specified Function To Call, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to remove</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveOneTimeEvent(UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Remove(new CParticleEvent(cFunction, EParticleEventTypes.OneTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Removes all OneTime Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllOneTimeEvents()
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.OneTime);
			}

			/// <summary>
			/// Returns if there is an OneTime Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsOneTimeEvent(UpdateParticleDelegate cFunction)
			{
				return Extensions.Exists(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.OneTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is an OneTime Event with the specifed Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsOneTimeEvent(UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Contains(new CParticleEvent(cFunction, EParticleEventTypes.OneTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Adds a new Timed Event with a default Execution Order and Group of zero. 
			/// Timed Events fire when the Particle's Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fTimeToFire">The Time when the Event should fire
			/// (i.e. when the Function should be called)</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			public void AddTimedEvent(float fTimeToFire, UpdateParticleDelegate cFunctionToCall)
			{
				AddTimedEvent(fTimeToFire, cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new Timed Event with a default Group of zero. 
			/// Timed Events fire when the Particle's Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fTimeToFire">The Time when the Event should fire
			/// (i.e. when the Function should be called)</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute. 
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			public void AddTimedEvent(float fTimeToFire, UpdateParticleDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddTimedEvent(fTimeToFire, cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new Timed Event. 
			/// Timed Events fire when the Particle's Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fTimeToFire">The Time when the Event should fire
			/// (i.e. when the Function should be called)</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddTimedEvent(float fTimeToFire, UpdateParticleDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				mcParticleEventList.Add(new CTimedParticleEvent(cFunctionToCall, EParticleEventTypes.Timed, iExecutionOrder, iGroup, fTimeToFire));
				mcParticleEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all Timed Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveTimedEvents(UpdateParticleDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.Timed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all Timed Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the Timed Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveTimedEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.Timed && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes a Timed Event with the specified Function, Time To Fire, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="fTimeToFire">The Time the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveTimedEvent(float fTimeToFire, UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Remove(new CTimedParticleEvent(cFunction, EParticleEventTypes.Timed, iExecutionOrder, iGroup, fTimeToFire));
			}

			/// <summary>
			/// Removes all Timed Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllTimedEvents()
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.Timed);
			}

			/// <summary>
			/// Returns if there is a Timed Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the Timed Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsTimedEvent(UpdateParticleDelegate cFunction)
			{
				return Extensions.Exists(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.Timed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is a Timed Event with the specifed Timed To Fire, Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="fTimeToFire">The Time the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function of the Timed Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsTimedEvent(float fTimeToFire, UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Contains(new CTimedParticleEvent(cFunction, EParticleEventTypes.Timed, iExecutionOrder, iGroup, fTimeToFire));
			}

			/// <summary>
			/// Adds a new Normalized Timed Event with a default Execution Order and Group of zero. 
			/// Normalized Timed Events fire when the Particle's Normalized Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) when the Event should fire. 
			/// <para>NOTE: This is clamped to the range of 0.0 - 1.0.</para></param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			public void AddNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleDelegate cFunctionToCall)
			{
				AddNormalizedTimedEvent(fNormalizedTimeToFire, cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new Normalized Timed Event with a default Group of zero. 
			/// Normalized Timed Events fire when the Particle's Normalized Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) when the Event should fire. 
			/// <para>NOTE: This is clamped to the range of 0.0 - 1.0.</para></param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute. NOTE: Events with lower Execution Order are executed first. NOTE: Events
			/// with the same Execution Order are not guaranteed to be executed in the order they are added.</param>
			public void AddNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddNormalizedTimedEvent(fNormalizedTimeToFire, cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new Normalized Timed Event. 
			/// Normalized Timed Events fire when the Particle's Normalized Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) when the Event should fire
			/// (compared against the Particle's Normalized Elapsed Time). NOTE: This is clamped to the range of 0.0 - 1.0</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in the 
			/// order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				fNormalizedTimeToFire = MathHelper.Clamp(fNormalizedTimeToFire, 0f, 1f);
				mcParticleEventList.Add(new CTimedParticleEvent(cFunctionToCall, EParticleEventTypes.NormalizedTimed, iExecutionOrder, iGroup, fNormalizedTimeToFire));
				mcParticleEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all Normalized Timed Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveNormalizedTimedEvents(UpdateParticleDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.NormalizedTimed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all Normalized Timed Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the Normalized Timed Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveNormalizedTimedEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.NormalizedTimed && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes a Normalized Timed Event with the specified Function, Time To Fire, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <param name="iExectionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleDelegate cFunction, int iExectionOrder, int iGroup)
			{
				return mcParticleEventList.Remove(new CTimedParticleEvent(cFunction, EParticleEventTypes.NormalizedTimed, iExectionOrder, iGroup, fNormalizedTimeToFire));
			}

			/// <summary>
			/// Removes all Normalized Timed Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllNormalizedTimedEvents()
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.NormalizedTimed);
			}

			/// <summary>
			/// Returns if there is a NormalizedTimed Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the NormalizedTimed Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsNormalizedTimedEvent(UpdateParticleDelegate cFunction)
			{
				return Extensions.Exists(mcParticleEventList, (CParticleEvent cEvent) => cEvent.eType == EParticleEventTypes.NormalizedTimed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is a NormalizedTimed Event with the specifed Normalized Time To Fire, Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function of the NormalizedTimed Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleEventList.Contains(new CTimedParticleEvent(cFunction, EParticleEventTypes.NormalizedTimed, iExecutionOrder, iGroup, fNormalizedTimeToFire));
			}

			/// <summary>
			/// Removes all Timed Events and Normalized Timed Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllTimedAndNormalizedTimedEvents()
			{
				return RemoveAllTimedEvents() + RemoveAllNormalizedTimedEvents();
			}

			/// <summary>
			/// Removes all Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllEvents()
			{
				int count = mcParticleEventList.Count;
				mcParticleEventList.Clear();
				return count;
			}

			/// <summary>
			/// Removes all Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove all Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllEventsInGroup(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleEventList, (CParticleEvent cEvent) => cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Updates the given Particle according to the Particle Events. This is called automatically
			/// every frame by the Particle System.
			/// </summary>
			/// <param name="cParticle">The Particle to update</param>
			/// <param name="fElapsedTimeInSeconds">The amount of Time Elapsed since the last Update</param>
			public void Update(Particle cParticle, float fElapsedTimeInSeconds)
			{
				if (mcParticleEventList.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < mcParticleEventList.Count; i++)
				{
					if (mcParticleEventList[i].eType == EParticleEventTypes.EveryTime || mcParticleEventList[i].eType == EParticleEventTypes.OneTime)
					{
						mcParticleEventList[i].cFunctionToCall(cParticle, fElapsedTimeInSeconds);
						continue;
					}
					CTimedParticleEvent cTimedParticleEvent = (CTimedParticleEvent)mcParticleEventList[i];
					if (cTimedParticleEvent.eType == EParticleEventTypes.Timed)
					{
						float num = ((cParticle.LastElapsedTime == 0f) ? (-0.1f) : cParticle.LastElapsedTime);
						if (num < cTimedParticleEvent.fTimeToFire && cParticle.ElapsedTime >= cTimedParticleEvent.fTimeToFire)
						{
							cTimedParticleEvent.cFunctionToCall(cParticle, fElapsedTimeInSeconds);
						}
					}
					else
					{
						float num2 = ((cParticle.LastNormalizedElapsedTime == 0f) ? (-0.1f) : cParticle.LastNormalizedElapsedTime);
						if (num2 < cTimedParticleEvent.fTimeToFire && cParticle.NormalizedElapsedTime >= cTimedParticleEvent.fTimeToFire)
						{
							cTimedParticleEvent.cFunctionToCall(cParticle, fElapsedTimeInSeconds);
						}
					}
				}
			}
		}

		/// <summary>
		/// Class to hold all of the Particle System Events and related info
		/// </summary>
		public class CParticleSystemEvents
		{
			/// <summary>
			/// The Particle System Event Types
			/// </summary>
			private enum EParticleSystemEventTypes
			{
				EveryTime,
				OneTime,
				Timed,
				NormalizedTimed
			}

			/// <summary>
			/// Class to hold a Particle System Event's information
			/// </summary>
			private class CParticleSystemEvent
			{
				public UpdateParticleSystemDelegate cFunctionToCall;

				public EParticleSystemEventTypes eType;

				public int iExecutionOrder;

				public int iGroup;

				/// <summary>
				/// Explicit Constructor
				/// </summary>
				/// <param name="_cFunctionToCall">The Function the Event should call when it Fires</param>
				/// <param name="_eType">The Type of Event this is</param>
				/// <param name="_iExecutionOrder">The Order, relative to other Events, of when this Event should be Fired</param>
				/// <param name="_iGroup">The Group this Event should belong to</param>
				public CParticleSystemEvent(UpdateParticleSystemDelegate _cFunctionToCall, EParticleSystemEventTypes _eType, int _iExecutionOrder, int _iGroup)
				{
					cFunctionToCall = _cFunctionToCall;
					eType = _eType;
					iExecutionOrder = _iExecutionOrder;
					iGroup = _iGroup;
				}

				/// <summary>
				/// Overload the == operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures have the same values, false if not</returns>
				public static bool operator ==(CParticleSystemEvent c1, CParticleSystemEvent c2)
				{
					if (c1.cFunctionToCall == c2.cFunctionToCall && c1.iExecutionOrder == c2.iExecutionOrder && c1.eType == c2.eType && c1.iGroup == c2.iGroup)
					{
						return true;
					}
					return false;
				}

				/// <summary>
				/// Overload the != operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures do not have the same values, false if they do</returns>
				public static bool operator !=(CParticleSystemEvent c1, CParticleSystemEvent c2)
				{
					return !(c1 == c2);
				}

				/// <summary>
				/// Override the Equals method
				/// </summary>
				public override bool Equals(object obj)
				{
					if (!(obj is CParticleSystemEvent))
					{
						return false;
					}
					return this == (CParticleSystemEvent)obj;
				}

				/// <summary>
				/// Override the GetHashCode method
				/// </summary>
				public override int GetHashCode()
				{
					return iExecutionOrder;
				}
			}

			/// <summary>
			/// Class to hold a Timed Particle System Event's information
			/// </summary>
			private class CTimedParticleSystemEvent : CParticleSystemEvent
			{
				public float fTimeToFire;

				/// <summary>
				/// Explicit Constructor
				/// </summary>
				/// <param name="_cFunctionToCall">The Function the Event should call when it Fires</param>
				/// <param name="_eTimedType">The Type of Timed Event this is (Timed or NormalizedTimed)</param>
				/// <param name="_iExecutionOrder">The Order, relative to other Events, of when this Event should Fire</param>
				/// <param name="_iGroup">The Group this Event should belong to</param>
				/// <param name="_fTimeToFire">The Time at which this Event should Fire</param>
				public CTimedParticleSystemEvent(UpdateParticleSystemDelegate _cFunctionToCall, EParticleSystemEventTypes _eTimedType, int _iExecutionOrder, int _iGroup, float _fTimeToFire)
					: base(_cFunctionToCall, _eTimedType, _iExecutionOrder, _iGroup)
				{
					fTimeToFire = _fTimeToFire;
				}

				/// <summary>
				/// Overload the == operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures have the same values, false if not</returns>
				public static bool operator ==(CTimedParticleSystemEvent c1, CTimedParticleSystemEvent c2)
				{
					if (c1.cFunctionToCall == c2.cFunctionToCall && c1.iExecutionOrder == c2.iExecutionOrder && c1.eType == c2.eType && c1.iGroup == c2.iGroup && c1.fTimeToFire == c2.fTimeToFire)
					{
						return true;
					}
					return false;
				}

				/// <summary>
				/// Overload the != operator to test for value equality
				/// </summary>
				/// <returns>Returns true if the structures do not have the same values, false if they do</returns>
				public static bool operator !=(CTimedParticleSystemEvent c1, CTimedParticleSystemEvent c2)
				{
					return !(c1 == c2);
				}

				/// <summary>
				/// Override the Equals method
				/// </summary>
				public override bool Equals(object obj)
				{
					if (!(obj is CTimedParticleSystemEvent))
					{
						return false;
					}
					return this == (CTimedParticleSystemEvent)obj;
				}

				/// <summary>
				/// Override the GetHashCode method
				/// </summary>
				public override int GetHashCode()
				{
					return iExecutionOrder;
				}
			}

			/// <summary>
			/// The Options of what should happen when the Particle System reaches the end of its Lifetime
			/// </summary>
			public enum EParticleSystemEndOfLifeOptions
			{
				/// <summary>
				/// When the Particle System reaches the end of its Lifetime nothing special happens; It just
				/// continues to operate as normal.
				/// </summary>
				Nothing,
				/// <summary>
				/// When the Particle System reaches the end of its Lifetime its Elapsed Time is reset to zero,
				/// so that all of the Timed Events will be repeated again.
				/// </summary>
				Repeat,
				/// <summary>
				/// When the Particle System reaches the end of its Lifetime it calls its Destroy() function, so
				/// the Particle System releases all its resources and is no longer updated or drawn.
				/// </summary>
				Destroy
			}

			/// <summary>
			/// Class to hold the Lifetime information of the Particle System
			/// </summary>
			public class CParticleSystemLifetimeData : DPSFParticle
			{
				private EParticleSystemEndOfLifeOptions meEndOfLifeOption;

				/// <summary>
				/// Get / Set what should happen when the Particle System reaches the end of its Lifetime
				/// </summary>
				public EParticleSystemEndOfLifeOptions EndOfLifeOption
				{
					get
					{
						return meEndOfLifeOption;
					}
					set
					{
						meEndOfLifeOption = value;
					}
				}

				/// <summary>
				/// Resets the class variables to their default values
				/// </summary>
				public override void Reset()
				{
					base.Reset();
					EndOfLifeOption = EParticleSystemEndOfLifeOptions.Nothing;
				}

				/// <summary>
				/// Deep copy the ParticleToCopy's values into this Particle
				/// </summary>
				/// <param name="ParticleToCopy">The Particle whose values should be Copied</param>
				public override void CopyFrom(DPSFParticle ParticleToCopy)
				{
					CParticleSystemLifetimeData cParticleSystemLifetimeData = (CParticleSystemLifetimeData)ParticleToCopy;
					base.CopyFrom(cParticleSystemLifetimeData);
					EndOfLifeOption = cParticleSystemLifetimeData.EndOfLifeOption;
				}
			}

			private List<CParticleSystemEvent> mcParticleSystemEventList = new List<CParticleSystemEvent>();

			private CParticleSystemLifetimeData mcParticleSystemLifetimeData = new CParticleSystemLifetimeData();

			/// <summary>
			/// Get / Set the Lifetime information of the Particle System
			/// </summary>
			public CParticleSystemLifetimeData LifetimeData
			{
				get
				{
					return mcParticleSystemLifetimeData;
				}
				set
				{
					mcParticleSystemLifetimeData = value;
				}
			}

			private int EventSorter(CParticleSystemEvent c1, CParticleSystemEvent c2)
			{
				return c1.iExecutionOrder.CompareTo(c2.iExecutionOrder);
			}

			/// <summary>
			/// Adds a new EveryTime Event with a default Execution Order and Group of zero. 
			/// EveryTime Events fire every frame (i.e. every time the Update() function is called).
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			public void AddEveryTimeEvent(UpdateParticleSystemDelegate cFunctionToCall)
			{
				AddEveryTimeEvent(cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new EveryTime Event with a default Group of zero. 
			/// EveryTime Events fire every frame (i.e. every time the Update() function is called).
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in 
			/// the order they are added.</para></param>
			public void AddEveryTimeEvent(UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddEveryTimeEvent(cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new EveryTime Event. 
			/// EveryTime Events fire every frame (i.e. every time the Update() function is called).
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in 
			/// the order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddEveryTimeEvent(UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				mcParticleSystemEventList.Add(new CParticleSystemEvent(cFunctionToCall, EParticleSystemEventTypes.EveryTime, iExecutionOrder, iGroup));
				mcParticleSystemEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all EveryTime Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to remove</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveEveryTimeEvents(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.EveryTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all EveryTime Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the EveryTime Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveEveryTimeEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.EveryTime && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes an EveryTime Event with the specified Function, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to remove</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveEveryTimeEvent(UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Remove(new CParticleSystemEvent(cFunction, EParticleSystemEventTypes.EveryTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Removes all EveryTime Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllEveryTimeEvents()
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.EveryTime);
			}

			/// <summary>
			/// Returns if there is an EveryTime Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsEveryTimeEvent(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.Exists(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.EveryTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is an EveryTime Event with the specifed Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="cFunction">The Function of the EveryTime Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsEveryTimeEvent(UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Contains(new CParticleSystemEvent(cFunction, EParticleSystemEventTypes.EveryTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Adds a new OneTime Event with a default Execution Order and Group of zero. 
			/// OneTime Events fire once then are automatically removed.
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			public void AddOneTimeEvent(UpdateParticleSystemDelegate cFunctionToCall)
			{
				AddOneTimeEvent(cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new OneTime Event with a default Group of zero. 
			/// OneTime Events fire once then are automatically removed.
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in 
			/// the order they are added.</para></param>
			public void AddOneTimeEvent(UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddOneTimeEvent(cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new OneTime Event. 
			/// OneTime Events fire once then are automatically removed.
			/// </summary>
			/// <param name="cFunctionToCall">The Function to Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in 
			/// the order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddOneTimeEvent(UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				mcParticleSystemEventList.Add(new CParticleSystemEvent(cFunctionToCall, EParticleSystemEventTypes.OneTime, iExecutionOrder, iGroup));
				mcParticleSystemEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all OneTime Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to remove</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveOneTimeEvents(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.OneTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all OneTime Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the OneTime Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveOneTimeEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.OneTime && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes a OneTime Event with the specified Function To Call, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to remove</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveOneTimeEvent(UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Remove(new CParticleSystemEvent(cFunction, EParticleSystemEventTypes.OneTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Removes all OneTime Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllOneTimeEvents()
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.OneTime);
			}

			/// <summary>
			/// Returns if there is an OneTime Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsOneTimeEvent(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.Exists(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.OneTime && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is an OneTime Event with the specifed Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="cFunction">The Function of the OneTime Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsOneTimeEvent(UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Contains(new CParticleSystemEvent(cFunction, EParticleSystemEventTypes.OneTime, iExecutionOrder, iGroup));
			}

			/// <summary>
			/// Adds a new Timed Event with a default Execution Order and Group of zero. 
			/// Timed Events fire when the Particle's Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fTimeToFire">The Time when the Event should fire
			/// (i.e. when the Function should be called)</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			public void AddTimedEvent(float fTimeToFire, UpdateParticleSystemDelegate cFunctionToCall)
			{
				AddTimedEvent(fTimeToFire, cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new Timed Event with a default Group of zero. 
			/// Timed Events fire when the Particle's Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fTimeToFire">The Time when the Event should fire
			/// (i.e. when the Function should be called)</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in 
			/// the order they are added.</para></param>
			public void AddTimedEvent(float fTimeToFire, UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddTimedEvent(fTimeToFire, cFunctionToCall, iExecutionOrder, 0);
			}

			/// <summary>
			/// Adds a new Timed Event. 
			/// Timed Events fire when the Particle's Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fTimeToFire">The Time when the Event should fire
			/// (i.e. when the Function should be called)</param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute.
			/// <para>NOTE: Events with lower Execution Order are executed first.</para>
			/// <para>NOTE: Events with the same Execution Order are not guaranteed to be executed in 
			/// the order they are added.</para></param>
			/// <param name="iGroup">The Group that this Event should belong to</param>
			public void AddTimedEvent(float fTimeToFire, UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				mcParticleSystemEventList.Add(new CTimedParticleSystemEvent(cFunctionToCall, EParticleSystemEventTypes.Timed, iExecutionOrder, iGroup, fTimeToFire));
				mcParticleSystemEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all Timed Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveTimedEvents(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.Timed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all Timed Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the Timed Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveTimedEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.Timed && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes a Timed Event with the specified Function, Time To Fire, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="fTimeToFire">The Time the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveTimedEvent(float fTimeToFire, UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Remove(new CTimedParticleSystemEvent(cFunction, EParticleSystemEventTypes.Timed, iExecutionOrder, iGroup, fTimeToFire));
			}

			/// <summary>
			/// Removes all Timed Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllTimedEvents()
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.Timed);
			}

			/// <summary>
			/// Returns if there is a Timed Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the Timed Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsTimedEvent(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.Exists(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.Timed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is a Timed Event with the specifed Timed To Fire, Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="fTimeToFire">The Time the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function of the Timed Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsTimedEvent(float fTimeToFire, UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Contains(new CTimedParticleSystemEvent(cFunction, EParticleSystemEventTypes.Timed, iExecutionOrder, iGroup, fTimeToFire));
			}

			/// <summary>
			/// Adds a new Normalized Timed Event with a default Execution Order and Group of zero. 
			/// Normalized Timed Events fire when the Particle's Normalized Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) when the Event should fire. 
			/// <para>NOTE: This is clamped to the range of 0.0 - 1.0.</para></param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			public void AddNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleSystemDelegate cFunctionToCall)
			{
				AddNormalizedTimedEvent(fNormalizedTimeToFire, cFunctionToCall, 0, 0);
			}

			/// <summary>
			/// Adds a new Normalized Timed Event with a default Group of zero. 
			/// Normalized Timed Events fire when the Particle's Normalized Elapsed Time reaches the specified Time To Fire.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) when the Event should fire. 
			/// <para>NOTE: This is clamped to the range of 0.0 - 1.0.</para></param>
			/// <param name="cFunctionToCall">The Function To Call when the Event fires</param>
			/// <param name="iExecutionOrder">The Order, relative to other Events, of when this Event 
			/// should Execute. NOTE: Events with lower Execution Order are executed first. NOTE: Events
			/// with the same Execution Order are not guaranteed to be executed in the order they are added.</param>
			public void AddNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder)
			{
				AddNormalizedTimedEvent(fNormalizedTimeToFire, cFunctionToCall, iExecutionOrder, 0);
			}

			public void AddNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleSystemDelegate cFunctionToCall, int iExecutionOrder, int iGroup)
			{
				fNormalizedTimeToFire = MathHelper.Clamp(fNormalizedTimeToFire, 0f, 1f);
				mcParticleSystemEventList.Add(new CTimedParticleSystemEvent(cFunctionToCall, EParticleSystemEventTypes.NormalizedTimed, iExecutionOrder, iGroup, fNormalizedTimeToFire));
				mcParticleSystemEventList.Sort(EventSorter);
			}

			/// <summary>
			/// Removes all Normalized Timed Events with the specified Function.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveNormalizedTimedEvents(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.NormalizedTimed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Removes all Normalized Timed Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove the Normalized Timed Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveNormalizedTimedEvents(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.NormalizedTimed && cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Removes a Normalized Timed Event with the specified Function, Time To Fire, Execution Order, and Group.
			/// Returns true if the Event was found and removed, false if not.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function that is called when the Event fires</param>
			/// <param name="iExectionOrder">The Execution Order of the Event to remove. Default is zero.</param>
			/// <param name="iGroup">The Group that the Event to remove is in. Default is zero.</param>
			/// <returns>Returns true if the Event was found and removed, false if not.</returns>
			public bool RemoveNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleSystemDelegate cFunction, int iExectionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Remove(new CTimedParticleSystemEvent(cFunction, EParticleSystemEventTypes.NormalizedTimed, iExectionOrder, iGroup, fNormalizedTimeToFire));
			}

			/// <summary>
			/// Removes all Normalized Timed Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllNormalizedTimedEvents()
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.NormalizedTimed);
			}

			/// <summary>
			/// Returns if there is a NormalizedTimed Event with the specified Function or not.
			/// </summary>
			/// <param name="cFunction">The Function of the NormalizedTimed Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function was found, false if not</returns>
			public bool ContainsNormalizedTimedEvent(UpdateParticleSystemDelegate cFunction)
			{
				return Extensions.Exists(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.eType == EParticleSystemEventTypes.NormalizedTimed && cEvent.cFunctionToCall == cFunction);
			}

			/// <summary>
			/// Returns if there is a NormalizedTimed Event with the specifed Normalized Time To Fire, Function, Execution Order, and Group or not.
			/// </summary>
			/// <param name="fNormalizedTimeToFire">The Normalized Time (0.0 - 1.0) the Event is scheduled to fire at</param>
			/// <param name="cFunction">The Function of the NormalizedTimed Event to look for</param>
			/// <param name="iExecutionOrder">The Execution Order of the Event to look for</param>
			/// <param name="iGroup">The Group of the Event to look for</param>
			/// <returns>Returns true if an Event with the specified Function, Execution Order, and Group was found, false if not</returns>
			public bool ContainsNormalizedTimedEvent(float fNormalizedTimeToFire, UpdateParticleSystemDelegate cFunction, int iExecutionOrder, int iGroup)
			{
				return mcParticleSystemEventList.Contains(new CTimedParticleSystemEvent(cFunction, EParticleSystemEventTypes.NormalizedTimed, iExecutionOrder, iGroup, fNormalizedTimeToFire));
			}

			/// <summary>
			/// Removes all Timed Events and Normalized Timed Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllTimedAndNormalizedTimedEvents()
			{
				return RemoveAllTimedEvents() + RemoveAllNormalizedTimedEvents();
			}

			/// <summary>
			/// Removes all Events.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllEvents()
			{
				int count = mcParticleSystemEventList.Count;
				mcParticleSystemEventList.Clear();
				return count;
			}

			/// <summary>
			/// Removes all Events in the specified Group.
			/// Returns the number of Events that were removed.
			/// </summary>
			/// <param name="iGroup">The Group to remove all Events from</param>
			/// <returns>Returns the number of Events that were removed.</returns>
			public int RemoveAllEventsInGroup(int iGroup)
			{
				return Extensions.RemoveAll(mcParticleSystemEventList, (CParticleSystemEvent cEvent) => cEvent.iGroup == iGroup);
			}

			/// <summary>
			/// Updates the Particle System according to the Particle System Events. This is done automatically
			/// by the Particle System every frame (i.e. Everytime the Update() function is called).
			/// </summary>
			/// <param name="fElapsedTimeInSeconds">How much Time has passed, in seconds, 
			/// since the last Update</param>
			public void Update(float fElapsedTimeInSeconds)
			{
				Update(fElapsedTimeInSeconds, fElapsedTimeInSeconds);
				RemoveAllOneTimeEvents();
			}

			/// <summary>
			/// Updates the Particle System according to the Particle System Events
			/// </summary>
			/// <param name="fElapsedTimeThisPass">The amount of Elapsed Time to pass
			/// into the Event Functions being called on this Pass</param>
			/// <param name="fTotalElapsedTimeThisFrame">How much Time has passed, in seconds, 
			/// since the last Frame</param>
			private void Update(float fElapsedTimeThisPass, float fTotalElapsedTimeThisFrame)
			{
				bool flag = false;
				float fElapsedTimeThisPass2 = 0f;
				LifetimeData.UpdateElapsedTimeVariables(fElapsedTimeThisPass);
				if (mcParticleSystemEventList.Count <= 0)
				{
					return;
				}
				if (LifetimeData.ElapsedTime >= LifetimeData.Lifetime && LifetimeData.EndOfLifeOption == EParticleSystemEndOfLifeOptions.Repeat)
				{
					flag = true;
					fElapsedTimeThisPass2 = LifetimeData.ElapsedTime - LifetimeData.Lifetime;
					fElapsedTimeThisPass -= fElapsedTimeThisPass2;
					fElapsedTimeThisPass = Math.Max(fElapsedTimeThisPass, 0f);
					fElapsedTimeThisPass2 %= LifetimeData.Lifetime;
					LifetimeData.ElapsedTime = LifetimeData.LastElapsedTime;
					LifetimeData.UpdateElapsedTimeVariables(LifetimeData.Lifetime - LifetimeData.ElapsedTime);
				}
				if (fElapsedTimeThisPass > 0f)
				{
					for (int i = 0; i < mcParticleSystemEventList.Count; i++)
					{
						if (mcParticleSystemEventList[i].eType == EParticleSystemEventTypes.EveryTime)
						{
							mcParticleSystemEventList[i].cFunctionToCall(fElapsedTimeThisPass);
							continue;
						}
						if (mcParticleSystemEventList[i].eType == EParticleSystemEventTypes.OneTime)
						{
							if (!flag)
							{
								mcParticleSystemEventList[i].cFunctionToCall(fTotalElapsedTimeThisFrame);
							}
							continue;
						}
						CTimedParticleSystemEvent cTimedParticleSystemEvent = (CTimedParticleSystemEvent)mcParticleSystemEventList[i];
						if (cTimedParticleSystemEvent.eType == EParticleSystemEventTypes.Timed)
						{
							float num = ((LifetimeData.LastElapsedTime == 0f) ? (-0.1f) : LifetimeData.LastElapsedTime);
							if (num < cTimedParticleSystemEvent.fTimeToFire && LifetimeData.ElapsedTime >= cTimedParticleSystemEvent.fTimeToFire)
							{
								cTimedParticleSystemEvent.cFunctionToCall(fElapsedTimeThisPass);
							}
						}
						else
						{
							float num2 = ((LifetimeData.LastNormalizedElapsedTime == 0f) ? (-0.1f) : LifetimeData.LastNormalizedElapsedTime);
							if (num2 < cTimedParticleSystemEvent.fTimeToFire && LifetimeData.NormalizedElapsedTime >= cTimedParticleSystemEvent.fTimeToFire)
							{
								cTimedParticleSystemEvent.cFunctionToCall(fElapsedTimeThisPass);
							}
						}
					}
				}
				if (flag)
				{
					LifetimeData.ElapsedTime = 0f;
					Update(fElapsedTimeThisPass2, fTotalElapsedTimeThisFrame);
				}
			}
		}

		private const int MAX_MEMORY_IN_BYTES_THAT_XBOX_CAN_DRAW = 524287;

		private InitializeParticleDelegate mcParticleInitializationFunction;

		private UpdateVertexDelegate mcVertexUpdateFunction;

		private ParticleTypes meParticleType;

		private Particle[] mcParticles;

		private int miNumberOfParticlesToDraw;

		private Vertex[] mcParticleVerticesToDraw;

		private int[] miaIndexBufferArray;

		private short[] msaIndexBufferReachArray;

		private int miIndexBufferIndex;

		private SpriteBatch mcSpriteBatch;

		private Particle[] mcParticleSpritesToDraw;

		private SpriteBatchSettings mcSpriteBatchSettings;

		private LinkedList<Particle> mcActiveParticlesList;

		private LinkedList<Particle> mcInactiveParticlesList;

		private VertexDeclaration mcVertexDeclaration;

		private int miVertexSizeInBytes;

		private RenderProperties mcRenderProperties;

		private Effect mcEffect;

		private Texture2D mcTexture;

		private int miMaxNumberOfParticlesAllowed;

		private bool mbPerformDraws = true;

		private bool mbPerformUpdates = true;

		private int miDrawOrder;

		private int miUpdateOrder;

		private Game mcGame;

		private GraphicsDevice mcGraphicsDevice;

		private ContentManager mcContentManager;

		private float mfSimulationSpeed = 1f;

		private float mfInternalSimulationSpeed = 1f;

		private float mfTimeToWaitBetweenUpdates;

		private float mfTimeElapsedSinceLastUpdate;

		private CParticleEvents mcParticleEvents;

		private CParticleSystemEvents mcParticleSystemEvents;

		private AutoMemoryManagerSettings mcAutoMemoryManagerSettings;

		private float mfAutoMemoryManagersElapsedTime;

		private int miAutoMemoryManagerMaxNumberOfParticlesActiveAtOnceOverTheLastXSeconds;

		private ParticleEmitter mcEmitter;

		private bool _lerpEmittersPositionAndOrientation = true;

		private Vector3 _emittersPreviousPosition = Vector3.Zero;

		private Quaternion _emittersPreviousOrientation = Quaternion.Identity;

		private Stopwatch _performanceProfilingStopwatch;

		private bool _performanceProfilingIsEnabled;

		private int miID;

		private int miType;

		private RandomNumbers mcRandom;

		private Matrix mcWorld = Matrix.Identity;

		private Matrix mcView = Matrix.Identity;

		private Matrix mcProjection = Matrix.Identity;

		private ParticleSystemManager mcParticleSystemManager;

		private static DPSFDefaultEffect SmcDPSFEffect;

		/// <summary>
		/// A static int used to keep track of the total number of Particle Systems created
		/// </summary>
		private static int _totalNumberOfParticleSystemsCreated;

		/// <summary>
		/// A static int used to keep track of how many DPSF particle systems are initialized at any given moment.
		/// </summary>
		private static int _numberOfParticleSystemsCurrentlyInitialized;

		private int miMaxParticlesThatXboxCanDrawAtOnce;

		/// <summary>
		/// The path used to load the Texture when the InitializeNonSerializableProperties() function is called.
		/// <para>NOTE: This is automatically set when the SetTexture() function is called.</para>
		/// </summary>
		public string DeserializationTexturePath { get; set; }

		/// <summary>
		/// The path used to load the Effect when the InitializeNonSerializableProperties() function is called.
		/// <para>NOTE: This is automatically set when the SetEffectAndTechnique(string, string) function is called. </para>
		/// </summary>
		public string DeserializationEffectPath { get; set; }

		/// <summary>
		/// The Name of the Technique to use when the InitializeNonSerializableProperties() function is called.
		/// <para>NOTE: This is automatically set when the SetEffectAndTechnique() and SetTechnique() functions are called.</para>
		/// </summary>
		public string DeserializationTechniqueName { get; set; }

		/// <summary>
		/// Returns true if the Particle System is Initialized, false if not.
		/// </summary>
		/// <returns>Returns true if the Particle System is Initialized, false if not.</returns>
		public bool IsInitialized
		{
			get
			{
				if (mcParticles != null && meParticleType != ParticleTypes.None)
				{
					if (GraphicsDevice == null)
					{
						return meParticleType == ParticleTypes.NoDisplay;
					}
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// A custom effect provided by DPSF. In DPSF v2.1.0 and prior this effect was used as the default effect for all particle system types.
		/// Each particle system type now uses one of the built-in XNA 4 effects as its default effect in order to make all of the particle system types
		/// fully compatible with the Reach profile, and usable on the Windows Phone 7.
		/// <para>This effect may still be used for a particle system by calling the SetEffectAndTechnique() function from the particle system's overridden
		/// InitializeRenderProperties() function.</para>
		/// <para>This Effect has several techniques that may be used (<see cref="T:DPSF.DPSFDefaultEffectTechniques" />).</para>
		/// </summary>
		public DPSFDefaultEffect DPSFDefaultEffect => SmcDPSFEffect;

		/// <summary>
		/// Get / Set if this Particle System should Draw its Particles or not.
		/// <para>NOTE: Setting this to false causes the Draw() function to not draw anything, including the 
		/// BeforeDraw() and AfterDraw() functions not to be called.</para>
		/// </summary>
		[DPSFViewerParameter(Description = "Toggle whether the particle system should be visible or not.", Group = "DPSF")]
		public bool Visible
		{
			get
			{
				return mbPerformDraws;
			}
			set
			{
				bool flag = mbPerformDraws;
				mbPerformDraws = value;
				if (flag != mbPerformDraws && VisibleChanged != null)
				{
					VisibleChanged(this, null);
				}
			}
		}

		/// <summary>
		/// Get / Set if this Particle System should Update itself and its Particles or not.
		/// <para>NOTE: Setting this to false causes the Update() function to not update anything.</para>
		/// </summary>
		[DPSFViewerParameter(Description = "Toggle whether the particle system should be updated or not.", Group = "DPSF")]
		public bool Enabled
		{
			get
			{
				return mbPerformUpdates;
			}
			set
			{
				mbPerformUpdates = value;
				if (EnabledChanged != null)
				{
					EnabledChanged(this, null);
				}
			}
		}

		/// <summary>
		/// The Order in which the Particle System should be Updated relative to other 
		/// DPSF Particle Systems in the same Particle System Manager. Particle Systems 
		/// are Updated in ascending order according to their Update Order (i.e. lowest first).
		/// <para>NOTE: The Update Order is one of the few properties that is not reset when
		/// the particle system is initialized or destroyed.</para>
		/// </summary>
		public int UpdateOrder
		{
			get
			{
				return miUpdateOrder;
			}
			set
			{
				miUpdateOrder = value;
				if (UpdateOrderChanged != null)
				{
					UpdateOrderChanged(this, null);
				}
			}
		}

		/// <summary>
		/// The Order in which the Particle System should be Drawn relative to other
		/// DPSF Particle Systems in the same Particle System Manager. Particle Systems
		/// are Drawn in ascending order according to their Draw Order (i.e. lowest first).
		/// <para>NOTE: The Draw Order is one of the few properties that is not reset when
		/// the particle system is initialized or destroyed.</para>
		/// </summary>
		public int DrawOrder
		{
			get
			{
				return miDrawOrder;
			}
			set
			{
				miDrawOrder = value;
				if (DrawOrderChanged != null)
				{
					DrawOrderChanged(this, null);
				}
			}
		}

		/// <summary>
		/// Get the Game object set in the constructor, if one was given.
		/// </summary>
		public Game Game => mcGame;

		/// <summary>
		/// Get / Set the Graphics Device to draw to
		/// </summary>
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return mcGraphicsDevice;
			}
			set
			{
				if (value != null || ParticleType == ParticleTypes.NoDisplay)
				{
					mcGraphicsDevice = value;
					return;
				}
				throw new ArgumentNullException("GraphicsDevice", "The specified Graphics Device is null. A valid Graphics Device is required.");
			}
		}

		/// <summary>
		/// Get if the Particle System is inheriting from DrawableGameComponent or not.
		/// <para>If inheriting from DrawableGameComponent, the Particle Systems
		/// are automatically added to the given Game object's Components and the
		/// Update() and Draw() functions are automatically called by the
		/// Game object when it updates and draws the rest of its Components.
		/// If the Update() and Draw() functions are called by the user anyways,
		/// they will exit without performing any operations, so it is suggested
		/// to include them anyways to make switching between inheriting and
		/// not inheriting from DrawableGameComponent seemless; just be aware
		/// that the updates and draws are actually being performed when the
		/// Game object is told to update and draw (i.e. when base.Update() and base.Draw()
		/// are called), not when these functions are being called.</para>
		/// </summary>
		public bool InheritsDrawableGameComponent => false;

		/// <summary>
		/// Get the unique ID of this Particle System.
		/// <para>NOTE: Each Particle System is automatically assigned a unique ID when it is instanciated.</para>
		/// </summary>
		public int ID => miID;

		/// <summary>
		/// Get / Set the Type of Particle System this is. This is a user provided value that you can use for whatever
		/// purpose you want; it is not used by the built-in DPSF functionality in any way.
		/// </summary>
		public int Type
		{
			get
			{
				return miType;
			}
			set
			{
				miType = value;
			}
		}

		/// <summary>
		/// Get the Name of the Class that this Particle System is using. This can be used to 
		/// check what type of Particle System this is at run-time.
		/// </summary>
		[DPSFViewerParameter(Description = "Get the Name of the Class that this Particle System is using.", Group = "DPSF")]
		public string ClassName => GetType().Name;

		/// <summary>
		/// Get / Set the Content Manager to use to load Textures and Effects
		/// </summary>
		public ContentManager ContentManager
		{
			get
			{
				if (mcContentManager != null)
				{
					return mcContentManager;
				}
				throw new NullReferenceException("The Content Manager is trying to be accessed, but is null. Be sure you have Initialized the particle system and provided a valid Content Manager.");
			}
			set
			{
				if (value != null || ParticleType == ParticleTypes.NoDisplay)
				{
					mcContentManager = value;
					return;
				}
				throw new ArgumentNullException("ContentManager", "The specified Content Manager is null. A valid Content Manager is required.");
			}
		}

		/// <summary>
		/// Get / Set the Index Buffer values. The Index Buffer is used when drawing Quads in the HiDef profile.
		/// </summary>
		protected int[] IndexBuffer
		{
			get
			{
				return miaIndexBufferArray;
			}
			set
			{
				miaIndexBufferArray = value;
			}
		}

		/// <summary>
		/// Get / Set the Index Buffer values. The Index Buffer Reach is used when drawing Quads in the Reach profile.
		/// </summary>
		protected short[] IndexBufferReach
		{
			get
			{
				return msaIndexBufferReachArray;
			}
			set
			{
				msaIndexBufferReachArray = value;
			}
		}

		/// <summary>
		/// Get / Set the current position in the Index Buffer
		/// </summary>
		protected int IndexBufferIndex
		{
			get
			{
				return miIndexBufferIndex;
			}
			set
			{
				miIndexBufferIndex = value;
			}
		}

		/// <summary>
		/// Particle Events may be used to update Particles
		/// </summary>
		public CParticleEvents ParticleEvents
		{
			get
			{
				if (mcParticleEvents != null)
				{
					return mcParticleEvents;
				}
				throw new NullReferenceException("The ParticleEvents property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
		}

		/// <summary>
		/// Particle System Events may be used to update the Particle System
		/// </summary>
		public CParticleSystemEvents ParticleSystemEvents
		{
			get
			{
				if (mcParticleSystemEvents != null)
				{
					return mcParticleSystemEvents;
				}
				throw new NullReferenceException("The ParticleSystemEvents property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
		}

		/// <summary>
		/// Get the render properties used to draw the particles.
		/// </summary>
		public RenderProperties RenderProperties => mcRenderProperties;

		/// <summary>
		/// Returns if this particle system is dependent on an external Sprite Batch to draw its particles or not.
		/// <para>If false, the particle system will use its own SpriteBatch to draw its particles.</para>
		/// <para>If true, then you must call SpriteBatch.Begin() before calling ParticleSystem.Draw() to
		/// draw the particle system, and then call SpriteBatch.End() when done drawing the particle system, where
		/// the SpriteBatch referred to here is the one you passed into the InitializeSpriteParticleSystem() function.</para>
		/// <para>NOTE: This property only applies to Sprite particle systems.</para>
		/// </summary>
		public bool UsingExternalSpriteBatchToDrawParticles { get; private set; }

		/// <summary>
		/// The Sprite Batch drawing Settings used in the Sprite Batch's Begin() function call.
		/// <para>NOTE: These settings are only available for Sprite particle systems, and only for
		/// the Sprite particle systems using their own SpriteBatch (i.e. UsingExternalSpriteBatchToDrawParticles = false).</para>
		/// </summary>
		public SpriteBatchSettings SpriteBatchSettings
		{
			get
			{
				if (mcSpriteBatchSettings != null)
				{
					return mcSpriteBatchSettings;
				}
				throw new NullReferenceException("The SpriteBatchSettings property is trying to be accessed, but is null. Be sure you have Initialized the particle system. Also, this property is only available when using a Sprite particle system, and not Using and External Sprite Batch To Draw the Particles.");
			}
		}

		/// <summary>
		/// The Settings used to control the Automatic Memory Manager
		/// </summary>
		public AutoMemoryManagerSettings AutoMemoryManagerSettings
		{
			get
			{
				if (mcAutoMemoryManagerSettings != null)
				{
					return mcAutoMemoryManagerSettings;
				}
				throw new NullReferenceException("The AutoMemoryManagerSettings property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
		}

		/// <summary>
		/// The Emitter is used to automatically generate new Particles
		/// </summary>
		[DPSFViewerParameter(Description = "The Emitter used by the particle system.", Group = "DPSF")]
		public ParticleEmitter Emitter
		{
			get
			{
				if (mcEmitter != null)
				{
					return mcEmitter;
				}
				throw new NullReferenceException("The Emitter property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
			set
			{
				if (value != null)
				{
					mcEmitter = value;
					return;
				}
				throw new ArgumentNullException("Emitter", "An invalid Emitter was specified. The Emitter cannot be null.");
			}
		}

		/// <summary>
		/// This property tells if the we should Lerp (Linearly Interpolate) the Position and Orientation of the Emitter from one update to 
		/// the next. If the Emitter is moving very fast, this allows the particle system to spawn new particles in between the Emitter's old
		/// and new position, so that new particles are evenly spaced out between the Emitter's previous and current position, instead of all 
		/// of the particles being spawned at the Emitter's new position.
		/// <para>If this property is true, the Emitter's Position and Orientation will be Lerped while emitting particles.</para>
		/// <para>If this property is false, all of the particles will be emitted as the Emitter's current Position and Orientation.</para>
		/// <para>If you generally want Lerping enabled, but want to temporarily disable it to "teleport" the emitter from one position
		/// to another without particles being Lerped between the two positions, you can set this properly to false and then back to true 
		/// after the particle system's Update() function has been called, or you can simply set the LerpEmittersPositionAndOrientationOnNextUpdate
		/// to false, which will disable Lerping the position and orientation only for the next particle system Update().</para>
		/// <para>Default is true.</para>
		/// </summary>
		[DPSFViewerParameter(Description = "This property tells if the we should Lerp (Linearly Interpolate) the Position and Orientation of the Emitter from one update to the next.", Group = "DPSF")]
		public bool LerpEmittersPositionAndOrientation
		{
			get
			{
				return _lerpEmittersPositionAndOrientation;
			}
			set
			{
				_lerpEmittersPositionAndOrientation = value;
			}
		}

		/// <summary>
		/// If this is true the Emitter's Position and Orientation will not be Lerped during the particle system's next Update() function call.
		/// The Update() function will always set this value back to false after all of the particle's have been emitted for that Update() call.
		/// <para>Setting this to true allows you to "teleport" the Emitter from one position to another without particles being released at any
		/// positions in between the Emitter's old and new Position and Orientation.</para>
		/// </summary>
		public bool LerpEmittersPositionAndOrientationOnNextUpdate { get; set; }

		/// <summary>
		/// Get a RandomNumbers object used to generate Random Numbers
		/// </summary>
		public RandomNumbers RandomNumber
		{
			get
			{
				if (mcRandom != null)
				{
					return mcRandom;
				}
				throw new NullReferenceException("The RandomNumber property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
		}

		/// <summary>
		/// Get / Set the World Matrix to use for drawing 3D Particles
		/// </summary>
		public Matrix World
		{
			get
			{
				return mcWorld;
			}
			set
			{
				mcWorld = value;
			}
		}

		/// <summary>
		/// Get / Set the View Matrix to use for drawing 3D Particles
		/// </summary>
		public Matrix View
		{
			get
			{
				return mcView;
			}
			set
			{
				mcView = value;
			}
		}

		/// <summary>
		/// Get / Set the Projection Matrix to use for drawing 3D Particles
		/// </summary>
		public Matrix Projection
		{
			get
			{
				return mcProjection;
			}
			set
			{
				mcProjection = value;
			}
		}

		/// <summary>
		/// Gets the result of multiplying the World, View, and Projection matrices.
		/// </summary>
		public Matrix WorldViewProjection => Matrix.Multiply(Matrix.Multiply(World, View), Projection);

		/// <summary>
		/// Set the VertexElement (i.e. Vertex Format) to use for each vertex of a Particle.
		/// <para>NOTE: VertexElement will not be changed if null value is given.</para>
		/// </summary>
		private VertexElement[] VertexElement
		{
			set
			{
				if (value != null && GraphicsDevice != null)
				{
					mcVertexDeclaration = new VertexDeclaration(value);
					return;
				}
				if (ParticleType == ParticleTypes.None || ParticleType == ParticleTypes.NoDisplay || ParticleType == ParticleTypes.Sprite)
				{
					mcVertexDeclaration = null;
					return;
				}
				if (GraphicsDevice == null)
				{
					throw new ArgumentNullException("GraphicsDevice", "The current Graphics Device is null. A valid Graphics Device is required to create a new Vertex Declaration in order to draw the current type of Particles.");
				}
				throw new ArgumentNullException("VertexElement", "The specified Vertex Element is null. A valid Vertex Element is required to draw the current Type of Particles.");
			}
		}

		/// <summary>
		/// Set the function to use to copy a Particle's renderable properties into the Vertex Buffer.
		/// <para>NOTE: VertexUpdateFunction will not be changed if null value is given.</para>
		/// </summary>
		public UpdateVertexDelegate VertexUpdateFunction
		{
			set
			{
				if (value == null)
				{
					if (ParticleType != ParticleTypes.None && ParticleType != ParticleTypes.NoDisplay && ParticleType != ParticleTypes.Sprite)
					{
						throw new ArgumentNullException("VertexUpdateFunction", "The specified Vertex Update Function is null. A valid Vertex Update Function is required to draw the current Type of Particles.");
					}
					mcVertexUpdateFunction = null;
				}
				else
				{
					mcVertexUpdateFunction = value.Invoke;
				}
			}
		}

		/// <summary>
		/// Sets the function to use to Initialize a Particle's properties.
		/// </summary>
		public InitializeParticleDelegate ParticleInitializationFunction
		{
			set
			{
				if (value != null)
				{
					mcParticleInitializationFunction = value.Invoke;
				}
				else
				{
					mcParticleInitializationFunction = null;
				}
			}
		}

		/// <summary>
		/// Get / Set the Effect to use to draw the Particles
		/// </summary>
		public Effect Effect
		{
			get
			{
				return mcEffect;
			}
			set
			{
				if (value == null)
				{
					if (ParticleType != ParticleTypes.None && ParticleType != ParticleTypes.NoDisplay && ParticleType != ParticleTypes.Sprite)
					{
						throw new ArgumentNullException("Effect", "Specified Effect to use is null. A valid Effect must be used to draw the current Type of Particles.");
					}
					mcEffect = null;
				}
				else
				{
					mcEffect = value.Clone();
				}
			}
		}

		/// <summary>
		/// Get / Set which Technique of the current Effect to use to draw the Particles
		/// </summary>
		public EffectTechnique Technique
		{
			get
			{
				if (mcEffect != null)
				{
					return mcEffect.CurrentTechnique;
				}
				return null;
			}
			set
			{
				if (mcEffect == null)
				{
					throw new InvalidOperationException("Effect is null when trying to specify the Technique to use. The Effect must be set before specifying the Technique.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("Technique", "An invalid Technique to use was specified. The Technique to use cannot be null.");
				}
				mcEffect.CurrentTechnique = value;
			}
		}

		/// <summary>
		/// Get / Set the Texture to use to draw the Particles
		/// </summary>
		public Texture2D Texture
		{
			get
			{
				return mcTexture;
			}
			set
			{
				if (value == null)
				{
					if (ParticleType == ParticleTypes.Sprite || ParticleType == ParticleTypes.TexturedQuad)
					{
						throw new ArgumentNullException("sTexture", "Specified Texture to use is null. A valid Texture must be set to draw the current Type of Particles.");
					}
					mcTexture = null;
				}
				else
				{
					mcTexture = value;
				}
			}
		}

		/// <summary>
		/// Get / Set how fast the Particle System Simulation should run.
		/// <para>Example: 1.0 = normal speed, 0.5 = half speed, 2.0 = double speed.</para>
		/// <para>NOTE: If a negative value is specified, the Speed Scale is set 
		/// to zero (pauses the simulation; has same effect as Enabled = false).</para>
		/// </summary>
		public float SimulationSpeed
		{
			get
			{
				return mfSimulationSpeed;
			}
			set
			{
				if (value < 0f)
				{
					mfSimulationSpeed = 0f;
				}
				else
				{
					mfSimulationSpeed = value;
				}
			}
		}

		/// <summary>
		/// Get / Set how fast the Particle System Simulation should run to look "normal".
		/// <para>1.0 = normal speed, 0.5 = half speed, 2.0 = double speed.</para>
		/// <para>This is provided as a way of speeding up / slowing down the simulation to have 
		/// it look as desired, without having to rescale all of the particle velocities, etc. This allows
		/// you to use the exact same particle system class to create two particle systems, and then have one run
		/// slower or faster than the other, creating two different effects. If you then wanted to speed up or slow down
		/// both effects (i.e. particle systems), you could adjust the SimulationSpeed property on both particle systems 
		/// without having to worry about adjusting this property at all to get the effects back to normal speed; just reset 
		/// the SimulationSpeed property you changed back to 1.0.</para>
		/// <para>NOTE: If a negative value is specified, the Internal Simulation Speed is set to zero 
		/// (pauses the simulation; has the same effect as Enabled = false).</para>
		/// </summary>
		public float InternalSimulationSpeed
		{
			get
			{
				return mfInternalSimulationSpeed;
			}
			set
			{
				if (value < 0f)
				{
					mfInternalSimulationSpeed = 0f;
				}
				else
				{
					mfInternalSimulationSpeed = value;
				}
			}
		}

		/// <summary>
		/// Specify how often the Particle System should be Updated.
		/// <para>NOTE: Specifying a value of zero (default) will cause the Particle 
		/// System to be Updated every time the Update() function is called 
		/// (i.e. as often as possible).</para>
		/// <para>NOTE: If the Update() function is not called often enough to
		/// keep up with this specified Update rate, the Update function
		/// updates the Particle Systems as often as possible.</para>
		/// </summary>
		public int UpdatesPerSecond
		{
			get
			{
				if (mfTimeToWaitBetweenUpdates == 0f)
				{
					return 0;
				}
				return (int)(1f / mfTimeToWaitBetweenUpdates);
			}
			set
			{
				if (value <= 0)
				{
					mfTimeToWaitBetweenUpdates = 0f;
				}
				else
				{
					mfTimeToWaitBetweenUpdates = 1f / (float)value;
				}
			}
		}

		/// <summary>
		/// Get / Set if performance timings should be measured or not, such as how long it takes to perform updates and draws.
		/// <para>This should be disabled before building a release version of your application.</para>
		/// <para>Note: Performance profiling is not available on the Reach profile, so this will always return False on the Reach profile.</para>
		/// </summary>
		public bool PerformanceProfilingIsEnabled
		{
			get
			{
				return _performanceProfilingIsEnabled;
			}
			set
			{
				if (GraphicsDevice != null && GraphicsDevice.GraphicsProfile == GraphicsProfile.Reach)
				{
					_performanceProfilingIsEnabled = false;
				}
				else
				{
					_performanceProfilingIsEnabled = value;
				}
				if (_performanceProfilingIsEnabled)
				{
					if (_performanceProfilingStopwatch == null)
					{
						_performanceProfilingStopwatch = new Stopwatch();
					}
				}
				else
				{
					_performanceProfilingStopwatch = null;
				}
			}
		}

		/// <summary>
		/// Get how long (in milliseconds) it took to perform the last Update() function call.
		/// <para>Returns 0 if Performance Profiling is not Enabled.</para>
		/// </summary>
		public double PerformanceTimeToDoUpdateInMilliseconds { get; private set; }

		/// <summary>
		/// Get how long (in milliseconds) it took to perform the last Draw() function call.
		/// <para>Returns 0 if Performance Profiling is not Enabled.</para>
		/// </summary>
		public double PerformanceTimeToDoDrawInMilliseconds { get; private set; }

		/// <summary>
		/// The Particle System Manager whose properties (SimulationSpeed and 
		/// UpdatesPerSecond) this particle system should follow.  If null is not specified,
		/// the Manager's properties will be copied into this particle system immediately.
		/// <para>NOTE: This Particle System's properties will only clone the Manager's properties
		/// if the Manager's properties are Enabled. For example, the Manager's SimulationSpeed
		/// will only be copied to this Particle System if the Manager's SimulationSpeedIsEnabled
		/// property is true.</para>
		/// <para>NOTE: This value is automatically set to the last Particle System Manager the 
		/// Particle System is added to.</para>
		/// </summary>
		public ParticleSystemManager ParticleSystemManagerToCopyPropertiesFrom
		{
			get
			{
				return mcParticleSystemManager;
			}
			set
			{
				mcParticleSystemManager = value;
				if (mcParticleSystemManager != null)
				{
					if (mcParticleSystemManager.SimulationSpeedIsEnabled)
					{
						SimulationSpeed = mcParticleSystemManager.SimulationSpeed;
					}
					if (mcParticleSystemManager.UpdatesPerSecondIsEnabled)
					{
						UpdatesPerSecond = mcParticleSystemManager.UpdatesPerSecond;
					}
				}
			}
		}

		/// <summary>
		/// Get the type of Particles that this Particle System should draw.
		/// </summary>
		public ParticleTypes ParticleType
		{
			get
			{
				return meParticleType;
			}
			private set
			{
				meParticleType = value;
			}
		}

		/// <summary>
		/// Get / Set the absolute Number of Particles to Allocate Memory for.
		/// <para>NOTE: This value must be greater than or equal to zero.</para>
		/// <para>NOTE: Even if this many particles aren't used, the space for this many Particles 
		/// is still allocated in memory.</para>
		/// </summary>
		public int NumberOfParticlesAllocatedInMemory
		{
			get
			{
				if (mcParticles == null)
				{
					return 0;
				}
				return mcParticles.Length;
			}
			set
			{
				if (value >= 0)
				{
					bool flag = mcActiveParticlesList != null && mcActiveParticlesList.Count > 0;
					LinkedList<Particle> linkedList = (flag ? mcActiveParticlesList : null);
					Vertex[] array = (flag ? mcParticleVerticesToDraw : null);
					int[] array2 = (flag ? miaIndexBufferArray : null);
					short[] array3 = (flag ? msaIndexBufferReachArray : null);
					int num = miNumberOfParticlesToDraw;
					Particle[] array4 = (flag ? mcParticleSpritesToDraw : null);
					mcParticles = new Particle[value];
					mcActiveParticlesList = new LinkedList<Particle>();
					mcInactiveParticlesList = new LinkedList<Particle>();
					InitializeParticleArrays();
					if (!flag)
					{
						return;
					}
					if (num > value)
					{
						num = value;
					}
					LinkedListNode<Particle> linkedListNode = linkedList.Last;
					while (linkedListNode != null && AddParticle(linkedListNode.Value))
					{
						linkedListNode = linkedListNode.Previous;
					}
					if (num <= 0)
					{
						return;
					}
					switch (meParticleType)
					{
					case ParticleTypes.Quad:
					case ParticleTypes.TexturedQuad:
					{
						int num2 = num * 4;
						for (int j = 0; j < num2; j++)
						{
							mcParticleVerticesToDraw[j] = array[j];
						}
						if (GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef)
						{
							int val = ((miaIndexBufferArray != null) ? miaIndexBufferArray.Length : 0);
							int num3 = Math.Min(num * 6, val);
							for (int k = 0; k < num3; k++)
							{
								miaIndexBufferArray[k] = array2[k];
							}
						}
						else
						{
							int val2 = ((msaIndexBufferReachArray != null) ? msaIndexBufferReachArray.Length : 0);
							int num4 = Math.Min(num * 6, val2);
							for (int l = 0; l < num4; l++)
							{
								msaIndexBufferReachArray[l] = array3[l];
							}
						}
						break;
					}
					case ParticleTypes.Sprite:
					{
						for (int i = 0; i < num; i++)
						{
							mcParticleSpritesToDraw[i] = array4[i];
						}
						break;
					}
					}
					miNumberOfParticlesToDraw = num;
					return;
				}
				throw new ArgumentException("MaxNumberOfParticles", "The specified Max Number Of Particles is less than or equal to zero. The Max Number Of Particles must be greater than zero.");
			}
		}

		/// <summary>
		/// Get / Set the Max Number of Particles this Particle System is Allowed to contain at any given time.
		/// <para>NOTE: The Automatic Memory Manager will never allocate space for more Particles than this.</para>
		/// <para>NOTE: This value must be greater than or equal to zero.</para>
		/// </summary>
		public int MaxNumberOfParticlesAllowed
		{
			get
			{
				return miMaxNumberOfParticlesAllowed;
			}
			set
			{
				int num = value;
				if (num < 0)
				{
					num = 0;
				}
				miMaxNumberOfParticlesAllowed = num;
			}
		}

		/// <summary>
		/// Get the number of Particles that are currently Active
		/// </summary>
		public int NumberOfActiveParticles
		{
			get
			{
				if (IsInitialized)
				{
					return mcActiveParticlesList.Count;
				}
				return 0;
			}
		}

		/// <summary>
		/// Get the number of Particles being Drawn. That is, how many Particles 
		/// are both Active AND Visible.
		/// </summary>
		public int NumberOfParticlesBeingDrawn => miNumberOfParticlesToDraw;

		/// <summary>
		/// Get the number of Particles that may still be added before reaching the
		/// Max Number Of Particles Allowed. If the Max Number Of Particles Allowed is 
		/// greater than the Number Of Particles Allocated In Memory AND the Auto Memory Manager is
		/// set to not increase the amount of Allocated Memory, than this returns the number 
		/// of Particles that may still be added before running out of Memory.
		/// </summary>
		public int NumberOfParticlesStillPossibleToAdd
		{
			get
			{
				if (NumberOfParticlesAllocatedInMemory < MaxNumberOfParticlesAllowed && AutoMemoryManagerSettings.MemoryManagementMode != AutoMemoryManagerModes.IncreaseAndDecrease && AutoMemoryManagerSettings.MemoryManagementMode != AutoMemoryManagerModes.IncreaseOnly)
				{
					return NumberOfParticlesAllocatedInMemory - NumberOfActiveParticles;
				}
				return MaxNumberOfParticlesAllowed - NumberOfActiveParticles;
			}
		}

		/// <summary>
		/// Get / Protected Set a Linked List whose Nodes point to the Active Particles.
		/// <para>NOTE: The Protected Set option is only provided to allow the order of the 
		/// LinkedListNodes to be changed, changing the update and drawing 
		/// order of the Particles. Be sure that all of the original LinkedListNodes 
		/// (and only the original LinkedListNodes, no more) obtained from the 
		/// Get are included; they may only be rearranged. If they are not, 
		/// there may (and probably will) be unexpected results.</para>
		/// </summary>
		public LinkedList<Particle> ActiveParticles
		{
			get
			{
				if (mcActiveParticlesList != null)
				{
					return mcActiveParticlesList;
				}
				throw new NullReferenceException("The ActiveParticles property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
			protected set
			{
				mcActiveParticlesList = value;
			}
		}

		/// <summary>
		/// Returns a Linked List whose Nodes point to the Inactive Particles
		/// </summary>
		public LinkedList<Particle> InactiveParticles
		{
			get
			{
				if (mcInactiveParticlesList != null)
				{
					return mcInactiveParticlesList;
				}
				throw new NullReferenceException("The InactiveParticles property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
		}

		/// <summary>
		/// Returns the array of all Particle objects
		/// </summary>
		public Particle[] Particles
		{
			get
			{
				if (mcParticles != null)
				{
					return mcParticles;
				}
				throw new NullReferenceException("The Particles property is trying to be accessed, but is null. Be sure you have Initialized the particle system.");
			}
		}

		/// <summary>
		/// Raised when the UpdateOrder property changes
		/// </summary>
		public event EventHandler<EventArgs> UpdateOrderChanged;

		/// <summary>
		/// Raised when the DrawOrder property changes
		/// </summary>
		public event EventHandler<EventArgs> DrawOrderChanged;

		/// <summary>
		/// Raised when the Enabled property changes
		/// </summary>
		public event EventHandler<EventArgs> EnabledChanged;

		/// <summary>
		/// Raised when the Visible property changes
		/// </summary>
		public event EventHandler<EventArgs> VisibleChanged;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSF(Game cGame)
		{
			mcGame = cGame;
			miID = _totalNumberOfParticleSystemsCreated++;
		}

		/// <summary>
		/// Initializes a new No Display Particle System. This type of Particle System does not allow any of the Particles
		/// to be drawn to a Graphics Device (e.g. the screen).
		/// </summary>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		public void InitializeNoDisplayParticleSystem(int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow)
		{
			try
			{
				InitializeCommonVariables(null, null, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, ParticleTypes.NoDisplay);
			}
			catch (Exception ex)
			{
				ParticleType = ParticleTypes.None;
				throw new Exception("A problem occurred while Initializing the Particle System. Inner Exception: " + ex.ToString(), ex);
			}
			AfterInitialize();
		}

		/// <summary>
		/// Initializes a new Sprite Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="sTexture">The asset name of the Texture to use to visualize the Particles</param>
		public void InitializeSpriteParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, string sTexture)
		{
			InitializeSpriteParticleSystem(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, sTexture, null);
		}

		/// <summary>
		/// Initializes a new Sprite Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="sTexture">The asset name of the Texture to use to visualize the Particles</param>
		/// <param name="cSpriteBatchToDrawWith">The Sprite Batch that this particle system should use to draw its
		/// particles with.
		/// <para>If null, the particle system will use its own SpriteBatch to draw its particles.</para>
		/// <para>If not null, then you must call SpriteBatch.Begin() before calling ParticleSystem.Draw() to
		/// draw the particle system, and then call SpriteBatch.End() when done drawing the particle system.</para></param>
		public void InitializeSpriteParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, string sTexture, SpriteBatch cSpriteBatchToDrawWith)
		{
			try
			{
				InitializeCommonVariables(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, ParticleTypes.Sprite);
				SetTexture(sTexture);
				if (cSpriteBatchToDrawWith != null)
				{
					mcSpriteBatch = cSpriteBatchToDrawWith;
					mcSpriteBatchSettings = null;
					UsingExternalSpriteBatchToDrawParticles = true;
				}
			}
			catch (Exception ex)
			{
				ParticleType = ParticleTypes.None;
				throw new Exception("A problem occurred while Initializing the Particle System. Inner Exception: " + ex.ToString(), ex);
			}
			AfterInitialize();
		}

		/// <summary>
		/// Initializes a new Sprite Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="cTexture">The Texture to use to visualize the Particles</param>
		public void InitializeSpriteParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, Texture2D cTexture)
		{
			InitializeSpriteParticleSystem(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, cTexture, null);
		}

		/// <summary>
		/// Initializes a new Sprite Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="cTexture">The Texture to use to visualize the Particles</param>
		/// <param name="cSpriteBatchToDrawWith">The Sprite Batch that this particle system should use to draw its
		/// particles with.
		/// <para>If null, the particle system will use its own SpriteBatch to draw its particles.</para>
		/// <para>If not null, then you must call SpriteBatch.Begin() before calling ParticleSystem.Draw() to
		/// draw the particle system, and then call SpriteBatch.End() when done drawing the particle system.</para></param>
		public void InitializeSpriteParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, Texture2D cTexture, SpriteBatch cSpriteBatchToDrawWith)
		{
			try
			{
				InitializeCommonVariables(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, ParticleTypes.Sprite);
				Texture = cTexture;
				if (cSpriteBatchToDrawWith != null)
				{
					mcSpriteBatch = cSpriteBatchToDrawWith;
					mcSpriteBatchSettings = null;
					UsingExternalSpriteBatchToDrawParticles = true;
				}
			}
			catch (Exception ex)
			{
				ParticleType = ParticleTypes.None;
				throw new Exception("A problem occurred while Initializing the Particle System. Inner Exception: " + ex.ToString(), ex);
			}
			AfterInitialize();
		}

		/// <summary>
		/// Initializes a new Quad Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="cVertexUpdateFunction">Function used to copy a Particle's drawable properties into the vertex buffer</param>
		public void InitializeQuadParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, UpdateVertexDelegate cVertexUpdateFunction)
		{
			try
			{
				InitializeCommonVariables(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, ParticleTypes.Quad);
				VertexUpdateFunction = cVertexUpdateFunction;
				Texture = null;
			}
			catch (Exception ex)
			{
				ParticleType = ParticleTypes.None;
				throw new Exception("A problem occurred while Initializing the Particle System. Inner Exception: " + ex.ToString(), ex);
			}
			AfterInitialize();
		}

		/// <summary>
		/// Initializes a new Textured Quad Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="cVertexUpdateFunction">Function used to copy a Particle's drawable properties into the vertex buffer</param>
		/// <param name="sTexture">The asset name of the Texture to use to visualize the Particles</param>
		public void InitializeTexturedQuadParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, UpdateVertexDelegate cVertexUpdateFunction, string sTexture)
		{
			try
			{
				InitializeCommonVariables(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, ParticleTypes.TexturedQuad);
				VertexUpdateFunction = cVertexUpdateFunction;
				SetTexture(sTexture);
			}
			catch (Exception ex)
			{
				ParticleType = ParticleTypes.None;
				throw new Exception("A problem occurred while Initializing the Particle System. Inner Exception: " + ex.ToString(), ex);
			}
			AfterInitialize();
		}

		/// <summary>
		/// Initializes a new Textured Quad Particle System
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager used to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. If the Auto Memory Manager is enabled (default), this will be dynamically adjusted at
		/// run-time to make sure there is always roughly as much Memory Allocated as there are Particles. This value
		/// may also be adjusted manually at run-time.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. If the Auto Memory Manager will not be enabled to increase
		/// memory, this should be less than or equal to the Number Of Particles To Allocate Memory For, 
		/// as the Particle System can only handle as many Particles as it has Memory Allocated For. Also, the
		/// Auto Memory Manager will never increase the Allocated Memory to handle more Particles than this value. 
		/// If this is set to a value lower than the Number Of Particles To Allocate Memory For, then only this many
		/// Particles will be allowed, even though there is memory allocated for more Particles. This value 
		/// may also be adjusted manually at run-time.</param>
		/// <param name="cVertexUpdateFunction">Function used to copy a Particle's drawable properties into the vertex buffer</param>
		/// <param name="cTexture">The Texture to use to visualize the Particles</param>
		public void InitializeTexturedQuadParticleSystem(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, UpdateVertexDelegate cVertexUpdateFunction, Texture2D cTexture)
		{
			try
			{
				InitializeCommonVariables(cGraphicsDevice, cContentManager, iNumberOfParticlesToAllocateMemoryFor, iMaxNumberOfParticlesToAllow, ParticleTypes.TexturedQuad);
				VertexUpdateFunction = cVertexUpdateFunction;
				Texture = cTexture;
			}
			catch (Exception ex)
			{
				ParticleType = ParticleTypes.None;
				throw new Exception("A problem occurred while Initializing the Particle System. Inner Exception: " + ex.ToString(), ex);
			}
			AfterInitialize();
		}

		/// <summary>
		/// Initialize the variables common to all Particle Systems
		/// </summary>
		/// <param name="cGraphicsDevice">Graphics Device to draw to</param>
		/// <param name="cContentManager">Content Manager to use to load Effect files and Textures</param>
		/// <param name="iNumberOfParticlesToAllocateMemoryFor">The Number of Particles memory should
		/// be Allocated for. The Maximum Number Of Particles the Particle System should Allow is also
		/// set to this value initially.</param>
		/// <param name="iMaxNumberOfParticlesToAllow">The Maximum Number of Active Particles that are
		/// Allowed at a single point in time. This should be less than or equal to the Number Of Particles 
		/// To Allocate Memory For if the Auto Memory Manager will not be used, as the Particle System 
		/// can only handle as many Particles as it has Memory Allocated For.</param>
		/// <param name="eParticleType">The Type of Particles this Particle System should draw</param>
		private void InitializeCommonVariables(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, int iNumberOfParticlesToAllocateMemoryFor, int iMaxNumberOfParticlesToAllow, ParticleTypes eParticleType)
		{
			Destroy();
			_numberOfParticleSystemsCurrentlyInitialized++;
			ParticleType = eParticleType;
			GraphicsDevice = cGraphicsDevice;
			ContentManager = cContentManager;
			if (SmcDPSFEffect == null && GraphicsDevice != null)
			{
				SmcDPSFEffect = new DPSFDefaultEffect(GraphicsDevice, DPSFDefaultEffect.DPSFDefaultEffectConfigurations.Xbox360HiDef);
			}
			SetDefaultEffect();
			Vertex val = default;
			SetVertexDeclaration(val.SizeInBytes, val.VertexElements);
			miVertexSizeInBytes = val.SizeInBytes;
			if (ParticleType == ParticleTypes.Sprite)
			{
				mcSpriteBatch = new SpriteBatch(GraphicsDevice);
				mcSpriteBatchSettings = new SpriteBatchSettings();
			}
			mcRenderProperties = new RenderProperties();
			InitializeRenderProperties();
			NumberOfParticlesAllocatedInMemory = iNumberOfParticlesToAllocateMemoryFor;
			MaxNumberOfParticlesAllowed = iMaxNumberOfParticlesToAllow;
			Visible = true;
			Enabled = true;
			SimulationSpeed = 1f;
			InternalSimulationSpeed = 1f;
			UpdatesPerSecond = DPSFDefaultSettings.UpdatesPerSecond;
			mcRandom = new RandomNumbers();
			mcParticleEvents = new CParticleEvents();
			mcParticleSystemEvents = new CParticleSystemEvents();
			mcAutoMemoryManagerSettings = new AutoMemoryManagerSettings(DPSFDefaultSettings.AutoMemoryManagementSettings);
			PerformanceProfilingIsEnabled = DPSFDefaultSettings.PerformanceProfilingIsEnabled;
			mcEmitter = new ParticleEmitter();
			_lerpEmittersPositionAndOrientation = true;
			_emittersPreviousPosition = Vector3.Zero;
			_emittersPreviousOrientation = Quaternion.Identity;
			LerpEmittersPositionAndOrientationOnNextUpdate = false;
			ParticleSystemManagerToCopyPropertiesFrom = ParticleSystemManagerToCopyPropertiesFrom;
		}

		/// <summary>
		/// Release all resources used by the Particle System and reset all properties to their default values
		/// </summary>
		public void Destroy()
		{
			BeforeDestroy();
			if (meParticleType != ParticleTypes.None)
			{
				_numberOfParticleSystemsCurrentlyInitialized--;
			}
			meParticleType = ParticleTypes.None;
			mcGraphicsDevice = null;
			mcContentManager = null;
			mcParticleInitializationFunction = null;
			mcVertexUpdateFunction = null;
			mcRenderProperties = null;
			mcParticles = null;
			miNumberOfParticlesToDraw = 0;
			mcParticleVerticesToDraw = null;
			miaIndexBufferArray = null;
			msaIndexBufferReachArray = null;
			miIndexBufferIndex = 0;
			mcSpriteBatch = null;
			mcParticleSpritesToDraw = null;
			mcSpriteBatchSettings = null;
			if (mcActiveParticlesList != null)
			{
				mcActiveParticlesList.Clear();
			}
			mcActiveParticlesList = null;
			if (mcInactiveParticlesList != null)
			{
				mcInactiveParticlesList.Clear();
			}
			mcInactiveParticlesList = null;
			miMaxNumberOfParticlesAllowed = 0;
			mcVertexDeclaration = null;
			miVertexSizeInBytes = 0;
			mcEffect = null;
			mcTexture = null;
			DeserializationTexturePath = null;
			DeserializationEffectPath = null;
			DeserializationTechniqueName = null;
			Visible = false;
			Enabled = false;
			SimulationSpeed = 1f;
			InternalSimulationSpeed = 1f;
			mfTimeToWaitBetweenUpdates = 0f;
			mfTimeElapsedSinceLastUpdate = 0f;
			World = Matrix.Identity;
			View = Matrix.Identity;
			Projection = Matrix.Identity;
			mcRandom = null;
			mcParticleEvents = null;
			mcParticleSystemEvents = null;
			mcAutoMemoryManagerSettings = null;
			mcEmitter = null;
			_lerpEmittersPositionAndOrientation = true;
			_emittersPreviousPosition = Vector3.Zero;
			_emittersPreviousOrientation = Quaternion.Identity;
			LerpEmittersPositionAndOrientationOnNextUpdate = false;
			AfterDestroy();
		}

		public void InitializeNonSerializableProperties(Game cGame, GraphicsDevice cGraphicsDevice, ContentManager cContentManager)
		{
			miID = _totalNumberOfParticleSystemsCreated++;
			mcGame = cGame;
			GraphicsDevice = cGraphicsDevice;
			ContentManager = cContentManager;
			VertexElement = default(Vertex).VertexElements;
			mcSpriteBatch = null;
			if (meParticleType == ParticleTypes.Sprite)
			{
				mcSpriteBatch = new SpriteBatch(GraphicsDevice);
			}
			if (meParticleType != ParticleTypes.NoDisplay && meParticleType != ParticleTypes.None)
			{
				if (!string.IsNullOrEmpty(DeserializationEffectPath) && !string.IsNullOrEmpty(DeserializationTechniqueName))
				{
					SetEffectAndTechnique(DeserializationEffectPath, DeserializationTechniqueName);
				}
				else
				{
					SetDefaultEffect();
				}
			}
			if (meParticleType == ParticleTypes.Sprite || meParticleType == ParticleTypes.TexturedQuad)
			{
				if (string.IsNullOrEmpty(DeserializationTexturePath))
				{
					throw new ArgumentNullException("DeserializationTexturePath", "The specified Texture to use is null. A valid Texture must be set to draw the current Type of Particles.");
				}
				SetTexture(DeserializationTexturePath);
			}
		}

		/// <summary>
		/// Sets the Graphics Device to use to the given graphics device.
		/// <para>NOTE: This only has an effect if the particle system does not inherit from DrawableGameComponent
		/// (i.e. InheritsDrawableGameComponent == false. i.e. using the DPSF.dll, not DPSFAsDrawableGameComponent.dll), since 
		/// the Graphics Device is read-only when inheriting from DrawableGameComponent. The Game object's Graphics Device
		/// is always used when inheriting from DrawableGameComponent.</para>
		/// </summary>
		/// <param name="graphicsDevice">The graphics device to use.</param>
		public void SetGraphicsDevice(GraphicsDevice graphicsDevice)
		{
			GraphicsDevice = graphicsDevice;
		}

		/// <summary>
		/// Set the World, View, and Projection matrices for this Particle System.
		/// <para>NOTE: Sprite particle systems are not affected by the World, View, and Projection matrices.</para>
		/// </summary>
		/// <param name="cWorld">The World matrix</param>
		/// <param name="cView">The View matrix</param>
		/// <param name="cProjection">The Projection matrix</param>
		public void SetWorldViewProjectionMatrices(Matrix cWorld, Matrix cView, Matrix cProjection)
		{
			World = cWorld;
			View = cView;
			Projection = cProjection;
		}

		/// <summary>
		/// Sets the vertex elements to use for each vertex of a particle.
		/// </summary>
		/// <param name="numberOfBytesPerVertex">The number of bytes per vertex.</param>
		/// <param name="elements">The vertex elements that make up the vertex.</param>
		private void SetVertexDeclaration(int numberOfBytesPerVertex, VertexElement[] elements)
		{
			if (numberOfBytesPerVertex > 0 && elements != null)
			{
				mcVertexDeclaration = new VertexDeclaration(numberOfBytesPerVertex, elements);
				return;
			}
			if (ParticleType == ParticleTypes.None || ParticleType == ParticleTypes.NoDisplay || ParticleType == ParticleTypes.Sprite)
			{
				mcVertexDeclaration = null;
				return;
			}
			throw new ArgumentNullException("VertexElement", "The specified Vertex Element is null. A valid Vertex Element is required to draw the current Type of Particles.");
		}

		/// <summary>
		/// Sets the Effect to be the default type for this type of particle system.
		/// This is done automatically when the particle system is initialized.
		/// <para>Default effect for each particle type is:</para>
		/// <list type="number">
		///     <item><description>NoDisplay and Sprite - null.</description></item>
		///     <item><description>Quad - BasicEffect.</description></item>
		///     <item><description>TexturedQuad - AlphaTestEffect.</description></item>
		/// </list>
		/// </summary>
		public void SetDefaultEffect()
		{
			switch (meParticleType)
			{
			case ParticleTypes.Quad:
				Effect = new BasicEffect(GraphicsDevice);
				break;
			case ParticleTypes.TexturedQuad:
				Effect = new AlphaTestEffect(GraphicsDevice);
				break;
			}
		}

		/// <summary>
		/// Sets the Effect and Technique to use to draw the Particles.
		/// <para>NOTE: This will automatically set the DeserializationEffectPath property to the given sEffect.</para>
		/// <para>NOTE: This will automatically set the DeserializationTechniqueName property to the given sTechnique.</para>
		/// </summary>
		/// <param name="sEffect">The Asset Name of the Effect to use</param>
		/// <param name="sTechnique">The name of the Effect's Technique to use</param>
		public void SetEffectAndTechnique(string sEffect, string sTechnique)
		{
			if (string.IsNullOrEmpty(sEffect))
			{
				throw new ArgumentNullException("sEffect", "The Effect string supplied is null or an empty string. The Effect to use cannot be null.");
			}
			Effect cEffect = ContentManager.Load<Effect>(sEffect);
			DeserializationEffectPath = sEffect;
			SetEffectAndTechnique(cEffect, sTechnique);
		}

		/// <summary>
		/// Sets the Effect and Technique to use to draw the Particles.
		/// <para>NOTE: This will automatically set the DeserializationTechniqueName property to the given sTechnique.</para>
		/// </summary>
		/// <param name="cEffect">The Effect to use</param>
		/// <param name="sTechnique">The name of the Effect's Technique to use</param>
		public void SetEffectAndTechnique(Effect cEffect, string sTechnique)
		{
			Effect = cEffect;
			SetTechnique(sTechnique);
		}

		/// <summary>
		/// Set which Technique of the current Effect to use to draw the Particles.
		/// <para>NOTE: This will automatically set the DeserializationTechniqueName property to the given sTechnique.</para>
		/// </summary>
		/// <param name="sTechnique">The name of the Effect's Technique to use</param>
		public void SetTechnique(string sTechnique)
		{
			if (mcEffect == null)
			{
				throw new InvalidOperationException("Effect is null when trying to specify the Technique to use. The Effect must be set before specifying the Technique.");
			}
			if (string.IsNullOrEmpty(sTechnique))
			{
				throw new ArgumentNullException("sTechnique", "The specified Technique to use is invalid. This parameter cannot be null or an empty string.");
			}
			mcEffect.CurrentTechnique = mcEffect.Techniques[sTechnique];
			DeserializationTechniqueName = sTechnique;
		}

		/// <summary>
		/// Set the Texture to use to draw the Particles
		/// </summary>
		/// <param name="sTexture">The Asset Name of the texture file to use (found in
		/// the XNA Properties of the file)</param>
		public void SetTexture(string sTexture)
		{
			if (string.IsNullOrEmpty(sTexture))
			{
				if (ParticleType == ParticleTypes.Sprite || ParticleType == ParticleTypes.TexturedQuad)
				{
					throw new ArgumentNullException("sTexture", "Specified Texture to use is null. A valid Texture must be set to draw the current Type of Particles.");
				}
				Texture = null;
				DeserializationTexturePath = null;
				return;
			}
			try
			{
				mcTexture = ContentManager.Load<Texture2D>(sTexture);
				mcTexture.Name = sTexture;
				DeserializationTexturePath = sTexture;
			}
			catch (KeyNotFoundException innerException)
			{
				throw new InvalidOperationException("There was a problem loading the texture \"" + sTexture + "\". Did you Dispose() this resource earlier somewhere else by accident?", innerException);
			}
		}

		/// <summary>
		/// This allocates the proper amount of space for the Particles and initializes the variables used to draw the Type of Particles specified. 
		/// For example, if using Textured Quads extra space will need to be allocated to hold the Particles, as each Quad Particle requires four 
		/// vertices, not one like Point Sprites. Also, the Index Buffer would be initialized, as it is required to draw Quads.
		/// </summary>
		private bool InitializeParticleArrays()
		{
			if (mcParticles == null)
			{
				return false;
			}
			int num = mcParticles.Length;
			switch (meParticleType)
			{
			case ParticleTypes.Quad:
			case ParticleTypes.TexturedQuad:
				mcParticleVerticesToDraw = new Vertex[num * 4];
				if (GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef)
				{
					miaIndexBufferArray = new int[num * 6];
				}
				else
				{
					msaIndexBufferReachArray = new short[num * 6];
				}
				miMaxParticlesThatXboxCanDrawAtOnce = 524287 / (miVertexSizeInBytes * 4);
				break;
			case ParticleTypes.Sprite:
				mcParticleSpritesToDraw = new Particle[num];
				break;
			}
			int num2 = mcParticles.Length;
			for (int i = 0; i < num2; i++)
			{
				mcParticles[i] = new Particle();
				mcInactiveParticlesList.AddFirst(mcParticles[i]);
			}
			if (mcParticleVerticesToDraw != null)
			{
				int num3 = mcParticleVerticesToDraw.Length;
				for (int j = 0; j < num3; j++)
				{
					mcParticleVerticesToDraw[j] = default;
				}
			}
			miNumberOfParticlesToDraw = 0;
			return true;
		}

		/// <summary>
		/// Initialize the given Particle using the current Initialization Function
		/// </summary>
		/// <param name="cParticle">The Particle to Initialize</param>
		public void InitializeParticle(Particle cParticle)
		{
			mcParticleInitializationFunction(cParticle);
		}

		/// <summary>
		/// Adds a new Particle to the particle system, at the start of the Active Particle List. 
		/// This new Particle is initialized using the particle system's Particle Initialization Function
		/// </summary>
		/// <returns>True if a particle was added, False if there is not enough memory for another Particle</returns>
		public bool AddParticle()
		{
			return AddParticle(null);
		}

		/// <summary>
		/// Adds a new Particle to the particle system, at the start of the Active Particle List. Returns true if
		/// the Particle was added, false if there is not enough memory for another Particle.
		/// </summary>
		/// <param name="cParticleToCopy">The Particle to add to the Particle System. If this is null then a
		/// new Particle is initialized using the particle system's Particle Initialization Function</param>
		/// <returns>True if a particle was added, False if there is not enough memory for another Particle</returns>
		public bool AddParticle(Particle cParticleToCopy)
		{
			if (mcActiveParticlesList.Count >= miMaxNumberOfParticlesAllowed)
			{
				return false;
			}
			if (mcInactiveParticlesList.Count <= 0)
			{
				if (AutoMemoryManagerSettings.MemoryManagementMode != AutoMemoryManagerModes.IncreaseAndDecrease && AutoMemoryManagerSettings.MemoryManagementMode != AutoMemoryManagerModes.IncreaseOnly)
				{
					return false;
				}
				int num = (int)Math.Ceiling((float)NumberOfParticlesAllocatedInMemory * AutoMemoryManagerSettings.IncreaseAmount);
				num = (int)MathHelper.Min(num, MaxNumberOfParticlesAllowed);
				NumberOfParticlesAllocatedInMemory = num;
			}
			BeforeAddParticle();
			LinkedListNode<Particle> last = mcInactiveParticlesList.Last;
			mcInactiveParticlesList.RemoveLast();
			Particle value = last.Value;
			value.Reset();
			if (cParticleToCopy == null)
			{
				if (mcParticleInitializationFunction != null)
				{
					mcParticleInitializationFunction(value);
				}
			}
			else
			{
				value.CopyFrom(cParticleToCopy);
			}
			mcActiveParticlesList.AddFirst(last);
			AfterAddParticle();
			return true;
		}

		/// <summary>
		/// Adds the specified number of new Particles to the particle system. 
		/// These new Particles are initialized using the particle systems Particle Initialization Function
		/// </summary>
		/// <param name="iNumberOfParticlesToAdd">How many Particles to Add to the particle system</param>
		/// <returns>Returns how many Particles were able to be added to the particle system</returns>
		public int AddParticles(int iNumberOfParticlesToAdd)
		{
			return AddParticles(iNumberOfParticlesToAdd, null);
		}

		/// <summary>
		/// Adds the specified number of new Particles to the particle system, copying the 
		/// properties of the given Particle To Copy
		/// </summary>
		/// <param name="iNumberOfParticlesToAdd">How many copyies of the Particle To Copy to Add 
		/// to the particle system</param>
		/// <param name="cParticleToCopy">The Particle to copy from when Adding the Particles to the 
		/// Particle System. If this is null then the new Particles will be initialized using the 
		/// particle system's Particle Initialization Function</param>
		/// <returns>Returns how many Particles were able to be added to the particle system</returns>
		public int AddParticles(int iNumberOfParticlesToAdd, Particle cParticleToCopy)
		{
			int i;
			for (i = 0; i < iNumberOfParticlesToAdd; i++)
			{
				if (!AddParticle(cParticleToCopy))
				{
					break;
				}
			}
			return i;
		}

		/// <summary>
		/// Removes all Active Particles from the Active Particle List and adds them 
		/// to the Inactive Particle List
		/// </summary>
		public void RemoveAllParticles()
		{
			while (mcActiveParticlesList.Count > 0)
			{
				LinkedListNode<Particle> first = mcActiveParticlesList.First;
				mcActiveParticlesList.Remove(first);
				mcInactiveParticlesList.AddFirst(first);
			}
		}

		/// <summary>
		/// Updates the Particle System. This involves executing the Particle System
		/// Events, updating all Active Particles according to the Particle Events, and 
		/// adding new Particles according to the Emitter settings.
		/// <para>NOTE: This will only Update the Particle System if it does not inherit from DrawableGameComponent, 
		/// since if it does it will be updated automatically by the Game object.</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has 
		/// elapsed since the last time this function was called</param>
		public void Update(float fElapsedTimeInSeconds)
		{
			Update(fElapsedTimeInSeconds, bCalledByDrawableGameComponent: false);
		}

		/// <summary>
		/// Updates the Particle System, even if the the Particle Systems inherits from DrawableGameComponent.
		/// <para>Updating the Particle System involves executing the Particle System Events, updating all Active 
		/// Particles according to the Particle Events, and adding new Particles according to the Emitter settings.</para>
		/// <para>NOTE: If inheriting from DrawableGameComponent and this is called, the Particle System will be updated
		/// twice per frame; once when it is called here, and again when automatically called by the Game object.
		/// If not inheriting from DrawableGameComponent, this acts the same as calling Update().</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has 
		/// elapsed since the last time this function was called</param>
		public void UpdateForced(float fElapsedTimeInSeconds)
		{
			Update(fElapsedTimeInSeconds, bCalledByDrawableGameComponent: true);
		}

		/// <summary>
		/// Updates the Particle System. This involves executing the Particle System
		/// Events, updating all Active Particles according to the Particle Events, and 
		/// adding new Particles according to the Emitter's settings.
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much time in seconds has 
		/// elapsed since the last time this function was called</param>
		/// <param name="bCalledByDrawableGameComponent">Indicates if this function was
		/// called manually by the user or called automatically by the Drawable Game Component.
		/// If this function Inherits Drawable Game Component, but was not called by
		/// Drawable Game Component, nothing will be updated since the Particle System will
		/// automatically be updated when the Game Component's Update() function is called.</param>
		private void Update(float fElapsedTimeInSeconds, bool bCalledByDrawableGameComponent)
		{
			if (!IsInitialized || !Enabled || (InheritsDrawableGameComponent && !bCalledByDrawableGameComponent))
			{
				return;
			}
			mfTimeElapsedSinceLastUpdate += fElapsedTimeInSeconds;
			if (mfTimeElapsedSinceLastUpdate < mfTimeToWaitBetweenUpdates)
			{
				return;
			}
			if (_performanceProfilingIsEnabled)
			{
				_performanceProfilingStopwatch.Reset();
				_performanceProfilingStopwatch.Start();
			}
			mfTimeElapsedSinceLastUpdate -= mfTimeToWaitBetweenUpdates;
			float num = 0f;
			if (mfTimeToWaitBetweenUpdates <= 0f)
			{
				num = fElapsedTimeInSeconds;
			}
			else if (mfTimeElapsedSinceLastUpdate >= mfTimeToWaitBetweenUpdates)
			{
				num = mfTimeElapsedSinceLastUpdate + mfTimeToWaitBetweenUpdates;
				mfTimeElapsedSinceLastUpdate = 0f;
			}
			else
			{
				num = mfTimeToWaitBetweenUpdates;
			}
			float num2 = num * SimulationSpeed * InternalSimulationSpeed;
			BeforeUpdate(num2);
			miNumberOfParticlesToDraw = 0;
			miIndexBufferIndex = 0;
			int num3 = 0;
			ParticleSystemEvents.Update(num2);
			if (!IsInitialized)
			{
				return;
			}
			int num4 = mcEmitter.UpdateAndGetNumberOfParticlesToEmit(num2);
			if (!IsInitialized)
			{
				return;
			}
			if (!LerpEmittersPositionAndOrientationOnNextUpdate)
			{
				_emittersPreviousPosition = mcEmitter.PositionData.Position;
				_emittersPreviousOrientation = mcEmitter.OrientationData.Orientation;
				LerpEmittersPositionAndOrientationOnNextUpdate = true;
			}
			if (num4 > 0)
			{
				Vector3 position = mcEmitter.PositionData.Position;
				Quaternion orientation = mcEmitter.OrientationData.Orientation;
				if (!LerpEmittersPositionAndOrientation)
				{
					_emittersPreviousPosition = position;
					_emittersPreviousOrientation = orientation;
				}
				float num5 = 1f / (float)num4;
				int num6 = 0;
				bool flag = true;
				while (num6 < num4 && flag)
				{
					num6++;
					mcEmitter.PositionData.Position = Vector3.Lerp(_emittersPreviousPosition, position, num5 * (float)num6);
					mcEmitter.OrientationData.Orientation = Quaternion.Slerp(_emittersPreviousOrientation, orientation, num5 * (float)num6);
					flag = AddParticle();
					if (flag)
					{
						Particle value = ActiveParticles.First.Value;
						float fElapsedTimeInSeconds2 = MathHelper.Lerp(num2, 0f, num5 * (float)num6);
						value.UpdateElapsedTimeVariables(fElapsedTimeInSeconds2);
						ParticleEvents.Update(value, fElapsedTimeInSeconds2);
						if (!value.IsActive())
						{
							ActiveParticles.RemoveFirst();
							continue;
						}
						AddParticleToVertexBuffer(value);
						num3++;
					}
				}
				mcEmitter.PositionData.Position = position;
				mcEmitter.OrientationData.Orientation = orientation;
			}
			_emittersPreviousPosition = mcEmitter.PositionData.Position;
			_emittersPreviousOrientation = mcEmitter.OrientationData.Orientation;
			LinkedListNode<Particle> linkedListNode = mcActiveParticlesList.First;
			for (int i = 0; i < num3; i++)
			{
				linkedListNode = linkedListNode.Next;
			}
			while (linkedListNode != null)
			{
				Particle value2 = linkedListNode.Value;
				value2.UpdateElapsedTimeVariables(num2);
				ParticleEvents.Update(value2, num2);
				if (!value2.IsActive())
				{
					LinkedListNode<Particle> node = linkedListNode;
					linkedListNode = linkedListNode.Next;
					mcActiveParticlesList.Remove(node);
					mcInactiveParticlesList.AddFirst(node);
				}
				else
				{
					AddParticleToVertexBuffer(value2);
					linkedListNode = linkedListNode.Next;
				}
			}
			ParticleEvents.RemoveAllOneTimeEvents();
			if (AutoMemoryManagerSettings.MemoryManagementMode == AutoMemoryManagerModes.IncreaseAndDecrease || AutoMemoryManagerSettings.MemoryManagementMode == AutoMemoryManagerModes.DecreaseOnly)
			{
				mfAutoMemoryManagersElapsedTime += fElapsedTimeInSeconds;
				if (NumberOfActiveParticles > miAutoMemoryManagerMaxNumberOfParticlesActiveAtOnceOverTheLastXSeconds)
				{
					miAutoMemoryManagerMaxNumberOfParticlesActiveAtOnceOverTheLastXSeconds = NumberOfActiveParticles;
					mfAutoMemoryManagersElapsedTime = 0f;
				}
				if (mfAutoMemoryManagersElapsedTime >= AutoMemoryManagerSettings.SecondsMaxNumberOfParticlesMustExistForBeforeReducingSize)
				{
					int num7 = (int)Math.Ceiling((float)miAutoMemoryManagerMaxNumberOfParticlesActiveAtOnceOverTheLastXSeconds * AutoMemoryManagerSettings.ReduceAmount);
					num7 = (int)MathHelper.Max(num7, AutoMemoryManagerSettings.AbsoluteMinNumberOfParticles);
					if (num7 < NumberOfParticlesAllocatedInMemory)
					{
						NumberOfParticlesAllocatedInMemory = num7;
					}
					miAutoMemoryManagerMaxNumberOfParticlesActiveAtOnceOverTheLastXSeconds = 0;
				}
			}
			if ((double)ParticleSystemEvents.LifetimeData.NormalizedElapsedTime >= 1.0 && ParticleSystemEvents.LifetimeData.EndOfLifeOption == CParticleSystemEvents.EParticleSystemEndOfLifeOptions.Destroy)
			{
				Destroy();
			}
			AfterUpdate(num2);
			if (_performanceProfilingIsEnabled)
			{
				_performanceProfilingStopwatch.Stop();
				PerformanceTimeToDoUpdateInMilliseconds = _performanceProfilingStopwatch.Elapsed.TotalMilliseconds;
			}
		}

		/// <summary>
		/// Adds the given Particle to the list of Particles to be Drawn (i.e. the Vertex Buffer), if it is Visible
		/// </summary>
		/// <param name="cParticle">The Particle to add to the Vertex Buffer</param>
		private void AddParticleToVertexBuffer(Particle cParticle)
		{
			if (cParticle.Visible)
			{
				switch (meParticleType)
				{
				case ParticleTypes.Quad:
				case ParticleTypes.TexturedQuad:
					mcVertexUpdateFunction(ref mcParticleVerticesToDraw, miNumberOfParticlesToDraw * 4, cParticle);
					break;
				case ParticleTypes.Sprite:
					mcParticleSpritesToDraw[miNumberOfParticlesToDraw] = cParticle;
					break;
				}
				miNumberOfParticlesToDraw++;
			}
		}

		/// <summary>
		/// Draws all of the Active Particles to the Graphics Device.
		/// <para>NOTE: This will only Draw the Particle System if it does not inherit from DrawableGameComponent, 
		/// since if it does it will be drawn automatically by the Game object.</para>
		/// </summary>
		public void Draw()
		{
			Draw(bCalledByDrawableGameComponent: false);
		}

		/// <summary>
		/// Draws all of the Active Particles to the Graphics Device, even if the the Particle Systems inherits
		/// from DrawableGameComponent.
		/// <para>NOTE: If inheriting from DrawableGameComponent and this is called, the Particle System will be drawn
		/// twice per frame; once when it is called here, and again when automatically called by the Game object.
		/// If not inheriting from DrawableGameComponent, this acts the same as calling Draw().</para>
		/// </summary>
		public void DrawForced()
		{
			Draw(bCalledByDrawableGameComponent: true);
		}

		/// <summary>
		/// Draws all of the Active Particles to the Graphics Device
		/// </summary>
		/// <param name="bCalledByDrawableGameComponent">Indicates if this function was
		/// called manually by the user or called automatically by the Drawable Game Component.
		/// If this function Inherits Drawable Game Component, but was not called by
		/// Drawable Game Component, nothing will be drawn since the Particle System will
		/// automatically be drawn when the Game Component's Draw() function is called.</param>
		private void Draw(bool bCalledByDrawableGameComponent)
		{
			if (!IsInitialized || !Visible || (InheritsDrawableGameComponent && !bCalledByDrawableGameComponent))
			{
				return;
			}
			if (_performanceProfilingIsEnabled)
			{
				_performanceProfilingStopwatch.Reset();
				_performanceProfilingStopwatch.Start();
			}
			ClearRenderStates();
			BeforeDraw();
			if (miNumberOfParticlesToDraw <= 0 || ParticleType == ParticleTypes.NoDisplay)
			{
				AfterDraw();
				if (_performanceProfilingIsEnabled)
				{
					_performanceProfilingStopwatch.Stop();
					PerformanceTimeToDoDrawInMilliseconds = _performanceProfilingStopwatch.Elapsed.TotalMilliseconds;
				}
				return;
			}
			if (meParticleType == ParticleTypes.Sprite && (Effect == null || Technique == null))
			{
				if (!UsingExternalSpriteBatchToDrawParticles)
				{
					mcSpriteBatch.Begin(SpriteBatchSettings.SortMode, RenderProperties.BlendState, RenderProperties.SamplerState, RenderProperties.DepthStencilState, RenderProperties.RasterizerState, Effect, SpriteBatchSettings.TransformationMatrix);
				}
				for (int i = 0; i < miNumberOfParticlesToDraw; i++)
				{
					DrawSprite(mcParticleSpritesToDraw[i], mcSpriteBatch);
				}
				if (!UsingExternalSpriteBatchToDrawParticles)
				{
					mcSpriteBatch.End();
				}
			}
			else
			{
				ApplyRenderState();
				SetEffectParameters();
				int count = mcEffect.CurrentTechnique.Passes.Count;
				for (int j = 0; j < count; j++)
				{
					EffectPass effectPass = mcEffect.CurrentTechnique.Passes[j];
					if (meParticleType == ParticleTypes.Sprite && !UsingExternalSpriteBatchToDrawParticles)
					{
						mcSpriteBatch.Begin(SpriteBatchSettings.SortMode, RenderProperties.BlendState, RenderProperties.SamplerState, RenderProperties.DepthStencilState, RenderProperties.RasterizerState, Effect, SpriteBatchSettings.TransformationMatrix);
					}
					effectPass.Apply();
					try
					{
						switch (meParticleType)
						{
						case ParticleTypes.Quad:
						case ParticleTypes.TexturedQuad:
						{
							int num = miNumberOfParticlesToDraw;
							int num2 = 0;
							int num3 = 0;
							while (num > 0)
							{
								int num4 = Math.Min(num, miMaxParticlesThatXboxCanDrawAtOnce);
								int primitiveCount = num4 * 2;
								int num5 = num4 * 4;
								GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, mcParticleVerticesToDraw, num2, num5, miaIndexBufferArray, num3, primitiveCount, mcVertexDeclaration);
								num2 += num5;
								num3 += num4 * 6;
								num -= num4;
							}
							break;
						}
						case ParticleTypes.Sprite:
						{
							for (int k = 0; k < miNumberOfParticlesToDraw; k++)
							{
								DrawSprite(mcParticleSpritesToDraw[k], mcSpriteBatch);
							}
							if (!UsingExternalSpriteBatchToDrawParticles)
							{
								mcSpriteBatch.End();
							}
							break;
						}
						case (ParticleTypes)4:
							break;
						}
					}
					catch (InvalidOperationException ex)
					{
						int num6 = miVertexSizeInBytes * miNumberOfParticlesToDraw;
						if (meParticleType == ParticleTypes.Quad || meParticleType == ParticleTypes.TexturedQuad)
						{
							num6 *= 4;
						}
						throw new Exception("Not enough video memory to draw the particle system. The XBox 360 can only allocate about " + 524287.ToString("###,###,###") + " bytes in video memory when using DrawUserPrimitives(). You are trying to allocate " + num6.ToString("###,###,###") + " bytes.\n\nInner exception: " + ex.ToString(), ex);
					}
				}
			}
			AfterDraw();
			if (_performanceProfilingIsEnabled)
			{
				_performanceProfilingStopwatch.Stop();
				PerformanceTimeToDoDrawInMilliseconds = _performanceProfilingStopwatch.Elapsed.TotalMilliseconds;
			}
		}

		/// <summary>
		/// Resets all of the Sampler States and Vertex Sampler States on the Graphics Device.
		/// This must be done before any rendering to prevent an XNA 4 bug that causes the graphics device
		/// to incorrectly track state information, which manifests itself as run-time errors.
		/// https://connect.microsoft.com/site226/feedback/details/586216/cloned-effect-?wa=wsignin1.0
		/// </summary>
		private void ClearRenderStates()
		{
			if (GraphicsDevice == null)
			{
				return;
			}
			for (int i = 0; i < 16; i++)
			{
				GraphicsDevice.SamplerStates[i] = SamplerState.PointClamp;
			}
			if (GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef)
			{
				for (int j = 0; j < 2; j++)
				{
					GraphicsDevice.VertexSamplerStates[j] = SamplerState.PointWrap;
				}
			}
		}

		/// <summary>
		/// Applies the Particle System's Render State properties to the Graphics Device.
		/// </summary>
		private void ApplyRenderState()
		{
			GraphicsDevice.BlendState = RenderProperties.BlendState;
			GraphicsDevice.DepthStencilState = RenderProperties.DepthStencilState;
			GraphicsDevice.RasterizerState = RenderProperties.RasterizerState;
			GraphicsDevice.SamplerStates[0] = RenderProperties.SamplerState;
		}

		/// <summary>
		/// Virtual function to Initialize the Particle System with default values.
		/// Particle system properties should not be set until after this is called, as 
		/// they are likely to be reset to their default values.
		/// </summary>
		/// <param name="cGraphicsDevice">The Graphics Device the Particle System should use</param>
		/// <param name="cContentManager">The Content Manager the Particle System should use to load resources</param>
		/// <param name="cSpriteBatch">The Sprite Batch that the Sprite Particle System should use to draw its particles.
		/// If this is not initializing a Sprite particle system, or you want the particle system to use its own Sprite Batch,
		/// pass in null.</param>
		public virtual void AutoInitialize(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, SpriteBatch cSpriteBatch)
		{
		}

		/// <summary>
		/// Sets the Camera Position of the particle system, so that the particles know how to make themselves face the camera if needed.
		/// This virtual function does not do anything unless overridden, and all it should do is set an internal Vector3 variable
		/// (e.g. public Vector3 CameraPosition { get; set; }) to match the given Vector3.
		/// </summary>
		/// <param name="cameraPosition">The position that the camera is currently at.</param>
		public virtual void SetCameraPosition(Vector3 cameraPosition)
		{
		}

		/// <summary>
		/// Virtual function to draw a Sprite Particle. This function should be used to draw the given
		/// Particle with the provided SpriteBatch.
		/// </summary>
		/// <param name="Particle">The Particle Sprite to Draw</param>
		/// <param name="cSpriteBatch">The SpriteBatch to use to doing the Drawing</param>
		protected virtual void DrawSprite(DPSFParticle Particle, SpriteBatch cSpriteBatch)
		{
		}

		/// <summary>
		/// Virtual function that is called at the end of the Initialize() function.
		/// This may be used to perform operations after the Particle System has been Initialized, such as 
		/// initializing other Particle Systems nested within this Particle System.
		/// </summary>
		protected virtual void AfterInitialize()
		{
		}

		/// <summary>
		/// Virtual function that is called at the beginning of the Destroy() function.
		/// This may be used to perform operations before the Destroy() code is executed.
		/// </summary>
		protected virtual void BeforeDestroy()
		{
		}

		/// <summary>
		/// Virtual function that is called at the end of the Destroy() function.
		/// This may be used to perform operations after the Particle System has been Destroyed, such as 
		/// to destroy other Particle Systems nested within this Particle System.
		/// </summary>
		protected virtual void AfterDestroy()
		{
		}

		/// <summary>
		/// Virtual function that is called at the beginning of the Update() function.
		/// This may be used to perform operations before the Update() code is executed.
		/// </summary>
		protected virtual void BeforeUpdate(float fElapsedTimeInSeconds)
		{
		}

		/// <summary>
		/// Virtual function that is called at the end of the Update() function.
		/// This may be used to perform operations after the Particle System has been updated, such as 
		/// to Update Particle Systems nested within this Particle System.
		/// </summary>
		protected virtual void AfterUpdate(float fElapsedTimeInSeconds)
		{
		}

		/// <summary>
		/// Virtual function that is called at the beginning of the Draw() function.
		/// This may be used to perform operations before the Draw() code is executed.
		/// </summary>
		protected virtual void BeforeDraw()
		{
		}

		/// <summary>
		/// Virtual function that is called at the end of the Draw() function.
		/// This may be used to perform operations after the Particle System has been drawn, such as 
		/// to Draw Particle Systems nested within this Particle System.
		/// </summary>
		protected virtual void AfterDraw()
		{
		}

		/// <summary>
		/// Virtual function that is called at the beginning of the AddParticle() function.
		/// This may be used to execute some code before a new Particle is initialized and added.
		/// </summary>
		protected virtual void BeforeAddParticle()
		{
		}

		/// <summary>
		/// Virtual function that is called at the end of the AddParticle() function.
		/// This may be used to execute some code after a new Particle is initialized and added.
		/// </summary>
		protected virtual void AfterAddParticle()
		{
		}

		/// <summary>
		/// Virtual function to setup the Render Properties (i.e. BlendState, DepthStencilState, RasterizerState, and SamplerState)
		/// which will be applied to the Graphics Device before drawing the Particle System's Particles.
		/// <para>This function is only called once when initializing the particle system.</para>
		/// </summary>
		protected virtual void InitializeRenderProperties()
		{
		}

		/// <summary>
		/// Virtual function to Set the Effect's Parameters before drawing the Particles.
		/// <para>This is called every time before the particle system is drawn.</para>
		/// </summary>
		protected virtual void SetEffectParameters()
		{
		}
	}
	/// <summary>
	/// The Base Particle System class that the Default Particle System classes inherit from
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public abstract class DPSFDefaultBaseParticleSystem<Particle, Vertex> : DPSF<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Particle System Properties used to initialize a Particle's Properties.
		/// <para>NOTE: These are only applied to the Particle when the InitializeParticleUsingInitialProperties()
		/// function is set as the Particle Initialization Function.</para>
		/// </summary>
		public class CInitialProperties
		{
			public float LifetimeMin = 1f;

			public float LifetimeMax = 1f;

			public Vector3 PositionMin = Vector3.Zero;

			public Vector3 PositionMax = Vector3.Zero;

			public Vector3 VelocityMin = Vector3.Zero;

			public Vector3 VelocityMax = Vector3.Zero;

			public Vector3 AccelerationMin = Vector3.Zero;

			public Vector3 AccelerationMax = Vector3.Zero;

			public Vector3 ExternalForceMin = Vector3.Zero;

			public Vector3 ExternalForceMax = Vector3.Zero;

			public float FrictionMin;

			public float FrictionMax;

			public Color StartColorMin = Color.White;

			public Color StartColorMax = Color.White;

			public Color EndColorMin = Color.White;

			public Color EndColorMax = Color.White;

			/// <summary>
			/// If true the Position will be somewhere on the vector joining the Min Position to the Max Position.
			/// <para>If false each of the XYZ components will be randomly calculated individually between the Min and Max Position XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxPosition;

			/// <summary>
			/// If true the Velocity will be somewhere on the vector joining the Min Velocity to the Max Velocity.
			/// <para>If false each of the XYZ components will be randomly calculated individually between the Min and Max Velocity XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxVelocity;

			/// <summary>
			/// If true the Acceleration will be somewhere on the vector joining the Min Acceleration to the Max Acceleration.
			/// <para>If false each of the XYZ components will be randomly calculated individually between the Min and Max Acceleration XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxAcceleration;

			/// <summary>
			/// If true the External Force will be somewhere on the vector joining the Min External Force to the Max External Force.
			/// <para>If false each of the XYZ components will be randomly calculated individually between the Min and Max External Force XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxExternalForce;

			/// <summary>
			/// If true a Lerp'd value between the Min and Max Colors will be randomly chosen.
			/// <para>If false the RGBA component values will be randomly chosen individually between the Min and Max Color RGBA values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxColors;

			/// <summary>
			/// If true the Emitter's Position will be added to the Particle's starting Position. For example, if the Particle is given
			/// an initial position of zero it will be placed wherever the Emitter currently is.
			/// <para>Default value is true.</para>
			/// </summary>
			public bool PositionIsAffectedByEmittersPosition = true;

			/// <summary>
			/// If true the Particle's Velocity direction will be adjusted according to the Emitter's Orientation. For example, if the
			/// Emitter is orientated to face backwards, the Particle's Velocity direction will be reversed.
			/// <para>Default value is true.</para>
			/// </summary>
			public bool VelocityIsAffectedByEmittersOrientation = true;
		}

		private CInitialProperties mcInitialProperties = new CInitialProperties();

		private string msName = "Default";

		/// <summary>
		/// A list of Magnets that should affect this Particle System's Particles.
		/// <para>NOTE: You must add a UpdateParticleXAccordingToMagnets function to the Particle
		/// Events in order for these Magnets to affect the Particles.</para>
		/// </summary>
		public LinkedList<DefaultParticleSystemMagnet> MagnetList = new LinkedList<DefaultParticleSystemMagnet>();

		/// <summary>
		/// Get the Settings used to specify the Initial Properties of a new Particle.
		/// <para>NOTE: These are only applied to the Particle when the InitializeParticleUsingInitialProperties()
		/// function is set as the Particle Initialization Function.</para>
		/// </summary>
		public CInitialProperties InitialProperties => mcInitialProperties;

		/// <summary>
		/// The Name of the Particle System
		/// </summary>
		public string Name
		{
			get
			{
				return msName;
			}
			set
			{
				msName = value;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultBaseParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to Initialize a Default Particle with the Initial Settings
		/// </summary>
		/// <param name="Particle">The Particle to be Initialized</param>
		public virtual void InitializeParticleUsingInitialProperties(DPSFParticle Particle)
		{
			InitializeParticleUsingInitialProperties(Particle, mcInitialProperties);
		}

		/// <summary>
		/// Function to Initialize a Default Particle with the Initial Settings
		/// </summary>
		/// <param name="Particle">The Particle to be Initialized</param>
		/// <param name="cInitialProperties">The Initial Settings to use to Initialize the Particle</param>
		public void InitializeParticleUsingInitialProperties(DPSFParticle Particle, CInitialProperties cInitialProperties)
		{
			DPSFDefaultBaseParticle dPSFDefaultBaseParticle = (DPSFDefaultBaseParticle)Particle;
			dPSFDefaultBaseParticle.Lifetime = DPSFHelper.RandomNumberBetween(cInitialProperties.LifetimeMin, cInitialProperties.LifetimeMax);
			if (cInitialProperties.InterpolateBetweenMinAndMaxPosition)
			{
				dPSFDefaultBaseParticle.Position = Vector3.Lerp(cInitialProperties.PositionMin, cInitialProperties.PositionMax, RandomNumber.NextFloat());
			}
			else
			{
				dPSFDefaultBaseParticle.Position = DPSFHelper.RandomVectorBetweenTwoVectors(cInitialProperties.PositionMin, cInitialProperties.PositionMax);
			}
			if (cInitialProperties.VelocityIsAffectedByEmittersOrientation)
			{
				dPSFDefaultBaseParticle.Position = Vector3.Transform(dPSFDefaultBaseParticle.Position, Emitter.OrientationData.Orientation);
			}
			if (cInitialProperties.PositionIsAffectedByEmittersPosition)
			{
				dPSFDefaultBaseParticle.Position += Emitter.PositionData.Position;
			}
			if (cInitialProperties.InterpolateBetweenMinAndMaxVelocity)
			{
				dPSFDefaultBaseParticle.Velocity = Vector3.Lerp(cInitialProperties.VelocityMin, cInitialProperties.VelocityMax, RandomNumber.NextFloat());
			}
			else
			{
				dPSFDefaultBaseParticle.Velocity = DPSFHelper.RandomVectorBetweenTwoVectors(cInitialProperties.VelocityMin, cInitialProperties.VelocityMax);
			}
			dPSFDefaultBaseParticle.Velocity = Vector3.Transform(dPSFDefaultBaseParticle.Velocity, Emitter.OrientationData.Orientation);
			if (cInitialProperties.InterpolateBetweenMinAndMaxAcceleration)
			{
				dPSFDefaultBaseParticle.Acceleration = Vector3.Lerp(cInitialProperties.AccelerationMin, cInitialProperties.AccelerationMax, RandomNumber.NextFloat());
			}
			else
			{
				dPSFDefaultBaseParticle.Acceleration = DPSFHelper.RandomVectorBetweenTwoVectors(cInitialProperties.AccelerationMin, cInitialProperties.AccelerationMax);
			}
			if (cInitialProperties.InterpolateBetweenMinAndMaxExternalForce)
			{
				dPSFDefaultBaseParticle.ExternalForce = Vector3.Lerp(cInitialProperties.ExternalForceMin, cInitialProperties.ExternalForceMax, RandomNumber.NextFloat());
			}
			else
			{
				dPSFDefaultBaseParticle.ExternalForce = DPSFHelper.RandomVectorBetweenTwoVectors(cInitialProperties.ExternalForceMin, cInitialProperties.ExternalForceMax);
			}
			dPSFDefaultBaseParticle.Friction = DPSFHelper.RandomNumberBetween(cInitialProperties.FrictionMin, cInitialProperties.FrictionMax);
			if (cInitialProperties.InterpolateBetweenMinAndMaxColors)
			{
				dPSFDefaultBaseParticle.StartColor = DPSFHelper.LerpColor(cInitialProperties.StartColorMin, cInitialProperties.StartColorMax, RandomNumber.NextFloat());
				dPSFDefaultBaseParticle.EndColor = DPSFHelper.LerpColor(cInitialProperties.EndColorMin, cInitialProperties.EndColorMax, RandomNumber.NextFloat());
			}
			else
			{
				dPSFDefaultBaseParticle.StartColor = DPSFHelper.LerpColor(cInitialProperties.StartColorMin, cInitialProperties.StartColorMax, RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat());
				dPSFDefaultBaseParticle.EndColor = DPSFHelper.LerpColor(cInitialProperties.EndColorMin, cInitialProperties.EndColorMax, RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat());
			}
			dPSFDefaultBaseParticle.Color = dPSFDefaultBaseParticle.StartColor;
		}

		/// <summary>
		/// Update a Particle's Position according to its Velocity
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticlePositionUsingVelocity(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Position += cParticle.Velocity * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Update a Particle's Velocity according to its Acceleration
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleVelocityUsingAcceleration(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Velocity += cParticle.Acceleration * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Updates a Particle's Velocity according to its Acceleration, and then the Position according
		/// to the new Velocity
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticlePositionAndVelocityUsingAcceleration(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Velocity += cParticle.Acceleration * fElapsedTimeInSeconds;
			cParticle.Position += cParticle.Velocity * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Applies the External Force to the Particle's Position
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticlePositionUsingExternalForce(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Position += cParticle.ExternalForce * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Applies the External Force to the Particle's Velocity
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleVelocityUsingExternalForce(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Velocity += cParticle.ExternalForce * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Applies the Particle's Friction to the its Velocity to slow the Particle down to a stop
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleVelocityUsingFriction(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			if (cParticle.Velocity != Vector3.Zero && cParticle.Friction != 0f)
			{
				Vector3 velocity = cParticle.Velocity;
				float num = velocity.Length();
				num -= cParticle.Friction * fElapsedTimeInSeconds;
				if (num <= 0f)
				{
					velocity = Vector3.Zero;
				}
				else
				{
					velocity.Normalize();
					velocity *= num;
				}
				cParticle.Velocity = velocity;
			}
		}

		/// <summary>
		/// Linearly interpolates the Particles Color between it's Start Color and End Color based on the 
		/// Particle's Normalized Elapsed Time.
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleColorUsingLerp(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Color = DPSFHelper.LerpColor(cParticle.StartColor, cParticle.EndColor, cParticle.NormalizedElapsedTime);
		}

		/// <summary>
		/// Linearly interpolates the Particles Transparency to fade out based on the Particle's Normalized Elapsed Time.
		/// <para>If you are also updating the Particle Color using an EveryTime Event, be sure to set the ExecutionOrder of the 
		/// event calling this function to be greater than that one, so that this function is called AFTER the color update function.</para>
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleTransparencyToFadeOutUsingLerp(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			byte a = (byte)(255f - cParticle.NormalizedElapsedTime * 255f);
			cParticle.Color = new Color(cParticle.Color.R, cParticle.Color.G, cParticle.Color.B, a);
		}

		/// <summary>
		/// Linearly interpolates the Particles Transparency to fade in based on the Particle's Normalized Elapsed Time.
		/// <para>If you are also updating the Particle Color using an EveryTime Event, be sure to set the ExecutionOrder of the 
		/// event calling this function to be greater than that one, so that this function is called AFTER the color update function.</para>
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleTransparencyToFadeInUsingLerp(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			byte a = (byte)(cParticle.NormalizedElapsedTime * 255f);
			cParticle.Color = new Color(cParticle.Color.R, cParticle.Color.G, cParticle.Color.B, a);
		}

		/// <summary>
		/// Quickly fades particle in when born and slowly fades it out as it gets closer to death.
		/// <para>If you are also updating the Particle Color using an EveryTime Event, be sure to set the ExecutionOrder of the 
		/// event calling this function to be greater than that one, so that this function is called AFTER the color update function.</para>
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleTransparencyWithQuickFadeInAndSlowFadeOut(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			byte a = DPSFHelper.FadeInQuicklyAndFadeOutSlowlyBasedOnLifetime(cParticle.NormalizedElapsedTime);
			cParticle.Color = new Color(cParticle.Color.R, cParticle.Color.G, cParticle.Color.B, a);
		}

		/// <summary>
		/// Quickly fades particle in when born and quickly fades it out as it approaches its death.
		/// <para>If you are also updating the Particle Color using an EveryTime Event, be sure to set the ExecutionOrder of the 
		/// event calling this function to be greater than that one, so that this function is called AFTER the color update function.</para>
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleTransparencyWithQuickFadeInAndQuickFadeOut(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			byte a = DPSFHelper.FadeInQuicklyAndFadeOutQuicklyBasedOnLifetime(cParticle.NormalizedElapsedTime);
			cParticle.Color = new Color(cParticle.Color.R, cParticle.Color.G, cParticle.Color.B, a);
		}

		/// <summary>
		/// Calculates how much affect each of the Particle System's Magnets should have on 
		/// this Particle and updates the Particle's Position accordingly.
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticlePositionAccordingToMagnets(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			DefaultParticleSystemMagnet defaultParticleSystemMagnet = null;
			for (LinkedListNode<DefaultParticleSystemMagnet> linkedListNode = MagnetList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				defaultParticleSystemMagnet = linkedListNode.Value;
				if (defaultParticleSystemMagnet.Mode != DefaultParticleSystemMagnet.MagnetModes.Other)
				{
					cParticle.Position += CalculateForceMagnetShouldExertOnParticle(defaultParticleSystemMagnet, cParticle) * fElapsedTimeInSeconds;
				}
			}
		}

		/// <summary>
		/// Calculates how much affect each of the Particle System's Magnets should have on 
		/// this Particle and updates the Particle's Velocity accordingly.
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleVelocityAccordingToMagnets(DPSFDefaultBaseParticle cParticle, float fElapsedTimeInSeconds)
		{
			DefaultParticleSystemMagnet defaultParticleSystemMagnet = null;
			for (LinkedListNode<DefaultParticleSystemMagnet> linkedListNode = MagnetList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				defaultParticleSystemMagnet = linkedListNode.Value;
				if (defaultParticleSystemMagnet.Mode != DefaultParticleSystemMagnet.MagnetModes.Other)
				{
					cParticle.Velocity += CalculateForceMagnetShouldExertOnParticle(defaultParticleSystemMagnet, cParticle) * fElapsedTimeInSeconds;
				}
			}
		}

		/// <summary>
		/// Returns the vector force that a Magnet should exert on a Particle
		/// </summary>
		/// <param name="cMagnet">The Magnet affecting the Particle</param>
		/// <param name="cParticle">The Particle being affected by the Magnet</param>
		/// <returns>Returns the vector force that a Magnet should exert on a Particle</returns>
		protected Vector3 CalculateForceMagnetShouldExertOnParticle(DefaultParticleSystemMagnet cMagnet, DPSFDefaultBaseParticle cParticle)
		{
			Vector3 result = Vector3.Zero;
			Vector3 vector;
			if (cMagnet.MagnetType == DefaultParticleSystemMagnet.MagnetTypes.PointMagnet)
			{
				MagnetPoint magnetPoint = (MagnetPoint)cMagnet;
				vector = magnetPoint.PositionData.Position - cParticle.Position;
			}
			else if (cMagnet.MagnetType == DefaultParticleSystemMagnet.MagnetTypes.LineMagnet)
			{
				MagnetLine magnetLine = (MagnetLine)cMagnet;
				Vector3 positionOnLine = magnetLine.PositionOnLine;
				Vector3 vector2 = magnetLine.PositionOnLine + magnetLine.Direction;
				float num = cParticle.Position.X - positionOnLine.X;
				float num2 = cParticle.Position.Y - positionOnLine.Y;
				float num3 = cParticle.Position.Z - positionOnLine.Z;
				float num4 = vector2.X - positionOnLine.X;
				float num5 = vector2.Y - positionOnLine.Y;
				float num6 = vector2.Z - positionOnLine.Z;
				float num7 = num * num4 + num2 * num5 + num3 * num6;
				vector = new Vector3
				{
					X = positionOnLine.X + num7 * num4,
					Y = positionOnLine.Y + num7 * num5,
					Z = positionOnLine.Z + num7 * num6
				} - cParticle.Position;
			}
			else if (cMagnet.MagnetType == DefaultParticleSystemMagnet.MagnetTypes.LineSegmentMagnet)
			{
				MagnetLineSegment magnetLineSegment = (MagnetLineSegment)cMagnet;
				Vector3 endPoint = magnetLineSegment.EndPoint1;
				Vector3 endPoint2 = magnetLineSegment.EndPoint2;
				float num8 = cParticle.Position.X - endPoint.X;
				float num9 = cParticle.Position.Y - endPoint.Y;
				float num10 = cParticle.Position.Z - endPoint.Z;
				float num11 = endPoint2.X - endPoint.X;
				float num12 = endPoint2.Y - endPoint.Y;
				float num13 = endPoint2.Z - endPoint.Z;
				float num14 = num8 * num11 + num9 * num12 + num10 * num13;
				float num15 = num11 * num11 + num12 * num12 + num13 * num13;
				float num16 = num14 / num15;
				Vector3 vector3 = default;
				if (num16 < 0f)
				{
					vector3 = endPoint;
				}
				else if (num16 > 1f)
				{
					vector3 = endPoint2;
				}
				else
				{
					vector3.X = endPoint.X + num16 * (endPoint2.X - endPoint.X);
					vector3.Y = endPoint.Y + num16 * (endPoint2.Y - endPoint.Y);
					vector3.Z = endPoint.Z + num16 * (endPoint2.Z - endPoint.Z);
				}
				vector = vector3 - cParticle.Position;
			}
			else
			{
				if (cMagnet.MagnetType != DefaultParticleSystemMagnet.MagnetTypes.PlaneMagnet)
				{
					return Vector3.Zero;
				}
				MagnetPlane magnetPlane = (MagnetPlane)cMagnet;
				float num17 = Vector3.Dot(cParticle.Position - magnetPlane.PositionOnPlane, magnetPlane.Normal);
				Vector3 vector4 = cParticle.Position + -magnetPlane.Normal * num17;
				vector = vector4 - cParticle.Position;
			}
			if (cMagnet.Mode == DefaultParticleSystemMagnet.MagnetModes.Repel)
			{
				vector *= -1f;
			}
			if (vector == Vector3.Zero && cMagnet.Mode == DefaultParticleSystemMagnet.MagnetModes.Repel)
			{
				vector = DPSFHelper.RandomNormalizedVector() * 1E-05f;
			}
			float num18 = vector.Length();
			if (num18 >= cMagnet.MinDistance && num18 <= cMagnet.MaxDistance)
			{
				if (vector != Vector3.Zero)
				{
					vector.Normalize();
				}
				float num19 = 0f;
				num19 = ((cMagnet.MaxDistance == cMagnet.MinDistance) ? 1f : ((num18 - cMagnet.MinDistance) / (cMagnet.MaxDistance - cMagnet.MinDistance)));
				float num20 = 0f;
				num20 = cMagnet.DistanceFunction switch
				{
					DefaultParticleSystemMagnet.DistanceFunctions.Linear => MathHelper.Lerp(0f, cMagnet.MaxForce, num19), 
					DefaultParticleSystemMagnet.DistanceFunctions.Squared => MathHelper.Lerp(0f, cMagnet.MaxForce, num19 * num19), 
					DefaultParticleSystemMagnet.DistanceFunctions.Cubed => MathHelper.Lerp(0f, cMagnet.MaxForce, num19 * num19 * num19), 
					DefaultParticleSystemMagnet.DistanceFunctions.LinearInverse => MathHelper.Lerp(cMagnet.MaxForce, 0f, num19), 
					DefaultParticleSystemMagnet.DistanceFunctions.SquaredInverse => MathHelper.Lerp(cMagnet.MaxForce, 0f, num19 * num19), 
					DefaultParticleSystemMagnet.DistanceFunctions.CubedInverse => MathHelper.Lerp(cMagnet.MaxForce, 0f, num19 * num19 * num19), 
					_ => cMagnet.MaxForce, 
				};
				result = vector * (num20 * cMagnet.MaxForce);
			}
			return result;
		}

		/// <summary>
		/// Sets the Emitter to Emit Particles Automatically
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemEmitParticlesAutomaticallyOn(float fElapsedTimeInSeconds)
		{
			Emitter.EmitParticlesAutomatically = true;
		}

		/// <summary>
		/// Sets the Emitter to not Emit Particles Automatically
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemEmitParticlesAutomaticallyOff(float fElapsedTimeInSeconds)
		{
			Emitter.EmitParticlesAutomatically = false;
		}

		/// <summary>
		/// Enables the Emitter
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemEnableEmitter(float fElapsedTimeInSeconds)
		{
			Emitter.Enabled = true;
		}

		/// <summary>
		/// Disables the Emitter
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemDisableEmitter(float fElapsedTimeInSeconds)
		{
			Emitter.Enabled = false;
		}
	}
	/// <summary>
	/// The Default Sprite Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultSpriteParticleSystem<Particle, Vertex> : DPSFDefaultBaseParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Particle System Properties used to initialize a Particle's Properties.
		/// <para>NOTE: These are only applied to the Particle when the InitializeParticleUsingInitialProperties()
		/// function is set as the Particle Initialization Function.</para>
		/// </summary>
		public class CInitialPropertiesForSprite : CInitialProperties
		{
			public float RotationMin;

			public float RotationMax;

			public float RotationalVelocityMin;

			public float RotationalVelocityMax;

			public float RotationalAccelerationMin;

			public float RotationalAccelerationMax;

			public float StartWidthMin = 10f;

			public float StartWidthMax = 10f;

			public float StartHeightMin = 10f;

			public float StartHeightMax = 10f;

			public float EndWidthMin = 10f;

			public float EndWidthMax = 10f;

			public float EndHeightMin = 10f;

			public float EndHeightMax = 10f;

			/// <summary>
			/// The Min Start Size for the particle's StartWidth and StartHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the StartWidthMin and StartHeightMin properties.</para>
			/// </summary>
			public float StartSizeMin;

			/// <summary>
			/// The Max Start Size for the particle's StartWidth and StartHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the StartWidthMax and StartHeightMax properties.</para>
			/// </summary>
			public float StartSizeMax;

			/// <summary>
			/// The Min End Size for the particle's EndWidth and EndHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the EndWidthMin and EndHeightMin properties.</para>
			/// </summary>
			public float EndSizeMin;

			/// <summary>
			/// The Max End Size for the particle's EndWidth and EndHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the EndWidthMax and EndHeightMax properties.</para>
			/// </summary>
			public float EndSizeMax;
		}

		private CInitialPropertiesForSprite mcInitialProperties = new CInitialPropertiesForSprite();

		/// <summary>
		/// Get the Settings used to specify the Initial Properties of a new Particle.
		/// <para>NOTE: These are only applied to the Particle when the InitializeParticleUsingInitialProperties()
		/// function is set as the Particle Initialization Function.</para>
		/// </summary>
		public new CInitialPropertiesForSprite InitialProperties => mcInitialProperties;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultSpriteParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to draw a Sprite Particle. This function should be used to draw the given
		/// Particle with the provided SpriteBatch.
		/// </summary>
		/// <param name="Particle">The Particle Sprite to Draw</param>
		/// <param name="cSpriteBatch">The SpriteBatch to use to doing the Drawing</param>
		protected override void DrawSprite(DPSFParticle Particle, SpriteBatch cSpriteBatch)
		{
			DefaultSpriteParticle defaultSpriteParticle = (DefaultSpriteParticle)Particle;
			Rectangle destinationRectangle = new Rectangle((int)defaultSpriteParticle.Position.X, (int)defaultSpriteParticle.Position.Y, (int)defaultSpriteParticle.Width, (int)defaultSpriteParticle.Height);
			float layerDepth = MathHelper.Clamp(defaultSpriteParticle.Position.Z, 0f, 1f);
			Rectangle value = new Rectangle(0, 0, Texture.Width, Texture.Height);
			cSpriteBatch.Draw(origin: new Vector2(value.Width / 2, value.Height / 2), texture: Texture, destinationRectangle: destinationRectangle, sourceRectangle: value, color: defaultSpriteParticle.Color, rotation: defaultSpriteParticle.Rotation, effects: defaultSpriteParticle.FlipMode, layerDepth: layerDepth);
		}

		/// <summary>
		/// Function to Initialize a Default Particle with default Properties
		/// </summary>
		/// <param name="Particle">The Particle to be Initialized</param>
		public override void InitializeParticleUsingInitialProperties(DPSFParticle Particle)
		{
			DefaultSpriteParticle defaultSpriteParticle = (DefaultSpriteParticle)Particle;
			InitializeParticleUsingInitialProperties(defaultSpriteParticle, mcInitialProperties);
			defaultSpriteParticle.Rotation = DPSFHelper.RandomNumberBetween(mcInitialProperties.RotationMin, mcInitialProperties.RotationMax);
			defaultSpriteParticle.RotationalVelocity = DPSFHelper.RandomNumberBetween(mcInitialProperties.RotationalVelocityMin, mcInitialProperties.RotationalVelocityMax);
			defaultSpriteParticle.RotationalAcceleration = DPSFHelper.RandomNumberBetween(mcInitialProperties.RotationalAccelerationMin, mcInitialProperties.RotationalAccelerationMax);
			defaultSpriteParticle.StartWidth = DPSFHelper.RandomNumberBetween((mcInitialProperties.StartSizeMin > 0f) ? mcInitialProperties.StartSizeMin : mcInitialProperties.StartWidthMin, (mcInitialProperties.StartSizeMax > 0f) ? mcInitialProperties.StartSizeMax : mcInitialProperties.StartWidthMax);
			defaultSpriteParticle.EndWidth = DPSFHelper.RandomNumberBetween((mcInitialProperties.EndSizeMin > 0f) ? mcInitialProperties.EndSizeMin : mcInitialProperties.EndWidthMin, (mcInitialProperties.EndSizeMax > 0f) ? mcInitialProperties.EndSizeMax : mcInitialProperties.EndWidthMax);
			defaultSpriteParticle.StartHeight = DPSFHelper.RandomNumberBetween((mcInitialProperties.StartSizeMin > 0f) ? mcInitialProperties.StartSizeMin : mcInitialProperties.StartHeightMin, (mcInitialProperties.StartSizeMax > 0f) ? mcInitialProperties.StartSizeMax : mcInitialProperties.StartHeightMax);
			defaultSpriteParticle.EndHeight = DPSFHelper.RandomNumberBetween((mcInitialProperties.EndSizeMin > 0f) ? mcInitialProperties.EndSizeMin : mcInitialProperties.EndHeightMin, (mcInitialProperties.EndSizeMax > 0f) ? mcInitialProperties.EndSizeMax : mcInitialProperties.EndHeightMax);
			defaultSpriteParticle.Width = defaultSpriteParticle.StartWidth;
			defaultSpriteParticle.Height = defaultSpriteParticle.StartHeight;
		}

		/// <summary>
		/// Update a Particle's Rotation according to its Rotational Velocity
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleRotationUsingRotationalVelocity(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Rotation += cParticle.RotationalVelocity * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Update a Particle's Rotational Velocity according to its Rotational Acceleration
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleRotationalVelocityUsingRotationalAcceleration(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.RotationalVelocity += cParticle.RotationalAcceleration * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Update a Particle's Rotation and Rotational Velocity according to its Rotational Acceleration
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleRotationAndRotationalVelocityUsingRotationalAcceleration(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.RotationalVelocity += cParticle.RotationalAcceleration * fElapsedTimeInSeconds;
			cParticle.Rotation += cParticle.RotationalVelocity * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Linearly interpolate the Particle's Width between the Start and End Width according
		/// to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleWidthUsingLerp(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Width = MathHelper.Lerp(cParticle.StartWidth, cParticle.EndWidth, cParticle.NormalizedElapsedTime);
		}

		/// <summary>
		/// Linearly interpolate the Particle's Height between the Start and End Height according
		/// to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleHeightUsingLerp(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Height = MathHelper.Lerp(cParticle.StartHeight, cParticle.EndHeight, cParticle.NormalizedElapsedTime);
		}

		/// <summary>
		/// Linearly interpolate the Particle's Width and Height between the Start and End values according
		/// to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleWidthAndHeightUsingLerp(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Width = MathHelper.Lerp(cParticle.StartWidth, cParticle.EndWidth, cParticle.NormalizedElapsedTime);
			cParticle.Height = MathHelper.Lerp(cParticle.StartHeight, cParticle.EndHeight, cParticle.NormalizedElapsedTime);
		}

		/// <summary>
		/// Linearly interpolate the Particle's Position.Z value from 1.0 (back) to
		/// 0.0 (front) according to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleDepthFromBackToFrontUsingLerp(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Position.Z = 1f - cParticle.NormalizedElapsedTime;
		}

		/// <summary>
		/// Linearly interpolate the Particle's Position.Z value from 0.0 (front) to
		/// 1.0 (back) according to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleDepthFromFrontToBackUsingLerp(DefaultSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Position.Z = cParticle.NormalizedElapsedTime;
		}

		/// <summary>
		/// Sorts the Particle System's Active Particles so that the Particles at the back
		/// (i.e. Position.Z = 1.0) are drawn before the Particles at the front (i.e. 
		/// Position.Z = 0.0).
		/// <para>NOTE: This operation is very expensive and should only be used when you are
		/// using a Shader (i.e. Effect and Technique).</para>
		/// <para>If you are not using a Shader and want the Particles sorted by Depth, use SpriteSortMode.BackToFront.</para>
		/// <para>Merge Sort is the sorting algorithm used, as it tends to be best for linked lists.
		/// TODO - WHILE MERGE SORT SHOULD BE USED, DUE TO TIME CONSTRAINTS A (PROBABLY) SLOWER METHOD (QUICK-SORT)
		/// IS BEING USED INSTEAD. THIS FUNCTION NEEDS TO BE UPDATED TO USE MERGE SORT STILL.
		/// THE LINKED LIST MERGE SORT ALGORITHM CAN BE FOUND AT http://www.chiark.greenend.org.uk/~sgtatham/algorithms/listsort.html</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemToSortParticlesByDepth(float fElapsedTimeInSeconds)
		{
			int count = ActiveParticles.Count;
			if (count > 1)
			{
				List<Particle> list = new List<Particle>(count);
				for (LinkedListNode<Particle> linkedListNode = ActiveParticles.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					list.Add(linkedListNode.Value);
				}
				list.Sort((Particle Particle1, Particle Particle2) =>
				{
					DPSFDefaultBaseParticle dPSFDefaultBaseParticle = (DPSFDefaultBaseParticle)(object)Particle1;
					DPSFDefaultBaseParticle dPSFDefaultBaseParticle2 = (DPSFDefaultBaseParticle)(object)Particle2;
					return dPSFDefaultBaseParticle.Position.Z.CompareTo(dPSFDefaultBaseParticle2.Position.Z);
				});
				ActiveParticles.Clear();
				for (int num = 0; num < count; num++)
				{
					ActiveParticles.AddFirst(list[num]);
				}
			}
		}
	}
	/// <summary>
	/// The Default Sprite 3D Billboard Particle System class.
	/// This class just inherits from the Default Sprite Particle System class and overrides the DrawSprite()
	/// function to draw the sprites as Billboards in 3D space.
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use.</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use.</typeparam>
	public class DPSFDefaultSprite3DBillboardParticleSystem<Particle, Vertex> : DPSFDefaultSpriteParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Get / Set the Position of the Camera.
		/// <para>NOTE: This should be Set (updated) every frame if Billboarding will be used (i.e. Always have the Particles face the Camera).</para>
		/// </summary>
		public Vector3 CameraPosition { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultSprite3DBillboardParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to draw a Sprite Particle. This function should be used to draw the given
		/// Particle with the provided SpriteBatch.
		/// </summary>
		/// <param name="Particle">The Particle Sprite to Draw</param>
		/// <param name="cSpriteBatch">The SpriteBatch to use to doing the Drawing</param>
		protected override void DrawSprite(DPSFParticle Particle, SpriteBatch cSpriteBatch)
		{
			DefaultSprite3DBillboardParticle defaultSprite3DBillboardParticle = (DefaultSprite3DBillboardParticle)Particle;
			Vector3 vector = Vector3.Transform(defaultSprite3DBillboardParticle.Position, View);
			Vector2 position = new Vector2(vector.X, vector.Y);
			Rectangle value = new Rectangle(0, 0, Texture.Width, Texture.Height);
			cSpriteBatch.Draw(scale: new Vector2(defaultSprite3DBillboardParticle.Width / (float)value.Width, (0f - defaultSprite3DBillboardParticle.Height) / (float)value.Height), origin: new Vector2(value.Width / 2, value.Height / 2), texture: Texture, position: position, sourceRectangle: value, color: defaultSprite3DBillboardParticle.ColorAsPremultiplied, rotation: defaultSprite3DBillboardParticle.Rotation, effects: defaultSprite3DBillboardParticle.FlipMode, layerDepth: vector.Z);
		}

		/// <summary>
		/// Function to setup the Render Properties (i.e. BlendState, DepthStencilState, RasterizerState, and SamplerState)
		/// which will be applied to the Graphics Device before drawing the Particle System's Particles.
		/// <para>This function is called when initializing the particle system.</para>
		/// </summary>
		protected override void InitializeRenderProperties()
		{
			Effect = new AlphaTestEffect(GraphicsDevice);
			RenderProperties.RasterizerState = DPSFHelper.CloneRasterizerState(RasterizerState.CullNone);
		}

		/// <summary>
		/// Function to set the Shader's global variables before drawing
		/// </summary>
		protected override void SetEffectParameters()
		{
			if (Effect is AlphaTestEffect alphaTestEffect)
			{
				alphaTestEffect.World = World;
				alphaTestEffect.View = Matrix.Identity;
				alphaTestEffect.Projection = Projection;
				alphaTestEffect.VertexColorEnabled = true;
			}
		}

		/// <summary>
		/// Sets the camera position, so that the particles know how to make themselves face the camera if needed.
		/// </summary>
		/// <param name="cameraPosition">The camera position.</param>
		public override void SetCameraPosition(Vector3 cameraPosition)
		{
			CameraPosition = cameraPosition;
		}

		/// <summary>
		/// Updates the Particle's DistanceFromCameraSquared property to reflect how far this Particle is from the Camera.
		/// </summary>
		/// <param name="cParticle">The Particle to update.</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update.</param>
		protected void UpdateParticleDistanceFromCameraSquared(DefaultSprite3DBillboardParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.DistanceFromCameraSquared = Vector3.DistanceSquared(CameraPosition, cParticle.Position);
		}

		/// <summary>
		/// Sorts the particles to draw particles furthest from the camera first, in order to achieve proper depth perspective.
		///
		/// <para>NOTE: This operation is very expensive and should only be used when you are
		/// drawing particles with both opaque and semi-transparent portions, and not using additive blending.</para>
		/// <para>Merge Sort is the sorting algorithm used, as it tends to be best for linked lists.
		/// TODO - WHILE MERGE SORT SHOULD BE USED, DUE TO TIME CONSTRAINTS A (PROBABLY) SLOWER METHOD (QUICK-SORT)
		/// IS BEING USED INSTEAD. THIS FUNCTION NEEDS TO BE UPDATED TO USE MERGE SORT STILL.
		/// THE LINKED LIST MERGE SORT ALGORITHM CAN BE FOUND AT http://www.chiark.greenend.org.uk/~sgtatham/algorithms/listsort.html</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemToSortParticlesByDistanceFromCamera(float fElapsedTimeInSeconds)
		{
			int count = ActiveParticles.Count;
			if (count > 1)
			{
				List<Particle> list = new List<Particle>(count);
				for (LinkedListNode<Particle> linkedListNode = ActiveParticles.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					list.Add(linkedListNode.Value);
				}
				list.Sort((Particle Particle1, Particle Particle2) =>
				{
					DefaultSprite3DBillboardParticle defaultSprite3DBillboardParticle = (DefaultSprite3DBillboardParticle)(object)Particle1;
					DefaultSprite3DBillboardParticle defaultSprite3DBillboardParticle2 = (DefaultSprite3DBillboardParticle)(object)Particle2;
					return defaultSprite3DBillboardParticle.DistanceFromCameraSquared.CompareTo(defaultSprite3DBillboardParticle2.DistanceFromCameraSquared);
				});
				ActiveParticles.Clear();
				for (int num = 0; num < count; num++)
				{
					ActiveParticles.AddFirst(list[num]);
				}
			}
		}
	}
	/// <summary>
	/// The Default Sprite 3D Billboard Particle System to inherit from, which uses Default Sprite 3D Billboard Particles
	/// </summary>
	public class DefaultSprite3DBillboardParticleSystem : DPSFDefaultSprite3DBillboardParticleSystem<DefaultSprite3DBillboardParticle, DefaultSpriteParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultSprite3DBillboardParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Sprite Particle System
	/// </summary>
	public class DefaultSpriteParticle : DPSFDefaultBaseParticle
	{
		/// <summary>
		/// How much the Particle should be Rotated
		/// </summary>
		public float Rotation;

		/// <summary>
		/// The Width of the Particle
		/// </summary>
		public float Width;

		/// <summary>
		/// The Height of the Particle
		/// </summary>
		public float Height;

		/// <summary>
		/// Tells if the Sprite should be flipped Horizontally or Vertically
		/// </summary>
		public SpriteEffects FlipMode;

		/// <summary>
		/// The Particle's Rotational Velocity
		/// </summary>
		public float RotationalVelocity;

		/// <summary>
		/// The Particle's Rotational Acceleration
		/// </summary>
		public float RotationalAcceleration;

		/// <summary>
		/// The Width of the Particle when it is born
		/// </summary>
		public float StartWidth;

		/// <summary>
		/// The Height of the Particle when it is born
		/// </summary>
		public float StartHeight;

		/// <summary>
		/// The Width of the Particle when it dies
		/// </summary>
		public float EndWidth;

		/// <summary>
		/// The Height of the Particle when it dies
		/// </summary>
		public float EndHeight;

		/// <summary>
		/// Sets the Width and Height properties to the given value.
		/// Gets the Width value, ignoring whether the Height value is the same or not.
		/// </summary>
		public float Size
		{
			get
			{
				return Width;
			}
			set
			{
				Width = value;
				Height = value;
			}
		}

		/// <summary>
		/// Sets the StartWidth and StartHeight properties to the given value.
		/// Gets the StartWidth value, ignoring whether the StartHeight value is the same or not.
		/// </summary>
		public float StartSize
		{
			get
			{
				return StartWidth;
			}
			set
			{
				StartWidth = value;
				StartHeight = value;
			}
		}

		/// <summary>
		/// Sets the EndWidth and EndHeight properties to the given value.
		/// Gets the EndWidth value, ignoring whether the EndHeight value is the same or not.
		/// </summary>
		public float EndSize
		{
			get
			{
				return EndWidth;
			}
			set
			{
				EndWidth = value;
				EndHeight = value;
			}
		}

		/// <summary>
		/// Scales the Width and Height by the given amount.
		/// </summary>
		/// <param name="scale">The amount to scale the Width and Height by.</param>
		public void Scale(float scale)
		{
			Width *= scale;
			Height *= scale;
		}

		/// <summary>
		/// Updates the Width to the given value and uniformly scales the Height to maintain the width-to-height ratio.
		/// </summary>
		/// <param name="newWidth">The Width the particle should have.</param>
		public void ScaleToWidth(float newWidth)
		{
			if (Width != 0f)
			{
				float num = newWidth / Width;
				Height *= num;
				Width = newWidth;
			}
		}

		/// <summary>
		/// Updates the Height to the given value and uniformly scales the Width to maintain the width-to-height ratio.
		/// </summary>
		/// <param name="newHeight">The Height the particle should have.</param>
		public void ScaleToHeight(float newHeight)
		{
			if (Height != 0f)
			{
				float num = newHeight / Height;
				Width *= num;
				Height = newHeight;
			}
		}

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			Rotation = 0f;
			Width = (Height = 10f);
			FlipMode = SpriteEffects.None;
			RotationalVelocity = (RotationalAcceleration = 0f);
			StartWidth = (StartHeight = (EndWidth = (EndHeight = 10f)));
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultSpriteParticle defaultSpriteParticle = (DefaultSpriteParticle)ParticleToCopy;
			base.CopyFrom(defaultSpriteParticle);
			Rotation = defaultSpriteParticle.Rotation;
			Width = defaultSpriteParticle.Width;
			Height = defaultSpriteParticle.Height;
			FlipMode = defaultSpriteParticle.FlipMode;
			RotationalVelocity = defaultSpriteParticle.RotationalVelocity;
			RotationalAcceleration = defaultSpriteParticle.RotationalAcceleration;
			StartWidth = defaultSpriteParticle.StartWidth;
			StartHeight = defaultSpriteParticle.StartHeight;
			EndWidth = defaultSpriteParticle.EndWidth;
			EndHeight = defaultSpriteParticle.EndHeight;
		}
	}
	/// <summary>
	/// Particle used by the Default Sprite 3D Billboard Particle System
	/// </summary>
	public class DefaultSprite3DBillboardParticle : DefaultSpriteParticle
	{
		/// <summary>
		/// The squared distance between this particle and the camera.
		/// <para>NOTE: This property is only used if you are sorting the particles based on their distance 
		/// from the camera, otherwise you can use this property for whatever you like.</para>
		/// </summary>
		public float DistanceFromCameraSquared;

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			DistanceFromCameraSquared = 0f;
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultSprite3DBillboardParticle defaultSprite3DBillboardParticle = (DefaultSprite3DBillboardParticle)ParticleToCopy;
			base.CopyFrom(defaultSprite3DBillboardParticle);
			DistanceFromCameraSquared = defaultSprite3DBillboardParticle.DistanceFromCameraSquared;
		}
	}
	/// <summary>
	/// Static class used to apply default settings to all DPSF particle systems and particle system managers when they are initialized.
	/// <para>Note: These settings are applied during particle system initialization, and may be overwritten by particle system manager settings.</para>
	/// </summary>
	public static class DPSFDefaultSettings
	{
		/// <summary>
		/// Gets / Sets the default particle system memory management settings.
		/// </summary>
		public static AutoMemoryManagerSettings AutoMemoryManagementSettings { get; private set; }

		/// <summary>
		/// Gets / Sets the default particle system updates per second.
		/// </summary>
		public static int UpdatesPerSecond { get; set; }

		/// <summary>
		/// Gets / Sets if particle system performance timings are Enabled by default or not.
		/// Performance profiling is not available on the Reach profile.
		/// </summary>
		public static bool PerformanceProfilingIsEnabled { get; set; }

		/// <summary>
		/// Initializes the <see cref="T:DPSF.DPSFDefaultSettings" /> class.
		/// This constructor cannot be explicitly called. It will be called the first time this class is accessed.
		/// </summary>
		static DPSFDefaultSettings()
		{
			ResetToDefaults();
		}

		/// <summary>
		/// Resets all of the DPSFDefaultSettings' properties to their default values.
		/// </summary>
		public static void ResetToDefaults()
		{
			AutoMemoryManagementSettings = new AutoMemoryManagerSettings();
			UpdatesPerSecond = 0;
			PerformanceProfilingIsEnabled = false;
		}
	}
	/// <summary>
	/// Class that may be used to obtain random numbers. This class inherits the Random class
	/// and adds additional functionality.
	/// </summary>
	public class RandomNumbers : Random
	{
		/// <summary>
		/// Returns a random number between the specified values
		/// </summary>
		/// <param name="fValue1">The first value</param>
		/// <param name="fValue2">The second value</param>
		/// <returns>Returns a random number between the specified values</returns>
		public float Between(float fValue1, float fValue2)
		{
			return fValue1 + (float)NextDouble() * (fValue2 - fValue1);
		}

		/// <summary>
		/// Returns a random number between 0.0f and 1.0f.
		/// </summary>
		/// <returns>Returns a random number between 0.0f and 1.0f.</returns>
		public float NextFloat()
		{
			return (float)NextDouble();
		}
	}
	/// <summary>
	/// The Default Quad Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultQuadParticleSystem<Particle, Vertex> : DPSFDefaultBaseParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Particle System Properties used to initialize a Particle's Properties.
		/// <para>NOTE: These are only applied to the Particle when the InitializeParticleUsingInitialProperties()
		/// function is set as the Particle Initialization Function.</para>
		/// </summary>
		public class CInitialPropertiesForQuad : CInitialProperties
		{
			public Vector3 RotationMin = Vector3.Zero;

			public Vector3 RotationMax = Vector3.Zero;

			public Vector3 RotationalVelocityMin = Vector3.Zero;

			public Vector3 RotationalVelocityMax = Vector3.Zero;

			public Vector3 RotationalAccelerationMin = Vector3.Zero;

			public Vector3 RotationalAccelerationMax = Vector3.Zero;

			public float StartWidthMin = 10f;

			public float StartWidthMax = 10f;

			public float StartHeightMin = 10f;

			public float StartHeightMax = 10f;

			public float EndWidthMin = 10f;

			public float EndWidthMax = 10f;

			public float EndHeightMin = 10f;

			public float EndHeightMax = 10f;

			/// <summary>
			/// The Min Start Size for the particle's StartWidth and StartHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the StartWidthMin and StartHeightMin properties.</para>
			/// </summary>
			public float StartSizeMin;

			/// <summary>
			/// The Max Start Size for the particle's StartWidth and StartHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the StartWidthMax and StartHeightMax properties.</para>
			/// </summary>
			public float StartSizeMax;

			/// <summary>
			/// The Min End Size for the particle's EndWidth and EndHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the EndWidthMin and EndHeightMin properties.</para>
			/// </summary>
			public float EndSizeMin;

			/// <summary>
			/// The Max End Size for the particle's EndWidth and EndHeight properties.
			/// <para>NOTE: If this is greater than zero, this will be used instead of 
			/// the EndWidthMax and EndHeightMax properties.</para>
			/// </summary>
			public float EndSizeMax;

			/// <summary>
			/// If true, the Rotation will be somewhere on the vector joining the Min Rotation to the Max Rotation.
			/// <para>If false, each of the XYZ components will be randomly calculated individually between the Min and Max Rotation XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxRotation;

			/// <summary>
			/// If true, the Rotational Velocity will be somewhere on the vector joining the Min Rotational Velocity to the Max Rotational Velocity.
			/// <para>If false, each of the XYZ components will be randomly calculated individually between the Min and Max Rotational Velocity XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxRotationalVelocity;

			/// <summary>
			/// If true, the Rotational Acceleration will be somewhere on the vector joining the Min Rotational Acceleration to the Max Rotational Acceleration.
			/// <para>If false, each of the XYZ components will be randomly calculated individually between the Min and Max Rotational Acceleration XYZ values.</para>
			/// <para>Default value is false.</para>
			/// </summary>
			public bool InterpolateBetweenMinAndMaxRotationalAcceleration;
		}

		private CInitialPropertiesForQuad mcInitialProperties = new CInitialPropertiesForQuad();

		/// <summary>
		/// Get the Settings used to specify the Initial Properties of a new Particle.
		/// <para>NOTE: These are only applied to the Particle when the InitializeParticleUsingInitialProperties()
		/// function is set as the Particle Initialization Function.</para>
		/// </summary>
		public new CInitialPropertiesForQuad InitialProperties => mcInitialProperties;

		/// <summary>
		/// Get / Set the Position of the Camera.
		/// <para>NOTE: This should be Set (updated) every frame if Billboarding will be used (i.e. Always have the Particles face the Camera).</para>
		/// </summary>
		public Vector3 CameraPosition { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultQuadParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to update the Vertex properties according to the Particle properties
		/// </summary>
		/// <param name="sVertexBuffer">The array containing the Vertices to be drawn</param>
		/// <param name="iIndex">The Index in the array where the Particle's Vertex info should be placed</param>
		/// <param name="Particle">The Particle to copy the information from</param>
		protected virtual void UpdateVertexProperties(ref DefaultQuadParticleVertex[] sVertexBuffer, int iIndex, DPSFParticle Particle)
		{
			DefaultQuadParticle defaultQuadParticle = (DefaultQuadParticle)Particle;
			float num = defaultQuadParticle.Width / 2f;
			float num2 = defaultQuadParticle.Height / 2f;
			Vector3 value = new Vector3(0f - num, 0f - num2, 0f);
			Vector3 value2 = new Vector3(num, 0f - num2, 0f);
			Vector3 value3 = new Vector3(0f - num, num2, 0f);
			Vector3 value4 = new Vector3(num, num2, 0f);
			value = Vector3.Transform(value, defaultQuadParticle.Orientation) + defaultQuadParticle.Position;
			value2 = Vector3.Transform(value2, defaultQuadParticle.Orientation) + defaultQuadParticle.Position;
			value3 = Vector3.Transform(value3, defaultQuadParticle.Orientation) + defaultQuadParticle.Position;
			value4 = Vector3.Transform(value4, defaultQuadParticle.Orientation) + defaultQuadParticle.Position;
			Color colorAsPremultiplied = defaultQuadParticle.ColorAsPremultiplied;
			sVertexBuffer[iIndex].Position = value3;
			sVertexBuffer[iIndex].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 1].Position = value;
			sVertexBuffer[iIndex + 1].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 2].Position = value4;
			sVertexBuffer[iIndex + 2].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 3].Position = value2;
			sVertexBuffer[iIndex + 3].Color = colorAsPremultiplied;
			if (GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef)
			{
				IndexBuffer[IndexBufferIndex++] = iIndex + 1;
				IndexBuffer[IndexBufferIndex++] = iIndex + 2;
				IndexBuffer[IndexBufferIndex++] = iIndex;
				IndexBuffer[IndexBufferIndex++] = iIndex + 1;
				IndexBuffer[IndexBufferIndex++] = iIndex + 3;
				IndexBuffer[IndexBufferIndex++] = iIndex + 2;
			}
			else
			{
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 1);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 2);
				IndexBufferReach[IndexBufferIndex++] = (short)iIndex;
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 1);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 3);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 2);
			}
		}

		/// <summary>
		/// Virtual function to Set the Effect's Parameters before drawing the Particles
		/// </summary>
		protected override void SetEffectParameters()
		{
			if (Effect is BasicEffect basicEffect)
			{
				basicEffect.World = World;
				basicEffect.View = View;
				basicEffect.Projection = Projection;
				basicEffect.VertexColorEnabled = true;
				basicEffect.TextureEnabled = false;
				basicEffect.FogEnabled = false;
				basicEffect.LightingEnabled = false;
			}
		}

		/// <summary>
		/// Sets the camera position, so that the particles know how to make themselves face the camera if needed.
		/// </summary>
		/// <param name="cameraPosition">The camera position.</param>
		public override void SetCameraPosition(Vector3 cameraPosition)
		{
			CameraPosition = cameraPosition;
		}

		/// <summary>
		/// Function to Initialize a Default Particle with default settings
		/// </summary>
		/// <param name="Particle">The Particle to be Initialized</param>
		public override void InitializeParticleUsingInitialProperties(DPSFParticle Particle)
		{
			DefaultQuadParticle defaultQuadParticle = (DefaultQuadParticle)Particle;
			InitializeParticleUsingInitialProperties(defaultQuadParticle, mcInitialProperties);
			if (mcInitialProperties.InterpolateBetweenMinAndMaxRotation)
			{
				Vector3 vector = Vector3.Lerp(mcInitialProperties.RotationMin, mcInitialProperties.RotationMax, RandomNumber.NextFloat());
				defaultQuadParticle.Orientation = Quaternion.CreateFromYawPitchRoll(vector.Y, vector.X, vector.Z);
			}
			else
			{
				Vector3 vector2 = DPSFHelper.RandomVectorBetweenTwoVectors(mcInitialProperties.RotationMin, mcInitialProperties.RotationMax);
				defaultQuadParticle.Orientation = Quaternion.CreateFromYawPitchRoll(vector2.Y, vector2.X, vector2.Z);
			}
			if (mcInitialProperties.InterpolateBetweenMinAndMaxRotationalVelocity)
			{
				defaultQuadParticle.RotationalVelocity = Vector3.Lerp(mcInitialProperties.RotationalVelocityMin, mcInitialProperties.RotationalVelocityMax, RandomNumber.NextFloat());
			}
			else
			{
				defaultQuadParticle.RotationalVelocity = DPSFHelper.RandomVectorBetweenTwoVectors(mcInitialProperties.RotationalVelocityMin, mcInitialProperties.RotationalVelocityMax);
			}
			if (mcInitialProperties.InterpolateBetweenMinAndMaxRotationalAcceleration)
			{
				defaultQuadParticle.RotationalAcceleration = Vector3.Lerp(mcInitialProperties.RotationalAccelerationMin, mcInitialProperties.RotationalAccelerationMax, RandomNumber.NextFloat());
			}
			else
			{
				defaultQuadParticle.RotationalAcceleration = DPSFHelper.RandomVectorBetweenTwoVectors(mcInitialProperties.RotationalAccelerationMin, mcInitialProperties.RotationalAccelerationMax);
			}
			defaultQuadParticle.StartWidth = DPSFHelper.RandomNumberBetween((mcInitialProperties.StartSizeMin > 0f) ? mcInitialProperties.StartSizeMin : mcInitialProperties.StartWidthMin, (mcInitialProperties.StartSizeMax > 0f) ? mcInitialProperties.StartSizeMax : mcInitialProperties.StartWidthMax);
			defaultQuadParticle.EndWidth = DPSFHelper.RandomNumberBetween((mcInitialProperties.EndSizeMin > 0f) ? mcInitialProperties.EndSizeMin : mcInitialProperties.EndWidthMin, (mcInitialProperties.EndSizeMax > 0f) ? mcInitialProperties.EndSizeMax : mcInitialProperties.EndWidthMax);
			defaultQuadParticle.StartHeight = DPSFHelper.RandomNumberBetween((mcInitialProperties.StartSizeMin > 0f) ? mcInitialProperties.StartSizeMin : mcInitialProperties.StartHeightMin, (mcInitialProperties.StartSizeMax > 0f) ? mcInitialProperties.StartSizeMax : mcInitialProperties.StartHeightMax);
			defaultQuadParticle.EndHeight = DPSFHelper.RandomNumberBetween((mcInitialProperties.EndSizeMin > 0f) ? mcInitialProperties.EndSizeMin : mcInitialProperties.EndHeightMin, (mcInitialProperties.EndSizeMax > 0f) ? mcInitialProperties.EndSizeMax : mcInitialProperties.EndHeightMax);
			defaultQuadParticle.Width = defaultQuadParticle.StartWidth;
			defaultQuadParticle.Height = defaultQuadParticle.StartHeight;
		}

		/// <summary>
		/// Update a Particle's Rotation according to its Rotational Velocity
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleRotationUsingRotationalVelocity(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			if (cParticle.RotationalVelocity != Vector3.Zero)
			{
				cParticle.Orientation.Normalize();
				Quaternion quaternion = new Quaternion(cParticle.RotationalVelocity * (fElapsedTimeInSeconds * 0.5f), 0f);
				cParticle.Orientation += cParticle.Orientation * quaternion;
			}
		}

		/// <summary>
		/// Update a Particle's Rotational Velocity according to its Rotational Acceleration
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleRotationalVelocityUsingRotationalAcceleration(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			if (cParticle.RotationalAcceleration != Vector3.Zero)
			{
				cParticle.RotationalVelocity += cParticle.RotationalAcceleration * fElapsedTimeInSeconds;
			}
		}

		/// <summary>
		/// Update a Particle's Rotation and Rotational Velocity according to its Rotational Acceleration
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleRotationAndRotationalVelocityUsingRotationalAcceleration(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			UpdateParticleRotationalVelocityUsingRotationalAcceleration(cParticle, fElapsedTimeInSeconds);
			UpdateParticleRotationUsingRotationalVelocity(cParticle, fElapsedTimeInSeconds);
		}

		/// <summary>
		/// Linearly interpolate the Particle's Width between the Start and End Width according
		/// to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleWidthUsingLerp(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Width = MathHelper.Lerp(cParticle.StartWidth, cParticle.EndWidth, cParticle.NormalizedElapsedTime);
		}

		/// <summary>
		/// Linearly interpolate the Particle's Height between the Start and End Height according
		/// to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleHeightUsingLerp(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Height = MathHelper.Lerp(cParticle.StartHeight, cParticle.EndHeight, cParticle.NormalizedElapsedTime);
		}

		/// <summary>
		/// Linearly interpolate the Particle's Width and Height between the Start and End values according
		/// to the Particle's Normalized Lifetime
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleWidthAndHeightUsingLerp(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Width = MathHelper.Lerp(cParticle.StartWidth, cParticle.EndWidth, cParticle.NormalizedElapsedTime);
			cParticle.Height = MathHelper.Lerp(cParticle.StartHeight, cParticle.EndHeight, cParticle.NormalizedElapsedTime);
		}

		protected void UpdateParticleToFaceTheCamera(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Normal = CameraPosition - cParticle.Position;
		}

		/// <summary>
		/// Orientates the Particle to face the camera, but constrains the particle to always be perpendicular to the 
		/// Y-Z plane.
		/// </summary>
		/// <param name="cParticle">The Particle to update.</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update.</param>
		protected void UpdateParticleToBeConstrainedAroundXAxis(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			Vector3 normal = CameraPosition - cParticle.Position;
			normal.X = 0f;
			cParticle.Normal = normal;
		}

		/// <summary>
		/// Orientates the Particle to face the camera, but constrains the particle to always be perpendicular to the 
		/// X-Z plane (i.e standing straight up).
		/// </summary>
		/// <param name="cParticle">The Particle to update.</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update.</param>
		protected void UpdateParticleToBeConstrainedAroundYAxis(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			Vector3 normal = CameraPosition - cParticle.Position;
			normal.Y = 0f;
			cParticle.Normal = normal;
		}

		/// <summary>
		/// Orientates the Particle to face the camera, but constrains the particle to always be perpendicular to the 
		/// X-Y plane.
		/// </summary>
		/// <param name="cParticle">The Particle to update.</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update.</param>
		protected void UpdateParticleToBeConstrainedAroundZAxis(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			Vector3 normal = CameraPosition - cParticle.Position;
			normal.Z = 0f;
			cParticle.Normal = normal;
		}

		/// <summary>
		/// Updates the Particle's DistanceFromCameraSquared property to reflect how far this Particle is from the Camera.
		/// </summary>
		/// <param name="cParticle">The Particle to update.</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update.</param>
		protected void UpdateParticleDistanceFromCameraSquared(DefaultQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.DistanceFromCameraSquared = Vector3.DistanceSquared(CameraPosition, cParticle.Position);
		}

		/// <summary>
		/// Sorts the particles to draw particles furthest from the camera first, in order to achieve proper depth perspective.
		///
		/// <para>NOTE: This operation is very expensive and should only be used when you are
		/// drawing particles with both opaque and semi-transparent portions, and not using additive blending.</para>
		/// <para>Merge Sort is the sorting algorithm used, as it tends to be best for linked lists.
		/// TODO - WHILE MERGE SORT SHOULD BE USED, DUE TO TIME CONSTRAINTS A (PROBABLY) SLOWER METHOD (QUICK-SORT)
		/// IS BEING USED INSTEAD. THIS FUNCTION NEEDS TO BE UPDATED TO USE MERGE SORT STILL.
		/// THE LINKED LIST MERGE SORT ALGORITHM CAN BE FOUND AT http://www.chiark.greenend.org.uk/~sgtatham/algorithms/listsort.html</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleSystemToSortParticlesByDistanceFromCamera(float fElapsedTimeInSeconds)
		{
			int count = ActiveParticles.Count;
			if (count > 1)
			{
				List<Particle> list = new List<Particle>(count);
				for (LinkedListNode<Particle> linkedListNode = ActiveParticles.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					list.Add(linkedListNode.Value);
				}
				list.Sort((Particle Particle1, Particle Particle2) =>
				{
					DefaultQuadParticle defaultQuadParticle = (DefaultQuadParticle)(object)Particle1;
					DefaultQuadParticle defaultQuadParticle2 = (DefaultQuadParticle)(object)Particle2;
					return defaultQuadParticle.DistanceFromCameraSquared.CompareTo(defaultQuadParticle2.DistanceFromCameraSquared);
				});
				ActiveParticles.Clear();
				for (int num = 0; num < count; num++)
				{
					ActiveParticles.AddFirst(list[num]);
				}
			}
		}
	}
	/// <summary>
	/// The Default Textured Quad Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultTexturedQuadParticleSystem<Particle, Vertex> : DPSFDefaultQuadParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultTexturedQuadParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to update the Vertex properties according to the Particle properties
		/// </summary>
		/// <param name="sVertexBuffer">The array containing the Vertices to be drawn</param>
		/// <param name="iIndex">The Index in the array where the Particle's Vertex info should be placed</param>
		/// <param name="Particle">The Particle to copy the information from</param>
		protected virtual void UpdateVertexProperties(ref DefaultTexturedQuadParticleVertex[] sVertexBuffer, int iIndex, DPSFParticle Particle)
		{
			DefaultTexturedQuadParticle defaultTexturedQuadParticle = (DefaultTexturedQuadParticle)Particle;
			float num = defaultTexturedQuadParticle.Width / 2f;
			float num2 = defaultTexturedQuadParticle.Height / 2f;
			Vector3 value = new Vector3(0f - num, 0f - num2, 0f);
			Vector3 value2 = new Vector3(num, 0f - num2, 0f);
			Vector3 value3 = new Vector3(0f - num, num2, 0f);
			Vector3 value4 = new Vector3(num, num2, 0f);
			value = Vector3.Transform(value, defaultTexturedQuadParticle.Orientation) + defaultTexturedQuadParticle.Position;
			value2 = Vector3.Transform(value2, defaultTexturedQuadParticle.Orientation) + defaultTexturedQuadParticle.Position;
			value3 = Vector3.Transform(value3, defaultTexturedQuadParticle.Orientation) + defaultTexturedQuadParticle.Position;
			value4 = Vector3.Transform(value4, defaultTexturedQuadParticle.Orientation) + defaultTexturedQuadParticle.Position;
			Color colorAsPremultiplied = defaultTexturedQuadParticle.ColorAsPremultiplied;
			sVertexBuffer[iIndex].Position = value3;
			sVertexBuffer[iIndex].TextureCoordinate = new Vector2(0f, 1f);
			sVertexBuffer[iIndex].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 1].Position = value;
			sVertexBuffer[iIndex + 1].TextureCoordinate = new Vector2(0f, 0f);
			sVertexBuffer[iIndex + 1].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 2].Position = value4;
			sVertexBuffer[iIndex + 2].TextureCoordinate = new Vector2(1f, 1f);
			sVertexBuffer[iIndex + 2].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 3].Position = value2;
			sVertexBuffer[iIndex + 3].TextureCoordinate = new Vector2(1f, 0f);
			sVertexBuffer[iIndex + 3].Color = colorAsPremultiplied;
			if (GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef)
			{
				IndexBuffer[IndexBufferIndex++] = iIndex + 1;
				IndexBuffer[IndexBufferIndex++] = iIndex + 2;
				IndexBuffer[IndexBufferIndex++] = iIndex;
				IndexBuffer[IndexBufferIndex++] = iIndex + 1;
				IndexBuffer[IndexBufferIndex++] = iIndex + 3;
				IndexBuffer[IndexBufferIndex++] = iIndex + 2;
			}
			else
			{
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 1);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 2);
				IndexBufferReach[IndexBufferIndex++] = (short)iIndex;
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 1);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 3);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 2);
			}
		}

		/// <summary>
		/// Virtual function to Set the Effect's Parameters before drawing the Particles
		/// </summary>
		protected override void SetEffectParameters()
		{
			if (Effect is AlphaTestEffect alphaTestEffect)
			{
				alphaTestEffect.World = World;
				alphaTestEffect.View = View;
				alphaTestEffect.Projection = Projection;
				alphaTestEffect.Texture = Texture;
				alphaTestEffect.VertexColorEnabled = true;
			}
		}
	}
	/// <summary>
	/// The Default Textured Quad with Texture Coordinates Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultTexturedQuadTextureCoordinatesParticleSystem<Particle, Vertex> : DPSFDefaultTexturedQuadParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultTexturedQuadTextureCoordinatesParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to update the Vertex properties according to the Particle properties
		/// </summary>
		/// <param name="sVertexBuffer">The array containing the Vertices to be drawn</param>
		/// <param name="iIndex">The Index in the array where the Particle's Vertex info should be placed</param>
		/// <param name="Particle">The Particle to copy the information from</param>
		protected override void UpdateVertexProperties(ref DefaultTexturedQuadParticleVertex[] sVertexBuffer, int iIndex, DPSFParticle Particle)
		{
			DefaultTextureQuadTextureCoordinatesParticle defaultTextureQuadTextureCoordinatesParticle = (DefaultTextureQuadTextureCoordinatesParticle)Particle;
			float num = defaultTextureQuadTextureCoordinatesParticle.Width / 2f;
			float num2 = defaultTextureQuadTextureCoordinatesParticle.Height / 2f;
			Vector3 value = new Vector3(0f - num, num2, 0f);
			Vector3 value2 = new Vector3(num, num2, 0f);
			Vector3 value3 = new Vector3(0f - num, 0f - num2, 0f);
			Vector3 value4 = new Vector3(num, 0f - num2, 0f);
			value = Vector3.Transform(value, defaultTextureQuadTextureCoordinatesParticle.Orientation) + defaultTextureQuadTextureCoordinatesParticle.Position;
			value2 = Vector3.Transform(value2, defaultTextureQuadTextureCoordinatesParticle.Orientation) + defaultTextureQuadTextureCoordinatesParticle.Position;
			value3 = Vector3.Transform(value3, defaultTextureQuadTextureCoordinatesParticle.Orientation) + defaultTextureQuadTextureCoordinatesParticle.Position;
			value4 = Vector3.Transform(value4, defaultTextureQuadTextureCoordinatesParticle.Orientation) + defaultTextureQuadTextureCoordinatesParticle.Position;
			Color colorAsPremultiplied = defaultTextureQuadTextureCoordinatesParticle.ColorAsPremultiplied;
			sVertexBuffer[iIndex].Position = value3;
			sVertexBuffer[iIndex].TextureCoordinate = new Vector2(defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateLeftTop.X, defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateRightBottom.Y);
			sVertexBuffer[iIndex].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 1].Position = value;
			sVertexBuffer[iIndex + 1].TextureCoordinate = defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateLeftTop;
			sVertexBuffer[iIndex + 1].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 2].Position = value4;
			sVertexBuffer[iIndex + 2].TextureCoordinate = defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateRightBottom;
			sVertexBuffer[iIndex + 2].Color = colorAsPremultiplied;
			sVertexBuffer[iIndex + 3].Position = value2;
			sVertexBuffer[iIndex + 3].TextureCoordinate = new Vector2(defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateRightBottom.X, defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateLeftTop.Y);
			sVertexBuffer[iIndex + 3].Color = colorAsPremultiplied;
			if (GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef)
			{
				IndexBuffer[IndexBufferIndex++] = iIndex;
				IndexBuffer[IndexBufferIndex++] = iIndex + 2;
				IndexBuffer[IndexBufferIndex++] = iIndex + 1;
				IndexBuffer[IndexBufferIndex++] = iIndex + 2;
				IndexBuffer[IndexBufferIndex++] = iIndex + 3;
				IndexBuffer[IndexBufferIndex++] = iIndex + 1;
			}
			else
			{
				IndexBufferReach[IndexBufferIndex++] = (short)iIndex;
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 2);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 1);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 2);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 3);
				IndexBufferReach[IndexBufferIndex++] = (short)(iIndex + 1);
			}
		}
	}
	/// <summary>
	/// The Default Textured Quad with Texture Coordinates Particle System to inherit from, which uses Default Textured Quad Texture Coordinates Particles
	/// </summary>
	public class DefaultTexturedQuadTextureCoordinatesParticleSystem : DPSFDefaultTexturedQuadTextureCoordinatesParticleSystem<DefaultTextureQuadTextureCoordinatesParticle, DefaultTexturedQuadParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultTexturedQuadTextureCoordinatesParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Quad Particle System
	/// </summary>
	public class DefaultQuadParticle : DPSFDefaultBaseParticle
	{
		/// <summary>
		/// The Orientation of the Particle
		/// </summary>
		public Quaternion Orientation;

		/// <summary>
		/// The Rotational Velocity of the Particle.
		/// X = Pitch Velocity, Y = Yaw Velocity, Z = Roll Velocity in radians
		/// </summary>
		public Vector3 RotationalVelocity;

		/// <summary>
		/// The Rotational Acceleration of the Particle.
		/// X = Pitch Acceleration, Y = Yaw Acceleration, Z = Roll Acceleration in radians
		/// </summary>
		public Vector3 RotationalAcceleration;

		/// <summary>
		/// The Width of the Particle
		/// </summary>
		public float Width;

		/// <summary>
		/// The Height of the Particle
		/// </summary>
		public float Height;

		/// <summary>
		/// The Width of the Particle when it is born
		/// </summary>
		public float StartWidth;

		/// <summary>
		/// The Height of the Particle when it is born
		/// </summary>
		public float StartHeight;

		/// <summary>
		/// The Width of the Particle when it dies
		/// </summary>
		public float EndWidth;

		/// <summary>
		/// The Height of the Particle when it dies
		/// </summary>
		public float EndHeight;

		/// <summary>
		/// The squared distance between this particle and the camera.
		/// <para>NOTE: This property is only used if you are sorting the particles based on their distance 
		/// from the camera, otherwise you can use this property for whatever you like.</para>
		/// </summary>
		public float DistanceFromCameraSquared;

		/// <summary>
		/// Sets the Width and Height properties to the given value.
		/// Gets the Width value, ignoring whether the Height value is the same or not.
		/// </summary>
		public float Size
		{
			get
			{
				return Width;
			}
			set
			{
				Width = value;
				Height = value;
			}
		}

		/// <summary>
		/// Sets the StartWidth and StartHeight properties to the given value.
		/// Gets the StartWidth value, ignoring whether the StartHeight value is the same or not.
		/// </summary>
		public float StartSize
		{
			get
			{
				return StartWidth;
			}
			set
			{
				StartWidth = value;
				StartHeight = value;
			}
		}

		/// <summary>
		/// Sets the EndWidth and EndHeight properties to the given value.
		/// Gets the EndWidth value, ignoring whether the EndHeight value is the same or not.
		/// </summary>
		public float EndSize
		{
			get
			{
				return EndWidth;
			}
			set
			{
				EndWidth = value;
				EndHeight = value;
			}
		}

		/// <summary>
		/// Get / Set the Normal (forward) direction of the Particle (i.e. which direction it is facing)
		/// </summary>
		public Vector3 Normal
		{
			get
			{
				return Orientation3D.GetNormalDirection(Orientation);
			}
			set
			{
				Orientation3D.SetNormalDirection(ref Orientation, value);
			}
		}

		/// <summary>
		/// Get / Set the Up direction of the Particle
		/// </summary>
		public Vector3 Up
		{
			get
			{
				return Orientation3D.GetUpDirection(Orientation);
			}
			set
			{
				Orientation3D.SetUpDirection(ref Orientation, value);
			}
		}

		/// <summary>
		/// Get / Set the Right direction of the Particle
		/// </summary>
		public Vector3 Right
		{
			get
			{
				return Orientation3D.GetRightDirection(Orientation);
			}
			set
			{
				Orientation3D.SetRightDirection(ref Orientation, value);
			}
		}

		/// <summary>
		/// Scales the Width and Height by the given amount.
		/// </summary>
		/// <param name="scale">The amount to scale the Width and Height by.</param>
		public void Scale(float scale)
		{
			Width *= scale;
			Height *= scale;
		}

		/// <summary>
		/// Updates the Width to the given value and uniformly scales the Height to maintain the width-to-height ratio.
		/// </summary>
		/// <param name="newWidth">The Width the particle should have.</param>
		public void ScaleToWidth(float newWidth)
		{
			if (Width != 0f)
			{
				float num = newWidth / Width;
				Height *= num;
				Width = newWidth;
			}
		}

		/// <summary>
		/// Updates the Height to the given value and uniformly scales the Width to maintain the width-to-height ratio.
		/// </summary>
		/// <param name="newHeight">The Height the particle should have.</param>
		public void ScaleToHeight(float newHeight)
		{
			if (Height != 0f)
			{
				float num = newHeight / Height;
				Width *= num;
				Height = newHeight;
			}
		}

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			Orientation = Quaternion.Identity;
			RotationalVelocity = (RotationalAcceleration = Vector3.Zero);
			Width = (Height = 10f);
			StartWidth = (StartHeight = (EndWidth = (EndHeight = 10f)));
			DistanceFromCameraSquared = 0f;
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultQuadParticle defaultQuadParticle = (DefaultQuadParticle)ParticleToCopy;
			base.CopyFrom(defaultQuadParticle);
			Orientation = defaultQuadParticle.Orientation;
			RotationalVelocity = defaultQuadParticle.RotationalVelocity;
			RotationalAcceleration = defaultQuadParticle.RotationalAcceleration;
			Width = defaultQuadParticle.Width;
			Height = defaultQuadParticle.Height;
			StartHeight = defaultQuadParticle.StartHeight;
			StartWidth = defaultQuadParticle.StartWidth;
			EndHeight = defaultQuadParticle.EndHeight;
			EndWidth = defaultQuadParticle.EndWidth;
			DistanceFromCameraSquared = defaultQuadParticle.DistanceFromCameraSquared;
		}
	}
	/// <summary>
	/// Particle used by the Default Textured Quad Particle System
	/// </summary>
	public class DefaultTexturedQuadParticle : DefaultQuadParticle
	{
	}
	/// <summary>
	/// Particle used by the Default Textured Quad with Texture Coordinates Particle System
	/// </summary>
	public class DefaultTextureQuadTextureCoordinatesParticle : DefaultTexturedQuadParticle
	{
		/// <summary>
		/// The Normalized (0.0 - 1.0) Top-Left Texture Coordinate to use for the Particle's image
		/// </summary>
		public Vector2 NormalizedTextureCoordinateLeftTop;

		/// <summary>
		/// The Normalized (0.0 - 1.0) Bottom-Right Texture Coordinate to use for the Particle's image
		/// </summary>
		public Vector2 NormalizedTextureCoordinateRightBottom;

		/// <summary>
		/// Sets the Normalized Texture Coordinates using the absolute (i.e. non-normalized) top-left coordinate and the dimensions of the Picture in the Texture
		/// </summary>
		/// <param name="sTextureCoordinates">The top-left Position and the Dimensions of the Picture in the Texture</param>
		/// <param name="iTextureWidth">The Width of the Texture that the Picture is in</param>
		/// <param name="iTextureHeight">The Height of the Texture that the Picture is in</param>
		public void SetTextureCoordinates(Rectangle sTextureCoordinates, int iTextureWidth, int iTextureHeight)
		{
			SetTextureCoordinates(sTextureCoordinates.Left, sTextureCoordinates.Top, sTextureCoordinates.Right, sTextureCoordinates.Bottom, iTextureWidth, iTextureHeight);
		}

		/// <summary>
		/// Sets the Normalized Texture Coordinates using the absolute (i.e. non-normalized) coordinates of the Picture in the Texture
		/// </summary>
		/// <param name="iLeft">The X position of the top-left corner of the Picture in the Texture</param>
		/// <param name="iTop">The Y position of the top-left corner of the Picture in the Texture</param>
		/// <param name="iRight">The X position of the bottom-right corner of the Picture in the Texture</param>
		/// <param name="iBottom">The Y position of the bottom-right corner of the Picture in the Texture</param>
		/// <param name="iTextureWidth">The Width of the Texture that the Picture is in</param>
		/// <param name="iTextureHeight">The Height of the Texture that the Picture is in</param>
		public void SetTextureCoordinates(int iLeft, int iTop, int iRight, int iBottom, int iTextureWidth, int iTextureHeight)
		{
			NormalizedTextureCoordinateLeftTop.X = (float)iLeft / (float)iTextureWidth;
			NormalizedTextureCoordinateLeftTop.Y = (float)iTop / (float)iTextureHeight;
			NormalizedTextureCoordinateRightBottom.X = (float)iRight / (float)iTextureHeight;
			NormalizedTextureCoordinateRightBottom.Y = (float)iBottom / (float)iTextureHeight;
		}

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			NormalizedTextureCoordinateLeftTop = new Vector2(0f, 0f);
			NormalizedTextureCoordinateRightBottom = new Vector2(1f, 1f);
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultTextureQuadTextureCoordinatesParticle defaultTextureQuadTextureCoordinatesParticle = (DefaultTextureQuadTextureCoordinatesParticle)ParticleToCopy;
			base.CopyFrom(defaultTextureQuadTextureCoordinatesParticle);
			NormalizedTextureCoordinateLeftTop = defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateLeftTop;
			NormalizedTextureCoordinateRightBottom = defaultTextureQuadTextureCoordinatesParticle.NormalizedTextureCoordinateRightBottom;
		}
	}
	/// <summary>
	/// The Default Sprite Particle System to inherit from, which uses Default Sprite Particles
	/// </summary>
	public class DefaultSpriteParticleSystem : DPSFDefaultSpriteParticleSystem<DefaultSpriteParticle, DefaultSpriteParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultSpriteParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Interface that must be implemented by all Particle Vertex's
	/// </summary>
	public interface IDPSFParticleVertex
	{
		/// <summary>
		/// An array describing the Elements of each Vertex
		/// </summary>
		VertexElement[] VertexElements { get; }

		/// <summary>
		/// The Size of one Vertex Element in Bytes
		/// </summary>
		int SizeInBytes { get; }
	}
	/// <summary>
	/// Dummy structure used for the vertices of Default Sprites. Since Sprites are drawn using a 
	/// SpriteBatch, they do not have vertices, so this structure is empty.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct DefaultSpriteParticleVertex : IDPSFParticleVertex
	{
		/// <summary>
		/// An array describing the attributes of each Vertex
		/// </summary>
		public VertexElement[] VertexElements => null;

		/// <summary>
		/// The Size of one Vertex in Bytes
		/// </summary>
		public int SizeInBytes => 0;
	}
	/// <summary>
	/// Class to manage the Updating and Drawing of DPSF Particle Systems each frame
	/// </summary>
	public class ParticleSystemManager
	{
		private List<IDPSFParticleSystem> mcParticleSystemListSortedByUpdateOrder = new List<IDPSFParticleSystem>();

		private List<IDPSFParticleSystem> mcParticleSystemListSortedByDrawOrder = new List<IDPSFParticleSystem>();

		private bool mbPerformUpdates = true;

		private bool mbPerfomDraws = true;

		private bool mbAParticleSystemsUpdateOrderWasChanged;

		private bool mbAParticleSystemsDrawOrderWasChanged;

		private float mfSimulationSpeed = 1f;

		private bool mbUseManagersSimulationSpeed = true;

		private int miUpdatesPerSecond;

		private bool mbUseManagersUpdatesPerSecond = true;

		/// <summary>
		/// Handle to the particle system whose Update() function is currently being called.
		/// We need this in case a PS removes itself from the Manager during its Update() function.
		/// </summary>
		private IDPSFParticleSystem _particleSystemBeingUpdated;

		private bool _isParticleSystemBeingUpdatedRemovedFromManager;

		/// <summary>
		/// Get if the Particle Systems are inheriting from DrawableGameComponent or not
		/// </summary>
		public bool ParticleSystemsInheritDrawableGameComponent => false;

		/// <summary>
		/// Get / Set if the Particle Systems should be Updated or not
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mbPerformUpdates;
			}
			set
			{
				mbPerformUpdates = true;
			}
		}

		/// <summary>
		/// Get / Set if this Particle Systems should be drawn or not.
		/// <para>NOTE: Setting this to false causes the particle systems' Draw() function to not be called, including the 
		/// particle systems' BeforeDraw() and AfterDraw() functions.</para>
		/// </summary>
		public bool Visible
		{
			get
			{
				return mbPerfomDraws;
			}
			set
			{
				mbPerfomDraws = value;
			}
		}

		/// <summary>
		/// Get / Set if the Particle System Manager's SimulationSpeed property
		/// should be used for each of the particle systems it contains or not.
		/// <para>Default value is true.</para>
		/// </summary>
		public bool SimulationSpeedIsEnabled
		{
			get
			{
				return mbUseManagersSimulationSpeed;
			}
			set
			{
				mbUseManagersSimulationSpeed = value;
				if (mbUseManagersSimulationSpeed)
				{
					SimulationSpeed = SimulationSpeed;
				}
			}
		}

		/// <summary>
		/// Get / Set if the Particle System Manager's UpdatesPerSecond property
		/// should be used for each of the particle systems it contains or not.
		/// <para>Default value is true.</para>
		/// </summary>
		public bool UpdatesPerSecondIsEnabled
		{
			get
			{
				return mbUseManagersUpdatesPerSecond;
			}
			set
			{
				mbUseManagersUpdatesPerSecond = value;
				if (mbUseManagersUpdatesPerSecond)
				{
					UpdatesPerSecond = UpdatesPerSecond;
				}
			}
		}

		/// <summary>
		/// Get / Set how fast the Particle System Simulations should run. 
		/// <para>Example: 1.0 = normal speed, 0.5 = half speed, 2.0 = double speed.</para>
		/// <para>NOTE: This sets the SimulationSpeed property of each individual Particle
		/// System in this Manager to the given value. It will also set a particle system's
		/// Simulation Speed when the particle system is re-initialized, 
		/// and when a new Particle System is added to the Manager in the future.</para>
		/// <para>NOTE: Setting this property only has an effect if the SimulationSpeedIsEnabled property is true.</para>
		/// <para>NOTE: This will be set to zero if a negative value is specified.</para>
		/// </summary>
		public float SimulationSpeed
		{
			get
			{
				return mfSimulationSpeed;
			}
			set
			{
				if (value < 0f)
				{
					mfSimulationSpeed = 0f;
				}
				else
				{
					mfSimulationSpeed = value;
				}
				if (mbUseManagersSimulationSpeed)
				{
					SetSimulationSpeedForAllParticleSystems(mfSimulationSpeed);
				}
			}
		}

		/// <summary>
		/// Get / Set how often the Particle Systems should be Updated. 
		/// <para>NOTE: This sets the UpdatesPerSecond property of each individual Particle
		/// System in this Manager to the given value. It will also set a particle system's
		/// Updates Per Second when the particle system is re-initialized, 
		/// and when a new Particle System is added to the Manager in the future.</para>
		/// <para>NOTE: Setting this property only has an effect if the UpdatesPerSecondIsEnabled property is true.</para>
		/// <para>NOTE: A value of zero means update the particle systems every time Update() is called.</para>
		/// <para>NOTE: This will be set to zero if a negative value is specified.</para>
		/// </summary>
		public int UpdatesPerSecond
		{
			get
			{
				return miUpdatesPerSecond;
			}
			set
			{
				if (value < 0)
				{
					miUpdatesPerSecond = 0;
				}
				else
				{
					miUpdatesPerSecond = value;
				}
				if (mbUseManagersUpdatesPerSecond)
				{
					SetUpdatesPerSecondForAllParticleSystems(miUpdatesPerSecond);
				}
			}
		}

		/// <summary>
		/// Get the cumulative Number Of Active Particles of all Particle Systems in this Manager
		/// </summary>
		public int TotalNumberOfActiveParticles
		{
			get
			{
				int num = 0;
				int count = mcParticleSystemListSortedByUpdateOrder.Count;
				for (int i = 0; i < count; i++)
				{
					num += mcParticleSystemListSortedByUpdateOrder[i].NumberOfActiveParticles;
				}
				return num;
			}
		}

		/// <summary>
		/// Get the cumulative Number Of Particles Being Drawn by all Particle Systems in this Manager.
		/// This is the total number of Active AND Visible Particles.
		/// <para>NOTE: This ignores whether the Manager is Visible or not.</para>
		/// </summary>
		public int TotalNumberOfParticlesBeingDrawn
		{
			get
			{
				int num = 0;
				int count = mcParticleSystemListSortedByUpdateOrder.Count;
				for (int i = 0; i < count; i++)
				{
					num += mcParticleSystemListSortedByUpdateOrder[i].NumberOfParticlesBeingDrawn;
				}
				return num;
			}
		}

		/// <summary>
		/// Get the cumulative Max Number Of Particles allocated in memory by all Particle Systems in the Manager.
		/// </summary>
		public int TotalNumberOfParticlesAllocatedInMemory
		{
			get
			{
				int num = 0;
				int count = mcParticleSystemListSortedByUpdateOrder.Count;
				for (int i = 0; i < count; i++)
				{
					num += mcParticleSystemListSortedByUpdateOrder[i].NumberOfParticlesAllocatedInMemory;
				}
				return num;
			}
		}

		/// <summary>
		/// Gets the cumulative time (in milliseconds) it took to perform the Update() function on each particle system in this manager.
		/// <para>Note: Only particle systems that have their PerformanceProfilingIsEnabled property set to true will be included in this total.</para>
		/// </summary>
		public double TotalPerformanceTimeToDoUpdatesInMilliseconds { get; private set; }

		/// <summary>
		/// Gets the cumulative time (in milliseconds) it took to perform the Draw() function on each particle system in this manager.
		/// <para>Note: Only particle systems that have their PerformanceProfilingIsEnabled property set to true will be included in this total.</para>
		/// </summary>
		public double TotalPerformanceTimeToDoDrawsInMilliseconds { get; private set; }

		/// <summary>
		/// Returns a Linked List of handles to the Particle Systems in this Manager
		/// </summary>
		public List<IDPSFParticleSystem> ParticleSystems => mcParticleSystemListSortedByUpdateOrder;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.ParticleSystemManager" /> class.
		/// </summary>
		public ParticleSystemManager()
		{
			UpdatesPerSecond = DPSFDefaultSettings.UpdatesPerSecond;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.ParticleSystemManager" /> class, copying the settings of the given Particle System Manager.
		/// </summary>
		/// <param name="managerToCopy">The Particle System Manager to copy from.</param>
		public ParticleSystemManager(ParticleSystemManager managerToCopy)
			: this()
		{
			CopyFrom(managerToCopy);
		}

		/// <summary>
		/// Copies the given DPSF Particle System Manager's information into this Manager.
		/// </summary>
		/// <param name="cManagerToCopy">The Particle System Manager to copy from.</param>
		public void CopyFrom(ParticleSystemManager cManagerToCopy)
		{
			mbPerformUpdates = cManagerToCopy.mbPerformUpdates;
			mbPerfomDraws = cManagerToCopy.mbPerfomDraws;
			mbAParticleSystemsUpdateOrderWasChanged = cManagerToCopy.mbAParticleSystemsUpdateOrderWasChanged;
			mbAParticleSystemsDrawOrderWasChanged = cManagerToCopy.mbAParticleSystemsDrawOrderWasChanged;
			mfSimulationSpeed = cManagerToCopy.mfSimulationSpeed;
			mbUseManagersSimulationSpeed = cManagerToCopy.mbUseManagersSimulationSpeed;
			miUpdatesPerSecond = cManagerToCopy.miUpdatesPerSecond;
			mbUseManagersUpdatesPerSecond = cManagerToCopy.mbUseManagersUpdatesPerSecond;
			int count = cManagerToCopy.ParticleSystems.Count;
			for (int i = 0; i < count; i++)
			{
				AddParticleSystem(cManagerToCopy.ParticleSystems[i]);
			}
		}

		/// <summary>
		/// Sets each individual Particle Systems' Simulation Speed to the specified Simulation Speed.
		/// </summary>
		/// <param name="fSimulationSpeed">The new Simulation Speed that all Particle Systems 
		/// currently in this Manager should have</param>
		public void SetSimulationSpeedForAllParticleSystems(float fSimulationSpeed)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.SimulationSpeed = fSimulationSpeed;
				}
			}
		}

		/// <summary>
		/// Sets each individual Particle Systems' Updates Per Second to the specified Updates Per Second.
		/// </summary>
		/// <param name="iUpdatesPerSecond">The new Updates Per Second that all particle systems
		/// currently in this Manager should have</param>
		public void SetUpdatesPerSecondForAllParticleSystems(int iUpdatesPerSecond)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.UpdatesPerSecond = iUpdatesPerSecond;
				}
			}
		}

		/// <summary>
		/// Sets the PerformanceProfilingIsEnabled property of all particle systems in this manager to the given value.
		/// </summary>
		/// <param name="performanceProfilingIsEnabled">Set if Performance Profiling should be enabled or not.</param>
		public void SetPerformanceProfilingIsEnabledForAllParticleSystems(bool performanceProfilingIsEnabled)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.PerformanceProfilingIsEnabled = performanceProfilingIsEnabled;
				}
			}
		}

		/// <summary>
		/// Sets the World, View, and Projection Matrices for all of the Particle Systems in this Manager.
		/// <para>NOTE: Sprite particle systems are not affected by the World, View, and Projection matrices.</para>
		/// </summary>
		/// <param name="cWorld">The World Matrix</param>
		/// <param name="cView">The View Matrix</param>
		/// <param name="cProjection">The Projection Matrix</param>
		public void SetWorldViewProjectionMatricesForAllParticleSystems(Matrix cWorld, Matrix cView, Matrix cProjection)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.SetWorldViewProjectionMatrices(cWorld, cView, cProjection);
				}
			}
		}

		/// <summary>
		/// Sets the SpriteBatchSettings.TransformationMatrix for all Sprite Particle Systems in this Manager.
		/// </summary>
		/// <param name="sTransformationMatrix">The Transformation Matrix to apply to the Sprite Particle Systems</param>
		public void SetTransformationMatrixForAllSpriteParticleSystems(Matrix sTransformationMatrix)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.ParticleType == ParticleTypes.Sprite && iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.SpriteBatchSettings.TransformationMatrix = sTransformationMatrix;
				}
			}
		}

		/// <summary>
		/// Sets the CameraPosition property of all particle systems in this manager to the given Camera Position.
		/// This is done by calling the particle system's virtual SetCameraPosition() function.
		/// </summary>
		/// <param name="cameraPosition">The current position of the Camera.</param>
		public void SetCameraPositionForAllParticleSystems(Vector3 cameraPosition)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.SetCameraPosition(cameraPosition);
				}
			}
		}

		/// <summary>
		/// Returns true if the given Particle System is in the Manager, false if not.
		/// </summary>
		/// <param name="cParticleSystemToFind">The Particle System to look for</param>
		/// <returns>Returns true if the given Particle System is in the Manager, false if not.</returns>
		public bool ContainsParticleSystem(IDPSFParticleSystem cParticleSystemToFind)
		{
			return mcParticleSystemListSortedByUpdateOrder.Contains(cParticleSystemToFind);
		}

		/// <summary>
		/// Returns true if the Particle System with the given ID is in the Manager, false if not.
		/// </summary>
		/// <param name="iIDOfParticleSystemToFind">The ID of the Particle System to find</param>
		/// <returns>Returns true if the Particle System with the given ID is in the Manager, false if not.</returns>
		public bool ContainsParticleSystem(int iIDOfParticleSystemToFind)
		{
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				if (mcParticleSystemListSortedByUpdateOrder[i].ID == iIDOfParticleSystemToFind)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Add an initialized Particle System to the Particle System Manager.
		/// <para>NOTE: This sets the Particle System's ParticleSystemManagerToCopyPropertiesFrom
		/// property to this Particle System Manager.</para>
		/// </summary>
		/// <param name="cParticleSystemToAdd">The initialized Particle System to add</param>
		public void AddParticleSystem(IDPSFParticleSystem cParticleSystemToAdd)
		{
			if (cParticleSystemToAdd == null)
			{
				throw new ArgumentNullException("cParticleSystemToAdd", "A particle system with a value of null cannot be added to the particle system manager.");
			}
			mcParticleSystemListSortedByUpdateOrder.Add(cParticleSystemToAdd);
			mcParticleSystemListSortedByDrawOrder.Add(cParticleSystemToAdd);
			cParticleSystemToAdd.UpdateOrderChanged += ParticleSystem_UpdateOrderChanged;
			cParticleSystemToAdd.DrawOrderChanged += ParticleSystem_DrawOrderChanged;
			cParticleSystemToAdd.ParticleSystemManagerToCopyPropertiesFrom = this;
			SortParticleSystemLists();
		}

		/// <summary>
		/// Removes the specified Particle System from the Particle System Manager.
		/// Returns true if the Particle System was found and removed, false if it was not found.
		/// </summary>
		/// <param name="cParticleSystemToRemove">A handle to the Particle System to Remove</param>
		/// <returns>Returns true if the Particle System was found and removed, false if it was not found.</returns>
		public bool RemoveParticleSystem(IDPSFParticleSystem cParticleSystemToRemove)
		{
			if (cParticleSystemToRemove == null)
			{
				throw new ArgumentNullException("cParticleSystemToRemove", "A particle system with a value of null cannot be removed from the particle system manager.");
			}
			if (cParticleSystemToRemove == _particleSystemBeingUpdated)
			{
				_isParticleSystemBeingUpdatedRemovedFromManager = true;
			}
			cParticleSystemToRemove.UpdateOrderChanged -= ParticleSystem_UpdateOrderChanged;
			cParticleSystemToRemove.DrawOrderChanged -= ParticleSystem_DrawOrderChanged;
			mcParticleSystemListSortedByUpdateOrder.Remove(cParticleSystemToRemove);
			return mcParticleSystemListSortedByDrawOrder.Remove(cParticleSystemToRemove);
		}

		/// <summary>
		/// Removes the specified Particle System from the Particle System Manager.
		/// Returns true if the Particle System was found and removed, false if it was not found.
		/// </summary>
		/// <param name="iIDOfParticleSystemToRemove">The ID of the Particle System to Remove</param>
		/// <returns>Returns true if the Particle System was found and removed, false if it was not found.</returns>
		public bool RemoveParticleSystem(int iIDOfParticleSystemToRemove)
		{
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByUpdateOrder[i];
				if (iDPSFParticleSystem.ID == iIDOfParticleSystemToRemove)
				{
					return RemoveParticleSystem(iDPSFParticleSystem);
				}
			}
			return false;
		}

		/// <summary>
		/// Removes all Particle Systems from the Particle System Manager
		/// </summary>
		public void RemoveAllParticleSystems()
		{
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int num = count - 1; num >= 0; num--)
			{
				RemoveParticleSystem(mcParticleSystemListSortedByUpdateOrder[num]);
			}
			mcParticleSystemListSortedByUpdateOrder.Clear();
			mcParticleSystemListSortedByDrawOrder.Clear();
		}

		/// <summary>
		/// Calls the AutoInitialize() function for every Particle System in this Manager
		/// </summary>
		/// <param name="cGraphicsDevice">The Graphics Device that the Particle Systems should be drawn to</param>
		/// <param name="cContentManager">The Content Manager used to load Effect files and Textures</param>
		/// <param name="cSpriteBatch">The Sprite Batch that the Sprite Particle System should use to draw its particles.
		/// If this is not initializing a Sprite particle system, or you want the particle system to use its own Sprite Batch,
		/// pass in null.</param>
		public void AutoInitializeAllParticleSystems(GraphicsDevice cGraphicsDevice, ContentManager cContentManager, SpriteBatch cSpriteBatch)
		{
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				mcParticleSystemListSortedByUpdateOrder[i].AutoInitialize(cGraphicsDevice, cContentManager, cSpriteBatch);
			}
		}

		/// <summary>
		/// Calls the Destroy() function for every Particle System in this Manager
		/// </summary>
		public void DestroyAllParticleSystems()
		{
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				mcParticleSystemListSortedByUpdateOrder[i].Destroy();
			}
		}

		/// <summary>
		/// Destroys each Particle System in the Manager, then removes them from the Manager
		/// </summary>
		public void DestroyAndRemoveAllParticleSystems()
		{
			DestroyAllParticleSystems();
			RemoveAllParticleSystems();
		}

		/// <summary>
		/// Updates all of the Particle Systems.
		/// <para>NOTE: This will only Update the Particle Systems if they do not inherit from DrawableGameComponent, 
		/// since if they do they will be updated automatically by the Game object.</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The amount of Time in seconds that has passed since
		/// the last Update</param>
		public void UpdateAllParticleSystems(float fElapsedTimeInSeconds)
		{
			if (!ParticleSystemsInheritDrawableGameComponent)
			{
				UpdateAllParticleSystemsForced(fElapsedTimeInSeconds);
			}
		}

		/// <summary>
		/// Updates all of the Particle Systems.
		/// <para>NOTE: If the Particle Systems inherit from DrawableGameComponent and this is called, the Particle
		/// Systems will be updated twice each frame; once here and once when called automatically by the game object.
		/// If not inheriting from DrawableGameComponent, this function acts the same as calling UpdateAllParticleSystems().</para>
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The amount of Time in seconds that has passed since
		/// the last Update</param>
		public void UpdateAllParticleSystemsForced(float fElapsedTimeInSeconds)
		{
			if (!Enabled)
			{
				return;
			}
			if (mbAParticleSystemsUpdateOrderWasChanged)
			{
				SortParticleSystemsByUpdateOrderList();
				mbAParticleSystemsUpdateOrderWasChanged = false;
			}
			TotalPerformanceTimeToDoUpdatesInMilliseconds = 0.0;
			ResetParticleSystemBeingUpdatedVariables();
			int count = mcParticleSystemListSortedByUpdateOrder.Count;
			for (int i = 0; i < count; i++)
			{
				_particleSystemBeingUpdated = mcParticleSystemListSortedByUpdateOrder[i];
				if (_particleSystemBeingUpdated.IsInitialized)
				{
					_particleSystemBeingUpdated.Update(fElapsedTimeInSeconds);
					TotalPerformanceTimeToDoUpdatesInMilliseconds += _particleSystemBeingUpdated.PerformanceTimeToDoUpdateInMilliseconds;
					count = mcParticleSystemListSortedByUpdateOrder.Count;
					if (_isParticleSystemBeingUpdatedRemovedFromManager)
					{
						i--;
						_isParticleSystemBeingUpdatedRemovedFromManager = false;
					}
				}
			}
			ResetParticleSystemBeingUpdatedVariables();
		}

		/// <summary>
		/// Resets the variables used for determining if a PS removed itself from the PS Manager during its Update() function call.
		/// </summary>
		private void ResetParticleSystemBeingUpdatedVariables()
		{
			_particleSystemBeingUpdated = null;
			_isParticleSystemBeingUpdatedRemovedFromManager = false;
		}

		/// <summary>
		/// Draws all of the Particle Systems.
		/// <para>NOTE: This will only Draw the Particle Systems if they do not inherit from DrawableGameComponent, 
		/// since if they do they will be drawn automatically by the Game object.</para>
		/// </summary>
		public void DrawAllParticleSystems()
		{
			if (!ParticleSystemsInheritDrawableGameComponent)
			{
				DrawAllParticleSystemsForced();
			}
		}

		/// <summary>
		/// Draws all of the Particle Systems, even if they inherit from DrawableGameComponent.
		/// <para>NOTE: If the Particle Systems inherit from DrawableGameComponent and this is called, the Particle
		/// Systems will be drawn twice each frame; once here and once when called automatically by the game object.
		/// If not inheriting from DrawableGameComponent, this function acts the same as calling DrawAllParticleSystems().</para>
		/// </summary>
		public void DrawAllParticleSystemsForced()
		{
			if (!Visible)
			{
				return;
			}
			if (mbAParticleSystemsDrawOrderWasChanged)
			{
				SortParticleSystemsByDrawOrderList();
				mbAParticleSystemsDrawOrderWasChanged = false;
			}
			TotalPerformanceTimeToDoDrawsInMilliseconds = 0.0;
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByDrawOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByDrawOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					iDPSFParticleSystem.DrawForced();
					TotalPerformanceTimeToDoDrawsInMilliseconds += iDPSFParticleSystem.PerformanceTimeToDoDrawInMilliseconds;
				}
			}
		}

		/// <summary>
		/// Draws all of the Particle Systems to a Texture and returns the Texture, which has a Transparent Black background
		/// </summary>
		/// <param name="cGraphicsDevice">A Graphics Device to use for drawing; The Graphics Device contents will not be overwritten.
		/// <para>NOTE: The size of the Texture before scaling will be the size of the Graphics Device's Viewport.</para></param>
		/// <param name="iTextureWidth">The desired Width of the Texture</param>
		/// <param name="iTextureHeight">The desired Height of the Texture</param>
		/// <returns>Returns a Texture with the Particle Systems in their current state drawn on it</returns>
		public Texture2D DrawAllParticleSystemsToTexture(GraphicsDevice cGraphicsDevice, int iTextureWidth, int iTextureHeight)
		{
			Texture2D texture2D = null;
			int width = cGraphicsDevice.Viewport.Width;
			int height = cGraphicsDevice.Viewport.Height;
			RenderTarget2D graphicsDevicesCurrentRenderTarget = GetGraphicsDevicesCurrentRenderTarget(cGraphicsDevice);
			RenderTarget2D renderTarget2D = new RenderTarget2D(cGraphicsDevice, width, height);
			cGraphicsDevice.SetRenderTarget(renderTarget2D);
			cGraphicsDevice.Clear(Color.Transparent);
			IDPSFParticleSystem iDPSFParticleSystem = null;
			int count = mcParticleSystemListSortedByDrawOrder.Count;
			for (int i = 0; i < count; i++)
			{
				iDPSFParticleSystem = mcParticleSystemListSortedByDrawOrder[i];
				if (iDPSFParticleSystem.IsInitialized)
				{
					RenderTarget2D graphicsDevicesCurrentRenderTarget2 = GetGraphicsDevicesCurrentRenderTarget(iDPSFParticleSystem.GraphicsDevice);
					iDPSFParticleSystem.GraphicsDevice.SetRenderTarget(renderTarget2D);
					iDPSFParticleSystem.DrawForced();
					iDPSFParticleSystem.GraphicsDevice.SetRenderTarget(graphicsDevicesCurrentRenderTarget2);
				}
			}
			cGraphicsDevice.SetRenderTarget(graphicsDevicesCurrentRenderTarget);
			Texture2D texture2D2 = renderTarget2D;
			if (width == iTextureWidth && height == iTextureHeight)
			{
				return texture2D2;
			}
			DepthStencilState depthStencilState = cGraphicsDevice.DepthStencilState;
			RenderTarget2D renderTarget2D2 = new RenderTarget2D(cGraphicsDevice, iTextureWidth, iTextureHeight);
			cGraphicsDevice.SetRenderTarget(renderTarget2D2);
			cGraphicsDevice.DepthStencilState = DepthStencilState.Default;
			cGraphicsDevice.Clear(Color.Transparent);
			SpriteBatch spriteBatch = new SpriteBatch(cGraphicsDevice);
			spriteBatch.Begin();
			spriteBatch.Draw(texture2D2, new Rectangle(0, 0, iTextureWidth, iTextureHeight), Color.White);
			spriteBatch.End();
			cGraphicsDevice.SetRenderTarget(graphicsDevicesCurrentRenderTarget);
			cGraphicsDevice.DepthStencilState = depthStencilState;
			return renderTarget2D2;
		}

		/// <summary>
		/// Draws the given Texture to the given Tile Set Render Target at the specified Position.
		/// </summary>
		/// <param name="cGraphicsDevice">The Graphics Device used to do the drawing</param>
		/// <param name="cTileSetRenderTarget">The Tile Set Render Target to draw to</param>
		/// <param name="cTexture">The Texture to draw</param>
		/// <param name="sPositionAndDimensionsInTileSetToAddImage">The Position where the Texture should be drawn
		/// on the Tile Set Render Target, and its Dimensions</param>
		private void AddImageToTileSet(GraphicsDevice cGraphicsDevice, ref RenderTarget2D cTileSetRenderTarget, Texture2D cTexture, Rectangle sPositionAndDimensionsInTileSetToAddImage)
		{
			RenderTarget2D graphicsDevicesCurrentRenderTarget = GetGraphicsDevicesCurrentRenderTarget(cGraphicsDevice);
			DepthStencilState depthStencilState = cGraphicsDevice.DepthStencilState;
			cGraphicsDevice.SetRenderTarget(cTileSetRenderTarget);
			cGraphicsDevice.DepthStencilState = DepthStencilState.Default;
			SpriteBatch spriteBatch = new SpriteBatch(cGraphicsDevice);
			spriteBatch.Begin();
			spriteBatch.Draw(cTexture, sPositionAndDimensionsInTileSetToAddImage, Color.White);
			spriteBatch.End();
			cGraphicsDevice.SetRenderTarget(graphicsDevicesCurrentRenderTarget);
			cGraphicsDevice.DepthStencilState = depthStencilState;
		}

		/// <summary>
		/// Gets the graphics device's current render target, or null if it is not set.
		/// </summary>
		/// <param name="graphicsDevice">The graphics device.</param>
		/// <returns></returns>
		private RenderTarget2D GetGraphicsDevicesCurrentRenderTarget(GraphicsDevice graphicsDevice)
		{
			RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
			if (renderTargets.Length <= 0)
			{
				return null;
			}
			return (RenderTarget2D)renderTargets[0].RenderTarget;
		}

		/// <summary>
		/// Sort the two Particle System Lists
		/// </summary>
		private void SortParticleSystemLists()
		{
			SortParticleSystemsByUpdateOrderList();
			SortParticleSystemsByDrawOrderList();
		}

		/// <summary>
		/// Sorts the Particle System List Sorted By Update Order
		/// </summary>
		private void SortParticleSystemsByUpdateOrderList()
		{
			mcParticleSystemListSortedByUpdateOrder.Sort((IDPSFParticleSystem cPS1, IDPSFParticleSystem cPS2) => cPS1.UpdateOrder.CompareTo(cPS2.UpdateOrder));
		}

		/// <summary>
		/// Sorts the Particle System List Sorted By Draw Order
		/// </summary>
		private void SortParticleSystemsByDrawOrderList()
		{
			mcParticleSystemListSortedByDrawOrder.Sort((IDPSFParticleSystem cPS1, IDPSFParticleSystem cPS2) => cPS1.DrawOrder.CompareTo(cPS2.DrawOrder));
		}

		/// <summary>
		/// Records that the Particle Systems need to be resorted before doing the next Updates
		/// </summary>
		/// <param name="sender">The Object that sent the event</param>
		/// <param name="e">Extra information</param>
		private void ParticleSystem_UpdateOrderChanged(object sender, EventArgs e)
		{
			mbAParticleSystemsUpdateOrderWasChanged = true;
		}

		/// <summary>
		/// Records that the Particle Systems need to be resorted before doing the next Draws
		/// </summary>
		/// <param name="sender">The Object that sent the event</param>
		/// <param name="e">Extra information</param>
		private void ParticleSystem_DrawOrderChanged(object sender, EventArgs e)
		{
			mbAParticleSystemsDrawOrderWasChanged = true;
		}
	}
	/// <summary>
	/// Class used to hold and update an object's 2D Position, Velocity, and Acceleration
	/// </summary>
	public class Position2D
	{
		/// <summary>
		/// The object's 2D Position
		/// </summary>
		public Vector2 Position = Vector2.Zero;

		/// <summary>
		/// The object's Position
		/// </summary>
		public Vector2 Velocity = Vector2.Zero;

		/// <summary>
		/// The object's Acceleration
		/// </summary>
		public Vector2 Acceleration = Vector2.Zero;

		/// <summary>
		/// Get / Set the object's 2D Position using a Vector3.
		/// <para>NOTE: The Z-value is ignored when Setting, and is given a value of zero when Getting.</para>
		/// <para>NOTE: This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>
		/// </summary>
		public Vector3 PositionVector3
		{
			get
			{
				return new Vector3(Position.X, Position.Y, 0f);
			}
			set
			{
				Position = new Vector2(value.X, value.Y);
			}
		}

		/// <summary>
		/// Get / Set the object's 2D Velocity using a Vector3.
		/// <para>NOTE: The Z-value is ignored when Setting, and is given a value of zero when Getting.</para>
		/// <para>NOTE: This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>para&gt;
		/// </summary>
		public Vector3 VelocityVector3
		{
			get
			{
				return new Vector3(Velocity.X, Velocity.Y, 0f);
			}
			set
			{
				Velocity = new Vector2(value.X, value.Y);
			}
		}

		/// <summary>
		/// Get / Set the object's 2D Acceleration using a Vector3.
		/// <para>NOTE: The Z-value is ignored when Setting, and is given a value of zero when Getting.</para>
		/// <para>NOTE: This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>para&gt;
		/// </summary>
		public Vector3 AccelerationVector3
		{
			get
			{
				return new Vector3(Acceleration.X, Acceleration.Y, 0f);
			}
			set
			{
				Acceleration = new Vector2(value.X, value.Y);
			}
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Position2D()
		{
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cPositionToCopy">The Position2D object to copy</param>
		public Position2D(Position2D cPositionToCopy)
		{
			CopyFrom(cPositionToCopy);
		}

		/// <summary>
		/// Copies the given Position2D object's data into this objects data
		/// </summary>
		/// <param name="cPositionToCopy">The Position2D object to copy</param>
		public void CopyFrom(Position2D cPositionToCopy)
		{
			Position = cPositionToCopy.Position;
			Velocity = cPositionToCopy.Velocity;
			Acceleration = cPositionToCopy.Acceleration;
		}

		/// <summary>
		/// Update the Position and Velocity according to the Acceleration
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The time Elapsed in Seconds since the last Update</param>
		public virtual void Update(float fElapsedTimeInSeconds)
		{
			Velocity += Acceleration * fElapsedTimeInSeconds;
			Position += Velocity * fElapsedTimeInSeconds;
		}
	}
	/// <summary>
	/// Class used to hold and update an object's 3D Position, Velocity, and Acceleration
	/// </summary>
	public class Position3D
	{
		/// <summary>
		/// The object's Position
		/// </summary>
		public Vector3 Position = Vector3.Zero;

		/// <summary>
		/// The object's Velocity
		/// </summary>
		public Vector3 Velocity = Vector3.Zero;

		/// <summary>
		/// The object's Acceleration
		/// </summary>
		public Vector3 Acceleration = Vector3.Zero;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Position3D()
		{
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cPositionToCopy">The Position3D object to copy</param>
		public Position3D(Position3D cPositionToCopy)
		{
			CopyFrom(cPositionToCopy);
		}

		/// <summary>
		/// Copy the given Position3D object's data into this objects data
		/// </summary>
		/// <param name="cPositionToCopy">The Position3D to copy from</param>
		public void CopyFrom(Position3D cPositionToCopy)
		{
			Position = cPositionToCopy.Position;
			Velocity = cPositionToCopy.Velocity;
			Acceleration = cPositionToCopy.Acceleration;
		}

		/// <summary>
		/// Update the Position and Velocity according to the Acceleration
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The time Elapsed in Seconds since the last Update</param>
		public void Update(float fElapsedTimeInSeconds)
		{
			Velocity += Acceleration * fElapsedTimeInSeconds;
			Position += Velocity * fElapsedTimeInSeconds;
		}
	}
	/// <summary>
	/// Class to hold and update an object's 2D Orientation, Rotational Velocity, and Rotational Acceleration
	/// </summary>
	public class Orientation2D
	{
		/// <summary>
		/// The object's Rotation (in radians) (i.e. How much it is currently rotated)
		/// </summary>
		public float Orientation;

		/// <summary>
		/// The object's Rotational Velocity (in radians)
		/// </summary>
		public float RotationalVelocity;

		/// <summary>
		/// The object's Rotational Acceleration (in radians)
		/// </summary>
		public float RotationalAcceleration;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Orientation2D()
		{
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cOrienationToCopy">The Orientation2D object to copy</param>
		public Orientation2D(Orientation2D cOrienationToCopy)
		{
			CopyFrom(cOrienationToCopy);
		}

		/// <summary>
		/// Copies the given Orientation2D object's data into this object's data
		/// </summary>
		/// <param name="cOrientationToCopy">The Orientation2D object to copy from</param>
		public void CopyFrom(Orientation2D cOrientationToCopy)
		{
			Orientation = cOrientationToCopy.Orientation;
			RotationalVelocity = cOrientationToCopy.RotationalVelocity;
			RotationalAcceleration = cOrientationToCopy.RotationalAcceleration;
		}

		/// <summary>
		/// Applies the given Rotation to the object's Orientation
		/// </summary>
		/// <param name="fRotation">The Rotation in radians that should be applied to the object</param>
		public void Rotate(float fRotation)
		{
			Orientation = Rotate(fRotation, Orientation);
		}

		/// <summary>
		/// Update the Orientation and Rotational Velocity according to the Rotational Acceleration
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The time Elapsed in Seconds since the last Update</param>
		public void Update(float fElapsedTimeInSeconds)
		{
			RotationalVelocity += RotationalAcceleration * fElapsedTimeInSeconds;
			Orientation += RotationalVelocity * fElapsedTimeInSeconds;
		}

		/// <summary>
		/// Returns the new Orientation after applying the given Rotation
		/// </summary>
		/// <param name="fRotationToApply">The Rotation in radians to apply to the Current Orientation</param>
		/// <param name="fCurrentOrientation">The object's Current Orientation in radians</param>
		/// <returns>Returns the new Orientation after applying the given Rotation</returns>
		public static float Rotate(float fRotationToApply, float fCurrentOrientation)
		{
			return fCurrentOrientation + fRotationToApply;
		}

		/// <summary>
		/// Returns the Rotation needed to rotate the object from the Current Rotation to
		/// the Desired Rotation
		/// </summary>
		/// <param name="fCurrentRotation">The object's Current Rotation in radians</param>
		/// <param name="fDesiredRotation">The object's Desired Rotation in radians</param>
		/// <returns>Returns the Rotation needed to rotate the object from the Current Rotation to
		/// the Desired Rotation</returns>
		public static float GetRotationTo(float fCurrentRotation, float fDesiredRotation)
		{
			return fDesiredRotation - fCurrentRotation;
		}
	}
	/// <summary>
	/// The Default Textured Quad Particle System to inherit from, which uses Default Textured Quad Particles
	/// </summary>
	public class DefaultTexturedQuadParticleSystem : DPSFDefaultTexturedQuadParticleSystem<DefaultTexturedQuadParticle, DefaultTexturedQuadParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultTexturedQuadParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Structure used to hold a Default Textured Quad Particle's Vertex's properties used for drawing.
	/// This contains a Vector3 Position, Vector2 TextureCoordinate, and Color Color.
	/// </summary>
	public struct DefaultTexturedQuadParticleVertex : IDPSFParticleVertex
	{
		private const int miSizeInBytes = 24;

		/// <summary>
		/// The Position of the vertex in 3D space. The position of this vertex
		/// relative to the quads other three vertices determines the Particle's orientation.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// The Coordinate of the Texture that this Vertex corresponds to
		/// </summary>
		public Vector2 TextureCoordinate;

		/// <summary>
		/// The Color to tint the Texture
		/// </summary>
		public Color Color;

		private static readonly VertexElement[] msVertexElements = new VertexElement[3]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(20, VertexElementFormat.Color, VertexElementUsage.Color, 0)
		};

		/// <summary>
		/// An array describing the attributes of each Vertex
		/// </summary>
		public VertexElement[] VertexElements => msVertexElements;

		/// <summary>
		/// The Size of one Vertex in Bytes
		/// </summary>
		public int SizeInBytes => 24;
	}
	/// <summary>
	/// The Default Sprite with Texture Coordinates Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultSpriteTextureCoordinatesParticleSystem<Particle, Vertex> : DPSFDefaultSpriteParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultSpriteTextureCoordinatesParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to draw a Sprite Particle. This function should be used to draw the given
		/// Particle with the provided SpriteBatch.
		/// </summary>
		/// <param name="Particle">The Particle Sprite to Draw</param>
		/// <param name="cSpriteBatch">The SpriteBatch to use to doing the Drawing</param>
		protected override void DrawSprite(DPSFParticle Particle, SpriteBatch cSpriteBatch)
		{
			DefaultSpriteTextureCoordinatesParticle defaultSpriteTextureCoordinatesParticle = (DefaultSpriteTextureCoordinatesParticle)Particle;
			Rectangle destinationRectangle = new Rectangle((int)defaultSpriteTextureCoordinatesParticle.Position.X, (int)defaultSpriteTextureCoordinatesParticle.Position.Y, (int)defaultSpriteTextureCoordinatesParticle.Width, (int)defaultSpriteTextureCoordinatesParticle.Height);
			float layerDepth = MathHelper.Clamp(defaultSpriteTextureCoordinatesParticle.Position.Z, 0f, 1f);
			Rectangle textureCoordinates = defaultSpriteTextureCoordinatesParticle.TextureCoordinates;
			cSpriteBatch.Draw(origin: new Vector2(textureCoordinates.Width / 2, textureCoordinates.Height / 2), texture: Texture, destinationRectangle: destinationRectangle, sourceRectangle: textureCoordinates, color: defaultSpriteTextureCoordinatesParticle.Color, rotation: defaultSpriteTextureCoordinatesParticle.Rotation, effects: defaultSpriteTextureCoordinatesParticle.FlipMode, layerDepth: layerDepth);
		}
	}
	/// <summary>
	/// The Default Sprite with Texture Coordinates Particle System to inherit from, which uses Default Sprite with Texture Coordinates Particles
	/// </summary>
	public class DefaultSpriteTextureCoordinatesParticleSystem : DPSFDefaultSpriteTextureCoordinatesParticleSystem<DefaultSpriteTextureCoordinatesParticle, DefaultSpriteParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultSpriteTextureCoordinatesParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Sprite with Texture Coordinates Particle System
	/// </summary>
	public class DefaultSpriteTextureCoordinatesParticle : DefaultSpriteParticle
	{
		/// <summary>
		/// The top-left Position and the Dimensions of this Picture in the Texture
		/// </summary>
		public Rectangle TextureCoordinates;

		/// <summary>
		/// Sets the Texture Coordinates to use for the Picture that represents this Particle.
		/// </summary>
		/// <param name="textureCoordinates">The top-left Position and the Dimensions of the Picture in the Texture.</param>
		public void SetTextureCoordinates(Rectangle textureCoordinates)
		{
			TextureCoordinates.X = textureCoordinates.X;
			TextureCoordinates.Y = textureCoordinates.Y;
			TextureCoordinates.Width = textureCoordinates.Width;
			TextureCoordinates.Height = textureCoordinates.Height;
		}

		/// <summary>
		/// Sets the Texture Coordinates to use for the Picture that represents this Particle.
		/// </summary>
		/// <param name="iLeft">The X position of the top-left corner of the Picture in the Texture</param>
		/// <param name="iTop">The Y position of the top-left corner of the Picture in the Texture</param>
		/// <param name="iRight">The X position of the bottom-right corner of the Picture in the Texture</param>
		/// <param name="iBottom">The Y position of the bottom-right corner of the Picture in the Texture</param>
		public void SetTextureCoordinates(int iLeft, int iTop, int iRight, int iBottom)
		{
			TextureCoordinates.X = iLeft;
			TextureCoordinates.Y = iTop;
			TextureCoordinates.Width = iRight - iLeft;
			TextureCoordinates.Height = iBottom - iTop;
		}

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			TextureCoordinates = default;
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultSpriteTextureCoordinatesParticle defaultSpriteTextureCoordinatesParticle = (DefaultSpriteTextureCoordinatesParticle)ParticleToCopy;
			base.CopyFrom(defaultSpriteTextureCoordinatesParticle);
			TextureCoordinates = defaultSpriteTextureCoordinatesParticle.TextureCoordinates;
		}
	}
	/// <summary>
	/// The Default Animated Sprite Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultAnimatedSpriteParticleSystem<Particle, Vertex> : DPSFDefaultSpriteTextureCoordinatesParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultAnimatedSpriteParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Updates the Animation, as well as the Particle's Texture Coordinates to match the Animation
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleAnimationAndTextureCoordinates(DefaultAnimatedSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Animation.Update(fElapsedTimeInSeconds);
			cParticle.TextureCoordinates = cParticle.Animation.CurrentPicturesTextureCoordinates;
		}

		/// <summary>
		/// Updates the Particle to be removed from the Particle System once the Animation finishes Playing
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleToDieOnceAnimationFinishesPlaying(DefaultAnimatedSpriteParticle cParticle, float fElapsedTimeInSeconds)
		{
			if (cParticle.Animation.CurrentAnimationIsDonePlaying)
			{
				cParticle.Lifetime = 1E-06f;
				cParticle.NormalizedElapsedTime = 1f;
			}
		}
	}
	/// <summary>
	/// The Default Animated Sprite Particle System to inherit from, which uses Default Animated Sprite Particles
	/// </summary>
	public class DefaultAnimatedSpriteParticleSystem : DPSFDefaultAnimatedSpriteParticleSystem<DefaultAnimatedSpriteParticle, DefaultSpriteParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultAnimatedSpriteParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Animated Sprite Particle System
	/// </summary>
	public class DefaultAnimatedSpriteParticle : DefaultSpriteTextureCoordinatesParticle
	{
		/// <summary>
		/// Class to hold this Particle's Animation information
		/// </summary>
		public Animations Animation;

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			Animation = new Animations();
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultAnimatedSpriteParticle defaultAnimatedSpriteParticle = (DefaultAnimatedSpriteParticle)ParticleToCopy;
			base.CopyFrom(defaultAnimatedSpriteParticle);
			Animation.CopyFrom(defaultAnimatedSpriteParticle.Animation);
		}
	}
	/// <summary>
	/// The Default Sprite 3D Billboard Particle System class.
	/// This class just inherits from the Default Sprite Particle System class and overrides the DrawSprite()
	/// function to draw the sprites as Billboards in 3D space.
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use.</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use.</typeparam>
	public class DPSFDefaultSprite3DBillboardTextureCoordinates<Particle, Vertex> : DPSFDefaultSprite3DBillboardParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultSprite3DBillboardTextureCoordinates(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Function to draw a Sprite Particle. This function should be used to draw the given
		/// Particle with the provided SpriteBatch.
		/// </summary>
		/// <param name="Particle">The Particle Sprite to Draw</param>
		/// <param name="cSpriteBatch">The SpriteBatch to use to doing the Drawing</param>
		protected override void DrawSprite(DPSFParticle Particle, SpriteBatch cSpriteBatch)
		{
			DefaultSprite3DBillboardTextureCoordinatesParticle defaultSprite3DBillboardTextureCoordinatesParticle = (DefaultSprite3DBillboardTextureCoordinatesParticle)Particle;
			Vector3 vector = Vector3.Transform(defaultSprite3DBillboardTextureCoordinatesParticle.Position, View);
			Vector2 position = new Vector2(vector.X, vector.Y);
			Rectangle textureCoordinates = defaultSprite3DBillboardTextureCoordinatesParticle.TextureCoordinates;
			cSpriteBatch.Draw(scale: new Vector2(defaultSprite3DBillboardTextureCoordinatesParticle.Width / (float)textureCoordinates.Width, (0f - defaultSprite3DBillboardTextureCoordinatesParticle.Height) / (float)textureCoordinates.Height), origin: new Vector2(textureCoordinates.Width / 2, textureCoordinates.Height / 2), texture: Texture, position: position, sourceRectangle: textureCoordinates, color: defaultSprite3DBillboardTextureCoordinatesParticle.ColorAsPremultiplied, rotation: defaultSprite3DBillboardTextureCoordinatesParticle.Rotation, effects: defaultSprite3DBillboardTextureCoordinatesParticle.FlipMode, layerDepth: vector.Z);
		}
	}
	/// <summary>
	/// The Default Sprite Particle System to inherit from, which uses Default Sprite Particles.
	/// </summary>
	public class DefaultSprite3DBillboardTextureCoordinatesParticleSystem : DPSFDefaultSprite3DBillboardTextureCoordinates<DefaultSprite3DBillboardTextureCoordinatesParticle, DefaultSpriteParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this.
		/// parameter if not using a Game object.</param>
		public DefaultSprite3DBillboardTextureCoordinatesParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Sprite 3D Billboard Particle System
	/// </summary>
	public class DefaultSprite3DBillboardTextureCoordinatesParticle : DefaultSprite3DBillboardParticle
	{
		/// <summary>
		/// The top-left Position and the Dimensions of this Picture in the Texture
		/// </summary>
		public Rectangle TextureCoordinates;

		/// <summary>
		/// Sets the Texture Coordinates to use for the Picture that represents this Particle.
		/// </summary>
		/// <param name="textureCoordinates">The top-left Position and the Dimensions of the Picture in the Texture.</param>
		public void SetTextureCoordinates(Rectangle textureCoordinates)
		{
			TextureCoordinates.X = textureCoordinates.X;
			TextureCoordinates.Y = textureCoordinates.Y;
			TextureCoordinates.Width = textureCoordinates.Width;
			TextureCoordinates.Height = textureCoordinates.Height;
		}

		/// <summary>
		/// Sets the Texture Coordinates to use for the Picture that represents this Particle
		/// </summary>
		/// <param name="iLeft">The X position of the top-left corner of the Picture in the Texture</param>
		/// <param name="iTop">The Y position of the top-left corner of the Picture in the Texture</param>
		/// <param name="iRight">The X position of the bottom-right corner of the Picture in the Texture</param>
		/// <param name="iBottom">The Y position of the bottom-right corner of the Picture in the Texture</param>
		public void SetTextureCoordinates(int iLeft, int iTop, int iRight, int iBottom)
		{
			TextureCoordinates.X = iLeft;
			TextureCoordinates.Y = iTop;
			TextureCoordinates.Width = iRight - iLeft;
			TextureCoordinates.Height = iBottom - iTop;
		}

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			TextureCoordinates = default;
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultSprite3DBillboardTextureCoordinatesParticle defaultSprite3DBillboardTextureCoordinatesParticle = (DefaultSprite3DBillboardTextureCoordinatesParticle)ParticleToCopy;
			base.CopyFrom(defaultSprite3DBillboardTextureCoordinatesParticle);
			TextureCoordinates = defaultSprite3DBillboardTextureCoordinatesParticle.TextureCoordinates;
		}
	}
	/// <summary>
	/// Class to hold and update an object's 3D Orientation, Rotational Velocity, and Rotational Acceleration
	/// </summary>
	public class Orientation3D
	{
		/// <summary>
		/// The object's Orientation
		/// </summary>
		public Quaternion Orientation = Quaternion.Identity;

		/// <summary>
		/// The object's Rotational Velocity around its center.
		/// <para>NOTE: Rotations are specified by giving a 3D Vector, where the direction is the axis to 
		/// rotate around, and the vector length is the amount (angle in radians) to rotate.
		/// It can also be thought of as Vector(PitchVelocity, YawVelocity, RollVelocity).</para>
		/// </summary>
		public Vector3 RotationalVelocity = Vector3.Zero;

		/// <summary>
		/// The object's Rotational Acceleration around its center.
		/// <para>NOTE: Rotations are specified by giving a 3D Vector, where the direction is the axis to 
		///  rotate around, and the vector length is the amount (angle in radians) to rotate.
		///  It can also be thought of as Vector(PitchAcceleration, YawAcceleration, RollAcceleration).</para>
		/// </summary>
		public Vector3 RotationalAcceleration = Vector3.Zero;

		/// <summary>
		/// Get / Set the Normal (i.e. Forward) direction of the object (i.e. which direction it is facing)
		/// </summary>
		public Vector3 Normal
		{
			get
			{
				return GetNormalDirection(Orientation);
			}
			set
			{
				SetNormalDirection(ref Orientation, value);
			}
		}

		/// <summary>
		/// Get / Set the Up direction of the object
		/// </summary>
		public Vector3 Up
		{
			get
			{
				return GetUpDirection(Orientation);
			}
			set
			{
				SetUpDirection(ref Orientation, value);
			}
		}

		/// <summary>
		/// Get / Set the Right direction of the object
		/// </summary>
		public Vector3 Right
		{
			get
			{
				return GetRightDirection(Orientation);
			}
			set
			{
				SetRightDirection(ref Orientation, value);
			}
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Orientation3D()
		{
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cOrienationToCopy">The Orienation3D object to copy</param>
		public Orientation3D(Orientation3D cOrienationToCopy)
		{
			CopyFrom(cOrienationToCopy);
		}

		/// <summary>
		/// Copies the given Orientation3D object's data into this object's data
		/// </summary>
		/// <param name="cOrientationToCopy">The Orientation3D object to copy from</param>
		public void CopyFrom(Orientation3D cOrientationToCopy)
		{
			Orientation = cOrientationToCopy.Orientation;
			RotationalVelocity = cOrientationToCopy.RotationalVelocity;
			RotationalAcceleration = cOrientationToCopy.RotationalAcceleration;
		}

		/// <summary>
		/// Rotates the object about its center, changing its Orientation
		/// </summary>
		/// <param name="sRotationMatrix">The Rotation to apply to the object</param>
		public void Rotate(Matrix sRotationMatrix)
		{
			Orientation = Rotate(sRotationMatrix, Orientation);
		}

		/// <summary>
		/// Update the Position and Velocity according to the Acceleration, as well as the Orientation
		/// according to the Rotational Velocity and Rotational Acceleration
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The time Elapsed in Seconds since the last Update</param>
		public void Update(float fElapsedTimeInSeconds)
		{
			if (RotationalAcceleration != Vector3.Zero)
			{
				RotationalVelocity += RotationalAcceleration * fElapsedTimeInSeconds;
			}
			if (RotationalVelocity != Vector3.Zero)
			{
				Orientation.Normalize();
				Quaternion quaternion = new Quaternion(RotationalVelocity * (fElapsedTimeInSeconds * 0.5f), 0f);
				Orientation += Orientation * quaternion;
			}
		}

		/// <summary>
		/// Returns the given Quaternion rotated about its center, changing its Orientation
		/// </summary>
		/// <param name="sRotationMatrix">The Rotation to apply to the Quaternion</param>
		/// <param name="sQuaterionToRotate">The Quaternion that should be Rotated</param>
		/// <returns>Returns the given Quaternion rotated about its center, changing its Orientation</returns>
		public static Quaternion Rotate(Matrix sRotationMatrix, Quaternion sQuaterionToRotate)
		{
			sQuaterionToRotate.Normalize();
			return sQuaterionToRotate *= Quaternion.CreateFromRotationMatrix(sRotationMatrix);
		}

		/// <summary>
		/// Returns the shortest arc Quaternion Rotation needed to rotate the CurrentDirection to
		/// be the same as the DestinationDirection.
		/// This method is based on Stan Melax's article in Game Programming Gems, and
		/// the code was referenced from OgreVector3.h of the Ogre library (www.Ogre3d.org)
		/// </summary>
		/// <param name="CurrentDirection">The current Direction the Vector is facing</param>
		/// <param name="DesiredDirection">The Direction we want the Vector to face</param>
		/// <returns>Returns the shortest arc Quaternion Rotation needed to rotate the CurrentDirection to
		/// be the same as the DestinationDirection.</returns>
		public static Quaternion GetRotationTo(Vector3 CurrentDirection, Vector3 DesiredDirection)
		{
			return GetRotationTo(CurrentDirection, DesiredDirection, Vector3.Zero);
		}

		/// <summary>
		/// Returns the shortest arc Quaternion Rotation needed to rotate the CurrentDirection to
		/// be the same as the DestinationDirection.
		/// This method is based on Stan Melax's article in Game Programming Gems, and
		/// the code was referenced from OgreVector3.h of the Ogre library (www.Ogre3d.org)
		/// </summary>
		/// <param name="CurrentDirection">The current Direction the Vector is facing</param>
		/// <param name="DesiredDirection">The Direction we want the Vector to face</param>
		/// <param name="sFallbackAxis">The Axis to rotate around if a 180 degree rotation is required</param>
		/// <returns>Returns the shortest arc Quaternion Rotation needed to rotate the CurrentDirection to
		/// be the same as the DestinationDirection.</returns>
		public static Quaternion GetRotationTo(Vector3 CurrentDirection, Vector3 DesiredDirection, Vector3 sFallbackAxis)
		{
			Quaternion result = default;
			Vector3 vector = CurrentDirection;
			Vector3 vector2 = DesiredDirection;
			vector.Normalize();
			vector2.Normalize();
			float num = Vector3.Dot(vector, vector2);
			if (num >= 1f)
			{
				return Quaternion.Identity;
			}
			if (num <= -0.999999f)
			{
				if (sFallbackAxis != Vector3.Zero)
				{
					sFallbackAxis.Normalize();
					return Quaternion.CreateFromAxisAngle(sFallbackAxis, (float)Math.PI);
				}
				Vector3 axis = Vector3.Cross(Vector3.UnitX, vector);
				if (axis.LengthSquared() < 0.999998f)
				{
					axis = Vector3.Cross(Vector3.UnitY, vector);
				}
				axis.Normalize();
				return Quaternion.CreateFromAxisAngle(axis, (float)Math.PI);
			}
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			double num2 = Math.Sqrt((1f + num) * 2f);
			double num3 = 1.0 / num2;
			result.X = (float)((double)vector3.X * num3);
			result.Y = (float)((double)vector3.Y * num3);
			result.Z = (float)((double)vector3.Z * num3);
			result.W = (float)(num2 * 0.5);
			result.Normalize();
			return result;
		}

		/// <summary>
		/// Returns a Quaternion orientated according to the given Normal and Up Directions
		/// </summary>
		/// <param name="sNormalDirection">The Normal (forward) direction that the Quaternion should face</param>
		/// <param name="sUpDirection">The Up direction that the Quaternion should have</param>
		/// <returns>Returns a Quaternion orientated according to the given Normal and Up Directions</returns>
		public static Quaternion GetQuaternionWithOrientation(Vector3 sNormalDirection, Vector3 sUpDirection)
		{
			Quaternion identity = Quaternion.Identity;
			Quaternion rotationTo = GetRotationTo(Vector3.Forward, sNormalDirection);
			identity = rotationTo * identity;
			Vector3 currentDirection = Vector3.Transform(Vector3.Up, identity);
			Quaternion rotationTo2 = GetRotationTo(currentDirection, sUpDirection);
			return rotationTo2 * identity;
		}

		/// <summary>
		/// Returns the Normal (Forward) Direction of the given Quaternion
		/// </summary>
		/// <param name="sOrientation">The Quaternion whose Direction we want</param>
		/// <returns>Returns the Normal (Forward) Direction of the given Quaternion</returns>
		public static Vector3 GetNormalDirection(Quaternion sOrientation)
		{
			return Vector3.Normalize(Vector3.Transform(Vector3.Forward, sOrientation));
		}

		/// <summary>
		/// Sets the Normal direction of the given Quaternion to be the given New Normal Direction
		/// </summary>
		/// <param name="sOrientation">The Quaternion to modify</param>
		/// <param name="sNewNormalDirection">The New Normal Direction the Quaternion should have</param>
		public static void SetNormalDirection(ref Quaternion sOrientation, Vector3 sNewNormalDirection)
		{
			Quaternion rotationTo = GetRotationTo(GetNormalDirection(sOrientation), sNewNormalDirection);
			sOrientation.Normalize();
			sOrientation = rotationTo * sOrientation;
		}

		/// <summary>
		/// Returns the Up Direction of the given Quaternion
		/// </summary>
		/// <param name="sOrientation">The Quaternion whose Direction we want</param>
		/// <returns>Returns the Up Direction of the given Quaternion</returns>
		public static Vector3 GetUpDirection(Quaternion sOrientation)
		{
			return Vector3.Normalize(Vector3.Transform(Vector3.Up, sOrientation));
		}

		/// <summary>
		/// Sets the Up direction of the given Quaternion to be the given New Up Direction
		/// </summary>
		/// <param name="sOrientation">The Quaternion to modify</param>
		/// <param name="sNewUpDirection">The New Up Direction the Quaternion should have</param>
		public static void SetUpDirection(ref Quaternion sOrientation, Vector3 sNewUpDirection)
		{
			Quaternion rotationTo = GetRotationTo(GetUpDirection(sOrientation), sNewUpDirection);
			sOrientation.Normalize();
			sOrientation = rotationTo * sOrientation;
		}

		/// <summary>
		/// Returns the Right Direction of the given Quaternion
		/// </summary>
		/// <param name="sOrientation">The Quaternion whose Direction we want</param>
		/// <returns>Returns the Right Direction of the given Quaternion</returns>
		public static Vector3 GetRightDirection(Quaternion sOrientation)
		{
			return Vector3.Normalize(Vector3.Transform(Vector3.Right, sOrientation));
		}

		/// <summary>
		/// Sets the Right direction of the given Quaternion to be the given New Right Direction
		/// </summary>
		/// <param name="sOrientation">The Quaternion to modify</param>
		/// <param name="sNewRightDirection">The New Right Direction the Quaternion should have</param>
		public static void SetRightDirection(ref Quaternion sOrientation, Vector3 sNewRightDirection)
		{
			Quaternion rotationTo = GetRotationTo(GetRightDirection(sOrientation), sNewRightDirection);
			sOrientation.Normalize();
			sOrientation = rotationTo * sOrientation;
		}
	}
	/// <summary>
	/// Class to hold a List of Animations and the texture coordintes of the Pictures used by the Animations.
	/// To start, Create Picture's of all images that will be used in any Animations. Then Create an Animation
	/// by specifying the order of the Picture IDs to go through, and the speed to flip through them at (i.e. frame-rate).
	/// </summary>
	public class Animations
	{
		/// <summary>
		/// Structure to store an individual Picture's position and dimensions within a texture
		/// </summary>
		private struct SPicture
		{
			public int iID;

			public Rectangle sTextureCoordinates;

			/// <summary>
			/// Explicit constructor
			/// </summary>
			/// <param name="iID">The ID of this Picture (this should be unique)</param>
			/// <param name="sTextureCoordinates">The top-left (x,y) position and (width,height) dimensions
			/// of the Picture within the texture</param>
			public SPicture(int iID, Rectangle sTextureCoordinates)
			{
				this.iID = iID;
				this.sTextureCoordinates = sTextureCoordinates;
			}
		}

		/// <summary>
		/// Class to hold a single Animation's (i.e. Walking, Running, Jumping, etc) sequence of 
		/// Pictures and how long to display each Picture in the Animation for
		/// </summary>
		private class Animation
		{
			public int miID;

			public List<int> mcPictureRotationOrder;

			public int miCurrentPictureIndex;

			public float mfPictureRotationTime;

			public int miNumberOfTimesToPlay;

			public int miNumberOfTimesPlayed;

			/// <summary>
			/// Returns the Picture ID of the Current Picture being displayed.
			/// </summary>
			public int CurrentPicturesID => mcPictureRotationOrder[miCurrentPictureIndex];

			/// <summary>
			/// Get if the Animation has finished Playing or not.
			/// NOTE: Animations with Number Of Times To Play == 0 will never end
			/// </summary>
			public bool AnimationHasEnded
			{
				get
				{
					if (miNumberOfTimesPlayed == miNumberOfTimesToPlay)
					{
						return miNumberOfTimesToPlay != 0;
					}
					return false;
				}
			}

			/// <summary>
			/// Explicit Constructor
			/// </summary>
			/// <param name="iID">The ID of this Animation (this should be unique)</param>
			/// <param name="cPictureRotationOrder">A List of Picture ID's which tell the sequence of 
			/// Pictures that make up the Animation</param>
			/// <param name="fPictureRotationTime">How long (in seconds) to wait before switching to the
			/// next Picture in the Picture Rotation Order</param>
			/// <param name="iNumberOfTimesToPlay">The Number of Times the Animation should Play before stopping. A value
			/// of zero means the Animation should repeat forever.</param>
			public Animation(int iID, List<int> cPictureRotationOrder, float fPictureRotationTime, int iNumberOfTimesToPlay)
			{
				miID = iID;
				mcPictureRotationOrder = new List<int>(cPictureRotationOrder);
				miCurrentPictureIndex = 0;
				mfPictureRotationTime = fPictureRotationTime;
				miNumberOfTimesToPlay = iNumberOfTimesToPlay;
				miNumberOfTimesPlayed = 0;
			}

			/// <summary>
			/// Moves the Current Picture Index to the next element in the Picture Rotation Order, and loops
			/// if it reaches the end of the Animation
			/// </summary>
			public void MoveToNextPictureInAnimation()
			{
				miCurrentPictureIndex++;
				if (miCurrentPictureIndex >= mcPictureRotationOrder.Count)
				{
					if (miNumberOfTimesToPlay == 0 || miNumberOfTimesPlayed < miNumberOfTimesToPlay)
					{
						miCurrentPictureIndex = 0;
						miNumberOfTimesPlayed++;
					}
					else
					{
						miNumberOfTimesPlayed = miNumberOfTimesToPlay;
						miCurrentPictureIndex = mcPictureRotationOrder.Count;
					}
				}
			}
		}

		private List<SPicture> mcPictures = new List<SPicture>();

		private List<Animation> mcAnimations = new List<Animation>();

		private int miCurrentAnimationID = -1;

		private float mfAnimationFrameTimer;

		private bool mbPaused;

		/// <summary>
		/// Get / Set the Current Animation being used. The Animation is started at its beginning.
		/// <para>NOTE: If an invalid Animiation ID is given when Setting, the Animation will not be changed.</para>
		/// <para>NOTE: If an Animation has not beeng set yet when Getting, -1 is returned.</para>
		/// </summary>
		public int CurrentAnimationID
		{
			get
			{
				return miCurrentAnimationID;
			}
			set
			{
				if (value < mcAnimations.Count && value >= 0)
				{
					miCurrentAnimationID = value;
					mcAnimations[miCurrentAnimationID].miCurrentPictureIndex = 0;
					mcAnimations[miCurrentAnimationID].miNumberOfTimesPlayed = 0;
					mfAnimationFrameTimer = 0f;
				}
			}
		}

		/// <summary>
		/// Get / Set how much Time should elapsed before switching frames in the Current Animation. 
		/// <para>NOTE: If no Animation has been set yet, zero will be returned.</para>
		/// </summary>
		public float CurrentAnimationsPictureRotationTime
		{
			get
			{
				return GetAnimationsPictureRotationTime(miCurrentAnimationID);
			}
			set
			{
				SetAnimationsPictureRotationTime(miCurrentAnimationID, value);
			}
		}

		/// <summary>
		/// Get / Set the Current Index in the Current Animation's Picture Rotation Order. 
		/// <para>NOTE: If no Animation has been set yet, Get returns -1, and Set doesn't change anything 
		/// (as well as if the specified Index is invalid).</para>
		/// </summary>
		public int CurrentAnimationsPictureRotationOrderIndex
		{
			get
			{
				if (CurrentAnimationIsValid)
				{
					return mcAnimations[miCurrentAnimationID].miCurrentPictureIndex;
				}
				return -1;
			}
			set
			{
				if (CurrentAnimationIsValid && value >= 0 && value < mcAnimations[miCurrentAnimationID].mcPictureRotationOrder.Count)
				{
					mcAnimations[miCurrentAnimationID].miCurrentPictureIndex = value;
					mfAnimationFrameTimer = 0f;
				}
			}
		}

		/// <summary>
		/// Get / Set the Number of times the Current Animation should Play
		/// (it replays when the end of the Animation is reached). 
		/// Specify a value of zero to have the Animation repeat forever.
		/// <para>NOTE: If no Animation has been set yet, no changes are made when
		/// Setting, and -1 is returned when Getting.</para>
		/// </summary>
		public int CurrentAnimationsNumberOfTimesToPlay
		{
			get
			{
				return GetAnimationsNumberOfTimesToPlay(miCurrentAnimationID);
			}
			set
			{
				SetAnimationsNumberOfTimesToPlay(miCurrentAnimationID, value);
			}
		}

		/// <summary>
		/// Get / Set the Number of times the Current Animation has Played already.
		/// <para>NOTE: If no Animation has been set yet, Get returns -1, and Set doesn't change anything.</para>
		/// </summary>
		public int CurrentAnimationsNumberOfTimesPlayed
		{
			get
			{
				if (CurrentAnimationIsValid)
				{
					return mcAnimations[miCurrentAnimationID].miNumberOfTimesPlayed;
				}
				return -1;
			}
			set
			{
				if (CurrentAnimationIsValid)
				{
					mcAnimations[miCurrentAnimationID].miNumberOfTimesPlayed = value;
				}
			}
		}

		/// <summary>
		/// Get if the Current Animation is Done Playing or not (i.e. Its Number Of Times Played is
		/// greater than or equal to its Number Of Times To Play). Returns true even if no
		/// Animation has been set to Play yet.
		/// </summary>
		public bool CurrentAnimationIsDonePlaying
		{
			get
			{
				if (CurrentAnimationIsValid)
				{
					int currentAnimationsNumberOfTimesToPlay = CurrentAnimationsNumberOfTimesToPlay;
					if (currentAnimationsNumberOfTimesToPlay != 0)
					{
						return CurrentAnimationsNumberOfTimesPlayed >= currentAnimationsNumberOfTimesToPlay;
					}
					return false;
				}
				return true;
			}
		}

		/// <summary>
		/// Gets the amount of Time (in seconds) required to play the Current Animation.
		/// <para>NOTE: If no Animation has been played yet, zero is returned.</para>
		/// </summary>
		public float TimeRequiredToPlayCurrentAnimation => TimeRequiredToPlayAnimation(miCurrentAnimationID);

		/// <summary>
		/// Gets the amount of Time (in seconds) required to play the remainder of the Current Animation.
		/// <para>NOTE: If no Animation has been played yet, zero is returned.</para>
		/// </summary>
		public float TimeRequiredToPlayTheRestOfTheCurrentAnimation
		{
			get
			{
				if (CurrentAnimationIsValid)
				{
					Animation animation = mcAnimations[miCurrentAnimationID];
					float num = animation.mfPictureRotationTime - mfAnimationFrameTimer;
					float num2 = (float)(animation.mcPictureRotationOrder.Count - (animation.miCurrentPictureIndex + 1)) * animation.mfPictureRotationTime + num;
					float num3 = (float)animation.mcPictureRotationOrder.Count * animation.mfPictureRotationTime * (float)(animation.miNumberOfTimesToPlay - (animation.miNumberOfTimesPlayed + 1));
					return num2 + num3;
				}
				return 0f;
			}
		}

		/// <summary>
		/// Get the Rectangle representing the Texture Coordinates of the Picture 
		/// in the Animation that should be displayed at this point in time
		/// </summary>
		public Rectangle CurrentPicturesTextureCoordinates
		{
			get
			{
				Rectangle result = default;
				if (CurrentAnimationIsValid && !CurrentAnimationIsDonePlaying)
				{
					return mcPictures[mcAnimations[miCurrentAnimationID].CurrentPicturesID].sTextureCoordinates;
				}
				return result;
			}
		}

		/// <summary>
		/// Get / Set if the Animation should be Paused or not. If Paused, the Animation will
		/// not be Updated.
		/// </summary>
		public bool Paused
		{
			get
			{
				return mbPaused;
			}
			set
			{
				mbPaused = value;
			}
		}

		/// <summary>
		/// Get if the Current Animation has been set yet or not
		/// </summary>
		private bool CurrentAnimationIsValid => AnimationIDIsValid(miCurrentAnimationID);

		/// <summary>
		/// Copies the given Animations data into this Animation
		/// </summary>
		/// <param name="cAnimationToCopy">The Animation to Copy from</param>
		public void CopyFrom(Animations cAnimationToCopy)
		{
			int num = 0;
			miCurrentAnimationID = cAnimationToCopy.miCurrentAnimationID;
			mfAnimationFrameTimer = cAnimationToCopy.mfAnimationFrameTimer;
			mcPictures = new List<SPicture>(cAnimationToCopy.mcPictures);
			int count = cAnimationToCopy.mcAnimations.Count;
			mcAnimations = new List<Animation>(count);
			for (num = 0; num < count; num++)
			{
				Animation item = new Animation(cAnimationToCopy.mcAnimations[num].miID, cAnimationToCopy.mcAnimations[num].mcPictureRotationOrder, cAnimationToCopy.mcAnimations[num].mfPictureRotationTime, cAnimationToCopy.mcAnimations[num].miNumberOfTimesToPlay);
				mcAnimations.Add(item);
				mcAnimations[num].miCurrentPictureIndex = cAnimationToCopy.mcAnimations[num].miCurrentPictureIndex;
				mcAnimations[num].miNumberOfTimesPlayed = cAnimationToCopy.mcAnimations[num].miNumberOfTimesPlayed;
			}
		}

		/// <summary>
		/// Creates a Picture that can be used in a Animation, and returns its unique ID. 
		/// A Picture can be used multiple times in an Animation.
		/// </summary>
		/// <param name="sTextureCoordinates">The top-left (x,y) position and (width,height) dimensions
		/// in the Texture that form this Picture</param>
		/// <returns>Returns the new Picture's unique ID.</returns>
		public int CreatePicture(Rectangle sTextureCoordinates)
		{
			int count = mcPictures.Count;
			SPicture item = new SPicture(count, sTextureCoordinates);
			mcPictures.Add(item);
			return count;
		}

		/// <summary>
		/// Automatically creates the specified Total Number Of Pictures. All pictures are assumed to have 
		/// the same width and height, as specified in the First Picture rectangle. Also, the First Picture
		/// is assumed to be at the top-left corner of the Tileset.
		/// <para>Pictures are created in left-to-right, top-to-bottom order. The ID of the first Picture created
		/// is returned, with each new Picture created incrementing the ID value, so the last Picture created
		/// will have an ID of (returned ID + (Total Number Of Pictures - 1)).</para>
		/// </summary>
		/// <param name="iTotalNumberOfPictures">The Total Number Of Pictures in the Tileset</param>
		/// <param name="iPicturesPerRow">How many Pictures are in a row in the texture</param>
		/// <param name="sFirstPicture">The Position of the top-left Picture in the Tileset, and the
		/// width and height of each Picture in the Tileset</param>
		/// <returns>The ID of the first Picture created
		/// is returned, with each new Picture created incrementing the ID value, so the last Picture created
		/// will have an ID of (returned ID + (Total Number Of Pictures - 1)).</returns>
		public int CreatePicturesFromTileSet(int iTotalNumberOfPictures, int iPicturesPerRow, Rectangle sFirstPicture)
		{
			int num = 0;
			int num2 = 0;
			for (num = 0; num < iTotalNumberOfPictures; num++)
			{
				int num3 = num / iPicturesPerRow;
				int num4 = num % iPicturesPerRow;
				Rectangle sTextureCoordinates = new Rectangle(sFirstPicture.X + sFirstPicture.Width * num4, sFirstPicture.Y + sFirstPicture.Height * num3, sFirstPicture.Width, sFirstPicture.Height);
				num2 = CreatePicture(sTextureCoordinates);
			}
			return num2 - (iTotalNumberOfPictures - 1);
		}

		/// <summary>
		/// Creates a new Animation and returns the Animation's unique ID.
		/// <para>NOTE: Returns -1 if an invalid Picture ID was specified in the PictureRotationOrder.</para>
		/// <para>NOTE: Be sure to Create the Pictures before creating the Animation.</para>
		/// </summary>
		/// <param name="cPictureRotationOrder">A List of Picture IDs that specifies the Order of Pictures
		/// to Rotate through in order to produce the Animation. A single Picture ID can be used many times.</param>
		/// <param name="fPictureRotationTime">How long (in seconds) to wait before switching to the
		/// next Picture in the Picture Rotation Order (i.e. The frame-rate of the Animation)</param>
		/// <param name="iNumberOfTimesToPlay">The number of times this Animation should be played 
		/// (it replays when the end of the Animation is reached). Specify a value of zero to have the 
		/// Animation repeat forever</param>
		/// <returns>Returns the new Animation's unique ID.</returns>
		public int CreateAnimation(List<int> cPictureRotationOrder, float fPictureRotationTime, int iNumberOfTimesToPlay)
		{
			int count = cPictureRotationOrder.Count;
			for (int i = 0; i < count; i++)
			{
				int num = cPictureRotationOrder[i];
				if (num < 0 || num > mcPictures.Count)
				{
					return -1;
				}
			}
			int count2 = mcAnimations.Count;
			Animation item = new Animation(count2, cPictureRotationOrder, fPictureRotationTime, iNumberOfTimesToPlay);
			mcAnimations.Add(item);
			return count2;
		}

		/// <summary>
		/// Creates a new Animation and returns the Animation's unique ID.
		/// <para>NOTE: Returns -1 if an invalid Picture ID was specified in the PictureRotationOrder.</para>
		/// <para>NOTE: Be sure to Create the Pictures before creating the Animation.</para>
		/// </summary>
		/// <param name="iaPictureRotationOrder">An array of Picture IDs that specifies the Order of Pictures
		/// to Rotate through in order to produce the Animation</param>
		/// <param name="fPictureRotationTime">How long (in seconds) to wait before switching to the
		/// next Picture in the Picture Rotation Order (i.e. The next Frame in the Animation)</param>
		/// <param name="iNumberOfTimesToPlay">The number of times this Animation should be played 
		/// (it replays when the end of the Animation is reached). Specify a value of zero to have the 
		/// Animation repeat forever</param>
		/// <returns>Returns the new Animation's unique ID.
		/// NOTE: Returns -1 if an invalid Picture ID was specified in the PictureRotationOrder.</returns>
		public int CreateAnimation(int[] iaPictureRotationOrder, float fPictureRotationTime, int iNumberOfTimesToPlay)
		{
			int num = 0;
			int num2 = iaPictureRotationOrder.Length;
			List<int> list = new List<int>(num2);
			for (num = 0; num < num2; num++)
			{
				list.Add(iaPictureRotationOrder[num]);
			}
			return CreateAnimation(list, fPictureRotationTime, iNumberOfTimesToPlay);
		}

		/// <summary>
		/// Returns true if the given Picture ID is valid (i.e. A Picture with the same ID exists).
		/// </summary>
		/// <param name="iPictureID">The Picture ID to look for</param>
		/// <returns>Returns true if the given Picture ID is valid (i.e. A Picture with the same ID exists).</returns>
		public bool PictureIDIsValid(int iPictureID)
		{
			if (iPictureID >= 0)
			{
				return iPictureID < mcPictures.Count;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the given Animation ID is valid (i.e. An Animation with the same ID exists).
		/// </summary>
		/// <param name="iAnimationID">The Animation ID to look for</param>
		/// <returns>Returns true if the given Animation ID is valid (i.e. An Animation with the same ID exists).</returns>
		public bool AnimationIDIsValid(int iAnimationID)
		{
			if (iAnimationID >= 0)
			{
				return iAnimationID < mcAnimations.Count;
			}
			return false;
		}

		/// <summary>
		/// Sets the Current Animation being used, as well as what index in the Animation's Picture Rotation
		/// Order the Animation should start at. 
		/// <para>NOTE: If the specified Animiation to use is not valid, the Current Animation will not be 
		/// changed, and if the specified Picture Rotation Order Index is not valid, the Animation will 
		/// start from the beginning of the Animation.</para>
		/// </summary>
		/// <param name="iAnimationID">The ID of the Animation to use</param>
		/// <param name="iPictureRotationOrderIndex">The Index in the Animation's Picture Rotation Order
		/// that the Animation should begin playing from</param>
		public void SetCurrentAnimationAndPositionInAnimation(int iAnimationID, int iPictureRotationOrderIndex)
		{
			if (AnimationIDIsValid(iAnimationID))
			{
				miCurrentAnimationID = iAnimationID;
				if (iPictureRotationOrderIndex >= 0 && iPictureRotationOrderIndex < mcAnimations[miCurrentAnimationID].mcPictureRotationOrder.Count)
				{
					mcAnimations[miCurrentAnimationID].miCurrentPictureIndex = iPictureRotationOrderIndex;
				}
				else
				{
					mcAnimations[miCurrentAnimationID].miCurrentPictureIndex = 0;
				}
				mcAnimations[miCurrentAnimationID].miNumberOfTimesPlayed = 0;
				mfAnimationFrameTimer = 0f;
			}
		}

		/// <summary>
		/// Returns how much Time (in seconds) should elapse before switching frames in the Animation.
		/// <para>NOTE: Returns zero if the specified Animation ID is not valid.</para>
		/// </summary>
		/// <param name="iAnimationID">The ID of the Animation containing the Picture Rotation Time to retrive</param>
		/// <returns>Returns how much Time (in seconds) should elapse before switching frames in the Animation.
		/// NOTE: Returns zero if the specified Animation ID is not valid.</returns>
		public float GetAnimationsPictureRotationTime(int iAnimationID)
		{
			float result = 0f;
			if (AnimationIDIsValid(iAnimationID))
			{
				result = mcAnimations[iAnimationID].mfPictureRotationTime;
			}
			return result;
		}

		/// <summary>
		/// Sets how much Time should elapse before switching frames in the Animation
		/// </summary>
		/// <param name="iAnimationID">The ID of the Animation to update</param>
		/// <param name="fNewPictureRotationTime">The Time (in seconds) to wait before moving to the
		/// next Picture in the Animations Picture Rotation Order</param>
		public void SetAnimationsPictureRotationTime(int iAnimationID, float fNewPictureRotationTime)
		{
			if (AnimationIDIsValid(iAnimationID))
			{
				mcAnimations[iAnimationID].mfPictureRotationTime = fNewPictureRotationTime;
			}
		}

		/// <summary>
		/// Returns the Number of times the given Animation ID is set to Play.
		/// Zero means the Animation will repeat forever.
		/// <para>NOTE: If the given Animation ID is invalid, -1 is returned.</para>
		/// </summary>
		/// <param name="iAnimationID">The ID of the Animation to update</param>
		/// <returns>Returns the Number of times the given Animation ID is set to Play.
		/// Zero means the Animation will repeat forever.
		/// NOTE: If the given Animation ID is invalid, -1 is returned.</returns>
		public int GetAnimationsNumberOfTimesToPlay(int iAnimationID)
		{
			int result = -1;
			if (AnimationIDIsValid(iAnimationID))
			{
				result = mcAnimations[iAnimationID].miNumberOfTimesToPlay;
			}
			return result;
		}

		/// <summary>
		/// Sets the Number of times the given Animation ID should Play
		/// (it replays when the end of the Animation is reached). 
		/// Specify a value of zero to have the Animation repeat forever.
		/// <para>NOTE: If the given Animation ID is invalid, no changes are made.</para>
		/// </summary>
		/// <param name="iAnimationID">The ID of the Animation to update</param>
		/// <param name="iNewNumberOfTimesToPlay">The New Number of times the Animation should Play</param>
		public void SetAnimationsNumberOfTimesToPlay(int iAnimationID, int iNewNumberOfTimesToPlay)
		{
			if (AnimationIDIsValid(iAnimationID))
			{
				mcAnimations[iAnimationID].miNumberOfTimesToPlay = iNewNumberOfTimesToPlay;
			}
		}

		/// <summary>
		/// Returns the amount of Time required to play the specified Animation.
		/// <para>NOTE: If an invalid AnimationID is specified, zero is returned.</para>
		/// </summary>
		/// <param name="iAnimationID">The ID of the Animation to check</param>
		/// <returns>Returns the amount of Time required to play the specified Animation.
		/// NOTE: If an invalid AnimationID is specified, zero is returned.</returns>
		public float TimeRequiredToPlayAnimation(int iAnimationID)
		{
			if (AnimationIDIsValid(iAnimationID))
			{
				Animation animation = mcAnimations[iAnimationID];
				return (float)animation.mcPictureRotationOrder.Count * animation.mfPictureRotationTime * (float)animation.miNumberOfTimesToPlay;
			}
			return 0f;
		}

		/// <summary>
		/// Returns the Rectangle representing the Texture Coordinates of the specified Picture.
		/// </summary>
		/// <param name="iPictureID">The Picture ID of the Picture whose Texture Coordinates 
		/// should be retrieved</param>
		/// <returns>Returns the Rectangle representing the Texture Coordinates of the specified Picture.</returns>
		public Rectangle GetPicturesTextureCoordinates(int iPictureID)
		{
			Rectangle result = default;
			if (iPictureID >= 0 && iPictureID < mcPictures.Count)
			{
				return mcPictures[iPictureID].sTextureCoordinates;
			}
			return result;
		}

		/// <summary>
		/// Updates the Animation according to how much time has elapsed
		/// </summary>
		/// <param name="fElapsedTime">The amount of Time (in seconds) since the last Update</param>
		public void Update(float fElapsedTime)
		{
			if (!Paused && CurrentAnimationIsValid)
			{
				mfAnimationFrameTimer += fElapsedTime;
				if (mfAnimationFrameTimer >= mcAnimations[miCurrentAnimationID].mfPictureRotationTime)
				{
					mfAnimationFrameTimer -= mcAnimations[miCurrentAnimationID].mfPictureRotationTime;
					mcAnimations[miCurrentAnimationID].MoveToNextPictureInAnimation();
				}
			}
		}
	}
	/// <summary>
	/// The base class that all Magnet classes inherit from. This class cannot be instantiated directly.
	/// A Magnet of a Particle System has an affect on its Particles, such as attracting or repelling them.
	/// </summary>
	public abstract class DefaultParticleSystemMagnet
	{
		/// <summary>
		/// The Modes that the Magnet can be in
		/// </summary>
		public enum MagnetModes
		{
			/// <summary>
			/// Attract Particles to the Magnet
			/// </summary>
			Attract,
			/// <summary>
			/// Repel Particles from the Magnet
			/// </summary>
			Repel,
			/// <summary>
			/// Have some other custom effect on the Particles
			/// </summary>
			Other
		}

		/// <summary>
		/// How much the Magnet should affect the Particles based on the Magnet's
		/// Max Distance and Strength
		/// </summary>
		public enum DistanceFunctions
		{
			/// <summary>
			/// As long as the Particle's distance from the Magnet is between the Min and Max Distance of 
			/// the Magnet, the Max Force will be applied to the Particle.
			/// <para>Function Logic: y = 1, where y is the normalized Force applied and 1 is the Particle's
			/// normalized distance between the Magnet's Min and Max Distances.</para>
			/// </summary>
			Constant,
			/// <summary>
			/// The Force applied to the Particle will be Linearly interpolated from zero to Max Force
			/// based on the Particle's distance from the Magnet between the Magnet's Min and Max Distances. 
			/// So when the Particle is at the Min Distance from the Magnet no force will be applied, when 
			/// the Particle is at half of the Max Distance from the Magnet 1/2 of the Max Force will be 
			/// applied, and when the Particle is at the Max Distance from the Magnet the Max Force will be 
			/// applied to the Particle.
			/// <para>Function Logic: y = x, where y is the normalized Force applied and x is the Particle's 
			/// normalized distance between the Magnet's Min and Max Distances.</para>
			/// </summary>
			Linear,
			/// <summary>
			/// The Force applied to the Particle will be Squared interpolated from zero to Max Force
			/// based on the Particle's distance from the Magnet between the Magnet's Min and Max Distances. 
			/// So when the Particle is at the Min Distance from the Magnet no force will be applied, when 
			/// the Particle is at half of the Max Distance from the Magnet 1/4 of the Max Force will be 
			/// applied, and when the Particle is at the Max Distance from the Magnet the Max Force will be 
			/// applied to the Particle.
			/// <para>Function Logic: y = x * x, where y is the normalized Force applied and x is the Particle's
			/// normalized distance between the Magnet's Min and Max Distances.</para>
			/// </summary>
			Squared,
			/// <summary>
			/// The Force applied to the Particle will be Cubed interpolated from zero to Max Force
			/// based on the Particle's distance from the Magnet between the Magnet's Min and Max Distances.
			/// So when the Particle is at the Min Distance from the Magnet no force will be applied, when 
			/// the Particle is at half of the Max Distance from the Manget 1/8 of the Max Force will be 
			/// applied, and when the Particle is at the Max Distance from the Magnet the Max Force will be 
			/// applied to the Particle.
			/// <para>Function Logic: y = x * x * x, where y is the normalized Force applied and x is the Particle's
			/// normalized distance between the Magnet's Min and Max Distances.</para>
			/// </summary>
			Cubed,
			/// <summary>
			/// The Inverse of the Linear function. That is, when the Particle is at the Min Distance from
			/// the Magnet the Max Force will be applied, when the Particle is at half of the Max Distance
			/// from the Magnet 1/2 of the Max Force will be applied, and when the Particle is at the Max Distance
			/// from the Magnet no force will be applied to the Particle.
			/// </summary>
			LinearInverse,
			/// <summary>
			/// The Inverse of the Squared function. That is, when the Particle is at the Min Distance from
			/// the Magnet the Max Force will be applied, when the Particle is at half of the Max Distance
			/// from the Magnet 1/4 of the Max Force will be applied, and when the Particle is at the Max Distance
			/// from the Magnet no force will be applied to the Particle.
			/// </summary>
			SquaredInverse,
			/// <summary>
			/// The Inverse of the Cubed function. That is, when the Particle is at the Min Distance from
			/// the Magnet the Max Force will be applied, when the Particle is at half of the Max Distance
			/// from the Magnet 1/8 of the Max Force will be applied, and when the Particle is at the Max Distance
			/// from the Magnet no force will be applied to the Particle.
			/// </summary>
			CubedInverse
		}

		/// <summary>
		/// The Types of Magnets available to choose from (i.e. which Magnet class is being used)
		/// </summary>
		public enum MagnetTypes
		{
			/// <summary>
			/// User-Defined Magnet Type (i.e. an instance of a user-defined Magnet class, not a Magnet class provided by DPSF)
			/// </summary>
			UserDefinedMagnet,
			/// <summary>
			/// Point Magnet (i.e. an instance of the MagnetPoint class)
			/// </summary>
			PointMagnet,
			/// <summary>
			/// Line Magnet (i.e. an instance of the MagnetLine class)
			/// </summary>
			LineMagnet,
			/// <summary>
			/// Line Segment Magnet (i.e. an instance of the MagnetLineSegment class)
			/// </summary>
			LineSegmentMagnet,
			/// <summary>
			/// Plane Magnet (i.e. an instance of the PlaneMagnet class)
			/// </summary>
			PlaneMagnet
		}

		/// <summary>
		/// The current Mode that the Magnet is in
		/// </summary>
		public MagnetModes Mode;

		/// <summary>
		/// The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is
		/// </summary>
		public DistanceFunctions DistanceFunction = DistanceFunctions.Linear;

		/// <summary>
		/// Holds the Type of Magnet this is
		/// </summary>
		protected MagnetTypes meMagnetType;

		/// <summary>
		/// The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect
		/// the Particle.
		/// </summary>
		public float MinDistance;

		/// <summary>
		/// The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not
		/// affect the Particle.
		/// </summary>
		public float MaxDistance = 100f;

		/// <summary>
		/// The Max Force that the Magnet is able to exert on a Particle
		/// </summary>
		public float MaxForce = 1f;

		/// <summary>
		/// The Type of User-Defined Magnet this is. User-defined Magnet classes will all have a 
		/// MagnetType = MagnetTypes.UserDefined, so this field can be used to distinguish between 
		/// different user-defined Magnet classes.
		/// This may be used in conjunction with the "Other" Magnet Mode to distinguish which type of 
		/// custom user effect the Magnet should have on the Particles.
		/// </summary>
		public int UserDefinedMagnetType;

		private static int SmiCounter;

		private int miID = SmiCounter++;

		/// <summary>
		/// Gets what Type of Magnet this is
		/// </summary>
		public MagnetTypes MagnetType => meMagnetType;

		/// <summary>
		/// Get the unique ID of this Magnet
		/// </summary>
		public int ID => miID;

		/// <summary>
		/// Explicit Constructor
		/// </summary>
		/// <param name="eMode">The Mode that the Magnet should be in</param>
		/// <param name="eDistanceFunction">The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is</param>
		/// <param name="fMinDistance">The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect the Particle.</param>
		/// <param name="fMaxDistance">The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not affect the Particle.</param>
		/// <param name="fMaxForce">The Max Force that the Magnet is able to exert on a Particle</param>
		/// <param name="iType">The Type of Magnet this is. This may be used in conjunction with the "Other" Magnet
		/// Mode to distinguish which type of custom user effect the Magnet should have on the Particles.</param>
		public DefaultParticleSystemMagnet(MagnetModes eMode, DistanceFunctions eDistanceFunction, float fMinDistance, float fMaxDistance, float fMaxForce, int iType)
		{
			Mode = eMode;
			DistanceFunction = eDistanceFunction;
			MinDistance = fMinDistance;
			MaxDistance = fMaxDistance;
			MaxForce = fMaxForce;
			UserDefinedMagnetType = iType;
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cMagnetToCopy">The Magnet to copy from</param>
		public DefaultParticleSystemMagnet(DefaultParticleSystemMagnet cMagnetToCopy)
		{
			CopyFrom(cMagnetToCopy);
		}

		/// <summary>
		/// Copies the given Magnet's data into this Magnet's data
		/// </summary>
		/// <param name="cMagnetToCopy">The Magnet to copy from</param>
		public void CopyFrom(DefaultParticleSystemMagnet cMagnetToCopy)
		{
			Mode = cMagnetToCopy.Mode;
			DistanceFunction = cMagnetToCopy.DistanceFunction;
			meMagnetType = cMagnetToCopy.MagnetType;
			MinDistance = cMagnetToCopy.MinDistance;
			MaxDistance = cMagnetToCopy.MaxDistance;
			MaxForce = cMagnetToCopy.MaxForce;
			UserDefinedMagnetType = cMagnetToCopy.UserDefinedMagnetType;
		}
	}
	/// <summary>
	/// Magnet that attracts particles to/from a single point in 3D space
	/// </summary>
	public class MagnetPoint : DefaultParticleSystemMagnet
	{
		/// <summary>
		/// The Position, Velocity, and Acceleration of the Magnet
		/// </summary>
		public Position3D PositionData = new Position3D();

		/// <summary>
		/// Explicit Constructor
		/// </summary>
		/// <param name="sPosition">The 3D Position of the Magnet</param>
		/// <param name="eMode">The Mode that the Magnet should be in</param>
		/// <param name="eDistanceFunction">The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is</param>
		/// <param name="fMinDistance">The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect the Particle.</param>
		/// <param name="fMaxDistance">The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not affect the Particle.</param>
		/// <param name="fMaxForce">The Max Force that the Magnet is able to exert on a Particle</param>
		/// <param name="iType">The Type of Magnet this is. This may be used in conjunction with the "Other" Magnet
		/// Mode to distinguish which type of custom user effect the Magnet should have on the Particles.</param>
		public MagnetPoint(Vector3 sPosition, MagnetModes eMode, DistanceFunctions eDistanceFunction, float fMinDistance, float fMaxDistance, float fMaxForce, int iType)
			: base(eMode, eDistanceFunction, fMinDistance, fMaxDistance, fMaxForce, iType)
		{
			meMagnetType = MagnetTypes.PointMagnet;
			PositionData.Position = sPosition;
		}

		/// <summary>
		/// Explicit Constructor
		/// </summary>
		/// <param name="cPositionData">The 3D Position, Velocity, and Acceleration of the Magnet</param>
		/// <param name="eMode">The Mode that the Magnet should be in</param>
		/// <param name="eDistanceFunction">The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is</param>
		/// <param name="fMinDistance">The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect the Particle.</param>
		/// <param name="fMaxDistance">The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not affect the Particle.</param>
		/// <param name="fMaxForce">The Max Force that the Magnet is able to exert on a Particle</param>
		/// <param name="iType">The Type of Magnet this is. This may be used in conjunction with the "Other" Magnet
		/// Mode to distinguish which type of custom user effect the Magnet should have on the Particles.</param>
		public MagnetPoint(Position3D cPositionData, MagnetModes eMode, DistanceFunctions eDistanceFunction, float fMinDistance, float fMaxDistance, float fMaxForce, int iType)
			: base(eMode, eDistanceFunction, fMinDistance, fMaxDistance, fMaxForce, iType)
		{
			meMagnetType = MagnetTypes.PointMagnet;
			PositionData = cPositionData;
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cMagnetToCopy">The Point Magnet to copy from</param>
		public MagnetPoint(MagnetPoint cMagnetToCopy)
			: base(MagnetModes.Attract, DistanceFunctions.Constant, 0f, 0f, 0f, 0)
		{
			CopyFrom(cMagnetToCopy);
		}

		/// <summary>
		/// Copies the given Point Magnet's data into this Point Magnet's data
		/// </summary>
		/// <param name="cMagnetToCopy">The Point Magnet to copy from</param>
		public void CopyFrom(MagnetPoint cMagnetToCopy)
		{
			CopyFrom((DefaultParticleSystemMagnet)cMagnetToCopy);
			PositionData.CopyFrom(cMagnetToCopy.PositionData);
		}
	}
	/// <summary>
	/// Magnet that attracts particles to/from an infinite line in 3D space
	/// </summary>
	public class MagnetLine : DefaultParticleSystemMagnet
	{
		/// <summary>
		/// The Direction that the Line points in
		/// </summary>
		private Vector3 msDirection = Vector3.Forward;

		/// <summary>
		/// A 3D point that the Line passes through
		/// </summary>
		public Vector3 PositionOnLine { get; set; }

		/// <summary>
		/// The direction that the Line points in. This direction, along with the opposite (i.e. negative) of 
		/// this direction form the line, since a line has infinite length. This value is 
		/// automatically normalized when it is set.
		/// </summary>
		public Vector3 Direction
		{
			get
			{
				return msDirection;
			}
			set
			{
				msDirection = value;
				msDirection.Normalize();
			}
		}

		/// <summary>
		/// Explicit Constructor
		/// </summary>
		/// <param name="sPositionOnLine">A 3D Position that the Line Magnet passes through</param>
		/// <param name="sDirection">The Direction that the Line points in</param>
		/// <param name="eMode">The Mode that the Magnet should be in</param>
		/// <param name="eDistanceFunction">The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is</param>
		/// <param name="fMinDistance">The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect the Particle.</param>
		/// <param name="fMaxDistance">The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not affect the Particle.</param>
		/// <param name="fMaxForce">The Max Force that the Magnet is able to exert on a Particle</param>
		/// <param name="iType">The Type of Magnet this is. This may be used in conjunction with the "Other" Magnet
		/// Mode to distinguish which type of custom user effect the Magnet should have on the Particles.</param>
		public MagnetLine(Vector3 sPositionOnLine, Vector3 sDirection, MagnetModes eMode, DistanceFunctions eDistanceFunction, float fMinDistance, float fMaxDistance, float fMaxForce, int iType)
			: base(eMode, eDistanceFunction, fMinDistance, fMaxDistance, fMaxForce, iType)
		{
			meMagnetType = MagnetTypes.LineMagnet;
			PositionOnLine = sPositionOnLine;
			Direction = sDirection;
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cMagnetToCopy">The Line Magnet to copy from</param>
		public MagnetLine(MagnetLine cMagnetToCopy)
			: base(MagnetModes.Attract, DistanceFunctions.Constant, 0f, 0f, 0f, 0)
		{
			CopyFrom(cMagnetToCopy);
		}

		/// <summary>
		/// Copies the given Line Magnet's data into this Line Magnet's data
		/// </summary>
		/// <param name="cMagnetToCopy">The Line Magnet to copy from</param>
		public void CopyFrom(MagnetLine cMagnetToCopy)
		{
			CopyFrom((DefaultParticleSystemMagnet)cMagnetToCopy);
			PositionOnLine = cMagnetToCopy.PositionOnLine;
			Direction = cMagnetToCopy.Direction;
		}

		/// <summary>
		/// Sets the Direction of the Line by specifying 2 points in 3D space that are on the Line.
		/// <para>NOTE: The 2 points cannot be the same.</para>
		/// </summary>
		/// <param name="sFirstPointOnTheLine">The first point that falls on the Line</param>
		/// <param name="sSecondPointOnTheLine">The second point that falls on the Line</param>
		public void SetDirection(Vector3 sFirstPointOnTheLine, Vector3 sSecondPointOnTheLine)
		{
			if (sFirstPointOnTheLine != sSecondPointOnTheLine)
			{
				Direction = sSecondPointOnTheLine - sFirstPointOnTheLine;
			}
		}
	}
	/// <summary>
	/// Magnet that attracts particles to/from a line with specified end points in 3D space
	/// </summary>
	public class MagnetLineSegment : DefaultParticleSystemMagnet
	{
		/// <summary>
		/// The position of the first End Point
		/// </summary>
		public Vector3 EndPoint1 { get; set; }

		/// <summary>
		/// The position of the second End Point
		/// </summary>
		public Vector3 EndPoint2 { get; set; }

		/// <summary>
		/// Explicit Constructor
		/// </summary>
		/// <param name="sEndPoint1Position">The 3D position of the first End Point of the Line Segment Magnet</param>
		/// <param name="sEndPoint2Position">The 3D position of the second End Point of the Line Segment Magnet</param>
		/// <param name="eMode">The Mode that the Magnet should be in</param>
		/// <param name="eDistanceFunction">The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is</param>
		/// <param name="fMinDistance">The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect the Particle.</param>
		/// <param name="fMaxDistance">The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not affect the Particle.</param>
		/// <param name="fMaxForce">The Max Force that the Magnet is able to exert on a Particle</param>
		/// <param name="iType">The Type of Magnet this is. This may be used in conjunction with the "Other" Magnet
		/// Mode to distinguish which type of custom user effect the Magnet should have on the Particles.</param>
		public MagnetLineSegment(Vector3 sEndPoint1Position, Vector3 sEndPoint2Position, MagnetModes eMode, DistanceFunctions eDistanceFunction, float fMinDistance, float fMaxDistance, float fMaxForce, int iType)
			: base(eMode, eDistanceFunction, fMinDistance, fMaxDistance, fMaxForce, iType)
		{
			meMagnetType = MagnetTypes.LineSegmentMagnet;
			EndPoint1 = sEndPoint1Position;
			EndPoint2 = sEndPoint2Position;
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cMagnetToCopy">The Line Segment Magnet to copy from</param>
		public MagnetLineSegment(MagnetLineSegment cMagnetToCopy)
			: base(MagnetModes.Attract, DistanceFunctions.Constant, 0f, 0f, 0f, 0)
		{
			CopyFrom(cMagnetToCopy);
		}

		/// <summary>
		/// Copies the given Line Segment Magnet's data into this Line Segment Magnet's data
		/// </summary>
		/// <param name="cMagnetToCopy">The Line Magnet to copy from</param>
		public void CopyFrom(MagnetLineSegment cMagnetToCopy)
		{
			CopyFrom((DefaultParticleSystemMagnet)cMagnetToCopy);
			EndPoint1 = cMagnetToCopy.EndPoint1;
			EndPoint2 = cMagnetToCopy.EndPoint2;
		}
	}
	/// <summary>
	/// Magnet that attracts particles to/from a plane in 3D space
	/// </summary>
	public class MagnetPlane : DefaultParticleSystemMagnet
	{
		/// <summary>
		/// The Normal direction of the Plane
		/// </summary>
		private Vector3 msNormal = Vector3.Up;

		/// <summary>
		/// A 3D point on the Plane
		/// </summary>
		public Vector3 PositionOnPlane { get; set; }

		/// <summary>
		/// The Normal direction of the Plane (i.e. the up direction away from the plane). This value is 
		/// automatically normalized when it is set.
		/// </summary>
		public Vector3 Normal
		{
			get
			{
				return msNormal;
			}
			set
			{
				msNormal = value;
				msNormal.Normalize();
			}
		}

		/// <summary>
		/// Explicit Constructor
		/// </summary>
		/// <param name="sPositionOnPlane">A 3D Position on the Plane Magnet's Plane</param>
		/// <param name="sNormal">The Normal direction of the Plane (i.e. the up direction away from the plane)</param>
		/// <param name="eMode">The Mode that the Magnet should be in</param>
		/// <param name="eDistanceFunction">The Function to use to determine how much a Particle should be affected by 
		/// the Magnet based on how far away from the Magnet it is</param>
		/// <param name="fMinDistance">The Min Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is closer to the Magnet than this distance, the Magnet will not affect the Particle.</param>
		/// <param name="fMaxDistance">The Max Distance that the Magnet should be able to affect Particles at. If the
		/// Particle is further away from the Magnet tan this distance, the Manget will not affect the Particle.</param>
		/// <param name="fMaxForce">The Max Force that the Magnet is able to exert on a Particle</param>
		/// <param name="iType">The Type of Magnet this is. This may be used in conjunction with the "Other" Magnet
		/// Mode to distinguish which type of custom user effect the Magnet should have on the Particles.</param>
		public MagnetPlane(Vector3 sPositionOnPlane, Vector3 sNormal, MagnetModes eMode, DistanceFunctions eDistanceFunction, float fMinDistance, float fMaxDistance, float fMaxForce, int iType)
			: base(eMode, eDistanceFunction, fMinDistance, fMaxDistance, fMaxForce, iType)
		{
			meMagnetType = MagnetTypes.PlaneMagnet;
			PositionOnPlane = sPositionOnPlane;
			Normal = sNormal;
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cMagnetToCopy">The Plane Magnet to copy from</param>
		public MagnetPlane(MagnetPlane cMagnetToCopy)
			: base(MagnetModes.Attract, DistanceFunctions.Constant, 0f, 0f, 0f, 0)
		{
			CopyFrom(cMagnetToCopy);
		}

		/// <summary>
		/// Copies the given Plane Magnet's data into this Plane Magnet's data
		/// </summary>
		/// <param name="cMagnetToCopy">The Plane Magnet to copy from</param>
		public void CopyFrom(MagnetPlane cMagnetToCopy)
		{
			CopyFrom((DefaultParticleSystemMagnet)cMagnetToCopy);
			PositionOnPlane = cMagnetToCopy.PositionOnPlane;
			Normal = cMagnetToCopy.Normal;
		}
	}
	/// <summary>
	/// The Default Quad Particle System to inherit from, which uses Default Quad Particles
	/// </summary>
	public class DefaultQuadParticleSystem : DPSFDefaultQuadParticleSystem<DefaultQuadParticle, DefaultQuadParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultQuadParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Structure used to hold a Default Quad Particle's Vertex's properties used for drawing.
	/// This contains a Vector3 Position and a Color Color.
	/// </summary>
	public struct DefaultQuadParticleVertex : IDPSFParticleVertex
	{
		private const int miSizeInBytes = 16;

		/// <summary>
		/// The Position of the vertex in 3D space. The position of this vertex
		/// relative to the quads other three vertices determines the Particle's orientation.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// The Color of the vertex
		/// </summary>
		public Color Color;

		private static readonly VertexElement[] msVertexElements = new VertexElement[2]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0)
		};

		/// <summary>
		/// An array describing the attributes of each Vertex
		/// </summary>
		public VertexElement[] VertexElements => msVertexElements;

		/// <summary>
		/// The Size of one Vertex in Bytes
		/// </summary>
		public int SizeInBytes => 16;
	}
	/// <summary>
	/// The Default No Display Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultNoDisplayParticleSystem<Particle, Vertex> : DPSFDefaultBaseParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultNoDisplayParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// The Default No Display Particle System to inherit from, which uses Default Pixel Particles
	/// </summary>
	public class DefaultNoDisplayParticleSystem : DPSFDefaultNoDisplayParticleSystem<DefaultNoDisplayParticle, DefaultNoDisplayParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultNoDisplayParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the No Display Particle System
	/// </summary>
	public class DefaultNoDisplayParticle : DPSFDefaultBaseParticle
	{
	}
	/// <summary>
	/// Dummy structure used for the vertices of a No Display particle system.
	/// Since the particles are not drawn, they do not have vertices, so this structure is empty.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct DefaultNoDisplayParticleVertex : IDPSFParticleVertex
	{
		/// <summary>
		/// An array describing the attributes of each Vertex
		/// </summary>
		public VertexElement[] VertexElements => null;

		/// <summary>
		/// The Size of one Vertex in Bytes
		/// </summary>
		public int SizeInBytes => 0;
	}
	/// <summary>
	/// The possible Modes the Automatic Memory Manager can be in
	/// </summary>
	public enum AutoMemoryManagerModes
	{
		/// <summary>
		/// Do not use the Automatic Memory Manager. The Number Of Particles Allocated In Memory will not be changed dynamically at run-time.
		/// This is the best option if performance is critical, as it can be expensive to allocate and release large chunks of memory at 
		/// run-time. If using this mode you should be sure that the Number Of Particles Allocated In Memory is large enough to accommodate 
		/// the particle system, but small enough that it does not waste large amounts of memory.
		/// </summary>
		Disabled,
		/// <summary>
		/// Allow the Automatic Memory Manager to allocate more memory when needed, and reduce it when not needed.
		/// </summary>
		IncreaseAndDecrease,
		/// <summary>
		/// Only allow the Automatic Memory Manager to allocate more memory when needed (cannot reduce space).
		/// </summary>
		IncreaseOnly,
		/// <summary>
		/// Only allow the Automatic Memory Manager to reduce the amount of memory allocated when it is not needed (cannot increase space).
		/// </summary>
		DecreaseOnly
	}
	/// <summary>
	/// Class to hold the Automatic Memory Manager Settings
	/// </summary>
	public class AutoMemoryManagerSettings
	{
		/// <summary>
		/// The Memory Management Mode being used.
		/// <para>NOTE: Default value is AutoMemoryManagerModes.IncreaseAndDecrease.</para>
		/// </summary>
		public AutoMemoryManagerModes MemoryManagementMode = AutoMemoryManagerModes.IncreaseOnly;

		private int miAbsoluteMinNumberOfParticles = 10;

		private float mfReduceAmount = 1.1f;

		private float mfIncreaseAmount = 2f;

		private float mfSecondsMaxNumberOfParticlesMustExistForBeforeReducingSize = 3f;

		/// <summary>
		/// The Absolute Minimum Number Of Particles this Particle System has to have memory allocated for.
		/// The Automatic Memory Manager will never allocate space for fewer Particles than this.
		/// <para>NOTE: This value must be greater than zero.</para>
		/// <para>NOTE: Default value is 10.</para>
		/// </summary>
		public int AbsoluteMinNumberOfParticles
		{
			get
			{
				return miAbsoluteMinNumberOfParticles;
			}
			set
			{
				if (value > 0)
				{
					miAbsoluteMinNumberOfParticles = value;
				}
			}
		}

		/// <summary>
		/// The Automatic Memory Manager keeps track of the Max Particles that were Active in a single
		/// frame over the last X seconds (call this number M). If the Max Number Of Particles is greater
		/// than M, the Automatic Memory Manager can de-allocate unused memory. The Reduce Amount determines
		/// how much more memory than M to allocate. For example, setting the Reduce Amount to 1.0 would set
		/// the Max Number Of Particles to M. Setting the Reduce Amount to 1.1 would set the Max Number Of
		/// Particles to M + 10%. Setting it to 2.0 would set the Max Number Of Particles to M + 100% (i.e. M * 2).
		/// <para>NOTE: This value is clamped to the range 1.0 - 2.0.</para>
		/// <para>NOTE: The Automatic Memory Manager will never reduce the amount of memory to be less than
		/// what is required for the Absolute Min Number Of Particles.</para>
		/// <para>NOTE: Default value is 1.1.</para>
		/// </summary>
		public float ReduceAmount
		{
			get
			{
				return mfReduceAmount;
			}
			set
			{
				mfReduceAmount = MathHelper.Clamp(value, 1f, 2f);
			}
		}

		/// <summary>
		/// The amount the Automatic Memory Manager increases the memory allocated for Particles by.
		/// When adding a new Particle, if we discover that the Number Of Active Particles has reached
		/// the Max Number Of Particles, the Automatic Memory Manager will increase the Max Number Of
		/// Particles by the Increase Amount. For example, if the Increase Amount is set to 2.0, then 
		/// the Max Number Of Particles will be doubled (200%). If it is set to 3.0 it will be tripled 
		/// (300%). If it is set to 0.5, the Max Number Of Particles will be increased to 150%.
		/// <para>NOTE: This value is clamped to the range 1.01 - 10.0 (i.e. 101% - 1000%).</para>
		/// <para>NOTE: The Automatic Memory Manager will never increase the amount of memory to be more than
		/// what is required by the Absolute Max Number Of Particles.</para>
		/// <para>NOTE: Default value is 2.0.</para>
		/// </summary>
		public float IncreaseAmount
		{
			get
			{
				return mfIncreaseAmount;
			}
			set
			{
				mfIncreaseAmount = MathHelper.Clamp(value, 1.01f, 10f);
			}
		}

		/// <summary>
		/// The Automatic Memory Manager keeps track of the Max Particles that were Active in a single
		/// frame over the last X seconds (call this number M). If the Max Number Of Particles is greater
		/// than M, the Automatic Memory Manager can de-allocate unused memory. The Seconds Max Number Of 
		/// Particles Must Exist For Before Reducing Size tells how long M must be unchanged for before
		/// the Automatic Memory Manager can reduce the amount of allocated memory.
		/// <para>NOTE: This value must be greater than zero.</para>
		/// <para>NOTE: Default value is 3.0.</para>
		/// </summary>
		public float SecondsMaxNumberOfParticlesMustExistForBeforeReducingSize
		{
			get
			{
				return mfSecondsMaxNumberOfParticlesMustExistForBeforeReducingSize;
			}
			set
			{
				if (value > 0f)
				{
					mfSecondsMaxNumberOfParticlesMustExistForBeforeReducingSize = value;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.AutoMemoryManagerSettings" /> class.
		/// </summary>
		public AutoMemoryManagerSettings()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.AutoMemoryManagerSettings" /> class, copying all of the settings from the given Settings To Copy.
		/// </summary>
		/// <param name="settingsToCopy">The settings to copy from.</param>
		public AutoMemoryManagerSettings(AutoMemoryManagerSettings settingsToCopy)
		{
			CopyFrom(settingsToCopy);
		}

		/// <summary>
		/// Copies the given Auto Memory Manager Settings into this instance.
		/// </summary>
		/// <param name="settingsToCopy">The settings to copy from.</param>
		public void CopyFrom(AutoMemoryManagerSettings settingsToCopy)
		{
			MemoryManagementMode = settingsToCopy.MemoryManagementMode;
			miAbsoluteMinNumberOfParticles = settingsToCopy.miAbsoluteMinNumberOfParticles;
			mfReduceAmount = settingsToCopy.mfReduceAmount;
			mfIncreaseAmount = settingsToCopy.mfIncreaseAmount;
			mfSecondsMaxNumberOfParticlesMustExistForBeforeReducingSize = settingsToCopy.mfSecondsMaxNumberOfParticlesMustExistForBeforeReducingSize;
		}
	}
	/// <summary>
	/// Collection of static functions for performing common operations
	/// </summary>
	public static class DPSFHelper
	{
		private static RandomNumbers RandomNumber;

		/// <summary>
		/// Return the version of the DPSF.dll being used. 
		/// This includes the Major, Minor, Build, and Revision numbers.
		/// </summary>
		public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

		/// <summary>
		/// Get if the Particle Systems are inheriting from DrawableGameComponent or not.
		/// <para>If inheriting from DrawableGameComponent, the Particle Systems
		/// are automatically added to the given Game object's Components and the
		/// Update() and Draw() functions are automatically called by the
		/// Game object when it updates and draws the rest of its Components.</para>
		/// <para>If the Update() and Draw() functions are called by the user anyways,
		/// they will exit without performing any operations, so it is suggested
		/// to include them anyways to make switching between inheriting and
		/// not inheriting from DrawableGameComponent seamless; just be aware
		/// that the updates and draws are actually being performed when the
		/// Game object is told to update and draw (i.e. when base.Update() and base.Draw()
		/// are called), not when the particle system functions are called.</para>
		/// </summary>
		public static bool DPSFInheritsDrawableGameComponent => false;

		/// <summary>
		/// Initializes the <see cref="T:DPSF.DPSFHelper" /> class.
		/// This constructor cannot be explicitly called. It will be called the first time this class is accessed.
		/// </summary>
		static DPSFHelper()
		{
			RandomNumber = new RandomNumbers();
		}

		/// <summary>
		/// Returns a random number between the specified values
		/// </summary>
		/// <param name="fValue1">The first value</param>
		/// <param name="fValue2">The second value</param>
		/// <returns>Returns a random number between the specified values</returns>
		public static float RandomNumberBetween(float fValue1, float fValue2)
		{
			return RandomNumber.Between(fValue1, fValue2);
		}

		/// <summary>
		/// Returns how transparent a Particle should be, based on it's Normalized Elapsed Time, so that it
		/// fades in quickly and fades out slowly.
		/// </summary>
		/// <param name="fNormalizedElapsedTime">The current Normalized Elapsed Time (0.0 - 1.0) of a Particle</param>
		/// <returns>Returns the Alpha Color component that should be used for the Particle</returns>
		public static byte FadeInQuicklyAndFadeOutSlowlyBasedOnLifetime(float fNormalizedElapsedTime)
		{
			return (byte)((double)(fNormalizedElapsedTime * (1f - fNormalizedElapsedTime) * (1f - fNormalizedElapsedTime)) * 6.7 * 255.0);
		}

		/// <summary>
		/// Returns how transparent a Particle should be, based on it's Normalized Elapsed Time, so that it
		/// fades in quickly and fades out quickly.
		/// </summary>
		/// <param name="fNormalizedElapsedTime">The current Normalized Elapsed Time (0.0 - 1.0) of a Particle</param>
		/// <returns>Returns the Alpha Color component that should be used for the Particle</returns>
		public static byte FadeInQuicklyAndFadeOutQuicklyBasedOnLifetime(float fNormalizedElapsedTime)
		{
			if (fNormalizedElapsedTime < 0.1f)
			{
				return (byte)(int)MathHelper.Lerp(0f, 255f, fNormalizedElapsedTime * 10f);
			}
			if (fNormalizedElapsedTime > 0.8f)
			{
				return (byte)(int)MathHelper.Lerp(255f, 0f, (fNormalizedElapsedTime - 0.8f) * 5f);
			}
			return byte.MaxValue;
		}

		/// <summary>
		/// Returns the interpolation amount (between 0.0 and 1.0) that should be used in a Lerp function to have a 
		/// property reach its full value when the NormalizedLifetime reaches 0.5, and go back to its original value 
		/// by the time the NormalizedLifetime reaches 1.0.
		/// <para>An example of where to use this would be if you wanted a particle to start off small, and reach
		/// its full size when the particle's lifetime is half over, and then to shrink back to being small by the
		/// time the particle dies. You would use the value returned by this function as the interpolation amount
		/// for the Lerp function. e.g. MathHelper.Lerp(SmallSize, LargeSize, ValueReturnedByThisFunction).</para>
		/// </summary>
		/// <param name="fNormalizedElapsedTime">The current Normalized Elapsed Time (0.0 - 1.0) of a Particle</param>
		/// <returns>Returns the interpolation amount (between 0.0 and 1.0) that should be used in a Lerp function to have a 
		/// property reach its full value when the NormalizedLifetime reaches 0.5, and go back to its original value 
		/// by the time the NormalizedLifetime reaches 1.0.</returns>
		public static float InterpolationAmountForEqualLerpInAndLerpOut(float fNormalizedElapsedTime)
		{
			if (fNormalizedElapsedTime <= 0.5f)
			{
				return fNormalizedElapsedTime * 2f;
			}
			return 1f - (fNormalizedElapsedTime - 0.5f) * 2f;
		}

		/// <summary>
		/// Returns a vector with a Random direction that has been Normalized.
		/// </summary>
		/// <returns>Returns a vector with a Random direction that has been Normalized.</returns>
		public static Vector3 RandomNormalizedVector()
		{
			Vector3 result = new Vector3(RandomNumber.Between(-1f, 1f), RandomNumber.Between(-1f, 1f), RandomNumber.Between(-1f, 1f));
			result.Normalize();
			return result;
		}

		/// <summary>
		/// Returns a vector representing the line from the source point to the target point.
		/// </summary>
		/// <param name="source">The source position.</param>
		/// <param name="target">The target position.</param>
		/// <returns>Returns a vector representing the line from the source point to the target point.</returns>
		public static Vector3 SourceToTargetVector(Vector3 source, Vector3 target)
		{
			return target - source;
		}

		/// <summary>
		/// Returns a normalized vector representing the direction that points from the source point to the target point.
		/// </summary>
		/// <param name="source">The source position.</param>
		/// <param name="target">The target position.</param>
		/// <returns>Returns a normalized vector representing the direction that points from the source point to the target point.</returns>
		public static Vector3 SourceToTargetDirection(Vector3 source, Vector3 target)
		{
			Vector3 result = SourceToTargetVector(source, target);
			result.Normalize();
			return result;
		}

		/// <summary>
		/// Returns a Vector whose individual XYZ components are each randomly chosen to be somewhere 
		/// between the two given Vectors' individual XYZ components. Unlike choosing a random Lerp value
		/// between two vectors, which would give a point somewhere on the LINE between the two points, this
		/// chooses a random Lerp value between each of the two vectors individual xyz components, returning
		/// a point somewhere in the cube-shaped AREA (i.e. Volume) between the two points.
		/// </summary>
		/// <param name="sVector1">The first Vector</param>
		/// <param name="sVector2">The second Vector</param>
		/// <returns>Returns a Vector whose individual XYZ components are each randomly chosen to be somewhere 
		/// between the two given Vectors' individual XYZ components.</returns>
		public static Vector3 RandomVectorBetweenTwoVectors(Vector3 sVector1, Vector3 sVector2)
		{
			return new Vector3(RandomNumber.Between(sVector1.X, sVector2.X), RandomNumber.Between(sVector1.Y, sVector2.Y), RandomNumber.Between(sVector1.Z, sVector2.Z));
		}

		/// <summary>
		/// Returns a random opaque Color (i.e. no transparency)
		/// </summary>
		/// <returns>Returns a random opaque Color (i.e. no transparency)</returns>
		public static Color RandomColor()
		{
			return new Color(RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat());
		}

		/// <summary>
		/// Returns a random Color with a random alpha value as well (i.e. random transparency)
		/// </summary>
		/// <returns>Returns a random Color with a random alpha value as well (i.e. random transparency)</returns>
		public static Color RandomColorWithRandomTransparency()
		{
			return new Color(RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat(), RandomNumber.NextFloat());
		}

		/// <summary>
		/// Returns the linearly interpolated Color between Color 1 and Color 2
		/// </summary>
		/// <param name="sColor1">The Color value used with InterpolationAmount 0.0</param>
		/// <param name="sColor2">The Color value used with InterpolationAmount 1.0</param>
		/// <param name="fInterpolationAmount">A value between 0.0 and 1.0 indicating how 
		/// much to interpolate the RGBA values between Color 1 and Color 2</param>
		/// <returns>Returns the interpolated Color</returns>
		public static Color LerpColor(Color sColor1, Color sColor2, float fInterpolationAmount)
		{
			return LerpColor(sColor1, sColor2, fInterpolationAmount, fInterpolationAmount, fInterpolationAmount, fInterpolationAmount);
		}

		/// <summary>
		/// Returns the linearly interpolated Color between Color 1 and Color 2
		/// </summary>
		/// <param name="sColor1">The Color value used with InterpolationAmount 0.0</param>
		/// <param name="sColor2">The Color value used with InterpolationAmount 1.0</param>
		/// <param name="fRInterpolationAmount">A value between 0.0 and 1.0 indicating how 
		/// much to interpolate the Red value between Color 1 and Color 2</param>
		/// <param name="fGInterpolationAmount">A value between 0.0 and 1.0 indicating how 
		/// much to interpolate the Green value between Color 1 and Color 2</param>
		/// <param name="fBInterpolationAmount">A value between 0.0 and 1.0 indicating how 
		/// much to interpolate the Blue value between Color 1 and Color 2</param>
		/// <param name="fAInterpolationAmount">A value between 0.0 and 1.0 indicating how 
		/// much to interpolate the Alpha value between Color 1 and Color 2</param>
		/// <returns>Returns the interpolated Color</returns>
		public static Color LerpColor(Color sColor1, Color sColor2, float fRInterpolationAmount, float fGInterpolationAmount, float fBInterpolationAmount, float fAInterpolationAmount)
		{
			byte r = (byte)((float)(int)sColor1.R + (float)(sColor2.R - sColor1.R) * fRInterpolationAmount);
			byte g = (byte)((float)(int)sColor1.G + (float)(sColor2.G - sColor1.G) * fGInterpolationAmount);
			byte b = (byte)((float)(int)sColor1.B + (float)(sColor2.B - sColor1.B) * fBInterpolationAmount);
			byte a = (byte)((float)(int)sColor1.A + (float)(sColor2.A - sColor1.A) * fAInterpolationAmount);
			return new Color(r, g, b, a);
		}

		/// <summary>
		/// Returns a point on a circle with a radius of one, on the X-Y axis plane. 
		/// To use a different radius simply multiply the returned value by the desired radius value.
		/// </summary>
		/// <param name="fAngle">The angle on the circle in radians</param>
		/// <returns>Returns a 2D position on a circle</returns>
		public static Vector2 PointOnCircle(float fAngle)
		{
			float x = (float)Math.Cos(fAngle);
			float y = (float)Math.Sin(fAngle);
			return new Vector2(x, y);
		}

		/// <summary>
		/// Returns a point on a circle with a radius of one, on the X-Y axis plane (Z value of zero). 
		/// To use a different radius simply multiply the returned value by the desired radius value.
		/// </summary>
		/// <param name="fAngle">The angle on the circle in radians</param>
		/// <returns>Returns a 3D position on a circle</returns>
		public static Vector3 PointOnCircleVector3(float fAngle)
		{
			return new Vector3(PointOnCircle(fAngle), 0f);
		}

		/// <summary>
		/// Returns a random point on a circle with a radius of one, on the X-Y axis plane. 
		/// To use a different radius simply multiply the returned value by the desired radius value.
		/// </summary>
		/// <returns>Returns a random 2D position on a circle</returns>
		public static Vector2 RandomPointOnCircle()
		{
			return PointOnCircle(RandomNumber.Between(0f, (float)Math.PI * 2f));
		}

		/// <summary>
		/// Returns a random point on a circle with a radius of one, on the X-Y axis plane (Z value of zero). 
		/// To use a different radius simply multiply the returned value by the desired radius value.
		/// </summary>
		/// <returns>Returns a random 3D position on a circle, with a Z value of zero</returns>
		public static Vector3 RandomPointOnCircleVector3()
		{
			return new Vector3(RandomPointOnCircle(), 0f);
		}

		/// <summary>
		/// Returns a point on a sphere with a radius of one.  To use a different radius simply 
		/// multiply the returned value by the desired radius value, before translating it
		/// to the sphere's position in world coordinates.  To create a circle simply use a
		/// constant value for one of the Angles while changing the other Angle.
		/// </summary>
		/// <param name="fYawAngle">Imagine a point on the surface of the sphere at the front 
		/// center of the sphere. This value will be how much to rotate that point around the 
		/// horizontal ring around the center of the sphere, in radians.</param>
		/// <param name="fPitchAngle">This value will be how much to rotate that point around 
		/// the vertical ring around the sphere, at the current position of the point, in radians.</param>
		/// <returns>Returns a point on a sphere with a radius of one</returns>
		public static Vector3 NormalizedPointOnSphere(float fYawAngle, float fPitchAngle)
		{
			return PivotPoint3D.RotatePosition(Matrix.CreateFromYawPitchRoll(fYawAngle, fPitchAngle, 0f), Vector3.Zero, Vector3.Forward);
		}

		/// <summary>
		/// Returns a random point on a sphere with a radius of one.  To use a different radius simply 
		/// multiply the returned value by the desired radius value, before translating it
		/// to the sphere's position in world coordinates.
		/// </summary>
		/// <returns>Returns a random point on a sphere with a radius of one</returns>
		public static Vector3 RandomNormalizedPointOnSphere()
		{
			return NormalizedPointOnSphere(RandomNumber.Between(0f, (float)Math.PI * 2f), RandomNumber.Between(0f, (float)Math.PI * 2f));
		}

		/// <summary>
		/// Returns a point on a sphere with the given Radius. To create a circle simply use a
		/// constant value for one of the Angles while changing the other Angle.
		/// </summary>
		/// <param name="fYawAngle">Imagine a point on the surface of the sphere at the front 
		/// center of the sphere. This value will be how much to rotate that point around the 
		/// horizontal ring around the center of the sphere, in radians.</param>
		/// <param name="fPitchAngle">This value will be how much to rotate that point around 
		/// the vertical ring around the sphere, at the current position of the point, in radians.</param>
		/// <param name="fRadius">The radius the sphere should have</param>
		/// <returns>Returns a point on a sphere with the given radius</returns>
		public static Vector3 PointOnSphere(float fYawAngle, float fPitchAngle, float fRadius)
		{
			return NormalizedPointOnSphere(fYawAngle, fPitchAngle) * fRadius;
		}

		/// <summary>
		/// Returns a random point on a sphere with the given Radius
		/// </summary>
		/// <param name="fRadius">The radius the sphere should have</param>
		/// <returns>Returns a random point on a sphere with the given Radius</returns>
		public static Vector3 RandomPointOnSphere(float fRadius)
		{
			return PointOnSphere(RandomNumber.Between(0f, (float)Math.PI * 2f), RandomNumber.Between(0f, (float)Math.PI * 2f), fRadius);
		}

		/// <summary>
		/// Returns a point on a sphere with the given Radius, at the specified Sphere Position.
		/// To create a circle simply use a constant value for one of the Angles while changing 
		/// the other Angle.
		/// </summary>
		/// <param name="fYawAngle">Imagine a point on the surface of the sphere at the front 
		/// center of the sphere. This value will be how much to rotate that point around the 
		/// horizontal ring around the center of the sphere, in radians.</param>
		/// <param name="fPitchAngle">This value will be how much to rotate that point around 
		/// the vertical ring around the sphere, at the current position of the point, in radians.</param>
		/// <param name="fRadius">The radius the sphere should have</param>
		/// <param name="sSpherePosition">The center position of the sphere in world coordinates</param>
		/// <returns>Returns a point on a sphere with the given Radius, at the specified Sphere Position</returns>
		public static Vector3 PointOnSphere(float fYawAngle, float fPitchAngle, float fRadius, Vector3 sSpherePosition)
		{
			return PointOnSphere(fYawAngle, fPitchAngle, fRadius) + sSpherePosition;
		}

		/// <summary>
		/// Returns a random point on a sphere with the given Radius, at the specified Sphere Position
		/// </summary>
		/// <param name="fRadius">The radius the sphere should have</param>
		/// <param name="sSpherePosition">The center position of the sphere in world coordinates</param>
		/// <returns>Returns a random point on a sphere with the given Radius, at the specified Sphere Position</returns>
		public static Vector3 RandomPointOnSphere(float fRadius, Vector3 sSpherePosition)
		{
			return PointOnSphere(RandomNumber.Between(0f, (float)Math.PI * 2f), RandomNumber.Between(0f, (float)Math.PI * 2f), fRadius, sSpherePosition);
		}

		/// <summary>
		/// Returns true if the difference between the individual XYZ components of the
		/// given Vectors are all less than the specified Tolerance
		/// </summary>
		/// <param name="sVector1">The first Vector</param>
		/// <param name="sVector2">The second Vector</param>
		/// <param name="fTolerance">How much of a difference there may be between the individual
		/// XYZ components for them to be considered equal</param>
		/// <returns>Returns true if the difference between the individual XYZ components of the
		/// given Vectors are each less than the specified Tolerance</returns>
		public static bool VectorsAreEqualWithinTolerance(Vector3 sVector1, Vector3 sVector2, float fTolerance)
		{
			sVector1.Normalize();
			sVector2.Normalize();
			Vector3 vector = sVector1 - sVector2;
			if (Math.Abs(vector.X) <= fTolerance && Math.Abs(vector.X) <= fTolerance && Math.Abs(vector.X) <= fTolerance)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns if the polarity (i.e. positive or negative) of each X,Y,Z component of the two vectors are the same or not.
		/// <para>Example: if Vector 1's X component is positive and Vector 2's X component is negative, this will return false.</para>
		/// <para>Example: if Vector 1's X,Y,Z components are all positive, and Vector 2's X,Y,Z components are all positive, this will return true.</para>
		/// </summary>
		/// <param name="sVector1">The first Vector.</param>
		/// <param name="sVector2">The second Vector.</param>
		/// <returns></returns>
		public static bool VectorsAreTheSamePolarity(Vector3 sVector1, Vector3 sVector2)
		{
			if ((sVector1.X > 0f && sVector2.X < 0f) || (sVector1.X < 0f && sVector2.X > 0f))
			{
				return false;
			}
			if ((sVector1.Y > 0f && sVector2.Y < 0f) || (sVector1.Y < 0f && sVector2.Y > 0f))
			{
				return false;
			}
			if ((sVector1.Z > 0f && sVector2.Z < 0f) || (sVector1.Z < 0f && sVector2.Z > 0f))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Returns a clone of the given BlendState.
		/// Read-only properties, such as the GraphicsDevice property, are not copied though.
		/// </summary>
		/// <param name="blendStateToClone">The BlendState to clone.</param>
		public static BlendState CloneBlendState(BlendState blendStateToClone)
		{
			BlendState blendState = new BlendState();
			blendState.AlphaBlendFunction = blendStateToClone.AlphaBlendFunction;
			blendState.AlphaDestinationBlend = blendStateToClone.AlphaDestinationBlend;
			blendState.AlphaSourceBlend = blendStateToClone.AlphaSourceBlend;
			blendState.BlendFactor = blendStateToClone.BlendFactor;
			blendState.ColorBlendFunction = blendStateToClone.ColorBlendFunction;
			blendState.ColorDestinationBlend = blendStateToClone.ColorDestinationBlend;
			blendState.ColorSourceBlend = blendStateToClone.ColorSourceBlend;
			blendState.ColorWriteChannels = blendStateToClone.ColorWriteChannels;
			blendState.ColorWriteChannels1 = blendStateToClone.ColorWriteChannels1;
			blendState.ColorWriteChannels2 = blendStateToClone.ColorWriteChannels2;
			blendState.ColorWriteChannels3 = blendStateToClone.ColorWriteChannels3;
			blendState.MultiSampleMask = blendStateToClone.MultiSampleMask;
			blendState.Name = blendStateToClone.Name;
			blendState.Tag = blendStateToClone.Tag;
			return blendState;
		}

		/// <summary>
		/// Returns a clone of the given DepthStencilState.
		/// Read-only properties, such as the GraphicsDevice property, are not copied though.
		/// </summary>
		/// <param name="depthStencilStateToClone">The DepthStencilState to clone.</param>
		public static DepthStencilState CloneDepthStencilState(DepthStencilState depthStencilStateToClone)
		{
			DepthStencilState depthStencilState = new DepthStencilState();
			depthStencilState.CounterClockwiseStencilFail = depthStencilStateToClone.CounterClockwiseStencilFail;
			depthStencilState.CounterClockwiseStencilDepthBufferFail = depthStencilStateToClone.CounterClockwiseStencilDepthBufferFail;
			depthStencilState.CounterClockwiseStencilFunction = depthStencilStateToClone.CounterClockwiseStencilFunction;
			depthStencilState.CounterClockwiseStencilPass = depthStencilStateToClone.CounterClockwiseStencilPass;
			depthStencilState.DepthBufferEnable = depthStencilStateToClone.DepthBufferEnable;
			depthStencilState.DepthBufferFunction = depthStencilStateToClone.DepthBufferFunction;
			depthStencilState.DepthBufferWriteEnable = depthStencilStateToClone.DepthBufferWriteEnable;
			depthStencilState.Name = depthStencilStateToClone.Name;
			depthStencilState.ReferenceStencil = depthStencilStateToClone.ReferenceStencil;
			depthStencilState.StencilDepthBufferFail = depthStencilStateToClone.StencilDepthBufferFail;
			depthStencilState.StencilEnable = depthStencilStateToClone.StencilEnable;
			depthStencilState.StencilFail = depthStencilStateToClone.StencilFail;
			depthStencilState.StencilFunction = depthStencilStateToClone.StencilFunction;
			depthStencilState.StencilMask = depthStencilStateToClone.StencilMask;
			depthStencilState.StencilPass = depthStencilStateToClone.StencilPass;
			depthStencilState.StencilWriteMask = depthStencilStateToClone.StencilWriteMask;
			depthStencilState.Tag = depthStencilStateToClone.Tag;
			depthStencilState.TwoSidedStencilMode = depthStencilStateToClone.TwoSidedStencilMode;
			return depthStencilState;
		}

		/// <summary>
		/// Returns a clone of the given RasterizerState.
		/// Read-only properties, such as the GraphicsDevice property, are not copied though.
		/// </summary>
		/// <param name="rasterizerStateToClone">The RasterizerState to clone.</param>
		public static RasterizerState CloneRasterizerState(RasterizerState rasterizerStateToClone)
		{
			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = rasterizerStateToClone.CullMode;
			rasterizerState.DepthBias = rasterizerStateToClone.DepthBias;
			rasterizerState.FillMode = rasterizerStateToClone.FillMode;
			rasterizerState.MultiSampleAntiAlias = rasterizerStateToClone.MultiSampleAntiAlias;
			rasterizerState.Name = rasterizerStateToClone.Name;
			rasterizerState.ScissorTestEnable = rasterizerStateToClone.ScissorTestEnable;
			rasterizerState.SlopeScaleDepthBias = rasterizerStateToClone.SlopeScaleDepthBias;
			rasterizerState.Tag = rasterizerStateToClone.Tag;
			return rasterizerState;
		}

		/// <summary>
		/// Returns a clone of the given SamplerState.
		/// Read-only properties, such as the GraphicsDevice property, are not copied though.
		/// </summary>
		/// <param name="samplerStateToClone">The SamplerState to clone.</param>
		public static SamplerState CloneSamplerState(SamplerState samplerStateToClone)
		{
			SamplerState samplerState = new SamplerState();
			samplerState.AddressU = samplerStateToClone.AddressU;
			samplerState.AddressV = samplerStateToClone.AddressV;
			samplerState.AddressW = samplerStateToClone.AddressW;
			samplerState.Filter = samplerStateToClone.Filter;
			samplerState.MaxAnisotropy = samplerStateToClone.MaxAnisotropy;
			samplerState.MaxMipLevel = samplerStateToClone.MaxMipLevel;
			samplerState.MipMapLevelOfDetailBias = samplerStateToClone.MipMapLevelOfDetailBias;
			samplerState.Name = samplerStateToClone.Name;
			samplerState.Tag = samplerStateToClone.Tag;
			return samplerState;
		}
	}
	/// <summary>
	/// The Techniques provided by the DPSF Default Effect.
	/// </summary>
	public enum DPSFDefaultEffectTechniques
	{
		/// <summary>
		/// The default technique used to display particles as sprites.
		/// </summary>
		Sprites,
		/// <summary>
		/// The default technique used to display particles as colored quads.
		/// </summary>
		Quads,
		/// <summary>
		/// The default technique used to display particles as textured quads.
		/// </summary>
		TexturedQuads,
		/// <summary>
		/// An experimental technique used to display particles as textured quads, doing the color blending using premultiplied colors.
		/// </summary>
		TexturedQuadsExperimental
	}
	/// <summary>
	/// The Default Effect provided by DPSF.
	/// </summary>
	public class DPSFDefaultEffect : Effect
	{
		/// <summary>
		/// The list of valid DPSF Default Effect configurations
		/// </summary>
		public enum DPSFDefaultEffectConfigurations
		{
			/// <summary>
			/// Windows HiDef configuration.
			/// </summary>
			WindowsHiDef,
			/// <summary>
			/// Windows Reach configuration.
			/// </summary>
			WindowsReach,
			/// <summary>
			/// Xbox 360 HiDef configuration.
			/// </summary>
			Xbox360HiDef
		}

		private EffectParameter _colorBlendAmountParameter;

		private EffectParameter _textureParameter;

		private EffectParameter _worldParameter;

		private EffectParameter _viewParameter;

		private EffectParameter _projectionParameter;

		/// <summary>
		/// Holds this effects configuration in case we need to clone it.
		/// </summary>
		private DPSFDefaultEffectConfigurations _configuration;

		/// <summary>
		/// How much of the vertex Color should be blended in with the Texture's Color.
		/// <para>0.0 = use Texture's color, 1.0 = use specified color. Default is 0.5.</para>
		/// </summary>
		public float ColorBlendAmount
		{
			get
			{
				return _colorBlendAmountParameter.GetValueSingle();
			}
			set
			{
				float num = value;
				if (num > 1f)
				{
					num = 1f;
				}
				if (num < 0f)
				{
					num = 0f;
				}
				_colorBlendAmountParameter.SetValue(num);
			}
		}

		/// <summary>
		/// The texture to use to draw the particles.
		/// </summary>
		public Texture2D Texture
		{
			get
			{
				return _textureParameter.GetValueTexture2D();
			}
			set
			{
				_textureParameter.SetValue(value);
			}
		}

		/// <summary>
		/// The World matrix.
		/// </summary>
		public Matrix World
		{
			get
			{
				return _worldParameter.GetValueMatrix();
			}
			set
			{
				_worldParameter.SetValue(value);
			}
		}

		/// <summary>
		/// The View matrix.
		/// </summary>
		public Matrix View
		{
			get
			{
				return _viewParameter.GetValueMatrix();
			}
			set
			{
				_viewParameter.SetValue(value);
			}
		}

		/// <summary>
		/// The Projection matrix.
		/// </summary>
		public Matrix Projection
		{
			get
			{
				return _projectionParameter.GetValueMatrix();
			}
			set
			{
				_projectionParameter.SetValue(value);
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="graphicsDevice">The Graphics Device to load the effect with.</param>
		/// <param name="configuration">The effect configuration to load (i.e. Windows HiDef, Xbox 360 Reach, etc.)</param>
		public DPSFDefaultEffect(GraphicsDevice graphicsDevice, DPSFDefaultEffectConfigurations configuration)
			: base(graphicsDevice, configuration switch
			{
				DPSFDefaultEffectConfigurations.WindowsReach => DPSFResources.DPSFDefaultEffectWindowsReach, 
				DPSFDefaultEffectConfigurations.WindowsHiDef => DPSFResources.DPSFDefaultEffectWindowsHiDef, 
				_ => DPSFResources.DPSFDefaultEffectXbox360HiDef, 
			})
		{
			_colorBlendAmountParameter = Parameters["xColorBlendAmount"];
			_textureParameter = Parameters["xTexture"];
			_worldParameter = Parameters["xWorld"];
			_viewParameter = Parameters["xView"];
			_projectionParameter = Parameters["xProjection"];
			_configuration = configuration;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.DPSFDefaultEffect" /> class.
		/// </summary>
		/// <param name="effectToClone">The effect to clone.</param>
		public DPSFDefaultEffect(DPSFDefaultEffect effectToClone)
			: this(effectToClone.GraphicsDevice, effectToClone._configuration)
		{
			ColorBlendAmount = effectToClone.ColorBlendAmount;
			Texture = effectToClone.Texture;
			World = effectToClone.World;
			View = effectToClone.View;
			Projection = effectToClone.Projection;
		}

		/// <summary>
		/// Creates and returns a clone of this DPSFDefaultEffect instance.
		/// </summary>
		public override Effect Clone()
		{
			return new DPSFDefaultEffect(this);
		}
	}
	/// <summary>
	/// The Default Animated Textured Quad Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultAnimatedTexturedQuadParticleSystem<Particle, Vertex> : DPSFDefaultTexturedQuadTextureCoordinatesParticleSystem<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultAnimatedTexturedQuadParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Updates the Animation, as well as the Particle's Texture Coordinates to match the Animation
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleAnimationAndTextureCoordinates(DefaultAnimatedTexturedQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Animation.Update(fElapsedTimeInSeconds);
			Rectangle currentPicturesTextureCoordinates = cParticle.Animation.CurrentPicturesTextureCoordinates;
			cParticle.SetTextureCoordinates(currentPicturesTextureCoordinates, Texture.Width, Texture.Height);
		}

		/// <summary>
		/// Updates the Particle to be removed from the Particle System once the Animation finishes Playing
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleToDieOnceAnimationFinishesPlaying(DefaultAnimatedTexturedQuadParticle cParticle, float fElapsedTimeInSeconds)
		{
			if (cParticle.Animation.CurrentAnimationIsDonePlaying)
			{
				cParticle.Lifetime = 1E-05f;
				cParticle.NormalizedElapsedTime = 1f;
			}
		}
	}
	/// <summary>
	/// The Default Animated Textured Quad Particle System to inherit from, which uses Default Animated Textured Quad Particles
	/// </summary>
	public class DefaultAnimatedTexturedQuadParticleSystem : DPSFDefaultAnimatedTexturedQuadParticleSystem<DefaultAnimatedTexturedQuadParticle, DefaultTexturedQuadParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultAnimatedTexturedQuadParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Animated Quad Particle System
	/// </summary>
	public class DefaultAnimatedTexturedQuadParticle : DefaultTextureQuadTextureCoordinatesParticle
	{
		/// <summary>
		/// Class to hold this Particle's Animation information
		/// </summary>
		public Animations Animation;

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			Animation = new Animations();
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultAnimatedTexturedQuadParticle defaultAnimatedTexturedQuadParticle = (DefaultAnimatedTexturedQuadParticle)ParticleToCopy;
			base.CopyFrom(ParticleToCopy);
			Animation.CopyFrom(defaultAnimatedTexturedQuadParticle.Animation);
		}
	}
	/// <summary>
	/// The Type of Particles that the Particle Systems can draw. Different Particle Types are drawn in 
	/// different ways. For example, four vertices are required to draw a Quad, and only one is required 
	/// to draw a Point Sprite.
	/// </summary>
	public enum ParticleTypes
	{
		/// <summary>
		/// This is the default settings when we don't know what Type of Particles are going to be used yet.
		/// A particle system is not considered Initialized until the Particle Type does not equal this.
		/// </summary>
		None = 0,
		/// <summary>
		/// Use this when you do not want to draw your particles to the screen, as no vertex buffer will be
		/// created, saving memory. Also, the Draw() function will do nothing when this Particle Type is used.
		/// This Particle Type is useful when you just want to collect and analyze particle information without
		/// visualizing the particles.
		/// </summary>
		NoDisplay = 1,
		/// <summary>
		/// Texture in 2D screen coordinates. Drawn using a SpriteBatch object. Only allows for 2D roll
		/// rotations, always faces the camera, and must use a Texture.
		/// </summary>
		Sprite = 3,
		/// <summary>
		/// Four vertices in 3D world coordinates. Allows for rotations in all 3 dimensions, does
		/// not have to always face the camera, may be skewed into any quadrilateral, such as
		/// a square, rectangle, or trapezoid, and do not use a Texture.
		/// </summary>
		Quad = 5,
		/// <summary>
		/// Four vertices in 3D world coordinates. Allows for rotations in all 3 dimensions, does
		/// not have to always face the camera, may be skewed into any quadrilateral, such as
		/// a square, rectangle, or trapezoid, and must use a Texture.
		/// </summary>
		TexturedQuad = 6
	}
	/// <summary>
	/// Class to hold all of the SpriteBatch-specific drawing Settings
	/// </summary>
	public class SpriteBatchSettings
	{
		/// <summary>
		/// The Sort Mode to use in the SpriteBatch.Begin() function call.
		/// </summary>
		public SpriteSortMode SortMode;

		/// <summary>
		/// The Transformation Matrix used in the SpriteBatch.Begin() function call.
		/// </summary>
		public Matrix TransformationMatrix = Matrix.Identity;
	}
	/// <summary>
	/// Class to hold all of the drawing Settings
	/// </summary>
	public class RenderProperties
	{
		/// <summary>
		/// Get / Set the BlendState to use when drawing the particles.
		/// <para>Default value is BlendState.AlphaBlend.</para>
		/// </summary>
		public BlendState BlendState { get; set; }

		/// <summary>
		/// Get / Set the DepthStencilState to use when drawing the particles.
		/// <para>Default value is DepthStencilState.DepthRead.</para>
		/// </summary>
		public DepthStencilState DepthStencilState { get; set; }

		/// <summary>
		/// Get / Set the RasterizerState to use when drawing the particles.
		/// <para>Default value is RasterizerState.CullCounterClockwise.</para>
		/// </summary>
		public RasterizerState RasterizerState { get; set; }

		/// <summary>
		/// Get / Set the SamplerState to use when drawing the particles.
		/// <para>Default value is SamplerState.LinearClamp.</para>
		/// </summary>
		public SamplerState SamplerState { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.RenderProperties" /> class, setting each property to its default value.
		/// </summary>
		public RenderProperties()
		{
			ResetToDefaults();
		}

		/// <summary>
		/// Resets each of the render properties to their default values.
		/// </summary>
		public void ResetToDefaults()
		{
			BlendState = DPSFHelper.CloneBlendState(BlendState.AlphaBlend);
			DepthStencilState = DPSFHelper.CloneDepthStencilState(DepthStencilState.DepthRead);
			RasterizerState = DPSFHelper.CloneRasterizerState(RasterizerState.CullCounterClockwise);
			SamplerState = DPSFHelper.CloneSamplerState(SamplerState.LinearClamp);
		}
	}
	/// <summary>
	/// The Default Animated Sprite Particle System class
	/// </summary>
	/// <typeparam name="Particle">The Particle class to use</typeparam>
	/// <typeparam name="Vertex">The Vertex Format to use</typeparam>
	public class DPSFDefaultAnimatedSprite3DBillboardParticleSystem<Particle, Vertex> : DPSFDefaultSprite3DBillboardTextureCoordinates<Particle, Vertex> where Particle : DPSFParticle, new() where Vertex : struct, IDPSFParticleVertex
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DPSFDefaultAnimatedSprite3DBillboardParticleSystem(Game cGame)
			: base(cGame)
		{
		}

		/// <summary>
		/// Updates the Animation, as well as the Particle's Texture Coordinates to match the Animation
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleAnimationAndTextureCoordinates(DefaultAnimatedSprite3DBillboardParticle cParticle, float fElapsedTimeInSeconds)
		{
			cParticle.Animation.Update(fElapsedTimeInSeconds);
			cParticle.TextureCoordinates = cParticle.Animation.CurrentPicturesTextureCoordinates;
		}

		/// <summary>
		/// Updates the Particle to be removed from the Particle System once the Animation finishes Playing
		/// </summary>
		/// <param name="cParticle">The Particle to update</param>
		/// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
		protected void UpdateParticleToDieOnceAnimationFinishesPlaying(DefaultAnimatedSprite3DBillboardParticle cParticle, float fElapsedTimeInSeconds)
		{
			if (cParticle.Animation.CurrentAnimationIsDonePlaying)
			{
				cParticle.Lifetime = 1E-06f;
				cParticle.NormalizedElapsedTime = 1f;
			}
		}
	}
	/// <summary>
	/// The Default Animated 3D Billboard Sprite Particle System to inherit from, which uses Default Animated Sprite Particles
	/// </summary>
	public class DefaultAnimatedSprite3DBillboardParticleSystem : DPSFDefaultAnimatedSprite3DBillboardParticleSystem<DefaultAnimatedSprite3DBillboardParticle, DefaultSpriteParticleVertex>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cGame">Handle to the Game object being used. Pass in null for this 
		/// parameter if not using a Game object.</param>
		public DefaultAnimatedSprite3DBillboardParticleSystem(Game cGame)
			: base(cGame)
		{
		}
	}
	/// <summary>
	/// Particle used by the Default Animated Sprite 3D Billboard Particle System
	/// </summary>
	public class DefaultAnimatedSprite3DBillboardParticle : DefaultSprite3DBillboardTextureCoordinatesParticle
	{
		/// <summary>
		/// Class to hold this Particle's Animation information
		/// </summary>
		public Animations Animation;

		/// <summary>
		/// Resets the Particle variables to their default values
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			Animation = new Animations();
		}

		/// <summary>
		/// Deep copy all of the Particle properties
		/// </summary>
		/// <param name="ParticleToCopy">The Particle to Copy the properties from</param>
		public override void CopyFrom(DPSFParticle ParticleToCopy)
		{
			DefaultAnimatedSprite3DBillboardParticle defaultAnimatedSprite3DBillboardParticle = (DefaultAnimatedSprite3DBillboardParticle)ParticleToCopy;
			base.CopyFrom(defaultAnimatedSprite3DBillboardParticle);
			Animation.CopyFrom(defaultAnimatedSprite3DBillboardParticle.Animation);
		}
	}
}
namespace System.Collections.Generic
{
	/// <summary>
	/// Class used to extend other classes.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Removes all elements from the List that match the conditions defined by the specified predicate.
		/// </summary>
		/// <typeparam name="T">The type of elements held by the List.</typeparam>
		/// <param name="list">The List to remove the elements from.</param>
		/// <param name="match">The Predicate delegate that defines the conditions of the elements to remove.</param>
		public static int RemoveAll<T>(this List<T> list, Func<T, bool> match)
		{
			int num = 0;
			for (int num2 = list.Count - 1; num2 >= 0; num2--)
			{
				if (match(list[num2]))
				{
					list.RemoveAt(num2);
					num++;
				}
			}
			return num;
		}

		/// <summary>
		/// Returns true if the List contains elements that match the conditions defined by the specified predicate.
		/// </summary>
		/// <typeparam name="T">The type of elements held by the List.</typeparam>
		/// <param name="list">The List to search for a match in.</param>
		/// <param name="match">The Predicate delegate that defines the conditions of the elements to match against.</param>
		public static bool Exists<T>(this List<T> list, Func<T, bool> match)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (match(list[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
namespace DPSF
{
	/// <summary>
	/// Class used to automatically create new Particles in a Particle System
	/// </summary>
	public class ParticleEmitter
	{
		private Position3D mcPositionData;

		private Orientation3D mcOrientationData;

		private PivotPoint3D mcPivotPointData;

		private bool mbEnabled = true;

		private bool mbEmitParticlesAutomatically = true;

		private int miBurstNumberOfParticles;

		private float mfBurstTimeInSeconds;

		private float mfParticlesPerSecond;

		private float mfSecondsPerParticle;

		private float mfTimeElapsedSinceGeneratingLastParticle;

		/// <summary>
		/// Get / Set if the Emitter is able to Emit Particles or not.
		/// <para>NOTE: If this is false, not even Bursts will Emit Particles.</para>
		/// <para>NOTE: The Position, Orientation, and Pivot Data will still be updated when this is false.</para>
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mbEnabled;
			}
			set
			{
				mbEnabled = value;
			}
		}

		/// <summary>
		/// Get the Position Data (Position, Velocity, and Acceleration)
		/// </summary>
		public Position3D PositionData => mcPositionData;

		/// <summary>
		/// Get the Orientation Data (Orientation, Rotational Velocity, and Rotational Acceleration)
		/// </summary>
		public Orientation3D OrientationData => mcOrientationData;

		/// <summary>
		/// Get the Pivot Point Data (Pivot Point, Pivot Rotational Velocity, and Pivot Rotational Acceleration)
		/// </summary>
		public PivotPoint3D PivotPointData => mcPivotPointData;

		/// <summary>
		/// Get / Set if the Emitter should Emit Particles Automatically or not.
		/// <para>NOTE: Particles will only be emitted if the Emitter is Enabled.</para>
		/// </summary>
		public bool EmitParticlesAutomatically
		{
			get
			{
				return mbEmitParticlesAutomatically;
			}
			set
			{
				mbEmitParticlesAutomatically = value;
			}
		}

		/// <summary>
		/// Get / Set how many Particles should be emitted per Second
		/// </summary>
		public float ParticlesPerSecond
		{
			get
			{
				return mfParticlesPerSecond;
			}
			set
			{
				mfParticlesPerSecond = value;
				if (mfParticlesPerSecond == 0f)
				{
					mfSecondsPerParticle = 0f;
				}
				else
				{
					mfSecondsPerParticle = 1f / mfParticlesPerSecond;
				}
			}
		}

		/// <summary>
		/// Get / Set how many Particles the Emitter should Burst. The Emitter will emit
		/// Particles, at the speed corresponding to its Particles Per Second rate, until this amount 
		/// of Particles have been emitted.
		/// <para>NOTE: Bursts are only processed when the Emit Particles Automatically property is false.</para>
		/// <para>NOTE: Bursts will only emit Particles if the Emitter is Enabled.</para>
		/// <para>NOTE: This will be set to zero if a negative value is specified.</para>
		/// <para>NOTE: This will fire the BurstComplete event when it reaches (or is set to) zero.</para>
		/// </summary>
		public int BurstParticles
		{
			get
			{
				return miBurstNumberOfParticles;
			}
			set
			{
				if (value <= 0)
				{
					miBurstNumberOfParticles = 0;
					if (BurstComplete != null)
					{
						BurstComplete(this, null);
					}
				}
				else
				{
					miBurstNumberOfParticles = value;
				}
			}
		}

		/// <summary>
		/// Get / Set how long the Emitter should Burst for (in seconds). The Emitter will emit
		/// Particles, at the speed corresponding to its Particles Per Second rate, until this amount 
		/// of time in seconds has elapsed.
		/// <para>NOTE: Bursts are only processed when the Emit Particles Automatically property is false.</para>
		/// <para>NOTE: Bursts will only emit Particles if the Emitter is Enabled.</para>
		/// <para>NOTE: This will be set to zero if a negative value is specified.</para>
		/// <para>NOTE: This will fire the BurstComplete event when it reaches (or is set to) zero.</para>
		/// </summary>
		public float BurstTime
		{
			get
			{
				return mfBurstTimeInSeconds;
			}
			set
			{
				if (value <= 0f)
				{
					mfBurstTimeInSeconds = 0f;
					if (BurstComplete != null)
					{
						BurstComplete(this, null);
					}
				}
				else
				{
					mfBurstTimeInSeconds = value;
				}
			}
		}

		/// <summary>
		/// Raised when a Burst property reaches (or is set to) zero 
		/// </summary>
		public event EventHandler BurstComplete;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.ParticleEmitter" /> class.
		/// </summary>
		public ParticleEmitter()
		{
			mcPositionData = new Position3D();
			mcOrientationData = new Orientation3D();
			mcPivotPointData = new PivotPoint3D(mcPositionData, mcOrientationData);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DPSF.ParticleEmitter" /> class.
		/// </summary>
		/// <param name="emitterToCopy">The emitter to copy from.</param>
		public ParticleEmitter(ParticleEmitter emitterToCopy)
			: this()
		{
			CopyFrom(emitterToCopy);
		}

		/// <summary>
		/// Copies the given Emitter's values into this instance.
		/// </summary>
		/// <param name="emitterToCopy">The emitter to copy from.</param>
		public void CopyFrom(ParticleEmitter emitterToCopy)
		{
			mcPositionData.CopyFrom(emitterToCopy.mcPositionData);
			mcOrientationData.CopyFrom(emitterToCopy.mcOrientationData);
			mcPivotPointData = new PivotPoint3D(mcPositionData, mcOrientationData);
			mbEnabled = emitterToCopy.mbEnabled;
			mbEmitParticlesAutomatically = emitterToCopy.mbEmitParticlesAutomatically;
			miBurstNumberOfParticles = emitterToCopy.miBurstNumberOfParticles;
			mfBurstTimeInSeconds = emitterToCopy.mfBurstTimeInSeconds;
			mfParticlesPerSecond = emitterToCopy.mfParticlesPerSecond;
			mfSecondsPerParticle = emitterToCopy.mfSecondsPerParticle;
			mfTimeElapsedSinceGeneratingLastParticle = emitterToCopy.mfTimeElapsedSinceGeneratingLastParticle;
			BurstComplete = emitterToCopy.BurstComplete;
		}

		/// <summary>
		/// Updates the Emitter's Position and Orientation according to its 
		/// Velocities and Accelerations, and returns how many Particles should 
		/// be emitted this frame.
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How long (in seconds) it has been 
		/// since this function was called</param>
		/// <returns>Returns the number of Particles that should be emitted</returns>
		public int UpdateAndGetNumberOfParticlesToEmit(float fElapsedTimeInSeconds)
		{
			PositionData.Update(fElapsedTimeInSeconds);
			OrientationData.Update(fElapsedTimeInSeconds);
			PivotPointData.Update(fElapsedTimeInSeconds);
			int num = 0;
			if (mfParticlesPerSecond > 0f && mbEnabled)
			{
				if (mbEmitParticlesAutomatically)
				{
					num = CalculateHowManyParticlesToEmit(fElapsedTimeInSeconds);
				}
				else if (mfBurstTimeInSeconds > 0f)
				{
					float num2 = fElapsedTimeInSeconds;
					if (num2 > mfBurstTimeInSeconds)
					{
						num2 = mfBurstTimeInSeconds;
					}
					num = CalculateHowManyParticlesToEmit(num2);
					BurstTime -= num2;
				}
				else if (miBurstNumberOfParticles > 0)
				{
					num = CalculateHowManyParticlesToEmit(fElapsedTimeInSeconds);
					if (num > miBurstNumberOfParticles)
					{
						num = miBurstNumberOfParticles;
					}
					BurstParticles -= num;
				}
			}
			return num;
		}

		/// <summary>
		/// Calculates how many Particles should be emitted based on the amount of Time Elapsed
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">How much Time has Elapsed (in seconds) since the last Update</param>
		/// <returns>Returns how many Particles should be emitted</returns>
		private int CalculateHowManyParticlesToEmit(float fElapsedTimeInSeconds)
		{
			int num = 0;
			mfTimeElapsedSinceGeneratingLastParticle += fElapsedTimeInSeconds;
			float num2 = mfTimeElapsedSinceGeneratingLastParticle / mfSecondsPerParticle;
			num = (int)Math.Floor(num2);
			mfTimeElapsedSinceGeneratingLastParticle = (num2 - (float)num) * mfSecondsPerParticle;
			return num;
		}
	}
	/// <summary>
	/// Class to hold and update an object's 2D Pivot Point (point to rotate around), Pivot Velocity, and 
	/// Pivot Acceleration. This class requires a Position2D object, and optionally a Orientation2D object,
	/// that should be affected by rotations around the Pivot Point.
	/// </summary>
	public class PivotPoint2D
	{
		/// <summary>
		/// The 2D Pivot Point that the object should rotate around.
		/// <para>NOTE: This only has effect when Rotational Pivot Velocity / Acceleration are used.</para>
		/// </summary>
		public Vector2 PivotPoint = Vector2.Zero;

		/// <summary>
		/// The object's Rotational Velocity around the Pivot Point (Position change).
		/// <para>NOTE: Rotations are specified in radians.</para>
		/// </summary>
		public float PivotRotationalVelocity;

		/// <summary>
		/// The object's Rotational Acceleration around the Pivot Point (Position change).
		/// <para>NOTE: Rotations are specified in radians.</para>
		/// </summary>
		public float PivotRotationalAcceleration;

		private Position2D mcPositionData;

		private Orientation2D mcOrientationData;

		private bool mbRotateOrientationToo = true;

		/// <summary>
		/// Get / Set the Position2D object that the Pivot Point should affect
		/// </summary>
		public Position2D PositionData
		{
			get
			{
				return mcPositionData;
			}
			set
			{
				mcPositionData = value;
			}
		}

		/// <summary>
		/// Get / Set the Orientation2D object that the Pivot Point should affect
		/// </summary>
		public Orientation2D OrientationData
		{
			get
			{
				return mcOrientationData;
			}
			set
			{
				mcOrientationData = value;
			}
		}

		/// <summary>
		/// Specify if the Update() function should Rotate the object's Orientation too when it
		/// rotates the object around the Pivot Point
		/// </summary>
		public bool RotateOrientationToo
		{
			get
			{
				return mbRotateOrientationToo;
			}
			set
			{
				mbRotateOrientationToo = value;
			}
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="cPivotPointToCopy">The PivotPoint2D object to copy</param>
		public PivotPoint2D(PivotPoint2D cPivotPointToCopy)
		{
			CopyFrom(cPivotPointToCopy);
		}

		/// <summary>
		/// Copies the given PivotPoint2D object's data into this object's data
		/// </summary>
		/// <param name="cPivotPointToCopy"></param>
		public void CopyFrom(PivotPoint2D cPivotPointToCopy)
		{
			PivotPoint = cPivotPointToCopy.PivotPoint;
			PivotRotationalVelocity = cPivotPointToCopy.PivotRotationalVelocity;
			PivotRotationalAcceleration = cPivotPointToCopy.PivotRotationalAcceleration;
			mbRotateOrientationToo = cPivotPointToCopy.RotateOrientationToo;
			mcPositionData = new Position2D(cPivotPointToCopy.PositionData);
			mcOrientationData = new Orientation2D(cPivotPointToCopy.OrientationData);
		}

		/// <summary>
		/// Explicit Constructor. Set the Position2D object that should be affected by rotations around
		/// this Pivot Point.
		/// </summary>
		/// <param name="cPosition">Handle to the Position2D object to update</param>
		public PivotPoint2D(Position2D cPosition)
		{
			mcPositionData = cPosition;
			mcOrientationData = null;
		}

		/// <summary>
		/// Explicit Constructor. Set the Position2D and Orientation2D objects that should be affected by 
		/// rotational around this Pivot Point.
		/// </summary>
		/// <param name="cPosition">Handle to the Position2D object to update</param>
		/// <param name="cOrientation">Handle to the Orienetation2D object to update</param>
		public PivotPoint2D(Position2D cPosition, Orientation2D cOrientation)
		{
			mcPositionData = cPosition;
			mcOrientationData = cOrientation;
		}

		/// <summary>
		/// Rotates the object about its center, changing its Orientation, as well as around the 
		/// specified Pivot Point, changing its Position
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around</param>
		public void RotatePositionAndOrientation(float fRotation, Vector2 sPivotPoint)
		{
			if (mcOrientationData != null)
			{
				mcOrientationData.Rotate(fRotation);
			}
			RotatePosition(fRotation, sPivotPoint);
		}

		/// <summary>
		/// Rotates the object about its center, changing its Orientation, as well as around the 
		/// specified 3D Pivot Point, changing its 2D Position.
		/// <para>NOTE: The Pivot Point's Z-value is ignored.</para>
		/// <para>NOTE: This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around. 
		/// NOTE: The Z-value is ignored, since this is a 2D rotation.</param>
		public void RotatePositionAndOrientationVector3(float fRotation, Vector3 sPivotPoint)
		{
			RotatePositionAndOrientation(fRotation, new Vector2(sPivotPoint.X, sPivotPoint.Y));
		}

		/// <summary>
		/// Rotates the object around the specified Pivot Point, changing its Position, without 
		/// changing its Orientation.
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around</param>
		public void RotatePosition(float fRotation, Vector2 sPivotPoint)
		{
			mcPositionData.Position = RotatePosition(fRotation, sPivotPoint, mcPositionData.Position);
		}

		/// <summary>
		/// Rotates the object around the specified Pivot Point, changing its Position, without 
		/// changing its Orientation.
		/// <para>NOTE: The Pivot Point's Z-value is ignored.</para>
		/// <para>NOTE: This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around.
		/// NOTE: The Z-value is ignored, since this is a 2D rotation.</param>
		public void RotatePositionVector3(float fRotation, Vector3 sPivotPoint)
		{
			RotatePosition(fRotation, new Vector2(sPivotPoint.X, sPivotPoint.Y));
		}

		/// <summary>
		/// Update the Position and Orientation according to the Pivot Rotational Velocity / Acceleration
		/// </summary>
		/// <param name="fElapsedTimeInSeconds">The time Elapsed in Seconds since the last Update</param>
		public void Update(float fElapsedTimeInSeconds)
		{
			if (PivotRotationalAcceleration != 0f)
			{
				PivotRotationalVelocity += PivotRotationalAcceleration * fElapsedTimeInSeconds;
			}
			if (PivotRotationalVelocity != 0f)
			{
				float fRotation = PivotRotationalVelocity * fElapsedTimeInSeconds;
				if (mbRotateOrientationToo)
				{
					RotatePositionAndOrientation(fRotation, PivotPoint);
				}
				else
				{
					RotatePosition(fRotation, PivotPoint);
				}
			}
		}

		/// <summary>
		/// Rotates the given Position and Orientation around the Pivot Point, changing the Position and Orientation
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around</param>
		/// <param name="srPosition">The Position of the object to be modified</param>
		/// <param name="frOrientation">The Orientation (rotation) of the object to be modified</param>
		public static void RotatePositionAndOrientation(float fRotation, Vector2 sPivotPoint, ref Vector2 srPosition, ref float frOrientation)
		{
			frOrientation = Orientation2D.Rotate(fRotation, frOrientation);
			srPosition = RotatePosition(fRotation, sPivotPoint, srPosition);
		}

		/// <summary>
		/// Rotates the given Position and Orientation around the Pivot Point, changing the Position and Orientation.
		/// <para>NOTE: The Pivot Point and Position's Z-values are ignored.</para>
		/// <para>NOTE: This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to apply to the object</param>
		/// <param name="sPivotPoint">The Point to rotate the object around.
		/// NOTE: The Z-value is ignored, since this is a 2D rotation.</param>
		/// <param name="srPosition">The Position of the object to be modified.
		/// NOTE: The Z-value is ignored and will not be changed, since this is a 2D rotation.</param>
		/// <param name="frOrientation">The Orientation (rotation) of the object to be modified</param>
		public static void RotatePositionAndOrientationVector3(float fRotation, Vector3 sPivotPoint, ref Vector3 srPosition, ref float frOrientation)
		{
			Vector2 srPosition2 = new Vector2(srPosition.X, srPosition.Y);
			RotatePositionAndOrientation(fRotation, new Vector2(sPivotPoint.X, sPivotPoint.Y), ref srPosition2, ref frOrientation);
			srPosition.X = srPosition2.X;
			srPosition.Y = srPosition2.Y;
		}

		/// <summary>
		/// Returns the new Position after Rotating the given Position around the specified Pivot Point
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to rotate around the Pivot Point by</param>
		/// <param name="sPivotPoint">The Point to Rotate around</param>
		/// <param name="sPosition">The current Position of the object</param>
		/// <returns>Returns the new Position after Rotating the given Position around the specified Pivot Point</returns>
		public static Vector2 RotatePosition(float fRotation, Vector2 sPivotPoint, Vector2 sPosition)
		{
			if (sPivotPoint != sPosition)
			{
				Vector2 vector = sPosition - sPivotPoint;
				float num = (float)Math.Atan(vector.Y / vector.X);
				float num2 = num + fRotation;
				float num3 = vector.Length();
				if (vector.X < 0f)
				{
					num3 *= -1f;
				}
				float x = (float)Math.Cos(num2) * num3;
				float y = (float)Math.Sin(num2) * num3;
				sPosition = sPivotPoint + new Vector2(x, y);
			}
			return sPosition;
		}

		/// <summary>
		/// Returns the new Position after Rotating the given Position around the specified Pivot Point.
		/// <para>NOTE: The Pivot Point and Position's Z-values are ignored.</para>
		/// <para>This function is provided for convenience when using 3D Vectors in 2D coordinate systems.</para>
		/// </summary>
		/// <param name="fRotation">The Rotation in radians to rotate around the Pivot Point by</param>
		/// <param name="sPivotPoint">The Point to Rotate around.
		/// NOTE: The Z-value is ignored, since this is a 2D rotation.</param>
		/// <param name="sPosition">The current Position of the object.
		/// NOTE: The Z-value is ignored and will not be changed, since this is a 2D rotation.</param>
		/// <returns>Returns the new Position after Rotating the given Position around the specified Pivot Point.</returns>
		public static Vector3 RotatePositionVector3(float fRotation, Vector3 sPivotPoint, Vector3 sPosition)
		{
			Vector2 vector = RotatePosition(fRotation, new Vector2(sPivotPoint.X, sPivotPoint.Y), new Vector2(sPosition.X, sPosition.Y));
			return new Vector3(vector.X, vector.Y, sPosition.Z);
		}
	}
}
