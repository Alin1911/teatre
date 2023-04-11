using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;

/// <summary>
/// This is and example of a smart enum, you can modify it however you see fit.
/// Note that the class is decorated with a JsonConverter attribute so that it is properly serialized as a JSON.
/// </summary>
[JsonConverter(typeof(SmartEnumNameConverter<UserRoleEnum, string>))]
public sealed class UserRoleEnum : SmartEnum<UserRoleEnum, string>
{
    public static readonly UserRoleEnum Admin = new(nameof(Admin), "Admin");
    public static readonly UserRoleEnum Personnel = new(nameof(Personnel), "Personnel");
    public static readonly UserRoleEnum Client = new(nameof(Client), "Client");

    private UserRoleEnum(string name, string value) : base(name, value)
    {
    }
}
