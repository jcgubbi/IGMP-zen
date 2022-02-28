using ZenLib;
using static ZenLib.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace ICMP_test
{

    public class IcmpPacket
    {
        public byte Type;
        public byte Code;
        public ushort Csum;

        public uint SeqNum;
        public uint Identifier;


        public Zen<IcmpPacket> Create(
            Zen<byte> type,
            Zen<byte> code,
            Zen<ushort> cs,
            Zen<uint> seq_num,
            Zen<uint> id)
        {
            return Language.Create<IcmpPacket>(
               ("Type", type),
               ("Code", code),
               ("Csum", cs),
               ("SeqNum", seq_num),
               ("Identifier", id)
                );
        }

        public override string ToString()
        {
            return $"Type: {Type}\nCode: {Code}\nCsum: {Csum}\nSeqNum: {SeqNum}\nId: {Identifier}\n";
        }
    }

    public static class IcmpPacketExtensions
    {
        
        public static Zen<byte> GetTyp(this Zen<IcmpPacket> dg) => dg.GetField<IcmpPacket, byte>("Type");

        
        public static Zen<byte> GetCode(this Zen<IcmpPacket> dg) => dg.GetField<IcmpPacket, byte>("Code");


        public static Zen<ushort> GetCsum(this Zen<IcmpPacket> dg) => dg.GetField<IcmpPacket, ushort>("Csum");


        public static Zen<uint> GetSeqNum(this Zen<IcmpPacket> dg) => dg.GetField<IcmpPacket, uint>("SeqNum");

        public static Zen<uint> GetIdentifier(this Zen<IcmpPacket> dg) => dg.GetField<IcmpPacket, uint>("Identifier");

        
    }
}
