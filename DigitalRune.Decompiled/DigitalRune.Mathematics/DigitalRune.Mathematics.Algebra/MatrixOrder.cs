namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       The matrix order defines in which order the matrix elements would be stored in a 1D array.
///       </summary>
public enum MatrixOrder
{
	/// <summary>
	///       The matrix is stored in column-major order. (Example: <c>M00, M10, M01, M11, M02, M12</c>)
	///       </summary>
	ColumnMajor,
	/// <summary>
	///       The matrix is stored in row-major order. (Example: <c>M00, M01, M02, M10, M11, M12</c>)
	///       </summary>
	RowMajor
}
