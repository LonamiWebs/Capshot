/// <copyright file="SerializableColor.cs" company="LonamiWebs">
///   Copyright (c) 2016 All Rights Reserved
/// </copyright>
/// <author>Lonami Exo</author>
/// <date>5/23/2016 10:22:36 AM</date>
/// <summary>Serializable colour for saving it under settings</summary>

using System.Drawing;
using System.Runtime.Serialization;

namespace Capshot
{
    [DataContract]
    public class SerializableColor
    {
        [DataMember]
        public byte A { get; set; }
        [DataMember]
        public byte R { get; set; }
        [DataMember]
        public byte G { get; set; }
        [DataMember]
        public byte B { get; set; }

        public static implicit operator Color(SerializableColor c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static implicit operator SerializableColor(Color c)
        {
            return new SerializableColor
            {
                A = c.A,
                R = c.R,
                G = c.G,
                B = c.B
            };
        }
    }
}
