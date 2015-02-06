using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexPaper.AspNet.Codem
{
    public class DBHelper
    {
        private Dictionary<Guid, string> Docs = new Dictionary<Guid, string>();

        public DBHelper()
        {
            Docs.Add(new Guid("380A3EF2B27FCF17AB7D9005D2B63FDE"), @"2015\01\380A3EF2B27FCF17AB7D9005D2B63FDE\banche.pdf");
            Docs.Add(new Guid("57B1AF6029C072D2DA6BCBF609BF47D0"), @"2015\01\57B1AF6029C072D2DA6BCBF609BF47D0\CSharpLanguageSpecification.pdf");
        }

        public DocModel getDoc(Guid DocID)
        {
            DocModel docm = new DocModel();
            foreach (var m in Docs)
            {
                if (m.Key.Equals(DocID))
                {
                    docm.DocID = m.Key;
                    docm.Preview_URL = m.Value;
                    break;
                }
            }
            return docm;
        }
    }

    public class DocModel
    {
        public Guid DocID { get; set; }
        public string Preview_URL { get; set; }
    }
}