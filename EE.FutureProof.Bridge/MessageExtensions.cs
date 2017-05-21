using System.Collections.Generic;
using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    internal static class MessageExtensions
    {
        public static IEnumerable<object> ToEnumerable(this Message m)
        {
            yield return m.Type;
            for (var i = 0u; i < m.Count; i++) yield return m[i];
        }

        public static Message ToMessage(this IEnumerable<object> message)
        {
            var enumerator = message.GetEnumerator();
            enumerator.MoveNext();

            var m = Message.Create((string)enumerator.Current);
            while (enumerator.MoveNext()) m.Add(enumerator.Current);
            return m;
        }

        public static IEnumerable<object> SetType(this IEnumerable<object> message, string type)
        {
            return message.InsertAt(-1, type);
        }

        // From http://stackoverflow.com/questions/41384035/replace-insert-delete-operations-on-ienumerable 
        public static IEnumerable<object> Add(this IEnumerable<object> message, object value)
        {
            foreach (var item in message) yield return item;

            yield return value;
        }

        public static IEnumerable<object> InsertAt(this IEnumerable<object> message, int index, object value)
        {
            var current = -1;
            foreach (var item in message)
            {
                if (current == index) yield return value;

                yield return item;
                current++;
            }
        }

        public static IEnumerable<object> ReplaceAt(this IEnumerable<object> message, int index, object value)
        {
            var current = -1;
            foreach (var item in message)
            {
                yield return current == index ? value : item;
                current++;
            }
        }

        public static IEnumerable<object> RemoveAt(this IEnumerable<object> message, int index)
        {
            var current = -1;
            foreach (var item in message)
            {
                if (current != index) yield return item;

                current++;
            }
        }
    }
}