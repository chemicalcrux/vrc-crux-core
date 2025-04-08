using System;
using System.Collections;
using System.Collections.Generic;

namespace Crux.Core.Runtime
{
    /// <summary>
    /// Unity 2022.3 does not let you apply a property drawer to an
    /// entire list.
    /// <br/><br/>
    /// This is a workaround: a class that contains a single list. It
    /// implicitly converts into a list and implements IEnumerable&gt;T&lt;,
    /// so it can generally be used like a list.
    /// </summary>
    /// <typeparam name="T">The type to store in the list</typeparam>
    [Serializable]
    public class DecoratedList<T> : IEnumerable<T>
    {
        public List<T> list;

        public static implicit operator List<T>(DecoratedList<T> decoratedList)
        {
            return decoratedList.list;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}