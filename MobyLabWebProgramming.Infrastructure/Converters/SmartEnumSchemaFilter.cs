using System.Reflection;
using Ardalis.SmartEnum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MobyLabWebProgramming.Infrastructure.Converters;

/// <summary>
/// This class is used to make the swagger compatible with the smart enums.
/// </summary>
public sealed class SmartEnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        if (!IsTypeDerivedFromGenericType(type, typeof(SmartEnum<>)) && !IsTypeDerivedFromGenericType(type, typeof(SmartEnum<,>)))
        {
            return;
        }

        var enumValues = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).Select(d => d.Name);
        var openApiValues = new OpenApiArray();
        openApiValues.AddRange(enumValues.Select(d => new OpenApiString(d)));

        schema.Type = "string";
        schema.Enum = openApiValues;
        schema.Properties = null;
        schema.AdditionalPropertiesAllowed = true;
    }

    private static bool IsTypeDerivedFromGenericType(Type typeToCheck, Type genericType)
    {
        while (true)
        {
            if (typeToCheck == typeof(object))
            {
                return false;
            }

            if (typeToCheck == null)
            {
                return false;
            }

            if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            if (typeToCheck.BaseType != null)
            {
                typeToCheck = typeToCheck.BaseType;
            }
            else
            {
                return false;
            }
        }
    }
}
