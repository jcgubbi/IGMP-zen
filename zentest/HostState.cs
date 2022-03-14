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
        public bool idle_member;
        public bool delaying_member;
        public bool non_member;

        public Zen<HostState> Create(Zen<bool> idle_member, Zen<bool> delaying_member, Zen<bool> non_member)
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
        public static Zen<bool> GetIdleMember(this Zen<HostState> hs) => hs.GetField<HostState, bool>("idle_member");

        public static Zen<bool> GetDelayingMember(this Zen<HostState> hs) => hs.GetField<HostState, bool>("delaying_member");

        public static Zen<bool> GetNonMember(this Zen<HostState> hs) => hs.GetField<HostState, bool>("non_member");
    }
}
