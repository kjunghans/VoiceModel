using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace VoiceModel
{
    public static class GrammarHelper
    {

        public static string GrammarRulesToString(Grammar grammar)
        {
            string grm = string.Empty;
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            Encoding encoding = Encoding.ASCII;
            settings.Encoding = encoding;
            XmlWriter writer = XmlWriter.Create(ms, settings);

            XmlSerializer s = new XmlSerializer(typeof(Grammar));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            s.Serialize(writer, grammar, ns);
            grm = encoding.GetString(ms.ToArray());
            return grm;
        }

        public static MvcHtmlString Grammar(this HtmlHelper helper, Grammar grammar)
        {
             return new MvcHtmlString(GrammarRulesToString(grammar));
        }

        public static MvcHtmlString BuiltinToVxml(this HtmlHelper helper, BuiltinGrammar grammar)
        {
            string sgrammar = grammar.Type.ToString();
            if (grammar.Type == BuiltinGrammar.GrammarType.digits)
            {
                if (grammar.MinLength > 0)
                    if (grammar.MaxLength > 0)
                        sgrammar += "?minlength=" + grammar.MinLength.ToString() + ";maxlength=" + grammar.MaxLength.ToString();
                    else
                        sgrammar += "?minlength=" + grammar.MinLength.ToString();
                else
                    if (grammar.MaxLength > 0)
                        sgrammar += "?maxlength=" + grammar.MaxLength.ToString();
            }
            return new MvcHtmlString(sgrammar);
        }

    }
}
