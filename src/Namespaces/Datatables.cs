using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Datatables.Extensions.Namespaces
{
	/// <summary>
	/// Namespace holder for making @Url.Datatables()...
	/// </summary>
	public class Datatables
	{
		#region UrlHelper
		public UrlHelper UrlHelper
		{
			get;
			private set;
		}
		#endregion

		#region Datatables (ctor)
		public Datatables(UrlHelper urlHelper)
		{
			this.UrlHelper = urlHelper;
		}
		#endregion
	}
}
