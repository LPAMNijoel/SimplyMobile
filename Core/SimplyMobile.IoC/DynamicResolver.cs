﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.IoC
{
    public class DynamicResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, Func<object>> registeredServices;

        public DynamicResolver()
        {
            this.registeredServices = new Dictionary<Type, Func<object>>();
        }

        public virtual T GetService<T>() where T : class
        {
            Func<object> getter;
            if (this.registeredServices.TryGetValue(typeof(T), out getter))
            {
                return getter.Invoke() as T;
            }

            return null;
        }

        public virtual IEnumerable<T> GetServices<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public virtual IDependencyResolver AddDynamic<T>(Func<T> getter) where T : class
        {
            this.registeredServices.Add(typeof(T), getter);
            return this;
        }
    }
}
