using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class VoiceModels : IVoiceModels
    {
        Dictionary<string, VoiceModel> _views = new Dictionary<string, VoiceModel>();

        public void Add(VoiceModel doc)
        {
            _views.Add(doc.id, doc);
        }

        public VoiceModel Get(string id, string json)
        {
            VoiceModel doc;
            if (id == null || id.Equals(string.Empty))
            {
                doc = new Exit("error","View ID is empty. Probably missing transition in call flow definition");
            }
            else if (!_views.TryGetValue(id, out doc))
            {
                doc = new Exit("error", "Error finding document with id of " + id);
            }

            return doc.BuildModel(json);

        }
    }
}
