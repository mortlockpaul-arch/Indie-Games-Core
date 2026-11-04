using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a shape consisting of several other geometric objects.
///       </summary>
[Serializable]
public class CompositeShape : Shape
{
	private ISpatialPartition<int> _partition;

	/// <summary>
	///       Gets the child geometric objects.
	///       </summary>
	public NotifyingCollection<IGeometricObject> Children { get; private set; }

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint
	{
		get
		{
			if (Children.Count > 0)
			{
				IGeometricObject geometricObject = Children[0];
				return geometricObject.Pose.ToWorldPosition(geometricObject.Shape.InnerPoint * geometricObject.Scale);
			}
			return Vector3F.Zero;
		}
	}

	/// <summary>
	///       Gets or set the spatial partition used to improve the performance of geometric queries.
	///       </summary>
	public ISpatialPartition<int> Partition
	{
		get
		{
			return _partition;
		}
		set
		{
			Func<int, Aabb> func = null;
			if (_partition == value)
			{
				return;
			}
			if (_partition != null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(CompositeShape.set_Partition(ISpatialPartition<int>));
				}
				_partition.GetAabbForItem = null;
				_partition.Clear();
			}
			_partition = value;
			if (_partition == null)
			{
				return;
			}
			ISpatialPartition<int> partition = _partition;
			if (func == null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				func = (int cdeb496a0c3d046b6bc838defe2d220d0) => Children[cdeb496a0c3d046b6bc838defe2d220d0].Aabb;
			}
			partition.GetAabbForItem = func;
			_partition.Clear();
			int count = Children.Count;
			for (int num = 0; num < count; num++)
			{
				_partition.Add(num);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" /> class.
	///       </summary>
	public CompositeShape()
	{
		Children = new NotifyingCollection<IGeometricObject>(allowNull: false, allowDuplicates: false);
		Children.CollectionChanged += cc3ef9dac86e0d10822e53f51a09cfb64;
	}

	internal void c56d88ced32e38076203610b9aa5a84d4(ISpatialPartition<int> c828d0c1f4103275cf37fe54b9af3f709)
	{
		if (c828d0c1f4103275cf37fe54b9af3f709 == null)
		{
			return;
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
				__ldtoken(CompositeShape.c56d88ced32e38076203610b9aa5a84d4(ISpatialPartition<int>));
			}
			_partition = c828d0c1f4103275cf37fe54b9af3f709;
			_partition.GetAabbForItem = (int cdeb496a0c3d046b6bc838defe2d220d0) => Children[cdeb496a0c3d046b6bc838defe2d220d0].Aabb;
			int count = Children.Count;
			if (_partition.Count == count)
			{
				return;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				_partition.Clear();
				for (int num = 0; num < count; num++)
				{
					_partition.Add(num);
				}
				_partition.Update(forceRebuild: false);
				return;
			}
		}
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new CompositeShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		CompositeShape compositeShape = (CompositeShape)sourceShape;
		foreach (IGeometricObject child in compositeShape.Children)
		{
			Children.Add((IGeometricObject)child.Clone());
		}
		if (compositeShape.Partition != null)
		{
			Partition = (ISpatialPartition<int>)compositeShape.Partition.Clone();
		}
	}

	/// <summary>
	///       Computes the axis-aligned bounding box (AABB) for this shape positioned in world space using
	///       the given scale and <see cref="T:DigitalRune.Geometry.Pose" />.
	///       </summary>
	/// <param name="scale">
	///       The scale factor by which the shape should be scaled. The scaling is applied in the
	///       shape's local space before the pose is applied. 
	///       </param>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> of the shape. This pose defines how the shape should be positioned
	///       in world space.
	///       </param>
	/// <returns>
	///       The AABB of the shape positioned in world space.
	///       </returns>
	public override Aabb GetAabb(Vector3F scale, Pose pose)
	{
		if (Partition != null)
		{
			return Partition.Aabb.GetAabb(scale, pose);
		}
		int count = Children.Count;
		if (count == 0)
		{
			return new Aabb(pose.Position, pose.Position);
		}
		if (scale.X == scale.Y && scale.Y == scale.Z)
		{
			IGeometricObject geometricObject = Children[0];
			Pose pose2 = new Pose(geometricObject.Pose.Position * scale.X, geometricObject.Pose.Orientation);
			Aabb aabb = geometricObject.Shape.GetAabb(scale.X * geometricObject.Scale, pose * pose2);
			for (int i = 1; i < count; i++)
			{
				geometricObject = Children[i];
				pose2 = new Pose(geometricObject.Pose.Position * scale.X, geometricObject.Pose.Orientation);
				aabb.Grow(geometricObject.Shape.GetAabb(scale.X * geometricObject.Scale, pose * pose2));
			}
			return aabb;
		}
		Aabb aabb2 = Children[0].Aabb;
		for (int j = 1; j < count; j++)
		{
			aabb2.Grow(Children[j].Aabb);
		}
		return aabb2.GetAabb(scale, pose);
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">
	///       The desired relative error for approximations (in the range [0, 1]). For example, use the 
	///       value 0.01 to get an error of about 1%.
	///       </param>
	/// <param name="iterationLimit">
	///       The iteration limit. Must be greater than or equal to 0. For most cases a small value like 4 
	///       is appropriate. 
	///       </param>
	/// <returns>The volume of this shape.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="relativeError" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="iterationLimit" /> is negative or 0.
	///       </exception>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		float num = 0f;
		using (List<IGeometricObject>.Enumerator enumerator = Children.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				IGeometricObject current = enumerator.Current;
				Vector3F vector3F = Vector3F.Absolute(current.Scale);
				num += current.Shape.GetVolume(relativeError, iterationLimit) * vector3F.X * vector3F.Y * vector3F.Z;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CompositeShape.GetVolume(float, int));
				}
				break;
			}
		}
		return num;
	}

	private void cc3ef9dac86e0d10822e53f51a09cfb64(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<IGeometricObject> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
		{
			return;
		}
		List<IGeometricObject> oldItems = c7fefeb91d4632901bd52a1f245b70db1.OldItems;
		int num = 0;
		num = oldItems.Count;
		for (int i = 0; i < num; i++)
		{
			IGeometricObject geometricObject = oldItems[i];
			geometricObject.PoseChanged -= ce495e81216e74908d006ffa40b560c26;
			geometricObject.ShapeChanged -= ce495e81216e74908d006ffa40b560c26;
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
				__ldtoken(CompositeShape.cc3ef9dac86e0d10822e53f51a09cfb64(object, CollectionChangedEventArgs<IGeometricObject>));
			}
			List<IGeometricObject> newItems = c7fefeb91d4632901bd52a1f245b70db1.NewItems;
			int num2 = 0;
			num2 = newItems.Count;
			for (int j = 0; j < num2; j++)
			{
				IGeometricObject geometricObject2 = newItems[j];
				geometricObject2.PoseChanged += ce495e81216e74908d006ffa40b560c26;
				geometricObject2.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
			}
			if (_partition != null)
			{
				_partition.Clear();
				int count = Children.Count;
				for (int k = 0; k < count; k++)
				{
					_partition.Add(k);
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			if (num == 1 && num2 == 1 && c7fefeb91d4632901bd52a1f245b70db1.OldItemsIndex == c7fefeb91d4632901bd52a1f245b70db1.NewItemsIndex)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
					{
						ShapeChangedEventArgs e = ShapeChangedEventArgs.Create(c7fefeb91d4632901bd52a1f245b70db1.OldItemsIndex);
						OnChanged(e);
						e.Recycle();
						return;
					}
					}
				}
			}
			OnChanged(ShapeChangedEventArgs.Empty);
			return;
		}
	}

	private void ce495e81216e74908d006ffa40b560c26(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		int num = Children.IndexOf(c807f682152464d11dde83a885bf87124 as IGeometricObject);
		if (Partition != null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CompositeShape.ce495e81216e74908d006ffa40b560c26(object, EventArgs));
			}
			if (num >= 0)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				Partition.Invalidate(num);
			}
			else
			{
				Partition.Invalidate();
			}
		}
		ShapeChangedEventArgs e = ShapeChangedEventArgs.Create(num);
		OnChanged(e);
		e.Recycle();
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		float largestComponent = GetAabb(Vector3F.One, Pose.Identity).Extent.LargestComponent;
		float num;
		if (Numeric.IsZero(largestComponent))
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CompositeShape.OnGetMesh(float, int));
			}
			num = Numeric.EpsilonF;
		}
		else
		{
			num = absoluteDistanceThreshold / largestComponent;
		}
		float relativeDistanceThreshold = num;
		TriangleMesh triangleMesh = new TriangleMesh();
		int count = Children.Count;
		for (int i = 0; i < count; i++)
		{
			IGeometricObject geometricObject = Children[i];
			TriangleMesh mesh = geometricObject.Shape.GetMesh(relativeDistanceThreshold, iterationLimit);
			mesh.Transform(geometricObject.Pose.ToMatrix44F() * Matrix44F.CreateScale(geometricObject.Scale));
			triangleMesh.Add(mesh, weldVerticesBruteForce: false);
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			return triangleMesh;
		}
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12891), Children.Count);
	}

	[CompilerGenerated]
	private Aabb cc8115fa4d90a38024a9d3283f6d5d47c(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		return Children[cdeb496a0c3d046b6bc838defe2d220d0].Aabb;
	}

	[CompilerGenerated]
	private Aabb c864907fd83dd9abccc91af0a00fc4160(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		return Children[cdeb496a0c3d046b6bc838defe2d220d0].Aabb;
	}
}
