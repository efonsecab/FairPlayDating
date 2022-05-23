﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class ServiceOfEntityAttribute : Attribute
    {
        public string EntityName { get; private set; }
        public Type PrimaryKeyType { get; private set; }
        public ServiceOfEntityAttribute(string entityName, Type primaryKeyType)
        {
            this.EntityName = entityName;
            this.PrimaryKeyType = primaryKeyType;
        }
    }
}
