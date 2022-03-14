using ZenLib;
using static ZenLib.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace ICMP_test
{

    public class IgmpPacket
    {
        public byte VersionType;
        public byte Unused;
        public ushort Csum;
        public uint GroupAddress;

        public Zen<IgmpPacket> Create(
            Zen<byte> version_type,
            Zen<byte> unused,
            Zen<ushort> cs,
            Zen<uint> group_addr)
        {
            return Language.Create<IgmpPacket>(
               ("VersionType", version_type),
               ("Unused", unused),
               ("Csum", cs),
               ("GroupAddress", group_addr)
                );
        }

        public override string ToString()
        {
            return $"VersionType: {VersionType}\nUnused: {Unused}\nCsum: {Csum}\nGroupAddress: {GroupAddress}\n";
        }
    }

    public static class IgmpPacketExtensions
    {

        public static Zen<byte> GetVersionType(this Zen<IgmpPacket> dg) => dg.GetField<IgmpPacket, byte>("VersionType");


        public static Zen<byte> GetCode(this Zen<IgmpPacket> dg) => dg.GetField<IgmpPacket, byte>("Unused");


        public static Zen<ushort> GetCsum(this Zen<IgmpPacket> dg) => dg.GetField<IgmpPacket, ushort>("Csum");

        
        public static Zen<uint> GetSeqNum(this Zen<IgmpPacket> dg) => dg.GetField<IgmpPacket, uint>("GroupAddress");

    }
}