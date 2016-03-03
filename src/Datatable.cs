using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Datatables.Extensions
{
	public class Datatable
	{
		private readonly string _selector;
		private readonly string _url;
		private Namespaces.Datatables _datatables;
		private readonly bool _filter;

		private IList<string> _columns;

		public Datatable(Namespaces.Datatables datatables, string selector, string url, bool filter = true)
		{
			_selector = selector;
			_url = url;
			_datatables = datatables;
			_filter = filter;
			_columns = new List<string>();
		}

		public Datatable AddColumn(string name)
		{
			_columns.Add(InsertComma(string.Format("{{ 'data': '{0}' }}", name)));
			return this;
		}

		public Datatable AddLinkColumn(string name, string actionName, string controllerName, object routeValues = null)
		{
			return AddLinkColumn(name, actionName, controllerName, null, routeValues);
		}

		public Datatable AddLinkColumn(string name, string actionName, string controllerName, string areaName = null, object routeValues = null)
		{
			RouteValueDictionary attributes = new RouteValueDictionary(routeValues);
			if (!string.IsNullOrEmpty(areaName))
			{
				attributes.Add("area", areaName);
			}
			var url = UrlHelper.GenerateUrl(null, actionName, controllerName, attributes, _datatables.UrlHelper.RouteCollection, _datatables.UrlHelper.RequestContext, true);

			_columns.Add(InsertComma(string.Format(@"{{ 'data': '{0}', 'render': function ( data, type, full, meta ) {{
				return '<a href=""{1}"">' + data + '</a>';
					}}
				}}", name, url.Replace("%7bid%7d", "' + full.Id + '"))));
			return this;
		}

		public Datatable AddFormatColumn(string name, string format)
		{
			string returnValue;
			// "{0} %"
			// data + ' %' 
			if (format.StartsWith("{0}"))
			{
				returnValue = string.Format("{0}'", format.Replace("{0}", "data + '"));
			}
			// "$ {0}"
			// '$ ' + data
			else if (format.EndsWith("{0}"))
			{
				returnValue = string.Format("'{0}", format.Replace("{0}", "' + data"));
			}
			// "$ {0} %"
			// '$ ' + data + ' %'
			else
			{
				returnValue = string.Format("'{0}'", format.Replace("{0}", " ' + data + '"));
			}

			_columns.Add(InsertComma(string.Format(@"{{ 'data': '{0}', 'render': function ( data, type, full, meta ) {{ return {1}; }} }}", name, returnValue)));
			return this;
		}

		public Datatable AddYesNoColumn(string name, string trueValue, string falseValue)
		{
			_columns.Add(InsertComma(string.Format(@"{{ 'data': '{0}', 'render': function ( data, type, full, meta ) {{
							if (data == true) {{
								return '{1}';
							}} else {{
								return '{2}';
							}}
						}} }}", name, trueValue, falseValue)));
			return this;
		}

		public MvcHtmlString Initialize()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat(@"$('{0}').DataTable({{
				'serverSide': true,
				'processing': true,
				'filter': {1},
				'sorting': [],
				'ajax': '{2}',
				'columns': [", _selector, _filter.ToString().ToLower(), _url);

			foreach (string column in _columns)
			{
				sb.AppendLine(column);
			}

			sb.Append(@"]});");
			return new MvcHtmlString(sb.ToString());
		}

		private string InsertComma(string column)
		{
			if (_columns.Count > 0)
			{
				return string.Format(",{0}", column);
			}
			return column;
		}
	}
}
