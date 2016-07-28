using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FixedColumnFileCollection
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class FixedColumnDataMember : Attribute
    {
        public FixedColumnDataMember(int Length, int Start, [CallerMemberName] string Name = null)
        {
            this.Length = Length;
            this.Start = Start;
            this.Name = Name;
        }

        public string Name { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
    }
}
