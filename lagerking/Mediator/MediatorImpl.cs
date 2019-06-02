using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lagerking
{
    static public class MediatorImpl
    {
        static IDictionary<string, List<Action<object>>> listObjects = new Dictionary<string, List<Action<object>>>();

        static public void Register(string token, Action<object> callback)
        {
            if (!listObjects.ContainsKey(token))
            {
                var list = new List<Action<object>>();
                list.Add(callback);
                listObjects.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in listObjects[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    listObjects[token].Add(callback);
            }
        }

        static public void NotifyColleagues(string msg, object args)
        {
            
            if (listObjects.ContainsKey(msg))
                foreach (var raise in listObjects[msg])
                    raise(args);
        }
    }
}
