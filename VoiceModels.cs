using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class VoiceModels : IVoiceModels
    {
        Dictionary<string, VxmlDocument> _views = new Dictionary<string, VxmlDocument>();

        public void Add(VxmlDocument doc)
        {
            _views.Add(doc.id, doc);
        }

        public VxmlDocument Get(string id, string json)
        {
            VxmlDocument doc;
            if (!_views.TryGetValue(id, out doc))
            {
                doc = new Say("error", "Error finding document with id of " + id);
            }
            doc.json = json;
            //return View(doc.ViewName, doc);
            return doc;

        }
    }
}
