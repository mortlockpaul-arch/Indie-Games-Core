using System;
using A;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       This collision algorithm combines 2 other algorithms: One is used for penetrating objects,
///       the other is used for closest-point queries of separated objects.
///       </summary>
public class CombinedCollisionAlgorithm : CollisionAlgorithm
{
	private readonly CollisionAlgorithm c51dc92cc91455774972f951968d2df9c;

	private readonly CollisionAlgorithm c8a4736dc19e94f0ba0ed65571a5ee3ca;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CombinedCollisionAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	/// <param name="closestPointsAlgorithm">The closest points algorithm.</param>
	/// <param name="contactAlgorithm">The contact algorithm.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="closestPointsAlgorithm" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="contactAlgorithm" /> is <see langword="null" />.
	///       </exception>
	public CombinedCollisionAlgorithm(CollisionDetection collisionDetection, CollisionAlgorithm closestPointsAlgorithm, CollisionAlgorithm contactAlgorithm)
		: base(collisionDetection)
	{
		if (closestPointsAlgorithm == null)
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
						__ldtoken(CombinedCollisionAlgorithm._002Ector(CollisionDetection, CollisionAlgorithm, CollisionAlgorithm));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13286));
				}
			}
		}
		if (contactAlgorithm == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13331));
		}
		c51dc92cc91455774972f951968d2df9c = closestPointsAlgorithm;
		c8a4736dc19e94f0ba0ed65571a5ee3ca = contactAlgorithm;
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		if (type == CollisionQueryType.ClosestPoints)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CombinedCollisionAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
					}
					c51dc92cc91455774972f951968d2df9c.ComputeCollision(contactSet, type);
					if (contactSet.HaveContact)
					{
						while (true)
						{
							switch (3)
							{
							case 0:
								break;
							default:
							{
								Contact contact = ((contactSet.Count > 0) ? contactSet[contactSet.Count - 1] : null);
								c8a4736dc19e94f0ba0ed65571a5ee3ca.ComputeCollision(contactSet, CollisionQueryType.Contacts);
								if (!contactSet.HaveContact)
								{
									while (true)
									{
										switch (3)
										{
										case 0:
											break;
										default:
											if (contact != null)
											{
												while (true)
												{
													switch (7)
													{
													case 0:
														continue;
													}
													break;
												}
												contact.PenetrationDepth = 0f - CollisionDetection.Epsilon;
												foreach (Contact item in contactSet)
												{
													if (item != contact)
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
														item.Recycle();
													}
												}
												contactSet.Clear();
												contactSet.Add(contact);
											}
											contactSet.HaveContact = false;
											return;
										}
									}
								}
								return;
							}
							}
						}
					}
					return;
				}
			}
		}
		c8a4736dc19e94f0ba0ed65571a5ee3ca.ComputeCollision(contactSet, type);
	}

	/// <summary>
	///       Gets the time of impact between two moving objects.
	///       </summary>
	/// <param name="objectA">The object A.</param>
	/// <param name="targetPoseA">The target pose of A.</param>
	/// <param name="objectB">The object B.</param>
	/// <param name="targetPoseB">The target pose of B.</param>
	/// <param name="allowedPenetration">
	///       The allowed penetration. A positive allowed penetration value makes sure that the objects 
	///       have a measurable contact at the time of impact.
	///       </param>
	/// <returns>The time of impact in the range [0, 1].</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> or <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		return c8a4736dc19e94f0ba0ed65571a5ee3ca.GetTimeOfImpact(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration);
	}
}
