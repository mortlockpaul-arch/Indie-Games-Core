using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DigitalRune.Collections;
using DigitalRune.Undo;

namespace A;

internal sealed class cf0df5c209e083b66f7ecb11929369cb6 : IUndoableOperation
{
	private readonly List<IUndoableOperation> cbe89e4ea9cb67a3ab2e3a5189fe0c096 = new List<IUndoableOperation>();

	[CompilerGenerated]
	private object cb54a2b8674f351784981414d191f815b;

	public object Description
	{
		[CompilerGenerated]
		get
		{
			return cb54a2b8674f351784981414d191f815b;
		}
		[CompilerGenerated]
		private set
		{
			cb54a2b8674f351784981414d191f815b = value;
		}
	}

	public cf0df5c209e083b66f7ecb11929369cb6(Deque<IUndoableOperation> cf31d5219434c8ece735ce4808292c738, int c87a1180d325336bbf3d7393600db369d, object cf1ac041db7f9e7a7a6dce3e57f3c2b5b)
	{
		if (cf31d5219434c8ece735ce4808292c738 == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf0df5c209e083b66f7ecb11929369cb6._002Ector(Deque<IUndoableOperation>, int, object));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2321));
				}
			}
		}
		Description = cf1ac041db7f9e7a7a6dce3e57f3c2b5b;
		if (c87a1180d325336bbf3d7393600db369d > cf31d5219434c8ece735ce4808292c738.Count)
		{
			c87a1180d325336bbf3d7393600db369d = cf31d5219434c8ece735ce4808292c738.Count;
		}
		for (int i = 0; i < c87a1180d325336bbf3d7393600db369d; i++)
		{
			cbe89e4ea9cb67a3ab2e3a5189fe0c096.Add(cf31d5219434c8ece735ce4808292c738.DequeueHead());
		}
	}

	public void Undo()
	{
		for (int i = 0; i < cbe89e4ea9cb67a3ab2e3a5189fe0c096.Count; i++)
		{
			cbe89e4ea9cb67a3ab2e3a5189fe0c096[i].Undo();
		}
	}

	public void Do()
	{
		for (int num = cbe89e4ea9cb67a3ab2e3a5189fe0c096.Count - 1; num >= 0; num--)
		{
			cbe89e4ea9cb67a3ab2e3a5189fe0c096[num].Do();
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(cf0df5c209e083b66f7ecb11929369cb6.Do());
			}
			return;
		}
	}
}
