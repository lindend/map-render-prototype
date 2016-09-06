using Ionic.Zlib;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Tiles.FileFormat
{
    [ProtoContract]
    public class ChunkFileFormat
    {
        [ProtoMember(1, IsRequired = true)]
        public int Width { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public int Height { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public int Resolution { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public LayerFormat[] Layers { get; set; }

        public static ChunkFileFormat Deserialize(Stream stream)
        {
            stream.ReadByte();
            stream.ReadByte();
            using (var zipStream = new DeflateStream(stream, CompressionMode.Decompress))
            {
                return Serializer.Deserialize<ChunkFileFormat>(zipStream);
            }
        }
    }
}
