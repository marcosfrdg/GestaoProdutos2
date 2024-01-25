using System;
using System.ComponentModel;

namespace Domain.Enums
{
    public enum Status
    {
        [Description("Ativo")]
        Ativo,
        [Description("Inativo")]
        Inativo
    }
}

namespace Domain.Enums
{
    public static class StatusExtensions
    {
        public static string GetDescription(this Status status)
        {
            var field = status.GetType().GetField(status.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? status.ToString() : attribute.Description;
        }
    }
}