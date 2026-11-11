using System;

namespace Y
{
	internal static class a
	{
		public const string Name = "";

		public const string ModelFile = "";

		public const string ModelMesh = "";

		public const string UpdateType = "";

		public const string Visibility = "";

		public const string Position = "";

		public const string RenderingErrors = "";

		public const string StaticLightingType = "";

		public const string CustomStaticLightingColor = "";

		public const string AffectedByGravity = "";

		public const string CollisionType = "";

		public const string HullType = "";

		public const string Mass = "";

		public const string AudioType = "";

		public const string AudioVolume = "";

		public const string AudioLoop = "";

		public const string AudioPlayWhenLoaded = "";

		public const string AudioRadius = "";

		public const string AudioSoundEffect = "";
	}
}
namespace y
{
	internal class a
	{
		internal enum w8a
		{
			Note,
			Warning,
			Error
		}

		internal static void wY(string param_0, w8a param_1)
		{
			switch (param_1)
			{
			}
		}

		internal static void w_u0005_<T>(ref T param_0) where T : IDisposable
		{
			if (param_0 != null)
			{
				param_0.Dispose();
				param_0 = default;
			}
		}
	}
}
