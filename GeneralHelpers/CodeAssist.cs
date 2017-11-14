using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMaker.GeneralHelpers
{
    public class CodeAssist
    {
        public static Dictionary<int, string> combineDictionary(Dictionary<int, string> dataDic, Dictionary<int, string> addDic)
        {
            foreach (var data in dataDic)
            {
                addDic.Add(data.Key, data.Value);
            }
            return addDic;
        }
        public static Dictionary<int, string> combineDictionaryWithIds (Dictionary<int, string> dataDic, Dictionary<int, string> addDic)
        {
            int id = addDic.Count();
            foreach (var data in dataDic)
            {
                addDic.Add(id, data.Value);
                id++;
            }
            return addDic;
        }
    }
}
