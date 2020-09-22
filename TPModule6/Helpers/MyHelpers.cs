﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using TPModule6.Models;

namespace TPModule6.Helpers
{
    public static class MyHelpers
    {
        public static IHtmlString CustomSubmit(this HtmlHelper htmlHelper, String name)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"form - group\">");
            result.Append("<div class=\"col-md-offset-2 col-md-10\">");
            result.Append($"<input type = \"submit\" value=\"{ name }\" class=\"btn btn - default\" />");
            result.Append("</div>");
            result.Append("</div>");

            return new MvcHtmlString(result.ToString());
        }

    }
}