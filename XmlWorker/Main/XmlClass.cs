using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlWorker
{
    public interface XmlClass
    {
        DataTree GetXmlData();
        void SetXmlData(DataTree data);
    }
}
