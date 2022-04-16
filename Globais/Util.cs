using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ParkingReserve.Globais
{
    public static class Util
    {
            public static dynamic ToDynamic(this object value)
            {
                IDictionary<string, object> expando = new ExpandoObject();

                var props = TypeDescriptor.GetProperties(value.GetType());
                foreach (PropertyDescriptor property in props)
                    expando.Add(property.Name, property.GetValue(value));

                return expando as ExpandoObject;
            }
    }
}
