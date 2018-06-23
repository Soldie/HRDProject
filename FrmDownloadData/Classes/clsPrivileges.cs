namespace HRDProject
{
	/// <summary>
	/// Represent an instance of User Privileges.
	/// </summary>
	public class Privileges
	{
		#region Constructor
		/// <summary>
		/// Initialize a new instance of the ZFamily.Privileges Class.
		/// </summary>
		public Privileges()
		{
		}
		#endregion

		#region Properties
		public bool Viewed
		{
			set;
            get;
		}

		public bool Added
		{
            set;
            get;
        }

		public bool Edited
		{
            set;
            get;
        }

		public bool Deleted
		{
            set;
            get;
        }

		public bool Printed
		{
            set;
            get;
        }

		public bool Downloaded
		{
            set;
            get;
        }
		#endregion
	}
}
