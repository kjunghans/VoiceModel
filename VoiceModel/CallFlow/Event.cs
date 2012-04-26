using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public class Event : IEquatable<Event>
    {
        string Name { get; set; }

        public Event(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Event)) return false;
            return this.Name.Equals(((Event)obj).Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public static bool operator ==(Event a, Event b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Event a, Event b)
        {
            return !(a == b);
        }

        public bool Equals(Event other)
        {
            if (other == null) return false;
            else return this.Name.Equals(other.Name);
        }

    }
}
