using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Tiles.FileFormat
{
    [ProtoContract]
    public class LayerFormat
    {
        [ProtoMember(1, IsRequired = true)]
        public int[] Tiles { get; set; }
    }
}
