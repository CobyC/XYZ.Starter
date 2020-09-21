using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace XYZ.Starter.Core
{
    public static class ServiceStackListConversionExtension
    {
        /// <summary>
        /// Convert a List of object types to another list of EntityBase types. (if possible)
        /// This is just a more convenient version of .ConvertAll(x=&gt;x.ConvertTo&lt;SomeEntity&gt;())      
        /// </summary>        
        public static IEnumerable<TOut> ConvertAllTo<TOut>(this IEnumerable<object> from) 
            where TOut : class
        {
            IEnumerable<TOut> to = from.ToList().ConvertAll(x => x.ConvertTo<TOut>());
            return to;
        }

        /// <summary>
        /// Convert a List of object types to another list of EntityBase types. (if possible)
        /// This is just a more convenient version of .ConvertAll(x=&gt;x.ConvertTo&lt;SomeEntity&gt;())      
        /// </summary>        
        public static IList<TOut> ConvertAllTo<TOut>(this IList<object> from) 
            where TOut : class
        {
            IList<TOut> to = from.ToList().ConvertAll(x => x.ConvertTo<TOut>());
            return to;
        }

        /// <summary>
        /// Convert a List of object types to another list of EntityBase types. (if possible)
        /// This is just a more convenient version of .ConvertAll(x=&gt;x.ConvertTo&lt;SomeEntity&gt;())       
        /// </summary>        
        public static List<TOut> ConvertAllTo<TOut>(this List<object> from) 
            where TOut : class
        {
            List<TOut> to = from.ToList().ConvertAll(x => x.ConvertTo<TOut>());
            return to;
        }
    }
}
