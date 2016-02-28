using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Datatables.Extensions.Namespaces
{
	public static class NamespaceExtensions
	{
		#region Datatables (extension method making @Url.Datatables()...)
		public static Datatables Datatables(this UrlHelper helper)
		{
			return new Datatables(helper);
		}
		#endregion
	}
}
