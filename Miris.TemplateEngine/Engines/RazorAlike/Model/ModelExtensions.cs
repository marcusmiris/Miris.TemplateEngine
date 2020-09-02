using System;
using System.Collections.Generic;
using System.Linq;

namespace Miris.TemplateEngine.Engines.RazorAlike.Model
{
    public static class ModelExtensions
    {

        public static object GetReference(
            this IDictionary<string, object> model,
            string collectionReference)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(collectionReference)) throw new ArgumentNullException(nameof(collectionReference));

            var navigationProperties = collectionReference.Split('.').Select(_ => _.Trim());
            var firstNavigationProperty = navigationProperties.First().Trim('@');

            if (!model.TryGetValue(firstNavigationProperty, out var obj))
            {
                throw new ArgumentException($"Not found bind variable: `{ firstNavigationProperty }`.");
            }

            var tailNailgationProperties = navigationProperties.Skip(1).ToList();
            foreach (var navProp in tailNailgationProperties)
            {
                var memberInfo = obj.GetType().GetProperty(navProp);
                if (memberInfo == null)
                {
                    throw new MissingMemberException(obj.GetType().FullName, navProp);
                }

                obj = memberInfo.GetValue(obj);
            }

            return obj;
        }

        public static bool HasDefined(
            this IDictionary<string, object> model,
            string propertyName)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            return model.ContainsKey(propertyName);

        }

        public static IDictionary<string, object> Clone(
            this IDictionary<string, object> model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return new Dictionary<string, object>(model);
        }

        public static void DefineVariable(
            this IDictionary<string, object> model,
            string variableName,
            object value)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (model.HasDefined(variableName))
            {
                throw new Exception($"A variable `{ variableName }` is already defined in the scope.");
            }

            model.Add(variableName, value);
        }

    }
}
