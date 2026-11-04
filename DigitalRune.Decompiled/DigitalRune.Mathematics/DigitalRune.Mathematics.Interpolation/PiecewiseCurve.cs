using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using A;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Represents a curve that is defined by piecewise interpolation of curve keys (control points).
///       </summary>
/// <typeparam name="TParam">
///       The type of the curve parameter (usually <see cref="T:System.Single" /> or <see cref="T:System.Double" />).
///       </typeparam>
/// <typeparam name="TPoint">
///       The type of the curve points (such as <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />, <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />, etc.).
///       </typeparam>
/// <typeparam name="TCurveKey">
///       The type of the curve key. (A type derived from <see cref="T:DigitalRune.Mathematics.Interpolation.CurveKey`2" />.)
///       </typeparam>
[Serializable]
public abstract class PiecewiseCurve<TParam, TPoint, TCurveKey> : Collection<TCurveKey>, ICurve<TParam, TPoint> where TCurveKey : CurveKey<TParam, TPoint>
{
	/// <summary>
	///       Gets or sets a value that defines how the curve looks after the last curve key.
	///       </summary>
	public CurveLoopType PostLoop { get; set; }

	/// <summary>
	///       Gets or sets a value that defines how the curve looks before the first path key.
	///       </summary>
	public CurveLoopType PreLoop { get; set; }

	/// <summary>
	///       Gets or sets a value indicating whether the curve ends are smoothed.
	///       </summary>
	public bool SmoothEnds { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.PiecewiseCurve`3" /> class</summary>
	protected PiecewiseCurve()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>
	///       The curve point. (If the curve does not contain any points a vector with NaN values is
	///       returned.)
	///       </returns>
	public abstract TPoint GetPoint(TParam parameter);

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public abstract TPoint GetTangent(TParam parameter);

	/// <summary>
	///       Computes the approximated length of the curve for the parameter interval
	///       [<paramref name="start" />, <paramref name="end" />].
	///       </summary>
	/// <param name="start">The parameter value of the start position.</param>
	/// <param name="end">The parameter value of the end position.</param>
	/// <param name="maxNumberOfIterations">
	///       The maximum number of iterations which are taken to compute the length.
	///       </param>
	/// <param name="tolerance">
	///       The tolerance value. This method will return an approximation of the precise length.
	///       The absolute error will be less than this tolerance.
	///       </param>
	/// <returns>
	///       The approximated length of the curve in the given interval.
	///       </returns>
	public abstract TParam GetLength(TParam start, TParam end, int maxNumberOfIterations, TParam tolerance);

	/// <summary>
	///       Gets the index of the curve key <i>before</i> or at the given parameter value.
	///       </summary>
	/// <param name="parameter">The parameter value.</param>
	/// <returns>The index of the curve key or <c>-1</c> if no suitable curve key exists.</returns>
	public abstract int GetKeyIndex(TParam parameter);

	/// <summary>
	///       Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
	///       </summary>
	/// <param name="index">
	///       The zero-based index at which <paramref name="item" /> should be inserted.
	///       </param>
	/// <param name="item">
	///       The object to insert. The value can be null for reference types.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero. Or <paramref name="index" /> is greater than 
	///       <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />.
	///       </exception>
	protected override void InsertItem(int index, TCurveKey item)
	{
		if (item == null)
		{
			throw new ArgumentNullException();
		}
		base.InsertItem(index, item);
	}

	/// <summary>
	///       Determines whether the given parameter corresponds to a mirrored oscillation loop.
	///       </summary>
	/// <param name="parameter">The parameter value.</param>
	/// <returns>
	///   <see langword="true" /> if the parameter is in a mirrored oscillation loop; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public abstract bool IsInMirroredOscillation(TParam parameter);

	/// <summary>
	///       Handles pre- and post-looping by changing the given parameter so that it lies on the curve.
	///       </summary>
	/// <param name="parameter">The parameter value.</param>
	/// <returns>The modified parameter value.</returns>
	public abstract TParam LoopParameter(TParam parameter);

	/// <summary>
	///       Replaces the element at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the element to replace.</param>
	/// <param name="item">
	///       The new value for the element at the specified index.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero. Or <paramref name="index" /> is greater than 
	///       <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />.
	///       </exception>
	protected override void SetItem(int index, TCurveKey item)
	{
		if (item == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(PiecewiseCurve<, , >.SetItem(int, TCurveKey));
					}
					throw new ArgumentNullException();
				}
			}
		}
		base.SetItem(index, item);
	}

	/// <summary>
	///       Sorts the curve keys in the collection by their parameter (see
	///       <see cref="P:DigitalRune.Mathematics.Interpolation.CurveKey`2.Parameter" />).
	///       </summary>
	public void Sort()
	{
		TCurveKey[] array = Items.OrderBy((TCurveKey c8f1b13f3e698dc6357eae9af1da14536) => c8f1b13f3e698dc6357eae9af1da14536.Parameter).ToArray();
		Clear();
		TCurveKey[] array2 = array;
		foreach (TCurveKey item in array2)
		{
			Add(item);
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(PiecewiseCurve<, , >.Sort());
			}
			return;
		}
	}

	/// <summary>
	///       Generates an object from its XML representation.
	///       </summary>
	/// <param name="reader">
	///       The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized. 
	///       </param>
	protected void ReadXml(XmlReader reader)
	{
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2765));
		string value = reader.ReadContentAsString();
		PreLoop = (CurveLoopType)Enum.Parse(typeof(CurveLoopType), value, ignoreCase: false);
		reader.ReadEndElement();
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2780));
		value = reader.ReadContentAsString();
		PostLoop = (CurveLoopType)Enum.Parse(typeof(CurveLoopType), value, ignoreCase: false);
		reader.ReadEndElement();
		int num = int.Parse(reader[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2797)]);
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2808));
		for (int i = 0; i < num; i++)
		{
			Type type = Type.GetType(reader.GetAttribute(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2827)));
			reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2836));
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			TCurveKey item = (TCurveKey)xmlSerializer.Deserialize(reader);
			Add(item);
			reader.ReadEndElement();
		}
		reader.ReadEndElement();
	}

	/// <summary>
	///       Converts an object into its XML representation.
	///       </summary>
	/// <param name="writer">
	///       The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized. 
	///       </param>
	protected void WriteXml(XmlWriter writer)
	{
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2765));
		writer.WriteValue(PreLoop.ToString());
		writer.WriteEndElement();
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2780));
		writer.WriteValue(PostLoop.ToString());
		writer.WriteEndElement();
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2808));
		writer.WriteStartAttribute(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2797));
		writer.WriteValue(Count);
		writer.WriteEndAttribute();
		using (List<TCurveKey>.Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TCurveKey current = enumerator.Current;
				Type type = current.GetType();
				writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2836));
				writer.WriteStartAttribute(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2827));
				writer.WriteValue(type.AssemblyQualifiedName);
				writer.WriteEndAttribute();
				XmlSerializer xmlSerializer = new XmlSerializer(type);
				xmlSerializer.Serialize(writer, current);
				writer.WriteEndElement();
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(PiecewiseCurve<, , >.WriteXml(XmlWriter));
				}
				break;
			}
		}
		writer.WriteEndElement();
	}

	/// <summary>
	///       Returns an enumerator that iterates through the curve keys of the 
	///       <see cref="T:DigitalRune.Mathematics.Interpolation.PiecewiseCurveF`2" />. 
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for <see cref="T:DigitalRune.Mathematics.Interpolation.PiecewiseCurveF`2" />.
	///       </returns>
	public new List<TCurveKey>.Enumerator GetEnumerator()
	{
		return ((List<TCurveKey>)Items).GetEnumerator();
	}

	[CompilerGenerated]
	private static TParam cc6f16206f768b819b6ee5df82fc4c764(TCurveKey c8f1b13f3e698dc6357eae9af1da14536)
	{
		return c8f1b13f3e698dc6357eae9af1da14536.Parameter;
	}
}
