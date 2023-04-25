using System.Collections.Generic;
using System.Linq;
using Models;
using Utilities;

namespace Manager
{
    public class DateManager : Singleton<DateManager>
    {
        private readonly List<ShowInfo> _showInfos = new List<ShowInfo>();

        public void SaveInfo(ShowInfo info)
        {
            _showInfos.Add(info);
        }

        public ShowInfo GetInfo(string infoName)
        {
            return _showInfos.FirstOrDefault(t => infoName == t.Account);
        }


    }
}