using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FW.Common.Helpers;

namespace FW.Data.Infrastructure
{
    public static class CustomEntityTypeConfigurationData
    {
        public static Dictionary<string, string> StaticCommentMap { get; set; }
        public static Dictionary<string, List<PropertyInfo>> FullTextIndexMap { get; set; }

        static CustomEntityTypeConfigurationData()
        {
            StaticCommentMap = new Dictionary<string, string>();
            FullTextIndexMap = new Dictionary<string, List<PropertyInfo>>();
        }
    }

    public class CustomEntityTypeConfiguration<TEntityType> : EntityTypeConfiguration<TEntityType> where TEntityType : class
    {
        public CustomEntityTypeConfiguration() : base()
        {
            CommentMap = new Dictionary<string, string>();
        }

        public Dictionary<string, string> CommentMap { get; set; }


        public EntityTypeConfiguration<TEntityType> PropertyComment<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string comment)
        {
            var pi = propertyExpression.GetPropertyInfo();
            if (!this.CommentMap.ContainsKey(pi.Name))
            {
                this.CommentMap.Add(pi.Name, comment);
            }
            else
            {
                //this.CommentMap[pi.Name] = comment;
                throw new Exception($"Comment for [{GetType().FullName}.{pi.Name}] property has already existed");
            }
            return this;
        }

        public EntityTypeConfiguration<TEntityType> CommonPropertyComment<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string comment)
        {
            var pi = propertyExpression.GetPropertyInfo();
            if (!CustomEntityTypeConfigurationData.StaticCommentMap.ContainsKey(pi.Name))
            {
                CustomEntityTypeConfigurationData.StaticCommentMap.Add(pi.Name, comment);
            }
            else
            {
                //StaticCommentMap[pi.Name] = comment;
                //throw new Exception($"Comment for common [{pi.Name}] property has already existed");
            }
            return this;
        }

        public EntityTypeConfiguration<TEntityType> EntityTypeComment(string comment)
        {
            string TypeName = this.GetType().BaseType.GetGenericArguments()[0].FullName;

            if (!this.CommentMap.ContainsKey(TypeName))
            {
                this.CommentMap.Add(TypeName, comment);
            }
            else
            {
                this.CommentMap[TypeName] = comment;
            }
            return this;
        }

        public EntityTypeConfiguration<TEntityType> FullTextSeachIndexFor<TProperty>(string ftsIndexName, params Expression<Func<TEntityType, TProperty>>[] propertyExpressions)
        {

            if (CustomEntityTypeConfigurationData.FullTextIndexMap?.ContainsKey(ftsIndexName) == true)
            {
                return this;
            }

            if (propertyExpressions != null)
            {
                var colList = new List<PropertyInfo>();
                foreach (var proExpr in propertyExpressions)
                {
                    var pi = proExpr.GetPropertyInfo();
                    colList.Add(pi);
                }

                CustomEntityTypeConfigurationData.FullTextIndexMap.Add(ftsIndexName, colList);
            }
            return this;
        }

        public EntityTypeConfiguration<TEntityType> FullTextSeachIndexForEntity<TProperty>(string ftsIndexName, params Expression<Func<TEntityType, TProperty>>[] exclusivePropExpressions)
        {
            if (CustomEntityTypeConfigurationData.FullTextIndexMap?.ContainsKey(ftsIndexName) == true)
            {
                return this;
            }

            var exclusivePropInfoMap = exclusivePropExpressions?.Select(pe => pe.GetPropertyInfo())?.ToDictionary(pi => pi.Name);

            var entityType = this.GetType().BaseType.GetGenericArguments()?[0];
            var entityPropInfoList = entityType?.GetProperties();

            if (entityPropInfoList != null)
            {
                var colList = new List<PropertyInfo>();
                foreach (var pi in entityPropInfoList)
                {
                    if (exclusivePropInfoMap?.ContainsKey(pi.Name) == true)
                    {
                        continue;
                    }

                    colList.Add(pi);
                }

                CustomEntityTypeConfigurationData.FullTextIndexMap.Add(ftsIndexName, colList);
            }
            return this;
        }
    }
}
