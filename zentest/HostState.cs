namespace ICMP_test
{
    using ZenLib;
    using static ZenLib.Language;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics.CodeAnalysis;

    public sealed class HostState
    {
        public byte idle_member;
        public byte delaying_member;
        public byte non_member;

        public Zen<HostState> Create(Zen<byte> idle_member, Zen<byte> delaying_member, Zen<byte> non_member)
        {
            return Language.Create<HostState>(("idle_member", idle_member), ("delaying_member", delaying_member), ("non_member", non_member));
        }

        public override string ToString()
        {
            return $"Idle member: {idle_member}\n" +
                $"Delaying member: {delaying_member}\n" +
                $"Non member: {non_member}\n";
        }
    }

    public static class HostStateExtensions
    {
        public static Zen<byte> GetIdleMember(this Zen<HostState> hs) => hs.GetField<HostState, byte>("idle_member");

        public static Zen<byte> GetDelayingMember(this Zen<HostState> hs) => hs.GetField<HostState, byte>("delaying_member");

        public static Zen<byte> GetNonMember(this Zen<HostState> hs) => hs.GetField<HostState, byte>("non_member");
    }
}
