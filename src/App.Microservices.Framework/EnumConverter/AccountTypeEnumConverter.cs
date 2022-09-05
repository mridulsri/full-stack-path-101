using App.Application.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Microservices.Framework.EnumConverter
{
    public class AccountTypeEnumConverter
    {
        public static void Converter()
        {
            var categoryTypeEnumConverter = new ValueConverter<UserRole, string>(
            ep => ep.ToString(),
            ep => (UserRole)Enum.Parse(typeof(UserRole), ep)
            );
        }
    }
}
