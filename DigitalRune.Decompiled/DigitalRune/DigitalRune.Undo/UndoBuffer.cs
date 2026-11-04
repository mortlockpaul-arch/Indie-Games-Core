using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;

namespace DigitalRune.Undo;

/// <summary>
///       Implements an undo/redo buffer.
///       </summary>
public class UndoBuffer : INotifyPropertyChanged
{
	private int cd8a13d2992bd70411893fa7aa5c4ddf5;

	private int c7556981c85c52dbcc32db915f11c1266;

	private EventHandler<EventArgs> c6b9cd9f3b1d12f983b1be9ad655dd79c;

	private EventHandler<EventArgs> c1cac4fbd10788112dceb2b32691c860c;

	private PropertyChangedEventHandler cf14c30cc64f3225eb1142f93eaadf3cb;

	private bool c6ecd3e54fa426f9ecec4b3904bd35ad7;

	private int c82820f2c37db998d1298b52e1dae5b9a = int.MaxValue;

	[CompilerGenerated]
	private Deque<IUndoableOperation> c81393a0b1cbe898343c70da701d8019b;

	[CompilerGenerated]
	private Deque<IUndoableOperation> c641f36a857b4131129fc8679adae5665;

	/// <summary>
	///       Gets or sets a value indicating whether this <see cref="T:DigitalRune.Undo.UndoBuffer" /> accepts changes.
	///       </summary>
	public bool AcceptChanges
	{
		get
		{
			return c6ecd3e54fa426f9ecec4b3904bd35ad7;
		}
		set
		{
			if (c6ecd3e54fa426f9ecec4b3904bd35ad7 != value)
			{
				c6ecd3e54fa426f9ecec4b3904bd35ad7 = value;
				OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6850)));
			}
		}
	}

	/// <summary>
	///       Gets a value indicating whether there are operations on the undo stack.
	///       </summary>
	public bool CanUndo => InternalUndoStack.Count > 0;

	/// <summary>
	///       Gets a value indicating whether there are operations on the redo stack.
	///       </summary>
	public bool CanRedo => InternalRedoStack.Count > 0;

	/// <summary>
	///       Gets a value indicating whether an undo group is open.
	///       </summary>
	public bool IsUndoGroupOpen => cd8a13d2992bd70411893fa7aa5c4ddf5 != 0;

	/// <summary>
	///       Gets or sets the max number of undo steps stored in the undo buffer.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public int SizeLimit
	{
		get
		{
			return c82820f2c37db998d1298b52e1dae5b9a;
		}
		set
		{
			if (value < 0)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(UndoBuffer.set_SizeLimit(int));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6877));
					}
				}
			}
			if (c82820f2c37db998d1298b52e1dae5b9a == value)
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
				c82820f2c37db998d1298b52e1dae5b9a = value;
				OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6962)));
				if (IsUndoGroupOpen)
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
					cf99c523eccae61a44f0582441d9f803d();
					return;
				}
			}
		}
	}

	private Deque<IUndoableOperation> InternalUndoStack
	{
		[CompilerGenerated]
		get
		{
			return c81393a0b1cbe898343c70da701d8019b;
		}
		[CompilerGenerated]
		set
		{
			c81393a0b1cbe898343c70da701d8019b = value;
		}
	}

	private Deque<IUndoableOperation> InternalRedoStack
	{
		[CompilerGenerated]
		get
		{
			return c641f36a857b4131129fc8679adae5665;
		}
		[CompilerGenerated]
		set
		{
			c641f36a857b4131129fc8679adae5665 = value;
		}
	}

	/// <summary>
	///       Gets the undo stack.
	///       </summary>
	public ReadOnlyCollection<IUndoableOperation> UndoStack => new ReadOnlyCollection<IUndoableOperation>(InternalUndoStack);

	/// <summary>
	///       Gets the redo stack.
	///       </summary>
	public ReadOnlyCollection<IUndoableOperation> RedoStack => new ReadOnlyCollection<IUndoableOperation>(InternalRedoStack);

	/// <summary>
	///       Occurs after an operation is undone.
	///       </summary>
	public event EventHandler<EventArgs> OperationUndone
	{
		add
		{
			EventHandler<EventArgs> eventHandler = c6b9cd9f3b1d12f983b1be9ad655dd79c;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c6b9cd9f3b1d12f983b1be9ad655dd79c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<EventArgs> eventHandler = c6b9cd9f3b1d12f983b1be9ad655dd79c;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c6b9cd9f3b1d12f983b1be9ad655dd79c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	/// <summary>
	///       Occurs after an operation is redone.
	///       </summary>
	public event EventHandler<EventArgs> OperationRedone
	{
		add
		{
			EventHandler<EventArgs> eventHandler = c1cac4fbd10788112dceb2b32691c860c;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c1cac4fbd10788112dceb2b32691c860c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(UndoBuffer.add_OperationRedone(EventHandler<EventArgs>));
				}
				return;
			}
		}
		remove
		{
			EventHandler<EventArgs> eventHandler = c1cac4fbd10788112dceb2b32691c860c;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c1cac4fbd10788112dceb2b32691c860c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(UndoBuffer.remove_OperationRedone(EventHandler<EventArgs>));
				}
				return;
			}
		}
	}

	/// <summary>
	///       Occurs when a property value changes.
	///       </summary>
	public event PropertyChangedEventHandler PropertyChanged
	{
		add
		{
			PropertyChangedEventHandler propertyChangedEventHandler = cf14c30cc64f3225eb1142f93eaadf3cb;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref cf14c30cc64f3225eb1142f93eaadf3cb, value2, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(UndoBuffer.add_PropertyChanged(PropertyChangedEventHandler));
				}
				return;
			}
		}
		remove
		{
			PropertyChangedEventHandler propertyChangedEventHandler = cf14c30cc64f3225eb1142f93eaadf3cb;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref cf14c30cc64f3225eb1142f93eaadf3cb, value2, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Undo.UndoBuffer" /> class.
	///       </summary>
	public UndoBuffer()
	{
		InternalUndoStack = new Deque<IUndoableOperation>();
		InternalRedoStack = new Deque<IUndoableOperation>();
		AcceptChanges = true;
	}

	/// <summary>
	///       Begins a new undo group.
	///       </summary>
	public void BeginUndoGroup()
	{
		if (cd8a13d2992bd70411893fa7aa5c4ddf5 == 0)
		{
			c7556981c85c52dbcc32db915f11c1266 = 0;
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6981)));
		}
		cd8a13d2992bd70411893fa7aa5c4ddf5++;
	}

	/// <summary>
	///       Ends an undo group and puts the group of operations onto the <see cref="T:DigitalRune.Undo.UndoBuffer" />.
	///       </summary>
	/// <param name="groupDescription">
	///       The description of the undo group. See <see cref="P:DigitalRune.Undo.IUndoableOperation.Description" /> for a
	///       more detailed description. (The parameter is ignored if this is a nested undo group.)
	///       </param>
	/// <exception cref="T:System.InvalidOperationException">
	///       There are no undo groups. (<see cref="M:DigitalRune.Undo.UndoBuffer.BeginUndoGroup" /> has not been called.)
	///       </exception>
	public void EndUndoGroup(object groupDescription)
	{
		if (cd8a13d2992bd70411893fa7aa5c4ddf5 == 0)
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
						__ldtoken(UndoBuffer.EndUndoGroup(object));
					}
					throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7012));
				}
			}
		}
		cd8a13d2992bd70411893fa7aa5c4ddf5--;
		if (cd8a13d2992bd70411893fa7aa5c4ddf5 != 0)
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
			if (c7556981c85c52dbcc32db915f11c1266 <= 1)
			{
				return;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				Add(new cf0df5c209e083b66f7ecb11929369cb6(InternalUndoStack, c7556981c85c52dbcc32db915f11c1266, groupDescription));
				OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6981)));
				return;
			}
		}
	}

	private void c69a755b47c9e43829406698600994f02()
	{
		if (!IsUndoGroupOpen)
		{
			return;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(UndoBuffer.c69a755b47c9e43829406698600994f02());
			}
			cd8a13d2992bd70411893fa7aa5c4ddf5 = 0;
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7073));
		}
	}

	private void cf99c523eccae61a44f0582441d9f803d()
	{
		c69a755b47c9e43829406698600994f02();
		while (InternalUndoStack.Count > c82820f2c37db998d1298b52e1dae5b9a)
		{
			InternalUndoStack.DequeueTail();
		}
		while (InternalRedoStack.Count > c82820f2c37db998d1298b52e1dae5b9a)
		{
			InternalRedoStack.DequeueTail();
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
				__ldtoken(UndoBuffer.cf99c523eccae61a44f0582441d9f803d());
			}
			return;
		}
	}

	/// <summary>
	///       Undoes the last operation.
	///       </summary>
	public void Undo()
	{
		c69a755b47c9e43829406698600994f02();
		if (InternalUndoStack.Count <= 0)
		{
			return;
		}
		IUndoableOperation undoableOperation = InternalUndoStack.DequeueHead();
		InternalRedoStack.EnqueueHead(undoableOperation);
		undoableOperation.Undo();
		OnOperationUndone();
		if (InternalUndoStack.Count == 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(UndoBuffer.Undo());
			}
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7158)));
		}
		if (InternalRedoStack.Count == 1)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7173)));
		}
		cf99c523eccae61a44f0582441d9f803d();
	}

	/// <summary>
	///       Redoes the last undone operation.
	///       </summary>
	public void Redo()
	{
		c69a755b47c9e43829406698600994f02();
		if (InternalRedoStack.Count <= 0)
		{
			return;
		}
		IUndoableOperation undoableOperation = InternalRedoStack.DequeueHead();
		InternalUndoStack.EnqueueHead(undoableOperation);
		undoableOperation.Do();
		OnOperationRedone();
		if (InternalRedoStack.Count == 0)
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
			if (1 == 0)
			{
				__ldtoken(UndoBuffer.Redo());
			}
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7173)));
		}
		if (InternalUndoStack.Count == 1)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7158)));
		}
		cf99c523eccae61a44f0582441d9f803d();
	}

	/// <summary>
	///       Adds an operation to the undo buffer.
	///       </summary>
	/// <param name="operation">The operation.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="operation" /> is <see langword="null" />.
	///       </exception>
	public void Add(IUndoableOperation operation)
	{
		if (operation == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(UndoBuffer.Add(IUndoableOperation));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7188));
				}
			}
		}
		if (!AcceptChanges)
		{
			return;
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			InternalUndoStack.EnqueueHead(operation);
			if (IsUndoGroupOpen)
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
				c7556981c85c52dbcc32db915f11c1266++;
			}
			if (InternalUndoStack.Count == 1)
			{
				OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7158)));
			}
			c6d92ad2222123fab13e99b653559e25f();
			if (IsUndoGroupOpen)
			{
				return;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				cf99c523eccae61a44f0582441d9f803d();
				return;
			}
		}
	}

	/// <summary>
	///       Clears the undo buffer.
	///       </summary>
	public void ClearAll()
	{
		c69a755b47c9e43829406698600994f02();
		c6d92ad2222123fab13e99b653559e25f();
		c5a89b77a3dc400d470487aac960450ee();
		c7556981c85c52dbcc32db915f11c1266 = 0;
	}

	private void c6d92ad2222123fab13e99b653559e25f()
	{
		if (InternalRedoStack.Count <= 0)
		{
			return;
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
				__ldtoken(UndoBuffer.c6d92ad2222123fab13e99b653559e25f());
			}
			InternalRedoStack.Clear();
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7173)));
			return;
		}
	}

	private void c5a89b77a3dc400d470487aac960450ee()
	{
		if (InternalUndoStack.Count <= 0)
		{
			return;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(UndoBuffer.c5a89b77a3dc400d470487aac960450ee());
			}
			InternalUndoStack.Clear();
			OnPropertyChanged(new PropertyChangedEventArgs(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7158)));
			return;
		}
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Undo.UndoBuffer.OperationUndone" /> event.
	///       </summary>
	protected void OnOperationUndone()
	{
		c6b9cd9f3b1d12f983b1be9ad655dd79c?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Undo.UndoBuffer.OperationRedone" /> event.
	///       </summary>
	protected void OnOperationRedone()
	{
		EventHandler<EventArgs> eventHandler = c1cac4fbd10788112dceb2b32691c860c;
		if (eventHandler == null)
		{
			return;
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
				__ldtoken(UndoBuffer.OnOperationRedone());
			}
			eventHandler(this, EventArgs.Empty);
			return;
		}
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Undo.UndoBuffer.PropertyChanged" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///       The <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> describing the property that has changed.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="eventArgs" /> is <see langword="null" />.
	///       </exception>
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
	{
		if (eventArgs == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(UndoBuffer.OnPropertyChanged(PropertyChangedEventArgs));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7207));
				}
			}
		}
		PropertyChangedEventHandler propertyChangedEventHandler = cf14c30cc64f3225eb1142f93eaadf3cb;
		if (propertyChangedEventHandler == null)
		{
			return;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			propertyChangedEventHandler(this, eventArgs);
			return;
		}
	}
}
