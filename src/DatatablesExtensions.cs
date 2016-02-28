using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Datatables.Extensions.Namespaces;

namespace Datatables.Extensions
{
	public static class DatatablesExtensions
	{
		public static Datatable CreateDatatable(this Namespaces.Datatables datatable, string selector, string actionName, string controllerName)
		{
			return CreateDatatable(datatable, selector, actionName, controllerName, string.Empty);
		}

		public static Datatable CreateDatatable(this Namespaces.Datatables datatable, string selector, string actionName, string controllerName, string areaName)
		{
			RouteValueDictionary attributes = new RouteValueDictionary();
			attributes.Add("area", areaName);
			var url = UrlHelper.GenerateUrl(null, actionName, controllerName, attributes, datatable.UrlHelper.RouteCollection, datatable.UrlHelper.RequestContext, true);

			return new Datatable(datatable, selector, url);
		}
	}
}
