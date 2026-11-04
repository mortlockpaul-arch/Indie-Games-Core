namespace B;

internal class g
{
	internal class nNB
	{
		private string fNB;

		private string fNa;

		private string fNg;

		internal string DisplayName => fNB;

		internal string DRMProductName => fNa;

		internal string FileName => "SunBurn-Deploy.xnb";

		internal nNB(string P_0, string P_1, string P_2)
		{
			fNB = P_0;
			fNa = P_1;
			fNg = P_2;
		}
	}

	internal enum nNa
	{
		SunBurn_Indie,
		SunBurn_Pro,
		SunBurn_Studio,
		SunBurn_ProNonCom
	}

	private const string fNB = "SunBurn-Deploy.xnb";

	internal static nNB[] fNa = new nNB[4]
	{
		new nNB("SunBurn Indie", "SunBurn Pro", "SunBurn2-Indie.auth"),
		new nNB("SunBurn Pro", "SunBurn Community", "SunBurn2-Pro.auth"),
		new nNB("SunBurn Studio", "SunBurn Studio", "SunBurn2-Studio.auth"),
		new nNB("SunBurn Pro Non-Commercial", "SunBurn ProNonCom", "SunBurn2-Pro-NonCommercial.auth")
	};

	private static string fNg = null;

	internal static string ActivationPath
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
			if (string.IsNullOrEmpty(fNg))
			{
				fNg = "";
				return;
			}
			char c = fNg[fNg.Length - 1];
			if (c != '\\' && c != '/')
			{
				fNg += "\\";
			}
		}
	}
}
