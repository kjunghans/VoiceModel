using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;

namespace VoiceModel
{
    public class Var
    {
        private CallFlow.CallFlow _cf;
        private string _name;

        public string Name { get { return _name; } }
        public string Value { get { return _cf[_name]; } set { _cf[_name] = value; } }

        public Var(CallFlow.CallFlow cf, string name, string value = "")
        {
            _cf = cf;
            _cf[name] = value;
            _name = name;
        }
    }
}
