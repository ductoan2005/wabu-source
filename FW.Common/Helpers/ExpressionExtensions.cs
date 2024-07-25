using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common.Helpers
{
    public static class ExpressionExtensions
    {
        public static Expression RemoveConvert(this Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert
                   || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }


        public static PropertyInfo GetPropertyInfoOfType<TSource>(Expression<Func<TSource, object>> propertyExpression)
        {
            PropertyInfo propInfo = null;

            try
            {
                Type type = typeof(TSource);

                MemberExpression member = propertyExpression.Body as MemberExpression;

                if (member == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a method, not a property.",
                        propertyExpression.ToString()));

                propInfo = member.Member as PropertyInfo;
                if (propInfo == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a field, not a property.",
                        propertyExpression.ToString()));

                if (type != propInfo.ReflectedType &&
                    !type.IsSubclassOf(propInfo.ReflectedType))
                    throw new ArgumentException(string.Format(
                        "Expresion '{0}' refers to a property that is not from type {1}.",
                        propertyExpression.ToString(),
                        type));
            }
            catch
            {
            }

            return propInfo;
        }

        public static PropertyInfo GetPropertyInfo<TSource>(this TSource sourceObj, Expression<Func<TSource, object>> propertyExpression)
        {
            PropertyInfo propInfo = null;

            try
            {
                Type type = typeof(TSource);

                if (propertyExpression.NodeType == ExpressionType.MemberAccess)
                {
                    propInfo = (propertyExpression as MemberExpression).Member as PropertyInfo;
                }
                else if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
                {
                    propInfo = (propertyExpression.Body as MemberExpression).Member as PropertyInfo;
                }
                else if (propertyExpression.Body.NodeType == ExpressionType.Convert)
                {
                    UnaryExpression ue = propertyExpression.Body as UnaryExpression;
                    if (ue.Operand.NodeType == ExpressionType.MemberAccess)
                    {
                        propInfo = (ue.Operand as MemberExpression).Member as PropertyInfo;
                    }
                }

                if (propInfo == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a field, not a property.",
                        propertyExpression.ToString()));


                if (type != propInfo.ReflectedType &&
                    !type.IsSubclassOf(propInfo.ReflectedType))
                    throw new ArgumentException(string.Format(
                        "Expresion '{0}' refers to a property that is not from type {1}.",
                        propertyExpression.ToString(),
                        type));
            }
            catch
            {
            }

            return propInfo;
        }

        public static PropertyInfo GetPropertyInfo(this Expression expression)
        {
            expression = expression.RemoveConvert();

            LambdaExpression lambda = expression as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("LambdaExpression");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("LambdaExpression");

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;

            return propInfo;
        }

        public static PropertyInfo GetPropertyInfo<TSource>(Expression<Func<TSource, object>> propertyExpression)
        {
            PropertyInfo propInfo = null;

            try
            {
                Type type = typeof(TSource);

                propertyExpression.GetPropertyInfo();

                if (propInfo == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a field, not a property.",
                        propertyExpression.ToString()));


                if (type != propInfo.ReflectedType &&
                    !type.IsSubclassOf(propInfo.ReflectedType))
                    throw new ArgumentException(string.Format(
                        "Expresion '{0}' refers to a property that is not from type {1}.",
                        propertyExpression.ToString(),
                        type));
            }
            catch
            {
            }

            return propInfo;
        }

        /// <summary>
        /// Gets the property information list.
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TProperty">The type of the t property.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertyLambdas">The property lambdas.</param>
        /// <returns>List&lt;PropertyInfo&gt;.</returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static List<PropertyInfo> GetPropertyInfoListOfType<TSource>(params Expression<Func<TSource, object>>[] propertyLambdas)
        {
            List<PropertyInfo> propInfoList = null;

            if (propertyLambdas == null || propertyLambdas.Length == 0)
            {
                return propInfoList;
            }

            Type type = typeof(TSource);
            propInfoList = new List<PropertyInfo>();


            foreach (var propertyLambda in propertyLambdas)
            {

                MemberExpression member = propertyLambda.Body as MemberExpression;

                if (member == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a method, not a property.",
                        propertyLambda.ToString()));

                PropertyInfo propInfo = member.Member as PropertyInfo;
                if (propInfo == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a field, not a property.",
                        propertyLambda.ToString()));

                if (type != propInfo.ReflectedType &&
                    !type.IsSubclassOf(propInfo.ReflectedType))
                    throw new ArgumentException(string.Format(
                        "Expresion '{0}' refers to a property that is not from type {1}.",
                        propertyLambda.ToString(),
                        type));

                propInfoList.Add(propInfo);
            }
            return propInfoList;
        }

        public static List<CustomPropInfo<T>> GetCustomPropertyInfoListOfType<T>(params Expression<Func<T, object>>[] expressions)
        //public static List<PropertyInfo> GetPropertyInfoListOfType<TSource>(params LambdaExpression[] propertyLambdas)
        {
            List<CustomPropInfo<T>> propInfoList = null;

            if (expressions == null || expressions.Length == 0)
            {
                return propInfoList;
            }

            //Type type = typeof(TSource);
            propInfoList = new List<CustomPropInfo<T>>();


            foreach (var propertyLambda in expressions)
            {
                CustomPropInfo<T> excelPropertyInfo = new CustomPropInfo<T>();

                if (propertyLambda.NodeType == ExpressionType.MemberAccess)
                {
                    excelPropertyInfo.PropertyPath = GetFullPropertyPath(propertyLambda as MemberExpression);
                    excelPropertyInfo.PropertyInfo = (propertyLambda as MemberExpression).Member as PropertyInfo;
                }
                else if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
                {
                    excelPropertyInfo.PropertyPath = GetFullPropertyPath(propertyLambda.Body as MemberExpression);
                    excelPropertyInfo.PropertyInfo = (propertyLambda.Body as MemberExpression).Member as PropertyInfo;
                }
                else if (propertyLambda.Body.NodeType == ExpressionType.Convert)
                {
                    UnaryExpression ue = propertyLambda.Body as UnaryExpression;
                    if (ue.Operand.NodeType == ExpressionType.MemberAccess)
                    {
                        excelPropertyInfo.PropertyPath = GetFullPropertyPath(ue.Operand as MemberExpression);
                        excelPropertyInfo.PropertyInfo = (ue.Operand as MemberExpression).Member as PropertyInfo;
                    }
                }

                if (excelPropertyInfo.PropertyInfo == null)
                    throw new ArgumentException(string.Format(
                        "Expression '{0}' refers to a field, not a property.",
                        propertyLambda.ToString()));

                excelPropertyInfo.DisplayName = GetDisplayName(excelPropertyInfo.PropertyInfo);
                excelPropertyInfo.GetValue = Expression.Lambda<Func<T, object>>(propertyLambda.Body, new ParameterExpression[] { propertyLambda.Parameters[0] }).Compile();
                //if (excelPropertyInfo.PropertyInfo.CanWrite)
                //{
                //    //ParameterExpression paramValue = Expression.Parameter(typeof(object), "value");

                //    //Expression body = Expression.Assign(propertyLambda.Body, Expression.Convert(paramValue, propertyLambda.Body.Type));

                //    //excelPropertyInfo.SetValue = Expression.Lambda<Action<T, object>>(propertyLambda.Body, new ParameterExpression[] { propertyLambda.Parameters[0], paramValue }).Compile();

                //    excelPropertyInfo.SetValue = GetMutator<T>(excelPropertyInfo.PropertyPath);
                //}
                propInfoList.Add(excelPropertyInfo);


            }

            return propInfoList;
        }

        public static CustomPropInfo<T> GetCustomPropertyInfo<T>(Expression<Func<T, object>> expression, bool needVerifyPropertyInfo = true, bool needVerifySetValueMethod = false)
        //public static List<PropertyInfo> GetPropertyInfoListOfType<TSource>(params LambdaExpression[] propertyLambdas)
        {
            CustomPropInfo<T> cusPropInfo = new CustomPropInfo<T>();

            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                cusPropInfo.PropertyPath = GetFullPropertyPath(expression as MemberExpression);
                cusPropInfo.PropertyInfo = (expression as MemberExpression).Member as PropertyInfo;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                cusPropInfo.PropertyPath = GetFullPropertyPath(expression.Body as MemberExpression);
                cusPropInfo.PropertyInfo = (expression.Body as MemberExpression).Member as PropertyInfo;
            }
            else if (expression.Body.NodeType == ExpressionType.Convert)
            {
                UnaryExpression ue = expression.Body as UnaryExpression;
                if (ue.Operand.NodeType == ExpressionType.MemberAccess)
                {
                    cusPropInfo.PropertyPath = GetFullPropertyPath(ue.Operand as MemberExpression);
                    cusPropInfo.PropertyInfo = (ue.Operand as MemberExpression).Member as PropertyInfo;
                }
            }

            if (cusPropInfo.PropertyInfo == null)
            {
                if (needVerifyPropertyInfo)
                {
                    throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.",
                                                expression.ToString()));
                }
            }
            else
            {
                cusPropInfo.DisplayName = GetDisplayName(cusPropInfo.PropertyInfo);
            }

            cusPropInfo.GetValue = Expression.Lambda<Func<T, object>>(expression.Body, new ParameterExpression[] { expression.Parameters[0] }).Compile();

            return cusPropInfo;
        }

        public static string GetFullPropertyPath(MemberExpression me)
        {
            Stack<string> propertyName = null;
            if (me != null)
            {
                propertyName = new Stack<string>();
            }
            while (me != null)
            {
                propertyName.Push(me.Member.Name);
                me = me.Expression as MemberExpression;

            }

            if (propertyName != null)
            {
                return string.Join(".", propertyName);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetDisplayName<T, TProperty>(T model, Expression<Func<T, TProperty>> expression)
        {
            Type type = typeof(T);

            MemberExpression memberExpression = (MemberExpression)expression.Body;
            string propertyName = ((memberExpression.Member is PropertyInfo) ? memberExpression.Member.Name : null);

            // First look into attributes on a type and it's parents
            DisplayNameAttribute attr;
            attr = (DisplayNameAttribute)type.GetProperty(propertyName).GetCustomAttribute<DisplayNameAttribute>();

            // Look for [MetadataType] attribute in type hierarchy
            // http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayNameAttribute)property.GetCustomAttribute<DisplayNameAttribute>();
                    }
                }
            }
            return (attr != null) ? attr.DisplayName : String.Empty;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <returns>System.String.</returns>
        public static string GetDisplayName(PropertyInfo prop)
        {
            // First look into attributes on a type and it's parents
            DisplayNameAttribute attr;
            attr = (DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();

            return (attr != null) ? attr.DisplayName : String.Empty;
        }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <returns>System.Int32.</returns>
        public static int GetMaxLength(this PropertyInfo prop)
        {
            if (prop == null)
            {
                return 0;
            }

            MaxLengthAttribute attr;
            attr = (MaxLengthAttribute)prop.GetCustomAttribute<MaxLengthAttribute>();

            return (attr != null) ? attr.Length : 0;
        }

        /// <summary>
        /// Gets the length of the string.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <returns>System.Int32.</returns>
        public static int GetStringLength(this PropertyInfo prop)
        {
            if (prop == null)
            {
                return 0;
            }

            StringLengthAttribute attr;
            attr = (StringLengthAttribute)prop.GetCustomAttribute<StringLengthAttribute>();

            return (attr != null) ? attr.MaximumLength : 0;
        }

        public static Func<T, object> GetAccessor<T>(string path)
        {
            ParameterExpression paramObj = Expression.Parameter(typeof(T), "obj");

            Expression body = paramObj;
            foreach (string property in path.Split('.'))
            {
                body = Expression.PropertyOrField(body, property);
            }

            Expression conversion = Expression.Convert(body, typeof(object));
            //func = Expression.Lambda<Func<T, Object>>(conversion, parameterExpression).Compile();

            return Expression.Lambda<Func<T, object>>(conversion, new ParameterExpression[] { paramObj }).Compile();
        }

        public static Action<T, object> GetMutator<T>(string path)
        {
            ParameterExpression paramObj = Expression.Parameter(typeof(T), "obj");
            ParameterExpression paramValue = null;

            Expression body = paramObj;
            foreach (string property in path.Split('.'))
            {
                body = Expression.PropertyOrField(body, property);
            }

            paramValue = Expression.Parameter(typeof(object), "value");

            body = Expression.Assign(body, Expression.Convert(paramValue, body.Type));

            return Expression.Lambda<Action<T, object>>(body, new ParameterExpression[] { paramObj, paramValue }).Compile();
        }

        public static CustomPropInfo<T> GetItem<T>(this Dictionary<string, CustomPropInfo<T>> propMap, string key)
        {
            if (propMap != null && propMap.ContainsKey(key))
            {
                return propMap[key];
            }
            return null;
        }

        public static bool IsNumericType(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }


    }

    public class CustomPropInfo<T>
    {
        public string PropertyPath { get; set; }

        public string DisplayName { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public Func<T, object> GetValue { get; set; }

        public Action<T, object> SetValue { get; set; }
    }
}
