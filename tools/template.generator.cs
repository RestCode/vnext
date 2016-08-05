using System.Linq;
using System.Text;
using System.Collections.Generic;
using WebApiProxy.Core.Infrastructure;
using WebApiProxy.Core.Models;

namespace Lekker
{
    public class FanieClass
    {

public Metadata Metadata { get; set; }

        private System.Text.StringBuilder __sb;

        private void Write(string text) {
            __sb.Append(text);
        }

        private void WriteLine(string text) {
            __sb.AppendLine(text);
        }

        public string TransformText()
        {
            __sb = new System.Text.StringBuilder();
__sb.Append(@"

(function($) {
	""use strict"";

	if (!$) {
		throw ""jQuery is required"";
	}

	$.proxies = $.proxies || { 
		baseUrl: """);
__sb.Append( this.Metadata.Host );
__sb.Append(@"""
	};

	function getQueryString(params, queryString) {
		queryString = queryString || """";
		for(var prop in params) {
			if (params.hasOwnProperty(prop)) {
				var val = getArgValue(params[prop]);
				if (val === null) continue;

				if ("""" + val === ""[object Object]"") {
					queryString = getQueryString(params[prop], queryString);
					continue;
				}

				if (queryString.length) {
					queryString += ""&"";
				} else {
					queryString += ""?"";
				}
				queryString = queryString + prop + ""="" +val;
			}
		}
		return queryString;
	}

	function getArgValue(val) {
		if (val === undefined || val === null) return null;
		return val;
	}

	function invoke(url, type, urlParams, body) {
		//url += getQueryString(urlParams);

		var ajaxOptions = $.extend({}, this.defaultOptions, {
			url: $.proxies.baseUrl + url,
			type: type,
			beforeSend : function(xhr) {
				if (typeof(webApiAuthToken) != ""undefined"" && webApiAuthToken.length > 0)
					xhr.setRequestHeader(""Authorization"", ""Bearer "" + webApiAuthToken);
			},
		});

		if (body) {
			ajaxOptions.data = body;
		}

		if (this.antiForgeryToken) {
			var token = $.isFunction(this.antiForgeryToken) ? this.antiForgeryToken() : this.antiForgeryToken;
			if (token) {
				ajaxOptions.headers = ajaxOptions.headers || {};
			}
		}
	
		return $.ajax(ajaxOptions);
	}

	function defaultAntiForgeryTokenAccessor() {
		return $(""input[name=__RequestVerificationToken]"").val();
	}

	function endsWith(str, suffix) {
		return str.indexOf(suffix, str.length - suffix.length) !== -1;
	}

	function appendPathDelimiter(url){
		if(!endsWith(url, '/')){
			return url + '/';
		}
		
		return url;
	}

	/* Proxies */

	");
 foreach(var definition in this.Metadata.Definitions) { __sb.Append(@"
	$.proxies.");
__sb.Append( definition.Name.ToLower() );
__sb.Append(@" = {
		defaultOptions: {},
		antiForgeryToken: defaultAntiForgeryTokenAccessor,
");
 foreach(var method in definition.ActionMethods) { __sb.Append(@"

");

	var allParameters = method.UrlParameters.AsEnumerable();
	
	if (method.BodyParameter != null) {
		allParameters = allParameters.Concat(new [] { method.BodyParameter });
	}
	var selectedParameters = allParameters.Where(m => m != null).Select(m => m.Name).ToList();
	selectedParameters.Add("options");

	var parameterList = string.Join(",", selectedParameters);

	
	
	var url = ("\"" + method.Url.Replace("{", "\" + ").Replace("}", " + \"") + "\"").Replace(" + \"\"","");

__sb.Append(@"


	");
__sb.Append( method.Name.ToCamelCasing() );
__sb.Append(@": function(");
__sb.Append(parameterList);
__sb.Append(@") {
		 var defaults = { fields: [] };
         var settings = $.extend({}, defaults, options || {});
		 var url = ");
__sb.Append( url );
__sb.Append(@";

		 if(settings.fields.length > 0) {
		    url +=  url.indexOf(""?"") == -1 ? ""?"" : ""&"";
			url += ""fields="" + settings.fields.join();
		 }

		return invoke.call(this, url, """);
__sb.Append( method.Type.ToString().ToLower() );
__sb.Append(@""", 
		");
 if (method.UrlParameters.Any()) { __sb.Append(@"
			{
			");
 foreach (var parameter in method.UrlParameters) { __sb.Append(@"
				");
__sb.Append( parameter.Name );
__sb.Append(@": ");
__sb.Append( parameter.Name );
__sb.Append(@",
			");
 } __sb.Append(@"
			}
		");
 } else { __sb.Append(@"
			{}
		");
 } __sb.Append(@"
		");
 if (method.BodyParameter != null) { __sb.Append(@"
			, ");
__sb.Append( method.BodyParameter.Name );
__sb.Append(@");
		");
 } else { __sb.Append(@"
			);
		");
 } __sb.Append(@"
	},
");
 } __sb.Append(@" 
};
	");
 } __sb.Append(@"
}(jQuery));

");

            return __sb.ToString();
        }
    }
}