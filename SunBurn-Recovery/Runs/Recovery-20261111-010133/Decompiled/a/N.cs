namespace a;

internal class N
{
	internal class w8a
	{
		private string l8a;

		private string l8_u0013_;

		private string l8N;

		internal string DisplayName => l8a;

		internal string DRMProductName => l8_u0013_;

		internal string FileName => "SunBurn-Deploy.xnb";

		internal w8a(string param_0, string param_1, string param_2)
		{
			l8a = param_0;
			l8_u0013_ = param_1;
			l8N = param_2;
		}
	}

	internal enum w8_u0013_
	{
		SunBurn_Indie,
		SunBurn_Pro,
		SunBurn_Studio,
		SunBurn_ProNonCom
	}

	private const string l8a = "SunBurn-Deploy.xnb";

	internal static w8a[] l8_u0013_ = new w8a[4]
	{
		new w8a("SunBurn Indie", "SunBurn Pro", "SunBurn2-Indie.auth"),
		new w8a("SunBurn Pro", "SunBurn Community", "SunBurn2-Pro.auth"),
		new w8a("SunBurn Studio", "SunBurn Studio", "SunBurn2-Studio.auth"),
		new w8a("SunBurn Pro Non-Commercial", "SunBurn ProNonCom", "SunBurn2-Pro-NonCommercial.auth")
	};

	private static string l8N = null;

	internal static string ActivationPath
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
			if (string.IsNullOrEmpty(l8N))
			{
				l8N = "";
				return;
			}
			char c = l8N[l8N.Length - 1];
			if (c != '\\' && c != '/')
			{
				l8N += "\\";
			}
		}
	}
}
