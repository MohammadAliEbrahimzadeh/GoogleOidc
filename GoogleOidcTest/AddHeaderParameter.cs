using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GoogleOidcTest;

public class AddHeaderParameter : IOperationFilter
{
        void IOperationFilter.Apply(OpenApiOperation operation, OperationFilterContext context)
        {
                if (operation.Parameters == null)
                        operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                        Name = "Access-Control-Allow-Origin",
                        In = ParameterLocation.Header,
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                                Type = "string"
                        }
                });
        }
}
