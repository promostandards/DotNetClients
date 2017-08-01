using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest
{
    static class HierarchyPrinter
    {
        public static List<Type> validTypes;
        public static string getIndenting(int indent)
        {
            string indenting = " ";
            for (int i = 0; i <= indent * 4; i++)
            {
                indenting += " ";
            }
            return indenting;
        }

        public static void printMembersValues(PropertyInfo property, Object currentObject, ref StringBuilder output, int indent)
        {
            Type[] printableTypes = { typeof(string), typeof(decimal), typeof(DateTime), typeof(String[]), typeof(Boolean), typeof(Boolean?) };
            if (printableTypes.Contains(property.PropertyType) || property.PropertyType.IsPrimitive || property.PropertyType.GenericTypeArguments.Contains(typeof(DateTime)))
            {
                if (property.PropertyType == typeof(String[]))
                {
                    output.AppendLine(getIndenting(indent) + property.Name + " : " + ((string[])currentObject)[0]);
                }
                else
                {
                    output.AppendLine(getIndenting(indent) + property.Name + " : " + currentObject);
                }
                return;
            }
            else if (property.PropertyType.BaseType == typeof(Enum))
            {
                output.AppendLine(getIndenting(indent) + property.Name + " : " + currentObject);
                return;
            }
            else if (property.PropertyType.BaseType == typeof(Array))
            {
                output.AppendLine(getIndenting(indent) + property.Name + " Array.....");
                if (currentObject != null)
                {
                    indent++;
                    foreach (var item in (Array)currentObject)
                    {
                        output.AppendLine(getIndenting(indent) + item.GetType().Name + " Object.....");
                        var innerCurrentObjectType = item.GetType();
                        foreach (var innerProperty in innerCurrentObjectType.GetProperties())
                        {
                            if (printableTypes.Contains(innerProperty.PropertyType) || innerProperty.PropertyType.IsPrimitive || innerProperty.PropertyType.GenericTypeArguments.Contains(typeof(DateTime)))
                            {
                                output.AppendLine(getIndenting(indent + 1) + innerProperty.Name + " : " + innerProperty.GetValue(item));
                            } else if (innerProperty.GetValue(item) != null)
                            {
                                printMembersValues(innerProperty, innerProperty.GetValue(item), ref output, indent + 1);
                            }
                        }
                    }
                }
            }
            else if (currentObject != null)
            {
                var currentObjType = currentObject.GetType();
                output.AppendLine(getIndenting(indent) + currentObjType.Name + " Object.....");
                foreach (var innerProperty in currentObjType.GetProperties())
                {
                    //foreach (var existingType in validTypes)
                    //{
                    //    if (existingType.Name == property.PropertyType.Name)
                    //    {
                            printMembersValues(innerProperty, innerProperty.GetValue(currentObject), ref output, indent);
                    //    }
                    //}
                }
            }
        }

    }
}
