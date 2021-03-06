using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore.Infrastructure.Html;
using OrchardCore.Scripting;

namespace StatCan.OrchardCore.Scripting
{
    public class FormsGlobalMethodsProvider : IGlobalMethodProvider
    {
        private readonly GlobalMethod _formAsJsonObject;

        public FormsGlobalMethodsProvider()
        {
            _formAsJsonObject = new GlobalMethod
            {
                Name = "requestFormAsJsonObject",
                Method = serviceProvider => (Func<JObject>)(() =>
                {
                    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                    var sanitizer = serviceProvider.GetRequiredService<IHtmlSanitizerService>();

                    return new JObject(httpContextAccessor.HttpContext.Request.Form.Select(
                      field =>
                      {
                          var arr = field.Value.ToArray();
                          if (arr.Length == 1)
                          {
                              return new JProperty(field.Key, sanitizer.Sanitize(field.Value[0]));
                          }
                          return new JProperty(field.Key, JArray.FromObject(arr.Select(o => sanitizer.Sanitize(o))));
                      }
                    ).ToArray());
                }
                )
            };
        }

        public IEnumerable<GlobalMethod> GetMethods()
        {
            return new[] { _formAsJsonObject };
        }
    }
}
