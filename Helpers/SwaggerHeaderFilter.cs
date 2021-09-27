using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaMiddleWare.Helpers
{
 
        public class SwaggerHeaderFilter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "CUSTOMAUTH",
                In = "header",
                Type = "string",
                Required = true,
                Default = "CUSTOM "
            });
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "MERCHANTID",
                In = "header",
                Type = "string",
                Required = true,
                Default = ""
            });
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "APIKEY",
                In = "header",
                Type = "string",
                Required = true,
                Default = ""
            });
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "APITOKEN",
                In = "header",
                Type = "string",
                Required = true,
                Default = ""
            });
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "IV",
                In = "header",
                Type = "string",
                Required = true,
                Default = ""
            });
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "ENCKEY",
                In = "header",
                Type = "string",
                Required = true,
                Default = ""
            });
        }
        
    }
}
